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

namespace MultiThreads.Service
{
    public class SchedulerDoc : ISchedulerDoc, IDisposable
    {
        public IFileOperation service;

        public SchedulerDoc(IFileOperation fileOperation)
        {
            this.service = fileOperation;
        }

        /// <summary>
        /// Пути к новым документам, которые есть.
        /// </summary>
        static HashSet<string> _pathToDocs = new HashSet<string>();

        public void FindNewDoc(string fullPathFolder)
        {
            if (!Directory.Exists(fullPathFolder))
                Directory.CreateDirectory(fullPathFolder);

            var docs = Directory.GetFiles(fullPathFolder, Settings.searchPattern)
                     .Select(fullPaths => Path.GetFileName(fullPaths))
                     .Where(fileName => !_pathToDocs.Contains(fileName));

            Parallel.ForEach(
                docs,
                new ParallelOptions() { MaxDegreeOfParallelism = Settings.maxThreads },
                async (fileName) => await ProcessingDoc(fileName));
        }

        public async Task ProcessingDoc(string fileName)
        {
            Console.WriteLine($"[{Program.GetTime()}] Start Thread={Thread.CurrentThread.ManagedThreadId}");
            var count = await service.CountLettersAsync(fileName);
            await service.SaveResultAsync(count, fileName);
            Console.WriteLine($"[{Program.GetTime()}] End Thread={Thread.CurrentThread.ManagedThreadId}");
        }

        public void Dispose()
        {
            Task.WaitAll(service.Realese());
        }

        ~SchedulerDoc()
        {
            if (!service.IsQueueEmpty())
                Dispose();
        }
    }
}
