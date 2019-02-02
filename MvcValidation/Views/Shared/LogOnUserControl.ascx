<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink(ViewResx.SharedVuRx.LogOff, "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink(ViewResx.SharedVuRx.LogOn, "LogOn", "Account") %> ]
<%
    }
%>
