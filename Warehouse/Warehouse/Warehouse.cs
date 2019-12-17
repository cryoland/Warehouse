using System.Collections.Generic;
using System.Xml.Serialization;

namespace Warehouse
{
    [XmlRoot("root")]
    public class Warehouse
    {
        [XmlElement("pallets")]
        public List<Pallet> Pallets { get; set; } = new List<Pallet>();
    }
}
