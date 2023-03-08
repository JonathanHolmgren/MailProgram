using System;
using System.IO;
using System.Text;


namespace MailProgram
{


    class Program
    {
        static string[] messages;

        static void Main(string[] args)
        {

            //LoadUserFile();
            FirstMenyOption();


        }

        // Denna metod är till för att skapa en användare och sätta ett lösen till den.
        static void CreateUser()
        {
            // 1. Skapa en metod som kör SkapaAnvändare. Detta är huvudprogramet som ska köra allt.
            // 2. Skapa en metod som skriver ut "Välj ut användare och lösenord.
            // 3.

            // * Programet ska kunna fråga användare vad deras användare ska vara och lösenord.
            // * Programet ska kunna jämföra om om användare och lösenord matchar vad som redan finns i  "user.txt. filen.
            // * Om det redan finns användare med det programmet måste en text med "Upptaget" skrivas ut.
            // * Om användare inte finns med i text filen så ska användare skapas (läggas till i filen).
            // * 

        }

        static void LoadUserFile()
        {
            StreamReader infil = new StreamReader("user.txt", Encoding.GetEncoding(28591));

            string rad;
            while ((rad = infil.ReadLine()) != null)
            {


            }
        }

        // This method is the first meny choice, here you can choice to create user, choice user and cancel the program.
        static void FirstMenyOption()
        {



            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Skapa användare:");
            Console.WriteLine("\t2 : Välj användare:");
            Console.WriteLine("\t3 : Avsluta program och spara:");
            int menyval = int.Parse(Console.ReadLine());



            if (menyval == 1)
            {
                Console.WriteLine("Skapa användare");
            }
            if (menyval == 2)
            {
                //Console.WriteLine("Välj användare");
                SecondMenyOption();
            }
            if (menyval == 3)
            {
                Console.WriteLine("Är du säker på att du vill avsluta och spara programmet?");
                string inmatning = Console.ReadLine().ToUpper();

                if (inmatning == "J")
                {
                    //endprogram metod
                }
            }
        }

        static void SecondMenyOption()
        {



            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Inkorg");
            Console.WriteLine("\t3 : Ta bort användare");
            Console.WriteLine("\t4 : Avsluta program och spara");
            int menyval = int.Parse(Console.ReadLine());



            if (menyval == 1)
            {
                Console.WriteLine("Skriv meddelande");
            }
            if (menyval == 2)
            {
                ThirdMenyOption();
            }
            if (menyval == 3)
            {
                Console.WriteLine("Ta bort användare");
            }
            if (menyval == 4)
            {
                Console.WriteLine("Är du säker på att du vill avsluta och spara programmet?");
                string inmatning = Console.ReadLine().ToUpper();

                if (inmatning == "J")
                {
                    //endprogram metod
                }
            }
        }

        static void ThirdMenyOption()
        {



            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Läs meddelande:");
            Console.WriteLine("\t2 : Ta bort meddelanden");
            Console.WriteLine("\t3 : Avsluta program och avslutaspara:");

            int menyval = int.Parse(Console.ReadLine());



            if (menyval == 1)
            {
                fourthMenyOption();
            }
            if (menyval == 2)
            {
                Console.WriteLine("Meddelandet är raderat!");
            }
            if (menyval == 3)
            {
                Console.WriteLine("Är du säker på att du vill avsluta och spara programmet?");
                string inmatning = Console.ReadLine().ToUpper();

                if (inmatning == "J")
                {
                    //endprogram metod
                }
            }

        }

        static void fourthMenyOption()
        {



            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Tillbaka till inkorg");


            int menyval = int.Parse(Console.ReadLine());



            if (menyval == 1)
            {
                Console.WriteLine("Läs meddelanden");
            }
            if (menyval == 2)
            {
                ThirdMenyOption();
            }

        }

        //This code will cancel the program.
        static void EndProgram()
        {


        }
        //
        static void ChoiceUser()
        {

        }


        //This method check if the input password belongs to the username 
        static void ValidationOfPassword()
        {


        }
        //This method check if the input password belongs to the username 
        static void WriteMessage()
        {



        }

    }

}


