using digitalsign.application.Services.Interface;
using digitalsign_api.Contracts.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MessageController : ControllerBase {

    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService) {
        _messageService = messageService;
    }

    [HttpGet(template: ApiRoutes.Message.Get)]
    public IActionResult Get() {
        return Ok(_messageService.Get());
    }
}
