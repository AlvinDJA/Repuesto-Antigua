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
        public rFacturas(int usuario)
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
        }

        private void IniciarCombobox()
        {
            ClientesComboBox.ItemsSource = ProductosBLL.GetList(c => true);
            ClientesComboBox.SelectedValuePath = "ClienteId";
            ClientesComboBox.DisplayMemberPath = "Nombres";

            ProductosComboBox.ItemsSource = ProductosBLL.GetList(c => true);
            ProductosComboBox.SelectedValuePath = "ProductoId";
            ProductosComboBox.DisplayMemberPath = "Descripcion";
        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = factura;
            PrecioTextBox.Clear();
            CantidadTextBox.Clear();
        }
        private void Limpiar()
        {
            this.factura = new Facturas();
            this.factura.Fecha = DateTime.Now;
            this.DataContext = factura;
            TotalTextBox.Clear();
        }
        private bool ValidarAgregar()
        {
            bool esValido = true;
            if (ProductosComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Eliga el productos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (CantidadTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Inserte la cantidad", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (PrecioTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Inserte el precio", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
                MessageBox.Show("No existe la Factura en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar la Factura  " + factura.FacturaId + "?",
                "Facturas", MessageBoxButton.YesNo);

                if (opcion.Equals(MessageBoxResult.Yes))
                {
                    FacturasBLL.Delete(factura.FacturaId);
                    MessageBox.Show("Eliminado", "Exito",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar();
                }
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
            FacturasDetalle facturas = GetSelectedCompra();

            if (facturas == null)
            {
                MessageBox.Show("Primero seleccione una Factura", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }
            if (DatosDataGrid.Items.Count >= 1 && DatosDataGrid.SelectedIndex <= DatosDataGrid.Items.Count - 1)
            {
                FacturasDetalle m = (FacturasDetalle)DatosDataGrid.SelectedValue;
                factura.Total -= m.Precio * m.Cantidad;
                factura.Detalle.RemoveAt(DatosDataGrid.SelectedIndex);
                Cargar();
            }
        }

        private FacturasDetalle GetSelectedCompra()
        {
            object facturas = DatosDataGrid.SelectedItem;

            if (facturas != null)
                return (FacturasDetalle)facturas;
            else
                return null;
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;
            factura.Total += Convert.ToSingle(PrecioTextBox.Text)* Convert.ToSingle(CantidadTextBox.Text);
            factura.Detalle.Add(new FacturasDetalle(factura.FacturaId, 
                Convert.ToInt32(ProductosComboBox.SelectedValue), Convert.ToSingle(CantidadTextBox.Text),
                Convert.ToSingle(PrecioTextBox.Text)));
            Cargar();
        }
    }
}
