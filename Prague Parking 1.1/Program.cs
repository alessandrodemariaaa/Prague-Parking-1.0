using system;
using System.ComponentModel.Design;

namespace PragueParking

    class program
{
    static string[] parkingSpots = new string[100];

        static void main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Prague Parking 1.0");
            Console.WriteLine("1. Parkera fordon");
            Console.WriteLine("2. Flytta fordon");
            Console.WriteLine("3. Hämta ut fordon");
            Console.WriteLine("4. Sök fordon");
            Console.WriteLine("5. Visa parkerade fordon");
            Console.WriteLine("0. Avsluta");
            Console.Write("\nVälj ett alternativ: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ParkVehicle(); break;
                case "2": MoveVehicle(); break;
                case "3": RemoveVehicle(); break;
                case "4": SearchVehicle(); break;
                case "5": ShowParking(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Ogiltigt val. Tryck på valfri tangent för att fortsätta..."); Pause(); break;
            }
        }
    }


    static void ParkVehicle()
    {
        Console.Clear();
        Console.Write("Ange fordonstyp (CAR eller MC): ");
        string type = Console.ReadLine()?.Trim().ToUpper();

        if (type != "CAR" && type != "MC")
        {
            Console.WriteLine("Ogiltig fordonstyp.");
            Pause();
            return;
        }

        Console.WriteLine("Ange ett registreringsnummer (max 10 tecken): ");
        string regnr = Console.ReadLine()?.Trim().ToUpper();

        if ((string.IsNullOrWhiteSpace(regnr) || regnr.Length > 100)
        {
            Console.WriteLine("Ogiltligt registreringnummer.");
            Pause();
            return;
        }

        if (VehicleExists(regnr))
        {
            Console.WriteLine("Fordonet finns redan parkerat.");
            Pause();
            return;
        }

        for (int i = 0; < parkingSpots.Length; i++)
        {
            String spot = parkingSpots[i];

            if (type == "CAR")
            {
                if (string.IsNullOrEmpty(spot))
                {
                    parkingSpots[i] = $"CAR: {regnr}";
                    Console.WriteLine($"Bilen {regnr} parkerades på¨plats {i + 1}");
                    Pause();
                    return;

                }
            }
            else if (type == "MC")
            {
                if (string.IsNullOrEmpty(spot))
                {
                    parkingSpots[i] = $"MC: {regnr}";
                    Console.WriteLine($"MC {regnr} dubbelparkerades på plats {i + 1}");
                    Pause();
                    return;
                }
            }
        }
        Console.WriteLine("Ingen ledig plats hittades.");
        pause();
    }

    static void MoveVehicle()
    {
        Console.Clear();
        Console.Write("Ange registreringsnummer för fordonet som ska flyttas: ");
        string regnr = Console.ReadLine()?.Trim().ToUpper();

        int fromIndex = FindVehicleIndex(regnr);

        if (fromIndex == -1)
        {
            Console.WriteLine("Fordonet hittades inte");
            Pause();
            return;
        }

        Console.WriteLine("Ange ny plats (1-100: ");
        if (!int.TryParse(Console.ReadLine(), out int newSpot) || newSpot < 1 || newSpot > 100)
        {
            Console.WriteLine("Ogiltigt platsnummer.");
            Pause();
            return;
        }

        int toIndex = newSpot - 1;
        string vehicle = ExtractVehicle(fromIndex, regnr);

        if (vehicle.StartsWith("CAR:"))
        {
            if ((string.IsNullOrEmpty(parkingSpots[toIndex]))
            {
                parkingSpots[toIndex] = vehicle;
                Console.WriteLine($"Bilen flyttades till plats {newSpot}");
                Pause();
            }
            else
            {
                Console.WriteLine("Platsen är upptagen.");
                // Lägg tillbaka fordonet där det var
                parkingSpots[fromIndex] = AppendToSpot(parkingSpots[fromIndex], vehicle);
                Pause();
            }
        }
        else if (vehicle.StartsWith("MC:"))
        {
            if (string.IsNullOrEmpty(parkingSpots[toIndex]))
            {
                parkingSpots[toIndex] = vehicle;
                Console.WriteLine($"MC:n flyttades till plats {newSpot}");
                Pause();
            }
            else if (parkingSpots[toIndex].StartsWith("MC:") && (parkingSpots[toIndex].Split(',')Length == 1)
            {
                parkingSpots[toIndex] += $", {vehicle}";
                Console.WriteLine($"MC:n dubbelparkerades på plats {newSpot}");
                Pause();
            }
            else
            {
                Console.WriteLine("Platsen är upptagen.");
                parkingSpots[fromIndex] = Appendtospot(parkingSpots[fromIndex], vehicle);
                Pause();
            }
        }
    }





    