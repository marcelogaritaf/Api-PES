using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WSpesProyecto.Models;

public partial class Compilado
{
    public int IdCompilado { get; set; }

    public string? Oficina { get; set; }

    public string? Area { get; set; }

    public int? CodigoPrograma { get; set; }

    public int? CodigoSubpartida { get; set; }

    public int? IdProductos { get; set; }

    public string? PeriodoEjecucion { get; set; }

    public int? Prioridad { get; set; }

    public string? TipoTramite { get; set; }

    public string? NumeroContratoVigente { get; set; }

    public string? RequisionCertificacion { get; set; }

    public int? NumeroScUcUa { get; set; }

    [JsonIgnore]
    public virtual ICollection<Asignados> Asignados { get; set; } = new List<Asignados>();
    public virtual Productos? oProductos { get; set; }
}
