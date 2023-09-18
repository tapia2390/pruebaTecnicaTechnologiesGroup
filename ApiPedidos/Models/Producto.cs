using System;
using System.Collections.Generic;

namespace ApiPedidos.Models;

public partial class Producto
{
    public int ProId { get; set; }

    public string? ProDesc { get; set; }

    public decimal? ProValor { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
