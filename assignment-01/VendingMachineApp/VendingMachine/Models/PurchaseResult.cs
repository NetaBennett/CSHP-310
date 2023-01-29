//***********************************
// Student: Bennett, Neta (netab)
//***********************************

namespace VendingMachine.Models
{
    public class PurchaseResult
    {
        public DenominationBag ReturnChange { get; set; }
        public Product PurchasedProduct { get; set; }
    }
}
