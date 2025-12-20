using ForensicsAnalyzer.Application.Cases;
using ForensicsAnalyzer.Application.Cases.Commands.CreateCase;
using ForensicsAnalyzer.Application.Cases.Queries;
using ForensicsAnalyzer.Contracts.Cases;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

// WebApi/Controllers/CasesController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CasesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCaseCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<List<CaseDto>>> Get()
    {
        var result = await _mediator.Send(new GetCasesQuery());
        return Ok(result);
    }
}
