using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using CAMADAS.BLL;

namespace CRUD.Models
{
    public class ModelProfessor
    {
        #region Properties
        public int ID;

        public string Nome;

        public string CPF; 
        #endregion

        #region Construtores
        public ModelProfessor()
        {

        }

        public ModelProfessor(int id)
        {
            this.Preencher(Professor.GetByID(id));
        } 
        #endregion

        #region Metodos
        public static List<ModelProfessor> Listar()
        {
            List<Professor> lista = Professor.Listar();

            List<ModelProfessor> retorno = new List<ModelProfessor>();

            foreach (Professor item in lista)
            {
                ModelProfessor prof = new ModelProfessor();
                prof.Preencher(item);
                retorno.Add(prof);
            }

            return retorno;
        }

        public static List<ModelProfessor> Listar(Func<Professor, bool> expression)
        {
            List<Professor> lista = Professor.Listar(expression);

            List<ModelProfessor> retorno = new List<ModelProfessor>();

            foreach (Professor item in lista)
            {
                ModelProfessor prof = new ModelProfessor();
                prof.Preencher(item);
                retorno.Add(prof);
            }

            return retorno;
        }

        public void Preencher(Professor professor)
        {
            this.ID = professor.ID;
            this.Nome = professor.Nome;
            // A ideia é trazer formatado aqui.
            this.CPF = professor.CPF.ToString();
        }

        public void Salvar()
        {
            Professor professor = new Professor();
            professor.ID = this.ID;
            professor.Nome = this.Nome;
            // Aqui extrair somente os números.
            professor.CPF = Convert.ToDouble(this.CPF);
            professor.Salvar();
        }
        #endregion
    }
}