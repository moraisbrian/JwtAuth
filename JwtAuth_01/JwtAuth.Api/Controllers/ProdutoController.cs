using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuth.Api.CustomAuth;
using JwtAuth.Data;
using JwtAuth.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JwtAuth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ProdutoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Policy = "ProdutoAdicionar")]
        //[ClaimsAuthorize("Produto", "Adicionar")]
        [HttpPost]
        [Route("Cadastrar")]
        public async Task<ActionResult> Cadastrar(Produto produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public async Task<ActionResult<IEnumerable<Produto>>> ConsultarTodos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        [HttpGet]
        [Route("ConsultarPorId/{id}")]
        public async Task<ActionResult<Produto>> ConsultarPorId(int id)
        {
            return await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [Authorize(Policy = "ProdutoDeletar")]
        //[ClaimsAuthorize("Produto", "Deletar")]
        [HttpDelete]
        [Route("Deletar/{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var produto = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produto != null)
            {
                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Policy = "ProdutoEditar")]
        //[ClaimsAuthorize("Produto", "Editar")]
        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult> Atualizar(int id, Produto produto)
        {
            var produtoParaAtualizar = await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produtoParaAtualizar != null)
            {
                produtoParaAtualizar.Identificacao = produto.Identificacao;
                produtoParaAtualizar.Preco = produto.Preco;
                produtoParaAtualizar.Estoque = produto.Estoque;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }
    }
}
