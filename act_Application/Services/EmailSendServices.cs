﻿using act_Application.Helper;
using act_Application.Models.Sistema.Complementos;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using act_Application.Data.Data;

namespace act_Application.Services
{
    public class EmailSendServices
    {
        /*Envia la solicitud de prestamo*/
        public async Task EnviarCorreoAdmin(int opcion, int IdUser, string Descripcion)
        {
            string correoDestino = CorreoHelper.ObtenerCorreoDestino(), subject = string.Empty, body = string.Empty;
            switch (opcion)
            {
                case 1:
                    //Nuevo Usuario
                    subject = "act - Application: Solicitud de Ingreso.";
                    body = Descripcion;
                    break;
                 case 2:
                    //Aportaciones
                    subject = "act - Application: Aportacion Reciente.";
                    body = Descripcion;
                    break;
                case 3:
                    //Pago de Multas
                    subject = "act - Application: Multa Cancelada Recientemente.";
                    body = Descripcion;
                    break;
                case 4:
                    //Pago de Multas
                    subject = "act - Application: Multa Abonada  Recientemente.";
                    body = Descripcion;
                    break;
                case 5:
                    //Pago de Cuotas
                    subject = "act - Application: Cuota Cancelada  Recientemente.";
                    body = Descripcion;
                    break;
                case 6:
                    //Pago de Cuotas
                    subject = "act - Application: Cuota Abonada  Recientemente.";
                    body = Descripcion;
                    break;
                case 7:
                    //Peticion de Prestamos Fase 1
                    subject = "act - Application: Solicitud de Prestamo (FASE 1)";
                    body = Descripcion;
                    break;
                case 8:
                    //Peticion de Prestamos Fase 2
                    subject = "act - Application: Solicitud de Prestamo (FASE 2)";
                    body = Descripcion;
                    break;
                case 9:
                    //Finalizacion de Evento de Participacion
                    subject = "act - Application: Evento de Participacion (FINALIZADO)";
                    body = Descripcion;
                    break;
                default:
                    Console.WriteLine("EnviarCorreoAdmin - EmailSendServices. Opcion inexistente");
                    break;
            }
            await EnviarCorreo(correoDestino, subject, body);

        }

        public async Task EnviarCorreoUsuario(int IdUser, int opcion, string Descripcion)
        {
            var uobj = new UsuarioRepository();
            string userEmail = uobj.CorreoUser(IdUser), subject = string.Empty;
            if (userEmail != null)
            {
                switch (opcion)
                {
                    case 1:
                        //Solicitud de Prestamos Fase 1
                        subject = "act - Application: Solicitud de Prestamo (FASE 1)";
                        break;
                    case 2:
                        //Solicitud de Prestamos Fase 2
                        subject = "act - Application: Solicitud de Prestamo (FASE 2)";
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("EnviarCorreoUsuario - EmailSendServices. Opcion inexistente");
                        break;
                }
                await EnviarCorreo(userEmail, subject, Descripcion);
            }
            else
            {
                Console.WriteLine("EnviarCorreoUsuario - EmailSendServices. Error al obtener el Correo");
            }
        }


        /*Envia el Correo*/
        public async Task EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            var smtpConfig = SmtpConfig.LoadConfig("Data/Config/smtpconfig.json");

            using (var smtpClient = new SmtpClient(smtpConfig.Server, smtpConfig.Port))
            {
                smtpClient.Credentials = new NetworkCredential(smtpConfig.Username, smtpConfig.Password);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpConfig.Username),
                    Subject = asunto,
                    Body = mensaje,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(destinatario);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
