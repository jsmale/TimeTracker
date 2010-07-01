<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ReportOutput>" %>
<%@ Import Namespace="TimeTracker.DTO"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Today
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Today</h2>
<table>
	<thead>
		<tr>
			<th>Project</th>
			<th>Task</th>
			<th>Type</th>
			<th>Hours</th>
		</tr>
	</thead>
	<tbody>
		<%
		double totalHours = 0;	
		foreach (var detail in Model.ReportDetails)
		{
			var hours = detail.Hours[0];
			if (hours == 0) continue;
			totalHours += hours;
			
			%>
			<tr>
				<td><%=detail.Project %></td>
				<td><%=detail.Task %></td>
				<td><%=detail.Type %></td>
				<td><%=hours.ToString("0.00")%></td>
			</tr>
			<%
		}
		%>		
	</tbody>
	<tfoot>
	<tr>
	<th>&nbsp;</th>
	<th>&nbsp;</th>
	<th>Total:</th>
	<th><%=totalHours.ToString("0.00")%></th>
	</tr>
	</tfoot>
</table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
