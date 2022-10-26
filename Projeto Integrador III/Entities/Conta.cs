using System;

namespace Projeto_Integrador_III.Entities
{
    public class Conta
    {
        public Guid Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}
