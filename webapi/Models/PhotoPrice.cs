using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Models
{
    public class PhotoPrice
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}