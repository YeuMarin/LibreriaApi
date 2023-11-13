using Datos.Contexto;
using Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Operaciones
{
    public class LibroDAO
    {
        // OBJETO PARA ACCEDER A LA BD
        public PContexto contexto = new PContexto();

        //METODO PARA MOSTRAR TODOS LOS LIBROS
        public List<Libro> seleccionarTodosL()
        {
            var profesor = contexto.Libros.ToList<Libro>();
            return profesor;
        }

        //METODO PARA MOSTRAR LIBRO POR SU ID
        public Libro seleccionarLibroId(int id)
        {
            var libro = contexto.Libros.Where(p => p.Id == id).FirstOrDefault();
            return libro;
        }

        //mETODO PARA CREAR UN NUEVO LIBRO
        public bool crearLibro(Libro nuevoLibro)
        {
            try
            {
                // VERIFICAR SI EXISTE EL AUTOR
                Autor autorExistente = contexto.Autores.FirstOrDefault(a => a.Nombre == nuevoLibro.Autor.Nombre);

                if (autorExistente == null)
                {
                    // sI NO EXISTE LO CREA
                    contexto.Autores.Add(nuevoLibro.Autor);
                    contexto.SaveChanges();
                }
                else
                {
                    // SI AUTOR EXISTE LO REALCIONA
                    nuevoLibro.AutorId = autorExistente.Id;
                }
                //SE AGREGA EL LIBRO A LA BD
                contexto.Libros.Add(nuevoLibro);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // METODO PARA MOSTRAR TODOS LOS LIBROS CON SUS AUTORES
        public List<LibAutDAO> seleccionarLibrosConAutor()
        {
            var query = from book in contexto.Libros
                        join author in contexto.Autores on book.AutorId
                        equals author.Id
                        select new LibAutDAO
                        {
                            TituloLibro = book.Titulo,
                            NombreAutor = author.Nombre,
                            CapitulosLibro = book.Capitulos,
                            PaginasLibro = book.Paginas,
                            PrecioLibro = book.Precio
                        };

            return query.ToList();
        }

        // METODO PARA BUSCAR UN LIBRO POR SU TITULO
        public List<LibAutDAO> buscarLibrosPorTitulo(string titulo)
        {
            var query = from book in contexto.Libros
                        join author in contexto.Autores on book.AutorId equals author.Id
                        where book.Titulo.Contains(titulo)
                        select new LibAutDAO
                        {
                            TituloLibro = book.Titulo,
                            NombreAutor = author.Nombre,
                            CapitulosLibro = book.Capitulos,
                            PaginasLibro = book.Paginas,
                            PrecioLibro = book.Precio
                        };

            return query.ToList();
        }

    }
}