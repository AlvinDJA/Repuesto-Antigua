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
    public class DetalleMostrar
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public float Costo { get; set; }
        public float Cantidad { get; set; }
        public float Subtotal { get; set; }
        public DetalleMostrar(FacturasDetalle m)
        {
            Id = m.ProductoId;
            Descripcion = ProductosBLL.Search(m.ProductoId).Descripcion;
            Precio = m.Precio;
            Cantidad = m.Cantidad;
            Subtotal = Precio * Cantidad;
        }
        public DetalleMostrar(ComprasDetalle m)
        {
            Id = m.ProductoId;
            Descripcion = ProductosBLL.Search(m.ProductoId).Descripcion;
            Costo = m.Costo;
            Cantidad = m.Cantidad;
            Subtotal = Costo * Cantidad;
        }
    }
    public partial class rFacturas : Window
    {
        int user;
        private Facturas factura = new Facturas();
        private List<DetalleMostrar> detallemostrar;
        public rFacturas(int usuario)
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
            user = usuario;
            detallemostrar = new List<DetalleMostrar>();
        }
        public rFacturas(int usuario, Facturas factura)
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
            user = usuario;
            this.factura = FacturasBLL.Search(factura.FacturaId);
            detallemostrar = new List<DetalleMostrar>();
            Cargar();
        }
        private void IniciarCombobox()
        {
            ClientesComboBox.ItemsSource = ClientesBLL.GetList();
            ClientesComboBox.SelectedValuePath = "ClienteId";
            ClientesComboBox.DisplayMemberPath = "Nombres";

            ProductosComboBox.ItemsSource = ProductosBLL.GetList();
            ProductosComboBox.SelectedValuePath = "ProductoId";
            ProductosComboBox.DisplayMemberPath = "Descripcion";
        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = factura;
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = detallemostrar;
            PrecioTextBox.Clear();
            CantidadTextBox.Clear();
        }
        private void Limpiar()
        {
            this.factura = new Facturas();
            this.factura.Fecha = DateTime.Now;
            detallemostrar = new List<DetalleMostrar>();
            DatosDataGrid.ItemsSource = detallemostrar;
            this.DataContext = factura;
            TotalTextBox.Clear();
        }
        private void Actualizar(List<FacturasDetalle>  detalle)
        {
            detallemostrar = new List<DetalleMostrar>();
            foreach (FacturasDetalle p in detalle)
                detallemostrar.Add(new DetalleMostrar(p));
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

            factura.UsuarioId = user;

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
                MessageBox.Show("Primero seleccione un producto", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }
            if (DatosDataGrid.Items.Count >= 1 && DatosDataGrid.SelectedIndex <= DatosDataGrid.Items.Count - 1)
            {
                FacturasDetalle m = (FacturasDetalle)factura.Detalle[DatosDataGrid.SelectedIndex];
                float itbs = (float)(ProductosBLL.Search(Convert.ToInt32(m.ProductoId)).PorcentajeITBIS) / 100;
                factura.Total -=m.Precio * m.Cantidad ;
                factura.Total -=  m.Precio * m.Cantidad * itbs;
                factura.Itbis -= m.Precio * m.Cantidad * itbs;
                factura.Detalle.RemoveAt(DatosDataGrid.SelectedIndex);
                Actualizar(factura.Detalle);
                Cargar();
            }
        }

        private FacturasDetalle GetSelectedCompra()
        {
            if (DatosDataGrid.SelectedIndex == -1)
                return null;
            object facturas = factura.Detalle[DatosDataGrid.SelectedIndex];
            if (facturas != null)
                return (FacturasDetalle)facturas;
            else
                return null;
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;
            float itbs = (float)(ProductosBLL.Search(Convert.ToInt32(ProductosComboBox.SelectedValue)).PorcentajeITBIS)/100;
            factura.Total -= factura.Itbis;
            factura.Itbis += Convert.ToSingle(PrecioTextBox.Text) * Convert.ToSingle(CantidadTextBox.Text) *itbs ;
            factura.Total += Convert.ToSingle(PrecioTextBox.Text)* Convert.ToSingle(CantidadTextBox.Text) + factura.Itbis;
            
            factura.Detalle.Add(new FacturasDetalle(factura.FacturaId, 
                Convert.ToInt32(ProductosComboBox.SelectedValue), Convert.ToSingle(CantidadTextBox.Text),
                Convert.ToSingle(PrecioTextBox.Text)));
            Actualizar(factura.Detalle);
            Cargar();
        }

        private void ProductosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos found = ProductosBLL.Search(Convert.ToInt32(ProductosComboBox.SelectedValue));

            if (found != null)
                PrecioTextBox.Text = found.Precio.ToString();
            else
                PrecioTextBox.Text = "";
        }
        
    }
}
