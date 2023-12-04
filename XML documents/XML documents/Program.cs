using System.Xml.Linq;

namespace XML_documents
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>
        {
            new Car { Name = "Car1", Price = 20000, Number = "ABC123" },
            new Car { Name = "Car2", Price = 25000, Number = "XYZ456" },
            new Car { Name = "Car3", Price = 18000, Number = "123DEF" }
        };

            SaveCollectionInXmlFile(cars);

            SetNewCarPriceByNumber("XYZ456", 27000);
        }

        static void SaveCollectionInXmlFile(IEnumerable<Car> cars)
        {
            XElement carsXml = new XElement("Cars",
                cars.Select(c => new XElement("Car",
                    new XElement("Name", c.Name),
                    new XElement("Price", c.Price),
                    new XElement("Number", c.Number)
                ))
            );

            carsXml.Save("Cars.xml");
            Console.WriteLine("Collection saved in Cars.xml");
        }

        static void SetNewCarPriceByNumber(string carNumber, double newPrice)
        {
            XDocument doc = XDocument.Load("Cars.xml");

            XElement carElement = doc.Descendants("Car")
                                    .FirstOrDefault(e => e.Element("Number").Value == carNumber);

            if (carElement != null)
            {
                carElement.Element("Price").SetValue(newPrice);
                doc.Save("Cars.xml");
                Console.WriteLine($"Price for car with number {carNumber} updated to {newPrice}");
            }
            else
            {
                Console.WriteLine($"Car with number {carNumber} not found");
            }
        }
    }
    
}