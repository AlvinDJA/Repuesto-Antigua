using BLL;
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
    /// Interaction logic for rFacturas.xaml
    /// </summary>
    public partial class rFacturas : Window
    {
        private Facturas factura = new Facturas();
        public rFacturas()
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
        }

        private void IniciarCombobox()
        {
            Limpiar();
        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = factura;
        }
        private void Limpiar()
        {
            this.factura = new Facturas();
            this.factura.Fecha = DateTime.Now;
            this.DataContext = factura;
            TiempoTotalTextBox.Clear();
        }
        private bool ValidarAgregar()
        {
            bool esValido = true;
            
            return esValido;
        }
        private bool ValidarGuardar()
        {
            bool esValido = true;
            if (DatosDataGrid.Items.Count == 0)
            {
                esValido = false;
                MessageBox.Show("Debe agregar productos", "Mensajes",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Facturas encontrado = FacturasBLL.Search(factura.FacturaId);

            if (encontrado != null)
            {
                factura = encontrado;
                Cargar();
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
            Facturas existe = FacturasBLL.Search(factura.FacturaId);

            if (existe == null)
            {
                MessageBox.Show("No existe el proyecto en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                FacturasBLL.Delete(factura.FacturaId);
                MessageBox.Show("Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarGuardar())
                return;
            bool paso = false;

            if (factura.FacturaId == 0)
                paso = FacturasBLL.Save(factura);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = FacturasBLL.Save(factura);
                }

                else
                    MessageBox.Show("No existe en la base de datos", "Información");
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Facturas esValido = FacturasBLL.Search(factura.FacturaId);
            return (esValido != null);
        }
        private void Remover_Click(object sender, RoutedEventArgs e)
        {
            if (DatosDataGrid.Items.Count >= 1 && DatosDataGrid.SelectedIndex <= DatosDataGrid.Items.Count - 1)
            {
                FacturasDetalle m = (FacturasDetalle)DatosDataGrid.SelectedValue;
                factura.Detalle.RemoveAt(DatosDataGrid.SelectedIndex);
                Cargar();
            }
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;
            Cargar();
        }
    }
}
