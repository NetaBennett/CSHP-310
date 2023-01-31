
//***********************************
// CSHP-310
// Assignment: 01
// Student: Bennett, Neta (netab)
//***********************************


namespace VendingMachine.Models
{
    public class Inventory
    {
        public Inventory(Product product, int numUnits)
        {
            this.Product = product;
            this.NumUnits = numUnits;
        }
        public Product Product { get; set; }
        public int NumUnits { get; set; }
    }
}
