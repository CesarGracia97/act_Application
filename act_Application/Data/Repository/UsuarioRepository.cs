﻿using act_Application.Helper;
using act_Application.Models.BD;
using act_Application.Models.Sistema.Complementos;
using MySql.Data.MySqlClient;
using System.Data;

namespace act_Application.Data.Repository
{
    public class UsuarioRepository
    {
        private readonly string connectionString = AppSettingsHelper.GetConnectionString();
        private ActUser GetData_User(string Correo, string Contrasena)
        {
            try
            {
                string Query = ConfigReader.GetQuery(1, "USER", "DBQU_SelectUsuario");
                ActUser uobj = new ActUser();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.Parameters.AddWithValue("@Correo", Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", Contrasena);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            uobj = MapToUser(reader);

                        }
                    }
                }
                return uobj;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_User || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_User || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return null;
            }
        }
        private int GetData_IdUser_Cedula(string Cedula) // Obtener el Id de un Usuario por medio de su Cedula porque es el Id Personalizado en Notificaciones
        {
            try
            {
                int Id = 0;
                string Query = ConfigReader.GetQuery(1, "USER", "DBQU_SelectIdUser");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(Query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", Cedula);
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Id = Convert.ToInt32(reader["Id"]);
                            }
                        }
                    }
                }
                return Id;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_IdUser || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_IdUser || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return -1;
            }
        }
        private List<UserList> GetData_ListUser()
        {
            try
            {
                string Query = ConfigReader.GetQuery(1, "USER", "DBQU_SelectListUser");
                List<UserList> users = new List<UserList>();
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand(Query, conexion);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserList user = new UserList()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Usuario = Convert.ToString(reader["NombreYApellido"])
                            };
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_ListUser || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_ListUser || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return null;
            }
        }
        private string GetData_CorreoUser(int IdUser)
        {
            try
            {
                string email = string.Empty;
                string Query = ConfigReader.GetQuery(2, "", "ASQ_SelectCorreoUser");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.Parameters.AddWithValue("@IdUser", IdUser);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            email = Convert.ToString(rd["Correo"]);
                        };
                    }
                }
                return email;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_CorreoUser || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_CorreoUser || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return null;
            }
        }
        private ActUser GetData_DataUser(int Id)
        {
            try
            {
                string Query = ConfigReader.GetQuery(1, "USER", "DBQU_SelectDataUserId");
                ActUser uobj = new ActUser();
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
                            uobj = MapToUser(reader);
                        }
                    }
                }
                return uobj;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_DataUser || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_DataUser || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return null;
            }
        }
        private ActRol GetData_RolUser(int IdRol)
        {
            try
            {
                string Query = ConfigReader.GetQuery(1, "ROLE", "DBQR_SelectRol");
                ActRol robj = new ActRol();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(Query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdRol", IdRol);
                        cmd.CommandType = CommandType.Text;

                        connection.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                robj = MapToRol(reader);
                            }
                        }
                    }
                }
                return robj;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_RolUser || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_RolUser || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return null;
            }
        }
        private ActRolUser GetData_DataUserRol(int IdUser)
        {
            try
            {
                string Query = ConfigReader.GetQuery(1, "ROLE", "DBQR_SelectDataaUserRol");
                ActRolUser robj = new ActRolUser();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(Query, connection);
                    cmd.Parameters.AddWithValue("@Id", IdUser);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            robj = MapToRolUser(reader);
                        }
                    }
                }
                return robj;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"\nGetData_DataUserRol || Error de Mysql");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGetData_DataUserRol || ErrorGeneral");
                Console.WriteLine($"\nRazon del Error: {ex.Message}\n");
                return null;
            }
        }
        private ActUser MapToUser(MySqlDataReader reader)
        {
            return new ActUser
            {
                Id = Convert.ToInt32(reader["Id"]),
                Cedula = Convert.ToString(reader["Cedula"]),
                Correo = Convert.ToString(reader["Correo"]),
                NombreYapellido = Convert.ToString(reader["NombreYApellido"]),
                TipoUser = Convert.ToString(reader["TipoUser"]),
                IdRol = Convert.ToInt32(reader["IdRol"]),
                Estado = Convert.ToString(reader["Estado"])
            };
        }
        private ActRol MapToRol(MySqlDataReader reader)
        {
            return new ActRol
            {
                Id = reader.GetInt32("Id"),
                NombreRol = reader["NombreRol"].ToString(),
                DescripcionRol = reader["DescripcionRol"].ToString()
            };
        }
        private ActRolUser MapToRolUser(MySqlDataReader reader)
        {
            return new ActRolUser
            {
                Id = reader.GetInt32("Id"),
                IdUser = reader.GetInt32("IdUser"),
                IdRol = reader.GetInt32("IdRol"),
            };
        }
        public object OperacionesUsuario(int Opcion, int Id, int IdUser, string Correo, string Contrasena)
        {
            try
            {
                switch (Opcion)
                {
                    case 1:
                        return GetData_User( Correo, Contrasena);
                    case 2:
                        return GetData_RolUser(Id);
                    case 3:
                        return GetData_ListUser();
                    case 4:
                        return GetData_CorreoUser(IdUser);
                    case 5:
                        return GetData_IdUser_Cedula(Correo);
                    case 6:
                        return GetData_DataUser(Id);
                    case 7:
                        return GetData_DataUserRol(IdUser);
                    default:
                        Console.WriteLine("\n-----------------------------------------");
                        Console.WriteLine("\nOperacionesUsuario || Opcion Inexistente.");
                        Console.WriteLine("\n-----------------------------------------\n");
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n-----------------------------------");
                Console.WriteLine("\nOperacionesUsuario || Error.");
                Console.WriteLine("\nRazon del Error: " + ex.Message);
                Console.WriteLine("\n-----------------------------------");
                return null;
            }
        }
    }
}
