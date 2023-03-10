using System;
using System.Collections.Generic;
using System.Linq;

//***********************************
// CSHP-310
// Assignment: 01
// Student: Bennett, Neta (netab)
//***********************************


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
        public static VendingMachine GetInstance()
        {
            if (_instance == null)
            {
                _instance = Initialize();
            }
            return _instance;
        }

        //Public Methods
        public PurchaseResult DispenseProduct(Product product)
        {
            var inventory = _productInventory.FirstOrDefault(t => t.Product.Name == product.Name);

            if (inventory == null || inventory.NumUnits == 0)
            {
                throw new InvalidOperationException($"Sorry, we're all out of {product.Name}. Please make another selection.");
            }

            if (!HasSufficientFunds(product.Price))
            {
                throw new InvalidOperationException($"Insufficient funds for {product.Name}. Please add more money.");
            }

            //decrement inventory
            inventory.NumUnits--;
            //calculate change
            DenominationBag returnChange = Calculator.CalculateChange(product.Price, _coinBag.BagTotal);
            this._coinBag.Coins.Clear();
            return new PurchaseResult() { PurchasedProduct = product, ReturnChange = returnChange };
        }

        public void AddProductInventory(Inventory inventory)
        {
            _productInventory.Add(inventory);
        }
        public decimal InsertDenomination(Denomination denomination)
        {
            _coinBag.Coins.Add(denomination);
            return _coinBag.BagTotal;
        }
        public bool HasSufficientFunds(decimal productPrice)
        {
            return (_coinBag.BagTotal >= productPrice);
        }   
        public decimal GetTotalPaid()
        {
            return _coinBag.BagTotal;
        }
        public Inventory GetProductInventory(ProductEnum productType)
        {
            return this._productInventory.First(t => t.Product.ProductCode == productType);
        }

        public DenominationBag ReturnMoneyWithoutSelection()
        {
            var returnedChange = _coinBag.Clone();
            _coinBag.Coins.Clear();
            return returnedChange;
        }

        //private helper methods
         private static VendingMachine Initialize()
        {
            //setup of the machine's initial inventory
            _instance = new VendingMachine();
            _instance.AddProductInventory(new Inventory(ProductFactory.GetProduct(ProductEnum.CocaCola), 3));
            _instance.AddProductInventory(new Inventory(ProductFactory.GetProduct(ProductEnum.Sprite), 3));
            _instance.AddProductInventory(new Inventory(ProductFactory.GetProduct(ProductEnum.MountainDew), 3));
            return _instance;
        }
    }

}
