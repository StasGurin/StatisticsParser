using System.Threading.Tasks;
using System.Web.Http;
using Operativ.BLL;

namespace Operativ.Controllers
{
    [RoutePrefix("/api/bisc")]
    public class BiscController : BaseController
    {
        public async Task Get(string id)
        {
            await Parse(id, new BiscParser(), "BISC");
        }
    }
}