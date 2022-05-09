using System;
using System.Collections.Generic;
using System.Text;

namespace TwoPoi
{
    public class PointOfInterest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("D");
        
        public string Name { get; set; } = "";
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }

        public PointOfInterest()
        { }

        public PointOfInterest(
            string name, 
            double latitude, 
            double longitude)
        {
            Name = (String.IsNullOrEmpty(name)) ? "" : name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
