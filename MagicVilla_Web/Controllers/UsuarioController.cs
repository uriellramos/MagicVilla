﻿using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto modelo)
        {
            var response = await _usuarioService.Login<APIResponse>(modelo);
            if (response != null && response.IsExitoso == true)
            {
                LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Resultado));
                HttpContext.Session.SetString(DS.SessionToken, loginResponse.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", response.ErroMessages.FirstOrDefault());
                return View(modelo);
            }
        }

        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroRequestDto modelo)
        {
            var response = await _usuarioService.Registro<APIResponse>(modelo);
            if (response != null && response.IsExitoso)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(DS.SessionToken, "");
            return RedirectToAction("Index","Home");
        }
        public IActionResult AccesoNEgado()
        {
            return View();
        }

    }
}
