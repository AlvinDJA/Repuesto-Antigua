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
    public class FacturasBLL
    {
        public static bool Save(Facturas factura)
        {
            if (!Existe(factura.FacturaId))
                return Insert(factura);
            else
                return Modify(factura);
        }
        private static bool Insert(Facturas factura)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Facturas.Add(factura);
                paso = contexto.SaveChanges() > 0;
                Productos producto;
                ICollection<FacturasDetalle> detalle = factura.Detalle;
                foreach (FacturasDetalle m in detalle)
                {
                    producto = ProductosBLL.Search(m.ProductoId);
                    producto.Cantidad -= m.Cantidad;
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

        public static bool Modify(Facturas factura)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Productos producto;
                ICollection<FacturasDetalle> detalle = Search(factura.FacturaId).Detalle;
                foreach (FacturasDetalle m in detalle)
                {
                    producto = ProductosBLL.Search(m.ProductoId);
                    producto.Cantidad += m.Cantidad;
                    ProductosBLL.Save(producto);
                }
                contexto.Database.ExecuteSqlRaw($"Delete FROM FacturasDetalle Where FacturaId={factura.FacturaId}");
                foreach (var item in factura.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                ICollection<FacturasDetalle> nuevo = factura.Detalle;
                foreach (FacturasDetalle m in nuevo)
                {
                    producto = ProductosBLL.Search(m.ProductoId);
                    producto.Cantidad -= m.Cantidad;
                    ProductosBLL.Save(producto);
                }
                contexto.Entry(factura).State = EntityState.Modified;
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
                var venta = Search(id);
                Productos producto;
                ICollection<FacturasDetalle> viejosDetalles = Search(venta.FacturaId).Detalle;
                foreach (FacturasDetalle d in viejosDetalles)
                {
                    producto = ProductosBLL.Search(d.ProductoId);
                    producto.Cantidad += d.Cantidad;
                    ProductosBLL.Save(producto);
                }
                if (venta != null)
                {
                    contexto.Entry(venta).State = EntityState.Deleted;
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
                encontrado = contexto.Facturas.Any(v => v.FacturaId == id);
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
        public static Facturas Search(int id)
        {
            Contexto contexto = new Contexto();
            Facturas venta;
            try
            {
                venta = contexto.Facturas
                        .Include(v => v.Detalle)
                        .Where(v => v.FacturaId == id)
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

            return venta;
        }

        public static List<Facturas> GetList(Expression<Func<Facturas, bool>> criterio)
        {
            List<Facturas> lista = new List<Facturas>();
            Contexto contexto = new Contexto();
           
            try
            {
                lista = contexto.Facturas.Where(criterio).AsNoTracking().ToList();
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