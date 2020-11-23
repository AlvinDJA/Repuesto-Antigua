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
using Entidades;
using BLL;
using DAL;

namespace RepuestoAntigua.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        private Usuarios usuarios;
        public rUsuarios()
        {
            InitializeComponent();
            this.usuarios = new Usuarios();
            this.DataContext = usuarios;
        }

        public rUsuarios(Usuarios usuario)
        {
            InitializeComponent();
            this.usuarios = usuario;
            this.DataContext = usuarios;
            

        }
        public void Limpiar()
        {
            this.usuarios = new Usuarios();
            this.DataContext = usuarios;
            Clave1TextBox.Clear();
            Clave2TextBox.Clear();
            UsuarioLabel.Visibility = Visibility.Hidden;
            ClaveLabel.Visibility = Visibility.Hidden;
        }
        private void ValidarNombre()
        {
            if (NombresTextBox.Text.Length < 3)
            {
                UsuarioLabel.Visibility = Visibility.Visible;
            }
           else
            {
                UsuarioLabel.Visibility = Visibility.Hidden;
            }
        }
        private void ValidarClave()
        {
            if (Clave1TextBox.Password.Length < 3)
                ClaveLabel.Visibility = Visibility.Visible;
            else
                ClaveLabel.Visibility = Visibility.Hidden;
        }
        private void ValidarClave2()
        {
             if (Clave2TextBox.Password != Clave1TextBox.Password)
                ClaveLabel2.Visibility = Visibility.Visible;
            else
                ClaveLabel2.Visibility = Visibility.Hidden;
        }

        private bool Validar()
        {
            bool esValido = true;
            if (NombresTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ingrese los nombres", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (NombresTextBox.Text.Length < 3)
            {
                esValido = false;
                MessageBox.Show("El nombre es muy corto", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Clave1TextBox.Password.Length < 3)
            {
                esValido = false;
                MessageBox.Show("La clave es muy corta", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Clave2TextBox.Password != Clave1TextBox.Password)
            {
                esValido = false;
                MessageBox.Show("Las claves deben ser iguales", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) 
                return;
            string clave = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(Clave1TextBox.Password));
            usuarios.Clave = clave;
                var paso = UsuariosBLL.Save(usuarios);
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado con Exito", "Exito", 
                    MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Ha ocurrido un error", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
       
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Usuarios encontrado = UsuariosBLL.Buscar(usuarios.UsuarioId);

            if (encontrado != null)
            {
                usuarios = encontrado;
                this.DataContext = usuarios;
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
            Usuarios existe = UsuariosBLL.Buscar(usuarios.UsuarioId);
            if (usuarios.UsuarioId == 1)
            {
                MessageBox.Show("No puede eliminar el Admin",
                    "Mensaje", MessageBoxButton.OK);
                return;
            }
            if (usuarios == null)
            {
                MessageBox.Show("Primero seleccione un Usuario",
                    "Error", MessageBoxButton.OK);
                return;
            }
            else
            {
                MessageBoxResult opcion = 
                    MessageBox.Show("Estas seguro de que desear eliminar a " + usuarios.Nombres + "?",
                    "Usuarios", MessageBoxButton.YesNo);
                if (opcion.Equals(MessageBoxResult.Yes))
                {
                    if (UsuariosBLL.Delete(usuarios.UsuarioId))
                    {
                        Limpiar();
                        UsuariosBLL.Eliminar(usuarios.UsuarioId);
                        MessageBox.Show("Ha sido eliminado exitosamente", "Exito",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
        private void NombresTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarNombre();
        }
        private void Clave1TextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidarClave();
        }
        private void Clave2TextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidarClave2();
        }
    }
}