using System;
using System.Net.Mime;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.InputModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign_api.Controllers.Base;
using digitalsign_api.Controllers.Contracts.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign_api.Controllers.V1
{
    [ApiController]
    [Authorize]
    public class MessageController : BaseController {

        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService) {
            _messageService = messageService;
        }

        [HttpGet(template: ApiRoutes.Message.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() {
            return Ok(await _messageService.GetAsync());
        }

        [HttpGet(template: ApiRoutes.Message.GetById)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _messageService.GetAsync(id));
        }

        [HttpPost(ApiRoutes.Message.Post)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(MessageInputModel createModel)
        {
            var result = await _messageService.AddAsync(UserId, createModel);
            return CreatedAtAction(nameof(Get), new { result.Id} ,result);
        }
    }
}
