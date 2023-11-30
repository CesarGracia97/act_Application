﻿using act_Application.Helper;
using act_Application.Models.BD;
using MySql.Data.MySqlClient;
using System.Data;

namespace act_Application.Data.Repository
{
    public class MultaRepository
    {
        private readonly string connectionString = AppSettingsHelper.GetConnectionString();
        public bool GetExistMultas()
        {
            try
            {
                string Query = ConfigReader.GetQuery( 1, "MULT", "DBQM_SelectMulta");

                int totalMultas = 0; // Variable para almacenar el valor de TotalAportaciones

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(Query, connection))
                    {
                        cmd.CommandType = CommandType.Text;

                        connection.Open();

                        using (MySqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read()) // Avanzar al primer registro
                            {
                                totalMultas = Convert.ToInt32(rd["TotalMultas"]);
                            }
                        }
                    }
                }
                // Si totalAportaciones es mayor que 0, devuelve true, de lo contrario, devuelve false.
                return totalMultas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetExistMultas || Error");
                Console.WriteLine("Razon del Error: " + ex.Message);
                return false;
            }
        }
        public List<ActMulta> GetDataMultas()
        {
            try
            {
                string multasQuery = ConfigReader.GetQuery( 1, "MULT", "DBQM_SelectMulta");

                List<ActMulta> multas = new List<ActMulta>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(multasQuery, connection))
                    {
                        cmd.CommandType = CommandType.Text;

                        connection.Open();

                        using (MySqlDataReader rd = cmd.ExecuteReader())
                        {
                            var multasPorUsuario = rd.Cast<IDataRecord>()
                                .Select(r => new
                                {
                                    Id = Convert.ToInt32(r["Id"]),
                                    IdMult = Convert.ToString(r["IdMult"]),
                                    IdUser = Convert.ToInt32(r["IdUser"]),
                                    FechaGeneracion = Convert.ToDateTime(r["FechaGeneracion"]),
                                    Cuadrante = Convert.ToString(r["Cuadrante"]),
                                    Razon = Convert.ToString(r["Razon"]),
                                    Valor = Convert.ToDecimal(r["Valor"]),
                                    Estado = Convert.ToString(r["Estado"]),
                                    FechaPago = Convert.ToString(r["FechaPago"]),
                                    CBancoOrigen = Convert.ToString(r["CBancoOrigen"]),
                                    NBancoOrigen = Convert.ToString(r["NBancoOrigen"]),
                                    CBancoDestino = Convert.ToString(r["CBancoDestino"]),
                                    NBancoDestino = Convert.ToString(r["NBancoDestino"]),
                                    HisotiralValores = Convert.ToString(r["HisotiralValores"]),
                                    CapturaPantalla = Convert.ToString(r["CapturaPantalla"]),
                                    NombreUsuario = Convert.ToString(r["NombreUsuario"])
                                })
                                .ToList();

                            var multasAgrupadas = multasPorUsuario
                                .GroupBy(m => new { m.NombreUsuario, m.FechaGeneracion.Month }) // Agrupamos por NombreUsuario y Mes
                                .ToList();

                            foreach (var group in multasAgrupadas)
                            {
                                var multa = new ActMulta
                                {
                                    Id = group.First().Id,
                                    IdUser = group.First().IdUser,
                                    FechaGeneracion = group.First().FechaGeneracion,
                                    NombreUsuario = group.Key.NombreUsuario
                                };

                                multa.NumeroMultas = group.Count();
                                multa.DetallesMulta = group.Select(a => new DetalleMulta
                                {
                                    Valor = (decimal)a.Valor,
                                    FechaMulta = a.FechaGeneracion,
                                    Cuadrante = a.FechaGeneracion.Day <= 15 ? 1 : 2
                                }).ToList();

                                // Calculamos el valor total de las multas en el mes
                                multa.Valor = group.Sum(m => m.Valor);

                                multas.Add(multa);
                            }
                        }
                    }
                }
                return multas;

            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDataMultas || Error");
                Console.WriteLine("Razon del Error: " + ex.Message);
                return null;
            }
        }
        public ActMulta GetDataIdMultaUser(int Id)
        {
            try
            {
                string Query = ConfigReader.GetQuery( 1, "MULT", "DBQM_SelectIdMultaUser");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ActMulta obj = MapToMulta(reader);
                            return obj;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDataIdMultaUser | Error");
                Console.WriteLine("Detalles del error: " + ex.Message);
            }
            return null;
        }
        public bool GetExistMultasUser(int IdUser)
        {
            try
            {
                string Query = ConfigReader.GetQuery( 1, "MULT", "DBQM_SelectMultasUser");
                int totalMultas = 0;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@IdUser", IdUser);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalMultas = Convert.ToInt32(reader["TotalMultas"]);
                            }
                        }
                    }
                }
                return totalMultas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetExistMultasUser || Error");
                Console.WriteLine("Razon del Error: " + ex.Message);
                return false;
            }
        }
        public int A_GetLastIdMultaData(int IdUser)
        {
            int Id = -1;
            try
            {
                string Query = ConfigReader.GetQuery( 2, "", "ASQ_SelectLastIdMultaUser");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.Parameters.AddWithValue("@IdUser", IdUser);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            Id = Convert.ToInt32(rd["Id"]);
                        }
                        return Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLastIdMultaData || Error");
                Console.WriteLine("Razon del Error: " + ex.Message);
            }
            return Id;
        }
        /*
        public List<DetallesMultasUsers> GetDataMultasUser(int IdUser)
        {
            string connectionString = AppSettingsHelper.GetConnectionString();
            string multasQuery = ConfigReader.GetQuery(1, "SelectMultasUser");

            List<DetallesMultasUsers> multas = new List<DetallesMultasUsers>();
            DetallesMultasUsers detallesMultas = new DetallesMultasUsers();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(multasQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", IdUser);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        decimal multasAcumuladas = 0;
                        while (reader.Read())
                        {
                            detallesMultas.TotalMultas = Convert.ToInt32(reader["TotalMultas"]);
                            detallesMultas.TotalCancelados = Convert.ToInt32(reader["TotalCancelados"]);
                            DetallesMultasUsers.DetallesPorMulta multa = new DetallesMultasUsers.DetallesPorMulta
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Valor = Convert.ToDecimal(reader["Valor"]),
                                Aprobacion = Convert.ToString(reader["Aprobacion"])
                            };
                            detallesMultas.Detalles.Add(multa);
                            multasAcumuladas += multa.Valor;
                        }
                        detallesMultas.MultasAcumuladas = multasAcumuladas;
                    }
                }
            }
            multas.Add(detallesMultas);
            return multas;
        }
        */
        private ActMulta MapToMulta(MySqlDataReader reader)
        {
            return new ActMulta
            {
                Id = Convert.ToInt32(reader["Id"]),
                IdMult = Convert.ToString(reader["IdMult"]),
                IdUser = Convert.ToInt32(reader["IdUser"]),
                FechaGeneracion = Convert.ToDateTime(reader["FechaGeneracion"]),
                Cuadrante = Convert.ToString(reader["Cuadrante"]),
                Razon = Convert.ToString(reader["Razon"]),
                Valor = Convert.ToDecimal(reader["Valor"]),
                Estado = Convert.ToString(reader["Estado"]),
                FechaPago = Convert.ToString(reader["CBancoOrigen"]),
                CBancoOrigen = Convert.ToString(reader["CBancoOrigen"]),
                NBancoOrigen = Convert.ToString(reader["NBancoOrigen"]),
                CBancoDestino = Convert.ToString(reader["CBancoDestino"]),
                NBancoDestino = Convert.ToString(reader["NBancoDestino"]),
                HistorialValores = Convert.ToString(reader["HistorialValores"]),
                CapturaPantalla = Convert.ToString(reader["CapturaPantalla"])
            };
        }
    }
}