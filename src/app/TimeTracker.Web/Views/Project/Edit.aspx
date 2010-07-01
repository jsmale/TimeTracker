<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TimeTracker.DTO.ProjectResponse>" %>
<%@ Import Namespace="TimeTracker.DTO"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBoxFor(x => x.Name) %>
                <%= Html.ValidationMessageFor(x => x.Name) %>
            </p>
            <p>
                <label for="ProjectCode">Project Code:</label>
                <%= Html.TextBoxFor(x => x.ProjectCode) %>
                <%= Html.ValidationMessageFor(x => x.ProjectCode) %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Delete", "Delete", new { id = Model.Id })%> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

