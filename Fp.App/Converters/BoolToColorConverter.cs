#pragma warning disable CS8767
#pragma warning disable CS8618

namespace Fp.App.Converters;

public class BoolToColorConverter : IValueConverter
{
    public string ColorTrue { get; set; }
    public string ColorFalse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool indicator)
        {
            if (indicator)
                return Color.FromArgb(ColorTrue);
            else
                return Color.FromArgb(ColorFalse);
        }

        // Not expected type, not visible
        return Colors.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

