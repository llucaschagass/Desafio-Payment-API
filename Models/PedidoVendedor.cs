
namespace DesafioTechPettencialPaymentAPI.Models
{
    public class PedidoVendedor
    {
        public int IdVenda { get; set; }
        public List<EnumProdutos> Produto { get; set; } = new List<EnumProdutos>();
        public int VendedorId { get; set; }
        public EnumStatusVenda StatusVenda {get; set;}
        public DateTime DataDaVenda { get; set; }
        
        
        
       
    }
}