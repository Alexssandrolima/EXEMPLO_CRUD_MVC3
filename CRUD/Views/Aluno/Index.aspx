<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CRUD.Models.ModelAluno>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

    <h2>
        Gerenciar Alunos</h2>
        <% Html.EnableClientValidation(); %>
    <table>
        <tr>
            <th>
                Aluno
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr name="<%= item.ID %>">
            <td>
                <span style="width: 250px; display: block; float: left">
                    <%= item.Nome %>
                </span>
                <span style="display: block; float: left">
                <a name="<%= item.ID %>" class="edit" href="#<%= item.ID%>"> Editar </a>
                | <a name="<%= item.ID %>" class="detalhes" href="#<%= item.ID%>">Detalhes</a>
                | <a name="<%= item.ID %>" class="excluir" href="#<%= item.ID%>">Excluir</a>
                </span>
                <br />
                <div class="Content" name="<%= item.ID %>">
                </div>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("Novo Aluno", "Create") %>
    </p>
    <script type="text/javascript">
    var isComplete = true;
    var lastId = 0;
    var lastAction;

    function updateContent(object, action) {
        var id = object.attr("name");
        if (isComplete && (lastId != id || lastAction != action)){
            isComplete = false;
            lastId = id;
            lastAction = action;
            $(".Content").slideUp("fast");
            $.ajax({
                url: action,
                data: { id: id },
                type: "get",
                dataType: "json",
                success: function (data) {
                    if (!data.IsErro){
                    $(".Content[name="+id+"]").html(data.Html);
                        //$("#Content").html(data.Html);
                    } else {
                    $(".Content[name="+id+"]").html(data.Message);
                        //$("#Content").html(data.Message);
                    }
                },
                complete: function (data) {
                    $(".Content[name="+id+"]").slideDown("fast");
                    //$("#Content").slideDown("fast");
                    isComplete = true;
                }
            });
        } else if ( lastId == id) {
                $(".Content[name="+id+"]").slideUp("fast");
                lastId=0;
                isComplete = true;
        }
    }

    $(".detalhes").live("click", function (e) {
        updateContent($(this), "<%= Url.Action("Details") %>");
            return false;
        });

    $(".edit").live("click", function (e) {
    updateContent($(this), "<%= Url.Action("Edit") %>");
        return false;
    });

    $(".excluir").live("click", function (e) {
        var id = $(this).attr("name");
        $.ajax({
            url: "<%= Url.Action("Delete") %>",
            data: { id: id },
            type: "get",
            dataType: "json",
            complete: function (data) {
                if (!data.IsErro){
                    $("tr[name="+id+"]").remove();
                } else {
                    $(".Content[name="+id+"]").html(data.Message);
                }
            }
        });
    });

    $("#formEdit").live("submit", function (e) {
            FormSerializado = $(this).serialize();
            $.post(
                "<%= Url.Action("Edit") %>",
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
