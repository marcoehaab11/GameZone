using GameZone.Data;
using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        
        private readonly ICategoriesServices categoriesServices;
        private readonly IDevicesServices devicesServices;
        private readonly IGameServices _gameServices;




        public GamesController(ICategoriesServices categoriesServices,
            IDevicesServices devicesServices, IGameServices gameServices)
        {

            this.categoriesServices = categoriesServices;
            this.devicesServices = devicesServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _gameServices.GetById(id);
            if (game==null)
            {
                return NotFound();
            }
            return View(game);
        }
        public IActionResult Create() 
        {

            CreateGameFromViewModel viewmodel = new()
            {
                  Categories = categoriesServices.GetSelectList(),
                  Devices = devicesServices.GetSelectList()
            };
            return View(viewmodel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFromViewModel model)
        {   //Server side Validation
            if (!ModelState.IsValid)
            {
                model.Categories = categoriesServices.GetSelectList();
                model.Devices = devicesServices.GetSelectList();
                return View(model);
            }
            //Save
            await _gameServices.Create(model);
            //Save Cover to server
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetById(id);
            if (game == null)
             return NotFound();

            EditeGameFromViewModel viewModel = new()
            {
                Id=id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories = categoriesServices.GetSelectList(),
                Devices = devicesServices.GetSelectList(),
                CurrentCover=game.Cover,
            };
            return View(viewModel);

        }
        public async Task<IActionResult> Edit(EditeGameFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoriesServices.GetSelectList();
                model.Devices = devicesServices.GetSelectList();
                return View(model);
            }
            var game =await _gameServices.Update(model);

            if (model is null)
                return BadRequest();


            return RedirectToAction(nameof(Index));

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            
            var isDeleted=_gameServices.Delete(id);

            return isDeleted? Ok():BadRequest();
        }
    }
}
