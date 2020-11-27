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
    /// Interaction logic for rCompras.xaml
    /// </summary>
    public partial class rCompras : Window
    {
        private Compras compra = new Compras();
        public rCompras(int usuario)
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
        }

        private void IniciarCombobox()
        {
            Limpiar();
            ProductosComboBox.ItemsSource = ProductosBLL.GetList(c => true);
            ProductosComboBox.SelectedValuePath = "ProductoId";
            ProductosComboBox.DisplayMemberPath = "Descripcion";

            ProveedoresComboBox.ItemsSource = ProveedoresBLL.GetList(c => true);
            ProveedoresComboBox.SelectedValuePath = "ProveedorId";
            ProveedoresComboBox.DisplayMemberPath = "Nombres";

        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = compra;
        }
        private void Limpiar()
        {
            this.compra = new Compras();
            this.compra.Fecha = DateTime.Now;
            this.DataContext = compra;
            TiempoTotalTextBox.Clear();
        }

        private bool ValidarDetalle()
        {
           //if (!CostoTextBox.Text.(char.IsNumber))
           // {
           //     MessageBox.Show("Ingrese un valor numerico  válido e intente de nuevo", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
           //     return false;
           // }

           // if (ValorTextBox.Text.Length == 0)
           // {
           //     MessageBox.Show("Ingrese un valor e intente de nuevo", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
           //     return false;
           // }

           // if (PrestamoComboBox.SelectedIndex < 0)
           // {
           //     MessageBox.Show("Seleccione un préstamo e intente de nuevo", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
           //     return false;
           // }

            return true;
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
            Compras encontrado = ComprasBLL.Search(compra.CompraId);

            if (encontrado != null)
            {
                compra = encontrado;
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
            Compras existe = ComprasBLL.Search(compra.CompraId);

            if (existe == null)
            {
                MessageBox.Show("No existe el proyecto en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                ComprasBLL.Delete(compra.CompraId);
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

            if (compra.CompraId == 0)
                paso = ComprasBLL.Save(compra);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = ComprasBLL.Save(compra);
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
            Compras esValido = ComprasBLL.Search(compra.CompraId);
            return (esValido != null);
        }
        private void Remover_Click(object sender, RoutedEventArgs e)
        {
            if (DatosDataGrid.Items.Count >= 1 && DatosDataGrid.SelectedIndex <= DatosDataGrid.Items.Count - 1)
            {
                ComprasDetalle m = (ComprasDetalle)DatosDataGrid.SelectedValue;
                compra.TotalCompra -= m.Costo * m.Cantidad;
                compra.Detalle.RemoveAt(DatosDataGrid.SelectedIndex);
                Cargar();
            }
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarDetalle())
            {
                return;
            }
            ComprasDetalle detalle = new ComprasDetalle(
                Convert.ToInt32(IdTextBox.Text),
                Convert.ToInt32(ProductosComboBox.SelectedValue.ToString()),
                Convert.ToSingle(CostoTextBox.Text),
                Convert.ToSingle(CantidadTextBox.Text)
            );

            compra.Detalle.Add(detalle);
            compra.TotalCompra += detalle.Costo * detalle.Cantidad;

            Cargar();

            ProveedoresComboBox.SelectedIndex = -1;
            CostoTextBox.Clear();
            CantidadTextBox.Clear();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

