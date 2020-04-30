using System;
using System.Collections.Generic;
using System.Linq;

namespace Need_For_Speed
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<int>> race = new Dictionary<string, List<int>>();

            int num = int.Parse(Console.ReadLine());

            string input;

            while (num != 0)
            {
                input = Console.ReadLine();
                string[] carInfo = input.Split("|");
                string carModel = carInfo[0];
                int mileage = int.Parse(carInfo[1]);
                int fuel = int.Parse(carInfo[2]);

                if (!race.ContainsKey(carModel))
                {
                    race[carModel] = new List<int>() { mileage, fuel };
                }
                num--;
            }
            while ((input = Console.ReadLine()) != "Stop")
            {
                string[] arg = input.Split(" : ");
                string command = arg[0];
                string car = arg[1];

                if (command == "Drive")
                {
                    if (race.ContainsKey(car))
                    {
                        int distance = int.Parse(arg[2]);
                        int fuelToDrive = int.Parse(arg[3]);

                        if (race[car][1] < fuelToDrive)
                        {
                            Console.WriteLine("Not enough fuel to make that ride");
                        }
                        else
                        {
                            race[car][0] += distance;
                            race[car][1] -= fuelToDrive;
                            Console.WriteLine($"{car} driven for {distance} kilometers. {fuelToDrive} liters of fuel consumed.");
                            if (race[car][0] >= 100000)
                            {
                                Console.WriteLine($"Time to sell the {car}!");
                                race.Remove(car);
                            }
                        }
                    }

                }
                else if (command == "Refuel")
                {
                    int fuel = int.Parse(arg[2]);


                    if (race[car][1] < 75)
                    {
                        int neededFuel = 75 - race[car][1];
                        race[car][1] += fuel;
                        if (race[car][1] > 75)
                        {
                            Console.WriteLine($"{car} refueled with {neededFuel} liters");
                            race[car][1] = 75;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"{car} refueled with {fuel} liters");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{car} refueled with {fuel} liters");
                    }
                }
                else if (command == "Revert")
                {
                    int kilometres = int.Parse(arg[2]);
                    race[car][0] -= kilometres;

                    if (race[car][0] < 10000)
                    {
                        race[car][0] = 10000;
                        continue;
                    }
                    Console.WriteLine($"{car} mileage decreased by {kilometres} kilometers");
                }
            }
            race = race.OrderByDescending(r => r.Value[0]).ThenBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);

            foreach (var kvp in race)
            {
                Console.WriteLine($"{kvp.Key} -> Mileage: {kvp.Value[0]} kms, Fuel in the tank: {kvp.Value[1]} lt.");
            }
        }
    }
}
