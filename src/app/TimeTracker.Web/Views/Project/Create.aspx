<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TimeTracker.DTO.CreateProjectRequest>" %>
<%@ Import Namespace="TimeTracker.DTO"%>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>
<div id="validationSummary">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
</div>    
<div id="created"></div>
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
				<%=Html.EditorFor(x => x)%>
            <%--<p>
                <label for="Name">Name:</label>
                <%= Html.TextBoxFor(x => x.Name) %>
                <%= Html.ValidationMessageFor(x => x.Name, "*") %>
            </p>
            <p>
                <label for="ProjectCode">Project Code:</label>
                <%= Html.TextBoxFor(x => x.ProjectCode) %>
                <%= Html.ValidationMessageFor(x => x.ProjectCode, "*") %>
            </p>--%>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>
    
    <%= Html.ClientSideValidation(typeof(CreateProjectRequest))
				 .UseValidationSummary("validationSummary", "Create was unsuccessful. Please correct the errors and try again.")%>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

