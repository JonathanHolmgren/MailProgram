using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
            FörstaMenyVal();
        }

        // *************************************** DETTA ÄR MENY VALEN ***************************************
        //****************************************************************************************************

        static void FörstaMenyVal()
        {
            Console.Clear();
            Console.WriteLine("Välj alternativ");
            Console.WriteLine("\t1 : Skapa användare:");
            Console.WriteLine("\t2 : Logga in:");
            Console.WriteLine("\t3 : Avsluta program och spara:");

            int.TryParse(Console.ReadLine(), out int menyval);

            switch (menyval)
            {
                case 1:
                    SparaAnvändareUppgifter();
                    Console.Clear();
                    FörstaMenyVal();
                    break;

                case 2:
                    LoggaIn();
                    break;

                case 3:
                    AvslutaProgram();
                    break;

                default:
                    Console.WriteLine("Ogiltigt val, Försök igen.");
                    Console.ReadKey();
                    FörstaMenyVal();
                    break;
            }
        }

        static void InloggadMeny()
        {
            Console.Clear();
            Console.WriteLine("Välkommen {0} ", Inloggingsanvändarnamn);
            Console.WriteLine("Välj alternativ ");

            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Inkorg");
            Console.WriteLine("\t3 : Ta bort användare");
            Console.WriteLine("\t4 : Avsluta program och spara");

            int.TryParse(Console.ReadLine(), out int menyval);

            switch (menyval)
            {
                case 1:
                    Console.WriteLine("Skriv meddelande");
                    SparaMeddelande();
                    Console.Clear();
                    InloggadMeny();
                    break;

                case 2:
                    InkorgMeny();
                    break;

                case 3:
                    TaBortAnvändare();
                    break;

                case 4:
                    AvslutaProgram();
                    break;

                default:
                    Console.WriteLine("Ogiltigt val, Försök igen.");
                    Console.ReadKey();
                    InloggadMeny();
                    break;
            }
        }

        static void InkorgMeny()
        {
            Console.Clear();
            Console.WriteLine("Välj alternativ");

            Console.WriteLine("\t1 : Läs meddelande:");
            Console.WriteLine("\t2 : Ta bort meddelanden");
            Console.WriteLine("\t3 : Avsluta program och avslutaspara:");
            int.TryParse(Console.ReadLine(), out int menyval);


            switch (menyval)
            {
                case 1:
                    SkrivUtMeddelande();
                    Console.WriteLine("Tryck på valfri knapp för att gå vidare!");
                    Console.ReadKey();
                    InloggadMeny();
                    break;

                case 2:
                    Console.WriteLine("Meddelandet är raderat!");
                    break;

                case 3:
                    AvslutaProgram();
                    break;

                default:
                    Console.WriteLine("Ogiltigt val, Försök igen.");
                    Console.ReadKey();
                    InkorgMeny();
                    break;
            }
        }

        static void MeddelandeMeny()
        {
            Console.Clear();
            Console.WriteLine("Välj alternativ");
            Console.WriteLine("\t1 : Skriv meddelande:");
            Console.WriteLine("\t2 : Tillbaka till inkorg");
            Console.WriteLine("\t3 : Avsluta program och avslutaspara:");

            int.TryParse(Console.ReadLine(), out int menyval);

            switch (menyval)
            {
                case 1:
                    SkrivUtMeddelande();
                    break;
                case 2:
                    InloggadMeny();
                    break;
                case 3:
                    AvslutaProgram();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, Försök igen.");
                    Console.ReadKey();
                    MeddelandeMeny();
                    break;
            }
        }

        // ****************************** HÄR SLUTAR SEKTIONEN FÖR MENY FILER  ******************************
        //****************************************************************************************************


        // ********************************** DETTA ÄR HÄMTA FILER *******************************************
        //****************************************************************************************************

        static StreamReader LaddaAnvändarFil()
        {
            StreamReader infil = new StreamReader("user.txt", Encoding.GetEncoding(28591));

            return infil;
        }

        static StreamReader LaddaMeddelandeFil()
        {
            StreamReader infil = new StreamReader("meddelande.txt", Encoding.GetEncoding(28591));

            return infil;
        }

        static Konto[] HämtaGamlaAnvändarListan()
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

        static Meddelande[] HämtaGamlaMeddelandeListan()
        {
            if (!File.Exists("meddelande.txt"))
            {
                return new Meddelande[0]; // Returnera en tom lista om filen inte finns
            }

            StreamReader infil = LaddaMeddelandeFil();

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

        // *************************************** HÄR SLUTAR SEKTIONEN FÖR HÄMTA FILER  *********************
        //****************************************************************************************************


        // *************************************** TILLHÖR SKAPA ANVÄNDARE ***********************************
        //****************************************************************************************************

        static void SparaAnvändareUppgifter()
        {
            Konto nyttkonto = SkapaAnvändare();
            Konto[] gamlaKontoLista = HämtaGamlaAnvändarListan();
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

        static Konto SkapaAnvändare()
        {
            Konto nyttKonto = new Konto();

            Console.Clear();
            Console.WriteLine("Välj ett användarnamn och ett lösenord");
            string rubrikAnvändarnamn = "Användarnamn: ";
            string användarnamn = CheckaTomtfält(rubrikAnvändarnamn);
            string rubrikLösenord = "Lösenord: ";
            string lösenord = Skrivlösenord(rubrikLösenord);

            nyttKonto.användarnamn = användarnamn;
            nyttKonto.lösenord = lösenord;

            Console.WriteLine("Har redan lagt till nyttkonto...");
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
       
        

        // *************************************** HÄR SLUTAR SEKTIONEN SKAPA ANVÄNDARE  *********************
        //****************************************************************************************************


        // *************************************   HÄR BÖRJAR SEKTIONEN LOGGA IN   ***************************
        //****************************************************************************************************

        static void LoggaIn()
        {
            Console.Clear();
            Console.WriteLine("+-+-+-+-+-");
            Console.WriteLine("Logga in: ");
            Console.WriteLine("+-+-+-+-+-");

            string rubrikAnvändarnamn = "Användarnamn: ";
            Inloggingsanvändarnamn = CheckaTomtfält(rubrikAnvändarnamn);
            string rubrikLösenord = "Lösenord: ";
            string lösenord = Skrivlösenord(rubrikLösenord);

            MatchaAnvändarNamn(Inloggingsanvändarnamn, lösenord);
        }

        static void MatchaAnvändarNamn(string användarnamn, string lösenord)
        {
            string[] rader = File.ReadAllLines("user.txt");

            Konto[] lista = new Konto[rader.Length];

            for (int i = 0; i < rader.Length; i++)
            {
                string[] värde = rader[i].Split('\t');

                Konto konto = new Konto();
                konto.användarnamn = värde[0];
                konto.lösenord = värde[1];

                lista[i] = konto;
            }

            bool ok = KollaAnvändare(lista, användarnamn, lösenord);

            if (ok == true)
            {
                InloggadMeny();
            }
            else if (ok == false)
            {
                Console.Clear();
                Console.WriteLine("Användarnamn finns inte!");
                Console.WriteLine("Tryck på valfri knapp");
                Console.ReadKey();
                FörstaMenyVal();
            }
        }

        static bool KollaAnvändare(Konto[] lista, string användarnamn, string lösenord)
        {
            bool x = false;

            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].användarnamn == användarnamn && lista[i].lösenord == lösenord)
                {
                    x = true;
                    break;
                }
            }
            return x;
        }

        // ************************************* HÄR SLUTAR SEKTIONEN FÖR LOGGA IN   *************************
        //****************************************************************************************************


        // *********************************** HÄR BÖRJAR SEKTIONEN FÖR MEDDELANDE   *************************
        //****************************************************************************************************

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

                DateTime datum = DateTime.ParseExact(
                    värde[3],
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture
                );
                string datumUtanSekunder = datum.ToString("yyyy-MM-dd HH:mm");

                if (Inloggingsanvändarnamn == mottagare)
                {
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Avsändare: {0}", avsändare);
                    Console.WriteLine("Rubrik: {0}", rubrik);
                    Console.WriteLine("Datum: {0}", datumUtanSekunder);
                }
            }
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-");
        }

        static void SparaMeddelande()
        {
            Meddelande nyttMeddelande = SkapaMeddelande();
            Meddelande[] gamlaMeddelandeLista = HämtaGamlaMeddelandeListan();
            Meddelande[] nyameddelanden = LäggMeddelandeVektor(
                gamlaMeddelandeLista,
                nyttMeddelande
            );

            StreamWriter utfil = new StreamWriter("meddelande.txt", true); // skapa fil eller öppna om den finns

            foreach (Meddelande meddelande in nyameddelanden)
            {
                utfil.WriteLine(
                    meddelande.användarnamn
                        + "\t"
                        + meddelande.mottagare
                        + "\t"
                        + meddelande.rubrik
                        + "\t"
                        + meddelande.datum
                        + "\t"
                        + meddelande.text
                );
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
            SkrivUtSorteradeAnvändare();
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-");

            Console.WriteLine("Från: {0} ", Inloggingsanvändarnamn);

            string rubrikTill = "Till: ";
            string mottagare = CheckaTomtfält(rubrikTill);

            Console.Write("Rubrik: ");
            string rubrik = Console.ReadLine();

            DateTime datum = DateTime.Now;

            string rubrikMeddelande = "Meddelande: ";
            string text = CheckaTomtfält(rubrikMeddelande);

            nyttMeddelande.användarnamn = Inloggingsanvändarnamn;
            nyttMeddelande.mottagare = mottagare;
            nyttMeddelande.rubrik = rubrik;
            nyttMeddelande.datum = datum;
            nyttMeddelande.text = text;

            return nyttMeddelande;
        }

        public static Meddelande[] LäggMeddelandeVektor(
            Meddelande[] gamlaMeddelandeLista,
            Meddelande nyttMeddelande
        )
        {
            Meddelande[] nyameddelanden = new Meddelande[gamlaMeddelandeLista.Length + 1];

            for (int i = 0; i < gamlaMeddelandeLista.Length; i++)
            {
                nyameddelanden[i] = gamlaMeddelandeLista[i];
            }

            nyameddelanden[gamlaMeddelandeLista.Length] = nyttMeddelande;

            return nyameddelanden;
        }

        // ************************************* HÄR SLUTAR SEKTIONEN FÖR MEDDELANDEN   *************************
        //****************************************************************************************************


        // ************************************* HÄR BÖRJAR SEKTIONEN FÖR ANVÄNDARE   *************************
        //****************************************************************************************************

        static void TaBortAnvändare()
        {
            Console.WriteLine("Ta bort användare");

            int index = SökIndexPåAnvändare(Inloggingsanvändarnamn);
            Konto[] nylista = TaBortAnvändareFrånLista(index);

            StreamWriter utfil = new StreamWriter("user.txt"); // skapa fil eller öppna om den finns

            foreach (Konto konto in nylista)
            {
                utfil.WriteLine(konto.användarnamn + "\t" + konto.lösenord);
            }

            utfil.Close(); // Stänger fil
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

        static void SkrivUtSorteradeAnvändare()
        {
            string[] Användare = LäggAnvändareIVektor();

            SorteraAvändare(Användare);
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-");
            Console.WriteLine("Olika användare");
            for (int i = 0; i < Användare.Length; i++)
            {
                Console.WriteLine(Användare[i]);
            }
        }

        static void SorteraAvändare(string[] osorterat)
        {
            bool osorterad = true;

            int end = osorterat.Length - 1;

            while (osorterad)
            {
                osorterad = false;
                for (int i = 0; i < end; i++)
                {
                    int resultat = osorterat[i].CompareTo(osorterat[i + 1]);

                    if (resultat > 0)
                    {
                        BytPlatsPåAnvändare(osorterat, i, i + 1);
                        osorterad = true;
                    }
                }
                end--;
            }
        }

        public static void BytPlatsPåAnvändare(string[] osorterat, int a, int b)
        {
            string tmp = osorterat[a];
            osorterat[a] = osorterat[b];
            osorterat[b] = tmp;
        }

        static string[] LäggAnvändareIVektor()
        {
            Console.Clear();
            string[] rader = File.ReadAllLines("user.txt");
            string[] konton = new string[rader.Length];

            for (int i = 0; i < rader.Length; i++)
            {
                string[] värde = rader[i].Split('\t');

                konton[i] = värde[0];
            }
            return konton;
        }

        // ************************************* HÄR SLUTAR SEKTIONEN FÖR ANVÄNDARE   *************************
        //****************************************************************************************************


        // ************************************* HÄR BÖRJAR SEKTIONEN FÖR FELHANTERING   *********************
        //****************************************************************************************************

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
                                Console.Clear();
                                Console.WriteLine("Fältet kan ej vara tomt, försök igen!");
                                Console.WriteLine("Användarnamn: {0}", Inloggingsanvändarnamn);
                                string andraText = Skrivlösenord(textpåskräm);
                                return andraText;
                                //break;
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

        static string CheckaTomtfält(string textpåskräm)
        {
            string text = "";
            try
            {
                Console.Write(textpåskräm);

                do
                {
                    text = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Console.Clear();
                        Console.WriteLine("Fältet kan ej vara tomt, försök igen!");

                        string andratext = CheckaTomtfält(textpåskräm);
                        return andratext;
                        //break;
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                return text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ************************************* HÄR SLUTAR SEKTIONEN FÖR FELHANTERING   *********************
        //****************************************************************************************************


        // ****************************** HÄR BÖRJAR SEKTIONEN FÖR AVSLUTA PROGRAM   *************************
        //****************************************************************************************************

        static void AvslutaProgram()
        {
            Console.Clear();
            Console.WriteLine("Är du säker på att du vill avsluta och spara programmet?");
            Console.WriteLine("J för avsluta och N för att komma till huvudmenyn.");
            string inmatning = Console.ReadLine().ToUpper();

            if (inmatning == "J")
            {
                Environment.Exit(0);
            }
            else if (inmatning == "N")
            {
                FörstaMenyVal();
            }
            else
            {
                Console.WriteLine("Felaktigt Svar! Försök igen");
                AvslutaProgram();
            }
        }

        // ********************************* HÄR SLUTAR SEKTIONEN FÖR AVSLUTA PROGRAM   ***********************
        //****************************************************************************************************


        // ************************************'''  HÄR BÖRJAR EXTRA FUNKTIONER   ****************************
        //****************************************************************************************************

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


     Slå på valfri knapp för att fortsätta"
            );

            Console.ReadKey();
        }

        // ********************************* HÄR SLUTAR SEKTIONEN FÖR EXTRA FUNKTIONER   ***********************
        //****************************************************************************************************
    }
}
