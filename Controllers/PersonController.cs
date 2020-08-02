using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using reCAPTCHA.AspNetCore;

namespace ApiWithRecaptcha.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PersonController : ControllerBase
    {
        private readonly RecaptchaService recaptcha;
        private readonly RecaptchaOptions recaptchaOptions;

        public PersonController(IRecaptchaService recaptcha, IOptions<RecaptchaOptions> recaptchaOptions)
        {
            this.recaptcha = recaptcha as RecaptchaService;
            this.recaptchaOptions = recaptchaOptions.Value;
        }

        [HttpPost("recaptcha-v3")]
        public async Task<object> PostWithRecaptchaV3([FromBody] RequestBody request)
        {
            this.recaptcha.RecaptchaSettings.SecretKey = this.recaptchaOptions.V3SecretKey;
            this.recaptcha.RecaptchaSettings.SiteKey = this.recaptchaOptions.V3SiteKey;
            var result = await this.recaptcha.Validate(request.RecaptchaToken);
            if (!result.success)
            {
                return BadRequest();
            }
            return new PersonEnvelope(request.Person);
        }

        [HttpPost("recaptcha-v2")]
        public async Task<object> PostWithRecaptchaV2([FromBody] RequestBody request)
        {
            this.recaptcha.RecaptchaSettings.SecretKey = this.recaptchaOptions.V2TickBoxSecretKey;
            this.recaptcha.RecaptchaSettings.SiteKey = this.recaptchaOptions.V2TickBoxSiteKey;
            var result = await this.recaptcha.Validate(request.RecaptchaToken);
            if (!result.success)
            {
                return BadRequest();
            }
            return new PersonEnvelope(request.Person);
        }
        [HttpPost("recaptcha-v2-invisible")]
        public async Task<object> PostWithRecaptchaV2Invisible([FromBody] RequestBody request)
        {
            this.recaptcha.RecaptchaSettings.SecretKey = this.recaptchaOptions.V2InvisibleSecretKey;
            this.recaptcha.RecaptchaSettings.SiteKey = this.recaptchaOptions.V2InvisibleSiteKey;
            var result = await this.recaptcha.Validate(request.RecaptchaToken);
            if (!result.success)
            {
                return BadRequest();
            }
            return new PersonEnvelope(request.Person);
        }

    }
}