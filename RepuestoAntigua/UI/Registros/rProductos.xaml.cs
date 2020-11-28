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
    /// Interaction logic for rProductos.xaml
    /// </summary>
    public partial class rProductos : Window
    {
        private int porcentaje;
        private Productos producto;
        private int user;
        char modo;
        public rProductos(int usuario)
        {
            InitializeComponent();
            InitializeComboBox();
            user = usuario;
            Limpiar();

        }
        public void Limpiar()
        {
            this.producto = new Productos();
            this.DataContext = producto;
            Radio1.IsChecked = true;
        }
        public rProductos(Productos producto)
        {
            InitializeComponent();
            InitializeComboBox();
            this.producto = producto;
            this.DataContext = producto;
        }
        public rProductos(char mode)
        {
            InitializeComponent();
            InitializeComboBox();
            this.producto = new Productos();
            this.DataContext = producto;
            this.modo = mode;
        }
        private void inicarRadio()
        {
            if (porcentaje == 0)
                Radio1.IsChecked = true;
            else if (porcentaje == 16)
                Radio2.IsChecked = true;
            else
                Radio3.IsChecked = true;
        }
        private void Radio()
        {
            if (Radio1.IsChecked == true)
                porcentaje = 0;
            else if (Radio2.IsChecked == true)
                porcentaje = 16;
            else
                porcentaje = 18;
            producto.PorcentajeITBIS = porcentaje;
        }

        private void InitializeComboBox()
        {
            MarcaComboBox.ItemsSource = MarcasBLL.GetList(c => true);
            MarcaComboBox.SelectedValuePath = "MarcaId";
            MarcaComboBox.DisplayMemberPath = "Nombres";
        }
        public void GuardarBoton_Click(object render, RoutedEventArgs e)
        {
            Radio();
            if (!Validar())
                return;
            producto.UsuarioId = user;
            bool paso = (bool)ProductosBLL.Save(producto);
            if (paso)
            {
                if (modo == 'C')
                {
                    MessageBox.Show("Guardado con Exito", "Exito",
                        MessageBoxButton.OK);
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Guardado con Exito", "Exito", 
                        MessageBoxButton.OK);
                }
            }
            else
                MessageBox.Show("Error al guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarBoton_Click(object render, RoutedEventArgs e)
        {
            Contexto context = new Contexto();

            Productos found = ProductosBLL.Search(Convert.ToInt32(ProductoIdTextBox.Text));

            if (found != null)
            {
                this.producto = found;
                inicarRadio();
            }
            else
            {
                Limpiar();
                MessageBox.Show("No encontrado", "Mensaje", 
                    MessageBoxButton.OK);
               
            }

            this.DataContext = this.producto;
        }

        private bool Validar()
        {
            bool esValido = true;

            if (DescripcionTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Debe colocar el nombre del producto",
                    "Mensaje", MessageBoxButton.OK);

            }
            else if (CantidadTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Debe colocar el Stock del producto",
                    "Mensaje", MessageBoxButton.OK);

            }
            else if (PrecioTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Debe colocar el Precio del producto",
                    "Mensaje", MessageBoxButton.OK);

            }
            else if (MarcaComboBox.SelectedItem == null)
            {
                esValido = false;
                MessageBox.Show("Debe colocar la marca del producto",
                    "Mensaje", MessageBoxButton.OK);
            }
            return esValido;
        }
        private void EliminarBoton_Click(object render, RoutedEventArgs e)
        {
            Productos producto = this.producto;

            if (producto == null)
            {
                MessageBox.Show("Primero seleccione un producto",
                    "Mensaje", MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar " +
                producto.Descripcion + "?", "Productos", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (ProductosBLL.Delete(producto.UsuarioId))
                    MessageBox.Show("Producto eliminado",
                        "Productos");
            }
        }

        private void NuevoBoton_Click(object render, RoutedEventArgs e)
        {
            Limpiar();
        }
        public Productos GetProducto()
        {
            return producto;
        }
        private bool ValidarMargen()
        {
            bool esValido = true;
            int m = Convert.ToInt32(MargenTextBox.Text);
            if (m > 100 || m < 0)
            {
                esValido = false;
                MessageBox.Show("El Margen debe estar entre 1 y 100",
                    "Mensaje", MessageBoxButton.OK);

            }
            return esValido;
        }
        private void MargenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!ValidarMargen())
                return;
            PrecioTextBox.Text = (Convert.ToSingle(CostoTextBox.Text) +
                Convert.ToSingle(CostoTextBox.Text) * Convert.ToSingle(MargenTextBox.Text)/100).ToString();
        }
    }
}
