using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
namespace ReadersWriters
{
    class ReadersWriters
    {
        private Mutex _temp;
        private Mutex[] _mutexes;
        private Thread[] _readers;
        private Thread[] _writters;
        private int _counterWriters;
        private int _counterReaders;

        private string[] _fileNames;
        private string[] _str;
        
        public ReadersWriters()
        {
            _temp = null;
            _mutexes = new Mutex[3];
            _readers = new Thread[3];
            _writters = new Thread[3];
            _counterWriters = 0;
            _counterReaders = 0;
            _fileNames = new string[3] { "archivo1.txt", "archivo2.txt", "archivo3.txt" };
            _str = new string[3] { RandomString(),RandomString(), RandomString() };
        }

        public void Proccess()
        {
            Console.WriteLine();
            _temp = new Mutex();
            for (var i = 0; i < 3; i++)
            {
                _mutexes[i] = new Mutex();
                _readers[i] = new Thread(ThreadWritter);
                _readers[i].Start();
                _writters[i] = new Thread(ThreadReader);
                _writters[i].Start();
            }
            Console.ReadKey();
        }

        private void ThreadWritter()
        {
            var index = 0;
            _temp.WaitOne();
            index = _counterWriters;
            _counterWriters++;
            _temp.ReleaseMutex();
            Console.WriteLine($"Se ejecuta el Writer {_counterWriters}");

            var counter = 0;

            while (true)
            {
                _mutexes[index].WaitOne();

                using (var writter = new StreamWriter(_fileNames[index]))
                {
                    _str[index] = RandomString();
                    writter.WriteLine(_str[index]);
                }

                _mutexes[index].ReleaseMutex();
                Thread.Sleep(1500);

                /*PARA HACERLO CON TPOS VARIABLES: */
                //Random tpovble = new Random();  
                //int _tpovble =tpoble.Next(1000,2000);
                //Thread.Sleep(_tpovble);
            }

            Console.WriteLine($"El Write {index} se esta ejecutando...");
        }

        private void ThreadReader()
        {
            var index = 0;
            _temp.WaitOne();
            index = _counterReaders;
            _counterReaders++;
            _temp.ReleaseMutex();

            var counter = 0;

            while (true)
            {
                _mutexes[index].WaitOne();

                var str = "";

                using (var reader = new StreamReader(_fileNames[index]))
                {
                    while (reader.EndOfStream == false)
                    {
                        str += reader.ReadLine();
                    }
                }
                Console.WriteLine($"Se escribe {str} por  {counter} vez");
                counter++;
                _mutexes[index].ReleaseMutex();
                Thread.Sleep(1500);
                /*PARA HACERLO CON TPOS VARIABLES: */
                //Random tpovble = new Random();  
                //int _tpovble =tpoble.Next(1000,2000);
                //Thread.Sleep(_tpovble);
            }
        }
    
       public static string RandomString (){
            StringBuilder str_build = new StringBuilder();  
            Random random = new Random();  
            char letter;  
            Random length = new Random();  
            int _length =length.Next(3,10);
            for (int i = 0; i < _length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);  
            }  
            return str_build.ToString();
        }
    } 
}
