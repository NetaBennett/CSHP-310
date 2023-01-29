using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class Denomination
    {
        public Denomination(string typeName, decimal value)
        {
            this.Name = typeName;
            this.Value = value;
        }
        public string Name { get; }
        public decimal Value { get; }

        public override string ToString()
        {
            return $"{Name} - ${Value}";
        }

    }

    public enum DenominationEnum
    {
        Nickel,
        Dime,
        Quarter,
        HalfDollar
    }


    public static class DenominationFactory
    {
        //these static properties are not generally a good practice but i'm using them
        //for ease of development
        public static Denomination HalfDollar { get { return CreateDenomination(DenominationEnum.HalfDollar); } }
        public static Denomination Quarter { get { return CreateDenomination(DenominationEnum.Quarter); } }
        public static Denomination Dime { get { return CreateDenomination(DenominationEnum.Dime); } }
        public static Denomination Nickel { get { return CreateDenomination(DenominationEnum.Nickel); } }


        private static Denomination CreateDenomination(DenominationEnum type)
        {
            switch(type)
            {
                case DenominationEnum.Nickel:
                {
                    return new Denomination("Nickel", .05m);
                }
                case DenominationEnum.Dime:
                {
                    return new Denomination("Dime", .10m);
                }
                case DenominationEnum.Quarter:
                {
                    return new Denomination("Quarter", .25m);
                }
                case DenominationEnum.HalfDollar:
                {
                    return new Denomination("Half-Dollar", .50m);
                }
                default:
                {
                    throw new InvalidOperationException($"Unable to create denomination {type.GetTypeCode()}");
                }
            }
        }
    }


}
