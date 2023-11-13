using Datos.Modelos;
using Datos.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace EXU2.Controllers
{
    [Route("api")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private LibroDAO libroDAO = new LibroDAO();

        // Endpoint para consultar a todos los libros de la BD.
        [HttpGet("libros/todos")]
        public List<Libro> getTodosLibros()
        {
            var libro = libroDAO.seleccionarTodosL();
            return libro;
        }

        // Endpoint para crear un nuevo libro.
        [HttpPost("libro/insertar")]
        public bool insertarLibro([FromBody] Libro libro)
        {
            return libroDAO.crearLibro(libro);
        }

        // Endpoint para consultar a todas los libros de la BD.        
        [HttpGet("LibrosConAutores/todos")]
        public List<LibAutDAO> getTodosLibrosConAutores()
        {
            var libro = libroDAO.seleccionarLibrosConAutor();
            return libro;
        }

        // Endpoint para consultar una autor específica por ID.      
        [HttpGet("libro/id")]
        public Libro getLibro(int id)
        {
            return libroDAO.seleccionarLibroId(id);
        }

        // Endpoint para buscar un libro por titulo
        [HttpGet("tituloBusqueda")]
        public IActionResult BuscarLibrosPorTitulo(string tituloBusqueda)
        {
            try
            {
                var libros = libroDAO.buscarLibrosPorTitulo(tituloBusqueda);
                return Ok(libros);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar libros por título");
            }
        }
    }
}
