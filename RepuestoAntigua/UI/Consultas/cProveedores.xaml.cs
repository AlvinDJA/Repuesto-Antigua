using BLL;
using Entidades;
using RepuestoAntigua.UI.Registros;
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

namespace RepuestoAntigua.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cProveedores.xaml
    /// </summary>
    public partial class cProveedores : Window
    {
        private int user;
        public cProveedores(int usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void Inicializar()
        {
            CriterioTextBox.Clear();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Buscar();
            }
        }
        private void Buscar()
        {
            var listado = new List<Object>();
            string criterio = CriterioTextBox.Text.Trim();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroCombobox.SelectedIndex)
                {
                    case 0:
                        listado = ProveedoresBLL.GetList("","");
                        break;
                    case 1:
                        listado = ProveedoresBLL.GetList("ProveedorId", criterio);
                        break;
                    case 2:
                       // listado = ProveedoresBLL.GetList(p => p.Nombres.ToLower().Contains(criterio.ToLower()));
                        break;
                    case 3:
                       // listado = ProveedoresBLL.GetList(p => p.RNC.ToLower().Contains(criterio.ToLower()));
                        break;
                    case 4:
                      //  listado = ProveedoresBLL.GetList(p => p.Telefono.ToLower().Contains(criterio.ToLower()));
                        break;
                }
            }
            else
            {
                listado = ProveedoresBLL.GetList("", "");
            }
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void EditarBoton_Click(object sender, RoutedEventArgs e)
        {
            Proveedores proveedor = GetSelectedProveedor();

            if (proveedor == null)
            {
                MessageBox.Show("Primero seleccione una Proveedor", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rProveedores(proveedor).Show();
            Inicializar();
        }

        private Proveedores GetSelectedProveedor()
        {
            object Proveedores = DatosDataGrid.SelectedItem;

            if (Proveedores != null)
                return (Proveedores)Proveedores;
            else
                return null;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            new rProveedores(user).ShowDialog();
            Inicializar();
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Proveedores Proveedor = GetSelectedProveedor();
            if (Proveedor == null)
            {
                MessageBox.Show("Primero seleccione una Proveedor", "Error",
                    MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a la proveedor " + Proveedor.Nombres + "?",
                "Proveedor", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (ProveedoresBLL.Delete(Proveedor.ProveedorId))
                {
                    Inicializar();
                    MessageBox.Show("Proveedor eliminada", "Exito");
                }
            }
            Inicializar();
        }
    }
}
