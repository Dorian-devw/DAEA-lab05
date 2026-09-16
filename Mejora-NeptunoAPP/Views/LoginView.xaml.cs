using System.Windows;

namespace Mejora_NeptunoAPP.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Simple hardcoded authentication for the prototype
            if (txtUser.Text == "admin" && txtPass.Password == "admin")
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
