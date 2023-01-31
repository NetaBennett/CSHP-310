using System;


//***********************************
// CSHP-310
// Assignment: 01
// Student: Bennett, Neta (netab)
//***********************************


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
        public static Denomination GetDenomination(DenominationEnum type)
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
