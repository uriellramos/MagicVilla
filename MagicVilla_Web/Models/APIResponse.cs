﻿using System.Net;

namespace MagicVilla_Web.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode  { get; set; }
        public bool IsExitoso { get; set; } = true;
        public List<string> ErroMessages { get; set; }
        public object Resultado { get; set; }
    }
}
