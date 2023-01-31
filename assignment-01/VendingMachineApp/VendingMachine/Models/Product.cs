using System;


//***********************************
// CSHP-310
// Assignment: 01
// Student: Bennett, Neta (netab)
//***********************************

namespace VendingMachine.Models
{
    public class Product
    {
        public Product(string productName, decimal productPrice, ProductEnum enumType)
        {
            this.Name = productName;
            this.Price = productPrice;
            this.ProductCode = enumType;
        }
        public string Name { get; }
        public decimal Price { get; }
        public ProductEnum ProductCode { get; }

        public override string ToString()
        {
            return $"{Name} - {Price}";
        }
    }
    public enum ProductEnum
    {
        CocaCola,
        Sprite,
        MountainDew
    }
    public static class ProductFactory
    { 
        public static Product GetProduct(ProductEnum productType)
        {
            switch(productType)
            {
                case ProductEnum.CocaCola:
                    {
                        return new Product("Coca-Cola", .75m, productType);
                    }
                case ProductEnum.Sprite:
                    {
                        return new Product("Sprite", .75m, productType);
                    }
                case ProductEnum.MountainDew:
                    {
                        return new Product("Mountain Dew", .65m, productType);
                    }
                default:
                    {
                        throw new InvalidOperationException($"Unable to create product {productType.GetTypeCode()}");
                    }

            }
        }
    }

  
}
