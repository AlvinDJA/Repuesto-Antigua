using BLL;
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

namespace RepuestoAntigua.UI.Consultas
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuarios>();
            string criterio = CriterioTextBox.Text.Trim();

            if (CriterioTextBox.Text.Trim().Length > 0)
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
                        listado = UsuariosBLL.GetList(p => p.Nombres.ToLower().Contains(criterio.ToLower()) );
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
    }
}
