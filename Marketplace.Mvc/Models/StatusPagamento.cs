using System.ComponentModel;

namespace Marketplace.Mvc.Models
{
    public enum StatusPagamento// : int
    {
        [Description("Não definido")]
        NaoDefinido = 0,

        [Description("Saldo insuficiente")]
        SaldoInsuficiente = 1,

        [Description("Pedido já pago")]
        PedidoJaPago = 2,

        [Description("Cartão inexistente")]
        CartaoInexistente = 3,

        [Description("Pagamento OK")]
        PagamentoOK = 4
    }
}