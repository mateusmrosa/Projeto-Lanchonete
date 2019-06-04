using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIT_tela_de_login.Models
{
    public class PedidoItem
    {
        Produto _produto;
        private int _quantidade;
        private double _valor;

        public PedidoItem()
        {
        }

        public PedidoItem(Produto produto, int quantidade, double valor)
        {
            _produto = produto;
            _quantidade = quantidade;
            _valor = valor;
        }

        public Produto Produto { get => _produto; set => _produto = value; }
        public int Quantidade { get => _quantidade; set => _quantidade = value; }
        public double Valor { get => _valor; set => _valor = value; }
    }
}
