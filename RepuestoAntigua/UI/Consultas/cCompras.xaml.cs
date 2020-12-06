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
    /// Interaction logic for cCompras.xaml
    /// </summary>
    public partial class cCompras : Window
    {
        private int user;
        public cCompras(int usuario)
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
            var listado = new List<Compras>();
            string criterio = CriterioTextBox.Text.Trim();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroCombobox.SelectedIndex)
                {
                    case 0:
                        listado = ComprasBLL.GetList(f => true);
                        break;
                    case 1:
                        listado = ComprasBLL.GetList(p => p.CompraId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 2:
                        listado = ComprasBLL.GetList(p => p.ProveedorId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 3:
                        listado = ComprasBLL.GetList(p => p.UsuarioId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = ComprasBLL.GetList(f => true);
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
            Compras compra = GetSelectedCompra();

            if (compra == null)
            {
                MessageBox.Show("Primero seleccione una Compra", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rCompras(user, compra).ShowDialog();
            Inicializar();
        }
        private Compras GetSelectedCompra()
        {
            object Compras = DatosDataGrid.SelectedItem;

            if (Compras != null)
                return (Compras)Compras;
            else
                return null;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            new rFacturas(user).Show();
            Inicializar();
        }
        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Compras compra = GetSelectedCompra();
            if (compra == null)
            {
                MessageBox.Show("Primero seleccione una Compra", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }
            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a la Compra " + compra.CompraId + "?",
                "Compras", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (ComprasBLL.Delete(compra.CompraId))
                {
                    Inicializar();
                    MessageBox.Show("Compra eliminada", "Exito");
                }
            }
            Inicializar();
        }
    }
}
