namespace ApiWithRecaptcha
{
    public class RequestBody
    {
        public Person Person { get; set; }
        public string RecaptchaToken { get; set; }
    }
}