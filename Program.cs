using System;
using System.IO;

namespace GeekTrust
{
    class Program
    {
        private static int corporationWater = 0;
        private static int borewellWater = 0;
        private static int waterConsumedByGuest = 0;
        private static int unitCost = 0;
        private static int consumptionPerPerson = 0;
        private static int guest = 0;
        static void Main(string[] args)
        {
            try
            {
                string[] inputData = File.ReadAllLines(args[0]);
                //Add your code here to process the input commands

                // string[] inputData = File.ReadAllLines(@"C:\Source\GeekTrust\csharp-starter-kit-water-management\GeekTrust\sample_input\input3.txt");

                for (int i = 0; i < inputData.Length; i++)
                {
                    string[] inputType = inputData[i].Trim().Split(" ");
                    if (inputType[0] == "ALLOT_WATER")
                    {
                        string apartmentType = inputType[1];
                        string[] waterRatio = inputType[2].Split(":");
                        corporationWater = Convert.ToInt32(waterRatio[0]);
                        borewellWater = Convert.ToInt32(waterRatio[1]);
                        allotWater(apartmentType, corporationWater, borewellWater);
                    }
                    else if (inputType[0] == "ADD_GUESTS")
                    {
                        var guest = inputType[1];
                        addGuest(guest);
                    }
                    else if (inputType[0] == "BILL")
                    {
                        Console.WriteLine(totalWaterUtilisedInLtrs() + " " + getBill());
                        Console.Read();
                    }
                }

                static int totalWaterUtilisedInLtrs()
                {
                    return waterConsumedByGuest + consumptionPerPerson;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static void addGuest(string guestCount)
        {
            guest += Convert.ToInt32(guestCount);
            waterConsumedByGuest = guest * 10 * 30;
        }

        private static void allotWater(string apartmentType, int corporationWater, int borewellWater)
        {
            if (apartmentType == "2")
            {
                consumptionPerPerson = 900;
            }
            else if (apartmentType == "3")
            {
                consumptionPerPerson = 1500;
            }

            unitCost = consumptionPerPerson / (corporationWater + borewellWater);
        }

        private static int getBill()
        {
            int guestBill = 0;

            if (waterConsumedByGuest > 0)
            {
                guestBill = waterConsumedByGuest * 2;
            }
            if (waterConsumedByGuest > 500)
            {
                guestBill = 500 * 2 + (waterConsumedByGuest - 500) * 3;
            }
            if (waterConsumedByGuest > 1500)
            {
                guestBill = 500 * 2 + 1000 * 3 + (waterConsumedByGuest - 1500) * 5;
            }
            if (waterConsumedByGuest > 3000)
            {
                guestBill =
                  500 * 2 + 1000 * 3 + 1500 * 5 + (waterConsumedByGuest - 3000) * 8;
            }

            int totalBill = Convert.ToInt32(Math.Ceiling(unitCost * corporationWater * 1 + unitCost * borewellWater * 1.5 + guestBill));
            return totalBill;
        }
    }
}
