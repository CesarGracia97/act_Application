﻿using System.ComponentModel.DataAnnotations.Schema;
namespace act_Application.Models.BD;
public partial class ActMulta
{
    public int Id { get; set; }
    public string IdMult { get; set; }
    public int IdUser { get; set; }
    public DateTime FechaGeneracion { get; set; }
    public string Cuadrante { get; set; }
    public string Razon { get; set; }
    public decimal Valor { get; set; }
    public string Estado { get; set; }
    public string FechaPago { get; set; }
    public string CBancoOrigen { get; set; }
    public string NBancoOrigen { get; set; }
    public string CBancoDestino { get; set; }
    public string NBancoDestino { get; set; }
    public string HistorialValores { get; set; }
    public string CapturaPantalla { get; set; }
    [NotMapped]
    public string NombreUsuario { get; set; }
    [NotMapped]
    public int NumeroMultas { get; set; }
    [NotMapped]
    public List<DetalleMulta> DetallesMulta { get; set; }
}
public class DetalleMulta
{
    public decimal Valor { get; set; }
    public DateTime FechaMulta { get; set; }
    public int Cuadrante { get; set; }
}