using MongoDB.Driver;
using Operativ.Models;
using System.Threading.Tasks;
using System.Web.Http;
using Operativ.BLL;

namespace Operativ.Controllers
{
    public class PreviousMonthsController : ApiController
    {

        private IMongoCollection<Month> сollectionBisc { get; set; }
        private IMongoCollection<Month> collectionIsc { get; set; }

        public PreviousMonthsController()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("Operativ");
            сollectionBisc = database.GetCollection<Month>("BISC");
            collectionIsc = database.GetCollection<Month>("ISC");
        }

        public async Task Get(string id)
        {
            var options = new UpdateOptions { IsUpsert = true };
            int i = 0;
            while (i != 2)
            {
                for (int parseLineNumber = i + 2; parseLineNumber <= i + 13; parseLineNumber++)
                {
                    var dateInput = Parser.Parse(id, parseLineNumber, i);
                    var filter = Builders<Month>.Filter.Eq(m => m.YearMonth, dateInput.YearMonth);
                    if ((dateInput.Percent == "до грудня \r\n\t\tпопереднього року") || (dateInput.Percent == "&nbsp;")) break;
                    if (i == 0) await collectionIsc.ReplaceOneAsync(filter, dateInput, options);
                    if (i == 1) await сollectionBisc.ReplaceOneAsync(filter, dateInput, options);             //insert and update date in DB
                }
                i++;
            }
        }
    }
}