using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konsultacje
{
    public interface IBuilder
    {
        void AddHdd();
        void AddRam();
        void AddProcessor();
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

    public class BaseLaptopBuilder : IBuilder
    {
        private Laptop laptop = new Laptop(1, "Bazowy laptop");
        public void AddHdd()
        {
            laptop.HDD = "1 TB HDD 5200 Rpm";
        }

        public void AddProcessor()
        {
            laptop.Processor = "Intel Core i3 1,8 GHz";
        }

        public void AddRam()
        {
            laptop.RAM = "8 GB DDR4 2600 MHz";
        }

        public Produkt Build()
        {
            return laptop;
        }
    }

    public class Kierownik
    {
        readonly IBuilder Builder;

        public Produkt BuildProdukt(IBuilder builder)
        {
            builder.AddHdd();
            builder.AddProcessor();
            builder.AddRam();

            return builder.Build();
        }
    }
}
