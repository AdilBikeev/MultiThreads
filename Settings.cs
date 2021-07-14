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

namespace MultiThreads
{
    /// <summary>
    /// Содержит настройки проекта.
    /// </summary>
    internal static class Settings
    {
        /// Паттерн для расшерения искомых файлов
        public const string searchPattern = "*.txt";

        /// Максимальное кол-во потоков
        public const int maxThreads = 4;
    }
}
