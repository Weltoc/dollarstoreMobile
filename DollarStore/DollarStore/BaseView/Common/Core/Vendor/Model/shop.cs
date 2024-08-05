using Plugin.CloudFirestore.Attributes;

namespace DollarStore.Models
{
    public class Shop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public bool IsValidated { get; set; }
    }
}
