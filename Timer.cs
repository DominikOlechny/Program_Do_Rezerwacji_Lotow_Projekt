namespace Program_Do_Rezerwacji_Lotow
{
    public class Timer
    { /// <summary>
      /// Klasa Timer służy do stworzenia prostego odliczania czasu.
      /// </summary>
        public void timer()
        {
            /// <summary>
            /// Metoda timer realizuje prosty mechanizm odliczania,
            /// symulując postęp zadania przez wizualne przedstawienie procentowego ukończenia.
            /// </summary>
            var x = 0; // Początkowa wartość procentowa ukończenia zadania.
            for (int i = 10; i > 0; i--)
            {

                Console.Write(x + "%"); // Wyświetla aktualny postęp.
                x = x + 10;// Zwiększa wartość procentową o 10
                Thread.Sleep(250); // Opóźnienie 
                Console.Clear();// Czyści konsolę przed wyświetleniem kolejnego postępu.
            }
            Console.WriteLine(); // Wypisuje nową linię po zakończeniu odliczania.
        }
    }
}
