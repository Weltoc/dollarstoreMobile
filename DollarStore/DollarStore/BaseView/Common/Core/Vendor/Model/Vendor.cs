using Plugin.CloudFirestore.Attributes;
using System.Collections.Generic;


namespace DollarStore.Models
{
    public class Vendor
    {
        [Id]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Shop> Stores { get; set; } = new List<Shop>();

    }
}
