using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konsultacje.StaticBuilder
{
    public interface IBuilder
    {
        Produkt AddHdd(string hdd);
        Produkt AddRam(string ram);
        Produkt AddProcessor(string processor);
        Produkt Build();
    }

    public abstract class Produkt
    {
        public long ID { get; }
        public string Description { get; set; }
        public string HDD;
        public string RAM;
        public string Processor;

        protected Produkt(long iD, string description)
        {
            ID = iD;
            Description = description;
        }

        public override string ToString()
        {
            return $"ID: {ID}\n Opis: {Description}\n Parametry: {HDD} {RAM} {Processor}";
        }
    }

    public class Laptop : Produkt
    {
        public Laptop(long iD, string description) : base(iD, description)
        {
        }
    }

    // nie jest to może dokładnie statyczny builder (wtedy korzystamy ze statycznej klasy), ale rozwiązanie bliskie
    // statycznego buildera
    public class BaseLaptopBuilder : IBuilder
    {
        private Laptop laptop = new Laptop(1, "Bazowy laptop");

        public BaseLaptopBuilder()
        {
            laptop.HDD = "1 TB HDD 5200 Rpm";
            laptop.Processor = "Intel Core i3 1,8 GHz";
            laptop.RAM = "8 GB DDR4 2600 MHz";
        }

        public Produkt AddHdd(string hdd)
        {
            laptop.HDD = hdd;
            return laptop;
        }

        public Produkt AddProcessor(string processor)
        {
            laptop.Processor = processor;
            return laptop;
        }

        public Produkt AddRam(string ram)
        {
            laptop.RAM = ram;
            return laptop;
        }

        public Produkt Build()
        {
            // tutaj można umieścić logikę, która będzie sprawdzała czy minimalne wymagania co do składowych produktu są spełnione
            // w tym przypadku inicjalizacja minimalnego produktu jest zapewniona przez kontruktor domyślny, ale w przypadku
            // klasycznego wzorca statyczny budowniczy, gdzie klasa budowniczego jest statyczna, konstruktor nie jest wywoływany
            // więc tutaj na pewno jakaś logika musi się znaleźć
            return laptop;
        }
    }
}
