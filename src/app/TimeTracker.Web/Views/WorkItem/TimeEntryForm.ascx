<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<WorkItemTimeEntryDetails>" %>
<%@ Import Namespace="TimeTracker.Web.Controllers"%>
<%@ Import Namespace="TimeTracker.DTO"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%	
    if (Model.StartTime != null)
    {   
        %>
        Start Time: <%= Model.StartTime.ToString() %>
        <%
        using (Ajax.BeginForm("Stop", new { id = Model.WorkItemId }, new AjaxOptions { UpdateTargetId = "timeEntries" }))
        {
        %>
	        Stop Time: <%=Html.TextBox("stopTime")%> <%=Html.SubmitButton("submit", "Stop")%> 		
        <%
        }
    }
    else
    {
        using (Ajax.BeginForm("Start", new {id=Model.WorkItemId}, new AjaxOptions{UpdateTargetId = "timeEntries"}))
        {
        %>
            Start Time: <%=Html.TextBox("startTime") %> <%=Html.SubmitButton("submit", "Start") %> 		
        <%
        }
    }   
        	
    Html.RenderAction<WorkItemController>(x => x.GetTimeEntries(Model.WorkItemId));
%>
 
