namespace ServerCore
{
    internal class Program
    {
        static void MainThread(object state)
        {
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine("HelloThread");
            }   
            
        }
        static void Main(string[] args)
        {

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);

            for(int i = 0; i < 5; i++)
            {
                Task t = new Task(  () => { while (true) { } },  TaskCreationOptions.LongRunning);
                t.Start();
            }
            ThreadPool.QueueUserWorkItem(MainThread);

            while(true)
            {

            }
        }
    }
}