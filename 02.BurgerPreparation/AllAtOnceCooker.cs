using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerPreparation
{
    class AllAtOnceCooker
    {
        public static async Task Work()
        {
            List<Task> cookingTasks = new List<Task>
            {
                HeatPans(),
                UnfreezeMeat(),
                PeelPotatoes(),
                CookBurgers(),
                FryFries(),
                PourDrinks(),
                ServeAndEat()
            };

            await Task.WhenAll(cookingTasks);
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
