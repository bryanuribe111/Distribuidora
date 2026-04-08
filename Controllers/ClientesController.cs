using Microsoft.AspNetCore.Mvc;
using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteDAO _clienteDAO;

        public ClientesController(IClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_clienteDAO.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteDAO.GetById(id);

            if (cliente == null)
                return NotFound("Cliente no encontrado");

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Datos inválidos");

            _clienteDAO.Insert(cliente);
            return Ok("Cliente creado correctamente");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Cliente cliente)
        {
            var existente = _clienteDAO.GetById(id);

            if (existente == null)
                return NotFound("Cliente no encontrado");

            cliente.IdCliente = id;
            _clienteDAO.Update(cliente);

            return Ok("Cliente actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _clienteDAO.GetById(id);

            if (cliente == null)
                return NotFound("Cliente no encontrado");

            _clienteDAO.Delete(id);
            return Ok("Cliente eliminado correctamente");
        }
    }
}