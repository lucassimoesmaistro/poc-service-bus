using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocServiceBus.Api.Events;
using PocServiceBus.Core.Mediator;
using PocServiceBus.BusMessaging;

namespace PocServiceBus.Api.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IMessageBus _bus;

        public ClientesController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {

            return Ok();
        }

        [HttpGet("clientes/send")]
        public async Task<IActionResult> Enviar()
        {
            ClienteCriadoEvent clienteCriadoEvent = new ClienteCriadoEvent();

            await _bus.SendMessage(clienteCriadoEvent);

            return Ok();
        }
        [HttpGet("clientes/delete")]
        public async Task<IActionResult> excluir()
        {
            ClienteExcluidoEvent clienteExcluidoEvent = new ClienteExcluidoEvent();

            await _bus.SendMessage(clienteExcluidoEvent);

            return Ok();
        }
    }
}
