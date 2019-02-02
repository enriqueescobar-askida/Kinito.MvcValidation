<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.ActionLink("English", "ChangeCulture", "Account", new { lang = "en", returnUrl = this.Request.RawUrl }, null)%>

<%= Html.ActionLink("Français", "ChangeCulture", "Account", new { lang = "fr", returnUrl = this.Request.RawUrl }, null)%>
