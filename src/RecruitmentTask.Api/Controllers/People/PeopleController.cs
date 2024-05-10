using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTask.Application.People.CreatePerson;
using RecruitmentTask.Application.People.GetAllPeople;
using RecruitmentTask.Application.People.GetPerson;
using RecruitmentTask.Application.People.RemovePerson;
using RecruitmentTask.Application.People.UpdatePerson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Api.Controllers.People;

[ApiController]
[Route("People")]
public class PeopleController : ControllerBase
{
    private readonly ISender _sender;

    public PeopleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    [Produces<PersonResponse>]
    public async Task<IActionResult> GetPerson(
        Guid id,
        CancellationToken cancellationToken)
    {
        var getPersonQuery = new GetPersonQuery(id);

        var result = await _sender.Send(getPersonQuery, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    [Produces<IEnumerable<PersonResponse>>]
    public async Task<IActionResult> GetAllPeople(CancellationToken cancellationToken)
    {
        var getAllPeopleQuery = new GetAllPeopleQuery();

        var result = await _sender.Send(getAllPeopleQuery, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(
        CreatePersonRequest createPersonRequest,
        CancellationToken cancellationToken)
    {
        var createPersonCommand = new CreatePersonCommand(
            createPersonRequest.FirstName,
            createPersonRequest.LastName,
            createPersonRequest.BirthDate,
            createPersonRequest.PhoneNumber,
            createPersonRequest.StreetName,
            createPersonRequest.HouseNumber,
            createPersonRequest.ApartmentNumber,
            createPersonRequest.Town,
            createPersonRequest.PostalCode);

        var result = await _sender.Send(createPersonCommand, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetPerson), new { id = result.Value }, result.Value);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePerson(
        UpdatePersonRequest updatePersonRequest,
        CancellationToken cancellationToken)
    {
        var updatePersonCommand = new UpdatePersonCommand(
            updatePersonRequest.Id,
            updatePersonRequest.FirstName,
            updatePersonRequest.LastName,
            updatePersonRequest.BirthDate,
            updatePersonRequest.PhoneNumber,
            updatePersonRequest.StreetName,
            updatePersonRequest.HouseNumber,
            updatePersonRequest.ApartmentNumber,
            updatePersonRequest.Town,
            updatePersonRequest.PostalCode);

        var result = await _sender.Send(updatePersonCommand, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePerson(
        Guid id,
        CancellationToken cancellationToken)
    {
        var removePersonCommand = new RemovePersonCommand(id);

        var result = await _sender.Send(removePersonCommand, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
