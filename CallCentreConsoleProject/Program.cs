using CallCentreConsoleProject.Internal;
using System;

namespace CallCentreConsoleProject
{
    class Program
    {
        const string DefaultTestFile = "test_data_onemonth.csv";

        static void Main(string[] args)
        {
            try
            {
                string testFile = args.Length > 0 ? args[0] : DefaultTestFile;

                var worker = new CallCentreWorker(testFile);

                worker.PrintPart1();

                worker.PrintPart2();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"При работе произошла ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
