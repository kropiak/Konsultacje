using System;

namespace Konsultacje.FluentBuilder
{
    public interface IKompBuilder<T, P>
    {
        T AddHdd(string hdd);
        T AddRam(string ram);
        T AddProcessor(string processor);
        P Build();
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

            
        public class Laptop : Produkt
        {
            public Laptop(long iD, string description) : base(iD, description)
            {
            }
        }

        public override string ToString()
        {
            return $"ID: {ID}\n Opis: {Description}\n Parametry: {HDD}, {RAM}, {Processor}";
        }
    }

    public class LaptopBuilder : IKompBuilder<LaptopBuilder, Laptop>
    {
        private Laptop laptop = new Laptop(1, "Basic laptop");

        public LaptopBuilder AddHdd(string hdd)
        {
            laptop.HDD = hdd;
            return this;
        }

        public LaptopBuilder AddProcessor(string processor)
        {
            laptop.Processor = processor;
            return this;
        }

        public LaptopBuilder AddRam(string ram)
        {
            laptop.RAM = ram;
            return this;
        }

        public LaptopBuilder AddMatryca(string matryca)
        {
            laptop.Matryca = matryca;
            return this;
        }

        public Laptop Build()
        {
            // tutaj można umieścić logikę, która będzie sprawdzała czy minimalne wymagania co do składowych produktu są spełnione
            // w tym przypadku inicjalizacja minimalnego produktu jest zapewniona przez kontruktor domyślny, ale w przypadku
            // klasycznego wzorca statyczny budowniczy, gdzie klasa budowniczego jest statyczna, konstruktor nie jest wywoływany
            // więc tutaj na pewno jakaś logika musi się znaleźć
            try
            {
                if (laptop.Processor is null && laptop.Matryca is null)
                {
                    throw new ProduktIncompleteException("Laptop musi posiadać procesor i matrycę.");
                }
            }
            catch(ProduktIncompleteException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
            return laptop;
        }
    }

    public class Laptop : Produkt
    {
        public string Matryca;

        public Laptop(long iD, string description) : base(iD, description)
        {
        }

        public override string ToString()
        {
            // jak Dekorator :)
            return base.ToString() + $", {Matryca}";
        }
    }

    public class ProduktIncompleteException : Exception
    {
        public ProduktIncompleteException(string message) : base(message)
        {
        }
    }
}
