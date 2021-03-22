using PocServiceBus.BusMessaging;
using System.Threading.Tasks;

namespace PocServiceBus.Api.Consumer
{
    public class ClienteCriadoEventHandler : AbstractHandlerData
    {
        public override Task<string> Handle(string request)
        {
            if (request.Contains("ClienteCriadoEvent"))
            {
                Task.Delay(10000);
                return Task.FromResult(request);
            }
            else
            {
                return base.Handle(request);
            }
        }


    }
}
