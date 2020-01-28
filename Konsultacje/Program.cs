using System;
using Konsultacje.StaticBuilder;
using Konsultacje.FluentBuilder;

namespace Konsultacje
{
    public enum Ocena { nk, niedostateczny, dostateczny, dostateczny_plus, dobry, dobry_plus, bardzo_dobry };

    class Program
    {
        static void Main(string[] args)
        {
            Student wojtus = new Student(125364);
            Wykladowca belfer = new Wykladowca("Belfer 2020");
            belfer.WpiszOceneDoUsosa(wojtus, Ocena.nk);
            belfer.WpiszOceneDoUsosa(wojtus, Ocena.dobry);
            //drugi przypadek
            try
            {
                wojtus.IdzNaEgzaminZerowy(Ocena.dobry);
            }
            catch(NieZostalesDopuszczonyException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                //
            }
            finally
            {
                Console.WriteLine("Idę się jeszcze pouczyć...");
            }
            Console.ReadKey();



            // builder
            Kierownik kiero = new Kierownik();
            Produkt p1 = kiero.BuildProdukt(new BaseLaptopBuilder());
            Console.WriteLine(p1);
            Console.ReadKey();

            // static builder v1 - niezbyt eleganckie rozwiązanie
            StaticBuilder.IBuilder laptop_basic_builder = new StaticBuilder.BaseLaptopBuilder();
            StaticBuilder.Produkt laptop_basic = laptop_basic_builder.Build();
            Console.WriteLine(laptop_basic);
            laptop_basic = laptop_basic_builder.AddProcessor("Intel Core i5 2,6 Ghz");
            Console.WriteLine(laptop_basic);
            Console.ReadKey();

            // fluent builder v2

            FluentBuilder.Laptop lapek = new FluentBuilder.LaptopBuilder().Build();
            Console.WriteLine($"Lapek null ?: {lapek is null}");
            Console.ReadKey();
            FluentBuilder.Laptop lapek2 = new FluentBuilder.LaptopBuilder().AddMatryca("15,6'' matowa").AddProcessor("Intel Core i3").Build();
            Console.WriteLine($"Lapek2 null ?: {lapek2 is null}");
            Console.WriteLine(lapek2);
            Console.ReadKey();

            // drukowanie
            Test t = new Test();
            Console.ReadKey();


        }
    }

    class StudentFactory
    {
        public Student GetStudent(string student)
        {
            if(student == "kozak")
            {
                return new Student(9999999);
            }
            else if (student == "takisobie")
            {
                return new Student(555555);
            }
            return null;
        }
    }

    class StudentNieZaliczylException : Exception
    {
        public StudentNieZaliczylException(string message) : base(message)
        {
        }
    }

    class Wykladowca
    {
        public Wykladowca(string imieNazwisko)
        {
            ImieNazwisko = imieNazwisko;
        }

        public string ImieNazwisko { get; }

        public void WpiszOceneDoUsosa(Student s, Ocena o)
        {
            try
            {
                if (o == Ocena.nk)
                {
                    throw new StudentNieZaliczylException($"Exception!: Student {s.Indeks} nie został sklasyfikowany");
                }
                else
                {
                    Console.WriteLine($"gratulacje {s.Indeks}");
                }
            }
            catch (StudentNieZaliczylException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // zamknij uchwyty do pliku, bazy, przywróć transakcję, cofnij zmiany itp.
                // to co wymagane aby wystąpienie wyjątku nie zakłóciło spójności danych, pracy programu
            }

        }

    }

    class Student
    {
        public Student(int indeks)
        {
            this.Indeks = indeks;
        }
        public int Indeks { get; }

        public void IdzNaEgzaminZerowy(Ocena o)
        {
            if (o != Ocena.bardzo_dobry || o != Ocena.dobry_plus)
            {
                throw new NieZostalesDopuszczonyException("Za niska ocena z ćwiczeń!");
            }
            else{
                Console.WriteLine("Zapraszam!");
            }
        }
    }

    public class NieZostalesDopuszczonyException : Exception
    {
        public NieZostalesDopuszczonyException(string message) : base(message)
        {
        }
    }
}
