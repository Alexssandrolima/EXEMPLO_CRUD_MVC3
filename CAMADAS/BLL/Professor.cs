using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMADAS.DAL;

namespace CAMADAS.BLL
{
    public class Professor
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

        private double _CPF;

        public double CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }

        #endregion

        #region Methods
        public static List<Professor> Listar()
        {
            return DALProfessor.GetInstance().Listar();
        }

        public static List<Professor> Listar(Func<Professor, bool> expression)
        {
            return DALProfessor.GetInstance().Listar().Where(expression).ToList();
        }

        public bool Salvar()
        {
            if (!string.IsNullOrEmpty(this.Nome.Trim())
                && this.CPF > 0)
            {
                if (this.ID > 0)
                {
                    if (DALProfessor.GetInstance().Alterar(this))
                    {
                        return true;
                    }
                    else return false;
                }
                else
                {
                    DALProfessor.GetInstance().Incluir(this);
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
            return DALProfessor.GetInstance().Excluir(this.ID);
        }

        public static bool Excluir(int id)
        {
            return DALProfessor.GetInstance().Excluir(id);
        }

        public void GetByID()
        {
            if (this.ID > 0)
            {
                DALProfessor.GetInstance().GetByID(this.ID);
            }
            else
            {
                throw new ApplicationException("O ID deve ser maior que 0");
            }
        }

        public static Professor GetByID(int id)
        {
            if (id > 0)
            {
                return DALProfessor.GetInstance().GetByID(id);
            }
            else
            {
                throw new ApplicationException("O ID deve ser maior que 0");
            }
        }
        #endregion

    }
}
