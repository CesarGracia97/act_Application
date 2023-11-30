﻿using act_Application.Data.Context;
using act_Application.Data.Repository;
using act_Application.Models.Sistema.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace act_Application.Controllers.General
{
    public class NotificacionesController : Controller
    {
        private readonly ActDesarrolloContext _context;

        public NotificacionesController(ActDesarrolloContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "AllOnly")]
        public IActionResult Index()
        {
            Notificaciones_VM viewModel = null;
            try
            {
                if(User.HasClaim("Rol", "Administrador"))
                {
                    var notiAdmi = new NotificacionesRepository().GetDataNotificacionesAdmin();
                    var viewModelList = notiAdmi.Select(notificacion => new Notificaciones_VM
                    {
                        Notificaciones = notificacion,
                        Prestamos = _context.ActPrestamos.FirstOrDefault(t => t.IdPres == notificacion.IdActividad),
                        Cuotas = _context.ActCuotas.FirstOrDefault(t => t.IdCuot == notificacion.IdActividad),
                        Aportaciones = _context.ActAportaciones.FirstOrDefault(t => t.IdApor == notificacion.IdActividad),
                        Eventos = _context.ActEventos.FirstOrDefault(t => t.IdEven == notificacion.IdActividad),
                        Multas = _context.ActMultas.FirstOrDefault(t => t.IdMult == notificacion.IdActividad)
                    });
                    return View(viewModelList);
                }
                else
                {
                    if (!User.HasClaim("Rol", "Administrador") && (User.HasClaim("Rol", "Socio") || User.HasClaim("Rol", "Referido")))
                    {
                        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                        int Bandera = 0;
                        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int IdUser))
                            Bandera = IdUser;
                        var notiUser = new NotificacionesRepository().GetDataNotificacionesUser(Bandera);
                        var viewModelList = notiUser.Select(notificacion => new Notificaciones_VM
                        {
                            Notificaciones = notificacion,
                            Prestamos = _context.ActPrestamos.FirstOrDefault(t => t.IdPres == notificacion.IdActividad),
                            Cuotas = _context.ActCuotas.FirstOrDefault(t => t.IdCuot == notificacion.IdActividad),
                            Eventos = _context.ActEventos.FirstOrDefault(t => t.IdEven == notificacion.IdActividad),
                            Multas = _context.ActMultas.FirstOrDefault(t => t.IdMult == notificacion.IdActividad)
                        });
                        return View(viewModelList);
                    }
                    else
                    {
                        Console.WriteLine("\n--------------------------------------------------------------------");
                        Console.WriteLine("\nError.");
                        Console.WriteLine("\nNotificacionesController-Index() | Este usuario no posee un rol.");
                        Console.WriteLine("\n--------------------------------------------------------------------\n");
                    }
                    if (viewModel == null)
                    {
                        viewModel = new Notificaciones_VM();
                    }
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n--------------------------------------------------------------------");
                Console.WriteLine($"\nError.");
                Console.WriteLine($"\nNotificacionesController-Index() | {ex.Message} ");
                Console.WriteLine($"\n--------------------------------------------------------------------\n");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
