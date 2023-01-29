using System.Collections.Generic;
using System.Linq;

//***********************************
// Student: Bennett, Neta (netab)
//***********************************

namespace VendingMachine.Models
{
    public class DenominationBag
    {
        public List<Denomination> Coins { get; set; } = new List<Denomination>();
        public decimal BagTotal
        {
            get
            {
                return Coins.Sum(t => t.Value);
            }
        }
    }
}
