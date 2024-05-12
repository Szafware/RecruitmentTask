using RecruitmentTask.Domain.Abstractions;

namespace RecruitmentTask.Domain.People;

public static class PersonErrors
{
    public static Error NotFound = new Error("Person.NotFound", "The person with the specified identifier was not found.");

    public static Error IdenticalData = new Error("Person.IdenticalData", "There already is another person containing identical data.");
}
