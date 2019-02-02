 <%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcValidation.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= ViewResx.RegisterVuRx.Title %>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" 
            type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcValidation.js" 
            type="text/javascript"></script>
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= ViewResx.RegisterVuRx.Main %></h2>
    <p>
        <%= ViewResx.RegisterVuRx.MainUsage %>
    </p>
    <p>
        <%= ViewResx.RegisterVuRx.MainPassword %> <%= Html.Encode(ViewData["PasswordLength"]) %> <%= ViewResx.RegisterVuRx.MainLength %>
    </p>

    <%
       Html.EnableClientValidation(); 
       using (Html.BeginForm())
        {
     %>
          
        <%= Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again." )
        %>
        <div>
            <fieldset>
                <legend><%= ViewResx.RegisterVuRx.AccountTitle %></legend>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                
                <p>
                    <input type="submit" value="<%= ViewResx.RegisterVuRx.Title %>" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
