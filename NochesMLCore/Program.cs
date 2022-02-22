using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NochesMLCore
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);

        static void Main(string[] args)
        {
            SetConsoleCP(949);
            SetConsoleOutputCP(949);
            Console.WriteLine("=====================================");
            Console.WriteLine("  ShareTech 말투 분석기");
            Console.WriteLine("=====================================\n");
            Console.WriteLine("추론하고 싶은 문장을 입력하세요.");
            Console.WriteLine("exit를 입력하면 종료합니다.\n");
            string input;
            GuessSender predict;
            while (true)
            {
                Console.Write(">> ");
                input = Console.ReadLine();

                if (input == "exit")
                    return;

                predict = new(input);

                Console.WriteLine("\n=====================================\n");
                Console.WriteLine($"{predict.SenderPrediction}님이 보냈을 확률이 높습니다.");
                foreach (var (sender, score) in predict.ScoreWithName)
                    Console.WriteLine($"{sender}: {score * 100:0.00}%");
                Console.WriteLine("\n=====================================\n");
            }
        }
    }
}