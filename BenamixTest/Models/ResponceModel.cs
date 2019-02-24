using System;
using System.Collections.Generic;

namespace BenamixTest.Models
{
    public class RootObject
    {
        public int lastUpdateId { get; set; }
        public object[,] bids { get; set; }
        public object[,] asks { get; set; }
    }
}
