using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DALlab4.Entities;

namespace Lab4.ApiContollers
{
    [Produces("application/json")]
    //[ApiVersion("1.0")]
    //swagger ne radi ako se apiVersion ne zakomentira
    [Route("api/CreditCards")]
    public class CreditCardsController : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public CreditCardsController(AdventureWorks2014Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<CreditCard> GetCreditCards()
        {
            return _context.CreditCard.Where(c => c.CardType == "SuperiorCard");
        }
    }

    [Produces("application/json")]
    //[ApiVersion("2.0")]
    //swagger ne radi ako se apiVersion ne zakomentira
    [Route("api/CreditCards")]
    public class CreditCardsControllerV2_Controller : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public CreditCardsControllerV2_Controller(AdventureWorks2014Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<CreditCard> GetCreditCards()
        {
            return _context.CreditCard.Where(c => c.CardType == "Vista");
        }
    }
}