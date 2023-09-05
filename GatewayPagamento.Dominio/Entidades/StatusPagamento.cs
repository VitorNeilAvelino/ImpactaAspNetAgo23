using System.ComponentModel;

namespace GatewayPagamento.Dominio.Entidades
{
    public enum StatusPagamento
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