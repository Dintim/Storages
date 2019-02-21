using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public abstract class Storage
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public Storage()
        {
        }
        public Storage(string name, string model)
        {
            this.Name = name;
            this.Model = model;
        }

        public abstract double GetMemory();
        public abstract double CopyData(double x);
        public abstract double GetFreeMemory();
        public abstract void StorageInfo();
    }

    public class Flash:Storage
    {
        private double Speed = 5000;
        public double OccupiedMemory { get; set; } 
        public double Memory { get; set; }
        public Flash()
        {
        }
        public Flash(string name, string model, int memory):base(name, model)
        {            
            this.Memory = memory;
            this.OccupiedMemory = 0;
        }

        public override double GetMemory()
        {
            return Memory;
        }
        public override double CopyData(double x)
        {
            if (x <= Memory)
            {
                OccupiedMemory = x;
                return (x * 8) / Speed;
            }
            else
                return -1;
        }
        public override double GetFreeMemory()
        {
            return Memory - OccupiedMemory;
        }
        public override void StorageInfo()
        {
            Console.WriteLine("Название: {0}\n" +
                "Модель: {1}\n" +
                "Скорость: {2}\n" +
                "Объем памяти: {3}\n" +
                "Свободный объем памяти: {4}\n", this.Name, this.Model, this.Speed, this.Memory, Memory - OccupiedMemory);
        }
    }

    public enum DVDType { oneSide=4700, bothSide=9000 }
    public class DVD : Storage
    {
        private double ReadSpeed = 10.5;
        private double WriteSpeed = 10.5;
        public DVDType Type { get; set; }
        public double OccupiedMemory { get; set; }

        public DVD()
        {
        }
        public DVD(string name, string model, DVDType type):base(name, model)
        {            
            this.Type = type;
            this.OccupiedMemory = 0;
        }

        public override double CopyData(double x)
        {
            if (x <= (int)Type)
            {
                OccupiedMemory = x;
                return (x * 8) / WriteSpeed;
            }
            else
                return -1;
        }

        public override double GetFreeMemory()
        {
            return Convert.ToDouble((int)Type) - OccupiedMemory;
        }

        public override double GetMemory()
        {
            return Convert.ToDouble((int)Type);
        }

        public override void StorageInfo()
        {
            Console.WriteLine("Название: {0}\n" +
                "Модель: {1}\n" +
                "Скорость чтения: {2}\n" +
                "Скорость записи: {3}\n" +
                "Тип: {4}\n" +
                "Объем памяти: {5}\n" +
                "Свободный объем памяти: {6}\n", this.Name, this.Model, this.ReadSpeed, this.WriteSpeed, this.Type, (int)this.Type, 
                Convert.ToDouble((int)Type) - OccupiedMemory);
        }
    }

    public class HDD : Storage
    {
        private double Speed = 480;
        public int Sections { get; set; }
        public double SectionCapacity { get; set; }
        public double OccupiedMemory { get; set; }
        public HDD()
        {
        }
        public HDD(string name, string model, int sections, int secCap):base(name, model)
        {
            this.Sections = sections;
            this.SectionCapacity = secCap;
            this.OccupiedMemory = 0;
        }

        public override double CopyData(double x)
        {
            if (x <= SectionCapacity * Sections)
            {
                OccupiedMemory = x;
                return (x * 8) / Speed;
            }
            else
                return -1;
        }

        public override double GetFreeMemory()
        {
            return (Sections * SectionCapacity - OccupiedMemory);
        }

        public override double GetMemory()
        {
            return SectionCapacity * Sections;
        }

        public override void StorageInfo()
        {
            Console.WriteLine("Название: {0}\n" +
                "Модель: {1}\n" +
                "Скорость: {2}\n" +
                "Количество разделов: {3}\n" + 
                "Объем памяти: {4}\n" +
                "Свободный объем памяти: {5}\n", this.Name, this.Model, Speed, Sections, Sections*SectionCapacity, Sections*SectionCapacity-OccupiedMemory);
        }
    }
}
