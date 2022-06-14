using System;
using System.Collections.Generic;
using System.Text;

namespace TwoPoi
{
    public class TravelStyle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("D");
        
        public PointOfInterest PointOfInterest { get; set; }
        
        public string TextHexColor { get; set; }
        
        public string BackgroundHexColor { get; set; }
        
        public string SettingsButtonHexColor { get; set; }

        public TravelStyle()
        {
        }

        public TravelStyle(
            PointOfInterest pointOfInterest, 
            string textHexColor, 
            string backgroundHexColor, 
            string settingsButtonHexColor)
        {
            PointOfInterest = pointOfInterest;
            TextHexColor = textHexColor;
            BackgroundHexColor = backgroundHexColor;
            SettingsButtonHexColor = settingsButtonHexColor;
        }
    }
}
