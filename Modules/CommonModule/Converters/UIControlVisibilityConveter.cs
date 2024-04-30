namespace ChequeWriter.Modules.CommonModule.Converters;

public class UIControlVisibilityConveter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            bool boolValue = false;

            if (value is bool)
            {
                boolValue = (bool)value;
            }
            else if (value is bool?)
            {
                boolValue = (bool?)value ?? false;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException($"Unable to convert at {nameof(UIControlVisibilityConveter)}: {ex.Message}");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility ? (Visibility)value == Visibility.Visible : false;
    }
}
