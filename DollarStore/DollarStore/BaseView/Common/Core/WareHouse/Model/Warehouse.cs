using Plugin.CloudFirestore.Attributes;

namespace DollarStore.Common.Core.WareHouse.Model
{
    public class Warehouse
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
    }

    public class FirestoreWarehouse : Warehouse
    {
        [Id]
        public override string Id { get => base.Id; set => base.Id = value; }
        [MapTo("Name")]
        public override string Name { get => base.Name; set => base.Name = value; }
        [MapTo("Location")]
        public override string Location { get => base.Location; set => base.Location = value; }
    }
}
