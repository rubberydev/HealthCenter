
namespace HealthCenter.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        const string isRememberme = "IsRememberme";

        static readonly string stringDefault = string.Empty;

        public static string IsRememberme
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRememberme, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRememberme, value);
            }
        }

    }
}
