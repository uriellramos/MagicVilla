﻿using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private APIResponse _response;
        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _response = new APIResponse();
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);
            if (loginResponse.Usuario == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErroMessages.Add("UserName o Password son incorrectos");
                return BadRequest(_response);
            }
            _response.IsExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Resultado = loginResponse;
            return Ok(_response);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            bool isUsuarioUnico = _usuarioRepo.IsUSuarioUnico(modelo.UserName);
            if (!isUsuarioUnico)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErroMessages.Add("Usuario ya existe");
                return BadRequest(_response);
            }
            var usuario = await _usuarioRepo.Registrar(modelo);
            if (usuario == null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErroMessages.Add("Error al registrar Usuario!");
                return BadRequest(_response);
            }

            _response.statusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            return Ok(_response);
        }

    }
}
