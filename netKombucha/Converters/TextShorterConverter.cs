using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace netKombucha.Converters;

public class TextShorterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string sourceText || !targetType.IsAssignableTo(typeof(string)))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        if (string.IsNullOrEmpty(sourceText) || sourceText.Length <= 21)
            return sourceText;
        return !string.IsNullOrEmpty(sourceText) ? $"{sourceText[..9]}...{sourceText[^9..]}" : sourceText;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();

    public static readonly TextShorterConverter Instance = new();
}