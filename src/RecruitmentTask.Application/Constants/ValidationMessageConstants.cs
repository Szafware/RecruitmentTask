namespace RecruitmentTask.Application.Constants;

internal static class ValidationMessageConstants
{
    public static readonly string BirthDateTooLowValidationMessage = "You're not that old, are ya?";
    public static readonly string BirthDateTooHighValidationMessage = "Sorry, no toddlers accepted.";

    public static readonly string PhoneNumberValidationMessage = "\'Phone Number\' should only contain digits eg. \"123456789\".";

    public static readonly string PostalCodeValidationMessage = "\'Postal Code\' should be specified in format XX-XXX.";
}
