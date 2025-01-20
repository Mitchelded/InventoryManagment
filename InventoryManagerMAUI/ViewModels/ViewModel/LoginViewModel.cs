using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InventoryManagerMAUI.Commands;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _username;
    private string _password;
    private bool _isBusy;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotBusy)); // Обновляем зависимое свойство
        }
    }

    public bool IsNotBusy => !IsBusy;

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(async () => await LoginAsync(), () => IsNotBusy);
    }

    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Введите имя пользователя и пароль.", "ОК");
            return;
        }

        IsBusy = true;

        try
        {
            await using InventoryManagmentEntities db = new();
            // Пример проверки учетных данных
            string hashedPassword = await HashHelper.ComputeMD5Hash(Password);
            User? user = db.Users.Include(user => user.UserRoles).ThenInclude(userRole => userRole.Role)
                .First(x => x.Username == Username && x.Password.ToUpper() == hashedPassword.ToUpper());
            // Сохранение текущего пользователя
            if (user != null)
            {
                Preferences.Set("CurrentUsername", user.UserID.ToString());
                Preferences.Set("CurrentRole", user.UserRoles.First().Role.Name);
                // Обновляем боковое меню
                UpdateMenuForRole(user.UserRoles.First().Role.Name);
                // Переход к главной странице
                if (Application.Current != null) Application.Current.MainPage = new AppShell();
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateMenuForRole(string role)
    {
        try
        {
            if (Application.Current?.MainPage is AppShell shell)
            {
                shell.Items.Clear(); // Очистка текущих элементов меню
                var menuItems = RoleBasedMenuService.GetMenuForRole(role);
                foreach (var menuItem in menuItems)
                {
                    shell.Items.Add(menuItem); // Добавляем элементы меню для текущей роли
                }
            }
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs
            Application.Current.MainPage.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}