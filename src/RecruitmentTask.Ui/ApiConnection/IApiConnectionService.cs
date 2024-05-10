using RecruitmentTask.Api.Controllers.People;
using RecruitmentTask.Application.People.GetAllPeople;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentTask.Ui.ApiConnection;

internal interface IApiConnectionService
{
    Task<IEnumerable<PersonResponse>> GetAllPeopleAsync();

    Task<ApiResponse> CreatePersonAsync(CreatePersonRequest createPersonRequest);

    Task<ApiResponse> UpdatePersonAsync(UpdatePersonRequest updatePersonRequest);

    Task<ApiResponse> RemovePerson(Guid personId);
}
