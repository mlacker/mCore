using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mCore.Application.ViewModels;
using mCore.Services.Process.Application.Definition;
using Microsoft.AspNetCore.Mvc;

namespace mCore.Services.Process.Application.Controllers
{
    [Route("api/[controller]")]
    public class ProcessController : Controller
    {
        private readonly ProcessAppService service;

        public ProcessController(ProcessAppService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagedFilterViewModel filter)
        {
            return Ok(await service.GetProcessDefinitionList(filter));
        }
    }
}
