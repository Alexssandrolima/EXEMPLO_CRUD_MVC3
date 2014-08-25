using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace CAMADAS.DAL
{
    public class DALBase
    {
        private static string GetConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["CRUDConnectionString"].ToString();
        }

        /// <summary>
        ///  DataAccess Method
        /// </summary>
        /// <param name="command"> Comando do SQL </param>
        /// <param name="parameters">Variáveis do SQL: @Variavel</param>
        /// <returns>
        /// Número de linhas afetadas
        /// </returns>
        public static int ExecuteNonQuery(string command, Dictionary<string, object> parameters)
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                cmd.CommandText = command;

                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);

                }

                con.Open();

                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Executa insert. OBSERVACAO: Não necessita de "SELECT @@IDENTITY" no comando.
        /// </summary>
        /// <param name="command"> Comando do SQL </param>
        /// <param name="parameters">Variáveis do SQL: @Variavel</param>
        /// <returns>
        /// ID do item inserido.
        /// </returns>
        public static int ExecuteInsert(string command, Dictionary<string, object> parameters)
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            try
            {
                if (!command.Trim().EndsWith("SELECT @@IDENTITY;"))
                {
                    if (!command.Trim().EndsWith(";"))
                        command += "; SELECT @@IDENTITY;";
                    else command += " SELECT @@IDENTITY;";
                }

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                cmd.CommandText = command;

                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);

                }

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Executa SELECT.
        /// </summary>
        /// <param name="command"> Comando do SQL </param>
        /// <param name="parameters">Variáveis do SQL: @Variavel</param>
        /// <returns>
        /// Resultado do SELECT.
        /// </returns>
        public static DataTable ExecuteSelect(string command, Dictionary<string, object> parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = new SqlConnection(GetConnectionString());

                cmd.CommandText = command;

                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
