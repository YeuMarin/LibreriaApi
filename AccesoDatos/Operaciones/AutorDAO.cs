using Datos.Contexto;
using Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Operaciones
{
    public class AutorDAO
    {
        // OBJETO PARA ACCEDER A LA BD
        public PContexto contexto = new PContexto();

        //METODO PARA MOSTRAR TODOS LOS AUTORES
        public List<Autor> seleccionarTodosA()
        {
            var autores = contexto.Autores.ToList<Autor>();
            return autores;
        }

        //METODO PARA MOSTRAR UN AUTOR POR ID
        public Autor seleccionarIdAutor(int id)
        {
            var autor = contexto.Autores.Where(a => a.Id == id).FirstOrDefault();
            return autor;
        }

        // METODO PARA CREAR UN AUTOR
        public bool crearAutor(string nombre)
        {
            try
            {
                Autor autor = new Autor();
                autor.Nombre = nombre;

                contexto.Autores.Add(autor);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // METODO PARA MOSTRAR TODO LOS AUTORES CON SUS LIBROS
        public List<LibAutDAO> seleccionarAutorConLibros()
        {
            var query = from author in contexto.Autores
                        join book in contexto.Libros on author.Id equals book.AutorId
                        select new LibAutDAO
                        {
                            NombreAutor = author.Nombre,
                            TituloLibro = book.Titulo,
                        };

            return query.ToList();
        }
    }
