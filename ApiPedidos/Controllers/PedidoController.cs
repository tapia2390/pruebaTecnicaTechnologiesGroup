using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPedidos.Models;

using Microsoft.AspNetCore.Cors;

namespace ApiPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private ProductosContext _dbContext;


        public PedidoController(ProductosContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("pedidos")]
        public IActionResult Lista()
        {
            List<Pedido> lista = new List<Pedido>();

            try
            {
                lista = _dbContext.Pedidos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }



        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Pedido objeto)
        {


            try
            {
                _dbContext.Pedidos.Add(objeto);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }




        }

        [HttpDelete]
        [Route("Eliminar/{idPedido:int}")]
        public IActionResult Eliminar(int idPedido)
        {

            var oPedido = _dbContext.Pedidos.Find(idPedido);

            if (oPedido == null)
            {
                return BadRequest("Pedido no encontrado");

            }

            try
            {

                _dbContext.Pedidos.Remove(oPedido);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }




        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Pedido objeto)
        {
            var oPedido = _dbContext.Pedidos.Find(objeto.PedId);

            if (oPedido == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {
                oPedido.PedUsu = objeto.PedUsu is null ? oPedido.PedUsu : objeto.PedUsu;
                oPedido.PedProd = objeto.PedProd is null ? oPedido.PedProd : objeto.PedProd;
                oPedido.PedVrUnit = objeto.PedVrUnit is null ? oPedido.PedVrUnit : objeto.PedVrUnit;
                oPedido.PedCant = objeto.PedCant is null ? oPedido.PedCant : objeto.PedCant;
                oPedido.PedSubTot = objeto.PedSubTot is null ? oPedido.PedSubTot : objeto.PedSubTot;
                oPedido.PedIva = objeto.PedIva is null ? oPedido.PedIva : objeto.PedIva;
                oPedido.PedTotal = objeto.PedTotal is null ? oPedido.PedTotal : objeto.PedTotal;




                _dbContext.Pedidos.Update(oPedido);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }




        }

    }
}
