namespace ServerCore
{
    internal class Program
    {
        class SpinLock
        {
            volatile int _locked = 0;

            public void Aquired()
            {
                while(true)
                {

                    int Expected = 0;
                    int Desired = 1;
                    int original = Interlocked.CompareExchange(ref _locked, Desired, Expected);
                    if(original == Expected) { break; }

                }

            }
            public void Release()
            {
                _locked = 0;
            }
        }
        static int _num = 0;
        static SpinLock _spinLock = new SpinLock();
        static void Thread1()
        {
            for (int i = 0; i < 100000; ++i)
            {
                _spinLock.Aquired();
                _num++;
                _spinLock.Release();
            }
        }
        static void Thread2()
        {
            for (int i = 0; i < 100000; ++i)
            {
                _spinLock.Aquired();
                _num--;
                _spinLock.Release();
            }
        }
        static void Main(string[] args)
        {
                Task t1 = new Task(Thread1);
                Task t2 = new Task(Thread2);

                t1.Start();
                t2.Start();
                Task.WaitAll(t1, t2);

                Console.WriteLine($"값은 : {_num}");
        }
    }
}