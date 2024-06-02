using ChequeWriter.GenericModels.Common.Utils;

namespace CommonUtilsUnitTests;

public class AmountUtilsUnitTest
{
    [Test]
    public void ConvertPriceToWords_NegativeValue_ThrowCommonUtilsException()
    {
        //Act
        double price = -6;

        //Assert
        Assert.Throws<CommonUtilsException>(() => AmountUtils.Convert(price));
    }

    [Test]
    public void ConvertPriceToWords_ReturnZero_Success()
    {
        //Act
        double price = 0;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Zero Dollar"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnUnits_Success()
    {
        //Act
        double price = 6.8;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Six Dollars and Eighty Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnTens_Success()
    {
        //Act
        double price = 15.6;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Fifteen Dollars and Sixty Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnHundereds_Success()
    {
        //Act
        double price = 750.3;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Seven Hundreds Fifty Dollars and Thirty Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnThousands_Success()
    {
        //Act
        double price = 6670.54;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Six Thousands Six Hundreds Seventy Dollars and Fifty-Four Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnTenThousands_Success()
    {
        //Act
        double price = 17980.54;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Seventeen Thousands Nine Hundreds Eighty Dollars and Fifty-Four Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnHunderdThousands_Success()
    {
        double price = 541290.79;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Five Hundreds Forty-One Thousands Two Hundreds Ninety Dollars and Seventy-Nine Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturnMillions_Success()
    {
        double price = 5356334.88;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Five Millions Three Hundreds Fifty-Six Thousands Three Hundreds Thirty-Four Dollars and Eighty-Eight Cents"));
    }

    [Test]
    public void ConvertPriceToWords_ReturBillions_Success()
    {
        double price = 6578966523.25;

        //Arrange
        string result = AmountUtils.Convert(price);

        //Act
        Assert.That(result, Is.EqualTo("Six Billions Five Hundreds Seventy-Eight Millions Nine Hundreds Sixty-Six Thousands Five Hundreds Twenty-Three Dollars and Twenty-Five Cents"));
    }
}