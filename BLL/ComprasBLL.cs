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
                var compra = contexto.Compras.Find(id);
                if (compra != null)
                {
                    contexto.Compras.Remove(compra);//remover la entidad
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
