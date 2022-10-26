using Projeto_Integrador_III.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Projeto_Integrador_III.ViewModel;
using System.Linq;
using Projeto_Integrador_III.InputModel;
using Projeto_Integrador_III.Entities;
using Projeto_Integrador_III.Exceptions;

namespace Projeto_Integrador_III.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }
        public async Task<List<ContaViewModel>> Obter(int pagina, int quantidade)
        {
            var contas = await _contaRepository.Obter(pagina, quantidade);
            return contas.Select(conta => new ContaViewModel
            {
                Id = conta.Id,
                Usuario = conta.Usuario,
                Senha = conta.Senha,
                Email = conta.Email,

            }).ToList();
        }
        public async Task<ContaViewModel> Obter(Guid id)
        {
            var conta = await _contaRepository.Obter(id);

            if (conta == null)
                return null;

            return new ContaViewModel
            {
                Id = conta.Id,
                Usuario = conta.Usuario,
                Senha = conta.Senha,
                Email = conta.Email,
            };
        }
        public async Task<ContaViewModel> Inserir(ContaInputModel conta)
        {
            var entidadeConta = await _contaRepository.Obter(conta.Email, conta.Usuario);

            if (entidadeConta.Count > 0)
                throw new ContaJaCadastradaException();

            var contaInsert = new Conta
            {
                Id = Guid.NewGuid(),
                Usuario = conta.Usuario,
                Senha = conta.Senha,
                Email = conta.Email,

            };

            await _contaRepository.Inserir(contaInsert);

            return new ContaViewModel
            {
                Id = contaInsert.Id,
                Usuario = contaInsert.Usuario,
                Senha = contaInsert.Senha,
                Email = contaInsert.Email,
            };

        }

        public async Task Atualizar(Guid id, ContaInputModel conta)
        {
            var entidadeConta = await _contaRepository.Obter(id);

            if (entidadeConta == null)
                throw new ContaNaoCadastradaException();

            entidadeConta.Usuario = conta.Usuario;
            entidadeConta.Senha = conta.Senha;
            entidadeConta.Email = conta.Email;

            await _contaRepository.Atualizar(entidadeConta);

        }

        public async Task Atualizar(Guid id, string senha)
        {
            var entidadeConta = await _contaRepository.Obter(id);

            if (entidadeConta == null)
                throw new ContaNaoCadastradaException();

            entidadeConta.Senha = senha;

            await _contaRepository.Atualizar(entidadeConta);
        }

        public async Task Remover(Guid id)
        {
            var conta = _contaRepository.Obter(id);

            if (conta == null)
                throw new ContaNaoCadastradaException();

            await _contaRepository.Remover(id);
        }

        public void Dispose()
        {
            _contaRepository.Dispose();
        }
    }
}
