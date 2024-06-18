using ChequeWriter.GenericModels.Common.Utils;

namespace CommonUtilsUnitTests;

public class AmountUtilsUnitTest
{
    [Test]
    public void ConvertPriceToWords_ThrowCommonUtilsException_WithNegativeValue()
    {
        //Act
        double price = -6;

        //Assert
        Assert.Throws<CommonUtilsException>(() => AmountUtils.Convert(price));
    }

    [Test]
    public void ConvertPriceToWords_ReturnZeroString_WithZeroPrice()
    {
        //Act
        double price = 0;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Zero Dollar"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnUnitsString_WithUnitsValue()
    {
        //Act
        double price = 6.8;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Six Dollars and Eighty Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnTensString_WithTensValue()
    {
        //Act
        double price = 15.6;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Fifteen Dollars and Sixty Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnHunderedsString_WithHundredsValue()
    { 
        //Act
        double price = 750.3;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Seven Hundreds Fifty Dollars and Thirty Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnThousandsString_WithThousandsValue()
    {
        //Act
        double price = 6670.54;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Six Thousands Six Hundreds Seventy Dollars and Fifty-Four Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnTenThousandsString_WithTenThousandsValue()
    {
        //Act
        double price = 17980.54;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Seventeen Thousands Nine Hundreds Eighty Dollars and Fifty-Four Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnHunderdThousandsString_WithHundredsThousandsValue()
    {
        double price = 541290.79;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Five Hundreds Forty-One Thousands Two Hundreds Ninety Dollars and Seventy-Nine Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnMillionsString_WithMillionsValue()
    {
        double price = 5356334.88;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Five Millions Three Hundreds Fifty-Six Thousands Three Hundreds Thirty-Four Dollars and Eighty-Eight Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturBillionsString_WithBillionsValue()
    {
        double price = 6578966523.25;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Six Billions Five Hundreds Seventy-Eight Millions Nine Hundreds Sixty-Six Thousands Five Hundreds Twenty-Three Dollars and Twenty-Five Cents"));
    }
}