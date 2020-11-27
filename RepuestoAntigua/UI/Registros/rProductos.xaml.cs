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
        private Productos producto;
        char modo;
        public rProductos(int usuario)
        {
            InitializeComponent();
            InitializeComboBox();
            Limpiar();
        }
        public void Limpiar()
        {
            this.producto = new Productos();
            this.DataContext = producto;
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
        private void InitializeComboBox()
        {
            MarcaComboBox.ItemsSource = MarcasBLL.GetList(c => true);
            MarcaComboBox.SelectedValuePath = "MarcaId";
            MarcaComboBox.DisplayMemberPath = "Nombres";
        }
        public void GuardarBoton_Click(object render, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            bool paso = (bool)ProductosBLL.Save(producto);
            if (paso)
            {
                if (modo == 'C')
                {
                    MessageBox.Show("Guardado con Exito", "Exito",
                        MessageBoxButton.OK);
                    this.Close();
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
                this.producto = found;
            else
            {
                this.producto = new Productos();
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

    }
}
