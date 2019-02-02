<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OldWebForm.aspx.cs" Inherits="MvcValidation.OldWebForm" %>

<script runat="server">

    protected void Page_Load()
    {
        if(IsPostBack && IsValid)
        {
            Response.Write("Hooray!");
        }
    }
    
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">       
        <asp:TextBox runat="server" ID="_userName" />
        <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="_userName"
                                    ErrorMessage="Please enter a username" />
        <asp:Button runat="server" ID="_submit" Text="Submit" />
    </form>
</body>
</html>
