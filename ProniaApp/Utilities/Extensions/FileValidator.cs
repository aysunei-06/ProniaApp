using ProniaApp.Models;
using ProniaApp.Utilities.Enums;

namespace ProniaApp.Utilities.Extensions
{
    public static class FileValidator
    {
        public static bool ValidateType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateSize(this IFormFile file, int size, FileSize fileSize)
        {
            switch (fileSize)
            {
                case FileSize.Kb:
                    return file.Length <= size * 1024;
                case FileSize.Mb:
                    return file.Length <= size * 1024 * 1024;
                case FileSize.Gb:
                    return file.Length <= size * 1024 * 1024 * 1024;
            }
            return false;
        }


        public async static Task<string> SaveFileAsync(this IFormFile file, params string[] root)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = string.Empty;

            for (int i = 0; i < root.Length; i++)
            {
                path = Path.Combine(path, root[i]);
            }

            path = Path.Combine(path, filename);

            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }
            return filename;
        }

        public static void DeleteFile(this string filename, params string[] root)
        {
            string path = string.Empty;
            for (int i = 0; i < root.Length; i++)
            {
                path = Path.Combine(path, root[i]);
            }
            path = Path.Combine(path, filename);

            File.Delete(path);
        }
    }
    
}