namespace ApiWithRecaptcha
{
    public class RecaptchaOptions
    {
        public string V3SiteKey { get; set; }
        public string V3SecretKey { get; set; }

        public string V2TickBoxSiteKey { get; set; }
        public string V2TickBoxSecretKey { get; set; }

        public string V2InvisibleSiteKey { get; set; }
        public string V2InvisibleSecretKey { get; set; }
    }
}