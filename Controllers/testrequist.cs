using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class testrequist : Controller
    {


        [HttpGet]
        public string testdts()
        {
            return "ASDASDASDASDASDZXCCZXZXCZCASD";
        }


    }
}