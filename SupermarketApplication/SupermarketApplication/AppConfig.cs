using System.Configuration;

public static class AppConfig
{
    public static decimal SellingPriceMarkup
    {
        get
        {
            var value = ConfigurationManager.AppSettings["SellingPriceMarkup"];
            return decimal.TryParse(value, out var result) ? result : 1.2m; // Default value
        }
        set
        {
            ConfigurationManager.AppSettings["SellingPriceMarkup"] = value.ToString();
            SaveConfiguration();
        }
    }

    private static void SaveConfiguration()
    {
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        config.AppSettings.Settings["SellingPriceMarkup"].Value = SellingPriceMarkup.ToString();
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}
