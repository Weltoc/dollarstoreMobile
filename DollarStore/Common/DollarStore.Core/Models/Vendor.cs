using Plugin.CloudFirestore.Attributes;
using System.Collections.Generic;

namespace DollarStore.Core.Models
{
    public class BaseVendor
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
    }

    public class FirestoreVendor : BaseVendor
    {
        [Id]
        public string ID { get; set; }
        [Ignored]
        public override int Id { get => base.Id; set => base.Id = value; }
        public List<BaseShop> Stores { get; set; } = new List<BaseShop>();
    }

    public class BaseShop: BaseVendor
    {
    }

}
