using Newtonsoft.Json;
using System.Collections.Generic;

namespace HealthCenter.Domain
{
    public class Value
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string value { get; set; }
    }

    public class Headers
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "values")]
        public List<Value> Values { get; set; }
    }

    public class ProfileLinkedIn
    {
        [JsonProperty(PropertyName = "headers")]
        public Headers Headers { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Country
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "country")]
        public Country Country { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class SiteProfileLinkedIn
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class LinkedInResponse
    {
        [JsonProperty(PropertyName = "apiStandardProfileRequest")]
        public ProfileLinkedIn ProfileLinkedIn { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "headline")]
        public string Headline { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }

        [JsonProperty(PropertyName = "siteStandardProfileRequest")]
        public SiteProfileLinkedIn SiteProfileLinkedIn { get; set; }
    }

}
