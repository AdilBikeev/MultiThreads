using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreads.Helpers
{
    public static class TaskHelper
    {
        /// <summary>
        /// Запускает планировщик, выполняющий метод func каждые ms - миллисекунд.
        /// </summary>
        /// <param name="func">Функция для выполняния по расписанию.</param>
        /// <param name="ms">Частота выполнения метода func в миллисекундах.</param>
        public static Task StartScheduler(Action func, int ms, CancellationTokenSource tokenSource)
        {
            var targetMethod = new Action(() =>
            {
                while (true)
                {
                    func();
                    Thread.Sleep(ms); // возможно нужно переписать на Task.Delay
                }
            });

            return Task.Factory.StartNew(targetMethod, tokenSource.Token);
        }
    }
}
