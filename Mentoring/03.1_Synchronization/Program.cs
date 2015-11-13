using System.Threading;

namespace _03._1_Synchronization
{
    internal class Program
    {
        private static Semaphore _pool;

        private static readonly ILog logger = new FileLog();

        public static void Main()
        {
            _pool = new Semaphore(0, 3);

            for (var i = 1; i <= 5; i++)
            {
                var t = new Thread(Worker);
                t.Start(i);
            }

            Thread.Sleep(500);

            logger.Log("Main thread calls Release(3).");
            _pool.Release(3);

            logger.Log("Main thread exits.");
        }

        private static void Worker(object num)
        {
            logger.Log("Thread {0} begins " + "and waits for the semaphore.", num);
            _pool.WaitOne();

            logger.Log("Thread {0} enters the semaphore.", num);
            Thread.Sleep(1000);

            logger.Log("Thread {0} releases the semaphore.", num);
        }
    }
}