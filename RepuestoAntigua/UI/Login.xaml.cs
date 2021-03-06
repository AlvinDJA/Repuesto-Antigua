﻿using BLL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace RepuestoAntigua.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            NombreUsuarioTextBox.Focus();
        }
        private void AccederBoton_Click(object sender, RoutedEventArgs e)
        {
            Acceder();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Acceder();
            }
        }
        private void Acceder()
        {
            string nombre = NombreUsuarioTextBox.Text.Trim();
            string pass;
            if (NombreUsuarioTextBox.Text=="admin")
                 pass = PasswordBox.Password;
            else
                pass = System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(PasswordBox.Password));
            
            
            if (UsuariosBLL.Validar(nombre, pass))
            {
                ClaveLabel.Visibility = Visibility.Hidden;
                var usuario = UsuariosBLL.Buscar(nombre, pass);
                if(usuario != null)
                    new MainWindow(usuario.UsuarioId).Show();
                this.Close();
            }
            else
            {
                ClaveLabel.Visibility = Visibility.Visible;
                NombreUsuarioTextBox.Clear();
                PasswordBox.Clear();
            }
        }
    }
}
