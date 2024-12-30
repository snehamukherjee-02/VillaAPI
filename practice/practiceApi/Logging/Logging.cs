namespace practiceApi.Logging
{
    public class Logging : ILogging
    {
        public void Log(string messege, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error - " + messege);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine(messege);
            }
        }
    }
}
