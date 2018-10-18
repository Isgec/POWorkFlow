<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="TechnoCommercialNegotiaition.aspx.cs" Inherits="PreOrderWorkflow.TechnoCommercialNegotiaition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-panel {
            background: #ffffff;
            margin: 10px;
            padding: 10px;
            box-shadow: 0px 3px 2px #aab2bd;
            text-align: left;
        }

        .mt {
            margin-top: 12px;
        }

        .row {
            margin-right: -1px;
            margin-left: -1px;
        }
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 61%;
            left: -140px;
            top: -55px;
            padding-left: 15px;
            padding-right: 15px;
            height: 129px;
        }
        .auto-style2 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 33.33333333%;
            left: -171px;
            top: 24px;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 83.33333333%;
            left: -27px;
            top: 7px;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style4 {
            height: 99px;
            width: 472px;
        }
    </style>
   <%-- <script type="text/javascript">
   
        function KeySelected(source, eventArgs) {

          var SupplierCode_Name;
          SupplierCode_Name = document.getElementById('<%=this.txtSupplier.ClientID%>').value
          var SupplierCode_Name_Len = SupplierCode_Name.lastIndexOf('-');
          SupplierCode_Name = SupplierCode_Name.substring(0, SupplierCode_Name_Len - 1);
          document.getElementById('<%=this.txtSupplier.ClientID%>').value = SupplierCode_Name;
          document.getElementById('<%=this.txtSupplier.ClientID%>').focus();     

      }
  
    </script>--%>
     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <div class="mb" runat="server" id="hHeader" style="text-align:center;font-size:16px;font-weight:bold"><i class="fa fa-angle-right"></i></div>
    <asp:HiddenField  runat="server" ID="hdfBuyerId"/> <asp:HiddenField  runat="server" ID="hdfRandomNo"/><asp:HiddenField  runat="server" ID="hdfParentWFID"/>
    <asp:Label runat="server" ID="lblWorkFlowID" />
    <div class="auto-style3">
        <div class="form-panel">
            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Project</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtProject" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Element</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtElement" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Specification No/Details</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSpecification" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">PMDL Doc No</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPMDLDoc" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>


            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Buyer</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtBuyer" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
              <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Project Manager</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtManager" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Supplier Code</label>
                    <div class="col-sm-8">
                                 <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplierCode" ></asp:TextBox>
                            </div>
                </div>
            </div>
             <div class="row mt">
                        <div class="form-group">
                            <label for="pwd" class="col-sm-3">Supplier Name</label>
                            <div class="col-sm-8">
                                 <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplier" ></asp:TextBox>
                            </div>
                        </div>
                    </div>

                <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Supplier Email</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplierEmail"></asp:TextBox>
                
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3"></label>
                    <div class="col-sm-8">
                    </div>
                    <br />
                    <br />
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="auto-style2"></label>
                    <div class="auto-style1">
                         <div="col-sm-10">
                         <div class="auto-style4" />
                        <asp:Button runat="server" ID="btnIdmsReceipt" Text="IDMS Receipt" CssClass="btn btn-sm btn-primary" Height="33px" Visible="true" OnClick="btnIdmsReceipt_Click" />
                             <br />
                             <br />
                             <br />
                        <asp:Button runat="server" ID="btnEnqTechCom" Text="Techno Commercial Negotiation For Enquiry Completed" CssClass="btn btn-sm btn-primary" Height="33px" OnClick="btnSendEnquiry_Click" />
                    </div>
                    <div="col-sm-2">
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
