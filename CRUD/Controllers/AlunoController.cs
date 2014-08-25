using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class AlunoController : BaseController
    {
        //
        // GET: /Aluno/

        public ActionResult Index()
        {
            return View(ModelAluno.Listar());
        }

        //
        // GET: /Aluno/Details/5
        public ActionResult Details(int id)
        {
            var Content = RenderPartial("Details", ModelAluno.Consultar(id));

            return Json(new { Message = "", Html = Content, IsErro = false}, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Aluno/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Aluno/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelAluno aluno = new ModelAluno();
                    aluno.Nome = collection["Nome"].ToString();
                    aluno.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
                    aluno.RG = collection["RG"].ToString();
                    aluno.CPF = collection["CPF"].ToString();

                    aluno.Salvar();
                }

                return Json(new { Message = "Sucesso.", IsErro = false });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Erro: " + ex.Message, IsErro = true });
            }
        }
        
        //
        // GET: /Aluno/Edit/5
 
        public ActionResult Edit(int id)
        {
            var Content = RenderPartial("Edit", ModelAluno.Consultar(id));

            return Json(new { Message = "", Html = Content, IsErro = false }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Aluno/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelAluno aluno = new ModelAluno();
                    aluno.ID = Convert.ToInt32(collection["ID"]);
                    aluno.Nome = collection["Nome"].ToString();
                    aluno.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
                    aluno.RG = collection["RG"].ToString();
                    aluno.CPF = collection["CPF"].ToString();

                    aluno.Salvar();
                }

                return Json(new { Message = "Sucesso.", IsErro = false });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Erro: " + ex.Message, IsErro = true });
            }
        }

        //
        // GET: /Aluno/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (ModelAluno.Excluir(id))
                return Json(new { Message = "Sucesso.", IsErro = false });
            else 
                return Json(new { Message = "Falha ao excluir.", IsErro = true });
        }
    }
}
