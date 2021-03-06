﻿using BLL;
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
    /// Interaction logic for cClientes.xaml
    /// </summary>
    public partial class cClientes : Window
    {
        private int user;
        public cClientes(int usuario)
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
                        listado = ClientesBLL.GetList("", "");
                        break;
                    case 1:
                        listado = ClientesBLL.GetList("ClienteId", criterio);
                        break;
                    case 2:
                        listado = ClientesBLL.GetList("Nombres", criterio);
                        break;
                    case 3:
                        listado = ClientesBLL.GetList("Apellidos", criterio);
                        break;
                    case 4:
                        listado = ClientesBLL.GetList("Celular", criterio);
                        break;
                    case 5:
                        listado = ClientesBLL.GetList("Cedula", criterio);
                        break;
                    case 6:
                        listado = ClientesBLL.GetList("Telefono", criterio);
                        break;
                    case 7:
                        listado = ClientesBLL.GetList("Usuario", criterio);
                        break;



                }
            }
            else
            {
                listado = ClientesBLL.GetList("","");
                
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
            Clientes cliente = GetSelectedCliente();

            if (cliente == null)
            {
                MessageBox.Show("Primero seleccione una Cliente", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rClientes(cliente).ShowDialog(); ;
            Inicializar();
        }

        private Clientes GetSelectedCliente()
        {
            object Clientes = DatosDataGrid.SelectedItem;

            if (Clientes != null)
                return ClientesBLL.Search(
                   Convert.ToInt32(
                   Clientes.GetType().
                   GetProperty("ClienteId").
                   GetValue(Clientes).
                   ToString()));
            else
                return null;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            new rClientes(user).Show();
            Inicializar();
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Clientes cliente = GetSelectedCliente();
            if (cliente == null)
            {
                MessageBox.Show("Primero seleccione una Cliente", "Error",
                    MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a la marca " + cliente.Nombres + "?",
                "Clientes", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (ClientesBLL.Delete(cliente.ClienteId))
                {
                    Inicializar();
                    MessageBox.Show("Cliente eliminada", "Exito");
                }
            }
            Inicializar();
        }
    }
}
