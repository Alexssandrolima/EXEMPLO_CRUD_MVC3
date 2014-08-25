using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMADAS.BLL;
using System.Data;

namespace CAMADAS.DAL
{
    public class DALProfessor
    {
        public static DALProfessor GetInstance()
        {
            return new DALProfessor();
        }

        public void Incluir(Professor entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"INSERT INTO PROFESSOR (NOME,CPF) VALUES (@NOME, @CPF)");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@CPF", entidade.CPF);

                entidade.ID = DALBase.ExecuteInsert(sql.ToString(), parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Alterar(Professor entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"UPDATE PROFESSOR SET NOME = @NOME, CPF = @CPF WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@ID", entidade.ID);
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@CPF", entidade.CPF);

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
                sql.AppendFormat(@"DELETE FROM PROFESSOR WHERE ID = @ID");

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

        public void PreencherEntidade(Professor entidade, DataRow dr)
        {
            entidade.ID = Convert.ToInt32(dr["ID"].ToString());
            entidade.Nome = dr["NOME"].ToString() ?? string.Empty;
            entidade.CPF = Convert.ToDouble(dr["CPF"]);
        }

        public List<Professor> Listar()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM PROFESSOR");

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), new Dictionary<string, object>());

                List<Professor> retorno = new List<Professor>();

                foreach (DataRow dr in dt.Rows)
                {
                    Professor professor = new Professor();
                    PreencherEntidade(professor, dr);
                    retorno.Add(professor);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Professor> ListarComFiltro(Professor entidade)
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

                if (entidade.CPF > 0)
                {
                    paramteros.Add("@CPF", entidade.CPF);

                    if (!sql.ToString().Contains("WHERE"))
                    {
                        sql.AppendLine("WHERE CPF = @CPF ");
                    }
                    else
                    {
                        sql.AppendLine(" AND CPF = @CPF ");
                    }
                }

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), paramteros);

                List<Professor> retorno = new List<Professor>();

                foreach (DataRow dr in dt.Rows)
                {
                    Professor professor = new Professor();
                    PreencherEntidade(professor, dr);
                    retorno.Add(professor);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Professor GetByID(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM ALUNO WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@ID", id);

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), parametros);

                Professor professor = new Professor();
                if (dt.Rows.Count > 0)
                {
                    PreencherEntidade(professor, dt.Rows[0]);
                }

                return professor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
