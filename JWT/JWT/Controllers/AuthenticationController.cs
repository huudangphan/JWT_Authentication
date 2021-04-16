using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
       
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            var user = _authenticateService.Authenticate(model.username, model.password);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }
        
        [HttpGet]
        public string test()
        {
            return _authenticateService.Token();
        }
    }
}
