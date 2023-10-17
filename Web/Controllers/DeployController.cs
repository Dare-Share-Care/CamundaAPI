using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
public class DeployController : ControllerBase
{
    private readonly DeployService _deployService;

    public DeployController(DeployService deployService)
    {
        _deployService = deployService;
    }
}