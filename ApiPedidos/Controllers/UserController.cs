using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPedidos.Models;

using Microsoft.AspNetCore.Cors;

namespace ApiPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private ProductosContext _dbContext;


        public UserController(ProductosContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        [Route("usuarios")]
        public IActionResult Lista()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                lista = _dbContext.Usuarios.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Usuario objeto)
        {


            try
            {
                _dbContext.Usuarios.Add(objeto);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }




        }

        [HttpDelete]
        [Route("Eliminar/{idUsuario:int}")]
        public IActionResult Eliminar(int idUsuario)
        {

            var oUsuario = _dbContext.Usuarios.Find(idUsuario);

            if (oUsuario == null)
            {
                return BadRequest("Usuarion no encontrado");

            }

            try
            {

                _dbContext.Usuarios.Remove(oUsuario);
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
        public IActionResult Editar([FromBody] Usuario objeto)
        {
            var oUsuario= _dbContext.Usuarios.Find(objeto.UsuId);

            if (oUsuario == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {
                oUsuario.UsuNombre = objeto.UsuNombre is null ? oUsuario.UsuNombre : objeto.UsuNombre;
                oUsuario.UsuPass = objeto.UsuPass is null ? oUsuario.UsuNombre : objeto.UsuPass;




                _dbContext.Usuarios.Update(oUsuario);
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
