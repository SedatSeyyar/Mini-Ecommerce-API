using System.Globalization;
using System.Text;

namespace ECommerce_be.Infrastructure.StaticServices
{
    public static class NameManager
    {
        public static string GenerateFileName(string path, string name)
        {
            string extension = Path.GetExtension(name);
            string withoutExtension = Path.GetFileNameWithoutExtension(name);
            string validFileName = $"{getValidFileName(withoutExtension)}";
            bool fileNameAlreadyUsed = File.Exists($"{path}\\{validFileName}{extension}");
            int index = 0;
            while (fileNameAlreadyUsed)
            {
                index++;
                fileNameAlreadyUsed = File.Exists($"{path}\\{validFileName}-{index}{extension}");
            };
            string fileNameResult = index > 0 ? $"{validFileName}-{index}{extension}" : $"{validFileName}{extension}";
            return fileNameResult;
        }

        private static string removeInvalidChars(string name)
        {
            return string.Concat(name.Split(Path.GetInvalidFileNameChars()));
        }

        private static string replaceTurkishCharacterToEnglish(string name)
        {
            return String.Join("", name.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }

        private static string getValidFileName(string filename)
        {
            filename = replaceTurkishCharacterToEnglish(filename);
            filename = removeInvalidChars(filename);
            return filename;
        }
        
    }
}
