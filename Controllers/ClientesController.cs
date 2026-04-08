using Microsoft.AspNetCore.Mvc;
using DistribuidoraAseo.DAO.Interfaces;

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
        public IActionResult Get()
        {
            var clientes = _clienteDAO.GetAll();
            return Ok(clientes);
        }
    }
}