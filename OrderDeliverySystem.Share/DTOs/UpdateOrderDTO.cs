﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDeliverySystem.Share.DTOs
{
    public class UpdateOrderDTO
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}
