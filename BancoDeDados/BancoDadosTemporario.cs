using DesafioTechPettencialPaymentAPI.Models;

namespace DesafioTechPettencialPaymentAPI.BancoDeDados
{
    public class BancoDadosTemporario
    {
        // Cria Lista de pedidos (Banco com os dados das Vendas)
        public static List<PedidoVendedor> bancoDadosVendas { get; } = new List<PedidoVendedor>();

        public PedidoVendedor AdicionarVenda(PedidoVendedor venda)
        {
            venda.StatusVenda = 0;
            venda.IdVenda = bancoDadosVendas.Count();

            bancoDadosVendas.Add(venda);
            return venda;
        }

        public List<PedidoVendedor> ListarVendas()
        {
            return bancoDadosVendas;
        }
        
        public PedidoVendedor RetornaVendaPorId(int id)
        {   
            bool resultado = bancoDadosVendas.Any(x => x.IdVenda == id);
            if (!resultado)
                return null;
            return bancoDadosVendas[id];
        }

        public PedidoVendedor AcrescentarProdutoNaVenda(int id, EnumProdutos produto)
        {
            bancoDadosVendas[id].Produto.Add(produto);
            return bancoDadosVendas[id];
        }

        public PedidoVendedor AtualizarStatusVenda(int id, EnumStatusVenda statusVenda)
        {
            bancoDadosVendas[id].StatusVenda = statusVenda;
            return (bancoDadosVendas[id]);
        }
        
        public IEnumerable<int> ListarVendasPorVendedor(int id)
        {
            var resultado = bancoDadosVendas.Where(x=> x.VendedorId == id).Select(x=> x.IdVenda);
            return resultado;
        }

        
    
        
        
    }
}