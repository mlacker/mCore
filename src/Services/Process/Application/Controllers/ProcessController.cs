using System;
using System.Threading.Tasks;
using mCore.Domain.Entities;
using mCore.Services.Process.Application.Definition;
using mCore.Services.Process.Application.Definition.ViewModels;
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
        public async Task<IActionResult> Get(PaginatedFilter filter)
        {
            return Ok(await service.GetPaginatedList(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await service.GetProcessDefinition(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProcessDefinitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.CreateProcessDefinition(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProcessDefinitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.ModifyProcessDefinition(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await service.DeleteProcessDefinition(id);

            return Ok();
        }

        [HttpGet("categories")]
        public async Task<IActionResult> CategoryOptions()
        {
            return Ok(await service.CategoryOptions());
        }
    }
}
