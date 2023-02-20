using System;
using System.Linq;
using System.Windows;


namespace _21._106_Kulakov_authorization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //init ValidationUseCase with private setter
        private readonly UseCases.ValidationUseCase validator;
        public MainWindow()
        {
            InitializeComponent();

            //Inject validateionUseCase
            validator = new UseCases.ValidationUseCase();
        }

        //Funcition for requset in databese for auth user
        private void auth(String userName, String password)
        {
            using (var context = new Dadabase.Entities() )
            {
                var user = context.User.FirstOrDefault(u => u.sername == userName && u.password == password);
                var role = context.Role.FirstOrDefault(r => r.id == user.role);
        
                resultMessage(user, role);
            }
        }
        //Message box for show auth result
        private void resultMessage(Dadabase.User user, Dadabase.Role role)
        {
            if (user != null)
            {
                MessageBox.Show(LOGIN_SUCCES 
                    + " \n Your surname is "
                    + user.name + "\n Your role is " 
                    + role.role_name, "Auth");
            }else
            {
                MessageBox.Show(LOGIN_FAILED, "Auth");
            }
        }
        private void btSignIn_Click(object sender, RoutedEventArgs e)
        {
            var login = edLOgin.Text.Trim();
            var password = edPassword.Password.Trim();

            //get auth useCase
            var validationState = validator.ValidateData(login, password);

            if (validationState.IsValid)
            {
               auth(login, password);
            }
            else
            {
                // Data is not valid, show error message
                MessageBox.Show(string.Join("\n", validationState.Errors), "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Const info string for message box
        public static string LOGIN_SUCCES = "Login Succes";
        public static string LOGIN_FAILED = "Auth failed";
        public static string AUTH_MESSAGE_TITLE = "Auth";

    }
}
