using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konsultacje
{
    public interface IPrintable
    {
        string GetBytesToPrint();
    }

    public interface IPrinter
    {
        void Print(IPrintable printable);
    }

    public interface IPdfConvertible
    {
        Byte[] ConvertToPdf();
    }

    public interface IPdfPrinter
    {
        void Print(Byte[] bytes);
    }

    public class HP : IPrinter
    {
        public void Print(IPrintable printable)
        {
            Console.WriteLine(printable.GetBytesToPrint());
        }
    }

    public class CV : IPrintable, IPdfConvertible
    {
        private string text;

        public CV(string text)
        {
            this.text = text;
        }

        public byte[] ConvertToPdf()
        {
            return Encoding.ASCII.GetBytes(text);
        }

        public string GetBytesToPrint()
        {
            return text;
        }
    }

    public class ComboPrinter : IPdfPrinter, IPrinter
    {
        public void Print(byte[] bytes)
        {
            Console.WriteLine($"generuję pdf... {bytes}");
        }

        public void Print(IPrintable printable)
        {
            Console.WriteLine($"drukuję... {printable.GetBytesToPrint()}");
        }
    }

    public class Test
    {
        public Test()
        {
            CV dok = new CV("Nazywam się...");
            HP druk = new HP();
            druk.Print(dok);

            ComboPrinter combo = new ComboPrinter();
            combo.Print(dok);
            combo.Print(dok.ConvertToPdf());
        }
    }
}
