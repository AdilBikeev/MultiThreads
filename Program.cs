using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MultiThreads.Helpers;
using System.Linq;
using MultiThreads.Models;
using MultiThreads.Interfaces;
using MultiThreads.Service;

namespace MultiThreads
{
    class Program
    {
        public static string GetTime() => DateTime.Now.ToLongTimeString();
        public static CancellationTokenSource tokenSource = new CancellationTokenSource();

        /// <summary>
        /// Полный путь к папке с исходными документами.
        /// </summary>
        public static string fullPathFolderDoc { get; private set; } = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
        
        /// <summary>
        /// Полный путь к папкке с результатами.
        /// </summary>
        public static string fullPathFolderSaves { get; private set; } = Path.Combine(Directory.GetCurrentDirectory(), "Processed_Documents");

        static void Main(string[] args)
        {
            try
            {
                // Добавить парсинг с args переменных
                using (var schedulerDoc = new SchedulerDoc(new DocumentService(fullPathFolderDoc, fullPathFolderSaves)))
                {

                    if (args.Length == 2)
                    {
                        fullPathFolderDoc = args[0];
                        fullPathFolderSaves = args[1];
                    }
                    Console.WriteLine(fullPathFolderDoc);
                    Console.WriteLine(fullPathFolderSaves);
                    Console.WriteLine($"[{GetTime()}] Start");

                    // Запускаем планировщик каждые 2 сек.
                    var task = TaskHelper.StartScheduler(() => schedulerDoc.FindNewDoc(fullPathFolderDoc), 2000, tokenSource);

                    Console.WriteLine("Для завершения работы нажмите ESC");
                    while (Console.ReadKey().Key != ConsoleKey.Escape) { }

                    tokenSource.Cancel();
                    tokenSource.Token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"[{GetTime()}] Ожидание завершения обработки файлов");
            }
            finally
            {
                Console.WriteLine($"{GetTime()} End");
                tokenSource.Dispose();
            }
        }
    }
}
