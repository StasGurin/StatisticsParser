using MongoDB.Driver;
using Operativ.Models;
using System.Threading.Tasks;
using System.Web.Http;
using Operativ.BLL;

namespace Operativ.Controllers
{
    public class PreviousMonthsController : ApiController
    {

        private IMongoCollection<Month> Collection { get; set; }

        public PreviousMonthsController()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("Operation");
            Collection = database.GetCollection<Month>("previousMonth");
        }

        public async Task Get(string id)
        {
            for (int i = 3; i <= 14; i++)
            {
                
                var dateInput = Parser.Parse(id, i);
                var options = new UpdateOptions { IsUpsert = true };
                var filter = Builders<Month>.Filter.Eq(m=>m.Id, dateInput.Id);
                if (dateInput.Persent == "до грудня \r\n\t\tпопереднього року") break;
                await Collection.ReplaceOneAsync(filter, dateInput, options);
            }
        }
    }
}