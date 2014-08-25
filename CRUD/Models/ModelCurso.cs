using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using CAMADAS.BLL;

namespace CRUD.Models
{
    public class ModelCurso
    {
        public int ID;
        public string Nome;
        public DateTime DataHoraInicial;
        public DateTime DataHoraFinal;

        public  ICollection<ModelAluno> Alunos;
        public ModelProfessor Professor;

        public void Preencher(Curso curso)
        {
            this.ID = curso.ID;
            this.Nome = curso.Nome;
            this.DataHoraInicial = curso.DataHoraInicial;
            this.DataHoraFinal = curso.DataHoraFinal;
        }

        public static List<ModelCurso> Listar(Func<Curso, bool> expression)
        {
            List<Curso> lista = Curso.Listar(expression);

            List<ModelCurso> retorno = new List<ModelCurso>();

            foreach (Curso item in lista)
            {
                ModelCurso curso = new ModelCurso();
                curso.Preencher(item);
                retorno.Add(curso);
            }

            return retorno;
        }

        public static List<ModelCurso> Listar()
        {
            List<Curso> lista = Curso.Listar();

            List<ModelCurso> retorno = new List<ModelCurso>();

            foreach (Curso item in lista)
            {
                ModelCurso curso = new ModelCurso();
                curso.Preencher(item);
                retorno.Add(curso);
            }

            return retorno;
        }

    }
}