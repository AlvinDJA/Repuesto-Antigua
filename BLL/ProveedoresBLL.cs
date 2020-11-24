using DAL;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL
{
    class ProveedoresBLL
    {
        public static bool Save(Proveedores Proveedor)
        {
            if (!Exist(Proveedor.ProveedorId))
                return Insert(Proveedor);
            else
                return Modify(Proveedor);
        }
        private static bool Insert(Proveedores proveedor)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Proveedores.Add(proveedor);
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

        public static bool Modify(Proveedores proveedor)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(proveedor).State = EntityState.Modified;
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
                var proveedor = contexto.Proveedores.Find(id);
                if (proveedor != null)
                {
                    contexto.Proveedores.Remove(proveedor);
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
        private static bool Exist(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Proveedores.Any(p => p.ProveedorId == id);
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

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var proveedor = contexto.Proveedores.Find(id);
                if (proveedor != null)
                {
                    contexto.Proveedores.Remove(proveedor);
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

        public static Proveedores Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Proveedores proveedor;
            try
            {
                proveedor = contexto.Proveedores.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return proveedor;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Proveedores
                    .Any(e => e.ProveedorId == id);
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

        public static List<Proveedores> GetList(Expression<Func<Proveedores, bool>> criterio)
        {
            List<Proveedores> lista = new List<Proveedores>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Proveedores.Where(criterio).ToList();
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
        public static List<Proveedores> GetList()
        {
            List<Proveedores> lista = new List<Proveedores>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Proveedores.ToList();
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
