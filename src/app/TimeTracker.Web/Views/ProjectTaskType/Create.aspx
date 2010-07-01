<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CreateProjectTaskTypeView>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TimeTracker.Web.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name") %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
					<label for="Project">Project:</label>
					<%= Html.DropDownList("ProjectId", Model.Projects, "Please select") %>
            </p>
            <p>
					<label for="Project">Task:</label>
					<%= Html.DropDownList("TaskId", Model.Tasks, "Please select")%>
            </p>
            <p>
					<label for="Project">Task Type:</label>
					<%= Html.DropDownList("TaskTypeId", Model.TaskTypes, "Please select")%>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

