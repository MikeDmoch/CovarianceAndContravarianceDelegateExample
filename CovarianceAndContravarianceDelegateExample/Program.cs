using System;
using System.IO;
using System.Linq;
using static CovarianceAndContravarianceDelegateExample.CarFactory;

namespace CovarianceAndContravarianceDelegateExample
{
    class Program
    {
        delegate Car CarFactoryDel(int id, string name);
        delegate void LogICECarDetailsDel(ICECar car);  // ICE = spalinowy
        delegate void LogEVCarDetailsDel(EVCar car);    // EV = elektrtyczny
        static void Main(string[] args)
        {
            CarFactoryDel carFactoryDel = ReturnICECar;

            Car iceCar = carFactoryDel(1, "Audi Q7");

            carFactoryDel = ReturnEvCar;

            Car evCar = carFactoryDel(2, "Tesla Roadster");

            LogICECarDetailsDel logICECarDetailsDel = LogCarDetails;

            logICECarDetailsDel(iceCar as ICECar);

            LogEVCarDetailsDel logEVCarDetailsDel = LogCarDetails;

            logEVCarDetailsDel(evCar as EVCar);

            Console.ReadKey();

        }

        static void LogCarDetails(Car car)
        {
            if (car is ICECar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICEDetails.txt"), true))
                {
                    sw.WriteLine($"Objekt Typu: {car.GetType()}");
                    sw.WriteLine($"Szczegóły pojazdu: {car.GetCarDetails()}");
                };

            }
            else if (car is EVCar)
            {
                Console.WriteLine($"Objekt typu: {car.GetType()}");
                Console.WriteLine($"Szczegóły pojazdu: {car.GetCarDetails()}");
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }

    public abstract class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual string GetCarDetails()
        {
            return $"{Id} - {Name} ";
        }
    }
    public class ICECar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Spalinowy";
        }
    }
    public class EVCar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Elektryczny";
        }
    }

    public static class CarFactory
    {
        public static ICECar ReturnICECar(int id, string name)
        {
            return new ICECar { Id = id, Name = name };
        }
        public static EVCar ReturnEvCar(int id, string name)
        {
            return new EVCar { Id = id, Name = name };
        }
    }
    
}