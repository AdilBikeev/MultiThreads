using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreads.Interfaces
{
    public interface IFileOperation
    {
        /// <summary>
        /// Подсчитывает кол-во букв в документе/
        /// </summary>
        Task<int> CountLettersAsync(string fileName);

        /// <summary>
        /// Сохрняет инф. о кол-ве символов в документе в отдельную папку/
        /// </summary>
        Task SaveResultAsync(int count, string fileName);

        /// <summary>
        /// Подсчитывает кол-во букв в документе/
        /// </summary>
        int CountLetters(string fileName);

        /// <summary>
        /// Сохрняет инф. о кол-ве символов в документе в отдельную папку/
        /// </summary>
        void SaveResult(int count, string fileName);

        /// <summary>
        /// Вернет true, если очередь на орбаботку документов пуста.
        /// </summary>
        public bool IsQueueEmpty();

        /// <summary>
        /// Метод, завершающий работу со всеми документами из очереди.
        /// </summary>
        Task Realese();
    }
}
