namespace ChequeWriter.GenericModels.Common.Utils;

public class CommonUtilsException : Exception
{
    public CommonUtilsException(string errorMessage) : base(errorMessage)
    {
    }
    public CommonUtilsException(string errorMessage, Exception inner) : base(errorMessage, inner)
    {
    }
}

public static class AmountUtils
{
    private static readonly string[] Units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    private static readonly string[] ThousandsGroups = { "", "Thousands", "Millions", "Billions" };

    public static string Convert(double number)
    {
        if (number < 0) 
        {
            throw new CommonUtilsException("Illegal number. The conversion not supported below than zero numbers.");
        }

        if (number == 0)
        { 
            return "Zero Dollar";
        }

        long dollars = (long)number;
        int cents = (int)Math.Round((number - dollars) * 100);

        string dollarSection = ConvertWholeNumberToWords(dollars);
        string centSection = ConvertWholeNumberToWords(cents);

        return (cents == 0) ? $"{dollarSection} Dollars" : $"{dollarSection} Dollars and {centSection} Cents";
    }

    private static string ConvertWholeNumberToWords(long number)
    {
        if (number == 0)
        { 
            return String.Empty;
        }

        int group = 0;
        string result = "";

        // Loop through the number, processing three digits at a time
        while (number > 0)
        {
            int lastThreeDigits = (int)(number % 1000);

            // If these three digits are not zero, convert them to words
            if (lastThreeDigits != 0)
            {
                string prefix = ConvertThreeDigitNumberToWords(lastThreeDigits);
                result = prefix + ThousandsGroups[group] + " " + result;
            }

            // Move to the next three digits and increment the thousand group counter
            number /= 1000;
            group++;
        }

        return result.Trim();
    }

    private static string ConvertThreeDigitNumberToWords(int number)
    {
        string result = "";

        // Handle the hundreds place
        int hundreds = number / 100;
        if (hundreds > 0)
        {
            result += Units[hundreds] + " Hundreds ";
        }

        // Handle the tens and units places
        number = number % 100;
        if (number > 0)
        {
            if (number < 10)
            {
                result += Units[number];
            }
            else if (number < 20)
            { 
                result += Teens[number - 10];
            }
            else
            {
                int tens = number / 10;
                number = number % 10;
                result += Tens[tens];
                if (number > 0)
                {
                    // If there is a unit place, add it with a hyphen
                    result += "-" + Units[number];
                }
            }
        }

        return result.Trim() + " ";
    }
}