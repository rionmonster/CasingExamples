<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Casing.aspx.cs" Inherits="CasingExamples.Casing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Casing Examples (C#)</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <!-- Example Textbox -->
        <asp:TextBox ID="ExampleTextBox" Text="this is an example" runat="server"></asp:TextBox>
        <asp:Button ID="CasingButton" runat="server" Text="Test Casing" OnClick="CasingButton_Click" />
        <hr />
        <!-- Example Output -->
        <asp:TextBox ID="Lower" runat="server" Enabled="False"></asp:TextBox><b>(Lower-case)</b><br />
        <asp:TextBox ID="Upper" runat="server" Enabled="False"></asp:TextBox><b>(Upper-case)</b><br />
        <asp:TextBox ID="NativeTitle" runat="server" Enabled="False"></asp:TextBox><b>(Native Title-case)</b><br />
        <asp:TextBox ID="Title" runat="server" Enabled="False"></asp:TextBox><b>(Title-case)</b><br />
        <asp:TextBox ID="Camel" runat="server" Enabled="False"></asp:TextBox><b>(Camel-case)</b><br />
        <asp:TextBox ID="Irish" runat="server" Enabled="False"></asp:TextBox><b>(Irish-case)</b><br />
        <asp:TextBox ID="ExtendedIrish" runat="server" Enabled="False"></asp:TextBox><b>(Extended Irish-case)</b><br />
        <asp:TextBox ID="Custom" runat="server" Enabled="False"></asp:TextBox><b>(Custom-case - all vowels (a,e,i,o,u,y) will be capitalized)</b><br />
    </form>
</body>
</html>
