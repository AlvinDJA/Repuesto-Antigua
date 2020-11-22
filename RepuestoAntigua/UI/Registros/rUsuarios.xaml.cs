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

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            var paso = UsuariosBLL.Save(usuarios);
            if (paso)
            {

                //Limpiar();
                MessageBox.Show("Guardado con Exito", "Exito!", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Error al guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
