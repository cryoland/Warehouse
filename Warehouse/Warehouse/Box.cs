using System;
using System.Threading;
using System.Xml.Serialization;

namespace Warehouse
{
    public enum DateType { Expired, Created }
    public class Box
    {
        private static int increment_id = -1;
        /// <summary>
        /// ID коробки
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }
        /// <summary>
        /// Ширина коробки
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
        /// <summary>
        /// Высота коробки
        /// </summary>
        [XmlAttribute("height")]
        public double Height { get; set; }
        /// <summary>
        /// Толщина коробки
        /// </summary>
        [XmlAttribute("depth")]
        public double Depth { get; set; }
        /// <summary>
        /// Вес коробки
        /// </summary>
        [XmlAttribute("weight")]
        public double Weight { get; set; }
        /// <summary>
        /// Срок годности
        /// </summary>
        [XmlAttribute("expired")]
        public DateTime Expired { get; set; }
        /// <summary>
        /// Объем коробки
        /// </summary>
        public double Volume { get => Width * Height * Depth; }

        public Box() { }
        public Box(double width, double height, double depth, double weight, DateTime date, DateType dateType)
        {
            ID = Interlocked.Increment(ref increment_id);
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            Expired = (dateType == DateType.Expired) ? date : date.AddDays(100);
        }
    }
}
