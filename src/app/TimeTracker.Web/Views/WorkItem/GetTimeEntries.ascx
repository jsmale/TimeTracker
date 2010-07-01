<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<TimeEntryDetail>>" %>
<%@ Import Namespace="TimeTracker.DTO"%>

<%
    foreach (var timeEntryDetail in Model)
    { %>
     
     Start Time: <%= timeEntryDetail.StartTime %><br />
     End Time: <%= timeEntryDetail.EndTime%><br />
     
     <%
        
    }
    
 %>
 
 