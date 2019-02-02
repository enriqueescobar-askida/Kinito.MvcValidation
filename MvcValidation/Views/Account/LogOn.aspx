<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcValidation.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewResx.LogOnVuRx.Title %>
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= ViewResx.LogOnVuRx.Main %></h2>
    <p>
        <%= ViewResx.LogOnVuRx.Please %> <%= Html.ActionLink(ViewResx.LogOnVuRx.Link, "Register") %> <%= ViewResx.LogOnVuRx.IfUDont %>
    </p>
    <%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend><%= ViewResx.LogOnVuRx.AccountTitle %></legend>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.CheckBoxFor(m => m.RememberMe) %>
                    <%= Html.LabelFor(m => m.RememberMe) %>
                </div>
                
                <p>
                    <input type="submit" value="<%= ViewResx.LogOnVuRx.Title %>" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
