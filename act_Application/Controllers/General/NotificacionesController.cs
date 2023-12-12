﻿using act_Application.Data.Context;
using act_Application.Data.Repository;
using act_Application.Models.BD;
using act_Application.Models.Sistema.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
                    var notiAdmi = (List<ActNotificacione>) new NotificacionesRepository().OperacionesNotificaciones(3, 0, 0);
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
                        var notiUser = (List<ActNotificacione>) new NotificacionesRepository().OperacionesNotificaciones( 4, 0, Bandera);
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
        public async Task<IActionResult> VisualizadoA(int Id, [Bind("Id,IdActividad,FechaGeneracion,Razon,Descripcion,Destino,Visto")] ActNotificacione actNotificacione)
        {
            if (Id != actNotificacione.Id)
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var nobj = (ActNotificacione)new NotificacionesRepository().OperacionesNotificaciones(5, Id, 0);
                    actNotificacione.IdActividad = nobj.IdActividad;
                    actNotificacione.FechaGeneracion = nobj.FechaGeneracion;
                    actNotificacione.Razon = nobj.Razon;
                    actNotificacione.Descripcion = nobj.Descripcion;
                    actNotificacione.Destino = nobj.Destino;
                    actNotificacione.Visto = "SI";
                    _context.Update(actNotificacione);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Notificaciones");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("Hubo un problema al actualizar el registro del pago de la Cuota.");
                    Console.WriteLine("Detalles del error: " + ex.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(actNotificacione);
        }            
    }
}
