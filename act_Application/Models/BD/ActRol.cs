﻿using System;
using System.Collections.Generic;

namespace act_Application.Models.BD;

/// <summary>
/// Tabla de Roles
/// </summary>
public partial class ActRol
{
    public int Id { get; set; }

    public string NombreRol { get; set; }

    public string DescripcionRol { get; set; }
}
