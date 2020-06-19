using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dan_XXXV_Bojana_Backo
{
    class Program
    {
        static Random random = new Random();
        static Object loopLock = new object();
        static uint numberOfThreads;
        static uint numberToGuess;
        static List<Thread> threads = new List<Thread>();
        static bool bingo = false;
        static void Main(string[] args)
        {
            // First thread created at beginning of the application
            Thread thread1 = new Thread(() => Begin());
            thread1.Start();
            thread1.Join();
            // Thread_Generator that creates a list of another threads
            Thread Thread_Genetaror = new Thread(() => CreateParticipiants());
            Thread_Genetaror.Name = "Thread_Generator";
            
            Thread_Genetaror.Start();
            Thread_Genetaror.Join();
            // Start the threads from the list 
            foreach (var i in threads)
            {
                i.Start();
            }
            
            Console.ReadKey();
        }

        // Function that creates participants and save them in List<Thread>
        static List<Thread> Participiants(uint number)
        {
            for (int i = 0; i < number; i++)
            {
                Thread t = new Thread(() => StartGuessing());
                threads.Add(t);
                threads[i].Name = string.Format("Participiant_{0}", threads[i].ManagedThreadId);
                //Console.WriteLine(threads[i].Name + threads[i].ManagedThreadId + threads[i].IsAlive);
            }
            return threads;
        }
        // Function called by Thread_Generator
        static void CreateParticipiants()
        {
            threads = Participiants(numberOfThreads);
        }
        // Function for guessing the input number
        static bool GuessTheNumber()
        {
            Thread.Sleep(100);
            int rnd = random.Next(1, 101);
            if (numberToGuess == rnd && !bingo)
            {
                Console.WriteLine("\n******Winner is " + Thread.CurrentThread.Name + " guessing number is {0}******", rnd);
                
                return true;
            }
            else //(numberToGuess != rnd)
            {
                Console.WriteLine("\n" + Thread.CurrentThread.Name + " guesses the number " + rnd);
                if (((int)numberToGuess - rnd)%2 == 0)
                {
                    Console.WriteLine("Numbers are the same type (both odd or both even)\n");
                }

                return false;
            }
        }
        // Function called by Participiant threads
        static void StartGuessing()
        {
            do
            {
                // Lock the critical section of code
                // When one Participient guesses the number, game is over
                lock (loopLock)
                {
                    if (bingo)
                    {
                        break;
                    }
                    bingo = GuessTheNumber();
                }
            } while (!bingo);
        }
        // Function called by thread1 on begining of app
        static void Begin()
        {
            numberOfThreads = (uint)Console_Messages.NumberOfParticipients();
            numberToGuess = (uint)Console_Messages.NumberToGuess();
            Console.WriteLine("\nThe user has entered the number of participants!");
            Console.WriteLine("There are {0} participants. Guessing number is selected.\n", numberOfThreads);
        }
    }
}
