namespace string_calculator.Extensions
{
    public static class StringExtensions
    {
        // Invalid numbers are 0 (non-numbers, numbers > 1000)
        public static int ConvertToInt(this string stringNumber)
        {
            if (int.TryParse(stringNumber, out var number))
                return number > 1000 ? 0 : number;
            else
                return 0;
        }
    }
}