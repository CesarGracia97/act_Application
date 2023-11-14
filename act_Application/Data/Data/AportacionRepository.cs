﻿using act_Application.Helper;
using act_Application.Models.BD;
using act_Application.Models.Sistema.Complementos;
using MySql.Data.MySqlClient;
using System.Data;

namespace act_Application.Data
{
    public class AportacionRepository
    {
        public bool GetExistAportaciones()
        {
            string connectionString = AppSettingsHelper.GetConnectionString();
            string aportacionesQuery = ConfigReader.GetQuery(1,"SelectAportaciones");

            int totalAportaciones = 0; // Variable para almacenar el valor de TotalAportaciones

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(aportacionesQuery, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read()) // Avanzar al primer registro
                        {
                            totalAportaciones = Convert.ToInt32(rd["TotalAportaciones"]);
                        }
                    }
                }
            }
            // Si totalAportaciones es mayor que 0, devuelve true, de lo contrario, devuelve false.
            return totalAportaciones > 0;
        }

        public List<ActAportacione> GetDataAportaciones()
        {
            string connectionString = AppSettingsHelper.GetConnectionString();
            string aportacionesQuery = ConfigReader.GetQuery(1, "SelectAportaciones");

            List<ActAportacione> aportaciones = new List<ActAportacione>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(aportacionesQuery, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        var aportacionesPorUsuario = rd.Cast<IDataRecord>()
                            .Select(r => new
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Razon = Convert.ToString(r["Razon"]),
                                Valor = Convert.ToDecimal(r["Valor"]),
                                IdUser = Convert.ToInt32(r["IdUser"]),
                                FechaAportacion = Convert.ToDateTime(r["FechaAportacion"]),
                                Estado = Convert.ToString(r["Estado"]),
                                CapturaPantalla = r.IsDBNull(r.GetOrdinal("CapturaPantalla")) ? null : (byte[])r["CapturaPantalla"],
                                NombreUsuario = Convert.ToString(r["NombreUsuario"]),

                            })
                            .ToList();

                        var aportacionesAgrupadas = aportacionesPorUsuario
                            .GroupBy(a => new { a.NombreUsuario, a.FechaAportacion.Month }) // Agrupamos por NombreUsuario y Mes
                            .ToList();

                        foreach (var group in aportacionesAgrupadas)
                        {
                            var aportacion = new ActAportacione
                            {
                                Id = group.First().Id,
                                IdUser = group.First().IdUser,
                                FechaAportacion = group.First().FechaAportacion,
                                Estado = group.First().Estado,
                                CapturaPantalla = group.First().CapturaPantalla,
                                NombreUsuario = group.Key.NombreUsuario

                            };

                            // Calculamos el número de aportaciones y almacenamos detalles
                            aportacion.NumeroAportaciones = group.Count();
                            aportacion.DetallesAportaciones = group.Select(a => new DetalleAportacion
                            {
                                Valor = (decimal)a.Valor,
                                FechaAportacion = a.FechaAportacion,
                                Cuadrante = a.FechaAportacion.Day <= 15 ? 1 : 2
                            }).ToList();

                            // Sumamos los valores para calcular la sumatoria total
                            aportacion.Valor = group.Sum(a => (decimal)a.Valor);

                            aportaciones.Add(aportacion);
                        }
                    }
                }
            }

            return aportaciones;
        }

        public bool GetExistApotacionesUser(int IdUser)
        {
            string connectionString = AppSettingsHelper.GetConnectionString();
            string aportacionesQuery = ConfigReader.GetQuery(1, "SelectAportacionesUser");

            int totalAportaciones = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(aportacionesQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", IdUser);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalAportaciones = Convert.ToInt32(reader["TotalAportaciones"]);
                        }
                    }
                }
            }
            return totalAportaciones > 0;
        }

        public List<DetallesAportacionesUsers> GetDataAportacionesUser(int IdUser)
        {
            string connectionString = AppSettingsHelper.GetConnectionString();
            string aportacionesQuery = ConfigReader.GetQuery(1,"SelectAportacionesUser");

            List<DetallesAportacionesUsers> aportaciones = new List<DetallesAportacionesUsers>();
            DetallesAportacionesUsers detallesAportaciones = new DetallesAportacionesUsers();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(aportacionesQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", IdUser);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        decimal aportacionesAcumuladas = 0;
                        while (reader.Read())
                        {
                            detallesAportaciones.TotalAportaciones = Convert.ToInt32(reader["TotalAportaciones"]);
                            detallesAportaciones.TotalAprobados = Convert.ToInt32(reader["TotalAprobados"]);
                            detallesAportaciones.TotalEspera = Convert.ToInt32(reader["TotalEspera"]);
                            DetallesAportacionesUsers.DetallesPorAportacion aportacion = new DetallesAportacionesUsers.DetallesPorAportacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Valor = Convert.ToDecimal(reader["Valor"]),
                                Aprobacion = Convert.ToString(reader["Aprobacion"]),
                                Nbanco = Convert.ToString(reader["Nbanco"])
                            };
                            detallesAportaciones.Detalles.Add(aportacion);
                            aportacionesAcumuladas += aportacion.Valor;
                        }
                        detallesAportaciones.AportacionesAcumuladas = aportacionesAcumuladas;
                    }
                }
            }

            aportaciones.Add(detallesAportaciones);
            return aportaciones;
        }

        public string GetLastIdApor (int IdUser)
        {
            string connectionString = AppSettingsHelper.GetConnectionString();
            string aportacionesQuery = ConfigReader.GetQuery(2, "SelectLastIdAporUser");
            List<ActAportacione> aportaciones = new List<ActAportacione>();
            string IdA = string.Empty;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(aportacionesQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", IdUser);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IdA = Convert.ToString(reader["IdApor"]);
                        }
                    }
                }
            }
            return IdA;

        }
    }
}