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
        //public int id;
    }

    class Program
    {
        static Konto[] konton = new Konto[0];

        static void Main(string[] args)
        {
            //LoadUserFile();
            //ValidationOfPassword();
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
            // 1. Skapa en metod som kör SkapaAnvändare. Detta är huvudprogramet som ska köra allt.
            // 2. Skapa en metod som skriver ut "Välj ut användare och lösenord.
            // 3.
            // * Programet ska kunna fråga användare vad deras användare ska vara och lösenord.
            // * Programet ska kunna jämföra om om användare och lösenord matchar vad som redan finns i  "user.txt. filen.
            // * Om det redan finns användare med det programmet måste en text med "Upptaget" skrivas ut.
            // * Om användare inte finns med i text filen så ska användare skapas (läggas till i filen).
            // *

            Konto nyttKonto = new Konto();

            Console.WriteLine("Välj ett användarnamn och ett lösenord");
            Console.Write("Användarnamn: ");
            string användarnamn = Console.ReadLine();
            Console.Write("Lösenord: ");
            string lösenord = Console.ReadLine();

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
                //Console.WriteLine("Felaktigt val, försök igen.22");
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
            string användarnamn = Console.ReadLine();
            Console.Write("Lösenord: ");
            string lösenord = Console.ReadLine();

            string[] inloggningsuppgifter = new string[2];
            inloggningsuppgifter[0] = användarnamn;
            inloggningsuppgifter[1] = lösenord;

            //return inloggningsuppgifter;

            //bool inlogging = ValidationOfPassword(inloggningsuppgifter);

            Matchaanvändarnamn(användarnamn, lösenord);

            /*
            if (inlogging == true) {
                Console.WriteLine("korrekt");
            }
            else if(inlogging == false)
            {
                Console.WriteLine("Fel användarnamn eller lösenord");
            }*/

            //Console.WriteLine("Tryck valfri knapp för att gå vidare");
            //Console.ReadKey();
        }

        //This method check if the input password belongs to the username
        static bool ValidationOfPassword(string[] inloggninsuppgifter)
        {
            string användarnamn = inloggninsuppgifter[0];
            string lösenord = inloggninsuppgifter[1];

            if (användarnamn == "Jonte" && lösenord == "1234")
            {
                return true;
            }
            else
            {
                return false;
            }
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
                string[] values = rader[i].Split('\t');

                //
                Konto konto = new Konto();
                konto.användarnamn = values[0];
                konto.lösenord = values[1];

                //
                lista[i] = konto;
            }

            Konto[] matchalista = lista;

            bool ok = Kollaanvändare(matchalista, användarnamn, lösenord);

            if (ok == true)
            {
                Console.WriteLine("Användarnamn finns listan");
            }
            else if (ok == false)
            {
                Console.WriteLine("Användarnamn finns inte listan");
            }

            //
            /* for (int i = 0; i < lista.Length; i++)
             {
                 Console.Write(lista[i].användarnamn + " ");
                 Console.WriteLine(lista[i].lösenord);
             }*/




            //}
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
                    //Console.WriteLine("Användarnamn finns listan");
                }
            }
            return x;

            //This method check if the input password belongs to the username
            static void WriteMessage() { }
        }

        static void StartSida() {

            Console.WriteLine(@"


 __    __     ______     __     __         ______   ______     ______     ______     ______     ______     __    __    
/\ ""-./  \   /\  __ \   /\ \   /\ \       /\  == \ /\  == \   /\  __ \   /\  ___\   /\  == \   /\  __ \   /\ ""-./  \   
\ \ \-./\ \  \ \  __ \  \ \ \  \ \ \____  \ \  _-/ \ \  __<   \ \ \/\ \  \ \ \__ \  \ \  __<   \ \  __ \  \ \ \-./\ \  
 \ \_\ \ \_\  \ \_\ \_\  \ \_\  \ \_____\  \ \_\    \ \_\ \_\  \ \_____\  \ \_____\  \ \_\ \_\  \ \_\ \_\  \ \_\ \ \_\ 
  \/_/  \/_/   \/_/\/_/   \/_/   \/_____/   \/_/     \/_/ /_/   \/_____/   \/_____/   \/_/ /_/   \/_/\/_/   \/_/  \/_/ 
                                                                                                                       
");
            //Console.ReadKey();
        
        }


       

    }


}



