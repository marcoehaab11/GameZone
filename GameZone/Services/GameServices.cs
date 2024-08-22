using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModel;

namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _imagesPath;

        public GameServices(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
           
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g=>g.Devices)
                .ThenInclude(d=>d.Device)
                .AsNoTracking()
                .ToList();
        }
        public async Task Create(CreateGameFromViewModel model)
        {
           
          

            Game game = new()
            {
                Name=model.Name,
                Description=model.Description,
                CategoryId=model.CategoryId,
                Cover=await SaveCover(model.Cover),
                Devices= model.SelectedDevices.Select(d=>new GameDevice { DeviceId = d}).ToList()

            };
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                 .Include(g => g.Category)
                 .Include(g => g.Devices)
                 .ThenInclude(d => d.Device)
                 .AsNoTracking()
                 .FirstOrDefault(x=>x.Id==id);
        }

        public async Task<Game?> Update(EditeGameFromViewModel model)
        {
            var Game = _context.Games
                .Include (g => g.Devices)
                .FirstOrDefault(x => x.Id == model.Id);
            var oldCover = Game.Cover;
            if (Game == null)
            {
                return null;
            }

            Game.Name = model.Name;
            Game.Description = model.Description;
            Game.CategoryId = model.CategoryId;
            Game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (model.Cover is not null)
            {
                Game.Cover = await SaveCover(model.Cover);
            }
            else
            {
                Game.Cover = oldCover;
            }

            var effRows = _context.SaveChanges();

            if (effRows > 0)
            {
                if (model.Cover != null)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return Game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, Game.Cover);
                File.Delete(cover);
                return null;
            }
        
        }


        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }
        public bool Delete(int id )
        {
            var IsDeleted =false;
            var game = _context.Games.Find(id);
            if (game == null)
              return IsDeleted;

            _context.Games.Remove(game);
            
            var effRows=_context.SaveChanges();

            if (effRows>0)
            {
                IsDeleted = true;
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }
            return IsDeleted;
        }
    }
}
