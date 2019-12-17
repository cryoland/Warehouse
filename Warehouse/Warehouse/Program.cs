using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Warehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            var box00 = new Box(width: 100, height: 200, depth: 20, weight: 20, date: new DateTime(2019, 12, 10), dateType: DateType.Created);
            var box01 = new Box(50, 250, 15, 15, new DateTime(2019, 12, 29), DateType.Expired);
            var pallet0 = new Pallet(200, 200, 20);
                pallet0.BoxesList.AddRange(new[] { box00, box01 });

            var box11 = new Box(100, 200, 20, 20, new DateTime(2019, 12, 10), DateType.Created);
            var box12 = new Box(50, 250, 15, 15, new DateTime(2019, 12, 29), DateType.Expired);
            var box13 = new Box(50, 250, 15, 15, new DateTime(2019, 11, 10), DateType.Created);
            var pallet1 = new Pallet(200, 200, 20);
                pallet1.BoxesList.AddRange(new[] { box11, box12, box13 });

            var warehouseOutput = new Warehouse();
            warehouseOutput.Pallets.AddRange(new[] { pallet0, pallet1 });

            //Сериализация коллекции в файл

            XmlSerializer serializer = new XmlSerializer(typeof(Warehouse));
            using (StreamWriter stream = new StreamWriter("output.xml", false, Encoding.Default))
            {
                serializer.Serialize(stream, warehouseOutput);
            }

            //Чтение коллекции из файла
            Warehouse warehouseInput;
            using (StreamReader stream = new StreamReader("input.xml", Encoding.Default))
            {
                warehouseInput = (Warehouse)serializer.Deserialize(stream);
            }

            /*
             Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу. Результат вывести на экран.
             */

            var groups = from pallet in warehouseInput.Pallets
                       orderby pallet.Expired ascending
                       group pallet by pallet.Expired into g
                       select new
                       {
                           Expired = g.Key,
                           Count = g.Count(),
                           Pallets = from p in g
                                     orderby p.Weight ascending
                                     select p
                       };

            foreach (var group in groups)
            {
                Console.WriteLine($"Срок годности: [{group.Expired}]");
                foreach(var palett in group.Pallets)
                {
                    Console.WriteLine($"\tПаллета#{palett.ID} Вес:[{palett.Weight}кг] Габариты:{palett.Width}x{palett.Height}x{palett.Depth} Объем:{palett.Volume} Срок годности:{palett.Expired}");
                    foreach(var box in palett.BoxesList)
                    {
                        Console.WriteLine($"\t\tЯщик#{box.ID} Вес:{box.Weight}кг Габариты:{box.Width}x{box.Height}x{box.Depth} Объем:{box.Volume} Срок годности:{box.Expired}");
                    }
                }
                Console.WriteLine();
            }


            Console.WriteLine($"{new string('=', 30)}\n");


            /*
             Вывести на экран 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.	
             */

            var groups11 = warehouseInput.Pallets.OrderByDescending(p => p.BoxesList.Max(b => b.Expired)).Take(3);

            foreach (var palett in groups11)
            {
                Console.WriteLine($"Паллета#{palett.ID} Макс. срок годности:[{palett.BoxesList.Max(b => b.Expired)}]");
                var boxes = palett.BoxesList.OrderBy(b => b.Volume);
                foreach (var box in boxes)
                {
                    Console.WriteLine($"\tЯщик#{box.ID} Вес:{box.Weight}кг Габариты:{box.Width}x{box.Height}x{box.Depth} Объем:[{box.Volume}] Срок годности:{box.Expired}");
                }
            }


            Console.ReadKey();
        }
    }
}
