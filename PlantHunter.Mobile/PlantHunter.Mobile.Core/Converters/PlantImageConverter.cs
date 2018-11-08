using PlantHunter.Mobile.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.Converters
{
    public class PlantImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            var builder = new UriBuilder(AppSettings.PlantsEndpoint);
            builder.AppendToPath(value.ToString());

            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
