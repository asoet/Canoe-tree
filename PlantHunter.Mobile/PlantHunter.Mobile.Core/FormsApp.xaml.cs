// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross;
using MvvmCross.Forms.Core;
using PlantHunter.Mobile.Core.Models;
using PlantHunter.Mobile.Core.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PlantHunter.Mobile.Core
{
    public partial class FormsApp : MvxFormsApplication
    {

        public FormsApp()
        {
            InitializeComponent();
        }
    }
}