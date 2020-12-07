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
    /// Interaction logic for cProductos.xaml
    /// </summary>
    public partial class cProductos : Window
    {
        private int user;
        public cProductos(int usuario)
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
                        listado = ProductosBLL.GetList(true);
                        break;
                    case 1:
                        listado = ProductosBLL.GetList("ProductoId", criterio);
                        break;
                    case 2:
                        listado = ProductosBLL.GetList("Descripcion", criterio);
                        break;
                    case 3:
                        listado = ProductosBLL.GetList("Marca", criterio);
                        break;
                }
            }
            else
            {
                listado = ProductosBLL.GetList(true);
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
            Productos usuario = GetSelectedProducto();

            if (usuario == null)
            {
                MessageBox.Show("Primero seleccione una Producto", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rProductos(usuario).Show();
            Inicializar();
        }

        private Productos GetSelectedProducto()
        {
            object Productos = DatosDataGrid.SelectedItem;

            if (Productos != null)
                return ProductosBLL.Search(
                    Convert.ToInt32(
                        Productos.GetType().
                        GetProperty("ProductoId").
                        GetValue(Productos).
                        ToString()));
            else
                return null;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            new rProductos(user).Show();
            Inicializar();
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Productos Producto = GetSelectedProducto();
            if (Producto == null)
            {
                MessageBox.Show("Primero seleccione una Producto", "Error",
                    MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a la marca " + Producto.Descripcion + "?",
                "Productos", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (ProductosBLL.Delete(Producto.ProductoId))
                {
                    Inicializar();
                    MessageBox.Show("Producto eliminada", "Exito");
                }
            }
            Inicializar();
        }
    }
}
