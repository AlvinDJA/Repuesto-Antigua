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
        public void Limpiar()
        {
            this.usuarios = new Usuarios();
            this.DataContext = usuarios;
            Clave1TextBox.Clear();
            Clave2TextBox.Clear();
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
            else if (Clave1TextBox.Text.Length < 3)///Editar
            {
                esValido = false;
                MessageBox.Show("Ingresse la clave", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Clave2TextBox.Text != Clave1TextBox.Text)///Editar
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

            if (existe == null)
            {
                MessageBox.Show("No existe el usuario", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                UsuariosBLL.Eliminar(usuarios.UsuarioId);
                MessageBox.Show("Ha sido eliminado exitosamente", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
    }
}
