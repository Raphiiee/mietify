using Mietyfy.Protobuf.Messages;

namespace Mietify.DataProducer;

public class KafkaMessageGenerator
{
    public IEnumerable<Listing> GenerateMessages(int count)
    {
        var listOfListings = new List<Listing>();
        for (int i = 0; i < count; i++)
            listOfListings.Add(new Listing
            {
                Name = $"Schöne Wohnung im {i}",
                Id = Guid.NewGuid().ToString(),
                Area = 100 * i,
                Price = 1000 * i,
                Address = new Address
                {
                    Country = "AT",
                    City = "Wien",
                    Street = "Schönbrunner Straße",
                    PostalCode = $"100{i}"
                }
            });
       return listOfListings;
    }
}