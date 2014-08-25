using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMADAS.BLL;
using System.Data;

namespace CAMADAS.DAL
{
    public class DALAluno
    {
        public static DALAluno GetInstance()
        {
            return new DALAluno();
        }

        public void Incluir(Aluno entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"INSERT INTO ALUNO (NOME, DATA_NASC, RG, CPF) 
                                    VALUES (@NOME, @DATA_NASC, @RG, @CPF)");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@DATA_NASC", entidade.DataNascimento);
                parametros.Add("@RG", entidade.RG);
                parametros.Add("@CPF", entidade.CPF);

                entidade.ID = DALBase.ExecuteInsert(sql.ToString(), parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Alterar(Aluno entidade)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"UPDATE ALUNO SET NOME = @NOME
                                    , DATA_NASC = @DATA_NASC
                                    , RG = @RG
                                    , CPF = @CPF
                                    WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@ID", entidade.ID);
                parametros.Add("@NOME", entidade.Nome);
                parametros.Add("@DATA_NASC", entidade.DataNascimento);
                parametros.Add("@RG", entidade.RG);
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
                sql.AppendFormat(@"DELETE FROM ALUNO WHERE ID = @ID");

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

        public void PreencherEntidade(Aluno entidade, DataRow dr)
        {
            entidade.ID = Convert.ToInt32(dr["ID"]);
            entidade.Nome = dr["NOME"].ToString() ?? string.Empty;
            entidade.DataNascimento = Convert.ToDateTime(dr["DATA_NASC"]);
            entidade.RG = dr["RG"].ToString() ?? string.Empty;
            entidade.CPF = dr["CPF"] != DBNull.Value ? Convert.ToDouble(dr["CPF"]) : 0;
        }

        public List<Aluno> Listar()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM ALUNO");

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), new Dictionary<string, object>());

                List<Aluno> retorno = new List<Aluno>();

                foreach (DataRow dr in dt.Rows)
                {
                    Aluno aluno = new Aluno();
                    PreencherEntidade(aluno, dr);
                    retorno.Add(aluno);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Aluno> ListarComFiltro(Aluno entidade)
        {
            try
            {
                Dictionary<string, object> paramteros = new Dictionary<string, object>();

                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM ALUNO ");

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

                if (!string.IsNullOrEmpty(entidade.RG))
                {
                    paramteros.Add("@RG", entidade.Nome);

                    if (!sql.ToString().Contains("WHERE"))
                    {
                        sql.AppendLine(" WHERE RG = @RG ");
                    }
                    else
                    {
                        sql.AppendLine(" AND RG = @RG ");
                    }
                }

                if (entidade.DataNascimento != DateTime.MinValue)
                {
                    paramteros.Add("@DATA_NASC", entidade.Nome);

                    if (!sql.ToString().Contains("WHERE"))
                    {
                        sql.AppendLine(" WHERE DATA_NASC = @DATA_NASC ");
                    }
                    else
                    {
                        sql.AppendLine(" AND DATA_NASC = @DATA_NASC ");
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

                List<Aluno> retorno = new List<Aluno>();

                foreach (DataRow dr in dt.Rows)
                {
                    Aluno aluno = new Aluno();
                    PreencherEntidade(aluno, dr);
                    retorno.Add(aluno);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Aluno GetByID(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"SELECT * FROM ALUNO WHERE ID = @ID");

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@ID", id);

                DataTable dt = DALBase.ExecuteSelect(sql.ToString(), parametros);

                Aluno aluno = new Aluno();
                if (dt.Rows.Count > 0)
                {
                    PreencherEntidade(aluno, dt.Rows[0]);
                }

                return aluno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
