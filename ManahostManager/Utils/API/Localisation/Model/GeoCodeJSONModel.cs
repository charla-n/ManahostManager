using System.Collections.Generic;

namespace ManahostManager.Utils.API.Localisation.Model
{
    public class GeoCodeJSONGeoCodingModel
    {
        public string housenumber { get; set; }
        public string street { get; set; }
        public string citycode { get; set; }
        public string postcode { get; set; }
        public string city { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public string name { get; set; }
    }

    public class GeoCodeJSONFeatureModel
    {
        public GeoCodeJSONGeoCodingModel properties { get; set; }
    }

    public class GeoCodeJSONModel
    {
        public string query { get; set; }
        public IEnumerable<GeoCodeJSONFeatureModel> features { get; set; }
    }
}