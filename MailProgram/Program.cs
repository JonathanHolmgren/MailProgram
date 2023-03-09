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
        public string text;
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

        static StreamReader LaddaAnvändarFil()
        {
            StreamReader infil = new StreamReader("user.txt", Encoding.GetEncoding(28591));

            return infil;
        }
        static StreamReader LoadmessagesFile()
        {
            StreamReader infil = new StreamReader("meddelande.txt", Encoding.GetEncoding(28591));

            return infil;
        }

        static void SkrivUtMeddelande()
        {
            Console.Clear();
            string[] rader = File.ReadAllLines("meddelande.txt");

            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-");
            for (int i = 0; i < rader.Length; i++)
            {
                string[] värde = rader[i].Split('\t');

                string avsändare = värde[0];
                string mottagare = värde[1];
                string rubrik = värde[2];
                string datum = värde[3];

                if (Inloggingsanvändarnamn == mottagare)
                {
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Avsändare: {0}", avsändare);
                    Console.WriteLine("Rubrik: {0}", rubrik);
                    Console.WriteLine(datum);                    
                }          
               
            }
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-");
        }
  
        static Konto[] HämtaGamlaListan()
        {
            if (!File.Exists("user.txt"))
            {
                return new Konto[0]; // Returnera en tom lista om filen inte finns
            }

            StreamReader infil = LaddaAnvändarFil();

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
            if (menyval != "1" || menyval != "2" || menyval != "3")
            {
                Console.WriteLine("Fel val. Försök igen!");
                FirstMenyOption();
            }
        }

        static void SecondMenyOption()
        {
            Console.Clear();
            Console.WriteLine("Välkommen {0} ", Inloggingsanvändarnamn);
            Console.WriteLine("Välj alternativ ");
           
            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Inkorg");
            Console.WriteLine("\t3 : Ta bort användare");
            Console.WriteLine("\t4 : Avsluta program och spara");
            string menyval = Console.ReadLine();

            if (menyval == "1")
            {
                Console.WriteLine("Skriv meddelande");
                SparaMeddelande();
                Console.Clear();
                SecondMenyOption();
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
                //fourthMenyOption();
                SkrivUtMeddelande();
                Console.WriteLine("Tryck på valfri knapp för att gå vidare!");
                Console.ReadKey();
                SecondMenyOption();
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
            Console.WriteLine("+-+-+-+-+-");
            Console.WriteLine("Logga in: ");
            Console.WriteLine("+-+-+-+-+-");
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

        //This method check if the input password belongs to the username
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
        
        static void SparaMeddelande()
        {
            Meddelande nyttMeddelande = SkapaMeddelande();
            Meddelande[] gamlaMeddelandeLista = HämtaGamlaMListan();
            Meddelande[] nyameddelanden = LäggMeddelandeVektor(gamlaMeddelandeLista, nyttMeddelande);

            StreamWriter utfil = new StreamWriter("meddelande.txt",true); // skapa fil eller öppna om den finns

            foreach (Meddelande meddelande in nyameddelanden)
            {
                utfil.WriteLine(meddelande.användarnamn + "\t" + meddelande.mottagare + "\t" + meddelande.rubrik + "\t" + meddelande.datum + "\t" + meddelande.text);
            }

            utfil.Close(); // Stänger fil

            Console.Clear();
            Console.WriteLine("Ditt Meddelande har nu Skickats.");
            Console.ReadKey();
        }

        // Denna metod är till för att skapa en användare och sätta ett lösen till den.
        static Meddelande SkapaMeddelande()
        {
            Meddelande nyttMeddelande = new Meddelande();

            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-");

            Console.WriteLine("Från: {0} ", Inloggingsanvändarnamn);
            
            Console.Write("Till: ");
            string mottagare = Console.ReadLine();
            
            Console.Write("Rubrik: ");
            string rubrik = Console.ReadLine();
           
            DateTime datum = DateTime.Now;

            Console.WriteLine("Meddelande: ");
            string text = Console.ReadLine();

            nyttMeddelande.användarnamn = Inloggingsanvändarnamn;
            nyttMeddelande.mottagare = mottagare;
            nyttMeddelande.rubrik = rubrik;
            nyttMeddelande.datum = datum;
            nyttMeddelande.text = text;

            return nyttMeddelande;
        }

        static Meddelande[] HämtaGamlaMListan()
        {
            if (!File.Exists("meddelande.txt"))
            {
                return new Meddelande[0]; // Returnera en tom lista om filen inte finns
            }

            StreamReader infil = LoadmessagesFile();

            int antalRader = File.ReadLines("meddelande.txt").Count();
            Meddelande[] gamlaMeddelandeLista = new Meddelande[antalRader];

            int index = 0;

            infil.Close();

            // Om inga Meddelande hittades i filen, returnera en tom lista istället för null
            if (index == 0)
            {
                return new Meddelande[0];
            }

            return gamlaMeddelandeLista;
        }

        public static Meddelande[] LäggMeddelandeVektor(
                    Meddelande[] gamlaMeddelandeLista,
                    Meddelande nyttMeddelande)
        {
            Meddelande[] nyameddelanden = new Meddelande[gamlaMeddelandeLista.Length + 1];

            for (int i = 0; i < gamlaMeddelandeLista.Length; i++)
            {
                nyameddelanden[i] = gamlaMeddelandeLista[i];
            }

            nyameddelanden[gamlaMeddelandeLista.Length] = nyttMeddelande;

            return nyameddelanden;
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
