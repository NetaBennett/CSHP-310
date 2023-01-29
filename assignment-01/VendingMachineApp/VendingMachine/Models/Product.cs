using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //these static properties are not generally a good practice but i'm using them
        //for ease of development
        public static Product CocaCola { get { return CreateProduct(ProductEnum.CocaCola);  } }
        public static Product Sprite { get { return CreateProduct(ProductEnum.Sprite); } }
        public static Product MountainDew { get { return CreateProduct(ProductEnum.MountainDew); } }

        private static Product CreateProduct(ProductEnum productType)
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
