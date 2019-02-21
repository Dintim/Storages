using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public class Menu
    {
        public List<Storage> storages = new List<Storage>();

        public void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать и заполнить список носителей\n" +
                    "2. Список уже создан и заполнен -> далее\n" +
                    "3. Выход");
                int x = Int32.Parse(Console.ReadLine());
                if (x == 1)
                    AddStorage();
                else if (x == 2)
                    CopyMenu();
                else
                    break;
            }
            
        }

        public void CopyMenu()
        {            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Общий объем памяти на всех устройствах\n" +
                    "2. Копирование информации на устройства и расчет времени копирования\n" +
                    "3. Расчет необходимого количества носителей для копирования\n" +
                    "4. Выход");
                string x = Console.ReadLine();
                int tmp;
                if (Int32.TryParse(x, out tmp))
                {
                    if (tmp == 1)
                        CountFullMemory();
                    else if (tmp == 2)
                        CopyToStorage();
                    else if (tmp == 3)
                        CountStoragesForCopy();
                    else
                        break;
                }                
                Thread.Sleep(2000);                
            }

        }

        public void CountStoragesForCopy()
        {
            Console.Clear();
            Console.WriteLine("Объем информации, необходимой скопировать на носитель:");
            int data = Int32.Parse(Console.ReadLine());
            int sum = 0, st=1;
            for (int i = 0; i < storages.Count; i++)
            {
                if (storages[i] is Flash)
                    sum += Convert.ToInt32(((Flash)storages[i]).Memory);
                else if (storages[i] is DVD)
                    sum += (int)((DVD)storages[i]).Type;
                else if (storages[i] is HDD)
                    sum += Convert.ToInt32(((HDD)storages[i]).SectionCapacity * ((HDD)storages[i]).Sections);
                if (data <= sum)
                {
                    st += i;
                    break;
                }
            }
            if (sum < data)
                Console.WriteLine("Недостаточное количество устройств для копирования информации");
            else
                Console.WriteLine("Для копирования информации небходимо {0} носителей", st);
            Thread.Sleep(2000);
        }        

        public void CopyToStorage()
        {
            Console.Clear();
            Console.WriteLine("Объем информации, необходимой скопировать на носитель:");
            int data = Int32.Parse(Console.ReadLine());            
            for (int i = 0; i < storages.Count; i++)
            {                
                storages[i].CopyData(data);
                if (storages[i].CopyData(data) >= 0)
                    Console.WriteLine("Для копирования на носитель {0} потребовалось {1} секунд", storages[i].Name, Math.Round(storages[i].CopyData(data),2));
                else
                    Console.WriteLine("На носителе {0} недостаточно памяти, запись не удалась", storages[i].Name);
            }
            Thread.Sleep(2000);
        }
        
        public void CountFullMemory()
        {
            Console.Clear();
            Console.WriteLine("В списке {0} носителей", storages.Count);
            int sum = 0;
            for (int i = 0; i < storages.Count; i++)
            {
                if (storages[i] is Flash)
                    sum+=Convert.ToInt32(((Flash)storages[i]).Memory);                    
                else if (storages[i] is DVD)
                    sum += (int)((DVD)storages[i]).Type;
                else if (storages[i] is HDD)
                    sum += Convert.ToInt32(((HDD)storages[i]).SectionCapacity * ((HDD)storages[i]).Sections);
            }
            Console.WriteLine("Общее количество памяти всех устройств: {0} Мбайт", sum);
            Thread.Sleep(2000);
        }

        public void AddStorage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать носитель\n" +
                    "2. Выход");
                int x = Int32.Parse(Console.ReadLine());
                if (x == 1)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Выберите носитель:");
                        Console.WriteLine("1. Flash-память\n" +
                            "2. DVD-диск\n" +
                            "3. Съемный HDD\n" +
                            "4. Выход");
                        int y = Int32.Parse(Console.ReadLine());
                        if (y == 1)
                            AddFlash();
                        else if (y == 2)
                            AddDVD();
                        else if (y == 3)
                            AddHDD();
                        else
                            break;
                    }
                }
                else 
                    break;
            }
            Console.WriteLine("в списке {0} накопителя", storages.Count);
        }

        public void AddHDD()
        {
            Console.Clear();
            HDD h = null;
            Console.WriteLine("Вы создаете съемный HDD, скорость USB 2.0");
            //Console.WriteLine("Введите название:");
            //string name = Console.ReadLine();
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Введите количество разделов:");
            int sections = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите объем раздела - в Мбайтах:");
            int secCap = Int32.Parse(Console.ReadLine());
            h = new HDD("Flash", model, sections, secCap);
            storages.Add(h);
            Console.WriteLine("Съемный HDD создан и добавлен в список!");
            Thread.Sleep(2000);
        }

        public void AddDVD()
        {
            Console.Clear();
            DVD d = null;
            Console.WriteLine("Вы создаете DVD-диск, скорость чтения/записи 10,5 Мбит/с");
            //Console.WriteLine("Введите название:");
            //string name = Console.ReadLine();
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Выберите тип:\n1. односторонний\n2. двусторонний");
            int x = Int32.Parse(Console.ReadLine());
            if (x==1)
            {
                d = new DVD("DVD", model, DVDType.oneSide);
            }
            else
            {
                d = new DVD("DVD", model, DVDType.bothSide);
            }
            storages.Add(d);
            Console.WriteLine("DVD-диск создан и добавлен в список!");
            Thread.Sleep(2000);
        }

        public void AddFlash()
        {
            Console.Clear();
            Flash f = null;
            Console.WriteLine("Вы создаете Flash-память, скорость USB 3.0");
            //Console.WriteLine("Введите название:");
            //string name = Console.ReadLine();
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Введите объем памяти - в Мбайтах:");
            int memory = Int32.Parse(Console.ReadLine());
            f = new Flash("HDD", model, memory);
            storages.Add(f);
            Console.WriteLine("Flash-накопитель создан и добавлен в список!");
            Thread.Sleep(2000); 
        }
    }
}
