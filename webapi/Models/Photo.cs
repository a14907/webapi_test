using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Person> Persons { get; set; }
    }
}
