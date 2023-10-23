namespace ServerCore
{
    internal class Program
    {
        static ThreadLocal<string> threadName = new ThreadLocal<string>(() => { return $"Thread Name Is {Thread.CurrentThread.ManagedThreadId}"; });

        static void whoAmI()
        {
            bool Result = threadName.IsValueCreated;
            if(Result)
            {

                Console.WriteLine($"{ threadName.Value} Repeated");
            }
            else
            {

                Console.WriteLine(threadName.Value);
            }
        }
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(3, 3);
            Parallel.Invoke(whoAmI, whoAmI, whoAmI, whoAmI, whoAmI);
        }
    }
}