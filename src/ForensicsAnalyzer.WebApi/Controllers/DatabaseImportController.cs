using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Database.Commands;
using ForensicsAnalyzer.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DatabaseImportController : ControllerBase
{
    private readonly IMediator _mediator;

    public DatabaseImportController(IMediator mediator) => _mediator = mediator;

    [HttpPost("import")]
    [HasPermission(Permissions.Admin.ImportDatabase)]
    public async Task<IActionResult> ImportDatabase(ImportDatabaseDto request)
    {
        await _mediator.Send(new ImportDatabaseCommand(request));
        return Ok();
    }
}
