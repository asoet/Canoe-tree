using System.Threading.Tasks;
using PlantHunter.Mobile.Core.ViewModels.Base;
using System.Collections.Generic;
using PlantHunter.Mobile.Core.Models;
using System.Windows.Input;
using Xamarin.Forms;
using PlantHunter.Mobile.Core.Services.Plants;
using PlantHunter.Mobile.Core.Services.Geolocator;
using Plugin.DeviceInfo;
using System.IO;
using Plugin.Media.Abstractions;
using ExifLib;
using System;

namespace PlantHunter.Mobile.Core.ViewModels
{
    public class PlantDetailsViewModel : ViewModelBase
    {
        readonly IPlantService plantService;
        readonly ILocationService locationService;
    
        bool hiker;
        bool scientificCitizen;
        bool botanist;
        bool isExisting;
        bool isNotExisting;
        ImageSource imageSource;
        Models.Plant plant;
        MediaFile Photo;

        public bool Hiker
        {
            get => hiker;
            set => SetProperty(ref hiker, value);
        }

        public bool ScientificCitizen
        {
            get => scientificCitizen;
            set => SetProperty(ref scientificCitizen, value);
        }

        public bool Botanist
        {
            get => botanist;
            set => SetProperty(ref botanist, value);
        }

        public bool IsExisting
        {
            get => isExisting;
            set => SetProperty(ref isExisting, value);
        }
        public bool IsNotExisting
        {
            get => isNotExisting;
            set => SetProperty(ref isNotExisting, value);
        }

        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public Plant Plant
        {
            get => plant;
            set => SetProperty(ref plant, value);
        }


        public PlantDetailsViewModel(IPlantService plantService,
            ILocationService locationService)
        {
            this.plantService = plantService;
            this.locationService = locationService;
        }
        
        public ICommand HikerCommand => new Command(SetHiker);

        public ICommand ScientificCitizenCommand => new Command(SetScientificCitizen);

        public ICommand BotanistCommand => new Command(SetBotanist);

        public ICommand UploadCommand => new AsyncCommand(UploadInformation);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            if (navigationData != null)
            {
                var navigationParameter = navigationData as Dictionary<string, object>;
                if(navigationParameter.TryGetValue("plant", out object Plantobj))
                {
                    IsNotExisting = false;
                    IsExisting = true;
                    Plant = (Plant)Plantobj;
                }
                else
                {
                    IsExisting = false;
                    IsNotExisting = true;
                    Plant = new Plant();
                }
                if (navigationParameter.TryGetValue("photo", out object photoobj))
                {
                    Photo = (MediaFile)photoobj;
                    ImageSource = ImageSource.FromStream(() => Photo.GetStream());
                }
            }
            IsBusy = false;
        }

        async Task UploadInformation()
        {
            IsBusy = true;
            //First try image;
            (double longitude, double lattitude) = GetPostionExif();

            if (longitude == 0 || lattitude == 0)
            {
                var postion = await locationService.GetPositionAsync();
                if (postion == null)
                    return;
                longitude = postion.Longitude;
                lattitude = postion.Latitude;
            }

            var additionalInfo = new AddPictureModel()
            {
                Longitude = longitude,
                Latitude = lattitude,
                DeviceId = CrossDeviceInfo.Current.Id,
                Name = Plant.Name,
                ScientificName = Plant.ScientificName,
                Family = Plant.Family,
                Description = Plant.Description,
                EndangeredLevel = Plant.EndangeredLevel,
                Surrounding = Plant.Surrounding
            };
            await plantService.UploadPictureAsync(Photo.GetStream(), Path.GetExtension(Photo.Path), additionalInfo);
            IsBusy = false;
            await NavigationService.NavigateBackAsync();
        }

        void SetHiker()
        {
            Hiker = true;
            ScientificCitizen = false;
            Botanist = false;
        }

        void SetScientificCitizen()
        {
            Hiker = false;
            ScientificCitizen = true;
            Botanist = false;
        }

        void SetBotanist()
        {
            Hiker = false;
            ScientificCitizen = false;
            Botanist = true;
        }

        public (double longitude, double lattitude) GetPostionExif()
        {
            using (ExifReader reader = new ExifReader(Photo.Path))
            {
                Double[] GpsLongArray;
                Double[] GpsLatArray;
                Double GpsLongDouble;
                Double GpsLatDouble;

                if (reader.GetTagValue<Double[]>(ExifTags.GPSLongitude, out GpsLongArray)
                    && reader.GetTagValue<Double[]>(ExifTags.GPSLatitude, out GpsLatArray))
                {
                    GpsLongDouble = GpsLongArray[0] + GpsLongArray[1] / 60 + GpsLongArray[2] / 3600;
                    GpsLatDouble = GpsLatArray[0] + GpsLatArray[1] / 60 + GpsLatArray[2] / 3600;
                    
                    return (-GpsLongDouble, GpsLatDouble); //TODO: fix - on longitude
                }
                return (0, 0);
            }
        }
    }
}