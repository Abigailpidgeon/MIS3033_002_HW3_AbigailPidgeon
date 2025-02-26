﻿using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace a
{
    public class Receipt
    {
        [Key]
        public string ReceiptID { get; set; }
        public int CogQuantity { get; set; }
        public int GearQuantity { get; set; }
        public double Total { get; set; }

        public double CalculateTotal()
        {
            Total = (CogQuantity * 79.99 + GearQuantity * 250) * 1.089;
            return Total;
        }

        public override string ToString()
        {
            return $"ReceiptID:{this.ReceiptID}, NCogs:{this.CogQuantity}, Ngears:{this.GearQuantity}, Total:{this.Total:C2}";
        }

    }
}