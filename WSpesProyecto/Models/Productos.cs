using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WSpesProyecto.Models;

public partial class Productos
{
    public int IdProductos { get; set; }

    public string? Articulo { get; set; }

    public decimal? CodigoArticulo { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? CostoUnitario { get; set; }

    public decimal? MontoTotal { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]//para que en la visualizacion de la api no salgan estos dos campos
    public virtual ICollection<Asignados> Asignados { get; set; } = new List<Asignados>();
    [JsonIgnore]
    public virtual ICollection<Compilado> Compilados { get; set; } = new List<Compilado>();
}
