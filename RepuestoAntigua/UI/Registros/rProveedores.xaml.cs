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
using BLL;
using Entidades;

namespace RepuestoAntigua.UI.Registros
{
    /// <summary>
    /// Interaction logic for rProveedores.xaml
    /// </summary>
    public partial class rProveedores : Window
    {
        Proveedores proveedor;
        public rProveedores(int usuario)
        {
            InitializeComponent();
            this.proveedor = new Proveedores();
            this.DataContext = proveedor;
        }

        public rProveedores(Proveedores proveedor)
        {
            InitializeComponent();
            this.proveedor = proveedor;
            this.DataContext = proveedor;
        }


        public void Limpiar()
        {
            this.proveedor = new Proveedores();
            this.DataContext = proveedor;
        }

        private bool Validar()
        {
            bool esValido = true;
            if (NombresTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ingrese un nombre e intente de nuevo", "Mensaje", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (CorreoTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un Correo e intente de nuevo", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (RNCTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un RNC e intente de nuevo", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (TelefonoTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un Telefono e intente de nuevo", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return esValido;
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            var paso = ProveedoresBLL.Save(proveedor);
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado con Exito", "Exito",
                    MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Ha ocurrido un error", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Proveedores encontrado = ProveedoresBLL.Buscar(proveedor.ProveedorId);

            if (encontrado != null)
            {
                proveedor = encontrado;
                this.DataContext = proveedor;
            }
            else
            {
                Limpiar();
                MessageBox.Show("No existe en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Proveedores existe = ProveedoresBLL.Buscar(proveedor.ProveedorId);
            if (proveedor == null)
            {
                MessageBox.Show("Primero seleccione un un proveedor",
                    "Error", MessageBoxButton.OK);
                return;
            }
            else
            {
                MessageBoxResult opcion =
                    MessageBox.Show("Estas seguro de que desear eliminar a " + proveedor.Nombres + "?",
                    "Proveedores", MessageBoxButton.YesNo);
                if (opcion.Equals(MessageBoxResult.Yes))
                {
                    if (ProveedoresBLL.Delete(proveedor.ProveedorId))
                    {
                        Limpiar();
                        ProveedoresBLL.Eliminar(proveedor.ProveedorId);
                        MessageBox.Show("Ha sido eliminado exitosamente", "Exito",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}
