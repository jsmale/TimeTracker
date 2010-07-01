<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TimeTracker.Web.Controllers"%>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink<AccountController>(x => x.LogOff(), "Log Off") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink<AccountController>(x => x.LogOn() ,"Log On") %> ]
<%
    }
%>
