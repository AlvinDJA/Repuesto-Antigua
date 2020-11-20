using BLL;
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
            string pass = PasswordBox.Password;

            if (UsuariosBLL.Validar(nombre, pass))
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                ValidacionIncorrectaLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
