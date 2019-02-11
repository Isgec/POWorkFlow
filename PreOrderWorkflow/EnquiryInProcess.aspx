<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="EnquiryInProcess.aspx.cs" Inherits="PreOrderWorkflow.EnquiryInPrecess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; font-size: 16px; font-weight: bold">Enquiry In Process</div>
    <div class="container">
        <div class="row">
            <div class="col-lg-12" style="text-align: end">
                <asp:Button ID="btn_refresh" runat="server" Text="Fetch All Enquiry" class="btn btn-default btn-sm" OnClick="btn_refresh_Click" />

            </div>
        </div>
        <div class="col-lg-12" style="background-color: #fff; min-height: 600px; font-size: 11px; margin-top: 20px">
            <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="95%" CssClass="table table-bordered table-hover" HeaderStyle-BackColor="#e9e9e9">
                <Columns>
                    <asp:TemplateField HeaderText="WFID">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lbl_headerwfid" Text="WFID"></asp:Label>
                            <asp:TextBox runat="server" ID="txt_searchwfid" Width="42px" AutoPostBack="true" Value=' <%#Eval("WFID") %>' OnTextChanged="txt_wfidonTextChange"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkViewWorkflow" Text=' <%#Eval("WFID") %>' CommandArgument='<%#Eval("WFID") %>' OnClick="lnkViewWorkflow_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lbl_headerproj" Text="Project"></asp:Label>
                            <asp:TextBox runat="server" ID="txt_searchproj" Width="80px" AutoPostBack="true" Value=' <%#Eval("WFID") %>' OnTextChanged="txt_wfidonTextChange"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Project") %>' ID="Project"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Element">

                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Element") %>' ID="Element"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Specification No">
                        <ItemTemplate>
                            <%#Eval("SpecificationNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PMDL Doc No">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Eval("PMDLDocNo") %>' ID="PMDLDocNo"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Buyer">
                        <ItemTemplate>
                            <%#Eval("BuyerName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Manager">
                        <ItemTemplate>
                            <%#Eval("ManagerName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--    <asp:TemplateField HeaderText="Supplier">
                        <ItemTemplate>
                      <%#Eval("SupplierName") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="WF Status">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lbl_headerstatus" Text="WF Status"></asp:Label>

                            <asp:DropDownList ID="ddl_srchstatus" Width="90px" runat="server" AutoPostBack="true" OnTextChanged="txt_wfidonTextChange">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Enquiry in progress">Enquiry in progress</asp:ListItem>
                                <asp:ListItem Value="Technical Specification Released">Technical Specification Released</asp:ListItem>
                                <asp:ListItem Value="All Offer Received">All Offer Received</asp:ListItem>
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#Eval("WF_Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="User">
                        <ItemTemplate>
                            <%#Eval("EmployeeName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DateTime">
                        <HeaderTemplate>
                             <asp:Label runat="server" ID="lbl_headerDate" Text="DateTime"></asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="txtDateCss" AutoPostBack="true" AutoComplete="off" OnTextChanged="txt_wfidonTextChange"></asp:TextBox>
                            <ajax:calendarextender id="txtDateCalendarExtender" runat="server" targetcontrolid="txtDate"
                                popupposition="BottomRight" format="dd/MM/yyyy">
                            </ajax:calendarextender>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#Convert.ToDateTime(Eval("DateTime")).ToString("dd-MM-yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Raise Enquiry" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="btn-link" runat="server" ID="btnRaiseEnquiry" OnClick="btnRaiseEnquiry_Click" CommandArgument='<%#Eval("WFID") %>' Text='<i class="fa fa-mail-forward" style="font-size:16px"></i>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View Enquiry" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="" runat="server" ID="btnView" OnClick="btnView_Click" CommandArgument='<%#Eval("WFID") %>' Text='<i class="fa fa-address-book" aria-hidden="true" style="font-size:16px"></i>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Return" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton Font-Size="11" runat="server" ID="btnReturn" OnClick="btnReturn_Click" CommandArgument='<%#Eval("WFID") %>' Visible='<%#GetVisible(Eval("WF_Status").ToString())%>' Text='<i class="fa fa-backward" aria-hidden="true" style="font-size:16px"></i>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="All Offer Received" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton Font-Size="11" runat="server" ID="btnAllOfferReceieved" OnClientClick="return confirm('Are you sure ?');" OnClick="btnAllOfferReceieved_Click" CommandArgument='<%#Eval("WFID") %>' Visible='<%#GetAllOfferReceiveVisible(Eval("WF_Status").ToString())%>' Text='<i class="fa fa-check-square-o" aria-hidden="true" style="font-size:16px"></i>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commercial offer Finalized" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton Font-Size="11" runat="server" ID="btnClosed" OnClick="btnClosed_Click" CommandArgument='<%#Eval("WFID") %>' ForeColor="Red" Text='<i class="fa fa-unlock-alt" aria-hidden="true" style="font-size:16px"></i>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
