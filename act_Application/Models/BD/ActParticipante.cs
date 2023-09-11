﻿using System;
using System.Collections.Generic;

namespace CodeGenerator.Models.BD;

/// <summary>
/// Tabla de Participantes/Garantes de Prestamo
/// </summary>
public partial class ActParticipante
{
    public int Id { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFinaliacion { get; set; }

    public DateTime FechaGeneracion { get; set; }

    public string Participantes { get; set; } 
}