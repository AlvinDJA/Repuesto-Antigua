using RepuestoAntigua.UI.Registros;
using RepuestoAntigua.UI.Consultas;
using System.Windows;

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
            new cUsuarios().Show();
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

        private void rClientesItem_Click(object sender, RoutedEventArgs e)
        {
            new rClientes(usuario).Show();
        }

        private void cProductosItem_Click(object sender, RoutedEventArgs e)
        {
            new cProductos(usuario).Show();
        }
        private void cFacturasItem_Click(object sender, RoutedEventArgs e)
        {
            new cFacturas(usuario).Show();
        }
        private void cComprasItem_Click(object sender, RoutedEventArgs e)
        {
        }
        private void cProveedoresItem_Click(object sender, RoutedEventArgs e)
        {
            new cProveedores(usuario).Show();
        }
        private void cClientesItem_Click(object sender, RoutedEventArgs e)
        {
            new cClientes(usuario).Show();
        }

        
        
    }
}
