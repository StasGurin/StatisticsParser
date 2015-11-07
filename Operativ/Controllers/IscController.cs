using System.Threading.Tasks;
using System.Web.Http;
using Operativ.BLL;

namespace Operativ.Controllers
{
    [RoutePrefix("/api/isc")]
    public class IscController : BaseController
    {
        public async Task Get(string id)
        {
            await Parse(id, new IscParser(), "ISC");
        }
    }
}