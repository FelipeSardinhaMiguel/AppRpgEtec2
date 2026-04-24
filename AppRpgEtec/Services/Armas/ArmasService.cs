using AppRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppRpgEtec.Services.Armas
{
    public class ArmasServices
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://luizsilva12.somee.com/RpgApi/Armas";

        private string _token;

        public ArmasServices(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<int> PostArmaAsync(Arma p)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, p, _token);
        }

        //método para buscar todas as armas. (get all)
        public async Task<ObservableCollection<Arma>> GetArmasAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Arma> listaArmas = await _request.GetAsync<ObservableCollection<Arma>>(apiUrlBase + urlComplementar, _token);
            return listaArmas;
        }
    }
}
