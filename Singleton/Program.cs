using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var singleton = Singleton.GetInstance("Привет, мир!");
            var singleton2 = Singleton.GetInstance("Здравствуй, космос!");
            Console.WriteLine(singleton2.Data);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Класс, реализующий паттерн Одиночка.
    /// </summary>
    /// <remarks>
    /// Порождающий паттерн, гарантирующий, что для класса будет создан только один единственный экземпляр.
    /// </remarks>
    public class Singleton
    {
        /// <summary>
        /// Объект синхронизации, необходим для безопасности при многопоточном использовании.
        /// </summary>
        private static object _sync = new object();

        /// <summary>
        /// Основной объект, в котором будет храниться уникальный экземпляр класса. 
        /// </summary>
        private static Singleton _instance;

        /// <summary>
        /// Какие-либо хранимые данные.
        /// </summary>
        private string _data;

        /// <summary>
        /// Данные, используемые в классе.
        /// </summary>
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                lock(_sync) // Используется чтобы избежать одновременного доступа критической секции из разных потоков.
                {
                    _data = value;
                }
            }
        }

        /// <summary>
        /// Защищенный конструктор для инициализации единственного экземпляра класса.
        /// </summary>
        /// <param name="data">Данные, используемые в классе.</param>
        private Singleton(string data)
        {
            Data = data;
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="data">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static Singleton GetInstance(string data)
        {
            lock(_sync) // Используется чтобы избежать одновременного доступа критической секции из разных потоков.
            {
                // Если экземпляр еще не инициализирован - выполняем инициализацию. 
                // Иначе возвращаем имеющийся экземпляр.
                if (_instance == null)
                {
                    _instance = new Singleton(data);
                }
            }
            return _instance;
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns>Данные класса в строковом формате.</returns>
        public override string ToString()
        {
            return Data;
        }
    }
}
