<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TimeTracker.DTO.ReportOutput>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Weekly Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Weekly Report</h2>

<table>
	<thead>
		<tr>
			<th>Day</th>
			<th>Project</th>
			<th>Task</th>
			<th>Type</th>
			<th>Requirement</th>
			<th>Comment</th>
			<th>Hours</th>
		</tr>
	</thead>
	<tbody>
		<%
		var startDate = Model.StartDate;	
		for (int i = 0; i < Model.NumberOfDays; i++)
		{
			foreach (var detail in Model.ReportDetails)
			{
				if (detail.Hours[i] == 0) continue;
				
				%>
				<tr>
					<td><%=startDate.AddDays(i).ToString("M/d/yyyy")%></td>
					<td><%=detail.Project %></td>
					<td><%=detail.Task %></td>
					<td><%=detail.Type %></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td><%=detail.Hours[i].ToString("0.00") %></td>
				</tr>
				<%
			}
		}	
		%>
	</tbody>
</table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
