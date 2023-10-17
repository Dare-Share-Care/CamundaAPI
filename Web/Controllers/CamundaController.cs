using Microsoft.AspNetCore.Mvc;
using Web.Models.Dtos;
using Web.Services;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
public class CamundaController : ControllerBase
{
    private readonly DeploymentService _deploymentService;

    public CamundaController(DeploymentService deploymentService)
    {
        _deploymentService = deploymentService;
    }
    
    [HttpPost]
    [Route("deployment/deploy")]
    public async Task<IActionResult> Deploy([FromBody] DeploymentDto dto)
    {
        var result = await _deploymentService.Deploy(dto.Name);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("deployment/delete")]
    public async Task<IActionResult> DeleteCamundaDeployment([FromBody] DeploymentDto dto)
    {
        var response = await _deploymentService.DeleteDeployment(dto.Id);
        return Ok(response);
    }

    [HttpGet]
    [Route("deployment/list")]
    public async Task<IActionResult> GetCamundaDeployments()
    {
        var response = await _deploymentService.GetDeployments();
        return Ok(response);
    }

}