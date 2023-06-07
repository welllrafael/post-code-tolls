
using GeoCoordinatePortable;

namespace craftable_postcode.Services
{
    public interface ICalculateCoordinate
    {
        double Calculate(double? latitude, double? longitude);
        double GetDistanceInKM(double distance);
        double GetDistanceInMiles(double distance);
    }
    public class CalculateCoordinate : ICalculateCoordinate
    {
        public CalculateCoordinate()
        {
                
        }

        public double Calculate(double? latitude, double? longitude)
        {
            if (latitude == null || longitude == null) { return 0.0; }

            double latitudeFixed = 51.4700223; //Airport latitude fixed
            double longitudeFixed = -0.4542955; //Airport longitude fixed

            var coordRemote = new GeoCoordinate((double)latitude, (double)longitude);
            var coordFixed = new GeoCoordinate(latitudeFixed, longitudeFixed);

            return coordRemote.GetDistanceTo(coordFixed);

        }

        public double GetDistanceInKM(double distance)
        {
            var fatorkm = 1000;
            return Math.Round(distance / fatorkm, 2);
        }

        public double GetDistanceInMiles(double distance)
        {
            var fatorMiles = 0.621371192;
            return Math.Round(GetDistanceInKM(distance) * fatorMiles, 2);
        }
    }
}
