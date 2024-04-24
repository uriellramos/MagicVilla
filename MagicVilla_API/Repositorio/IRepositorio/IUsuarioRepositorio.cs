using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        bool IsUSuarioUnico(string userName);
        Task<LoginResponseDto> Login(LoginRequestDTO loginRequestDTO);
        Task<UsuarioDTO> Registrar(RegistroRequestDTO registroRequestDTO);

    }
}
