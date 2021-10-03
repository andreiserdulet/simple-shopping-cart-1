﻿
using Data.Abstraction;

namespace Data.Models
{
    public class Order : BaseEntityModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public decimal Total { get; set; }
        public Cart Cart { get; set; }
    }
}
