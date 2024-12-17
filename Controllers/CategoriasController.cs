﻿using Azure;
using Examen_05.Models;
using Examen_05.Request;
using Examen_05.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen_05.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        [HttpGet]
        public List<CategoriaResponse> Listar()
        {
            List<CategoriaResponse> response = new List<CategoriaResponse>();
            try
            {
                using (var _context= new AppDBContext())
                {
                    var categorias=_context.Categorias.ToList();

                    foreach (var categoria in categorias)
                    {
                        response.Add(new CategoriaResponse
                        {
                            Nombre = categoria.Nombre,
                            Descripcion = categoria.Descripcion
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
        public ResponseBase Insertar(CategoriaRequest request)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                Categoria categoria = new Categoria{
                    Nombre = request.Nombre,
                    Descripcion =request.Descripcion
                };

                using (var _context = new AppDBContext())
                {
                    _context.Add(categoria);
                    _context.SaveChanges();
                    response.CodigoError = 0;
                    response.Mensaje = "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                response.CodigoError= -1;
                response.Mensaje = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        public CategoriaResponse ListarPorID(int id) {
            CategoriaResponse response = new CategoriaResponse();
            try
            {
                using (var _context= new AppDBContext())
                {
                    var categoria = _context.Categorias.Find(id);

                    response.Nombre = categoria.Nombre;
                    response.Descripcion = categoria.Descripcion;
                }
            }
            catch (Exception )
            {
                throw;
            }
            return response;
        }
    }
}