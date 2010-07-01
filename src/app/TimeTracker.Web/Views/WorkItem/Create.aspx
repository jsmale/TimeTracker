<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CreateWorkItemView>" %>
<%@ Import Namespace="TimeTracker.DTO"%>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="System.Web.Script.Serialization"%>
<%@ Import Namespace="TimeTracker.Web.Models"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
		
	<script language="javascript" type="text/javascript">
		$(document).ready(function() {
			var data = <%= new JavaScriptSerializer().Serialize(Model.ProjectTaskTypes) %>;
			$("#projectTaskTypeSelect").autocomplete(data, {
				formatItem: function(item) {
					return item.Text;
				}
			}).result(function(event, item) {
				$("#<%=Html.IdFor(x => x.CreateWorkItemRequest.ProjectTaskTypeId)%>").val(item.Value);
			});
		});
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>
	<% Html.EnableClientValidation(); %>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
	
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
					<%= Html.LabelFor(x => x.CreateWorkItemRequest.Name) %>:
                <%= Html.TextBoxFor(x => x.CreateWorkItemRequest.Name) %>
                <%= Html.ValidationMessageFor(x => x.CreateWorkItemRequest.Name, "*")%>
            </p>
            <p>
                <label for="projectTaskTypeSelect">Project Task Type:</label>
                <input id="projectTaskTypeSelect" type="text" />
                <%= Html.HiddenFor(x => x.CreateWorkItemRequest.ProjectTaskTypeId) %>
					<%= Html.ValidationMessageFor(x => x.CreateWorkItemRequest.ProjectTaskTypeId, "*")%>
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

