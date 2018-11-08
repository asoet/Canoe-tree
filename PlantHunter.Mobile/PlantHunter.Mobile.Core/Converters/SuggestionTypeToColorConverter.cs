using PlantHunter.Mobile.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.Converters
{
    public class PlantTypeToColorConverter : IValueConverter
    {
        readonly Color otherColor = Color.FromHex("#BD4B14");
        readonly Color ownColor = Color.FromHex("#348E94");
        readonly Color noColor = Color.FromHex("#FFFFFF");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PlantType suggestionType)
            {
                switch (suggestionType)
                {
                    case PlantType.own:
                        return ownColor;
                    case PlantType.other:
                        return otherColor;
                }
            }


            return noColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}