using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using LandscapeRover.GraphManager.Items;

namespace LandscapeRover.Converters
{
    internal class MatrixWayDisplayConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MatrixWayItem way)
            {
                var stringBuilder = new StringBuilder();

                for (var i = 0; i < way.Steps.Count; i++)
                {
                    var step = way.Steps[i];
                    stringBuilder.Append($"[{step.Row}][{step.Column}]");

                    if (i != way.Steps.Count - 1)
                    {
                        stringBuilder.Append("-> ");
                    }
                }

                return stringBuilder.ToString();
            }

            if (value == null)
            {
                return null;
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}