using VendingMachine.Models;


//***********************************
// Student: Bennett, Neta (netab)
//***********************************

namespace VendingMachine
{
    public static class Calculator
    {
        internal static DenominationBag CalculateChange(decimal productPrice, decimal payment)
        {
            var denominationBag = new DenominationBag();

            var returnTotal = payment - productPrice;


            while (returnTotal >= .50m)
            {
                var hd = DenominationFactory.HalfDollar;
                denominationBag.Coins.Add(hd);
                returnTotal -= hd.Value;
            }

            while (returnTotal >= .25m)
            {
                var qt = DenominationFactory.Quarter;
                denominationBag.Coins.Add(qt);
                returnTotal -= qt.Value;
            }

            while (returnTotal >= .10m)
            {
                var dm = DenominationFactory.Dime;
                denominationBag.Coins.Add(dm);
                returnTotal -= dm.Value;
            }

            while (returnTotal > .00m)
            {
                var nc = DenominationFactory.Nickel;
                denominationBag.Coins.Add(nc);
                returnTotal -= nc.Value;
            }

            return denominationBag;
        }
    }
}
