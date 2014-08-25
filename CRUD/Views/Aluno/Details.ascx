<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CRUD.Models.ModelAluno>" %>

<p> 
Nome: 
<%= Model.Nome %>
<br />
Data de Nascimento:
<%= Model.DataNascimento %>
<br />
RG:
<%= Model.RG %>
<br />
CPF:
<%= Model.CPF %>
</p>

