using System;
using System.Collections.Generic;
using System.Text;

namespace trofimon.Models
{
    public class Receitas
    {
        public string UserReceita { get; set; }
        public string NomeReceita { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacao { get; set; }
        public string Comentarios { get; set; }
        public string Imagem { get; set; }
        public bool Privacidade { get; set; }
    }
}
