using Microsoft.AspNetCore.Mvc;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.Services;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoDAO _pedidoDAO;
        private readonly PedidoService _pedidoService;

        public PedidosController(IPedidoDAO pedidoDAO, PedidoService pedidoService)
        {
            _pedidoDAO = pedidoDAO;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_pedidoDAO.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pedido = _pedidoDAO.GetById(id);

            if (pedido == null)
                return NotFound("Pedido no encontrado");

            return Ok(pedido);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PedidoRequest request)
        {
            if (request == null || request.Pedido == null || request.Detalles == null)
                return BadRequest("Datos inválidos");

            try
            {
                _pedidoService.CrearPedido(request.Pedido, request.Detalles);
                return Ok("Pedido creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pedido = _pedidoDAO.GetById(id);

            if (pedido == null)
                return NotFound("Pedido no encontrado");

            _pedidoDAO.Delete(id);
            return Ok("Pedido eliminado");
        }
    }

    public class PedidoRequest
    {
        public Pedido? Pedido { get; set; }
        public List<DetallePedido>? Detalles { get; set; }
    }
}