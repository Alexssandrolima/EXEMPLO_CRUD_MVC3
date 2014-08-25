using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMADAS.DAL;

namespace CAMADAS.BLL
{
    public class Curso
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

        private DateTime _DataHoraInicial;

        public DateTime DataHoraInicial
        {
            get { return _DataHoraInicial; }
            set { _DataHoraInicial = value; }
        }

        private DateTime _DataHoraFinal;

        public DateTime DataHoraFinal
        {
            get { return _DataHoraFinal; }
            set { _DataHoraFinal = value; }
        }

        #endregion

        #region Methods
        public static List<Curso> Listar()
        {
            return DALCurso.GetInstance().Listar();
        }

        public static List<Curso> Listar(Func<Curso, bool> expression)
        {
            return DALCurso.GetInstance().Listar().Where(expression).ToList();
        }

        public bool Salvar()
        {
            if (!string.IsNullOrEmpty(this.Nome.Trim())
                && this.DataHoraInicial > DateTime.MinValue
                && this.DataHoraFinal > DateTime.MinValue)
            {
                if (this.ID > 0)
                {
                    if (DALCurso.GetInstance().Alterar(this))
                    {
                        return true;
                    }
                    else return false;
                }
                else
                {
                    DALCurso.GetInstance().Incluir(this);
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
            return DALCurso.GetInstance().Excluir(this.ID);
        }

        public static bool Excluir(int id)
        {
            return DALCurso.GetInstance().Excluir(id);
        }

        public void GetByID()
        {
            if (this.ID > 0)
            {
                DALCurso.GetInstance().GetByID(this.ID);
            }
            else
            {
                throw new ApplicationException("O ID deve ser maior que 0");
            }
        }

        public static Curso GetByID(int id)
        {
            if (id > 0)
            {
                return DALCurso.GetInstance().GetByID(id);
            }
            else
            {
                throw new ApplicationException("O ID deve ser maior que 0");
            }
        } 
        #endregion
    }
}
