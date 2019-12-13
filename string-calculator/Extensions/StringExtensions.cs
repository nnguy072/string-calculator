namespace string_calculator.Extensions
{
    public static class StringExtensions
    {
        public static int ConvertToInt(this string stringNumber)
        {
            if (int.TryParse(stringNumber, out var number))
                return number;
            else
                return 0;
        }
    }
}