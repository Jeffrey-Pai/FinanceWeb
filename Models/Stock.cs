﻿using System.ComponentModel.DataAnnotations;

namespace FinanceWeb.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
    }

}
