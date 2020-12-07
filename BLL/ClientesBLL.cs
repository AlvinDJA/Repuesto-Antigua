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
    public class ClientesBLL
    {
        public static bool Save(Clientes cliente)
        {
            if (!Exist(cliente.ClienteId))
                return Insert(cliente);
            else
                return Modify(cliente);
        }
        private static bool Insert(Clientes cliente)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Clientes.Add(cliente);
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

        public static bool Modify(Clientes cliente)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(cliente).State = EntityState.Modified;
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
                var cliente = contexto.Clientes.Find(id);
                if (cliente != null)
                {
                    contexto.Clientes.Remove(cliente);//remover la entidad
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

        public static Clientes Search(int id)
        {
            Contexto contexto = new Contexto();
            Clientes cliente;
            try
            {
                cliente = contexto.Clientes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return cliente;
        }
        public static Clientes Search(string codigo)
        {
            Contexto contexto = new Contexto();
            Clientes cliente;
            try
            {
                cliente = contexto.Clientes.Where(p => p.ClienteId.Equals(codigo)).
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

            return cliente;
        }

        public static bool Exist(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Clientes.Any(p => p.ClienteId == id);
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

        public static List<object> GetList(string criterio, string valor)
        {
            List<object> lista;
            Contexto contexto = new Contexto();
            try
            {
                var query = (
                    from c in contexto.Clientes
                    join u in contexto.Usuarios on c.UsuarioId equals u.UsuarioId
                    select new
                    {
                        c.ClienteId,
                        c.Nombres,
                        c.Apellidos,
                        c.Celular,
                        c.Cedula,
                        c.Telefono,
                        c.Direccion,
                        c.Correo,
                        u.Usuario
                    }
                );

                if (criterio.Length != 0)
                {
                    switch (criterio)
                    {
                        case "ProveedorId":
                            query = query.Where(p => p.ClienteId == Convert.ToInt32(valor));
                            break;
                        case "Nombres":
                            query = query.Where(p => p.Nombres.ToLower().Contains(valor.ToLower()));
                            break;
                        case "Apellidos":
                            query = query.Where(p => p.Apellidos.ToLower().Contains(valor.ToLower()));
                            break;
                        case "Celular":
                            query = query.Where(p => p.Celular.ToLower().Contains(valor.ToLower()));
                            break;
                        case "Cedula":
                            query = query.Where(p => p.Cedula.ToLower().Contains(valor.ToLower()));
                            break;
                        case "Telefono":
                            query = query.Where(p => p.Telefono.ToLower().Contains(valor.ToLower()));
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

        public static List<Clientes> GetList(Expression<Func<Clientes, bool>> criterio)
        {
            List<Clientes> lista = new List<Clientes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Clientes.Where(criterio).AsNoTracking().ToList();
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
        public static List<Clientes> GetList()
        {
            List<Clientes> lista = new List<Clientes>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Clientes.ToList();
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
