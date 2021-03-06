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
using RepuestoAntigua.UI.Registros;

namespace RepuestoAntigua.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cUsuarios.xaml
    /// </summary>

    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
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
            var listado = new List<Usuarios>();
            string criterio = CriterioTextBox.Text.Trim();

            if (CriterioTextBox.Text.Trim().Length > 0 || FiltroCombobox.SelectedIndex == 3)
            {
                switch (FiltroCombobox.SelectedIndex)
                {
                    case 0:
                        listado = UsuariosBLL.GetList();
                        break;
                    case 1:
                        listado = UsuariosBLL.GetList(p => p.UsuarioId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 2:
                        listado = UsuariosBLL.GetList(p => p.Usuario.ToLower().Contains(criterio.ToLower()));
                        break;
                    case 3:
                        DateTime hasta = (DateTime)HastaPicker.SelectedDate;
                        DateTime desde = (DateTime)DesdePicker.SelectedDate;
                        listado = UsuariosBLL.GetList(p => p.Fecha >= desde && p.Fecha <= hasta);
                        break;
                }
            }
            else
            {
                listado = UsuariosBLL.GetList();
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
            Usuarios usuario =  GetSelectedUsuario();

            if (usuario == null)
            {
                MessageBox.Show("Primero seleccione un Usuario", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rUsuarios(usuario).Show();
            Inicializar();
        }

        private Usuarios GetSelectedUsuario()
        {
            object Usuarios = DatosDataGrid.SelectedItem;

            if (Usuarios != null)
                return (Usuarios)Usuarios;
            else
                return null;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            new rUsuarios().Show();
            Inicializar();
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Usuarios Usuario = GetSelectedUsuario();
            if(Usuario.UsuarioId == 1)
            {
                MessageBox.Show("No puede Eliminar el Admin", "Error",
                    MessageBoxButton.OK);
                return;
            }
            if (Usuario == null)
            {
                MessageBox.Show("Primero seleccione un Usuario", "Error",
                    MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a " + Usuario.Usuario + "?",
                "Usuarios", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (UsuariosBLL.Delete(Usuario.UsuarioId))
                {
                    Inicializar();
                    MessageBox.Show("Usuario eliminado", "Exito");
                }
            }

            Inicializar();
        }

  
    }
}
