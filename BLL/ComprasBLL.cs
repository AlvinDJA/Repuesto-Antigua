using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DAL;
using Entidades;

namespace BLL
{
    public class ComprasBLL
    {
        public static bool Save(Compras compra)
        {
            if (!Existe(compra.CompraId))
                return Insert(compra);
            else
                return Modify(compra);
        }
        private static bool Insert(Compras compra)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Compras.Add(compra);
                paso = contexto.SaveChanges() > 0;
                Productos producto;
                ICollection<ComprasDetalle> detalle = compra.Detalle;
                foreach (ComprasDetalle m in detalle)
                {
                    producto = ProductosBLL.Search(m.ProductoId);
                    producto.Cantidad += m.Cantidad;
                    ProductosBLL.Save(producto);
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

        public static bool Modify(Compras compra)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Productos producto;
                ICollection<ComprasDetalle> detalle = Search(compra.CompraId).Detalle;
                foreach (ComprasDetalle m in detalle)
                {
                    producto = ProductosBLL.Search(m.ProductoId);
                    producto.Cantidad += m.Cantidad;
                    ProductosBLL.Save(producto);
                }
                contexto.Database.ExecuteSqlRaw($"Delete FROM ComprasDetalle Where CompraId={compra.CompraId}");
                foreach (var item in compra.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                ICollection<ComprasDetalle> nuevo = compra.Detalle;
                foreach (ComprasDetalle m in nuevo)
                {
                    producto = ProductosBLL.Search(m.ProductoId);
                    producto.Cantidad -= m.Cantidad;
                    ProductosBLL.Save(producto);
                }
                contexto.Entry(compra).State = EntityState.Modified;
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
                var compra = Search(id);
                Productos producto;
                ICollection<ComprasDetalle> viejosDetalles = Search(compra.CompraId).Detalle;
                foreach (ComprasDetalle d in viejosDetalles)
                {
                    producto = ProductosBLL.Search(d.ProductoId);
                    producto.Cantidad += d.Cantidad;
                    ProductosBLL.Save(producto);
                }
                if (compra != null)
                {
                    contexto.Entry(compra).State = EntityState.Deleted;
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
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Compras.Any(v => v.CompraId == id);
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
        public static Compras Search(int id)
        {
            Contexto contexto = new Contexto();
            Compras compra;
            try
            {
                compra = contexto.Compras
                        .Include(v => v.Detalle)
                        .Where(v => v.CompraId == id)
                        .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return compra;
        }

        public static List<Compras> GetList(Expression<Func<Compras, bool>> criterio)
        {
            List<Compras> lista = new List<Compras>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Compras.Where(criterio).AsNoTracking().ToList();
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
