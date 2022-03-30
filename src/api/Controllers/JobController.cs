using Microsoft.AspNetCore.Mvc;
using Services;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService jobService;

    public JobController(IJobService jobService)
    {
        this.jobService = jobService;
    }  

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("hello");
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleJob([FromBody] Models.Job job)
    {
        await jobService.UpdateJob(job);

        return Ok();
    }
}
