using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WSpesProyecto.Models;

public partial class Asignados
{
    public int IdAsignados { get; set; }

    public int? IdCompilado { get; set; }

    public int? IdProductos { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? NombrePersona { get; set; }

    public string? Correo { get; set; }
    [JsonIgnore]
    public virtual Compilado? oCompilado { get; set; }
    [JsonIgnore]
    public virtual Productos? oProductos { get; set; }
}
