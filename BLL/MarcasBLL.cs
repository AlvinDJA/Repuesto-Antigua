using DAL;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public class MarcasBLL
    {
        public static bool Save(Marcas marca)
        {
            if (!Existe(marca.MarcaId))
                return Insert(marca);
            else
                return Modify(marca);
        }
        private static bool Insert(Marcas marca)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Marcas.Add(marca);
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

        public static bool Modify(Marcas marca)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(marca).State = EntityState.Modified;
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
                var marca = contexto.Marcas.Find(id);
                if (marca != null)
                {
                    contexto.Marcas.Remove(marca);
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
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var marca = contexto.Marcas.Find(id);
                if (marca != null)
                {
                    contexto.Marcas.Remove(marca);
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

        public static Marcas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Marcas marca;
            try
            {
                marca = contexto.Marcas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return marca;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Marcas
                    .Any(e => e.MarcaId == id);
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
        public static bool Existe(string nombre)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Marcas
                    .Any(e => e.Nombres == nombre);
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

        public static Marcas Search(int id)
        {
            Contexto contexto = new Contexto();
            Marcas marca;
            try
            {
                marca = contexto.Marcas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return marca;
        }


        public static List<object> GetList(string criterio, string valor)
        {
            List<object> lista;
            Contexto contexto = new Contexto();
            try
            {
                var query = (
                    from m in contexto.Marcas
                    join u in contexto.Usuarios on m.UsuarioId equals u.UsuarioId
                    select new
                    {
                        m.MarcaId,
                        m.Nombres,
                        u.Usuario
                    }
                );

                if (criterio.Length != 0)
                {
                    switch (criterio)
                    {
                        case "MarcaId":
                            query = query.Where(p => p.MarcaId == Convert.ToInt32(valor));
                            break;
                        case "Nombres":
                            query = query.Where(p => p.Nombres.ToLower().Contains(valor.ToLower()));
                            break;
                        case "Usuario":
                            query = query.Where(p => p.Usuario.ToLower().Contains(valor.ToLower()));
                            break;


                    }
                }
                lista = query.ToList<object>();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<Marcas> GetList(Expression<Func<Marcas, bool>> criterio)
        {
            List<Marcas> lista = new List<Marcas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Marcas.Where(criterio).ToList();
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
        public static List<Marcas> GetList()
        {
            List<Marcas> lista = new List<Marcas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Marcas.ToList();
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
