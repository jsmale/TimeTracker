<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WorkItemDetail>" %>
<%@ Import Namespace="TimeTracker.Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TimeTracker.DTO"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>
    
    <p>Name: <%= Html.Encode(Model.Name) %></p>
    
    <p>Project Task Type: <%= Html.Encode(Model.ProjectTaskTypeName) %></p>
    
    <div id="timeEntries">
    <%Html.RenderPartial("TimeEntryForm",
    	                   new WorkItemTimeEntryDetails
    	                   	{
    	                   		WorkItemId = Model.Id,
										StartTime = Model.StartTime
    	                   	});%>
	</div>

</asp:Content>
