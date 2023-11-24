﻿using act_Application.Data.Context;
using act_Application.Data.Data;
using act_Application.Models.BD;
using Microsoft.AspNetCore.Mvc;

namespace act_Application.Services.ServiciosAplicativos
{
    public class InteresesServices
    {
        private readonly ActDesarrolloContext _context;
        public InteresesServices(ActDesarrolloContext context)
        {
            context = _context;
        }
        public async Task AddNewInteres(int IdUser, string torigen, decimal Valor, decimal PorcentajeGarante, decimal PorcentajeTodos [Bind("Id,IdUser,IdPersonnalizado,Valor,Estado,ValorGarante,ValorTodos,ValorActual")]ActTablaInteres actTablaInteres)
        {
            int IdMultaUser = new MultaRepository().A_GetLastIdMultaData(IdUser);
            actTablaInteres.IdUser = IdUser;
            actTablaInteres.IdPersonalizado = torigen + "-" + IdMultaUser.ToString();
            actTablaInteres.Valor = Valor;
            actTablaInteres.ValorGarante =
        }
    }
}
