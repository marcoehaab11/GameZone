using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModel
{
    public class EditeGameFromViewModel: GameFormViewModel
    {
        public int Id { get; set; } 

        public string? CurrentCover {  get; set; }

        [AllowedExtensionsAttribute(FileSettings.AllowedExtensions)]
        [MaxFileSizaAttribute(FileSettings.MaxFileSizeInByte)]
        public IFormFile? Cover { get; set; } = default;
    }
}
