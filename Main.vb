Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Reporting.WinForms
Imports System.Configuration



Public Class Main

    Private versionNumber As String = "2.0.4"

    Private currentSemester As Integer
    Private currentYear As Integer
    Private currentUserId As Integer
    Private currentUserAmsLevel As String
    Private currentUserLearningArea As String

    ' True if portrait/ false if landscape - used for pdf export algorithm.
    Private blnPortraitPDF As Boolean
    Private inactivityMsec As Integer
    Private currentUserName As String

    ' Constants and style values. 
    Private Const AllStudentsLabel As String = "All students"
    Private Const lineSeparator As String = "------------------------"
    Private studentButtonBackColorDefault As Color = Color.FromArgb(240, 248, 255)
    Private studentButtonForeColorDefault As Color = Color.FromArgb(33, 62, 102)
    Private studentButtonBackColorSelected As Color = studentButtonForeColorDefault
    Private studentButtonForeColorSelected As Color = studentButtonBackColorDefault
    Private Const intViewerRightBorderWidth As Integer = 10
    Private Const intViewerBottomBorderWidth As Integer = 40

    Private Const userActivityTimeoutMsec As Integer = 900000 ' 15 min x 60 sec x 1000 msec
    Private renderingTimeElapsedMsec As Integer
    Private renderingMessageTimeoutMsec As Integer = 1000


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Me.Text = Me.Text & " v" & versionNumber

        ' The MatchingComboBox is a custom control which enables better text-matching than an ordinary ComboBox. 
        ' It can't be successfully added to this form in the Design window because its code keeps disappearing 
        ' from the InitialiseComponent() function, so I have added it here. 
        Me.StudentsMbx = New MatchingComboBox()
        With Me.StudentsMbx
            .FilterRule = Nothing
            .Font = New System.Drawing.Font("Gill Sans MT", 10)
            .ForeColor = Color.FromArgb(33, 62, 102)
            .FormattingEnabled = True
            .Name = "StudentsMbx"
            .PropertySelector = Nothing
            .Location = New System.Drawing.Point(12, 344)
            .Size = New System.Drawing.Size(252, 26)
            .SuggestBoxHeight = 96
            .SuggestListOrderRule = Nothing
            .Anchor = AnchorStyles.Top + AnchorStyles.Left
            .TabIndex = 1
            .Visible = True
        End With
        Me.Controls.Add(Me.StudentsMbx)

        Me.ClassesMbx = New MatchingComboBox()
        With Me.ClassesMbx
            .FilterRule = Nothing
            .Font = New System.Drawing.Font("Gill Sans MT", 10)
            .ForeColor = Color.FromArgb(33, 62, 102)
            .FormattingEnabled = True
            .Name = "ClassesMbx"
            .PropertySelector = Nothing
            .Location = New System.Drawing.Point(12, 291)
            .Size = New System.Drawing.Size(252, 26)
            .SuggestBoxHeight = 96
            .SuggestListOrderRule = Nothing
            .Anchor = AnchorStyles.Top + AnchorStyles.Left
            .TabIndex = 2
            .Visible = True
        End With
        Me.Controls.Add(Me.ClassesMbx)


        ' Set form size to fill primary monitor. 
        Dim screenLeft As Integer = Screen.PrimaryScreen.Bounds.Left
        Dim screenTop As Integer = Screen.PrimaryScreen.Bounds.Top
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
        Me.Size = New Size(screenWidth, screenHeight)
        Me.Location = New Point(screenLeft, screenTop)

        ' Obtain user name by stripping 'WOODCROFT\' domain prefix from Windows ID. 
        currentUserName = Strings.Replace(
                Security.Principal.WindowsIdentity.GetCurrent().Name,
                ConfigurationManager.AppSettings("NetworkLoginPrefix"), "")

        PopulateClassesMbx()
        PopulateStudentsMbx()
        ScaleViewport()

        ClassesMbx.SelectedIndex = 0

        StudentsMbx.Select()
        StudentDetailRbtn.Checked = True

        currentYear = Date.Now.Year
        currentSemester = If(Date.Now.Month <= 6, 1, 2)
        inactivityMsec = 0

    End Sub


    ' ==================================================================================================
    ' ==================================================================================================
    ' FUNCTIONS
    ' ==================================================================================================
    ' ==================================================================================================


    Private Sub HandleError(Ex As Exception)
        MsgBox("Please contact Data Management for assistance" + vbCrLf + vbCrLf + Ex.Message,
               vbOKOnly + vbCritical, "An Error Occurred - Program Will Exit")
        Me.Close()
    End Sub


    Private Sub ScaleViewport()

        ' Scale SSRS report and PDF viewers in the main form based on the current window size. 
        ReportViewer.Left = 270
        ReportViewer.Top = 5
        ReportViewer.Width = Me.Width - ReportViewer.Left - intViewerRightBorderWidth
        ReportViewer.Height = Me.Height - ReportViewer.Top - intViewerBottomBorderWidth

        AxAcroPDF1.Left = 270
        AxAcroPDF1.Top = StudentReportsCbx.Top + StudentReportsCbx.Height + 5
        AxAcroPDF1.Width = Me.Width - AxAcroPDF1.Left - intViewerRightBorderWidth
        AxAcroPDF1.Height = Me.Height - AxAcroPDF1.Top - intViewerBottomBorderWidth

    End Sub


    Private Sub PopulateClassesMbx()

        Try

            ClassesMbx.Items.Add(AllStudentsLabel)

            Using synergyConnection As New SqlConnection(
                    ConfigurationManager.ConnectionStrings("Synergy").ConnectionString)

                synergyConnection.Open()

                ' Get current user's Synergy ID 
                ' This query should only return a single row. 
                Using userInfoCmd As New SqlCommand(
                        ConfigurationManager.AppSettings("GetUserDetailsFromNetworkLoginProc"), synergyConnection)

                    userInfoCmd.Parameters.AddWithValue("NetworkLogin", currentUserName)
                    userInfoCmd.CommandType = CommandType.StoredProcedure

                    Using userInfoReader As SqlDataReader = userInfoCmd.ExecuteReader()

                        If userInfoReader.HasRows Then
                            userInfoReader.Read()
                            currentUserId = CInt(userInfoReader("ID").ToString)
                        Else
                            Throw New Exception(String.Format(
                                "Could not find Synergy ID for current user " & vbCrLf &
                                "with network login '{0}'" & vbCrLf & vbCrLf &
                                "Please contact Data Management for assistance.",
                                currentUserName.ToUpper()))
                        End If

                    End Using
                End Using

                '=======================================================================================

                ' Ascertain whether current user has manager (M) level access. 

                ' This query should only return a single row. 
                Using amsUsersCmd As New SqlCommand(
                        ConfigurationManager.AppSettings("GetUserAccessProc"), synergyConnection)

                    amsUsersCmd.CommandType = CommandType.StoredProcedure
                    amsUsersCmd.Parameters.AddWithValue("ID", currentUserId.ToString)

                    Using amsUsersRdr As SqlDataReader = amsUsersCmd.ExecuteReader()

                        If amsUsersRdr.HasRows Then
                            amsUsersRdr.Read()
                            currentUserAmsLevel = amsUsersRdr("UserType").ToString
                            currentUserLearningArea = amsUsersRdr("Learning Area").ToString
                        End If

                    End Using 'AmsUsersReader
                End Using 'AmsUsersCmd

                '=======================================================================================

                ' Get current user's classes.
                Using currentUserClassListCmd As New SqlCommand(
                        ConfigurationManager.AppSettings("GetUserClassListProc"), synergyConnection)

                    currentUserClassListCmd.CommandType = CommandType.StoredProcedure
                    currentUserClassListCmd.Parameters.AddWithValue("ID", currentUserId)

                    Using currentUserClassListRdr As SqlDataReader = currentUserClassListCmd.ExecuteReader()
                        If currentUserClassListRdr.HasRows Then

                            ClassesMbx.Items.Add(lineSeparator)

                            Do While currentUserClassListRdr.Read()
                                ClassesMbx.Items.Add(New StudentClass(
                                    currentUserClassListRdr("ClassCode").ToString,
                                    currentUserClassListRdr("Description").ToString))
                            Loop
                        End If
                    End Using
                End Using

                '=======================================================================================

                ' If current user has been assigned level = M (Manager) in the AMS Users table, 
                ' add extra classes after a line break. 

                If currentUserAmsLevel = "M" Then

                    ClassesMbx.Items.Add(lineSeparator)

                    Using extraClassesCmd As New SqlCommand()

                        extraClassesCmd.CommandType = CommandType.StoredProcedure
                        extraClassesCmd.Connection = synergyConnection

                        If Regex.Match(currentUserLearningArea, "^\s*[0-9]+\s*(,{1}\s*[0-9]{1,2}\s*)*$").Success Then

                            ' Learning Area consisting of one or more year levels 
                            ' separated by commas. e.g. "6" Or "6,7,8"

                            extraClassesCmd.CommandText = ConfigurationManager.AppSettings("GetClassesForYearLevelsProc")
                            extraClassesCmd.Parameters.AddWithValue("YearLevelList", currentUserLearningArea)

                        ElseIf Regex.Match(currentUserLearningArea, "^\s*[A-z]{2}\s*(,{1}\s*[A-z]{2}\s*)*$").Success Then

                            ' Faculty heads and coordinators. Learning areas will be two-character codes
                            ' Multiple codes may be separated by commas.
                            ' eg. "SC" (Science) and "MA" (Mathematics) and "EN" (English) ==> "SC,MA,EN" etc. 

                            extraClassesCmd.CommandText = ConfigurationManager.AppSettings("GetClassesForLearningAreasProc")
                            extraClassesCmd.Parameters.AddWithValue("LearningAreaList", currentUserLearningArea)

                        ElseIf currentUserLearningArea.ToUpper = "ALL" Then

                            ' Access to all classes - e.g. for senior managers, admins, etc.

                            extraClassesCmd.CommandText = ConfigurationManager.AppSettings("GetAllClassesProc")

                        Else
                            Throw New System.ArgumentException(String.Format(
                                "Current user with ID {0} is listed as a Manager in the AMS users table " &
                                " with invalid learning areas [{1}]. Learning areas must be a list " &
                                " of either one or more years OR one or more 2-digit learning area codes," &
                                " separated by commas (e.g. '6,7,8' or 'EN,MA')." &
                                vbCrLf + vbCrLf &
                                "Please contact Data Management for assistance.",
                                currentUserId,
                                currentUserLearningArea))
                        End If

                        Using extraClassesRdr As SqlDataReader = extraClassesCmd.ExecuteReader()

                            If extraClassesRdr.HasRows Then
                                Do While extraClassesRdr.Read()
                                    ClassesMbx.Items.Add(New StudentClass(
                                        extraClassesRdr("ClassCode").ToString,
                                        extraClassesRdr("Description").ToString))
                                Loop
                            End If

                        End Using 'extraClassesRdr
                    End Using 'extraClassesCmd

                End If

            End Using ' synergyConnection

        Catch Ex As Exception
            HandleError(Ex)
        End Try

    End Sub


    Private Sub PopulateStudentsMbx()

        ' The student selector MatchingComboBox is populated with either all students at the school, 
        ' or only students in the class selected by the user. If the user has checked the 'StudentServices'
        ' radio button, only students with Student Services documents are shown.


        ' Before re-populating the current students list, save the currently selected student. 
        Dim selectedStudent As Student = StudentsMbx.SelectedItem

        StudentsMbx.Items.Clear()

        Try
            Using SynergyConn As New SqlConnection(
                    ConfigurationManager.ConnectionStrings("Synergy").ConnectionString)
                Using StudentsCmd As New SqlCommand()

                    StudentsCmd.Connection = SynergyConn
                    StudentsCmd.CommandType = CommandType.StoredProcedure

                    If ClassesMbx.SelectedItem Is Nothing _
                            Or ClassesMbx.SelectedItem Is lineSeparator _
                            Or ClassesMbx.SelectedItem Is AllStudentsLabel Then
                        StudentsCmd.CommandText = ConfigurationManager.AppSettings("GetAllStudentsProc")
                    Else
                        StudentsCmd.CommandText = ConfigurationManager.AppSettings("GetStudentsForClassProc")
                        StudentsCmd.Parameters.AddWithValue("ClassCode",
                                CType(ClassesMbx.SelectedItem, StudentClass).ClassCode)
                    End If

                    SynergyConn.Open()

                    Using StudentsReader As SqlDataReader = StudentsCmd.ExecuteReader()

                        If StudentsReader.HasRows Then
                            Do While StudentsReader.Read()

                                If StudentServicesRbtn.Checked Then

                                    If CInt(StudentsReader("StudentServicesDocumentSeq")) > 0 Then

                                        StudentsMbx.Items.Add(New Student(
                                            StudentsReader("Preferred").ToString(),
                                            StudentsReader("Surname").ToString(),
                                            StudentsReader("ID").ToString(),
                                            CInt(StudentsReader("YearLevel")),
                                            CInt(StudentsReader("StudentServicesDocumentSeq"))))

                                    End If

                                Else

                                    StudentsMbx.Items.Add(New Student(
                                        StudentsReader("Preferred").ToString(),
                                        StudentsReader("Surname").ToString(),
                                        StudentsReader("ID").ToString(),
                                        CInt(StudentsReader("YearLevel")),
                                        CInt(StudentsReader("StudentServicesDocumentSeq"))))

                                End If

                            Loop
                        End If

                    End Using
                End Using
            End Using

            ' If a student has been already selected by the user, that student is only retained
            ' if they exist in the currently selected class. Otherwise, we set [currentStudent = nothing].
            Dim currentStudentInClass As Boolean = False
            If selectedStudent IsNot Nothing Then

                ' Is there a faster way to select the current student from the list of Combobox items?
                For Each student As Student In StudentsMbx.Items
                    If student.id = selectedStudent.id Then
                        StudentsMbx.SelectedItem = student
                        currentStudentInClass = True
                        Exit For
                    End If
                Next

            End If

            If Not currentStudentInClass Then
                StudentsMbx.Text = "Search for a student"
                StudentsMbx.SelectedItem = Nothing
                StudentsMbx.SelectedIndex = -1
                StudentsMbx_SelectedValueChanged(StudentsMbx, Nothing)
            End If

        Catch Ex As Exception
            HandleError(Ex)
        End Try

    End Sub


    Private Sub ClearViewport()

        ' Removes everything from the current viewport. Add components back in as required. 

        ReportViewer.Clear()
        ReportViewer.Visible = False
        StudentReportsCbx.Visible = False
        AxAcroPDF1.src = Nothing
        AxAcroPDF1.Visible = False
        ExportPdfButton.Visible = False
        NoStudentSelectedLbl.Visible = False
        StudentReportsCbx.Visible = False

    End Sub


    Private Sub DisplayStudentReport()

        ClearViewport()
        StudentReportsCbx.Visible = True

        If StudentsMbx.SelectedItem Is Nothing Then
            StudentReportsCbx.Items.Clear()
            StudentReportsCbx.SelectedItem = Nothing
            StudentReportsCbx.Text = "No student selected"
            NoStudentSelectedLbl.Visible = True
            Return
        End If


        If StudentReportsCbx.SelectedItem Is Nothing Then
            Return
        End If

        AxAcroPDF1.Visible = True

        Dim reportSequenceNumber As Integer = CType(StudentReportsCbx.SelectedItem, StudentReportDocument).DocumentSeqNumber
        Dim sFilePath As String
        Dim buffer As Byte()

        Try
            Using synergyConn As New SqlConnection(
                    ConfigurationManager.ConnectionStrings("Synergy").ConnectionString)

                synergyConn.Open()

                ' This query should return only one result. 
                Using getDocumentDataCmd As New SqlCommand(
                        ConfigurationManager.AppSettings("GetDocumentDataProc"), synergyConn)

                    getDocumentDataCmd.CommandType = CommandType.StoredProcedure
                    getDocumentDataCmd.Parameters.AddWithValue("DocumentSeq", reportSequenceNumber.ToString())
                    buffer = getDocumentDataCmd.ExecuteScalar()

                End Using
            End Using

            ' Check for empty buffer here for cases where no student report is available. 
            If Not buffer Is Nothing Then

                ' Save PDF data to a temp file location and display in the form. 

                sFilePath = System.IO.Path.GetTempFileName()
                System.IO.File.Move(sFilePath, System.IO.Path.ChangeExtension(sFilePath, ".pdf"))
                sFilePath = System.IO.Path.ChangeExtension(sFilePath, ".pdf")
                System.IO.File.WriteAllBytes(sFilePath, buffer)

                AxAcroPDF1.src = sFilePath
                AxAcroPDF1.setPageMode("none")
                AxAcroPDF1.setShowToolbar(False)
                AxAcroPDF1.setLayoutMode("SinglePage")
                AxAcroPDF1.Visible = True

                My.Computer.FileSystem.DeleteFile(sFilePath)
            Else
                MsgBox(String.Format(
                        "No report was found for {0} (document seq {1})." +
                        "Please contact Data Management for assistance.",
                        StudentsMbx.SelectedItem.ToString(),
                        reportSequenceNumber.ToString()),
                    MsgBoxStyle.Critical,
                    "No document found")
            End If

        Catch Ex As Exception
            HandleError(Ex)
        End Try

    End Sub


    Private Sub DisplayStudentOverview()

        ClearViewport()

        If StudentsMbx.SelectedItem Is Nothing Then
            NoStudentSelectedLbl.Visible = True
            Return
        End If

        NoStudentSelectedLbl.Visible = False
        ReportViewer.Visible = True
        ExportPdfButton.Visible = True

        Try

            ' TODO: Add separate report case for reception? 
            Select Case CType(StudentsMbx.SelectedItem, Student).yearLevel
                Case 0 To 5
                    ReportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings("ssrsReportPathJuniorSchoolStudent")
                Case Else
                    ReportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings("ssrsReportPathMiddleSeniorStudent")
            End Select

            AxAcroPDF1.Visible = False
            blnPortraitPDF = True
            ReportViewer.Visible = True

            ReportViewer.ServerReport.SetParameters(
                New List(Of ReportParameter) From
                    {New ReportParameter("StudentIdent", CType(StudentsMbx.SelectedItem, Student).id.ToString)})

            ReportViewer.RefreshReport()
            ReportViewer.Visible = True

        Catch Ex As Exception
            HandleError(Ex)
        End Try


    End Sub


    Private Sub DisplayClassOverview()

        ClearViewport()

        If ClassesMbx.SelectedItem Is Nothing _
                Or ClassesMbx.SelectedItem Is AllStudentsLabel _
                Or ClassesMbx.SelectedItem Is lineSeparator Then

            NoClassSelectedLbl.Visible = True
            Return

        End If

        NoClassSelectedLbl.Visible = False
        ReportViewer.Visible = True

        Dim selectedClass As StudentClass = ClassesMbx.SelectedItem

        Try

            ReportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings("ssrsReportPathClassOverview")
            ReportViewer.ServerReport.SetParameters(New List(Of ReportParameter) From {
                    New ReportParameter("ClassCode", selectedClass.ClassCode)})
            ReportViewer.RefreshReport()

        Catch Ex As Exception
            HandleError(Ex)
        End Try

    End Sub


    Private Sub DisplayStudentServicesReport()

        ClearViewport()

        If StudentsMbx.SelectedItem Is Nothing Then
            NoStudentSelectedLbl.Visible = True
            Return
        ElseIf CType(StudentsMbx.SelectedItem, Student).studentServicesDocumentSeq <= 0 Then
            NoStudentServicesReportLbl.Visible = True
            Return
        End If

        NoStudentSelectedLbl.Visible = False
        NoStudentServicesReportLbl.Visible = False
        ReportViewer.Visible = True

        Try

            Using synergyConn As New SqlConnection(
                    ConfigurationManager.ConnectionStrings("Synergy").ConnectionString)
                Using studentServicesPdfCmd As New SqlCommand(
                        ConfigurationManager.AppSettings("GetDocumentDataProc"),
                        synergyConn)

                    studentServicesPdfCmd.CommandType = CommandType.StoredProcedure
                    studentServicesPdfCmd.Parameters.AddWithValue("DocumentSeq",
                            CType(StudentsMbx.SelectedItem, Student).studentServicesDocumentSeq)

                    synergyConn.Open()

                    Using studentServicesPdfRdr As SqlDataReader = studentServicesPdfCmd.ExecuteReader()

                        If studentServicesPdfRdr.HasRows Then

                            studentServicesPdfRdr.Read()
                            Dim buffer As Byte() = studentServicesPdfRdr("Document")

                            If Not buffer Is Nothing Then

                                ' Save PDF data to a temp file location and display in the form. 

                                Dim sFilePath As String = System.IO.Path.GetTempFileName()
                                System.IO.File.Move(sFilePath, System.IO.Path.ChangeExtension(sFilePath, ".pdf"))
                                sFilePath = System.IO.Path.ChangeExtension(sFilePath, ".pdf")
                                System.IO.File.WriteAllBytes(sFilePath, buffer)

                                AxAcroPDF1.src = sFilePath '& "#toolbar=0" '&navpanes=0"
                                AxAcroPDF1.setPageMode("none")
                                AxAcroPDF1.setShowToolbar(False)
                                AxAcroPDF1.setLayoutMode("SinglePage")
                                AxAcroPDF1.Visible = True

                                My.Computer.FileSystem.DeleteFile(sFilePath)

                            End If
                        End If

                    End Using
                End Using
            End Using

        Catch Ex As Exception
            HandleError(Ex)
        End Try

    End Sub


    Private Sub PopulateStudentReportsCbx()

        StudentReportsCbx.Items.Clear()

        If StudentsMbx.SelectedItem Is Nothing Then
            StudentReportsCbx.SelectedItem = Nothing
            StudentReportsCbx.Text = "No student selected"
            Return
        End If

        Try

            Using synergyConnection As New SqlConnection(
                    ConfigurationManager.ConnectionStrings("Synergy").ConnectionString)
                Using selectStudentReportListCmd As New SqlCommand(
                        ConfigurationManager.AppSettings("GetAllStudentReportsProc"),
                        synergyConnection)

                    selectStudentReportListCmd.CommandType = CommandType.StoredProcedure
                    selectStudentReportListCmd.Parameters.AddWithValue("ID", CType(StudentsMbx.SelectedItem, Student).id)

                    synergyConnection.Open()

                    Using studentReportListDataRdr As SqlDataReader = selectStudentReportListCmd.ExecuteReader()

                        If studentReportListDataRdr.HasRows Then

                            Do While studentReportListDataRdr.Read()
                                StudentReportsCbx.Items.Add(
                                    New StudentReportDocument(
                                        StudentOwner:=CType(StudentsMbx.SelectedItem, Student),
                                        ReportTitle:=studentReportListDataRdr("Description").ToString,
                                        DocumentSeqNumber:=studentReportListDataRdr("DocumentReferencesSeq").ToString))
                            Loop

                        End If

                    End Using
                End Using
            End Using

            If StudentReportsCbx.Items.Count = 0 Then
                StudentReportsCbx.Text = "No reports available."
                StudentReportsCbx.SelectedItem = Nothing
            Else
                StudentReportsCbx.SelectedItem = StudentReportsCbx.Items(0)
            End If

        Catch Ex As Exception
            HandleError(Ex)
        End Try


    End Sub


    ' ==================================================================================================
    ' ==================================================================================================
    ' EVENT HANDLERS
    ' ==================================================================================================
    ' ==================================================================================================


    Private Sub AMS_Start_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) _
            Handles Me.FormClosed
        Application.Exit()
    End Sub


    Private Sub ClassesMbx_SelectedValueChanged(sender As Object, e As System.EventArgs) _
            Handles ClassesMbx.SelectedValueChanged

        inactivityMsec = 0

        Try

            If ClassesMbx.SelectedItem Is lineSeparator Then
                ClassesMbx.SelectedIndex = 0
            End If

            PopulateStudentsMbx()

            If ClassOverviewRbtn.Checked Then
                DisplayClassOverview()
            End If

        Catch Ex As Exception
            HandleError(Ex)
        End Try


    End Sub



    Private Sub StudentsMbx_SelectedValueChanged(sender As Object, e As EventArgs) _
            Handles StudentsMbx.SelectedValueChanged

        inactivityMsec = 0

        If StudentDetailRbtn.Checked Then
            DisplayStudentOverview()
        ElseIf StudentReportsRbtn.Checked Then
            PopulateStudentReportsCbx()
            DisplayStudentReport()
        ElseIf StudentServicesRbtn.Checked Then
            DisplayStudentServicesReport()
        End If

    End Sub



    Private Sub StudentDetailRbn_CheckedChanged(sender As System.Object, e As System.EventArgs) _
            Handles StudentDetailRbtn.CheckedChanged

        inactivityMsec = 0

        If StudentDetailRbtn.Checked Then
            StudentsMbx.Enabled = True
            PopulateStudentsMbx()
            DisplayStudentOverview()
        End If

    End Sub


    Private Sub ClassRbn_CheckedChanged(sender As System.Object, e As System.EventArgs) _
            Handles ClassOverviewRbtn.CheckedChanged

        inactivityMsec = 0

        If ClassOverviewRbtn.Checked Then
            StudentsMbx.Enabled = False
            DisplayClassOverview()
        Else
            StudentsMbx.Enabled = True
            NoClassSelectedLbl.Visible = False
        End If

    End Sub


    Private Sub StudentReportsRbtn_CheckedChanged(sender As System.Object, e As System.EventArgs) _
            Handles StudentReportsRbtn.CheckedChanged

        inactivityMsec = 0

        If StudentReportsRbtn.Checked Then

            StudentReportsCbx.Visible = True

            PopulateStudentsMbx()

            If StudentsMbx.SelectedItem Is Nothing Then
                StudentReportsCbx.Items.Clear()
                StudentReportsCbx.SelectedItem = Nothing
                StudentReportsCbx.Text = "No student selected."
            Else
                PopulateStudentReportsCbx()
                DisplayStudentReport()
            End If
        Else
            StudentReportsCbx.Visible = False
        End If

    End Sub


    Private Sub StudentServicesRbn_CheckedChanged(sender As Object, e As EventArgs) _
            Handles StudentServicesRbtn.CheckedChanged

        inactivityMsec = 0

        If StudentServicesRbtn.Checked Then

            PopulateStudentsMbx()

            If StudentsMbx.SelectedItem IsNot Nothing Then
                If CType(StudentsMbx.SelectedItem, Student).studentServicesDocumentSeq > 0 Then
                    NoStudentServicesReportLbl.Visible = False
                    DisplayStudentServicesReport()
                Else
                    NoStudentServicesReportLbl.Visible = True
                End If
            End If
        Else
            NoStudentServicesReportLbl.Visible = False
        End If

    End Sub


    Private Sub ExportPdfButton_Click(sender As System.Object, e As System.EventArgs) Handles ExportPdfButton.Click

        'Export current report as a pdf
        'change to excel or word if required

        Try

            SaveFileDialog1.Filter = "PDF (*.PDF)|*.PDF"
            SaveFileDialog1.RestoreDirectory = True
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim Path As String = SaveFileDialog1.FileName
                'Export current report as a pdf
                'change to excel or word if required
                Dim warnings As Warning() = Nothing
                Dim streamids As String() = Nothing
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim extension As String = Nothing
                Dim bytes As Byte()

                ' set to landscape/portrait as appropriate
                Dim deviceInf As String
                If (blnPortraitPDF = False) Then
                    deviceInf = "<DeviceInfo><PageHeight>210mm</PageHeight><PageWidth>297mm</PageWidth></DeviceInfo>"
                Else
                    deviceInf = "<DeviceInfo><PageHeight>297mm</PageHeight><PageWidth>210mm</PageWidth></DeviceInfo>"
                End If

                bytes = ReportViewer.ServerReport.Render("PDF",
                  deviceInf, mimeType,
                    encoding, extension, streamids, warnings)

                Dim fs As New FileStream(Path, FileMode.Create)
                fs.Write(bytes, 0, bytes.Length)
                fs.Close()

                Beep()
                MsgBox("File Successfully Exported.")

            End If

        Catch Ex As Exception
            HandleError(Ex)
        End Try


    End Sub


    Private Sub ReportViewer1_RenderingBegin(
            sender As Object, e As System.ComponentModel.CancelEventArgs) _
            Handles ReportViewer.RenderingBegin
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        renderingTimeElapsedMsec = 0
        RenderingTmr.Start()
    End Sub


    Private Sub RenderingTmr_Tick(sender As Object, e As EventArgs) Handles RenderingTmr.Tick
        renderingTimeElapsedMsec += RenderingTmr.Interval
        If renderingTimeElapsedMsec >= renderingMessageTimeoutMsec Then
            ReportViewerLoadingLbl.Visible = True
        End If
    End Sub


    Private Sub ReportViewer1_RenderingComplete(
            sender As Object, e As Microsoft.Reporting.WinForms.RenderingCompleteEventArgs) _
            Handles ReportViewer.RenderingComplete
        Me.Cursor = System.Windows.Forms.Cursors.Default
        ReportViewerLoadingLbl.Visible = False
        RenderingTmr.Stop()
    End Sub


    Private Sub UserActivityTmr_Tick(sender As Object, e As System.EventArgs) Handles UserActivityTmr.Tick
        inactivityMsec += UserActivityTmr.Interval
        If inactivityMsec >= userActivityTimeoutMsec Then
            Application.Exit()
        End If
    End Sub


    Private Sub StudentReportCbx_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles StudentReportsCbx.SelectedIndexChanged
        DisplayStudentReport()
    End Sub


    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ScaleViewport()
    End Sub

End Class
