using System.Configuration;
using MongoDB.Driver;
using Operativ.BLL;
using Operativ.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace Operativ.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected async Task Parse(string id, Parser parser, string collectionName)
        {
            var host = ConfigurationManager.AppSettings["MongoDBHost"];
            var dbName = ConfigurationManager.AppSettings["MongoDBName"];
            var collection = new MongoClient(host).GetDatabase(dbName).GetCollection<Month>(collectionName);

            foreach(var month in parser.Parse(id))
            {
                var filter = Builders<Month>.Filter.Eq(m => m.YearMonth, month.YearMonth);
                await collection.ReplaceOneAsync(filter, month, new UpdateOptions { IsUpsert = true });
            }
        }
    }
}