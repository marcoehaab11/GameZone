using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly ApplicationDbContext context;

        public DevicesServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return context.Devices
              .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
              .OrderBy(c => c.Text)
              .AsNoTracking()
              .ToList();
        }
    }
}
