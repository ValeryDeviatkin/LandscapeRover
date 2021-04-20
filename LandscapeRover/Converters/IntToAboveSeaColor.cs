using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace LandscapeRover.Converters
{
    internal class IntToAboveSeaColor : MarkupExtension, IValueConverter
    {
        public int Max { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int level &&
                level >= 0 &&
                level <= Max)
            {
                var color = new Color {A = byte.MaxValue};

                if (level == 0)
                {
                    color.G = byte.MaxValue;
                }
                else if (level == Max)
                {
                    color.R = byte.MaxValue;
                }
                else
                {
                    if (Max % 2 == 0)
                    {
                        var middle = Max * 0.5d;
                        var step = byte.MaxValue / middle;

                        if (level < middle)
                        {
                            color.R = (byte) (level * step);
                            color.G = byte.MaxValue;
                        }
                        else if (level == middle)
                        {
                            color.R = byte.MaxValue;
                            color.G = byte.MaxValue;
                        }
                        else if (level < Max)
                        {
                            color.R = byte.MaxValue;
                            color.G = (byte) ((Max - level) * step);
                        }
                    }
                    else
                    {
                        var middle1 = (Max - 1) * 0.5d;
                        var middle2 = middle1 + 1d;
                        var step = byte.MaxValue / middle1;

                        if (level < middle1)
                        {
                            color.R = (byte) (level * step);
                            color.G = byte.MaxValue;
                        }
                        else if (level == middle1)
                        {
                            color.R = (byte) (byte.MaxValue - step * 0.5d);
                            color.G = byte.MaxValue;
                        }
                        else if (level == middle2)
                        {
                            color.R = byte.MaxValue;
                            color.G = (byte) (byte.MaxValue - step * 0.5);
                        }
                        else if (level < Max)
                        {
                            color.R = byte.MaxValue;
                            color.G = (byte) ((Max - level) * step);
                        }
                    }
                }

                return new SolidColorBrush(color);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}