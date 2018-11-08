using Xamarin.Forms.Maps;

namespace PlantHunter.Mobile.Core.Models
{
    public class CustomPin
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string PinIcon { get; set; }

        public string Address { get; set; }

        public Position Position { get; set; }

        public PlantType Type { get; set; }
    }

    public enum PlantType
    {
        own, other
    }
}