using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAMADAS.BLL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRUD.Models
{
    public class ModelAluno
    {
        #region Properties
        public int ID;

        [Required(ErrorMessage="Campo obrigatório")]
        [StringLength(510, ErrorMessage="Limite de caracteres ultrapassado.")]
        [RegularExpression(@"[a-zA-Z]", ErrorMessage="Digite somente letras.")]
        public string Nome;

        [Required(ErrorMessage="Campo obrigatório")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
            , ErrorMessage="Data inválida.")]
        public DateTime DataNascimento;

        [Required]
        [RegularExpression(@"^[0-9]{2}\.[0-9]{3}\.[0-9]{3}\.[0-9X]$", ErrorMessage="RG inválido.")]
        public string RG;

        // Para trazer formatado
        [RegularExpression(@"^[0-9]{3}\.[0-9]{3}\.[0-9]{3}\.[0-9]{2}$",ErrorMessage="CPF inválido.")]
        public string CPF; 
        #endregion

        #region Metodos
        public static List<ModelAluno> Listar()
        {
            List<Aluno> lista = Aluno.Listar();
            List<ModelAluno> retorno = new List<ModelAluno>();

            foreach (Aluno item in lista)
            {
                ModelAluno aluno = new ModelAluno();
                aluno.Preencher(item);
                retorno.Add(aluno);
            }

            return retorno;
        }

        public List<ModelAluno> Listar(Func<Aluno, bool> expression)
        {
            List<Aluno> lista = Aluno.Listar(expression);
            List<ModelAluno> retorno = new List<ModelAluno>();

            foreach (Aluno item in lista)
            {
                ModelAluno aluno = new ModelAluno();
                aluno.Preencher(item);
                retorno.Add(aluno);
            }

            return retorno;
        }

        public void Preencher(Aluno aluno)
        {
            this.ID = aluno.ID;
            this.DataNascimento = aluno.DataNascimento;
            this.Nome = aluno.Nome;
            this.RG = aluno.RG;
            this.CPF = aluno.CPF.ToString();
        }

        public void Salvar()
        {
            Aluno aluno = new Aluno();

            aluno.ID = this.ID;
            aluno.Nome = this.Nome;
            aluno.DataNascimento = this.DataNascimento;
            aluno.RG = this.RG;
            aluno.CPF = Convert.ToDouble(string.IsNullOrEmpty(this.CPF.Replace(".", string.Empty).Replace("-", "")) ? "0" : this.CPF.Replace(".", string.Empty).Replace("-", ""));

            aluno.Salvar();
        }

        public static ModelAluno Consultar(int id)
        {
            ModelAluno aluno = new ModelAluno();
            aluno.Preencher(Aluno.GetByID(id));

            return aluno;
        }

        public static bool Excluir(int id)
        {
            return Aluno.Excluir(id);
        }

        public bool Excluir()
        {
            return Aluno.Excluir(this.ID);
        }

        #endregion
    }
}