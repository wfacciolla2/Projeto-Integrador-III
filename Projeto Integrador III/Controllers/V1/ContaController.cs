using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_Integrador_III.Exceptions;
using Projeto_Integrador_III.InputModel;
using Projeto_Integrador_III.Services;
using Projeto_Integrador_III.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Integrador_III.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var contas = await _contaService.Obter(pagina, quantidade);

            if (contas.Count() == 0)
                return NoContent();

            return Ok(contas);
        }

        [HttpGet("{idConta:guid}")]
        public async Task<ActionResult<IEnumerable<ContaViewModel>>> Obter([FromRoute] Guid idConta)
        {
            var conta = await _contaService.Obter(idConta);

            if (conta == null)
                return NoContent();

            return Ok(conta);
        }

        [HttpPost]
        public async Task<ActionResult<ContaViewModel>> InserirConta([FromBody] ContaInputModel contaInputModel)
        {
            try
            {
                var conta = await _contaService.Inserir(contaInputModel);

                return Ok(conta);
            }
            catch (ContaJaCadastradaException ex)
            {
                return UnprocessableEntity("Já existe uma conta com este nome de Usuario");
            }
        }

        [HttpPut("{idConta:guid}")]
        public async Task<ActionResult> AtualizarConta([FromRoute] Guid idConta, ContaInputModel contaInputModel)
        {
            try
            {
                await _contaService.Atualizar(idConta, contaInputModel);
                return Ok();
            }
            catch (ContaNaoCadastradaException ex)
            {
                return NotFound("Não existe essa conta");
            }
        }

        [HttpPatch("{idConta:guid}/senha/{senha}")]
        public async Task<ActionResult> AtualizarConta([FromRoute] Guid idConta, [FromRoute] string senha)
        {
            try
            {
                await _contaService.Atualizar(idConta, senha);
                return Ok();
            }
            catch (ContaNaoCadastradaException ex)
            {
                return NotFound("Essa conta não existe");
            }
        }

        [HttpDelete("{idConta:guid}")]
        public async Task<ActionResult> ApagarConta([FromRoute] Guid idConta)
        {
            try
            {
                await _contaService.Remover(idConta);
                return Ok();
            }
            catch (ContaNaoCadastradaException ex)
            {
                return NotFound("Essa conta não existe");
            }
        }
    }
}
