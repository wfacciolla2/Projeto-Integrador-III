using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Projeto_Integrador_III.Entities;
using System.Linq;

namespace Projeto_Integrador_III.Repositories
{
    public class ContaSqlRepository : IContaRepository
    {
        private static Dictionary<Guid, Conta> contas = new Dictionary<Guid, Conta>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Conta{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Email = "facciollacorp@gmail.com", Usuario = "wfacciolla", Senha = "segredo123"} },
        };

        public Task<List<Conta>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(contas.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Conta> Obter(Guid id)
        {
            if (!contas.ContainsKey(id))
                return null;

            return Task.FromResult(contas[id]);
        }

        public Task<List<Conta>> Obter(string usuario, string email)
        {
            return Task.FromResult(contas.Values.Where(conta => conta.Usuario.Equals(usuario) && conta.Email.Equals(email)).ToList());
        }

        public Task<List<Conta>> ObterSemLambda(string usuario, string email)
        {
            var retorno = new List<Conta>();

            foreach (var conta in contas.Values)
            {
                if (conta.Usuario.Equals(usuario) && conta.Email.Equals(email))
                    retorno.Add(conta);
            }
            return Task.FromResult(retorno);
        }

        public Task Inserir(Conta conta)
        {
            contas.Add(conta.Id, conta);
            return Task.CompletedTask;
        }

        public Task Atualizar(Conta conta)
        {
            contas[conta.Id] = conta;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            contas.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
