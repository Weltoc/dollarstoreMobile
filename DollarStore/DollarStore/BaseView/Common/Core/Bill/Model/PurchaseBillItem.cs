using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DollarStore.Common.Core.Bill.Model
{
    public class PurchaseBillItem 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string BillUrl { get; set; }
    }
}
