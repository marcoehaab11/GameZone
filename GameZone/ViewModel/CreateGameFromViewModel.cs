using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModel
{
    public class CreateGameFromViewModel: GameFormViewModel
    {
        
        [AllowedExtensionsAttribute(FileSettings.AllowedExtensions)]
        [MaxFileSizaAttribute(FileSettings.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; } = default;
    }
}
