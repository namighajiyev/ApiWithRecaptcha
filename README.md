# ApiWithRecaptcha

Asp.Net Core web api project demonstrating Google ReCaptcha token backend vaidation. To be able to use the project the Recaptcha sites must be created at [Recaptcha admin console](https://www.google.com/recaptcha/admin/create). 
For this project create 3 sites to get 3 site key & secret key pairs one for 
- reCaptcha v3
- reCaptcha v2 Tickbox
- reCaptcha v2 Invisible

## Add site key & secret key pairs to user-secrets or to appsettings.json in order to be able to run or debug the projects.
Following user-secrets or configurations must be set :
- ApiWithRecaptcha:V2TickboxRecaptchaSettings:SiteKey
- ApiWithRecaptcha:V2TickboxRecaptchaSettings:SecretKey
- ApiWithRecaptcha:V2InvisibleRecaptchaSettings:SiteKey
- ApiWithRecaptcha:V2InvisibleRecaptchaSettings:SecretKey
- ApiWithRecaptcha:RecaptchaSettings:SiteKey
- ApiWithRecaptcha:RecaptchaSettings:SecretKey

For more about user secrets see this part of [the ASP.NET Core documentation](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#enable-secret-storage)

# Frontend implementation

[See this repository](https://github.com/namik-hajiyev/recaptcha-react-sample) for frontend React app that consumes this API 
