using BLL;
using Entidades;
using System.Windows;

namespace RepuestoAntigua.UI.Registros
{
    /// <summary>
    /// Interaction logic for rMarcas.xaml
    /// </summary>
    public partial class rMarcas : Window
    {
        private bool edit;
        private Marcas marcas;

        int user;
        public rMarcas(int usuario)
        {
            InitializeComponent();
            this.marcas = new Marcas();
            this.DataContext = marcas;
            edit = false;
            user = usuario;
        }

        public rMarcas(Marcas marcas, int usuario)
        {
            InitializeComponent();
            this.marcas = marcas;
            this.DataContext = marcas;
            edit = true;
            user = usuario;
        }
        public void Limpiar()
        {
            this.marcas = new Marcas();
            this.DataContext = marcas;
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
            else if (MarcasBLL.Existe(NombresTextBox.Text) && edit == false)
            {
                esValido = false;
                MessageBox.Show("Ya existe esta marca", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (NombresTextBox.Text.Length < 2)
            {
                esValido = false;
                MessageBox.Show("El nombre es muy corto", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            edit = false;
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            marcas.UsuarioId = user;
            var paso = MarcasBLL.Save(marcas);
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
            Marcas encontrado = MarcasBLL.Buscar(marcas.MarcaId);
            edit = true;
            if (encontrado != null)
            {
                marcas = encontrado;
                this.DataContext = marcas;
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
            Marcas existe = MarcasBLL.Buscar(marcas.MarcaId);
            if (marcas.MarcaId == 1)
            {
                MessageBox.Show("No puede eliminar el Admin",
                    "Mensaje", MessageBoxButton.OK);
                return;
            }
            if (marcas == null)
            {
                MessageBox.Show("Primero seleccione un Usuario",
                    "Error", MessageBoxButton.OK);
                return;
            }
            else
            {
                MessageBoxResult opcion =
                    MessageBox.Show("Estas seguro de que desear eliminar a " + marcas.Nombres + "?",
                    "Marcas", MessageBoxButton.YesNo);
                if (opcion.Equals(MessageBoxResult.Yes))
                {
                    if (MarcasBLL.Delete(marcas.MarcaId))
                    {
                        Limpiar();
                        MarcasBLL.Eliminar(marcas.MarcaId);
                        MessageBox.Show("Ha sido eliminado exitosamente", "Exito",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}
