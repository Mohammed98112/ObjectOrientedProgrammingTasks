namespace OOP_Part1
{
    internal class Program
    {


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


        static void Main(string[] args)
        {


            bool exit = false;
            while (exit == false)
            {
                switch (mainmenue())
                {



                    //case 1) Add New Room

                    case 1:
                        break;


                    //case 2) Register New Guest
                    case 2:
                        break;


                    //case 3) Book a Room for a Guest
                    case 3:
                        break;



                    //case 4) Search & Filter Rooms
                    case 4:
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
