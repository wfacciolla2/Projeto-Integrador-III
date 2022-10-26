using System;

namespace Projeto_Integrador_III.Exceptions
{
    public class ContaNaoCadastradaException : Exception
    {
        public ContaNaoCadastradaException() : base("Conta não cadastrada") { }
    }
}
