using RecruitmentTask.Application.Exceptions;
using System.Collections.Generic;

namespace RecruitmentTask.Ui.ApiConnection;

internal class ApiResponse
{
    public bool IsSuccess { get; init; }

    public string GeneralError { get; set; }

    public List<ValidationError> ValidationErrors { get; init; } = new();
}
