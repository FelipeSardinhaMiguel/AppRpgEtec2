using AppRpgEtec.Models;
using AppRpgEtec.Services.Armas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppRpgEtec.ViewModels.Armas
{
    public class ListagemArmaViewModel : BaseViewModel
    {
        private ArmasServices pServices;
        public ObservableCollection<Arma> Armas { get; set; }

        //pega as armas do usuario e coloca-as todas em um método para exibilas
        public ListagemArmaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pServices = new ArmasServices(token);
            Armas = new ObservableCollection<Arma>();
            _ = ObterArmas();
        }

        //obter todas as armas em lista
        public async Task ObterArmas()
        {
            try
            {
                Armas = await pServices.GetArmasAsync();
                OnPropertyChanged(nameof(Armas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
