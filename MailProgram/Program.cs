using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace MailProgram
{
    public class Konto
    {
        public string användarnamn;
        public string lösenord;
    }

    public class Meddelande
    {
        public string användarnamn;
        public string mottagare;
        public string rubrik;
        public DateTime datum;
        public string meddelande;
    }

    class Program
    {
        public static string Inloggingsanvändarnamn;

        static void Main(string[] args)
        {
            StartSida();
            FirstMenyOption();
        }

        static void SparaAnvändareUppgifter()
        {
            Konto nyttkonto = CreateUser();
            Konto[] gamlaKontoLista = HämtaGamlaListan();
            Konto[] nyaAnvändarkonton = LäggAnvändareUppgifterVektor(gamlaKontoLista, nyttkonto);

            StreamWriter utfil = new StreamWriter("user.txt", true); // skapa fil eller öppna om den finns

            foreach (Konto konto in nyaAnvändarkonton)
            {
                utfil.WriteLine(konto.användarnamn + "\t" + konto.lösenord);
            }
            utfil.Close(); // Stänger fil

            Console.Clear();
            Console.WriteLine("Ditt Konto har nu skapats.");
            Console.ReadKey();
        }

        // Denna metod är till för att skapa en användare och sätta ett lösen till den.
        static Konto CreateUser()
        {
            Konto nyttKonto = new Konto();

            Console.WriteLine("Välj ett användarnamn och ett lösenord");
            Console.Write("Användarnamn: ");
            string användarnamn = Console.ReadLine();
            string textpåskärm = "Lösenord: ";
            string lösenord = Skrivlösenord(textpåskärm);

            nyttKonto.användarnamn = användarnamn;
            nyttKonto.lösenord = lösenord;

            return nyttKonto;
        }

        public static Konto[] LäggAnvändareUppgifterVektor(
            Konto[] gamlaAnvändarkonton,
            Konto nyttKonto
        )
        {
            Konto[] nyaAnvändarkonton = new Konto[gamlaAnvändarkonton.Length + 1];

            for (int i = 0; i < gamlaAnvändarkonton.Length; i++)
            {
                nyaAnvändarkonton[i] = gamlaAnvändarkonton[i];
            }

            nyaAnvändarkonton[gamlaAnvändarkonton.Length] = nyttKonto;

            return nyaAnvändarkonton;
        }

        static StreamReader LoadUserFile()
        {
            StreamReader infil = new StreamReader("user.txt", Encoding.GetEncoding(28591));

            return infil;
        }

        static Konto[] HämtaGamlaListan()
        {
            if (!File.Exists("user.txt"))
            {
                return new Konto[0]; // Returnera en tom lista om filen inte finns
            }

            StreamReader infil = LoadUserFile();

            int antalRader = File.ReadLines("user.txt").Count();
            Konto[] gamlaKontoLista = new Konto[antalRader];

            string rad;
            int index = 0;
            while ((rad = infil.ReadLine()) != null)
            {
                string[] delar = rad.Split(',');
                if (delar.Length == 2)
                {
                    Konto konto = new Konto();
                    konto.användarnamn = delar[0];
                    konto.lösenord = delar[1];
                    gamlaKontoLista[index] = konto;
                    index++;
                }
            }
            infil.Close();

            // Om inga konton hittades i filen, returnera en tom lista istället för null
            if (index == 0)
            {
                return new Konto[0];
            }

            return gamlaKontoLista;
        }

        // This method is the first meny choice, here you can choice to create user, choice user and cancel the program.
        static void FirstMenyOption()
        {
            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Skapa användare:");
            Console.WriteLine("\t2 : Logga in:");
            Console.WriteLine("\t3 : Avsluta program och spara:");
            string menyval = Console.ReadLine();

            if (menyval == "1")
            {
                SparaAnvändareUppgifter();
                Console.Clear();
                FirstMenyOption();
            }
            if (menyval == "2")
            {
                LoggaIn();
            }
            if (menyval == "3")
            {
                AvslutaProgram();
            }
            else
            {
                FirstMenyOption();
            }
        }

        static void SecondMenyOption()
        {
            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Inkorg");
            Console.WriteLine("\t3 : Ta bort användare");
            Console.WriteLine("\t4 : Avsluta program och spara");
            string menyval = Console.ReadLine();

            if (menyval == "1")
            {
                Console.WriteLine("Skriv meddelande");
            }
            if (menyval == "2")
            {
                ThirdMenyOption();
            }
            if (menyval == "3")
            {
                Console.WriteLine("Ta bort användare");
                //string användare = "1";
                //Konto[] gamlaKontolista = HämtaGamlaListan();
                //string x = "1";
                int index = SökIndexPåAnvändare(Inloggingsanvändarnamn);
                Konto[] nylista = TaBortAnvändareFrånLista(index);
                //Console.WriteLine(index);

                StreamWriter utfil = new StreamWriter("user.txt"); // skapa fil eller öppna om den finns

                foreach (Konto konto in nylista)
                {
                    utfil.WriteLine(konto.användarnamn + "\t" + konto.lösenord);
                }
                /* foreach (Konto konto in nylista)
                 {
                     Console.WriteLine(konto.användarnamn + "\t" + konto.lösenord);
                 }*/

                utfil.Close(); // Stänger fil
            }
            if (menyval == "4")
            {
                AvslutaProgram();
            }
        }

        static void ThirdMenyOption()
        {
            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Läs meddelande:");
            Console.WriteLine("\t2 : Ta bort meddelanden");
            Console.WriteLine("\t3 : Avsluta program och avslutaspara:");

            string menyval = Console.ReadLine();

            if (menyval == "1")
            {
                fourthMenyOption();
            }
            if (menyval == "2")
            {
                Console.WriteLine("Meddelandet är raderat!");
            }
            if (menyval == "3")
            {
                AvslutaProgram();
            }
        }

        static void fourthMenyOption()
        {
            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Tillbaka till inkorg");

            string menyval = Console.ReadLine();

            if (menyval == "1")
            {
                Console.WriteLine("Läs meddelanden");
            }
            if (menyval == "2")
            {
                ThirdMenyOption();
            }
        }

        //This code will cancel the program.
        static void AvslutaProgram()
        {
            Console.WriteLine("Är du säker på att du vill avsluta och spara programmet?");
            Console.WriteLine("J för avsluta och N för att komma till huvudmenyn.");
            string inmatning = Console.ReadLine().ToUpper();

            if (inmatning == "J")
            {
                Environment.Exit(0);
            }
            else if (inmatning == "N")
            {
                FirstMenyOption();
            }
            else
            {
                Console.WriteLine("Felaktigt Svar! Försök igen");
                AvslutaProgram();
            }
        }

        //
        static void LoggaIn()
        {
            Console.WriteLine("____Logga in____" + "\n");
            Console.Write("Användarnamn: ");
            Inloggingsanvändarnamn = Console.ReadLine();
            Console.Write("Lösenord: ");
            string lösenord = Console.ReadLine();

            string[] inloggningsuppgifter = new string[2];
            inloggningsuppgifter[0] = Inloggingsanvändarnamn;
            inloggningsuppgifter[1] = lösenord;

            Matchaanvändarnamn(Inloggingsanvändarnamn, lösenord);
        }

        static void Matchaanvändarnamn(string användarnamn, string lösenord)
        {
            //
            string[] rader = File.ReadAllLines("user.txt");

            //
            Konto[] lista = new Konto[rader.Length];

            //
            for (int i = 0; i < rader.Length; i++)
            {
                //
                string[] värde = rader[i].Split('\t');

                //
                Konto konto = new Konto();
                konto.användarnamn = värde[0];
                konto.lösenord = värde[1];

                //
                lista[i] = konto;
            }

            bool ok = Kollaanvändare(lista, användarnamn, lösenord);

            if (ok == true)
            {
                SecondMenyOption();
            }
            else if (ok == false)
            {
                Console.WriteLine("Användarnamn finns inte listan");
            }
        }

        static bool Kollaanvändare(Konto[] lista, string användarnamn, string lösenord)
        {
            bool x = false;

            for (int i = 0; i < lista.Length; i++)
            {
                // if (lista[i].användarnamn == användarnamn)
                if (lista[i].användarnamn == användarnamn && lista[i].lösenord == lösenord)
                {
                    x = true;
                    break;
                }
            }
            return x;

            //This method check if the input password belongs to the username
            static void WriteMessage() { }
        }

        static int SökIndexPåAnvändare(string användare)
        {
            string[] rader = File.ReadAllLines("user.txt");

            string[] lista = new string[rader.Length];

            for (int i = 0; i < rader.Length; i++)
            {
                string[] värde = rader[i].Split('\t');

                string användarnamn = värde[0];

                lista[i] = användarnamn;
            }

            int index = Array.IndexOf(lista, användare);

            return index;
        }

        static Konto[] TaBortAnvändareFrånLista(int index)
        {
            string[] rader = File.ReadAllLines("user.txt");

            Konto[] nyLista = new Konto[rader.Length - 1];

            for (int i = 0; i < index; i++)
            {
                string[] värde = rader[i].Split('\t');
                Konto konto = new Konto();
                konto.användarnamn = värde[0];
                konto.lösenord = värde[1];
                nyLista[i] = konto;
            }
            for (int i = index + 1; i < rader.Length; i++)
            {
                string[] värde = rader[i].Split('\t');
                Konto konto = new Konto();
                konto.användarnamn = värde[0];
                konto.lösenord = värde[1];
                nyLista[i - 1] = konto;
            }

            return nyLista;
        }

        static void StartSida()
        {
            Console.WriteLine(
                @"███╗   ███╗ █████╗ ██╗██╗     ██████╗ ██████╗  ██████╗  ██████╗ ██████╗  █████╗ ███╗   ███╗
████╗ ████║██╔══██╗██║██║     ██╔══██╗██╔══██╗██╔═══██╗██╔════╝ ██╔══██╗██╔══██╗████╗ ████║
██╔████╔██║███████║██║██║     ██████╔╝██████╔╝██║   ██║██║  ███╗██████╔╝███████║██╔████╔██║
██║╚██╔╝██║██╔══██║██║██║     ██╔═══╝ ██╔══██╗██║   ██║██║   ██║██╔══██╗██╔══██║██║╚██╔╝██║
██║ ╚═╝ ██║██║  ██║██║███████╗██║     ██║  ██║╚██████╔╝╚██████╔╝██║  ██║██║  ██║██║ ╚═╝ ██║
╚═╝     ╚═╝╚═╝  ╚═╝╚═╝╚══════╝╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝
                                                                                           "
            );
            Console.WriteLine(
                @"              _
             | |
             | |===( )   //////
             |_|   |||  | o o|
                    ||| ( c  )                  ____
                     ||| \= /                  ||   \_
                      ||||||                   ||     |
                      ||||||                ...||__/|-""
                      ||||||             __|________|__
                        |||             |______________|
                        |||             || ||      || ||
                        |||             || ||      || ||
------------------------|||-------------||-||------||-||-------
                        |__>            || ||      || ||


     hit any key to continue"
            );

            Console.ReadKey();
            Console.Clear();
        }

        static string Skrivlösenord(string textpåskräm)
        {
            string valtlösenord = "";
            try
            {
                Console.Write(textpåskräm);
                valtlösenord = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        valtlösenord += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && valtlösenord.Length > 0)
                        {
                            valtlösenord = valtlösenord.Substring(0, (valtlösenord.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            if (string.IsNullOrWhiteSpace(valtlösenord))
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Fältet kan ej vara tomt!");
                                Skrivlösenord(textpåskräm);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("");
                                break;
                            }
                        }
                    }
                } while (true);
                return valtlösenord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
