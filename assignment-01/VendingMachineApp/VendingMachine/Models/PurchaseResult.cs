
//***********************************
// CSHP-310
// Assignment: 01
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
