using Projeto_Integrador_III.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_Integrador_III.Repositories
{
    public interface IContaRepository : IDisposable
    {
        Task<List<Conta>> Obter(int pagina, int quantidade);
        Task<Conta> Obter(Guid id);
        Task<List<Conta>> Obter(string Usuario, string Email);
        Task Inserir(Conta conta);
        Task Atualizar(Conta conta);
        Task Remover(Guid id);
    }
}
