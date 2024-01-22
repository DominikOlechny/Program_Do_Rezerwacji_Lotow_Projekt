namespace Program_Do_Rezerwacji_Lotow
{
    public class Timer
    {
        public void timer()
        {
            var x = 0;
            for (int i = 10; i > 0; i--)
            {

                Console.Write(x + "%");
                x = x + 10;
                Thread.Sleep(250); // Opóźnienie 
                Console.Clear();
            }
            Console.WriteLine(); //enter
        }
    }
}
