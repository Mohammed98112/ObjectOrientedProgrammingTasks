using System.Drawing;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOP_Part1
{

    public class Room  // Represents a hotel room with details such as room number, type, price per night, and availability status.

    {
        public int RoomNumber;
        public string RoomType;
        public double PricePerNight;
        public bool IsAvailable;
}

    public class Guest // Represents a hotel guest, storing their personal details, assigned room number, check-in date, and number of nights staying.

    {
        public string GuestId;
        public string GuestName;
        public string RoomNumber;
        public DateTime CheckInDate;
        public int TotalNights;
    }
    internal class Program
    {
        static List<Room> rooms = new List<Room>(); // Stores all rooms in the hotel system (used for adding, searching, filtering, and booking operations)

        static List<Guest> guests = new List<Guest>(); // Stores all registered guests in the hotel system (used for booking, check-in, and check-out operations)



       

        //MainMenu
        public static int mainmenue()
        {
            
            Console.WriteLine("=================================");
            Console.WriteLine(" GRAND VISTA HOTEL — MANAGEMENT SYSTEM");
            Console.WriteLine("=================================");

            Console.WriteLine("1. Add New Room");
            Console.WriteLine("2. Register New Guest");
            Console.WriteLine("3. Book a Room for a Guest");
            Console.WriteLine("4. Search & Filter Rooms");
            Console.WriteLine("5. Guest & Booking Statistics");
            Console.WriteLine("6. Check Out a Guest");
            Console.WriteLine("7. Remove Unavailable Rooms");
            Console.WriteLine("0. exit");
            Console.WriteLine("=================================");

            Console.WriteLine("enter your choice: ");

            return Convert.ToInt32(Console.ReadLine());


        }
        

        //case 1) Add New Room

        public static void AddNewRoom()
        {

            //req1: Prompt the clerk to enter: room number, room type (Single / Double / Suite), and price per night.

            Console.WriteLine("Enter room number: ");
            int RoomNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter  room type (Single / Double / Suite): ");
            string RoomType = Console.ReadLine();
            Console.WriteLine("Enter room price: ");
            double RoomPrice = Convert.ToDouble(Console.ReadLine());

            //validated as positive numbers.
            if (RoomNumber <= 0 || RoomPrice <= 0)
            {
                Console.WriteLine("roomNumber and price must be positive");
                return;
            }
            //req2: Before adding, check whether a room with the same room number already exists in the rooms list. If it does, display anerror and return to the menu without adding.

            //Duplicate check using LINQ Any()
            bool Roomcheck = rooms.Any(r=> r.RoomNumber == RoomNumber);
            if (Roomcheck)
            {
                Console.WriteLine("Error: Room already exists.");
                return;

            }


            //req3: If the room number is unique, create a new Room object and add it to the rooms list. The room is always available whenfirst added.
            Room NewRoom = new Room
            {
                RoomNumber = RoomNumber,
                RoomType = RoomType,
                PricePerNight = RoomPrice,
                IsAvailable = true
              

            };

            rooms.Add(NewRoom);

            //req4: Display a success message showing all entered details and the updated total room count.

            Console.WriteLine("Room added successfully!");
            Console.WriteLine($"Room Number : {NewRoom.RoomNumber}");
            Console.WriteLine($"Room Type   : {NewRoom.RoomType}");
            Console.WriteLine($"Price       : {NewRoom.PricePerNight}");
            Console.WriteLine($"Available   : {NewRoom.IsAvailable}");
            Console.WriteLine($"Total Rooms : {rooms.Count}");


        }



        //case 2) Register New Guest

        public static void RegisterNewGuest()
        {
            //req1: Prompt for: guest name, check -in date(as a string), and number of nights they plan to stay.

            Console.WriteLine("Enter guest name: ");
            string GuestName = Console.ReadLine();
            Console.WriteLine("Enter the date: ");
            DateTime CheckInDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Total Night: ");
            int TotalNights = Convert.ToInt32(Console.ReadLine());

            // validation: nights must be positive
            if (TotalNights <= 0)
            {
                Console.WriteLine("Error: Total nights must be a positive number.");
                return;
            }


            //req 2: Auto - generate the guest ID from the current size of the guests list(format: G001, G002, G003...).

            string generatedGuestId = "G" + (guests.Count + 1).ToString("D3");


            //req 3: Create a new Guest object with roomNumber set to a default value of 'Not Assigned', then add it to the guests list.
            Guest newGuest = new Guest
            {
                GuestId = generatedGuestId,
                GuestName = GuestName,
                CheckInDate = CheckInDate,
                TotalNights = TotalNights,
                RoomNumber = "Not Assigned"
            };

            guests.Add(newGuest);

            //req 4: Display a confirmation showing the generated guest ID and all entered details.

            Console.WriteLine("Guest registered successfully!");
            Console.WriteLine($"Guest ID   : {newGuest.GuestId}");
            Console.WriteLine($"Name       : {newGuest.GuestName}");
            Console.WriteLine($"Check-in   : {newGuest.CheckInDate}");
            Console.WriteLine($"Nights     : {newGuest.TotalNights}");
            Console.WriteLine($"Room       : {newGuest.RoomNumber}");
        }


        //case 3) Book a Room for a Guest

        public static void BookaRoomforaGuest()
        {
            //req1: Prompt for the guest ID and the desired room number.

            Console.Write("Enter Guest ID: ");
            string guestID = Console.ReadLine();

            Console.Write("Enter Room Number: ");
            int roomNum = Convert.ToInt32(Console.ReadLine());


            //req2: Use LINQ to find the guest in the guests list and the room in the rooms list.
            //If either is not found, display an appropriate error and return.

            // Find the guest using LINQ FirstOrDefault()
            Guest guest = guests.FirstOrDefault(g => g.GuestId == guestID);

            if (guest == null)
            {
                Console.WriteLine("Error: Guest not found.");
                return;
            }

            // Find the room using LINQ FirstOrDefault()
            Room room = rooms.FirstOrDefault(r => r.RoomNumber == roomNum);

            if (room == null)
            {
                Console.WriteLine("Error: Room not found.");
                return;
            }


            //req3: Check that the selected room is currently available.
            //If not, display "Room is already booked." and return.

            if (!room.IsAvailable)
            {
                Console.WriteLine("Room is already booked.");
                return;
            }


            //req4: If all checks pass:
            //Assign the room number to the guest's RoomNumber field,
            //and set the room's IsAvailable to false.

            guest.RoomNumber = room.RoomNumber.ToString();
            room.IsAvailable = false;


            //req5: Display a booking confirmation showing:
            //Guest name, room number, room type,
            //price per night, total nights,
            //and total cost using CalculateTotalCost().

            double totalCost = room.PricePerNight * guest.TotalNights;

            Console.WriteLine("\nBooking Successful!");
            Console.WriteLine($"Guest Name   : {guest.GuestName}");
            Console.WriteLine($"Room Number  : {room.RoomNumber}");
            Console.WriteLine($"Room Type    : {room.RoomType}");
            Console.WriteLine($"Price/Night  : {room.PricePerNight}");
            Console.WriteLine($"Total Nights : {guest.TotalNights}");
            Console.WriteLine($"Total Cost   : {totalCost}");
        }


        //case 4) Search & Filter Rooms

        //req1: Display a sub-menu: (1) Show all available rooms (2)
        //Filter by room type (3) Filter by max price (4) Room price statistics(0) Back.

        //sub menu method
        static void SubMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("--- Search & Filter Rooms ---");
                Console.WriteLine("1) Show all available rooms");
                Console.WriteLine("2) Filter by room type");
                Console.WriteLine("3) Filter by max price");
                Console.WriteLine("4) Room price statistics");
                Console.WriteLine("0) Back");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllAvailableRooms();
                        break;
                    case "2":
                        FilterByRoomType();
                        break;
                    case "3":
                        FilterByMaxPrice();
                        break;
                    case "4":
                        RoomPriceStatistics();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        //req2: Option 1: retrieve all rooms where isAvailable is true using Where(), sorted by price ascending with OrderBy(). Display
        //count of results.
        public static void ShowAllAvailableRooms()
        {
            var availableRooms = rooms
                .Where(r => r.IsAvailable)
                .OrderBy(r => r.PricePerNight);

            Console.WriteLine($"Available Rooms: {availableRooms.Count()}");

            //req6: If no rooms match display an appropriate message.
            if (!availableRooms.Any())
            {
                Console.WriteLine("No rooms found for the selected criteria.");
                return;
            }

            foreach (var room in availableRooms)
            {
                Console.WriteLine($"Room Number : {room.RoomNumber}");
                Console.WriteLine($"Room Type   : {room.RoomType}");
                Console.WriteLine($"Price       : {room.PricePerNight:F2}");
                Console.WriteLine($"Available   : {room.IsAvailable}");
                Console.WriteLine("----------------------------");
            }
        }

        //req3: Option 2: prompt for a room type, then use Where() to retrieve all rooms of that type regardless of availability. Display
        //count and results.
        public static void FilterByRoomType()
        {
            Console.Write("Enter Room Type (Single/Double/Suite): ");
            string roomType = Console.ReadLine();

            var filteredRooms = rooms
               .Where(r => r.RoomType.ToUpper() == roomType.ToUpper())

            Console.WriteLine($"Rooms Found: {filteredRooms.Count()}");

            //req6: If no rooms match display an appropriate message.
            if (!filteredRooms.Any())
            {
                Console.WriteLine("No rooms found for the selected criteria.");
                return;
            }

            foreach (var room in filteredRooms)
            {
                Console.WriteLine($"Room Number : {room.RoomNumber}");
                Console.WriteLine($"Room Type   : {room.RoomType}");
                Console.WriteLine($"Price       : {room.PricePerNight:F2}");
                Console.WriteLine($"Available   : {room.IsAvailable}");
                Console.WriteLine("----------------------------");
            }
        }

        //req4: Option 3: prompt for a maximum price, then use Where() to retrieve available rooms at or below that price, sorted
        //ascending.Display count.
        public static void FilterByMaxPrice()
        {
            Console.Write("Enter Maximum Price: ");
            double maxPrice = Convert.ToDouble(Console.ReadLine());

            var filteredRooms = rooms
                .Where(r => r.IsAvailable && r.PricePerNight <= maxPrice)
                .OrderBy(r => r.PricePerNight);

            Console.WriteLine($"Rooms Found: {filteredRooms.Count()}");

            //req6: If no rooms match display an appropriate message.
            if (!filteredRooms.Any())
            {
                Console.WriteLine("No rooms found for the selected criteria.");
                return;
            }

            foreach (var room in filteredRooms)
            {
                Console.WriteLine($"Room Number : {room.RoomNumber}");
                Console.WriteLine($"Room Type   : {room.RoomType}");
                Console.WriteLine($"Price       : {room.PricePerNight:F2}");
                Console.WriteLine($"Available   : {room.IsAvailable}");
                Console.WriteLine("----------------------------");
            }
        }

        //req5: Option 4: use LINQ aggregation on the rooms list to display — total rooms (Count), available rooms (Count with
        //condition), average price(Average), cheapest price(Min), most expensive price(Max).
        public static void RoomPriceStatistics()
        {
            if (!rooms.Any())
            {
                Console.WriteLine("No rooms available.");
                return;
            }

            Console.WriteLine($"Total Rooms       : {rooms.Count()}");
            Console.WriteLine($"Available Rooms   : {rooms.Count(r => r.IsAvailable)}");
            Console.WriteLine($"Average Price     : {rooms.Average(r => r.PricePerNight):F2}");
            Console.WriteLine($"Cheapest Price    : {rooms.Min(r => r.PricePerNight):F2}");
            Console.WriteLine($"Most Expensive    : {rooms.Max(r => r.PricePerNight):F2}");
        }









        //case 5) Guest & Booking Statistics

        //case 6) Check Out a Guest

        //case 7) Remove Unavailable Rooms


        static void Main(string[] args)
        {
            // Pre-load 6 rooms before starting the system (mix of Single, Double, Suite)
            rooms.Add(new Room { RoomNumber = 101, RoomType = "Single", PricePerNight = 60, IsAvailable = true });
            rooms.Add(new Room { RoomNumber = 102, RoomType = "Single", PricePerNight = 88, IsAvailable = true });
            rooms.Add(new Room { RoomNumber = 103, RoomType = "Double", PricePerNight = 77, IsAvailable = true });
            rooms.Add(new Room { RoomNumber = 104, RoomType = "Double", PricePerNight = 14, IsAvailable = true });
            rooms.Add(new Room { RoomNumber = 105, RoomType = "Suite", PricePerNight = 555, IsAvailable = true });
            rooms.Add(new Room { RoomNumber = 106, RoomType = "Suite", PricePerNight = 88888, IsAvailable = true });


            bool exit = false;
            while (exit == false)
            {
                switch (mainmenue())
                {



                    //case 1) Add New Room

                    case 1:
                        AddNewRoom();
                        break;


                    //case 2) Register New Guest
                    case 2:
                        RegisterNewGuest();
                        break;


                    //case 3) Book a Room for a Guest
                    case 3:
                        BookaRoomforaGuest();
                        break;



                    //case 4) Search & Filter Rooms
                    case 4:
                        SubMenu();
                        break;


                    //case 5) Guest & Booking Statistics
                    case 5:
                        break;



                    //case 6) Check Out a Guest
                    case 6:
                        break;


                    //case 7) Remove Unavailable Rooms
                    case 7:
                        break;


                    //case 0) Exit
                    case 0:
                        exit = true;
                        break;


                    //wrong option
                    default:
                        Console.WriteLine("invalid option");
                        break;

                }
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
                Console.Clear();








            }
        }
    }
}
