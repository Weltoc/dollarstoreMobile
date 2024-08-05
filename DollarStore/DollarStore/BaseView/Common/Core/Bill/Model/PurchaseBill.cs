using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;

namespace DollarStore.Common.Core.Bill.Model
{
    public class PurchaseBill
    {
        public virtual string Id { get; set; }
        public virtual DateTime CreateAt { get; set; }
        public virtual List<PurchaseBillItem> ImageUrl { get; set; } = new List<PurchaseBillItem>();
    }

    public class FirestoreBill : PurchaseBill
    {
        [Id]
        public override string Id { get => base.Id; set => base.Id = value; }
        [MapTo("CreateAt")]
        public override DateTime CreateAt { get => base.CreateAt; set => base.CreateAt = value; }
        [MapTo("ImageUrl")]
        public override List<PurchaseBillItem> ImageUrl { get => base.ImageUrl; set => base.ImageUrl = value; }
    }
}
