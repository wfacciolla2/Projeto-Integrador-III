using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Projeto_Integrador_III.Entities;

namespace Projeto_Integrador_III.Repositories
{
        public class ContaSqlServerRepository : IContaRepository
        {
            private readonly SqlConnection sqlConnection;

            public ContaSqlServerRepository(IConfiguration configuration)

            {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
            }

            public async Task<List<Conta>> Obter(int pagina, int quantidade)
            {

                var contas = new List<Conta>();

                var comando = $"select * from Contas order by Id offset 0 rows fetch next 5 rows only";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new(comando, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
                {
                    contas.Add(new Conta
                    {
                        Id = (Guid)sqlDataReader["Id"],
                        Usuario = (string)sqlDataReader["Usuario"],
                        Senha = (string)sqlDataReader["Senha"],
                        Email = (string)sqlDataReader["Email"],
                    });

                    await sqlConnection.CloseAsync();


                }
                return contas;
            }

            public async Task<Conta> Obter(Guid id)
            {
                Conta conta = null;

                var comando = $"select * from Contas where Id = '{id}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    conta = new Conta
                    {
                        Id = (Guid)sqlDataReader["Id"],
                        Usuario = (string)sqlDataReader["Usuario"],
                        Senha = (string)sqlDataReader["Senha"],
                        Email = (string)sqlDataReader["Email"],
                    };
                }

                await sqlConnection.CloseAsync();

                return conta;
            }

            public async Task<List<Conta>> Obter(string usuario, string email)
            {
                var contas = new List<Conta>();

                var comando = $"select * from Contas where Usuario = '{usuario}' and Email = '{email}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    contas.Add(new Conta
                    {
                        Id = (Guid)sqlDataReader["Id"],
                        Usuario = (string)sqlDataReader["Usuario"],
                        Senha = (string)sqlDataReader["Senha"],
                        Email = (string)sqlDataReader["Email"],
                    });
                }

                await sqlConnection.CloseAsync();

                return contas;
            }

            public async Task Inserir(Conta conta)
            {
                var comando = $"insert Contas (Id, Usuario, Senha, Email) values (" +
                    $"'{conta.Id}','{conta.Usuario}','{conta.Senha}','{conta.Email}')";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                await sqlConnection.CloseAsync();
            }

            public async Task Atualizar(Conta conta)
            {
                var comando = $"update Contas set (Usuario, Senha, Email) values '{conta.Usuario}','{conta.Senha}','{conta.Email}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                sqlCommand.BeginExecuteNonQuery();
                await sqlConnection.CloseAsync();
            }

            public async Task Remover(Guid id)
            {
                var comando = $"delete from Contas where Id = '{id}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                sqlCommand.BeginExecuteNonQuery();
                await sqlConnection.CloseAsync();
            }

            public void Dispose()
            {
                sqlConnection?.Close();
                sqlConnection?.Dispose();
            }
        }
}


