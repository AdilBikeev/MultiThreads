using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreads.Interfaces
{
    public interface ISchedulerDoc
    {
        /// <summary>
        /// Ищет в директории новые файлы.
        /// </summary>
        void FindNewDoc(string fullPathFolder);

        /// <summary>
        /// Производит обработку соответствующего документа.
        /// </summary>
        Task ProcessingDoc(string fileName);
    }
}
