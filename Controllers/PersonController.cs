using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reCAPTCHA.AspNetCore;

namespace ApiWithRecaptcha.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PersonController : ControllerBase
    {
        private readonly IRecaptchaService recaptcha;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PersonController(IRecaptchaService recaptcha, IHttpContextAccessor httpContextAccessor)
        {
            this.recaptcha = recaptcha;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("recaptcha-v3")]
        public async Task<object> PostWithRecaptchaV3([FromBody] RequestBody request)
        {
            var result = await this.recaptcha.Validate(request.RecaptchaToken);
            if (!result.success)
            {
                return BadRequest();
            }
            var context = this.httpContextAccessor.HttpContext;
            return new PersonEnvelope(request.Person);
        }

        [HttpPost("recaptcha-v2")]
        public Task<PersonEnvelope> PostWithRecaptchaV2([FromBody] Person person)
        {
            return Task.FromResult(new PersonEnvelope(person));
        }

    }
}