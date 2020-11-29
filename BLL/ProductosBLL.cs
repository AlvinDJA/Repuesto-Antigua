using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Entidades;
using DAL;

namespace BLL
{
    public class ProductosBLL
    {
        public static bool Save(Productos producto)
        {
            if (!Exist(producto.ProductoId))
                return Insert(producto);
            else
                return Modify(producto);
        }
        private static bool Insert(Productos producto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Productos.Add(producto);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Modify(Productos producto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(producto).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Delete(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var persona = contexto.Productos.Find(id);
                if (persona != null)
                {
                    contexto.Productos.Remove(persona);//remover la entidad
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static Productos Search(int id)
        {
            Contexto contexto = new Contexto();
            Productos producto;
            try
            {
                producto = contexto.Productos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return producto;
        }
        public static Productos Search(string codigo)
        {
            Contexto contexto = new Contexto();
            Productos producto;
            try
            {
                producto = contexto.Productos.Where(p => p.ProductoId.Equals(codigo)).
                    FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return producto;
        }

        public static bool Exist(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            
            try
            {
                encontrado = contexto.Productos.Any(p => p.ProductoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }

        public static List<Object> GetList(string a, string criterio)
        {
            List<Object> lista = new List<Object>();
            Contexto contexto = new Contexto();
            if(a == "ProductoId")
                try
                {
                    lista = (
                      from p in contexto.Productos
                      join m in contexto.Marcas
                      on p.MarcaId equals m.MarcaId
                      where p.ProductoId == Convert.ToInt32(criterio)
                      select new
                      {
                          p.ProductoId,
                          p.UsuarioId,
                          Marca = m.Nombres,
                          p.Descripcion,
                          p.Cantidad,
                          p.NoSerie,
                          p.Precio,
                          p.Costo,
                          p.PorcentajeITBIS,
                          p.MargenGanancia
                      }).ToList<Object>();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }
            else if (a == "Marca")
                try
                {
                    lista = (
                      from p in contexto.Productos
                      join m in contexto.Marcas
                      on p.MarcaId equals m.MarcaId
                      where m.Nombres.ToLower() == criterio.ToLower()
                      select new
                      {
                          p.ProductoId,
                          p.UsuarioId,
                          Marca = m.Nombres,
                          p.Descripcion,
                          p.Cantidad,
                          p.NoSerie,
                          p.Precio,
                          p.Costo,
                          p.PorcentajeITBIS,
                          p.MargenGanancia
                      }).ToList<Object>();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }
            else
                try
                {
                    lista = (
                      from p in contexto.Productos
                      join m in contexto.Marcas
                      on p.MarcaId equals m.MarcaId
                      where p.Descripcion.ToLower() == criterio.ToLower()
                      select new
                      {
                          p.ProductoId,
                          p.UsuarioId,
                          Marca = m.Nombres,
                          p.Descripcion,
                          p.Cantidad,
                          p.NoSerie,
                          p.Precio,
                          p.Costo,
                          p.PorcentajeITBIS,
                          p.MargenGanancia
                      }).ToList<Object>();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    contexto.Dispose();
                }
            return lista;
        }
        public static List<Productos> GetList()
        {
            List<Productos> lista = new List<Productos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Productos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        public static List<Object> GetList(bool f)
        {
            List<Object> lista = new List<Object>();
            Contexto contexto = new Contexto();
            try
            {
                lista = (
               from p in contexto.Productos
               join m in contexto.Marcas
               on p.MarcaId equals m.MarcaId
               select new
               {
                   p.ProductoId,
                   p.UsuarioId,
                   Marca = m.Nombres,
                   p.Descripcion,
                   p.Cantidad,
                   p.NoSerie,
                   p.Precio,
                   p.Costo,
                   p.PorcentajeITBIS,
                   p.MargenGanancia
               }).ToList<Object>();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
