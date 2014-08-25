using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMADAS.BLL;
using System.Data;
using System.Linq.Expressions;

namespace CAMADAS.DAL
{
    public class DALCurso
    {
        public static DALCurso GetInstance()
        {
            return new DALCurso();
        }

        public void Incluir(Curso entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"INSERT INTO CURSO (NOME,DATA_HORA_INICI,DATA_HORA_FINAL) VALUES (@NOME, @DATAINI, @DATAFIM)");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@DATAINI", entidade.DataHoraInicial);
                parametros.Add("@DATAFIM", entidade.DataHoraFinal);

                entidade.ID = DALBase.ExecuteInsert(sql.ToString(), parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Alterar(Curso entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"UPDATE CURSO SET NOME = @NOME, DATA_HORA_INICI = @DATAINI, DATA_HORA_FINAL = @DATAFIM WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@ID", entidade.ID);
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@DATAINI", entidade.DataHoraInicial);
                parametros.Add("@DATAFIM", entidade.DataHoraFinal);

                if (DALBase.ExecuteNonQuery(sql.ToString(), parametros) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Excluir(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"DELETE FROM CURSO WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@ID", id);

                if (DALBase.ExecuteNonQuery(sql.ToString(), parametros) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PreencherEntidade(Curso entidade, DataRow dr)
        {
            entidade.ID = Convert.ToInt32(dr["ID"].ToString());
            entidade.Nome = dr["NOME"].ToString() ?? string.Empty;
            entidade.DataHoraInicial = Convert.ToDateTime(dr["DATA_HORA_INICI"]);
            entidade.DataHoraFinal = Convert.ToDateTime(dr["DATA_HORA_FINAL"]);
        }

        public List<Curso> Listar()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM CURSO");

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), new Dictionary<string, object>());

                List<Curso> retorno = new List<Curso>();

                foreach (DataRow dr in dt.Rows)
                {
                    Curso curso = new Curso();
                    PreencherEntidade(curso, dr);
                    retorno.Add(curso);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Curso> ListarComFiltro(Curso entidade)
        {
            try
            {
                Dictionary<string, object> paramteros = new Dictionary<string, object>();

                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM CURSO ");

                if (!string.IsNullOrEmpty(entidade.Nome))
                {
                    paramteros.Add("@NOME", entidade.Nome);

                    if (!sql.ToString().Contains("WHERE"))
                    {
                        sql.AppendLine(" WHERE NOME LIKE @NOME ");
                    }
                    else
                    {
                        sql.AppendLine(" AND NOME LIKE @NOME ");
                    }
                }

                if (entidade.DataHoraInicial != DateTime.MinValue)
                {
                    paramteros.Add("@DATAINI", entidade.DataHoraInicial);

                    if (!sql.ToString().Contains("WHERE"))
                    {
                        sql.AppendLine("WHERE DATA_HORA_INICI = @DATAINI ");
                    }
                    else
                    {
                        sql.AppendLine(" AND DATA_HORA_INICI = @DATAINI ");
                    }
                }

                if (entidade.DataHoraFinal != DateTime.MinValue)
                {
                    paramteros.Add("@DATAFIM", entidade.DataHoraFinal);

                    if (!sql.ToString().Contains("WHERE"))
                    {
                        sql.AppendLine("WHERE AND DATA_HORA_FINAL = @DATAFIM ");
                    }
                    else
                    {
                        sql.AppendLine(" AND DATA_HORA_FINAL = @DATAFIM ");
                    }
                }

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), paramteros);

                List<Curso> retorno = new List<Curso>();

                foreach (DataRow dr in dt.Rows)
                {
                    Curso curso = new Curso();
                    PreencherEntidade(curso, dr);
                    retorno.Add(curso);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Curso GetByID(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM CURSO WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@ID", id);

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), parametros);

                Curso curso = new Curso();
                if (dt.Rows.Count > 0)
                {
                    PreencherEntidade(curso, dt.Rows[0]);
                }

                return curso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void GetByID(Curso entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM CURSO WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@ID", entidade.ID);

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), parametros);

                
                if (dt.Rows.Count > 0)
                {
                    PreencherEntidade(entidade, dt.Rows[0]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
