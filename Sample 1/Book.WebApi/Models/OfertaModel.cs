using System;
using System.Collections.Generic;

namespace Book.WebApi.Models
{
    public class CompraModel
    {
        public CompraModel(int quantidadeOrdem, double total, double valor)
        {
            QuantidadeOrdem = quantidadeOrdem;
            Total = total;
            Valor = valor;
        }

        public int QuantidadeOrdem { get; set; }
        public double Total { get; set; }
        public double Valor { get; set; }
    }

    public class VendaModel
    {
        public VendaModel(int quantidadeOrdem, double total, double valor)
        {
            QuantidadeOrdem = quantidadeOrdem;
            Total = total;
            Valor = valor;
        }

        public int QuantidadeOrdem { get; set; }
        public double Total { get; set; }
        public double Valor { get; set; }
    }

    public class OfertaModel
    {
        public OfertaModel(CompraModel compra, VendaModel venda)
        {
            Compra = compra;
            Venda = venda;
        }

        public CompraModel Compra { get; set; }
        public VendaModel Venda { get; set; }
    }

    public class BookModel
    {
        public BookModel(DateTime ultimaCotacao, IList<OfertaModel> ofertas)
        {
            UltimaCotacao = ultimaCotacao;
            Ofertas = ofertas;
        }

        public DateTime UltimaCotacao { get; set; }
        public IList<OfertaModel> Ofertas { get; set; }
    }
}
