using MongoDB.Driver;
using Operativ.BLL;
using Operativ.Models;
using System.Threading.Tasks;
using System.Web.Http;

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
                for (;;)
                {
                    var biscInput = BiscParser.Parse(id);
                    var iscInput = IscParser.Parse(id);
                    var biscFilter = Builders<Month>.Filter.Eq(m => m.YearMonth, biscInput.YearMonth);
                    var iscFilter = Builders<Month>.Filter.Eq(m => m.YearMonth, iscInput.YearMonth);
                    if ((biscInput.Percent == "до грудня \r\n\t\tпопереднього року") || (iscInput.Percent == "&nbsp;")) break;
                    await collectionIsc.ReplaceOneAsync(iscFilter, iscInput, options);
                    await сollectionBisc.ReplaceOneAsync(biscFilter, biscInput, options);             //insert and update date in DB
                }
            IscParser.parseLineNumber = 2;
            BiscParser.parseLineNumber = 3;
        }
    }
}