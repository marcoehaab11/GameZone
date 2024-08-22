using GameZone.Models;
using GameZone.ViewModel;

namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFromViewModel game);

        Task<Game?> Update(EditeGameFromViewModel game);
        bool Delete (int id);

    }
}
