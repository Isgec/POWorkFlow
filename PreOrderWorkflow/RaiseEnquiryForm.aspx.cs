using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PreOrderWorkflow
{
    public partial class RaiseEnquiryForm : System.Web.UI.Page
    {
        WorkFlow objWorkFlow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Status"] == "Enquiry Raised")
                {
                    btnSendEnquiry.Text = "Send Enquiry";
                    hHeader.InnerHtml = "Send Enquiry";

                }
                if (Request.QueryString["Status"] == "Technical offer Received")
                {
                    int nCount = 0;
                    objWorkFlow = new WorkFlow();
                    objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFPID"]);
                     nCount = objWorkFlow.SelectAllOfferReceivedRFQCount();
                    if (nCount > 0)
                    {
                        btnSendEnquiry.Visible = false;
                        ddlSupplier.Enabled = false;
                        txtSupplier.Enabled = false;
                        txtSupplierEmail.Enabled = false;
                        trNotes.Visible = false;
                        txtNotes.Text = "";
                    }
                    else
                    {
                        btnSendEnquiry.Text = "Technical offer Received";
                        // ddlSupplier.Enabled = false;
                        txtSupplier.Enabled = false;
                        txtSupplierEmail.Enabled = false;
                        hHeader.InnerHtml = "View Enquiry";
                        trNotes.Visible = false;
                        txtNotes.Text = "";
                        btnIdmsReceipt.Visible = false;
                        // Changed this as now we have to display this button on Techno Negotiation completed page, Change- 18-10-18
                        //btn Generate IDMS Receipt. visible= true
                    }
                }
                if (Request.QueryString["Status"] == "Isgec Comment Submitted")
                {
                    btnSendEnquiry.Text = "Release comments";
                    hHeader.InnerHtml = "View Enquiry";
                    ddlSupplier.Enabled = false;
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }

                if (Request.QueryString["Status"] == "Commercial offer Received")
                {
                    btnSendEnquiry.Text = "Commercial offer Received";
                    hHeader.InnerHtml = "View Enquiry";
                    ddlSupplier.Enabled = false;
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";

                }
                if (Request.QueryString["Status"] == "done")
                {
                    btnSendEnquiry.Visible = false;
                    hHeader.InnerHtml = "View Enquiry";
                    ddlSupplier.Enabled = false;
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }
                if (Request.QueryString["Status"] == "All Offer Received")
                {
                    btnSendEnquiry.Visible = false;
                    hHeader.InnerHtml = "View Enquiry";
                    ddlSupplier.Enabled = false;
                    txtSupplier.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    trNotes.Visible = false;
                    txtNotes.Text = "";
                }

                if (ddlSupplier.Enabled)
                {
                    GetSupplier();
                }
                GetData();
                if (Request.QueryString["Status"] == "Enquiry For Techno Commercial Negotiation Completed")
                {
                    Response.Redirect("TechnoCommercialNegotiaition.aspx?WFID=" + objWorkFlow.WFID + "&Status=Technical offer Received&u=" + Request.QueryString["u"] + "&WFPID=" + hdfParentWFID.Value);

                }
            }
        }
        private void GetData()

        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);
            DataTable dt = objWorkFlow.GetWFById();

            if (dt.Rows.Count > 0)
            {
                txtProject.Text = dt.Rows[0]["Project"].ToString();
                txtElement.Text = dt.Rows[0]["Element"].ToString();
                txtSpecification.Text = dt.Rows[0]["SpecificationNo"].ToString();
                txtPMDLDoc.Text = dt.Rows[0]["PMDLDocNo"].ToString();
                if (dt.Rows[0]["BuyerName"].ToString() != "" && dt.Rows[0]["BuyerName"].ToString() != null)
                {
                    txtBuyer.Text = dt.Rows[0]["BuyerName"].ToString() + "-" + dt.Rows[0]["Buyer"].ToString();
                }
                if (dt.Rows[0]["ManagerName"].ToString() != "" && dt.Rows[0]["ManagerName"].ToString() != null)
                {
                    txtManager.Text = dt.Rows[0]["ManagerName"].ToString() + "-" + dt.Rows[0]["Manager"].ToString();
                }
                txtSupplierEmail.Text = dt.Rows[0]["Supplier"].ToString();
                txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();
                //if (ddlSupplier.Enabled)

                //{
                // ddlSupplier.SelectedItem.Text = dt.Rows[0]["SupplierCode"].ToString();
                if (dt.Rows[0]["SupplierCode"].ToString() != "")
                {
                    ddlSupplier.SelectedValue = dt.Rows[0]["SupplierCode"].ToString();
                }
                else
                {
                    ddlSupplier.SelectedValue = "Select";
                }

                if (Request.QueryString["Status"] == "Enquiry Raised")
                {
                    ddlSupplier.Enabled = true;
                }
                else
                {
                    ddlSupplier.Enabled = false;
                }
                
                //  ddlSupplier.Items.FindByText(dt.Rows[0]["SupplierCode"].ToString()).Selected = true;
                // txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();    
                //}
                //else
                //{
                ////}
                txtEmailSub.Text = dt.Rows[0]["EmailSubject"].ToString();
                hdfBuyerId.Value = dt.Rows[0]["Buyer"].ToString();
                hdfRandomNo.Value = dt.Rows[0]["RandomNo"].ToString();
                hdfParentWFID.Value = dt.Rows[0]["Parent_WFID"].ToString();

                Session["Project"] = txtProject.Text;
                Session["Element"] = txtElement.Text;
                Session["SpecificationNo"] = txtSpecification.Text;
                Session["PMDLDocNo"] = txtPMDLDoc.Text;
                Session["BuyerName"] = txtBuyer.Text;
                Session["ManagerName"] = txtManager.Text;
                Session["SupplierEmail"] = txtSupplierEmail.Text;
                Session["SupplierCode"] = ddlSupplier.SelectedValue;
                Session["SupplierName"] = txtSupplier.Text;
                Session["BuyerId"] = hdfBuyerId.Value;
                Session["RandomNo"] = hdfRandomNo.Value;
                Session["Parent_WFID"] = hdfParentWFID.Value;

               
            }
        }
        private void GetSupplier()
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetSupplierCode();
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataValueField = "SupplierCode";
            ddlSupplier.DataTextField = "drpdwn";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, "Select");
        }
        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetSupplierMailId(ddlSupplier.SelectedValue);
            txtSupplier.Text = dt.Rows[0][1].ToString();
            txtSupplierEmail.Text = dt.Rows[0][2].ToString();
            // GetSpecification();
            // txtSpecification.Text = "";
        }


        [WebMethod]
        public static string[] GetSupplier(string prefixText, int count)
        {
            WorkFlow objWorkFlow = new WorkFlow();
            objWorkFlow.Supplier = prefixText;
            DataTable dt = objWorkFlow.GetSupplier();
            List<string> lst = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string Supplier = dr["SupplierName"].ToString() + "-" + dr["SupplierCode"].ToString();
                lst.Add(Supplier);
            }
            return lst.ToArray();


        }

        protected void btnSendEnquiry_Click(object sender, EventArgs e)
        {
            string RandomNo = string.Empty;
            WorkFlow objWorkFlow = new WorkFlow();
            if (hdfRandomNo.Value == "")
            {
                RandomNo = GetRandomAlphanumericString(8);
            }

            if (txtSupplierEmail.Text != "")
            {
                string MailTo = string.Empty;

                //if (ddlSupplier.SelectedValue.ToString().Contains("-"))
                //{
                //    string[] supplier = txtSupplier.Text.Split('-');

                //    if (supplier[1] !=null && Regex.IsMatch(supplier[1], @"[^ a - zA - Z0 - 9]"))
                //    {
                //        objWorkFlow.SupplierName = supplier[0];
                //        objWorkFlow.SupplierCode = supplier[1];
                //    }
                //    else
                //    {
                //        objWorkFlow.SupplierName = txtSupplier.Text;
                //       objWorkFlow.SupplierCode = "";
                //    }
                //}
                //else
                //{
                //    objWorkFlow.SupplierName = txtSupplier.Text;
                //    objWorkFlow.SupplierCode = "";
                //} 
                if (ddlSupplier.SelectedValue != "Select")
                {
                    objWorkFlow.SupplierCode = ddlSupplier.SelectedValue;
                }
                else
                {
                    objWorkFlow.SupplierCode = "";
                }
                objWorkFlow.SupplierName = txtSupplier.Text;
                objWorkFlow.WFID = Convert.ToInt32(Request.QueryString["WFID"]);
                objWorkFlow.Parent_WFID= Convert.ToInt32(Request.QueryString["WFPID"]);
                objWorkFlow.WF_Status = Request.QueryString["Status"];
                objWorkFlow.UserId = Request.QueryString["u"];
                objWorkFlow.Supplier = txtSupplierEmail.Text.Trim();
                objWorkFlow.EmailSubject = txtEmailSub.Text.Trim();
                objWorkFlow.RandomNo = RandomNo != "" ? RandomNo : hdfRandomNo.Value;
                int res = objWorkFlow.UpdateEnquiryRaised();
                objWorkFlow.UpdateWF_StatusInBAANTable(); // Dump Preorder Data TO BAAN table change 25/08/2018 sagar 
                if (res > 0)
                {
                    string IndexValue = InsertPreHistory(Convert.ToInt32(Request.QueryString["WFID"]), Request.QueryString["Status"]);
                    // UploadAttachments(IndexValue);
                    if (Request.QueryString["Status"] == "Enquiry Raised")
                    {
                        MailTo = txtSupplierEmail.Text.Trim();
                        SendMail(MailTo, RandomNo, objWorkFlow.EmailSubject);

                        string url = "http://192.9.200.146/webtools2/CreateExternalUser.aspx?LoginID=" + RandomNo + "&Password=" + RandomNo + "&UserName=" + txtSupplier.Text.Trim() + "&EMailID=" + txtSupplierEmail.Text.Trim();
                        HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                        rq.Method = "GET";
                        rq.ContentType = "application/json";
                        WebResponse rs = rq.GetResponse();
                        System.IO.Stream st = rs.GetResponseStream();
                        System.IO.StreamReader sr = new System.IO.StreamReader(st);
                        String strResponse = sr.ReadToEnd();
                        sr.Close();
                    }
                    GetData();

                    //  Change - 26 June 2018 -- ERP Transaction Update for Control Tower
                    // Insert data into ttpisg229200
                    #region ERP Transaction Update for Control Tower

                    if (Request.QueryString["Status"] == "Enquiry Raised" || Request.QueryString["Status"] == "Technical offer Received")
                    {
                        objWorkFlow.TransactionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if (Request.QueryString["Status"] == "Technical offer Received")
                        {
                            objWorkFlow.BusinessObjectHandle = "CT_RFQOFFERECEIVED";
                        }
                        else
                        {
                            objWorkFlow.BusinessObjectHandle = "CT_RFQRAISED";
                        }
                        objWorkFlow.IndexValue = (Request.QueryString["WFID"]).ToString();
                        int SerialNoCount = objWorkFlow.GetSerialNumber();
                        SerialNoCount++;
                        objWorkFlow.SLNO_WFID = SerialNoCount;
                        string[] Project = txtProject.Text.Split('-');
                        objWorkFlow.Project = Project[0];
                        string[] Element = txtElement.Text.Split('-');
                        objWorkFlow.Element = Element[0];
                        objWorkFlow.UserId = Request.QueryString["u"];
                        int nRecordInserted = objWorkFlow.InsertPreOrderDatatoControlTower();
                        if (nRecordInserted > 0)
                        {
                            // for each PMDL doc insert a new record ( Sagar new change 11-July-2018)
                            // 2 ways- comma seperated substring or select PMDLDoc for given WFID from WF1_PreOrderPMDL table 
                            //and loop through each PMDL doc.
                            int DetailSerialNoCount = 0;
                            DataTable dtPMDL = objWorkFlow.GetMultiPMDL(hdfParentWFID.Value);
                            if (dtPMDL.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtPMDL.Rows)
                                {
                                    DetailSerialNoCount = objWorkFlow.GetDetailSerialNumber();
                                    DetailSerialNoCount++;
                                    objWorkFlow.DetailSerialCount = DetailSerialNoCount;

                                    objWorkFlow.PMDLdocDesc = dr["PMDLDocNo"].ToString();
                                    DataTable dt = objWorkFlow.GetPartItemCount_Weight();
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (dt.Rows[0]["PartItemCount"].ToString() != "")
                                        {
                                            objWorkFlow.PartItemCount = (int)dt.Rows[0]["PartItemCount"];
                                        }
                                        else { objWorkFlow.PartItemCount = 0; }
                                        if (dt.Rows[0]["PartItenWeight"].ToString() != "")
                                        {
                                            objWorkFlow.PartItemWeight = (double)dt.Rows[0]["PartItenWeight"];
                                        }
                                        else { objWorkFlow.PartItemWeight = 0; }
                                    }
                                    else
                                    {
                                        objWorkFlow.PartItemCount = 0;
                                        objWorkFlow.PartItemWeight = 0;
                                    }

                                    objWorkFlow.InsertPreOrderDatatoControlTowerChildTable();

                                    if (Request.QueryString["Status"] == "Enquiry Raised")
                                    {
                                        // update t_rfqd in tdmisg140200 table 
                                        //change - 21-08-2018
                                       
                                        DataTable dataTable = objWorkFlow.GetRaisedEnquiryDate();
                                        objWorkFlow.ItemReference = (string)dataTable.Rows[0]["t_iref"];
                                        string Itemref_Typ = objWorkFlow.GetItemRefType();

                                        if (dataTable.Rows[0]["RaisedDate"].ToString() == "01-01-1970" || dataTable.Rows[0]["RaisedDate"].ToString() == "01-01-1900")
                                        {
                                            string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                                            objWorkFlow.UpdateRaisedEnquiryDate(CurrentDate);
                                        }
                                       
                                            // update t_acsd in table ttpisg220200 with the Min of t_rfqd for the given item ref and t_bohd

                                            DateTime MinRFQdate = objWorkFlow.GetMinRFQDate();
                                            if (MinRFQdate != default(DateTime))
                                            {
                                                string MinRaisedEnquiryDate = MinRFQdate.ToString("yyyy-MM-dd hh:mm:ss");
                                               // objWorkFlow.UpdateActualStartDate(MinRaisedEnquiryDate);
                                            }
                                       

                                        // update % field t_cpgv of table ttpisg220200 with (Drawings for which 
                                        // raised Date is updated /No of Total drawings)* 100 
                                        //against a particular pair of busisness handle and Item reference

                                        //double RFQraisedDrawingCount = objWorkFlow.GeRFQraisedDrawingCount();
                                        //double TotalDrawingCount = objWorkFlow.GeTotalDrawingCount();
                                        //double EnquiryRaisedpercentage = objWorkFlow.GeRFQraisedDrawingpercentage();
                                        //double percentageRaisedDrawing = Math.Round((RFQraisedDrawingCount / TotalDrawingCount), 3) * 100;
                                        //objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                        double percentageRaisedDrawingbyWeight = 0;
                                        double percentageRaisedDrawing = 0;
                                        double RFQraisedDrawingCount = objWorkFlow.GeRFQraisedDrawingCount();
                                        double TotalDrawingCount = objWorkFlow.GeTotalDrawingCount();
                                        if (TotalDrawingCount != 0)
                                        {
                                             percentageRaisedDrawing = Math.Round((RFQraisedDrawingCount / TotalDrawingCount), 4) * 100;
                                        }
                                        
                                        double RFQRaisedDrawingWeight = objWorkFlow.GetRFQRaisedDrawingWeight();
                                        double TotalRaisedDrawingWeight = objWorkFlow.GetTotalDrawingWeight();
                                        if (TotalRaisedDrawingWeight != 0)
                                        {
                                             percentageRaisedDrawingbyWeight = Math.Round((RFQRaisedDrawingWeight / TotalRaisedDrawingWeight), 4) * 100;
                                        }
                                        

                                        if (percentageRaisedDrawing < 100.00 && percentageRaisedDrawingbyWeight < 100.00)
                                        {
                                            if ((percentageRaisedDrawing >= percentageRaisedDrawingbyWeight))
                                            {
                                               // objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                            }
                                            else
                                            {
                                                if (Itemref_Typ == "4")// when item reference typ=="Self Engineered"
                                                {
                                                  //  objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawingbyWeight);
                                                }
                                                else
                                                {
                                                   // objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                                }
                                            }
                                        }

                                        else
                                        {
                                            if (percentageRaisedDrawing >= 100)
                                            {
                                               // objWorkFlow.UpdateDrawingpercentage(100);
                                            }
                                            else
                                            {
                                               // objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                            }
                                        }

                                       // objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);-- changed this to above on 11-09-18 sagar
                                        // Update total number of count for which enquiry is sent to Vendor against each PMDL Doc
                                        //First search the total number of count against Parent WFID for which child IDs are in 
                                        //'Raised Enquiry' status then store this count  in t_numv of table tdmisg140200
                                        string parent_id = Request.QueryString["WFPID"];
                                        objWorkFlow.Parent_WFID = Convert.ToInt32(parent_id);
                                        //int nCount = objWorkFlow.GetEnquiryRaisedCount(parent_id);
                                        DataTable dtPMDLDoc = objWorkFlow.GetPMDLFromItemRef();
                                        int nCount = 0;
                                        string PMDL = "";
                                        if (dtPMDLDoc.Rows.Count > 0)
                                        {

                                            for (int i = 0; i < dtPMDLDoc.Rows.Count; i++)
                                            {

                                                if (i == 0)
                                                {
                                                    PMDL = "'" + dtPMDLDoc.Rows[0]["t_docn"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    PMDL += ",'" + dtPMDLDoc.Rows[i]["t_docn"].ToString() + "'";
                                                }

                                            }
                                            nCount= objWorkFlow.GetTotalChildRecordCount(PMDL);
                                            //foreach (DataRow drow in dtPMDLDoc.Rows)
                                            //{
                                            //    string sPMDLDoc = drow["t_docn"].ToString();
                                            //    nCount += objWorkFlow.GetEnquiryRaisedCount(sPMDLDoc);
                                            //    //nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                            //    //nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                            //}
                                        }
                                      //  objWorkFlow.UpdateRFQCount(nCount);
                                    }
                                   
                                    if (Request.QueryString["Status"] == "Technical offer Received")
                                    {
                                        double nTotalChildRecordCount = 0;
                                        double nTechofferChildRecordCount = 0;
                                        double nOfferRecevied = 0;
                                        double nTotalInquirySent = 0;
                                        double nTotalWeight = 0;
                                        double nTotalWeightForOfferReceieved = 0;
                                        string PMDLDocs = "";
                                        string PMDLDocuments = "";
                                        string PMDLDocNo = "";
                                        double percentageTechOfferReceivedDrawing_byDrawing=0;
                                        double percentageTechOfferReceivedDrawing_byWeight=0;
                                        DataTable dataTable1 = objWorkFlow.GetTechOfferReceiveDate();
                                        objWorkFlow.ItemReference = (string)dataTable1.Rows[0]["t_iref"];

                                        string Itemref_Typ = objWorkFlow.GetItemRefType();

                                        if (dataTable1.Rows[0]["OfferReceiveDate"].ToString() == "01-01-1970" || dataTable1.Rows[0]["OfferReceiveDate"].ToString() == "01-01-1900")
                                        {
                                            string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                                            objWorkFlow.UpdateOfferReceiveDate(CurrentDate);
                                        }
                                        DateTime MinOfferReceieveDate = objWorkFlow.GetMinOfferReceieveDate();
                                        if (MinOfferReceieveDate != default(DateTime))
                                        {
                                            string OfferReceieveDate = MinOfferReceieveDate.ToString("yyyy-MM-dd hh:mm:ss");
                                           // objWorkFlow.UpdateActualStartDate_tor(OfferReceieveDate);
                                        }

                                        string parent_id1 = Request.QueryString["WFPID"];
                                        string sStatus = objWorkFlow.GetParenIDStatus(parent_id1);

                                        if (sStatus == "All Offer Received")
                                        {
                                            percentageTechOfferReceivedDrawing_byDrawing = 100;
                                        }
                                        else
                                        {
                                            DataTable dtPMDLDoc = objWorkFlow.GetPMDLFromItemRef();
                                            //if(dtPMDLDoc.Rows.Count>0)
                                            //{

                                            //    foreach (DataRow drow in dtPMDLDoc.Rows)
                                            //    {
                                            //        string sPMDLDoc = drow["t_docn"].ToString();
                                            //        nTotalChildRecordCount+=objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                            //        nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                            //    }
                                            // }

                                            if (dtPMDLDoc.Rows.Count > 0)
                                            {

                                                for (int i = 0; i < dtPMDLDoc.Rows.Count; i++)
                                                {

                                                    if (i == 0)
                                                    {
                                                        PMDLDocs = "'" + dtPMDLDoc.Rows[0]["t_docn"].ToString() + "'";
                                                    }
                                                    else
                                                    {
                                                        PMDLDocs += ",'" + dtPMDLDoc.Rows[i]["t_docn"].ToString() + "'";
                                                    }

                                                }
                                            }
                                            DataTable dtPMDLDocForOfferReceieved = objWorkFlow.GetPMDLFromItemRefForOfferReceived();
                                            if (dtPMDLDocForOfferReceieved.Rows.Count > 0)
                                            {

                                                for (int i = 0; i < dtPMDLDocForOfferReceieved.Rows.Count; i++)
                                                {

                                                    if (i == 0)
                                                    {
                                                        PMDLDocuments = "'" + dtPMDLDocForOfferReceieved.Rows[0]["t_docn"].ToString() + "'";
                                                    }
                                                    else
                                                    {
                                                        PMDLDocuments += ",'" + dtPMDLDocForOfferReceieved.Rows[i]["t_docn"].ToString() + "'";
                                                    }

                                                }
                                            }
                                            int sWFID1 = objWorkFlow.WFID;
                                            objWorkFlow.WFID = objWorkFlow.Parent_WFID;
                                            DataTable dtPMDLbyWFID = objWorkFlow.GetPMDLbyWFID();
                                            if (dtPMDLbyWFID.Rows.Count > 0)
                                            {

                                                for (int i = 0; i < dtPMDLbyWFID.Rows.Count; i++)
                                                {

                                                    if (i == 0)
                                                    {
                                                        PMDLDocNo = "'" + dtPMDLbyWFID.Rows[0]["PMDLDocNo"].ToString() + "'";
                                                    }
                                                    else
                                                    {
                                                        PMDLDocNo += ",'" + dtPMDLbyWFID.Rows[i]["PMDLDocNo"].ToString() + "'";
                                                    }

                                                }
                                            }

                                            objWorkFlow.WFID = sWFID1;
                                            nTotalChildRecordCount = objWorkFlow.GetTotalChildRecordCount(PMDLDocs);
                                            nTechofferChildRecordCount = objWorkFlow.GetTechofferChildRecordCount(PMDLDocs);
                                           // nTotalWeight += objWorkFlow.GetTotalWeight(PMDLDocs);
                                            nTotalWeight = objWorkFlow.GetTotalWeight();
                                            nTotalWeightForOfferReceieved = objWorkFlow.GetTotalWeight(PMDLDocNo);
                                            //  double EnquiryRaisedpercentage = objWorkFlow.GeRFQraisedDrawingpercentage();
                                            nTotalInquirySent = objWorkFlow.GetTotalInquirySentCount();
                                            nOfferRecevied = objWorkFlow.GeTechOfferReceievedDrawingCount(PMDLDocNo);

                                            if (nTotalChildRecordCount != 0 && nTotalInquirySent!=0)
                                            {
                                                percentageTechOfferReceivedDrawing_byDrawing = Math.Round((nTechofferChildRecordCount / nTotalChildRecordCount) * (nOfferRecevied/ nTotalInquirySent)* 100, 4);
                                            }
                                            else
                                            {
                                                percentageTechOfferReceivedDrawing_byDrawing = 0;
                                            }

                                            if (nTotalChildRecordCount != 0 && nTotalWeight != 0)
                                            {
                                                percentageTechOfferReceivedDrawing_byWeight = Math.Round((nTechofferChildRecordCount / nTotalChildRecordCount) * (nTotalWeightForOfferReceieved / nTotalWeight) * 100, 4);
                                            }
                                            else
                                            {
                                                percentageTechOfferReceivedDrawing_byWeight = 0;
                                            }
                                        }
                                        string parent_id2 = Request.QueryString["WFPID"];
                                        objWorkFlow.Parent_WFID = Convert.ToInt32(parent_id2);

                                        objWorkFlow.InsertIntoItemReferencewiseProgressTable(percentageTechOfferReceivedDrawing_byWeight, percentageTechOfferReceivedDrawing_byDrawing);
                                        DataTable dtPercentage1 = objWorkFlow.GetPercentagebyCount_Weight();
                                        double percentage_byCount = 0;
                                        double percentage_byWeight = 0;
                                        if (dtPercentage1.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr2 in dtPercentage1.Rows)
                                            {
                                                objWorkFlow.Project = dr2["Project"].ToString();
                                                objWorkFlow.ItemReference = dr2["ItemReference"].ToString();
                                                percentage_byCount += Convert.ToDouble(dr2["CountPercentage"]);
                                                percentage_byWeight += Convert.ToDouble(dr2["WeightPercentage"]);
                                            }
                                        }
                                                if (percentage_byCount < 100.00 && percentage_byWeight < 100.00)
                                                {
                                                    if ((percentage_byCount >= percentage_byWeight))
                                                    {
                                                       // objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byCount);
                                                    }
                                                    else
                                                    {
                                                        if (Itemref_Typ == "4")// when item reference typ=="Self Engineered" then only update with weight %
                                                        {
                                                          //  objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byWeight);
                                                        }
                                                        else
                                                        {
                                                            //objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byCount);
                                                        }
                                                    }
                                                }

                                                else
                                                {
                                                    if (percentage_byCount >= 100)
                                                    {
                                                       // objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(100);
                                                    }
                                                    else
                                                    {
                                                      //  objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byCount);
                                                    }
                                                }
                                        //    }
                                        //}

                                        //if (percentageTechOfferReceivedDrawing_byDrawing >= 100.00 || percentageTechOfferReceivedDrawing_byWeight >= 100.00)
                                        //{
                                        //    objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(100);
                                        //}
                                        //else if  (percentageTechOfferReceivedDrawing_byDrawing >= percentageTechOfferReceivedDrawing_byWeight)
                                        //{
                                        //    objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentageTechOfferReceivedDrawing_byDrawing);
                                        //}
                                        //else
                                        //{
                                        //    objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentageTechOfferReceivedDrawing_byWeight);
                                        //}
                                        //double RFQraisedDrawingCount = objWorkFlow.GeRFQraisedDrawingCount();
                                        //double TotalDrawingCount = objWorkFlow.GeTotalDrawingCount();
                                        //double EnquiryRaisedpercentage = objWorkFlow.GeRFQraisedDrawingpercentage();
                                        //double percentageTechOfferReceivedDrawing = Math.Round((RFQraisedDrawingCount / TotalDrawingCount) * EnquiryRaisedpercentage, 3);
                                        //objWorkFlow.UpdateDrawingpercentage(percentageTechOfferReceivedDrawing);
                                        //objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentageTechOfferReceivedDrawing);


                                        //string parent_id = Request.QueryString["WFPID"];
                                        //// int nCount = objWorkFlow.GetTechOfferReceiveDateCount(parent_id);
                                        //int nCount = 0;
                                        //DataTable dtPMDLDoc1 = objWorkFlow.GetPMDLFromItemRef();
                                        //if (dtPMDLDoc1.Rows.Count > 0)
                                        //{

                                        //    foreach (DataRow drow in dtPMDLDoc1.Rows)
                                        //    {
                                        //        string sPMDLDoc = drow["t_docn"].ToString();
                                        //        nCount += objWorkFlow.GetTechOfferReceiveDateCount(sPMDLDoc);
                                        //        //nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                        //        //nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                        //    }
                                        //}

                                        ////objWorkFlow.UpdateRFQCount(nCount);
                                        //objWorkFlow.UpdateTechOfferReceiveCount(nCount);

                                        DataTable dtPMDLDoc1 = objWorkFlow.GetPMDLFromItemRef();
                                        int nCount = 0;
                                        string PMDL = "";
                                        if (dtPMDLDoc1.Rows.Count > 0)
                                        {

                                            for (int i = 0; i < dtPMDLDoc1.Rows.Count; i++)
                                            {

                                                if (i == 0)
                                                {
                                                    PMDL = "'" + dtPMDLDoc1.Rows[0]["t_docn"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    PMDL += ",'" + dtPMDLDoc1.Rows[i]["t_docn"].ToString() + "'";
                                                }

                                            }
                                            nCount = objWorkFlow.GetTechofferChildRecordCount(PMDL);
                                            //foreach (DataRow drow in dtPMDLDoc.Rows)
                                            //{
                                            //    string sPMDLDoc = drow["t_docn"].ToString();
                                            //    nCount += objWorkFlow.GetEnquiryRaisedCount(sPMDLDoc);
                                            //    //nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                            //    //nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                            //}
                                        }
                                       // objWorkFlow.UpdateTechOfferReceiveCount(nCount);
                                    }


                                }// change 14-09 foreach

                            }
                            else
                            {
                                DetailSerialNoCount = objWorkFlow.GetDetailSerialNumber();
                                DetailSerialNoCount++;
                                objWorkFlow.DetailSerialCount = DetailSerialNoCount;
                                // earlier all PMDL were saved in one colum comma seperated
                                objWorkFlow.PMDLdocDesc = txtPMDLDoc.Text;
                                DataTable dt = objWorkFlow.GetPartItemCount_Weight();
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["PartItemCount"].ToString() != "")
                                    {
                                        objWorkFlow.PartItemCount = (int)dt.Rows[0]["PartItemCount"];
                                    }
                                    else { objWorkFlow.PartItemCount = 0; }
                                    if (dt.Rows[0]["PartItenWeight"].ToString() != "")
                                    {
                                        objWorkFlow.PartItemWeight = (double)dt.Rows[0]["PartItenWeight"];
                                    }
                                    else { objWorkFlow.PartItemWeight = 0; }
                                }
                                else
                                {
                                    objWorkFlow.PartItemCount = 0;
                                    objWorkFlow.PartItemWeight = 0;
                                }

                                objWorkFlow.InsertPreOrderDatatoControlTowerChildTable();
                                if (Request.QueryString["Status"] == "Enquiry Raised")
                                {
                                    // update t_rfqd in tdmisg140200 table
                                    //change change -3,- 21-08-2018 
                                    DataTable dataTable = objWorkFlow.GetRaisedEnquiryDate();
                                    objWorkFlow.ItemReference = (string)dataTable.Rows[0]["t_iref"];
                                    string Itemref_Typ = objWorkFlow.GetItemRefType();

                                    if (dataTable.Rows[0]["RaisedDate"].ToString() == "01-01-1970" || dataTable.Rows[0]["RaisedDate"].ToString() == "01-01-1900")
                                    {
                                        string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                                        objWorkFlow.UpdateRaisedEnquiryDate(CurrentDate);
                                    }
                                    DateTime MinRFQdate = objWorkFlow.GetMinRFQDate();
                                    if (MinRFQdate != default(DateTime))
                                    {
                                        string MinRaisedEnquiryDate = MinRFQdate.ToString("yyyy-MM-dd hh:mm:ss");
                                       // objWorkFlow.UpdateActualStartDate(MinRaisedEnquiryDate);
                                    }
                                    // update % field t_cpgv of table ttpisg220200 with (Drawings for which 
                                    // raised Date is updated /No of Total drawings)* 100 
                                    //against a particular pair of busisness handle and Item reference
                                    // change -4

                                    double percentageRaisedDrawingbyWeight = 0;
                                    double percentageRaisedDrawing = 0;
                                    double RFQraisedDrawingCount = objWorkFlow.GeRFQraisedDrawingCount();
                                    double TotalDrawingCount = objWorkFlow.GeTotalDrawingCount();
                                    if (TotalDrawingCount != 0)
                                    {
                                        percentageRaisedDrawing = Math.Round((RFQraisedDrawingCount / TotalDrawingCount), 4) * 100;
                                    }

                                    double RFQRaisedDrawingWeight = objWorkFlow.GetRFQRaisedDrawingWeight();
                                    double TotalRaisedDrawingWeight = objWorkFlow.GetTotalDrawingWeight();
                                    if (TotalRaisedDrawingWeight != 0)
                                    {
                                        percentageRaisedDrawingbyWeight = Math.Round((RFQRaisedDrawingWeight / TotalRaisedDrawingWeight), 4) * 100;
                                    }


                                    if (percentageRaisedDrawing < 100.00 && percentageRaisedDrawingbyWeight < 100.00)
                                    {
                                        if ((percentageRaisedDrawing >= percentageRaisedDrawingbyWeight))
                                        {
                                          //  objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                        }
                                        else
                                        {
                                            if (Itemref_Typ == "4")// when item reference typ=="Self Engineered"
                                            {
                                                //objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawingbyWeight);
                                            }
                                            else
                                            {
                                               // objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                            }
                                        }
                                    }

                                    else
                                    {
                                        if (percentageRaisedDrawing >= 100)
                                        {
                                          //  objWorkFlow.UpdateDrawingpercentage(100);
                                        }
                                        else
                                        {
                                           // objWorkFlow.UpdateDrawingpercentage(percentageRaisedDrawing);
                                        }
                                    }
                                    // Update total number of count for which enquiry is sent to Vendor against each PMDL Doc
                                    //First search the total number of count against Parent WFID for which child IDs are in 
                                    //'Raised Enquiry' status then store this count  in t_numv of table tdmisg140200
                                    // Change 2- 22/08/2018
                                    //string parent_id = Request.QueryString["WFPID"];
                                    //int nCount = objWorkFlow.GetEnquiryRaisedCount(parent_id);
                                    //objWorkFlow.UpdateRFQCount(nCount);

                                    DataTable dtPMDLDoc = objWorkFlow.GetPMDLFromItemRef();
                                    int nCount = 0;
                                    string PMDL = "";
                                    if (dtPMDLDoc.Rows.Count > 0)
                                    {

                                        for (int i = 0; i < dtPMDLDoc.Rows.Count; i++)
                                        {

                                            if (i == 0)
                                            {
                                                PMDL = "'" + dtPMDLDoc.Rows[0]["t_docn"].ToString() + "'";
                                            }
                                            else
                                            {
                                                PMDL += ",'" + dtPMDLDoc.Rows[i]["t_docn"].ToString() + "'";
                                            }

                                        }
                                        nCount = objWorkFlow.GetTotalChildRecordCount(PMDL);
                                        //foreach (DataRow drow in dtPMDLDoc.Rows)
                                        //{
                                        //    string sPMDLDoc = drow["t_docn"].ToString();
                                        //    nCount += objWorkFlow.GetEnquiryRaisedCount(sPMDLDoc);
                                        //    //nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                        //    //nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                        //}
                                    }
                                   // objWorkFlow.UpdateRFQCount(nCount);
                                }
                                if (Request.QueryString["Status"] == "Technical offer Received")
                                {
                                    double nTotalChildRecordCount = 0;
                                    double nTechofferChildRecordCount = 0;
                                    double nOfferRecevied = 0;
                                    double nTotalInquirySent = 0;
                                    double nTotalWeight = 0;
                                    double nTotalWeightForOfferReceieved = 0;
                                    string PMDLDocs = "";
                                    string PMDLDocuments = "";
                                    string PMDLDocNo = "";
                                    double percentageTechOfferReceivedDrawing_byDrawing = 0;
                                    double percentageTechOfferReceivedDrawing_byWeight = 0;
                                    DataTable dataTable1 = objWorkFlow.GetTechOfferReceiveDate();
                                    objWorkFlow.ItemReference = (string)dataTable1.Rows[0]["t_iref"];
                                    string Itemref_Typ = objWorkFlow.GetItemRefType();

                                    if (dataTable1.Rows[0]["OfferReceiveDate"].ToString() == "01-01-1970" || dataTable1.Rows[0]["OfferReceiveDate"].ToString() == "01-01-1900")
                                    {
                                        string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                                        objWorkFlow.UpdateOfferReceiveDate(CurrentDate);
                                    }
                                    DateTime MinOfferReceieveDate = objWorkFlow.GetMinOfferReceieveDate();
                                    if (MinOfferReceieveDate != default(DateTime))
                                    {
                                        string OfferReceieveDate = MinOfferReceieveDate.ToString("yyyy-MM-dd hh:mm:ss");
                                       // objWorkFlow.UpdateActualStartDate_tor(OfferReceieveDate);
                                    }
                                    string parent_id1 = Request.QueryString["WFPID"];
                                    string sStatus=objWorkFlow.GetParenIDStatus(parent_id1);

                                    if (sStatus== "All Offer Received")
                                    {
                                        percentageTechOfferReceivedDrawing_byDrawing = 100;
                                    }
                                    else
                                    {
                                        DataTable dtPMDLDoc = objWorkFlow.GetPMDLFromItemRef();
                                        //if (dtPMDLDoc.Rows.Count > 0)
                                        //{

                                        //    foreach (DataRow drow in dtPMDL.Rows)
                                        //    {
                                        //        string sPMDLDoc = drow["t_docn"].ToString();
                                        //        nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                        //        nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                        //    }
                                        //}
                                        if (dtPMDLDoc.Rows.Count > 0)
                                        {

                                            for (int i = 0; i < dtPMDLDoc.Rows.Count; i++)
                                            {

                                                if (i == 0)
                                                {
                                                    PMDLDocs = "'" + dtPMDLDoc.Rows[0]["t_docn"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    PMDLDocs += ",'" + dtPMDLDoc.Rows[i]["t_docn"].ToString() + "'";
                                                }

                                            }
                                        }
                                        DataTable dtPMDLDocForOfferReceieved = objWorkFlow.GetPMDLFromItemRefForOfferReceived();
                                        if (dtPMDLDocForOfferReceieved.Rows.Count > 0)
                                        {

                                            for (int i = 0; i < dtPMDLDocForOfferReceieved.Rows.Count; i++)
                                            {

                                                if (i == 0)
                                                {
                                                    PMDLDocuments = "'" + dtPMDLDocForOfferReceieved.Rows[0]["t_docn"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    PMDLDocuments += ",'" + dtPMDLDocForOfferReceieved.Rows[i]["t_docn"].ToString() + "'";
                                                }

                                            }
                                        }
                                        DataTable dtPMDLbyWFID = objWorkFlow.GetPMDLbyWFID();
                                        if (dtPMDLbyWFID.Rows.Count > 0)
                                        {

                                            for (int i = 0; i < dtPMDLbyWFID.Rows.Count; i++)
                                            {

                                                if (i == 0)
                                                {
                                                    PMDLDocNo = "'" + dtPMDLbyWFID.Rows[0]["PMDLDocNo"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    PMDLDocNo += ",'" + dtPMDLbyWFID.Rows[i]["PMDLDocNo"].ToString() + "'";
                                                }

                                            }
                                        }

                                        nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(PMDLDocs);
                                        nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(PMDLDocs);
                                        // nTotalWeight += objWorkFlow.GetTotalWeight(PMDLDocs);
                                        nTotalWeight += objWorkFlow.GetTotalWeight();
                                        nTotalWeightForOfferReceieved += objWorkFlow.GetTotalWeight(PMDLDocuments);
                                        //  double EnquiryRaisedpercentage = objWorkFlow.GeRFQraisedDrawingpercentage();
                                        nTotalInquirySent = objWorkFlow.GetTotalInquirySentCount();
                                        nOfferRecevied = objWorkFlow.GeTechOfferReceievedDrawingCount(PMDLDocNo);

                                        if (nTotalChildRecordCount != 0 && nTotalInquirySent != 0)
                                        {
                                            percentageTechOfferReceivedDrawing_byDrawing = Math.Round((nTechofferChildRecordCount / nTotalChildRecordCount) * (nOfferRecevied / nTotalInquirySent) * 100, 2);
                                        }
                                        else
                                        {
                                            percentageTechOfferReceivedDrawing_byDrawing = 0;
                                        }

                                        if (nTotalChildRecordCount != 0 && nTotalWeight != 0)
                                        {
                                            percentageTechOfferReceivedDrawing_byWeight = Math.Round((nTechofferChildRecordCount / nTotalChildRecordCount) * (nTotalWeightForOfferReceieved / nTotalWeight) * 100, 2);
                                        }
                                        else
                                        {
                                            percentageTechOfferReceivedDrawing_byWeight = 0;
                                        }
                                    }
                                    string parent_id2 = Request.QueryString["WFPID"];
                                    objWorkFlow.Parent_WFID = Convert.ToInt32(parent_id2);
                                    objWorkFlow.InsertIntoItemReferencewiseProgressTable(percentageTechOfferReceivedDrawing_byWeight, percentageTechOfferReceivedDrawing_byDrawing);
                                    //DataTable dtPercentage = objWorkFlow.GetTechnoComNegotiationPercentagebyCount_Weight();
                                    DataTable dtPercentage = objWorkFlow.GetPercentagebyCount_Weight();
                                    double percentage_byCount = 0;
                                    double percentage_byWeight = 0;

                                    if (dtPercentage.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dtPercentage.Rows)
                                        {
                                            objWorkFlow.Project = dr["Project"].ToString();
                                            objWorkFlow.ItemReference = dr["ItemReference"].ToString();
                                            //double percentage_byCount = Convert.ToDouble(dr["CountPercentage"]);
                                            //double percentage_byWeight = Convert.ToDouble(dr["WeightPercentage"]);
                                            percentage_byCount += Convert.ToDouble(dr["CountPercentage"]);
                                            percentage_byWeight += Convert.ToDouble(dr["WeightPercentage"]);
                                        }
                                    }



                                            if (percentage_byCount < 100.00 && percentage_byWeight < 100.00)
                                            {
                                                if ((percentage_byCount >= percentage_byWeight))
                                                {
                                                  //  objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byCount);
                                                }
                                                else
                                                {
                                                    if (Itemref_Typ == "4")// when item reference typ=="Self Engineered" then only update with weight %
                                                    {
                                                       // objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byWeight);
                                                    }
                                                    else
                                                    {
                                                      //  objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byCount);
                                                    }
                                                }
                                            }

                                            else
                                            {
                                                if (percentage_byCount >= 100)
                                                {
                                                  //  objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(100);
                                                }
                                                else
                                                {
                                                 //   objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentage_byCount);
                                                }
                                            }
                                    //    }
                                    //}
                                    //double RFQraisedDrawingCount = objWorkFlow.GeRFQraisedDrawingCount();
                                    //double TotalDrawingCount = objWorkFlow.GeTotalDrawingCount();
                                    //double EnquiryRaisedpercentage = objWorkFlow.GeRFQraisedDrawingpercentage();
                                    //double percentageTechOfferReceivedDrawing = Math.Round((RFQraisedDrawingCount / TotalDrawingCount) * EnquiryRaisedpercentage, 3);
                                    //objWorkFlow.UpdateDrawingpercentage(percentageTechOfferReceivedDrawing);
                                    //objWorkFlow.UpdateTechOfferReceivedDrawingpercentage(percentageTechOfferReceivedDrawing);


                                    //string parent_id = Request.QueryString["WFPID"];
                                    //int nCount = objWorkFlow.GetTechOfferReceiveDateCount(parent_id);
                                    //objWorkFlow.UpdateTechOfferReceiveCount(nCount);
                                    DataTable dtPMDLDoc1 = objWorkFlow.GetPMDLFromItemRef();
                                    int nCount = 0;
                                    string PMDL = "";
                                    if (dtPMDLDoc1.Rows.Count > 0)
                                    {

                                        for (int i = 0; i < dtPMDLDoc1.Rows.Count; i++)
                                        {

                                            if (i == 0)
                                            {
                                                PMDL = "'"+dtPMDLDoc1.Rows[0]["t_docn"].ToString()+"'";
                                            }
                                            else
                                            {
                                                PMDL += ",'" + dtPMDLDoc1.Rows[i]["t_docn"].ToString() + "'";
                                            }

                                        }
                                        nCount = objWorkFlow.GetTechofferChildRecordCount(PMDL);
                                        //foreach (DataRow drow in dtPMDLDoc.Rows)
                                        //{
                                        //    string sPMDLDoc = drow["t_docn"].ToString();
                                        //    nCount += objWorkFlow.GetEnquiryRaisedCount(sPMDLDoc);
                                        //    //nTotalChildRecordCount += objWorkFlow.GetTotalChildRecordCount(sPMDLDoc);
                                        //    //nTechofferChildRecordCount += objWorkFlow.GetTechofferChildRecordCount(sPMDLDoc);
                                        //}
                                    }
                                    //objWorkFlow.UpdateTechOfferReceiveCount(nCount);
                                }

                                }
                        }
                    }
                    else
                    {
                    }
                    //if (Request.QueryString["Status"] == "Enquiry Raised")
                    //{

                    //    // update t_acsd in table ttpisg220200 with the Min of t_rfqd for the given item ref and t_bohd

                    //    DateTime MinRFQdate = objWorkFlow.GetMinRFQDate();
                    //    if (MinRFQdate != default(DateTime))
                    //    {
                    //        string MinRaisedEnquiryDate = MinRFQdate.ToString("yyyy-MM-dd hh:mm:ss");
                    //        objWorkFlow.UpdateActualStartDate(MinRaisedEnquiryDate);
                    //    }
                    //}
                    //if (Request.QueryString["Status"] == "Technical offer Received")
                    //{

                    //    // update t_acsd in table ttpisg220200 with the Min of t_rfqd for the given item ref and t_bohd

                    //    DateTime MinOfferReceieveDate = objWorkFlow.GetMinOfferReceieveDate();
                    //    if (MinOfferReceieveDate != default(DateTime))
                    //    {
                    //        string OfferReceieveDate = MinOfferReceieveDate.ToString("yyyy-MM-dd hh:mm:ss");
                    //        objWorkFlow.UpdateActualStartDate_tor(OfferReceieveDate);
                    //    }
                    //}
                    #endregion




                    if (Request.QueryString["Status"] == "Enquiry Raised")
                    {
                        Response.Redirect("EnquiryInProcess.aspx?u=" + Request.QueryString["u"]);
                    }
                    // old one 
                    if (Request.QueryString["Status"] == "Technical offer Received" || Request.QueryString["Status"] == "Commercial offer Received" || Request.QueryString["Status"] == "Enquiry For Techno Commercial Negotiation Completed")
                    {
                        Response.Redirect("RaisedEnquiry.aspx?u=" + Request.QueryString["u"] + "&WFPID=" + Request.QueryString["WFPID"]);
                    }
                    //if (Request.QueryString["Status"] == "Technical offer Received")
                    //{
                    //    Response.Redirect("TechnoCommercialNegotiaition.aspx?u=" + Request.QueryString["u"] + "&WFID=" + Request.QueryString["WFID"]);
                    //}
                    //if (Request.QueryString["Status"] == "Commercial offer Received")
                    //{
                    //    Response.Redirect("RaisedEnquiry.aspx?u=" + Request.QueryString["u"] + "&WFPID=" + Request.QueryString["WFPID"]);
                    //}

                    if (Request.QueryString["Status"] == "Isgec Comment Submitted")
                    {
                        Response.Redirect("ReleaseComments.aspx?u=" + Request.QueryString["u"]);
                    }
                    // ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + Request.QueryString["Status"] + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Fill All information');", true);
            }

            //
            //send mail
        }
        private string InsertPreHistory(int Id, string status)
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Id;
            DataTable dt = objWorkFlow.GetWFById();
            objWorkFlow.Parent_WFID = Convert.ToInt32(dt.Rows[0]["Parent_WFID"]);
            objWorkFlow.Project = dt.Rows[0]["Project"].ToString();
            Session["ProjCode"] = objWorkFlow.Project.Substring(0, 6);
            objWorkFlow.Element = dt.Rows[0]["Element"].ToString();
            objWorkFlow.SpecificationNo = dt.Rows[0]["SpecificationNo"].ToString();
            objWorkFlow.Buyer = dt.Rows[0]["Buyer"].ToString();
            objWorkFlow.UserId = Request.QueryString["u"];
            objWorkFlow.PMDLdocDesc= dt.Rows[0]["PMDLDocNo"].ToString();
            objWorkFlow.WF_Status = status;
            objWorkFlow.Supplier = dt.Rows[0]["Supplier"].ToString();
            objWorkFlow.SupplierName = dt.Rows[0]["SupplierName"].ToString();
            objWorkFlow.Manager= dt.Rows[0]["Manager"].ToString();
            objWorkFlow.Notes = txtNotes.Text;
            DataTable dtWFHID = objWorkFlow.InserPreOrderHistory();
            objWorkFlow.InserPreOrderHistoryToBAAN(); // Dump Preorder Data TO BAAN table change 25/08/2018 sagar
            string IndexValue = dtWFHID.Rows[0][0].ToString() + "_" + dtWFHID.Rows[0][1].ToString();
            return IndexValue;
        }

        #region Attachment
        //protected void UploadAttachments(string IndexValue)
        //{
        //    // if (Request.QueryString["AthHandle"] != null)
        //    //  {
        //    objWorkFlow = new WorkFlow();
        //    // objWorkFlow.IndexValue = Request.QueryString["Index"];
        //    objWorkFlow.AttachmentHandle = "J_PREORDER_WORKFLOW";
        //    DataTable dt = objWorkFlow.GetPath();
        //    if (dt.Rows.Count > 0)
        //    {
        //        //string ServerPath = "\\\\" + dt.Rows[0]["ServerName"].ToString() + "\\" + dt.Rows[0]["Path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//
        //        string ServerPath = "D:\\" + dt.Rows[0]["t_path"].ToString() + "\\";  //dt.Rows[0]["Path"].ToString() + "\\";//      // Server.MapPath("~/Files/");//

        //        if (fileAttachment.HasFile)
        //        {
        //            int filecount = 0;
        //            filecount = fileAttachment.PostedFiles.Count();
        //            if (filecount > 0)
        //            {
        //                foreach (HttpPostedFile PostedFile in fileAttachment.PostedFiles)
        //                {
        //                    string fileName = Path.GetFileNameWithoutExtension(PostedFile.FileName);
        //                    string fileExtension = Path.GetExtension(PostedFile.FileName);
        //                    try
        //                    {
        //                        objWorkFlow = new WorkFlow();
        //                        objWorkFlow.AttachmentHandle = "J_PREORDER_WORKFLOW";
        //                        objWorkFlow.IndexValue = IndexValue;
        //                        objWorkFlow.PurposeCode = "PreOrderWorkFlow";// Request.QueryString["PurposeCode"];
        //                        objWorkFlow.AttachedBy = Request.QueryString["u"];
        //                        objWorkFlow.FileName = fileName + fileExtension;
        //                        objWorkFlow.LibraryCode = dt.Rows[0]["LibCode"].ToString();
        //                        // DataTable dtFile = objWorkFlow.GetFileName();
        //                        //  if (dtFile.Rows.Count == 0)
        //                        //  {
        //                        DataTable dtDocID = objWorkFlow.InsertAttachmentdata();
        //                        if (dtDocID.Rows[0][0].ToString() != "0")
        //                        {
        //                            try
        //                            {
        //                                fileAttachment.SaveAs(ServerPath + dtDocID.Rows[0][0]);
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                            }
        //                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Uploaded');", true);
        //                        }
        //                        else
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Attachment Handle does not exist');", true);
        //                        }
        //                        //  }
        //                        //  else
        //                        //  {
        //                        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('This file name already exist please change your file name');", true);
        //                        // }
        //                    }
        //                    catch (System.Exception ex)
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                    }
        //                }
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Path does not exist');", true);
        //    }
        //    // }
        //    // else
        //    // {
        //    //     ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found Properly');", true);
        //    // }
        //}
        #endregion
        public void SendMail(string MailTo, string UserRandomNo, string EmailSub)
        {
            try
            {
                //  if (fileAttachment.HasFile)
                //  {
                objWorkFlow = new WorkFlow();
                DataTable dtMailTo = objWorkFlow.GetMAilID(Request.QueryString["u"]);
                string UserMailId = dtMailTo.Rows[0]["EMailid"].ToString();
                MailMessage mM = new MailMessage();
                mM.From = new MailAddress(UserMailId);
                // mM.To.Add(txtTo.Text.Trim());
                // string[] MailTo = txtTo.Text.Split(';');
                // foreach (string Mailid in MailTo)
                // {
                //     mM.To.Add(new MailAddress(Mailid));
                //  }
                //mM.To.Add(MailTo); //MailTo
                //mM.To.Add(UserMailId);
                // Change - 18-11-2018 sagar - To add email in To list from table tdisg231200 based on Project
                string eMailTo = "";
                string sExtraEmail = "";

                if (Session["ProjCode"].ToString() != "")
                {
                    objWorkFlow.Project = Session["ProjCode"].ToString();
                    DataTable dtExtraEmail = objWorkFlow.GetExtraMAilID();
                    if (dtExtraEmail.Rows.Count > 0)
                    {
                        foreach (DataRow datarow1 in dtExtraEmail.Rows)
                        {
                            sExtraEmail += datarow1["t_mail"] + ",";
                        }
                    }
                    MailTo = sExtraEmail + MailTo + "," + UserMailId;
                }
                else
                {
                    eMailTo = MailTo + "," + UserMailId;
                }
                foreach (var address in MailTo.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mM.To.Add(address);
                }
                mM.Subject = EmailSub;
                    //Request.QueryString["Status"] + "-" + txtSpecification.Text;
                //foreach (HttpPostedFile PostedFile in fileAttachment.PostedFiles)
                //{
                //    string fileName = Path.GetFileName(PostedFile.FileName);
                //    Attachment myAttachment = new Attachment(fileAttachment.FileContent, fileName);
                //    mM.Attachments.Add(myAttachment);
                //}
                mM.Body = txtNotes.Text;
                mM.IsBodyHtml = true;
                mM.Body += "<br /><br /><br /><a href='http://cloud.isgec.co.in/SupWF/SupEnquiryResponseForm.aspx?user=" + UserRandomNo + "'>Vendor Portal for Response Submission </a>";
                mM.Body += "<br /> <br /> You are requested to access the Vendor Portal for all communication and exchange of documents.";
                mM.Body += "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />This mail has been triggered to draw your attention on the respective ERP/Joomla module. Please login to respective module to see further details and file attachments";
                mM.Body = mM.Body.Replace("\n", "<br />");
                SmtpClient sC = new SmtpClient("192.9.200.214", 25);
                //   sC.Host = "192.9.200.214"; //"smtp-mail.outlook.com"// smtp.gmail.com
                //   sC.Port = 25; //587
                sC.DeliveryMethod = SmtpDeliveryMethod.Network;
                sC.UseDefaultCredentials = false;
                sC.Credentials = new NetworkCredential("baansupport@isgec.co.in", "isgec");
                //sC.Credentials = new NetworkCredential("adskvaultadmin", "isgec@123");
                sC.EnableSsl = false;  // true
                sC.Timeout = 10000000;
                sC.Send(mM);
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Some technical issue Mail not sent');", true);
            }
            //}
        }

        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            //// Changes needed to verify Sagar
            //WorkFlow objectWorkFlow = new WorkFlow();
            // string[] Project = txtProject.Text.Split('-');
            // objectWorkFlow.Project = Project[0];
            // objectWorkFlow.PMDLdocDesc = txtPMDLDoc.Text;
            // string sRefIndex = objectWorkFlow.GetIndex();
            // string sRefHandle = "TRANSMITTALLINES_200";

            // if (Request.QueryString["WFPID"] != null)
            // {
            //     if (sRefIndex != null && sRefIndex != "")
            //     {
            //         if (Request.QueryString["WFPID"] != null)
            //         {
            //              string url = "http://localhost/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFPID"] + "&AttachedBy=" + Request.QueryString["u"] + "&RefHandle=" + sRefHandle + "&RefIndex=" + sRefIndex + "&ed=a";
            //            // string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFPID"] + "&AttachedBy=" + Request.QueryString["u"] + "&RefHandle=" + sRefHandle + "&RefIndex=" + sRefIndex + "&ed=a";
            //             string s = "window.open('" + url + "','abc','width=1300,height=700,left=100,top=100,resizable=yes,scrollbars=yes');"; //," + hdfWFID.Value + " 'width=300,height=100,left=100,top=100,resizable=yes'
            //             ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //         }

            //     }
            //     else
            //     {
            //         string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFPID"] + "&AttachedBy=" + Request.QueryString["u"] + "&ed=a";
            //         string s = "window.open('" + url + "','abc','width=800,height=600,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
            //         ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //     }
            // }
            if (Request.QueryString["WFPID"] != null)
            {
                string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFPID"] + "&AttachedBy=" + Request.QueryString["u"] + "&ed=a";
                //string url = "http://192.9.200.146/Attachment/Attachment.aspx?AthHandle=J_PREORDER_WORKFLOW" + "&Index=" + Request.QueryString["WFPID"] + "&AttachedBy=" + Request.QueryString["u"] + "&ed=a";
                string s = "window.open('" + url + "','abc','width=800,height=600,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        protected void btnNotes_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["WFID"] != null)
            {
                string MailTo = string.Empty;
                WorkFlow objectWorkFlow = new WorkFlow();
                string[] Project = txtProject.Text.Split('-');
                objectWorkFlow.Project = Project[0];
                string WFID = Request.QueryString["WFID"];
                // objectWorkFlow.Project = txtProject.Text;
                objectWorkFlow.PMDLdocDesc = txtPMDLDoc.Text;
                string sRefIndex = objectWorkFlow.NoteRefIndex(WFID);
                string sRefHandle = "IDMSEVALUATOR_200";

                MailTo = txtSupplierEmail.Text;

                string Header = string.Empty;
                if (txtSpecification.Enabled)
                {
                    if (txtSpecification.Text.Contains("("))
                    {
                        string[] Heading = txtSpecification.Text.Split('(');
                        Header = Heading[0];
                        string Title = txtSpecification.Text;
                    }
                    else
                    {
                        Header = txtSpecification.Text;
                        string Title = txtSpecification.Text;
                    }
                }
                else
                {
                    Header = txtEmailSub.Text;
                    string Title = txtEmailSub.Text;
                }
                if (sRefIndex != null && sRefIndex != "")
                {
                    string url = "http://192.9.200.146/Attachment/Notes.aspx?handle=J_PREORDER_WORKFLOW&Index=" + Request.QueryString["WFID"] + "&user=" + Request.QueryString["u"] + "&Em=" + MailTo + "&Hd=" + Header + "&Tl=" + txtSpecification.Text + "&RefHandle=" + sRefHandle + "&RefIndex=" + sRefIndex;
                    string s = "window.open('" + url + "','abc','width=1300,height=700,left=100,top=100,resizable=yes,scrollbars=yes');"; //," + hdfWFID.Value + " 'width=300,height=100,left=100,top=100,resizable=yes'
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else
                {

                    string url = "http://192.9.200.146/Attachment/Notes.aspx?handle=J_PREORDER_WORKFLOW&Index=" + Request.QueryString["WFID"] + "&user=" + Request.QueryString["u"] + "&Em=" + MailTo + "&Hd=" + Header + "&Tl=" + Title;
                    string s = "window.open('" + url + "','abc','width=1300,height=700,left=100,top=100,resizable=yes,scrollbars=yes');"; //, 'width=300,height=100,left=100,top=100,resizable=yes'
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
            }
        }

        //Genrate Random No 
        public string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }
        public string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }

        protected void btnIdmsReceipt_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Status"] == "Technical offer Received")
            {
                Response.Redirect("GeneratePreOrderReceipt.aspx?u=" + Request.QueryString["u"] + "&WFID=" + Request.QueryString["WFID"] + "&WFPID="+ hdfParentWFID.Value);
            }

        }
    }
}