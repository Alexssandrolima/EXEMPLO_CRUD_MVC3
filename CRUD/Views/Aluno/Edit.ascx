<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CRUD.Models.ModelAluno>" %>
<% using (Html.BeginForm("Edit", "Aluno", FormMethod.Post, new { @class = "formEdit" }))
   {%>
<%= Html.ValidationSummary(true)%>
<fieldset>
    <div id="Message" style="font-size: large">
    </div>
    <%= Html.HiddenFor(m => m.ID) %>
    <div class="editor-label">
        <%= Html.LabelFor(m => m.Nome)%>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(m => m.Nome, new { @maxlength = "510" })%>
        <%= Html.ValidationMessageFor(m => m.Nome)%>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(m => m.DataNascimento)%>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(m => m.DataNascimento, new { @maxlength = "10", @class = "DatePicker Data" })%>
        <%= Html.ValidationMessageFor(m => m.DataNascimento)%>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(m => m.RG)%>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(m => m.RG, new { @maxlength = "12", @class = "RG" })%>
        <%= Html.ValidationMessageFor(m => m.RG)%>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(m => m.CPF)%>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(m => m.CPF, new { @maxlength = "13", @class = "CPF" })%>
        <%= Html.ValidationMessageFor(m => m.CPF)%>
    </div>
    <p>
        <input type="submit" value="Salvar" />
    </p>
</fieldset>
<% } %>