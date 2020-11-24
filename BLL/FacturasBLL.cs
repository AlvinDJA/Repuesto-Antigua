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
    class FacturasBLL
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
                var factura = contexto.Facturas.Find(id);
                if (factura != null)
                {
                    contexto.Facturas.Remove(factura);//remover la entidad
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