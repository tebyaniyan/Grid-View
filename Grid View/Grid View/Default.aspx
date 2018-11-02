<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <br />
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
        <br />
        <br />
<asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False" DataKeyNames="ID"
              DataSourceID="SqlDataSource1">
<Columns>
<asp:BoundField DataField="ID" HeaderText="ID" 
                InsertVisible="False" ReadOnly="True"
                               SortExpression="ID" />
<asp:BoundField DataField="ImageName" HeaderText="ImageName" 
                               SortExpression="ImageName" />
<asp:TemplateField HeaderText="Image">
<ItemTemplate>
<asp:Image ID="Image1" runat="server" 
           ImageUrl='<%# "Handler.ashx?ID=" + Eval("ID")%>'/>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
SelectCommand="SELECT [ID], [ImageName], [Image] 
              FROM [Images]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
