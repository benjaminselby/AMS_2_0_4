<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ReportViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ExportPdfButton = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.UserActivityTmr = New System.Windows.Forms.Timer(Me.components)
        Me.PdfViewer = New AxAcroPDFLib.AxAcroPDF()
        Me.StudentReportsCbx = New System.Windows.Forms.ComboBox()
        Me.SelectViewGbx = New System.Windows.Forms.GroupBox()
        Me.StudentServicesRbtn = New System.Windows.Forms.RadioButton()
        Me.StudentReportsRbtn = New System.Windows.Forms.RadioButton()
        Me.StudentDetailRbtn = New System.Windows.Forms.RadioButton()
        Me.ClassOverviewRbtn = New System.Windows.Forms.RadioButton()
        Me.ClassFilterLbl = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NoStudentServicesReportLbl = New System.Windows.Forms.Label()
        Me.NoClassSelectedLbl = New System.Windows.Forms.Label()
        Me.SearchTipsLbl = New System.Windows.Forms.Label()
        Me.NoStudentSelectedLbl = New System.Windows.Forms.Label()
        Me.ViewLoadingLbl = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PdfViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SelectViewGbx.SuspendLayout()
        Me.SuspendLayout()
        '
        'ReportViewer
        '
        Me.ReportViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportViewer.AutoScroll = True
        Me.ReportViewer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ReportViewer.BackColor = System.Drawing.Color.AliceBlue
        Me.ReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ReportViewer.DocumentMapWidth = 1
        Me.ReportViewer.Font = New System.Drawing.Font("Gill Sans MT", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportViewer.Location = New System.Drawing.Point(230, 5)
        Me.ReportViewer.Name = "ReportViewer"
        Me.ReportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote
        Me.ReportViewer.PromptAreaCollapsed = True
        Me.ReportViewer.ServerReport.ReportServerUrl = New System.Uri("http://testserver2.woodcroft.sa.edu.au/Reportserver", System.UriKind.Absolute)
        Me.ReportViewer.ShowToolBar = False
        Me.ReportViewer.Size = New System.Drawing.Size(4447, 2443)
        Me.ReportViewer.TabIndex = 6
        Me.ReportViewer.Visible = False
        '
        'ExportPdfButton
        '
        Me.ExportPdfButton.Font = New System.Drawing.Font("Gill Sans MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExportPdfButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.ExportPdfButton.Location = New System.Drawing.Point(153, 7)
        Me.ExportPdfButton.Name = "ExportPdfButton"
        Me.ExportPdfButton.Size = New System.Drawing.Size(111, 27)
        Me.ExportPdfButton.TabIndex = 12
        Me.ExportPdfButton.Text = "Export to PDF"
        Me.ExportPdfButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ExportPdfButton.UseVisualStyleBackColor = True
        Me.ExportPdfButton.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(83, 105)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'UserActivityTmr
        '
        Me.UserActivityTmr.Enabled = True
        Me.UserActivityTmr.Interval = 60000
        '
        'PdfViewer
        '
        Me.PdfViewer.Enabled = True
        Me.PdfViewer.Location = New System.Drawing.Point(270, 37)
        Me.PdfViewer.Name = "PdfViewer"
        Me.PdfViewer.OcxState = CType(resources.GetObject("PdfViewer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.PdfViewer.Size = New System.Drawing.Size(1387, 918)
        Me.PdfViewer.TabIndex = 14
        Me.PdfViewer.Visible = False
        '
        'StudentReportsCbx
        '
        Me.StudentReportsCbx.Font = New System.Drawing.Font("Gill Sans MT", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StudentReportsCbx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.StudentReportsCbx.FormattingEnabled = True
        Me.StudentReportsCbx.Location = New System.Drawing.Point(270, 5)
        Me.StudentReportsCbx.Name = "StudentReportsCbx"
        Me.StudentReportsCbx.Size = New System.Drawing.Size(315, 26)
        Me.StudentReportsCbx.TabIndex = 16
        Me.StudentReportsCbx.Text = "No report selected"
        Me.StudentReportsCbx.Visible = False
        '
        'SelectViewGbx
        '
        Me.SelectViewGbx.BackColor = System.Drawing.Color.AliceBlue
        Me.SelectViewGbx.Controls.Add(Me.StudentServicesRbtn)
        Me.SelectViewGbx.Controls.Add(Me.StudentReportsRbtn)
        Me.SelectViewGbx.Controls.Add(Me.StudentDetailRbtn)
        Me.SelectViewGbx.Controls.Add(Me.ClassOverviewRbtn)
        Me.SelectViewGbx.Font = New System.Drawing.Font("Gill Sans MT", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SelectViewGbx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.SelectViewGbx.Location = New System.Drawing.Point(12, 123)
        Me.SelectViewGbx.Name = "SelectViewGbx"
        Me.SelectViewGbx.Size = New System.Drawing.Size(252, 141)
        Me.SelectViewGbx.TabIndex = 4
        Me.SelectViewGbx.TabStop = False
        Me.SelectViewGbx.Text = "Select View"
        '
        'StudentServicesRbtn
        '
        Me.StudentServicesRbtn.AutoSize = True
        Me.StudentServicesRbtn.Location = New System.Drawing.Point(16, 106)
        Me.StudentServicesRbtn.Name = "StudentServicesRbtn"
        Me.StudentServicesRbtn.Size = New System.Drawing.Size(124, 25)
        Me.StudentServicesRbtn.TabIndex = 3
        Me.StudentServicesRbtn.TabStop = True
        Me.StudentServicesRbtn.Text = "Student Services"
        Me.StudentServicesRbtn.UseVisualStyleBackColor = True
        '
        'StudentReportsRbtn
        '
        Me.StudentReportsRbtn.AutoSize = True
        Me.StudentReportsRbtn.Location = New System.Drawing.Point(16, 79)
        Me.StudentReportsRbtn.Name = "StudentReportsRbtn"
        Me.StudentReportsRbtn.Size = New System.Drawing.Size(106, 25)
        Me.StudentReportsRbtn.TabIndex = 2
        Me.StudentReportsRbtn.TabStop = True
        Me.StudentReportsRbtn.Text = "View Reports"
        Me.StudentReportsRbtn.UseVisualStyleBackColor = True
        '
        'StudentDetailRbtn
        '
        Me.StudentDetailRbtn.AutoSize = True
        Me.StudentDetailRbtn.Location = New System.Drawing.Point(16, 25)
        Me.StudentDetailRbtn.Name = "StudentDetailRbtn"
        Me.StudentDetailRbtn.Size = New System.Drawing.Size(112, 25)
        Me.StudentDetailRbtn.TabIndex = 1
        Me.StudentDetailRbtn.Text = "Student Detail"
        Me.StudentDetailRbtn.UseVisualStyleBackColor = True
        '
        'ClassOverviewRbtn
        '
        Me.ClassOverviewRbtn.AutoSize = True
        Me.ClassOverviewRbtn.Location = New System.Drawing.Point(16, 52)
        Me.ClassOverviewRbtn.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.ClassOverviewRbtn.Name = "ClassOverviewRbtn"
        Me.ClassOverviewRbtn.Size = New System.Drawing.Size(118, 25)
        Me.ClassOverviewRbtn.TabIndex = 0
        Me.ClassOverviewRbtn.Text = "Class Overview"
        Me.ClassOverviewRbtn.UseVisualStyleBackColor = True
        '
        'ClassFilterLbl
        '
        Me.ClassFilterLbl.AutoSize = True
        Me.ClassFilterLbl.Font = New System.Drawing.Font("Gill Sans MT", 10.0!)
        Me.ClassFilterLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.ClassFilterLbl.Location = New System.Drawing.Point(12, 267)
        Me.ClassFilterLbl.Name = "ClassFilterLbl"
        Me.ClassFilterLbl.Size = New System.Drawing.Size(42, 21)
        Me.ClassFilterLbl.TabIndex = 17
        Me.ClassFilterLbl.Text = "Class:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Gill Sans MT", 10.0!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 320)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 21)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Student:"
        '
        'NoStudentServicesReportLbl
        '
        Me.NoStudentServicesReportLbl.AutoSize = True
        Me.NoStudentServicesReportLbl.Font = New System.Drawing.Font("Gill Sans MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoStudentServicesReportLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.NoStudentServicesReportLbl.Location = New System.Drawing.Point(284, 50)
        Me.NoStudentServicesReportLbl.Name = "NoStudentServicesReportLbl"
        Me.NoStudentServicesReportLbl.Size = New System.Drawing.Size(334, 18)
        Me.NoStudentServicesReportLbl.TabIndex = 19
        Me.NoStudentServicesReportLbl.Text = "No Student Services information is available for this student."
        Me.NoStudentServicesReportLbl.Visible = False
        '
        'NoClassSelectedLbl
        '
        Me.NoClassSelectedLbl.AutoSize = True
        Me.NoClassSelectedLbl.Font = New System.Drawing.Font("Gill Sans MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoClassSelectedLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.NoClassSelectedLbl.Location = New System.Drawing.Point(284, 50)
        Me.NoClassSelectedLbl.Name = "NoClassSelectedLbl"
        Me.NoClassSelectedLbl.Size = New System.Drawing.Size(104, 18)
        Me.NoClassSelectedLbl.TabIndex = 20
        Me.NoClassSelectedLbl.Text = "No class selected."
        Me.NoClassSelectedLbl.Visible = False
        '
        'SearchTipsLbl
        '
        Me.SearchTipsLbl.AutoSize = True
        Me.SearchTipsLbl.Font = New System.Drawing.Font("Gill Sans MT", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchTipsLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.SearchTipsLbl.Location = New System.Drawing.Point(12, 373)
        Me.SearchTipsLbl.Name = "SearchTipsLbl"
        Me.SearchTipsLbl.Size = New System.Drawing.Size(236, 72)
        Me.SearchTipsLbl.TabIndex = 21
        Me.SearchTipsLbl.Text = "* Type any part of a student's name to start " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "searching. For example, if you typ" &
    "e ""TONY"" " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "then you will find students such as " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """Tony Jones"" as well as ""Mark A" &
    "ntony""."
        '
        'NoStudentSelectedLbl
        '
        Me.NoStudentSelectedLbl.AutoSize = True
        Me.NoStudentSelectedLbl.Font = New System.Drawing.Font("Gill Sans MT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoStudentSelectedLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.NoStudentSelectedLbl.Location = New System.Drawing.Point(284, 50)
        Me.NoStudentSelectedLbl.Name = "NoStudentSelectedLbl"
        Me.NoStudentSelectedLbl.Size = New System.Drawing.Size(119, 18)
        Me.NoStudentSelectedLbl.TabIndex = 22
        Me.NoStudentSelectedLbl.Text = "No student selected."
        Me.NoStudentSelectedLbl.Visible = False
        '
        'ViewLoadingLbl
        '
        Me.ViewLoadingLbl.AutoSize = True
        Me.ViewLoadingLbl.Font = New System.Drawing.Font("Gill Sans MT", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewLoadingLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(131, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.ViewLoadingLbl.Location = New System.Drawing.Point(284, 74)
        Me.ViewLoadingLbl.Name = "ViewLoadingLbl"
        Me.ViewLoadingLbl.Size = New System.Drawing.Size(176, 23)
        Me.ViewLoadingLbl.TabIndex = 23
        Me.ViewLoadingLbl.Text = "Loading. Please wait..."
        Me.ViewLoadingLbl.Visible = False
        '
        'Main
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(1630, 966)
        Me.Controls.Add(Me.ViewLoadingLbl)
        Me.Controls.Add(Me.NoStudentSelectedLbl)
        Me.Controls.Add(Me.SearchTipsLbl)
        Me.Controls.Add(Me.NoClassSelectedLbl)
        Me.Controls.Add(Me.NoStudentServicesReportLbl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ClassFilterLbl)
        Me.Controls.Add(Me.PdfViewer)
        Me.Controls.Add(Me.StudentReportsCbx)
        Me.Controls.Add(Me.ExportPdfButton)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.SelectViewGbx)
        Me.Controls.Add(Me.ReportViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Academic Monitoring System"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PdfViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SelectViewGbx.ResumeLayout(False)
        Me.SelectViewGbx.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StudentsMbx As MatchingComboBox
    Friend WithEvents ClassesMbx As MatchingComboBox
    Friend WithEvents ExportPdfButton As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents chkbtnStudent As System.Windows.Forms.CheckBox
    Friend WithEvents UserActivityTmr As System.Windows.Forms.Timer
    Private WithEvents ReportViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents PdfViewer As AxAcroPDFLib.AxAcroPDF
    Friend WithEvents StudentReportsCbx As ComboBox
    Friend WithEvents SelectViewGbx As GroupBox
    Friend WithEvents StudentServicesRbtn As RadioButton
    Friend WithEvents StudentReportsRbtn As RadioButton
    Friend WithEvents StudentDetailRbtn As RadioButton
    Friend WithEvents ClassOverviewRbtn As RadioButton
    Friend WithEvents ClassFilterLbl As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents NoStudentServicesReportLbl As Label
    Friend WithEvents NoClassSelectedLbl As Label
    Friend WithEvents SearchTipsLbl As Label
    Friend WithEvents NoStudentSelectedLbl As Label
    Friend WithEvents ViewLoadingLbl As Label
End Class
