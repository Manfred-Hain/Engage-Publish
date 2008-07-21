<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Publish.Admin.CommentList" Codebehind="CommentList.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelControl.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<table border="0" class="Normal">
    <tr valign="top">
        <td><dnn:Label ID="lblItemType" ResourceKey="lblItemType" Runat="server" cssClass="Normal" ControlName="cboCategories"></dnn:Label></td>
        <td><asp:DropDownList ID="cboCategories" Runat="server" AutoPostBack="True" CssClass="Normal"></asp:DropDownList></td>
        <td><dnn:Label ID="lblWorkflow" ResourceKey="lblWorkFlow" Runat="server" CssClass="Normal" ControlName="cboWorkFlow"></dnn:Label></td>
        <td><asp:DropDownList ID="cboWorkflow" Runat="server" AutoPostBack="True" CssClass="Normal" OnSelectedIndexChanged="cboWorkflow_SelectedIndexChanged"></asp:DropDownList></td>
    </tr>
</table>
<div id="divArticleRepeater">
    <asp:GridView ID="dgItems" Visible="false" runat="server" 
        EnableViewState="true" 
        AutoGenerateColumns="false" width="100%"
        AlternatingRowStyle-CssClass="DataGrid_AlternatingItem Normal" 
        HeaderStyle-CssClass="DataGrid_Header"
        RowStyle-CssClass="DataGrid_Item Normal"
        PagerStyle-CssClass="Normal"
        CssClass="Normal" 

        AllowPaging="true" 
        PagerSettings-Mode="Numeric" 
        PagerSettings-Visible="true" 
        PageSize="10" 
        OnPageIndexChanging="dgItems_PageIndexChanging"
        >
        <Columns>
            <asp:TemplateField ShowHeader="true"  HeaderText="SelectText" ItemStyle-CssClass="Publish_CheckBoxColumn">
                <ItemTemplate>
                       <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="ArticleName">
                <ItemTemplate>
                    <asp:Label ID="lblCommentId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CommentId") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblItemName" runat="server" CssClass="Normal" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true"  HeaderText="CommentText">
                <ItemTemplate>
                    <asp:Label ID="lblCommentText" runat="server" CssClass="Normal" Text='<%# GetShortCommentText(DataBinder.Eval(Container.DataItem,"CommentText")) %>'></asp:Label>
                    
                     <ajaxToolkit:HoverMenuExtender 
                    id="hme2" 
                    runat="Server" 
                    TargetControlID="lblCommentText" 
                    PopupControlID="PopupMenu" 
                    PopupPosition="Bottom"
                    OffsetX="0" OffsetY="0" 
                    PopDelay="50" />
                         
                    <asp:Panel CssClass="Publish_CommentApproval" ID="PopupMenu" runat="server" style="display:none;">
                        <div class="Normal">
                        <asp:Label ID="lblName" CssClass="Publish_CommentApprovalName" runat="server" Text='<%# BuildName(DataBinder.Eval(Container.DataItem,"FirstName"), DataBinder.Eval(Container.DataItem,"LastName")) %>'></asp:Label>
                        <asp:Label ID="lblEmailAddress" CssClass="Publish_CommentApprovalEmailAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmailAddress") %>'></asp:Label>
                        <div class="Publish_CommentApprovalComment">
                            <%# GetCommentText(DataBinder.Eval(Container.DataItem,"CommentText")) %>
                        </div>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true"  HeaderText="">
                <ItemTemplate>
                       <asp:HyperLink ID="hlEdit" runat="server" CssClass="Normal" NavigateUrl='<%# GetCommentEditUrl(DataBinder.Eval(Container.DataItem,"CommentId")) %>'
                            Text='<%# GetLocalizedEditText() %>'></asp:HyperLink>                                                           
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView></div> 
<asp:label id="lblMessage" runat="server" CssClass="Subhead"></asp:label>
<br />
<div style="text-align:center;">
    <asp:linkbutton cssclass="CommandButton" id="cmdApprove" resourcekey="cmdApprove" runat="server" text="Approve Checked Comments" causesvalidation="False" OnClick="cmdApprove_Click"></asp:linkbutton>
    <asp:linkbutton cssclass="CommandButton" id="cmdDelete" resourcekey="cmdDelete" runat="server" text="Delete Checked Comments" causesvalidation="False" OnClick="cmdDelete_Click"></asp:linkbutton>
    <asp:linkbutton cssclass="CommandButton" id="cmdBack" resourcekey="cmdBack" runat="server" text="Back" causesvalidation="False" OnClick="cmdBack_Click"></asp:linkbutton>
</div>
