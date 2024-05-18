namespace ChequeWriter.Modules.ChequeModule.Utils;

public class AmountUtils
{
    private static string[] Units = {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"};
    private static string[] Teens = {"", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"};
    private static string[] Tens = {"", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};
    private static string[] Thousands = {"Hundred", "Thousand", "Million", "Billion"};

    public static string ConvertToWords(double amount)
    {
        if (amount == 0)
            return "Zero";

        long intPart = (long)Math.Floor(amount);
        int decPart = (int)Math.Round((amount - intPart) * 100);

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(ConvertToWordsHelper(intPart));

        if (intPart != 0)
            stringBuilder.Append(" Dollars");

        if (decPart > 0)
        {
            stringBuilder.Append(" and ");
            stringBuilder.Append(ConvertToWordsHelper(decPart));
            stringBuilder.Append(" Cents");
        }

        return stringBuilder.ToString();
    }

    private static string ConvertToWordsHelper(long number)
    {
        if (number < 10)
            return Units[number];
        else if (number < 20)
            return Teens[number - 10];
        else if (number < 100)
            return Tens[number / 10] + ((number % 10 > 0) ? " " + Units[number % 10] : string.Empty);

        for (int i = 3; i >= 1; i--)
        {
            if (number >= Math.Pow(10, 3 * i))
            {
                return ConvertToWordsHelper(number / (long)Math.Pow(10, 3 * i)) + " " + Thousands[i] +
                       ((number % (long)Math.Pow(10, 3 * i) > 0) ? ", " + ConvertToWordsHelper(number % (long)Math.Pow(10, 3 * i)) : string.Empty);
            }
        }
        return string.Empty;
    }
}