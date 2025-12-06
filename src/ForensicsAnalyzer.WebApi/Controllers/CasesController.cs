using ForensicsAnalyzer.Application.Cases;
using ForensicsAnalyzer.Contracts.Cases;
using Microsoft.AspNetCore.Mvc;

namespace ForensicsAnalyzer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CasesController : ControllerBase
{
    private readonly ICaseService _caseService;

    public CasesController(ICaseService caseService)
    {
        _caseService = caseService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CaseDto>>> Get(CancellationToken cancellationToken)
    {
        var result = await _caseService.GetAllAsync(cancellationToken);
        return Ok(result);
    }
}