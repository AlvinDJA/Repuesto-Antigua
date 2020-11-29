using BLL;
using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RepuestoAntigua.UI.Registros
{
    /// <summary>
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        private Clientes  cliente;
        private int user;
        public rClientes(int usuario)
        {
            InitializeComponent();
            user = usuario;
            Limpiar();
        }

        public void Limpiar()
        {
            this.cliente = new Clientes();
            this.DataContext = cliente;
        }
        public rClientes(Clientes cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
            this.DataContext = cliente;
        }
        
        public void GuardarBoton_Click(object render, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            cliente.UsuarioId = user;
            bool paso = (bool)ClientesBLL.Save(cliente);
            if (paso)
            {
               
                    MessageBox.Show("Guardado con Exito", "Exito",
                        MessageBoxButton.OK);
               
            }
            else
                MessageBox.Show("Error al guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarBoton_Click(object render, RoutedEventArgs e)
        {
            Contexto context = new Contexto();

            Clientes found = ClientesBLL.Search(Convert.ToInt32(IdTextBox.Text));

            if (found != null)
                this.cliente = found;
            else
            {
                this.cliente = new Clientes();
                MessageBox.Show("No encontrado", "Mensaje",
                    MessageBoxButton.OK);
            }

            this.DataContext = this.cliente;
        }

        private bool Validar()
        {
            bool esValido = true;

            if (NombresTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un nombre e intente de nuevo", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (ApellidosTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un apellido e intente de nuevo", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (TelefonoTextBox.Text.Length == 0 && CelularTextBox.Text.Length == 0)
            {
                MessageBox.Show("Debe ingresar teléfono o celular", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (DireccionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese una direccion e intente de nuevo", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return esValido;
        }
        private void EliminarBoton_Click(object render, RoutedEventArgs e)
        {
            Clientes clientes = this.cliente;

            if (clientes == null)
            {
                MessageBox.Show("Primero seleccione un Cliente",
                    "Mensaje", MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar " +
                clientes.Nombres + "?", "Cliente", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (ClientesBLL.Delete(cliente.UsuarioId))
                    MessageBox.Show("cliente eliminado",
                        "cliente");
            }
        }

        private void NuevoBoton_Click(object render, RoutedEventArgs e)
        {
            Limpiar();
        }
        public Clientes GetCliente()
        {
            return cliente;
        }
    }
}
