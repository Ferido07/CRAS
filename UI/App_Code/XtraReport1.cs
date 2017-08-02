using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRControlStyle EvenStyle;
    private XRControlStyle OddStyle;
    private ReportHeaderBand ReportHeader;
    private XRLabel TitleXRLabel;
    private XRPageInfo DateXRPageInfo;
    private PageFooterBand PageFooter;
    private XRPageInfo TotalPagesXRPageInfo;
    private XRPageInfo PageXRPageInfo;
    private PageHeaderBand PageHeader;
    private XRLabel SignatureXRLabel;
    private XRLabel GradeXRLabel;
    private XRLabel NameXRLabel;
    private XRLabel YearXRLabel;
    private XRLabel RegistrationNoXRLabel;
    private XRControlStyle Page_Header;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public XtraReport1()
	{
		InitializeComponent();
	}
	
	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
            string resourceFileName = "XtraReport1.resx";
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.EvenStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.OddStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.TitleXRLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.DateXRPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.TotalPagesXRPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.PageXRPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.SignatureXRLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.GradeXRLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.NameXRLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.YearXRLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.RegistrationNoXRLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.Page_Header = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.EvenStyleName = "EvenStyle";
            this.Detail.HeightF = 20F;
            this.Detail.Name = "Detail";
            this.Detail.OddStyleName = "OddStyle";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 40F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseTextAlignment = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // EvenStyle
            // 
            this.EvenStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.EvenStyle.BorderColor = System.Drawing.Color.Transparent;
            this.EvenStyle.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.EvenStyle.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.EvenStyle.BorderWidth = 0F;
            this.EvenStyle.ForeColor = System.Drawing.Color.Black;
            this.EvenStyle.Name = "EvenStyle";
            this.EvenStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 0, 0, 0, 100F);
            this.EvenStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // OddStyle
            // 
            this.OddStyle.BackColor = System.Drawing.Color.White;
            this.OddStyle.BorderColor = System.Drawing.Color.Transparent;
            this.OddStyle.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.OddStyle.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.OddStyle.BorderWidth = 0F;
            this.OddStyle.Name = "OddStyle";
            this.OddStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 0, 0, 0, 100F);
            this.OddStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.TitleXRLabel,
            this.DateXRPageInfo});
            this.ReportHeader.HeightF = 116.6667F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // TitleXRLabel
            // 
            this.TitleXRLabel.Font = new System.Drawing.Font("Times New Roman", 18F);
            this.TitleXRLabel.LocationFloat = new DevExpress.Utils.PointFloat(193.3331F, 10.00001F);
            this.TitleXRLabel.Name = "TitleXRLabel";
            this.TitleXRLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TitleXRLabel.SizeF = new System.Drawing.SizeF(350F, 45.29166F);
            this.TitleXRLabel.StylePriority.UseFont = false;
            this.TitleXRLabel.StylePriority.UseTextAlignment = false;
            this.TitleXRLabel.Text = "Manually Added Student Records";
            this.TitleXRLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // DateXRPageInfo
            // 
            this.DateXRPageInfo.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.DateXRPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(555.5416F, 75F);
            this.DateXRPageInfo.Name = "DateXRPageInfo";
            this.DateXRPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.DateXRPageInfo.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.DateXRPageInfo.SizeF = new System.Drawing.SizeF(154.1667F, 22.99999F);
            this.DateXRPageInfo.StylePriority.UseFont = false;
            this.DateXRPageInfo.StylePriority.UseTextAlignment = false;
            this.DateXRPageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.TotalPagesXRPageInfo,
            this.PageXRPageInfo});
            this.PageFooter.HeightF = 486.125F;
            this.PageFooter.Name = "PageFooter";
            // 
            // TotalPagesXRPageInfo
            // 
            this.TotalPagesXRPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(695.8332F, 453.125F);
            this.TotalPagesXRPageInfo.Name = "TotalPagesXRPageInfo";
            this.TotalPagesXRPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TotalPagesXRPageInfo.PageInfo = DevExpress.XtraPrinting.PageInfo.Total;
            this.TotalPagesXRPageInfo.SizeF = new System.Drawing.SizeF(18.04169F, 23F);
            this.TotalPagesXRPageInfo.StylePriority.UseTextAlignment = false;
            this.TotalPagesXRPageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // PageXRPageInfo
            // 
            this.PageXRPageInfo.Format = "Page {0}  of ";
            this.PageXRPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(620.1248F, 453.125F);
            this.PageXRPageInfo.Name = "PageXRPageInfo";
            this.PageXRPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.PageXRPageInfo.SizeF = new System.Drawing.SizeF(75.70837F, 23F);
            this.PageXRPageInfo.StylePriority.UseTextAlignment = false;
            this.PageXRPageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.SignatureXRLabel,
            this.GradeXRLabel,
            this.NameXRLabel,
            this.YearXRLabel,
            this.RegistrationNoXRLabel});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StyleName = "Page_Header";
            // 
            // SignatureXRLabel
            // 
            this.SignatureXRLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.SignatureXRLabel.ForeColor = System.Drawing.Color.White;
            this.SignatureXRLabel.LocationFloat = new DevExpress.Utils.PointFloat(620.1248F, 0F);
            this.SignatureXRLabel.Multiline = true;
            this.SignatureXRLabel.Name = "SignatureXRLabel";
            this.SignatureXRLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.SignatureXRLabel.SizeF = new System.Drawing.SizeF(93.75F, 25F);
            this.SignatureXRLabel.StylePriority.UseFont = false;
            this.SignatureXRLabel.StylePriority.UseForeColor = false;
            this.SignatureXRLabel.StylePriority.UseTextAlignment = false;
            this.SignatureXRLabel.Text = " Signature";
            this.SignatureXRLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GradeXRLabel
            // 
            this.GradeXRLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.GradeXRLabel.ForeColor = System.Drawing.Color.White;
            this.GradeXRLabel.LocationFloat = new DevExpress.Utils.PointFloat(555.5415F, 0F);
            this.GradeXRLabel.Name = "GradeXRLabel";
            this.GradeXRLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.GradeXRLabel.SizeF = new System.Drawing.SizeF(64.58337F, 25F);
            this.GradeXRLabel.StylePriority.UseFont = false;
            this.GradeXRLabel.StylePriority.UseForeColor = false;
            this.GradeXRLabel.StylePriority.UseTextAlignment = false;
            this.GradeXRLabel.Text = " Grade";
            this.GradeXRLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // NameXRLabel
            // 
            this.NameXRLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.NameXRLabel.ForeColor = System.Drawing.Color.White;
            this.NameXRLabel.LocationFloat = new DevExpress.Utils.PointFloat(261.0414F, 0F);
            this.NameXRLabel.Name = "NameXRLabel";
            this.NameXRLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.NameXRLabel.SizeF = new System.Drawing.SizeF(294.5001F, 25F);
            this.NameXRLabel.StylePriority.UseFont = false;
            this.NameXRLabel.StylePriority.UseForeColor = false;
            this.NameXRLabel.StylePriority.UseTextAlignment = false;
            this.NameXRLabel.Text = " Name";
            this.NameXRLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // YearXRLabel
            // 
            this.YearXRLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.YearXRLabel.ForeColor = System.Drawing.Color.White;
            this.YearXRLabel.LocationFloat = new DevExpress.Utils.PointFloat(181.8748F, 0F);
            this.YearXRLabel.Multiline = true;
            this.YearXRLabel.Name = "YearXRLabel";
            this.YearXRLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.YearXRLabel.SizeF = new System.Drawing.SizeF(79.16666F, 25F);
            this.YearXRLabel.StylePriority.UseFont = false;
            this.YearXRLabel.StylePriority.UseForeColor = false;
            this.YearXRLabel.StylePriority.UseTextAlignment = false;
            this.YearXRLabel.Text = " Year\r\n";
            this.YearXRLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // RegistrationNoXRLabel
            // 
            this.RegistrationNoXRLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.RegistrationNoXRLabel.ForeColor = System.Drawing.Color.White;
            this.RegistrationNoXRLabel.LocationFloat = new DevExpress.Utils.PointFloat(40.20808F, 0F);
            this.RegistrationNoXRLabel.Name = "RegistrationNoXRLabel";
            this.RegistrationNoXRLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.RegistrationNoXRLabel.SizeF = new System.Drawing.SizeF(141.6667F, 25F);
            this.RegistrationNoXRLabel.StyleName = "Page_Header";
            this.RegistrationNoXRLabel.StylePriority.UseFont = false;
            this.RegistrationNoXRLabel.StylePriority.UseForeColor = false;
            this.RegistrationNoXRLabel.StylePriority.UsePadding = false;
            this.RegistrationNoXRLabel.StylePriority.UseTextAlignment = false;
            this.RegistrationNoXRLabel.Text = " Registration No";
            this.RegistrationNoXRLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Page_Header
            // 
            this.Page_Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(124)))), ((int)(((byte)(209)))));
            this.Page_Header.BorderColor = System.Drawing.Color.Transparent;
            this.Page_Header.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Page_Header.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Page_Header.ForeColor = System.Drawing.Color.White;
            this.Page_Header.Name = "Page_Header";
            this.Page_Header.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 0, 6, 0, 100F);
            this.Page_Header.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // XtraReport1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter,
            this.PageHeader});
            this.Margins = new System.Drawing.Printing.Margins(49, 48, 40, 40);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.EvenStyle,
            this.OddStyle,
            this.Page_Header});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    public void AddBoundLabel(String bindingMember, Rectangle bounds)
    {
        //create a label
        XRLabel label = new XRLabel();
        //add the label to the report'ss Detail band
        Detail.Controls.Add(label);

        //set its location and size
        label.Location = bounds.Location;
        label.Size = bounds.Size;

        //set padding
        label.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
        
        //binding it to the bindingMember data field
        //when the dataSource parameter is null the report's data source is used
        label.DataBindings.Add("Text", null, bindingMember);
    }
}
