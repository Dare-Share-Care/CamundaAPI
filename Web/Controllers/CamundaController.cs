using Microsoft.AspNetCore.Mvc;
using Web.Models.Dtos;
using Web.Services;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
public class CamundaController : ControllerBase
{
    private readonly CamundaDeployment _camundaDeployment;
    private readonly CamundaProcess _camundaProcess;

    public CamundaController(CamundaDeployment camundaDeployment, CamundaProcess camundaProcess)
    {
        _camundaDeployment = camundaDeployment;
        _camundaProcess = camundaProcess;
    }
    
    [HttpPost]
    [Route("deployment/deploy")]
    public async Task<IActionResult> Deploy([FromBody] DeploymentDto dto)
    {
        var result = await _camundaDeployment.Deploy(dto.Name);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("deployment/delete")]
    public async Task<IActionResult> DeleteCamundaDeployment([FromBody] DeploymentDto dto)
    {
        var response = await _camundaDeployment.DeleteDeployment(dto.Id);
        return Ok(response);
    }

    [HttpGet]
    [Route("deployment/list")]
    public async Task<IActionResult> GetCamundaDeployments()
    {
        var response = await _camundaDeployment.GetDeployments();
        return Ok(response);
    }
    
    [HttpPost]
    [Route("process/start")]
    public async Task<IActionResult> StartCamundaProcess([FromBody] StartProcessDto dto)
    {
        var response = await _camundaProcess.StartCamundaProcess(dto.ProcessKey);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("process/list")]
    public async Task<IActionResult> GetCamundaProcesses()
    {
        var response = await _camundaProcess.GetProcesses();
        return Ok(response);
    }
}