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
    /// Interaction logic for cFacturas.xaml
    /// </summary>
    public partial class cFacturas : Window
    {
        private int user;
        public cFacturas(int usuario)
        {
            InitializeComponent();
            user = usuario;
            DesdePicker.SelectedDate = DateTime.Now;
            HastaPicker.SelectedDate = DateTime.Now;
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
            var listado = new List<Facturas>();
            string criterio = CriterioTextBox.Text.Trim();

            if (CriterioTextBox.Text.Trim().Length > 0 || FiltroCombobox.SelectedIndex==4)
            {
                switch (FiltroCombobox.SelectedIndex)
                {
                    case 0:
                        listado = FacturasBLL.GetList(f => true);
                        break;
                    case 1:
                        listado = FacturasBLL.GetList(p => p.FacturaId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 2:
                        listado = FacturasBLL.GetList(p => p.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 3:
                        listado = FacturasBLL.GetList(p => p.UsuarioId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 4:
                        DateTime hasta = (DateTime)HastaPicker.SelectedDate;
                        DateTime desde = (DateTime)DesdePicker.SelectedDate;
                        listado = FacturasBLL.GetList(p => p.Fecha >= desde && p.Fecha <= hasta);
                        break;
                }
            }
            else
            {
                listado = FacturasBLL.GetList(f => true);
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
            Facturas factura = GetSelectedFactura();

            if (factura == null)
            {
                MessageBox.Show("Primero seleccione una Factura", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rFacturas(user , factura).ShowDialog();
            Inicializar();
        }
        private Facturas GetSelectedFactura()
        {
            object Facturas = DatosDataGrid.SelectedItem;

            if (Facturas != null)
                return (Facturas)Facturas;
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
            Facturas Factura = GetSelectedFactura();
            if (Factura == null)
            {
                MessageBox.Show("Primero seleccione una Factura", "Error",
                    MessageBoxButton.OK);
                return;
            }
            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a la factura " + Factura.FacturaId + "?",
                "Facturas", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (FacturasBLL.Delete(Factura.FacturaId))
                {
                    Inicializar();
                    MessageBox.Show("Factura eliminada", "Exito");
                }
            }
            Inicializar();
        }
    }
}
