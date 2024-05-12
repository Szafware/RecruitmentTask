namespace RecruitmentTask.Application.Constants;

internal static class ValidationValueConstants
{
    public static readonly int FirstNameCharacterCountMin = 2;
    public static readonly int FirstNameCharacterCountMax = 15;

    public static readonly int LastNameCharacterCountMin = 2;
    public static readonly int LastNameCharacterCountMax = 20;

    public static readonly int BirthDateYearMin = 3;
    public static readonly int BirthDateYearMax = 100;

    public static readonly int PhoneNumberCharacterCount = 9;

    public static readonly int StreetNameCharacterCountMin = 2;
    public static readonly int StreetNameCharacterCountMax = 20;

    public static readonly int HouseNumberCharacterCountMin = 1;
    public static readonly int HouseNumberCharacterCountMax = 10;

    public static readonly int ApartmentNumberValueMin = 1;
    public static readonly int ApartmentNumberValueMax = 1000;

    public static readonly int TownCharacterCountMin = 2;
    public static readonly int TownCharacterCountMax = 20;
}
