using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using DALlab4.Entities;
using Lab4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Lab4.ApiContollers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly AdventureWorks2014Context _context;
        public IConfiguration Configuration { get; }

        public TokenController(AdventureWorks2014Context context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.Customer.Any(c => c.Person.FirstName == tokenRequest.FirstName && c.Person.PersonPhone.Any(p => p.PhoneNumber == tokenRequest.Phone)))
            {
                JwtSecurityToken token = JwsTokenCreator.CreateToken(tokenRequest.FirstName,
                    Configuration["Auth:JwtSecurityKey"],
                    Configuration["Auth:ValidIssuer"],
                    Configuration["Auth:ValidAudience"]);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenStr);
            }
            return Unauthorized();
        }

    }

    [Produces("application/json")]
    [ApiVersion("2.0")]
    [Route("api/Token")]
    public class TokenV2_Controller : Controller
    {
        private readonly AdventureWorks2014Context _context;
        public IConfiguration Configuration { get; }

        public TokenV2_Controller(AdventureWorks2014Context context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.Customer.Any(c => !String.IsNullOrWhiteSpace(c.Person.LastName) && c.Person.LastName == tokenRequest.LastName))
            {
                JwtSecurityToken token = JwsTokenCreator.CreateToken(tokenRequest.LastName,
                    Configuration["Auth:JwtSecurityKey"],
                    Configuration["Auth:ValidIssuer"],
                    Configuration["Auth:ValidAudience"]);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenStr);
            }
            return Unauthorized();
        }

    }
}