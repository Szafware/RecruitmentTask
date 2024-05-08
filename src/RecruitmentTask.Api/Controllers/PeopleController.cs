using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentTask.Api.Controllers;

[ApiController]
[Route("People")]
public class PeopleController : ControllerBase
{
    private readonly ISender _sender;

    public PeopleController(ISender sender)
    {
        _sender = sender;
    }

    //[HttpGet]
    //[Produces<IEnumerable<PersonResponse>>]
    //public Task<IActionResult> GetAll()
    //{

    //}
}
