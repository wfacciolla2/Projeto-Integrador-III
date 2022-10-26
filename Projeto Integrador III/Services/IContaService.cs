using Projeto_Integrador_III.InputModel;
using Projeto_Integrador_III.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_Integrador_III.Services
{
    public interface IContaService
    {
        Task<List<ContaViewModel>> Obter(int pagina, int quantidade);
        Task<ContaViewModel> Obter(Guid id);
        Task<ContaViewModel> Inserir(ContaInputModel conta);
        Task Atualizar(Guid id, ContaInputModel conta);
        Task Atualizar(Guid id, string senha);
        Task Remover(Guid id);
    }
}
