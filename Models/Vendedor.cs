namespace DesafioTechPettencialPaymentAPI.Models
{
    public class Vendedor
    {
        public int VendedorID {get; set;}
        public string CpfVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string EmailVendedor { get; set; }
        public string FoneVendedor { get; set; }
        
        public List<Vendedor> ListarVendedor()
        {
            return new List<Vendedor>
            {
                new Vendedor{VendedorID = 0, CpfVendedor = "cpf1", NomeVendedor = "Fulano", EmailVendedor = "fulano@vendedor",FoneVendedor = "111111"},
                new Vendedor{VendedorID = 1, CpfVendedor = "cpf2", NomeVendedor = "Deltrano", EmailVendedor = "deltrano@vendedor",FoneVendedor = "222222"},
                new Vendedor{VendedorID = 2, CpfVendedor = "cpf3", NomeVendedor = "Ciclano", EmailVendedor = "ciclano@vendedor",FoneVendedor = "333333"},
            };
        }

        
        
        
               
    }
}