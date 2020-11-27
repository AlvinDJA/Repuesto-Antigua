﻿using RepuestoAntigua.UI.Registros;
using RepuestoAntigua.UI.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepuestoAntigua
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int usuario;
        public MainWindow(int usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void rUsuariosItem_Click(object sender, RoutedEventArgs e)
        {
            new rUsuarios(usuario).Show();
        }

        private void cUsuariosItem_Click(object sender, RoutedEventArgs e)
        {
            new cUsuarios(usuario).Show();
        }

        private void cMarcasItem_Click(object sender, RoutedEventArgs e)
        {
            new cMarcas(usuario).Show();
        }

        private void rMarcasItem_Click(object sender, RoutedEventArgs e)
        {
            new rMarcas(usuario).Show();
        }


        private void rProveedoresItem_Click(object sender, RoutedEventArgs e)
        {
            new rProveedores(usuario).Show();
        }

        private void rProductosItem_Click(object sender, RoutedEventArgs e)
        {
            new rProductos(usuario).Show();
        }

        private void rComprasItem_Click(object sender, RoutedEventArgs e)
        {
            new rCompras(usuario).Show();
        }

        private void rFacturasItem_Click(object sender, RoutedEventArgs e)
        {
            new rFacturas(usuario).Show();
        }

        private void cProductosItem_Click(object sender, RoutedEventArgs e)
        {
            new cProductos(usuario).Show();
        }

        private void cFacturasItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cComprasItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cProveedoresItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
