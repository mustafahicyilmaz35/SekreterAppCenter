using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sekreter.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }

        public IList Numbers { get; set; }
        public IList Emails { get; set; }

        public Contact()
        {
            Numbers = new List<string>();
            Emails = new List<string>();
        }
    }
}
