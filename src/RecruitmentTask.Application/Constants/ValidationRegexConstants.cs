namespace RecruitmentTask.Application.Constants;

internal static class ValidationRegexConstants
{
    public static readonly string OnlyLettersRegex = @"^[A-Za-z]+$";
    public static readonly string AllPolishLettersRegex = @"^[a-zA-ZąĄćĆęĘńŃóÓżŻźŹ]+$";
    public static readonly string PolishPostalCodeRegex = @"^[0-9]{2}-[0-9]{3}";
    public static readonly string OnlyDigitsRegex = @"^\d+$";
    public static readonly string DigitsAndLettersRegex = @"^[a-zA-Z0-9]+$";
}
