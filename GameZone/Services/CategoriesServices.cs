using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ApplicationDbContext context;

        public CategoriesServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return context.Categories
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
