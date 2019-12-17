using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace Warehouse
{
    public class Pallet
    {
        private static int increment_id = -1;
        private const double weight = 30.0;
        /// <summary>
        /// ID палетты
        /// </summary>
        [XmlAttribute("id")]
        public int ID { get; set; }
        /// <summary>
        /// Ширина палетты
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
        /// <summary>
        /// Высота палетты
        /// </summary>
        [XmlAttribute("height")]
        public double Height { get; set; }
        /// <summary>
        /// Толщина палетты
        /// </summary>
        [XmlAttribute("depth")]
        public double Depth { get; set; }
        /// <summary>
        /// Вес снаряженной палетты
        /// </summary>
        public double Weight { get => weight + BoxesList.Select(x => x.Weight).Sum(); }
        /// <summary>
        /// Срок годности палетты
        /// </summary>
        public DateTime Expired { get => BoxesList.Select(x => x.Expired).Min(); }
        /// <summary>
        /// Объем палетты
        /// </summary>
        public double Volume { get => Width * Height * Depth + BoxesList.Select(x => x.Volume).Sum(); }

        [XmlElement("boxes")]
        public List<Box> BoxesList { get; set; } = new List<Box>();

        public Pallet() { }
        public Pallet(double width, double height, double depth)
        {
            ID = Interlocked.Increment(ref increment_id);
            Width = width;
            Height = height;
            Depth = depth;
        }
    }
}
