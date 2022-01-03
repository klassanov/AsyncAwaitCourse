using System;
using System.Threading.Tasks;

namespace BurgerPreparation
{
    class AsyncCooker
    {
        public static async Task Work()
        {
            //These 3 have no dependencies
            await Task.WhenAll(
                HeatPans(),
                UnfreezeMeat(),
                PeelPotatoes());


            //These 3 depend on the previous 3, so I should wait for them
            await Task.WhenAll(
                FryFries(),
                PourDrinks(),
                CookBurgers());


            //This depends on everything done previously, so I should wait for them
            await ServeAndEat();
        }

        public static async Task HeatPans()
        {
            await Task.Delay(2000);
            Console.WriteLine("Pans heated");
        }

        public static async Task UnfreezeMeat()
        {
            await Task.Delay(3000);
            Console.WriteLine("Meate Unfreezed");
        }

        public static async Task PeelPotatoes()
        {
            await Task.Delay(2000);
            Console.WriteLine("Potatoes peeled!");
        }

        public static async Task CookBurgers()
        {
            await Task.Delay(5000);
            Console.WriteLine("Burgers cooked!");
        }

        public static async Task FryFries()
        {
            await Task.Delay(3000);
            Console.WriteLine("Fries fried!");
        }

        public static async Task PourDrinks()
        {
            await Task.Delay(1000);
            Console.WriteLine("Drinks poured!");
        }

        public static async Task ServeAndEat()
        {
            await Task.Delay(5000);
            Console.WriteLine("Delicious!");
        }
    }
}
