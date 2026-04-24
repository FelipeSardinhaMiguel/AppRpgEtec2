using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using AppRpgEtec.Views.Personagens;
using AppRpgEtec.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService uService;

        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }

        public UsuarioViewModel()
        {
            uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
        }

        //atributos que são chamadas na view
        #region AtributosPropriedades
        private string login = string.Empty;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.Password = Password; //<-- Password (propriedade da model de usuario) vai para Password (atributo que é chamado na view)

                Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {uAutenticado.Username}.";

                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);
                    Preferences.Set("UsuarioId", uAutenticado.Token);

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new ListagemView();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }

            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.Password = Password;

                Usuario uRegistrado = await uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0)
                {
                    string mensagem = $"Usuario Id {uRegistrado.Id} registrado com sucesso.";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    await Application.Current.MainPage
                        .Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage
                    .Navigation.PushAsync(new CadastroView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
