using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class PersonModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid Plant_ID { get; set; }

    }
}
