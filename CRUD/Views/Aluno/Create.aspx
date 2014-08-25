<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CRUD.Models.ModelAluno>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

    <h2>Novo</h2>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Create","Aluno", FormMethod.Post, new { @id="formCreate"}))
       {%>

        <fieldset>
            <legend>Aluno</legend>
            <div id="Message" style="font-size: large" ></div>
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
    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

    <script type="text/javascript">
        $("#formCreate").live("submit", function (e) {
            FormSerializado = $(this).serialize();
            $.post(
                $("#formCreate").attr("action"),
                FormSerializado,
                function (data) {
                    if (data.IsError) {
                        $("#Message").css("color", "red");
                    } else {
                        $("#Message").css("color", "green");
                    }

                    $("#Message").html(data.Message);
                });
            return false;
        });
    </script>

</asp:Content>

