using System;
using System.Net.Mime;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.Autherization.ApplicationUser;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign_api.Contracts.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign_api.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageController : ControllerBase {

        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService) {
            _messageService = messageService;
        }

        [HttpGet(template: ApiRoutes.Message.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() {
            return Ok(_messageService.Get());
        }

        [HttpGet(template: ApiRoutes.Message.GetById)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _messageService.GetAsync(id);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Message.Post)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(MessageCreateModel createModel)
        {
            var result = await _messageService.AddAsync(createModel);
            if(!result.Success)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { result.ContainingObject.Id} ,result);
        }
    }
}
