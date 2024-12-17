using Examen_05.Models;
using Examen_05.Request;
using Examen_05.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen_05.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public List<ProductoResponse> Listar()
        {
            List<ProductoResponse> response = new List<ProductoResponse>();
            try
            {
                using (var _context = new AppDBContext())
                {
                    var productos = _context.Productos.ToList();

                    foreach (var producto in productos)
                    {
                        response.Add(new ProductoResponse
                        {
                            ProductoID= producto.ProductoID,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio

                        });
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            return response;
        }

        [HttpPost]
        public ResponseBase Insertar(ProductoRequest request)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                Producto producto = new Producto
                {
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    CategoriaID= request.CategoriaID
                };

                using (var _context = new AppDBContext())
                {
                    _context.Add(producto);
                    _context.SaveChanges();
                    response.CodigoError = 0;
                    response.Mensaje = "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                response.CodigoError = -1;
                response.Mensaje = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        public ProductoResponse ListarPorID(int id)
        {
            ProductoResponse response = new ProductoResponse();
            try
            {
                using (var _context = new AppDBContext())
                {
                    var producto = _context.Productos.Find(id);

                    response.Nombre = producto.Nombre;
                    response.Precio = producto.Precio;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
