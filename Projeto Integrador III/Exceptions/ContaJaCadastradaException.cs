using System;

namespace Projeto_Integrador_III.Exceptions
{
    public class ContaJaCadastradaException : Exception
    {
        public ContaJaCadastradaException() : base("Conta já cadastrada") { }
    }
}
