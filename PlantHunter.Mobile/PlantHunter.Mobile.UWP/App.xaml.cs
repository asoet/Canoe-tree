// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Forms.Platforms.Uap.Views;

namespace PlantHunter.Mobile.UWP
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public abstract class UwpApp : MvxWindowsApplication<Setup, Core.MvxApp, Core.FormsApp, MainPage>
    {
    }
}