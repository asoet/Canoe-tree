using System;
using System.Collections.Generic;
using System.Text;

namespace PlantHunter.Mobile.Core.Services.Push
{
    public interface ITokenReceiver
    {
        string SaveToken(string refreshedToken);
    }
}
