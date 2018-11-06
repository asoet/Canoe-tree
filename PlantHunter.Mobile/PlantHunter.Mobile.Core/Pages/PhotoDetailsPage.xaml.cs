// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross;
using MvvmCross.Forms.Views;
using PlantHunter.Mobile.Core.Services;
using System;
using Xamarin.Forms;

namespace PlantHunter.Mobile.Core.Pages
{
    public partial class PhotoDetailsPage : MvxContentPage
    {
        public PhotoDetailsPage()
        {
            InitializeComponent();
        }

        private const string Hiker = "Hiker";
        private const string Botanist = "Botanist";
        private const string CitizenScientist = "Citizen Scientist";

        //public bool ShowForHiker { get; set; }
        //public bool ShowForCitizenScientist { get; set; }
        //public bool ShowForBotanist { get; set; }

        private void SortUserPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = (Picker)sender;
            Mvx.Resolve<IAppSettings>().Role = obj.SelectedItem as string;
            var SelectedSortUser = Mvx.Resolve<IAppSettings>().Role;
            switch (SelectedSortUser)
            {
                case Hiker:
                    NameDetails.IsVisible = true;
                    ScientificNameDetails.IsVisible = false;
                    FamilyDetails.IsVisible = false;
                    DescriptionDetails.IsVisible = false;
                    EndangeredLevelDetails.IsVisible = false;
                    SurroundingDetails.IsVisible = false;
                    break;
                case CitizenScientist:
                    NameDetails.IsVisible = true;
                    ScientificNameDetails.IsVisible = false;
                    FamilyDetails.IsVisible = true;
                    DescriptionDetails.IsVisible = true;
                    EndangeredLevelDetails.IsVisible = true;
                    SurroundingDetails.IsVisible = true;
                    break;
                case Botanist:
                    NameDetails.IsVisible = true;
                    ScientificNameDetails.IsVisible = true;
                    FamilyDetails.IsVisible = true;
                    DescriptionDetails.IsVisible = true;
                    EndangeredLevelDetails.IsVisible = true;
                    SurroundingDetails.IsVisible = true;
                    break;
                default:
                    NameDetails.IsVisible = true;
                    ScientificNameDetails.IsVisible = false;
                    FamilyDetails.IsVisible = false;
                    DescriptionDetails.IsVisible = false;
                    EndangeredLevelDetails.IsVisible = false;
                    SurroundingDetails.IsVisible = false;
                    break;
            }
        }


    }
}
