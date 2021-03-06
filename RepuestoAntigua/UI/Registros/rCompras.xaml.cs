﻿using BLL;
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
        int user;
        private List<DetalleMostrar> detallemostrar;
        public rCompras(int usuario)
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
            user = usuario;
            detallemostrar = new List<DetalleMostrar>();
        }
        public rCompras(int usuario, Compras compra)
        {
            InitializeComponent();
            IniciarCombobox();
            Limpiar();
            this.compra = ComprasBLL.Search(compra.CompraId);
            this.DataContext = this.compra;
            detallemostrar = new List<DetalleMostrar>();
        }

        private void IniciarCombobox()
        {
            compra.Fecha = DateTime.Now;
            Limpiar();
            ProductosComboBox.ItemsSource = ProductosBLL.GetList();
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
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = detallemostrar;
        }
        private void Limpiar()
        {
            this.compra = new Compras();
            this.compra.Fecha = DateTime.Now;
            this.DataContext = compra;
            detallemostrar = new List<DetalleMostrar>();
            DatosDataGrid.ItemsSource = detallemostrar;
            TiempoTotalTextBox.Clear();
        }
        private void Actualizar(List<ComprasDetalle> detalle)
        {
            detallemostrar = new List<DetalleMostrar>();
            foreach (ComprasDetalle p in detalle)
                detallemostrar.Add(new DetalleMostrar(p));
        }
        private bool ValidarDetalle()
        {
            if (CostoTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un Costo e intente de nuevo", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (CantidadTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese una Cantidad e intente de nuevo", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (ProductosComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto e intente de nuevo", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!float.TryParse(CostoTextBox.Text, out _))
            {
                MessageBox.Show("Ingrese un Costo válido, que sea numerico", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
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

            if (ProveedoresComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un proveedor e intente de nuevo", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                esValido = false;
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
                MessageBox.Show("No existe la Compra en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar la compra  " + compra.CompraId + "?",
                "Compras", MessageBoxButton.YesNo);

                if (opcion.Equals(MessageBoxResult.Yes))
                {
                    ComprasBLL.Delete(compra.CompraId);
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
            compra.UsuarioId = user;
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
            ComprasDetalle compras = GetSelectedCompra();

            if ( compras == null)
            {
                MessageBox.Show("Primero seleccione un producto", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }
            if (DatosDataGrid.Items.Count >= 1 && DatosDataGrid.SelectedIndex <= DatosDataGrid.Items.Count - 1)
            {
                ComprasDetalle m = (ComprasDetalle)compra.Detalle[DatosDataGrid.SelectedIndex];
                float itbs = (float)(ProductosBLL.Search(Convert.ToInt32(m.ProductoId)).PorcentajeITBIS) / 100;
                compra.TotalCompra -= m.Costo * m.Cantidad;
                compra.TotalCompra -= m.Costo * m.Cantidad * itbs;
                compra.Itbis -= m.Costo * m.Cantidad * itbs;
                compra.Detalle.RemoveAt(DatosDataGrid.SelectedIndex);
                Actualizar(compra.Detalle);
                Cargar();
            }
        }

        private ComprasDetalle GetSelectedCompra()
        {
            if (DatosDataGrid.SelectedIndex == -1)
                return null;
            object Compras = compra.Detalle[DatosDataGrid.SelectedIndex];
            if (Compras != null)
                return (ComprasDetalle)Compras;
            else
                return null;
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
            float itbs = (float)(ProductosBLL.Search(Convert.ToInt32(ProductosComboBox.SelectedValue)).PorcentajeITBIS) / 100;
            compra.TotalCompra -= compra.Itbis;
            compra.Itbis += Convert.ToSingle(CostoTextBox.Text) * Convert.ToSingle(CantidadTextBox.Text) * itbs;
            compra.TotalCompra += Convert.ToSingle(CostoTextBox.Text) * Convert.ToSingle(CantidadTextBox.Text) + compra.Itbis;
            Actualizar(compra.Detalle);
            Cargar();
            CostoTextBox.Clear();
            CantidadTextBox.Clear();
        }
   
        private void ProductosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Productos found = ProductosBLL.Search(Convert.ToInt32(ProductosComboBox.SelectedValue));

            if (found != null)
                CostoTextBox.Text = found.Costo.ToString();
            else
                CostoTextBox.Text = "";
        }
    }
}

