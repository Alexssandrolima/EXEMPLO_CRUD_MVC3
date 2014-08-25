using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMADAS.DAL;

namespace CAMADAS.BLL
{
    public class Aluno
    {
        #region Properties
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Nome;

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        private DateTime _DataNascimento;

        public DateTime DataNascimento
        {
            get { return _DataNascimento; }
            set { _DataNascimento = value; }
        }

        private string _RG;

        public string RG
        {
            get { return _RG; }
            set { _RG = value; }
        }

        private double _CPF;

        public double CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }
        #endregion

        #region Methods
        public static List<Aluno> Listar()
        {
            return DALAluno.GetInstance().Listar();
        }

        public static List<Aluno> Listar(Func<Aluno, bool> expression)
        {
            return DALAluno.GetInstance().Listar().Where(expression).ToList();
        }

        public bool Salvar()
        {
            if (!string.IsNullOrEmpty(this.Nome.Trim())
                && this.DataNascimento > DateTime.MinValue 
                && !string.IsNullOrEmpty(this.RG.Trim()) )
            {
                if (this.ID > 0)
                {
                    if (DALAluno.GetInstance().Alterar(this))
                    {
                        return true;
                    }
                    else return false;
                }
                else
                {
                    DALAluno.GetInstance().Incluir(this);
                    return this.ID > 0;
                }
            }
            else
            {
                throw new ApplicationException("Argumentos inválidos");
            }
        }

        public bool Excluir()
        {
            return DALAluno.GetInstance().Excluir(this.ID);
        }

        public static bool Excluir(int id)
        {
            return DALAluno.GetInstance().Excluir(id);
        }

        public void GetByID()
        {
            if (this.ID > 0)
            {
                DALAluno.GetInstance().GetByID(this.ID);
            }
            else
            {
                throw new ApplicationException("O ID deve ser maior que 0");
            }
        }

        public static Aluno GetByID(int id)
        {
            if (id > 0)
            {
                return DALAluno.GetInstance().GetByID(id);
            }
            else
            {
                throw new ApplicationException("O ID deve ser maior que 0");
            }
        }
        #endregion
    }
}
