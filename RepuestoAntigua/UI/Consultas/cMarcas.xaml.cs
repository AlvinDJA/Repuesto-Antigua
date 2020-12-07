using BLL;
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
    /// Interaction logic for cMarcas.xaml
    /// </summary>
    public partial class cMarcas : Window
    {
        private int user;
        public cMarcas(int usuario)
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
                        listado = MarcasBLL.GetList("", "");
                        break;
                    case 1:
                        listado = MarcasBLL.GetList("MarcaId", criterio);
                        break;
                    case 2:
                        listado = MarcasBLL.GetList("Nombres", criterio);
                        break;
                    case 3:
                        listado = MarcasBLL.GetList("Usuario", criterio);
                        break;
                }
            }
            else
            {
                listado = MarcasBLL.GetList("","");
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
            Marcas marca = GetSelectedMarca();

            if (marca == null)
            {
                MessageBox.Show("Primero seleccione una Marca", "Mensaje",
                    MessageBoxButton.OK);
                return;
            }

            new rMarcas(marca,user).Show();
            Inicializar();
        }

        private Marcas GetSelectedMarca()
        {
            object Marcas = DatosDataGrid.SelectedItem;

            if (Marcas != null)
                return MarcasBLL.Search(
                  Convert.ToInt32(
                      Marcas.GetType().
                      GetProperty("MarcaId").
                      GetValue(Marcas).
                      ToString()));
            else
                return null;
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            new rMarcas(user).ShowDialog();
            Inicializar();
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            Marcas Marca = GetSelectedMarca();
            if (Marca == null)
            {
                MessageBox.Show("Primero seleccione una Marca", "Error",
                    MessageBoxButton.OK);
                return;
            }

            MessageBoxResult opcion = MessageBox.Show("Estas seguro de que desear eliminar a la marca " + Marca.Nombres + "?",
                "Marcas", MessageBoxButton.YesNo);

            if (opcion.Equals(MessageBoxResult.Yes))
            {
                if (MarcasBLL.Delete(Marca.MarcaId))
                {
                    Inicializar();
                    MessageBox.Show("Marca eliminada", "Exito");
                }
            }
            Inicializar();
        }
    }
}
