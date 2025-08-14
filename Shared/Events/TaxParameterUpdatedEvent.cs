using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class TaxParameterUpdatedEvent
    {
        public string Key { get; set; }         
        public decimal Value { get; set; }      
        public int Year { get; set; }
    }
}
