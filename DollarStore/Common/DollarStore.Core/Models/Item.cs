using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;

namespace DollarStore.Core
{

    public class BaseItem
    {

        public virtual int Itemd { get; set; }
        public virtual string Name { get; set; }
        public virtual string Barcode { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string Categorie { get; set; }
        public virtual bool HasChilds { get; set; }
        public virtual bool IsProductValidated { get; set; }
        public virtual bool IsExpirationDateRequire { get; set; }
    }

    public class FirestoreItem : BaseItem
    {
        [Id]
        public string ID { get; set; }
        [Ignored]
        public override int Itemd { get => base.Itemd; set => base.Itemd = value; }
        [MapTo("IsParent")]
        public List<FirestorePackedItem> ChildItems { get; set; } = new List<FirestorePackedItem>();
    }

    public class PackedItem : BaseItem
    {
        public int QuantityInPack { get; set; }
    }

    public class FirestorePackedItem: PackedItem
    {
        [Id]
        public string ID { get; set; }
        [Ignored]
        public override int Itemd { get => base.Itemd; set => base.Itemd = value; }
        [Ignored]
        public override string Name { get => base.Name; set => base.Name = value; }
        [Ignored]
        public override string Barcode { get => base.Barcode; set => base.Barcode = value; }
        [Ignored]
        public override string ImageUrl { get => base.ImageUrl; set => base.ImageUrl = value; }
        [Ignored]
        public override string Categorie { get => base.Categorie; set => base.Categorie = value; }
        [Ignored]
        public override bool HasChilds { get => base.HasChilds; set => base.HasChilds = value; }
        [Ignored]
        public override bool IsExpirationDateRequire { get => base.IsExpirationDateRequire; set => base.IsExpirationDateRequire = value; }
        [Ignored]
        public override bool IsProductValidated { get => base.IsProductValidated; set => base.IsProductValidated = value; }
    }
}
