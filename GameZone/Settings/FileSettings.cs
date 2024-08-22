namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath= "/assets/images/games";
        public const string AllowedExtensions = ".jpg,.jpge ,.png, .jpg";
        public const int MaxFileSizeInMB=1;
        public const int MaxFileSizeInByte= MaxFileSizeInMB * 1024 * 1024;

    }
}
