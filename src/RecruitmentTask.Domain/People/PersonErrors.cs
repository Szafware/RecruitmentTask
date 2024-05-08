using RecruitmentTask.Domain.Abstractions;

namespace RecruitmentTask.Domain.People;

public static class PersonErrors
{
    public static Error NotFound = new Error("Person.Found", "The person with the specified identifier was not found.");
}
