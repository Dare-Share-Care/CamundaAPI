using Microsoft.AspNetCore.Mvc;
using Web.Camunda;
using Web.Models.Dtos;
using Web.Services;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
public class CamundaController : ControllerBase
{
    private readonly CamundaDeployment _camundaDeployment;
    private readonly CamundaProcess _camundaProcess;
    private readonly CamundaTask _camundaTask;

    public CamundaController(CamundaDeployment camundaDeployment, CamundaProcess camundaProcess, CamundaTask camundaTask)
    {
        _camundaDeployment = camundaDeployment;
        _camundaProcess = camundaProcess;
        _camundaTask = camundaTask;
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
    
    [HttpGet]
    [Route("tasks")]
    public async Task<IActionResult> GetCamundaTasks()
    {
        var response = await _camundaTask.GetTasks();
        return Ok(response);
    }

    [HttpPost]
    [Route("task/{id}/complete")]
    public async Task<IActionResult> CompleteCamundaTaskWithCondition(string id, [FromBody] CompleteTaskDto? dto)
    {
        var result = await _camundaTask.CompleteTask(id, dto);
        return Ok(result);
    }
}