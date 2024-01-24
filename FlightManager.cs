namespace Program_Do_Rezerwacji_Lotow

{
    public class FlightManager : Connectdb // Klasa FlightManager zarządza funkcjonalnościami związanymi z wyszukiwaniem lotów.
    {

        private Timer timer = new Timer();

        public void SearchFlights() // Metoda SearchFlights wyświetla menu pozwalające użytkownikowi na wyszukiwanie lotów
                                    // według różnych kryteriów oraz obsługuje interakcję z użytkownikiem.
        {
            bool isRunning = true;  // Zmienna kontrolująca działanie pętli menu.
            while (isRunning)
            {
                // Prezentacja opcji menu.
                Console.WriteLine("Wyszukiwanie lotów");
                Console.WriteLine("1. Po miejscu odlotu");
                Console.WriteLine("2. Po miejscu przylotu");
                Console.WriteLine("3. Po numerze rezerwacji");
                Console.WriteLine("4. Wyświetl wszystko");
                Console.WriteLine("5. Cofnij");
                Console.Write("Wprowadź swój wybór: ");

                string choice = Console.ReadLine(); // Pobranie wyboru użytkownika.

                switch (choice) // Obsługa wyboru użytkownika.
                {
                    case "1":
                        Console.WriteLine("Podaj miejsce odlotu:");
                        string odlot = Console.ReadLine();
                        timer.timer(); // Uruchomienie timera.
                        wyszukajpoodlotach(odlot); // Wyszukanie lotów po miejscu odlotu.
                        break;
                    case "2":
                        Console.WriteLine("Podaj miejsce przylotu:");
                        string cel = Console.ReadLine();
                        timer.timer();  // Uruchomienie timera.
                        wyszukajpodpcelowych(cel); // Wyszukanie lotów po miejscu przylotu.
                        break;
                    case "3":
                        Console.WriteLine("Podaj numer rezerwacji:");
                        string id = Console.ReadLine();
                        timer.timer(); // Uruchomienie timera.
                        wyszukajpoid(id); // numerze rezerwacji
                        break;
                    case "4":
                        timer.timer(); // Uruchomienie Timera
                        wypiszbaze(); //wyswietlenie calej bazy
                        break;
                    case "5":
                        isRunning = false;  // Zakończenie pętli i wyjście z metody.
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie."); //nieprawidlowy wybor wracamy do poczatku
                        break;
                }
            }
        }
    }
}