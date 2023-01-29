using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class VendingMachine
    {
        private List<Inventory> _productInventory = new List<Inventory>();
        private DenominationBag _coinBag = new DenominationBag();
        private static VendingMachine _instance = null;
     
        //private constructor so we can make it a singleton
        private VendingMachine()
        {
        }
        //singleton get instance
        public static VendingMachine GetInstance()
        {
            if (_instance == null)
            {
                _instance = Initialize();
            }
            return _instance;
        }

        //Public Interface Methods
        public void AddProductInventory(Inventory inventory)
        {
            _productInventory.Add(inventory);
        }
        public decimal InsertDenomination(Denomination denomination)
        {
            _coinBag.Coins.Add(denomination);
            return _coinBag.BagTotal;
        }
        public bool HasSufficientFunds(Product product)
        {
            return (_coinBag.BagTotal >= product.Price);
        }   
        public decimal GetTotalPaid()
        {
            return _coinBag.BagTotal;
        }

        public Inventory? GetProductInventory(ProductEnum productType)
        {
            return this._productInventory.FirstOrDefault(t => t.Product.ProductCode == productType);
        }
        public PurchaseResult DispenseProduct(Product product)
        {
            var inventory = _productInventory.FirstOrDefault(t => t.Product.Name == product.Name);

            if (inventory == null || inventory.NumUnits == 0)
            {
                throw new InvalidOperationException($"Sorry, we're all out of {product.Name}. Please make another selection.");
            }
           
            if (!HasSufficientFunds(product))
            {
                throw new InvalidOperationException("Insufficient funds for product. Please add more money.");
            }

            //decrement inventory available
            inventory.NumUnits--;
            DenominationBag returnChange = Calculator.CalculateChange(product.Price, _coinBag.BagTotal);
            this._coinBag.Coins.Clear();
            return new PurchaseResult() { PurchasedProduct = product, ReturnChange = returnChange };
        }
        private static VendingMachine Initialize()
        {
            //setup of the machine's initial inventory
            _instance = new VendingMachine();
            _instance.AddProductInventory(new Inventory(ProductFactory.CocaCola, 3));
            _instance.AddProductInventory(new Inventory(ProductFactory.Sprite, 3));
            _instance.AddProductInventory(new Inventory(ProductFactory.MountainDew, 3));
            return _instance;
        }
    }

}
