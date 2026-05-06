using Microsoft.AspNetCore.Mvc;
using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoDAO _productoDAO;

        public ProductosController(IProductoDAO productoDAO)
        {
            _productoDAO = productoDAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productoDAO.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producto = _productoDAO.GetById(id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return Ok(producto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            _productoDAO.Insert(producto);

            return Ok(new { mensaje = "Producto creado correctamente" });
        }

        [HttpPut("{id}")]
<<<<<<< HEAD
        public IActionResult Update(int id, [FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            var existente = _productoDAO.GetById(id);

            if (existente == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            producto.IdProducto = id;
            _productoDAO.Update(producto);

            return Ok(new { mensaje = "Producto actualizado correctamente" });
        }
=======
public IActionResult Update(int id, Producto producto)
{
    if (id != producto.IdProducto)
        return BadRequest("ID no coincide");

    var existente = _productoDAO.GetById(id);

    if (existente == null)
        return NotFound();

    _productoDAO.Update(producto);

    return Ok();
}
>>>>>>> a4eebaf (Proyecto backend funcional con integración completa)

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = _productoDAO.GetById(id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            _productoDAO.Delete(id);

            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }
    }
}