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
        private Marcas marcas;
        public rMarcas(int usuario)
        {
            InitializeComponent();
            this.marcas = new Marcas();
            this.DataContext = marcas;
        }

        public rMarcas(Marcas marcas)
        {
            InitializeComponent();
            this.marcas = marcas;
            this.DataContext = marcas;
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
            else if (NombresTextBox.Text.Length < 3)
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
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
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
