using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using DesafioTechPettencialPaymentAPI.Models;
using DesafioTechPettencialPaymentAPI.BancoDeDados;

namespace DesafioTechPettencialPaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendasController : ControllerBase
    {
        //Listar todas as vendas
        [HttpGet("ListarVendas")]
        public IActionResult ListarTodasVendas()
        {
            var resultado = new BancoDadosTemporario().ListarVendas();
            if (resultado.Count() == 0)
              
                return BadRequest(new { Erro = "Sem vendas registradas. Banco de dados vazio"});
            return Ok(resultado);
        }

        // Retorna pelo Id da Venda
        [HttpGet("ListarVendaPorId/{id}")]
        public IActionResult ListarVendaId(int id)
        {
            var resultado = new BancoDadosTemporario().RetornaVendaPorId(id);
            if (resultado == null)
                return BadRequest(new { Erro = "Venda (pedido) não localizado"});

            return Ok(resultado);
        }
       
        //Listar todos vendedores
        [HttpGet("ListarVendedores")]
        public IActionResult ListarTodosVendedores()
        {
            var resultado = new Vendedor().ListarVendedor();
            return Ok(resultado);
        }

        //Listar todas vendas feito por um vendedor específico
        [HttpGet("ListarVendasPorVendedor/{id}")]
        public IActionResult ListarVendasdoVendedor(int id)
        {
            var resultado = new BancoDadosTemporario().ListarVendasPorVendedor(id).ToList();
            if (resultado.Count == 0)
                return BadRequest(new { Erro = "Sem vendas ou vendedor não localizado"});

            return Ok(resultado);
        }

        // Lista todos os produtos que são cadastrados no sistema
        [HttpGet("ListarProdutos")]
        public IActionResult ListarTodosProdutos()
        {
            var resultado = Enum.GetValues(typeof(EnumProdutos)).Cast<EnumProdutos>().Select(v => v.ToString()).ToList();
            return Ok(resultado);
        }

        // Cadastrar Vendas
        [HttpPost("CadastrarVenda")]
        public IActionResult CadastarUmaVenda(PedidoVendedor venda)    
        {   
            if (venda.Produto.Count == 0)
                return BadRequest(new { Erro = "O pedido deve ter um produto"});

            var vendedores = new Vendedor().ListarVendedor();    
            if (!vendedores.Any(x => x.VendedorID == venda.VendedorId))
                return BadRequest(new { Erro = "Vendedor não localizado"});

            var resultado = new BancoDadosTemporario().AdicionarVenda(venda);
            return Ok(resultado);
        }

        //Inserir mais um produto em um pedido
        [HttpPut("InserirNoPedido/{idPedido},{produto}")]
        public IActionResult InserirNoPedido(int idPedido, EnumProdutos produto)
        {
            var pedido = new BancoDadosTemporario().RetornaVendaPorId(idPedido);
            if (pedido == null)
                return BadRequest(new { Erro = "Venda (pedido) não localizado"});

            var resultado = new BancoDadosTemporario().AcrescentarProdutoNaVenda(idPedido, produto);
            return Ok(resultado);
        }

        // Atualizar status da venda
        [HttpPut("AtualizarStatus/{idPedido},{atualizarStatusVenda}")]
        public IActionResult AtualizarStatus(int idPedido, EnumStatusVenda atualizarStatusVenda)
        {
            var resultado = new BancoDadosTemporario().RetornaVendaPorId(idPedido);
            if (resultado == null)
                return BadRequest(new { Erro = "Pedido não localizado"});

            int statusArmazenado = (int)resultado.StatusVenda;

            if (statusArmazenado == 0) //Se o pedido estiver: "Aguardando Pagamento"
            {
                if ((int)atualizarStatusVenda == 1 || (int)atualizarStatusVenda == 4) // Se selecionado ou "Pagamento Aprovado" ou "Cancelado"
                {
                    return Ok(new BancoDadosTemporario().AtualizarStatusVenda(idPedido, atualizarStatusVenda));
                }
            }
            else if (statusArmazenado == 1) //Se o pedido estiver: PagamentoAprovado
            {
                if ((int)atualizarStatusVenda == 2 || (int)atualizarStatusVenda == 4) //Se selecionado: "EnviadoParaTransportadora" ou "Cancelado"
                {
                    return Ok(new BancoDadosTemporario().AtualizarStatusVenda(idPedido, atualizarStatusVenda));
                }
            }
            else if (statusArmazenado == 2) //Se o pedido estiver: EnviadoPataTransportadora
            {
                if ((int)atualizarStatusVenda == 3) //Se estiver selecionado: "Entregue"
                {
                    return Ok(new BancoDadosTemporario().AtualizarStatusVenda(idPedido, atualizarStatusVenda));
                }
            }
            else if (statusArmazenado == 3) //Entregue
            {
                return BadRequest(new { Erro = "O pedido já havia sido entregue"});
            }
            else if (statusArmazenado == 4) //Cancelada
            {
                return BadRequest(new { Erro = "O pedido já havia sido cancelado"});
            }
            
            return BadRequest(new { Erro = "Opção Iválida"});
        }
    }
}