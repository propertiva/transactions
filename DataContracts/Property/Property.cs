namespace DataContracts.Property
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Property
    {
        public string PropertyID { get; set; }

        public string PropertyCode { get; set; }

        public double SharePrice { get; set; }

        public double PercentageChange { get; set; }

        public double PropertyPrice { get; set; }

        public string ThumbnailURL { get; set; }

        public string Address { get; set; }

        public string Layout { get; set; }

        public string Size { get; set; }

        public TrendPoint[] TrendPoints { get; set; }
    }
}
