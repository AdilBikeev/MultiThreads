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


namespace MultiThreads.Models
{
    /// <summary>
    /// Класс для обработки документов.
    /// </summary>
    public class DocumentService : IFileOperation
    {
        /// <summary>
        /// Дириктория с документами.
        /// </summary>
        private string RootPath { get; init; }

        /// <summary>
        /// Дирикория для сохранения результата обработки файлов.
        /// </summary>
        private string SavePath { get; init; }

        /// <summary>
        /// Вернет true, если очередь на орбаботку документов пуста.
        /// </summary>
        public bool IsQueueEmpty() => processDocs.Count == 0;

        /// <summary>
        /// Очередь документов для обработки.
        /// </summary>
        public static Queue<string> processDocs = new Queue<string>();

        public DocumentService(string rootPath = "", string savePath = "")
        {
            this.RootPath = string.IsNullOrEmpty(rootPath) ? Path.Combine(Directory.GetCurrentDirectory(), "Documents") : rootPath;
            this.SavePath = string.IsNullOrEmpty(rootPath) ? Path.Combine(Directory.GetCurrentDirectory(), "Processed_Documents") : savePath;
        }

        public int CountLetters(string fileName)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), this.RootPath, fileName);
            var count = 0;

            using (StreamReader sr = new StreamReader(fullPath))
            {
                var text = sr.ReadToEnd();

                Parallel.For(0, text.Length, (i, state) =>
                {
                    // Если входной символ - буква
                    if (char.IsLetter(text[i]))
                        count++;
                });

                return count;
            }
        }

        public void SaveResult(int count, string fileName)
        {
            if (!Directory.Exists(this.SavePath))
                Directory.CreateDirectory(this.SavePath);

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), this.SavePath, fileName);

            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                sw.Write(count.ToString());
            }
        }

        public async Task SaveResultAsync(int count, string fileName)
            => await Task.Factory.StartNew(() =>
            {
                processDocs.Enqueue(fileName);
                SaveResult(count, fileName);
                if (!this.IsQueueEmpty())
                {
                    processDocs.Dequeue();
                }
            });

        public async Task<int> CountLettersAsync(string fileName)
            => await Task.Factory.StartNew(() => CountLetters(fileName));

        public async Task Realese()
        {
            while (processDocs.Count != 0)
            {
                var fileName = processDocs.Dequeue();
                var count = await this.CountLettersAsync(fileName);
                this.SaveResultAsync(count, fileName);
            }
        }
    }
}
