using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace netKombucha.Converters;

public class TextShorterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string sourceText || parameter is not string lenghtString || !targetType.IsAssignableTo(typeof(string)))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        if (!int.TryParse(lenghtString, out var lenght))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        if (string.IsNullOrEmpty(sourceText) || sourceText.Length <= lenght)
            return sourceText;

        var half = (lenght - 3) / 2;
        return $"{sourceText[..half]}...{sourceText[^half..]}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();

    public static readonly TextShorterConverter Instance = new();
}