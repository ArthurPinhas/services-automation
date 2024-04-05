using Newtonsoft.Json;

namespace GeneralUtils
{
    public class AppSettings
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NotifyTopic { get; set; }

        public static AppSettings LoadSettings(string file_path)
        {
            string jsonContent = File.ReadAllText(file_path);

            return JsonConvert.DeserializeObject<AppSettings>(jsonContent);
        }
    }
}
