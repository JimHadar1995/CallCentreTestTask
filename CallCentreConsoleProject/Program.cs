using CallCentreConsoleProject.Internal;
using System;

namespace CallCentreConsoleProject
{
    class Program
    {
        const string DefaultTestFile = "test_data_onemonth.csv";

        static void Main(string[] args)
        {
            string testFile = args.Length > 0 ? args[0] : DefaultTestFile;

            var worker = new CallCentreWorker(testFile);

            worker.PrintPart1();

            //worker.PrintPart2();

            Console.ReadKey();
        }
    }
}
