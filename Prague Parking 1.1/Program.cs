using System;

namespace PragueParking
{
    class Program
{
    static string[] parkingSpots = new string[100];

        static void Main(string[] args)
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

        if (string.IsNullOrWhiteSpace(regnr) || regnr.Length > 10)
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

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            String spot = parkingSpots[i];

            if (type == "CAR")
            {
                if (string.IsNullOrEmpty(spot))
                {
                    parkingSpots[i] = $"CAR: {regnr}";
                    Console.WriteLine($"Bil {regnr} parkerades på¨plats {i + 1}");
                    Pause();
                    return;

                }
            }
            else if (type == "MC")
            {
                if (string.IsNullOrEmpty(spot))
                {
                    parkingSpots[i] = $"MC: {regnr}";
                    Console.WriteLine($"MC {regnr} parkerades på plats {i + 1}");
                    Pause();
                    return;
                }
            }
        }
        Console.WriteLine("Ingen ledig plats hittades.");
        Pause();
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

        Console.WriteLine("Ange ny plats (1-100:) ");
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
            if (string.IsNullOrEmpty(parkingSpots[toIndex]))
            {
                parkingSpots[toIndex] = vehicle;
                Console.WriteLine($"Bilen flyttades till plats {newSpot}");
                Pause();
            }
            else
            {
                Console.WriteLine("Platsen är upptagen.");
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
            else if (parkingSpots[toIndex].StartsWith($"MC:") && parkingSpots[toIndex].Split(',').Length == 1)
            {
                parkingSpots[toIndex] += $", {vehicle}";
                Console.WriteLine($"MC:n parkerades på plats {newSpot}");
                Pause();
            }
            else
            {
                Console.WriteLine("Platsen är upptagen.");
                parkingSpots[fromIndex] = AppendToSpot(parkingSpots[fromIndex], vehicle);
                Pause();
            }
        }
    }


static void RemoveVehicle()
    {
        Console.Clear();
        Console.Write("Ange registreringsnumret på fordonet som ska tas bort: ");
        string regnr = Console.ReadLine()?.Trim().ToUpper();

        int index = FindVehicleIndex(regnr);
        if (index == -1)
        {
            Console.WriteLine("Fordonet hittades inte");
            Pause();
            return;
        }

        parkingSpots[index] = RemoveFromSpot(parkingSpots[index], regnr);
        Console.WriteLine($"Fordonet {regnr} har tagits bort från parkeringen.");
        Pause();
    }

    static void SearchVehicle()
    {
        Console.Clear();
        Console.WriteLine("Ange registreringsnumret att söka efter: ");
        string regnr = Console.ReadLine()?.Trim().ToUpper();

        int index = FindVehicleIndex(regnr);
        if (index == -1)
        {
            Console.WriteLine("Fordonet hittades inte.");
        }
        else
        {
            Console.WriteLine($"Fordonet {regnr} står på plats {index + 1}");
        }
        Pause();
    }

    static void ShowParking()
    {
        Console.Clear();
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            Console.WriteLine($"Plats {i + 1}: {parkingSpots[i]}");
        }
        Pause();
    }

    static int FindVehicleIndex(string regnr)
    {
        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null && parkingSpots[i].ToUpper().Contains(regnr))
            {
                return i;
            }
        }
        return -1;
    }

    static bool VehicleExists(string regnr)
    {
        return FindVehicleIndex(regnr) != -1;
    }

    static string RemoveFromSpot(string spot, string regnr)
    {
        string[] vehicles = spot.Split(',');
        string result = string.Join(",", Array.FindAll(vehicles, v => !v.EndsWith(regnr)));
        return result;
    }

    static string ExtractVehicle(int index, string regnr)
    {
        string[] vehicles = parkingSpots[index].Split(',');
        string found = Array.Find(vehicles, v => v.EndsWith(regnr));
        parkingSpots[index] = RemoveFromSpot(parkingSpots[index], regnr);
        return found;
    }

    static string AppendToSpot(string spot, string vehicle)
    {
        if (string.IsNullOrEmpty(spot))
            return vehicle;
        else return spot + "," + vehicle;
    }

        static void Pause()
        {
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}



