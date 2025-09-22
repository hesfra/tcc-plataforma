using System;

namespace Domain.Entities
{
    public class Currency
    {
        public string Code { get; set; }

        public string codein { get; set; }

        public string name { get; set; }

        public decimal high { get; set; }

        public decimal low { get; set; }
        public decimal varBid { get; set; }
        public decimal pctChange { get; set; }

        public decimal bid { get; set; }

        public decimal ask { get; set; }

        public DateTime create_date { get; set; }

    }
}