using RecruitmentTask.Api.Controllers.People;
using RecruitmentTask.Application.People.GetAllPeople;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using RecruitmentTask.Application.Exceptions;

namespace RecruitmentTask.Ui.ApiConnection;

internal class ApiConnectionService : IApiConnectionService
{
    private readonly Uri _baseUri = new Uri("https://localhost:7213/");
    private readonly string _peopleUri = "people";

    private readonly RestClient _restClient;

    public ApiConnectionService()
    {
        _restClient = new RestClient(_baseUri);
    }

    public async Task<IEnumerable<PersonResponse>> GetAllPeopleAsync()
    {
        var restRequest = new RestRequest(_peopleUri, Method.Get);

        var restResponse = await _restClient.ExecuteAsync<IEnumerable<PersonResponse>>(restRequest);

        var peopleResponses = restResponse.Data;

        return peopleResponses;
    }

    public async Task<ApiResponse> CreatePersonAsync(CreatePersonRequest createPersonRequest)
    {
        var restRequest = new RestRequest(_peopleUri, Method.Post);

        restRequest.AddJsonBody(createPersonRequest);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        var apiResponse = CreateApiResponse(restResponse);

        return apiResponse;
    }

    public async Task<ApiResponse> UpdatePersonAsync(UpdatePersonRequest updatePersonRequest)
    {
        var restRequest = new RestRequest(_peopleUri, Method.Patch);

        restRequest.AddJsonBody(updatePersonRequest);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        var apiResponse = CreateApiResponse(restResponse);

        return apiResponse;
    }

    public async Task<ApiResponse> RemovePerson(Guid personId)
    {
        var personToRemoveUri = new Uri(_baseUri, $"{_peopleUri}/{personId}");

        var restRequest = new RestRequest(personToRemoveUri, Method.Delete);

        var restResponse = await _restClient.ExecuteAsync(restRequest);

        var apiResponse = new ApiResponse
        {
            IsSuccess = restResponse.IsSuccessful
        };

        return apiResponse;
    }

    private ApiResponse CreateApiResponse(RestResponse restResponse)
    {
        var apiResponse = new ApiResponse
        {
            IsSuccess = restResponse.IsSuccessful
        };

        if (!restResponse.IsSuccessful)
        {
            try
            {
                var validationErrorsJson = restResponse.Content;

                var jsonObject = JObject.Parse(validationErrorsJson);

                bool isPropertyValidationError = jsonObject.ContainsKey("type");
                bool isBadRequest = jsonObject.ContainsKey("code");

                if (isPropertyValidationError)
                {
                    var propertyErrorJArray = (JArray)jsonObject["errors"];

                    var validationErrors = propertyErrorJArray.Select(error =>
                    {
                        string propertyName = (string)error["propertyName"];
                        string errorMessage = (string)error["errorMessage"];
                        return new ValidationError(propertyName, errorMessage);
                    }).ToList();

                    foreach (var validationError in validationErrors)
                    {
                        apiResponse.ValidationErrors.Add(validationError);
                    }
                }
                else if (isBadRequest)
                {
                    var errorMessage = jsonObject["name"].Value<string>();

                    apiResponse.GeneralError = $"Error: {errorMessage}";
                }
            }
            catch
            {
                apiResponse.GeneralError = $"Unexpected error occurred.";
            }

        }

        return apiResponse;
    }
}
