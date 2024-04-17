using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class UsuarioService :BaseService,  IUsuarioService
    {
        public readonly IHttpClientFactory _httpCient;
        private string _villaUrl;
        public UsuarioService(IHttpClientFactory httpCient,IConfiguration configuration): base(httpCient)
        {
            _httpCient = httpCient;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Login<T>(LoginRequestDto dto)
        {
            return SendAsync<T>(new APIRequest() { 
                APITipo= DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl+ "/api/v1/usuario/login",
            });
        }

        public Task<T> Registro<T>(RegistroRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl + "/api/v1/usuario/registrar",
            });
        }
    }
}
