'#-------------------------------------------------------------------------------------
'# Created Date: 05/02/2009 12:04:36 PM
'# Created User: Thiên Huỳnh
'# Modify Date: 05/02/2009 12:04:36 PM
'# Modify User: Thiên Huỳnh
'#-------------------------------------------------------------------------------------
Public Class D02F6005
	Dim report As D99C2003

    Dim dtSelection As DataTable
    Dim dtSelectionCaption As DataTable

#Region "Form Load"

    Private Sub Loadlanguage()
        '================================================================ 
        Me.Text = rl3("Bao_cao_nghiep_vu_tac_dongF") & " - D02F6005" & UnicodeCaption(gbUnicode) 'BÀo cÀo nghiÖp vó tÀc ¢èng - D02F6005
        '================================================================ 
        lblDivisionID.Text = rl3("Don_vi") 'Đơn vị
        lblReportID.Text = rl3("Ma__bao_cao") 'Mã  báo cáo
        lblReEportName2.Text = rl3("Ten_bao_cao") 'Tên báo cáo
        lblNotes.Text = rl3("Ghi_chu") 'Ghi chú
        lblSelection01IDFrom.Text = rl3("Tieu_thuc_1V") 'Tieâu thöùc 1
        lblSelection02IDFrom.Text = rl3("Tieu_thuc_2V") 'Tieâu thöùc 2
        lblSelection03IDFrom.Text = rl3("Tieu_thuc_3V") 'Tieâu thöùc 3
        lblSelection04IDFrom.Text = rl3("Tieu_thuc_4V") 'Tieâu thöùc 4
        lblSelection05IDFrom.Text = rl3("Tieu_thuc_5V") 'Tieâu thöùc 5
        lblPeriodFrom.Text = rl3("Ky") 'Kỳ
        '================================================================ 
        btnPrint.Text = rl3("_In") '&In
        btnClose.Text = rl3("Do_ng") 'Đó&ng
        btnFilter.Text = rl3("Loc") '&Lọc
        chkNoShowLiquidatedAseets.Text = rl3("Khong_hien_thi_cac_tai_san_da_thanh_ly")
        '================================================================ 
        grp1.Text = "1. " & rl3("Don_vi") 'Đơn vị
        grp2.Text = "2. " & rl3("Ma__bao_cao") 'Mã  báo cáo
        grpGroup.Text = "3. " & rl3("Chon_tieu_thuc") 'Chọn tiêu thức
        grpTimeInfo.Text = "4. " & rl3("Thong_tin_thoi_gian") 'Thông tin thời gian
        '================================================================ 
        tdbcReportID.Columns("ReportCode").Caption = rl3("Ma") 'Mã 
        tdbcReportID.Columns("ReportName1").Caption = rl3("Ten") 'Tên
        tdbcSelection04IDTo.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection04IDTo.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection05IDTo.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection05IDTo.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection03IDTo.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection03IDTo.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection02IDTo.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection02IDTo.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection01IDTo.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection01IDTo.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection05IDFrom.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection05IDFrom.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection04IDFrom.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection04IDFrom.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection03IDFrom.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection03IDFrom.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection02IDFrom.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection02IDFrom.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
        tdbcSelection01IDFrom.Columns("Code").Caption = rl3("Ma") 'Mã 
        tdbcSelection01IDFrom.Columns("Description").Caption = rl3("Dien_giai") 'Diễn giải
    End Sub

    Private Sub SetBackColorObligatory()
        tdbcDivisionID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcReportID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        txtReportName2.BackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodFrom.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodTo.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub

    Private Sub D02F6005_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            UseEnterAsTab(Me)
            Exit Sub
        End If
    End Sub



    Private Sub D02F6005_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	LoadInfoGeneral()
        Loadlanguage()
        LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, D02)
        InputbyUnicode(Me, gbUnicode)
        SetBackColorObligatory()
        LoadTDBCombo()
        LoadDefault()
        SetResolutionForm(Me)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadDefault()
        tdbcPeriodFrom.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
        tdbcPeriodTo.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
        LoadtdbcSelection(tdbcReportID.Columns("Selection01").Text, tdbcSelection01IDFrom, tdbcSelection01IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection02").Text, tdbcSelection02IDFrom, tdbcSelection02IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection03").Text, tdbcSelection03IDFrom, tdbcSelection03IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection04").Text, tdbcSelection04IDFrom, tdbcSelection04IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection05").Text, tdbcSelection05IDFrom, tdbcSelection05IDTo)
        'Load caption selection
        LoadCaptionSelection()
    End Sub

#End Region

#Region "Load Combo"

    Private Sub LoadTDBCombo()

        Dim sSQL As String = ""
        ''Load dtSelection,dtSelectionCaption
        dtSelection = CreatedtSelection()
        dtSelectionCaption = CreatedtSelectionCaption()

        'Load tdbcReportID
        sSQL = "Select ReportCode, ReportName1" & UnicodeJoin(gbUnicode) & " as ReportName1, ReportName2" & UnicodeJoin(gbUnicode) & " as ReportName2, ReportID, Selection01,  Selection02, Selection03, Selection04, Selection05,BracketNegative, DecimalPlaces, AmountFormat, Customized From D02T3040  WITH (NOLOCK)  Where Disabled = 0 Order By ReportCode, ReportName1"
        LoadDataSource(tdbcReportID, sSQL, gbUnicode)

        'Thêm ngày 09/10/2012 theo incident 51651 của Bảo Trân bởi Văn Vinh
        'Load tdbcDivisionID
        LoadCboDivisionIDReport(tdbcDivisionID, D02, , gbUnicode)
        tdbcDivisionID.Text = gsDivisionID
    End Sub

    Private Function CreatedtSelection() As DataTable
        Dim sSQL As String = ""

        sSQL = " Select 1 as DisplayOrder,Code, Description" & UnicodeJoin(gbUnicode) & " as Description, Type From D02V3047 " _
        & "Union All   Select 0 as DisplayOrder,'%' as Code , " & AllName & " As Description, '%' As Type " _
        & "Order by DisplayOrder,Code"
        Return ReturnDataTable(sSQL)
    End Function

    Private Function CreatedtSelectionCaption() As DataTable
        Dim sSQL As String = ""
        sSQL = "Select  Code, Description" & UnicodeJoin(gbUnicode) & " as Description,Language From D02V3046 Where Language = " & SQLString(gsLanguage)
        Return ReturnDataTable(sSQL)
    End Function

    Private Sub LoadtdbcSelection(ByVal ID As String, ByVal tdbcSelectionIDFrom As C1.Win.C1List.C1Combo, ByVal tdbcSelectionIDTo As C1.Win.C1List.C1Combo)
        Dim dt As DataTable

        dt = ReturnTableFilter(dtSelection, " Type = " & SQLString(ID) & " Or Type = '%'")
        LoadDataSource(tdbcSelectionIDFrom, dt.Copy, gbUnicode)
        LoadDataSource(tdbcSelectionIDTo, dt.Copy, gbUnicode)

        If ID = "" Or ID = "-1" Then
            tdbcSelectionIDFrom.Enabled = False
            tdbcSelectionIDTo.Enabled = False
            tdbcSelectionIDFrom.Text = ""
            tdbcSelectionIDTo.Text = ""
            Exit Sub
        Else
            tdbcSelectionIDFrom.Enabled = True
            tdbcSelectionIDTo.Enabled = True
            tdbcSelectionIDFrom.SelectedIndex = 0
            tdbcSelectionIDTo.SelectedIndex = 0
        End If
    End Sub

#End Region

#Region "Events tdbcReportID with txtReportName"

    Private Sub tdbcReportID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcReportID.SelectedValueChanged
        txtReportName.Text = tdbcReportID.Columns("ReportName1").Value.ToString
        txtReportName2.Text = tdbcReportID.Columns("ReportName2").Value.ToString

        If Not (tdbcReportID.Tag Is Nothing OrElse tdbcReportID.Tag.ToString = "") Then
            tdbcReportID.Tag = ""
            Exit Sub
        End If
        If tdbcReportID.SelectedValue Is Nothing Then
            LoadtdbcSelection("-1", tdbcSelection01IDFrom, tdbcSelection01IDTo)
            LoadtdbcSelection("-1", tdbcSelection02IDFrom, tdbcSelection02IDTo)
            LoadtdbcSelection("-1", tdbcSelection03IDFrom, tdbcSelection03IDTo)
            LoadtdbcSelection("-1", tdbcSelection04IDFrom, tdbcSelection04IDTo)
            LoadtdbcSelection("-1", tdbcSelection05IDFrom, tdbcSelection05IDTo)
            Exit Sub
        End If
        LoadtdbcSelection(tdbcReportID.Columns("Selection01").Text, tdbcSelection01IDFrom, tdbcSelection01IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection02").Text, tdbcSelection02IDFrom, tdbcSelection02IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection03").Text, tdbcSelection03IDFrom, tdbcSelection03IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection04").Text, tdbcSelection04IDFrom, tdbcSelection04IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection05").Text, tdbcSelection05IDFrom, tdbcSelection05IDTo)

        LoadCaptionSelection()
    End Sub

    Private Sub tdbcReportID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcReportID.LostFocus
        If tdbcReportID.FindStringExact(tdbcReportID.Text) = -1 Then
            tdbcReportID.Text = ""
            txtReportName.Text = ""
            txtReportName2.Text = ""
        End If
    End Sub

    Private Sub LoadCaptionSelection()

        If tdbcReportID.Text.Trim <> "" Then
            lblSelection02IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection01IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection03IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection04IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection05IDFrom.Font = FontUnicode(gbUnicode)
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection01").Text)).Rows.Count > 0 Then
                lblSelection01IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection01").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection01IDFrom.Text = IIf(gbUnicode, ConvertVniToUnicode(rl3("Tieu_thuc_V")), rl3("Tieu_thuc_V")).ToString & " 1"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection02").Text)).Rows.Count > 0 Then
                lblSelection02IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection02").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection02IDFrom.Text = IIf(gbUnicode, ConvertVniToUnicode(rl3("Tieu_thuc_V")), rl3("Tieu_thuc_V")).ToString & " 2"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection03").Text)).Rows.Count > 0 Then
                lblSelection03IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection03").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection03IDFrom.Text = IIf(gbUnicode, ConvertVniToUnicode(rl3("Tieu_thuc_V")), rl3("Tieu_thuc_V")).ToString & " 3"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection04").Text)).Rows.Count > 0 Then
                lblSelection04IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection04").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection04IDFrom.Text = IIf(gbUnicode, ConvertVniToUnicode(rl3("Tieu_thuc_V")), rl3("Tieu_thuc_V")).ToString & " 4"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection05").Text)).Rows.Count > 0 Then
                lblSelection05IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection05").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection05IDFrom.Text = IIf(gbUnicode, ConvertVniToUnicode(rl3("Tieu_thuc_V")), rl3("Tieu_thuc_V")).ToString & " 5"
            End If
        End If
    End Sub
#End Region

#Region "Events tdbcSelection01IDFrom"

    Private Sub tdbcSelection01IDFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection01IDFrom.LostFocus
        If tdbcSelection01IDFrom.FindStringExact(tdbcSelection01IDFrom.Text) = -1 Then tdbcSelection01IDFrom.Text = ""
        SetValueTo(tdbcSelection01IDFrom, tdbcSelection01IDTo)
    End Sub

#End Region

#Region "Events tdbcSelection01IDTo"

    Private Sub tdbcSelection01IDTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection01IDTo.LostFocus
        If tdbcSelection01IDTo.FindStringExact(tdbcSelection01IDTo.Text) = -1 Then tdbcSelection01IDTo.Text = ""

    End Sub
#End Region

#Region "Events tdbcSelection02IDFrom"

    Private Sub tdbcSelection02IDFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection02IDFrom.LostFocus
        If tdbcSelection02IDFrom.FindStringExact(tdbcSelection02IDFrom.Text) = -1 Then tdbcSelection02IDFrom.Text = ""
        SetValueTo(tdbcSelection02IDFrom, tdbcSelection02IDTo)
    End Sub
#End Region

#Region "Events tdbcSelection02IDTo"

    Private Sub tdbcSelection02IDTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection02IDTo.LostFocus
        If tdbcSelection02IDTo.FindStringExact(tdbcSelection02IDTo.Text) = -1 Then tdbcSelection02IDTo.Text = ""
    End Sub
#End Region

#Region "Events tdbcSelection03IDFrom"

    Private Sub tdbcSelection03IDFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection03IDFrom.LostFocus
        If tdbcSelection03IDFrom.FindStringExact(tdbcSelection03IDFrom.Text) = -1 Then tdbcSelection03IDFrom.Text = ""
        SetValueTo(tdbcSelection03IDFrom, tdbcSelection03IDTo)
    End Sub
#End Region

#Region "Events tdbcSelection03IDTo"

    Private Sub tdbcSelection03IDTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection03IDTo.LostFocus
        If tdbcSelection03IDTo.FindStringExact(tdbcSelection03IDTo.Text) = -1 Then tdbcSelection03IDTo.Text = ""
    End Sub
#End Region

#Region "Events tdbcSelection04IDFrom"

    Private Sub tdbcSelection04IDFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection04IDFrom.LostFocus
        If tdbcSelection04IDFrom.FindStringExact(tdbcSelection04IDFrom.Text) = -1 Then tdbcSelection04IDFrom.Text = ""
        SetValueTo(tdbcSelection04IDFrom, tdbcSelection04IDTo)
    End Sub

#End Region

#Region "Events tdbcSelection04IDTo"

    Private Sub tdbcSelection04IDTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection04IDTo.LostFocus
        If tdbcSelection04IDTo.FindStringExact(tdbcSelection04IDTo.Text) = -1 Then tdbcSelection04IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection05IDFrom"

    Private Sub tdbcSelection05IDFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection05IDFrom.LostFocus
        If tdbcSelection05IDFrom.FindStringExact(tdbcSelection05IDFrom.Text) = -1 Then tdbcSelection05IDFrom.Text = ""
        SetValueTo(tdbcSelection05IDFrom, tdbcSelection05IDTo)
    End Sub

#End Region

#Region "Events tdbcSelection05IDTo"

    Private Sub tdbcSelection05IDTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcSelection05IDTo.LostFocus
        If tdbcSelection05IDTo.FindStringExact(tdbcSelection05IDTo.Text) = -1 Then tdbcSelection05IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcPeriodFrom"

    Private Sub tdbcPeriodFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcPeriodFrom.LostFocus
        If tdbcPeriodFrom.FindStringExact(tdbcPeriodFrom.Text) = -1 Then tdbcPeriodFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcPeriodTo"

    Private Sub tdbcPeriodTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcPeriodTo.LostFocus
        If tdbcPeriodTo.FindStringExact(tdbcPeriodTo.Text) = -1 Then tdbcPeriodTo.Text = ""
    End Sub

#End Region
    'Thêm ngày 09/10/2012 theo incident 51651 của Bảo Trân bởi Văn Vinh
#Region "Events tdbcDivisionID with txtDivisionName"

    Private Sub tdbcDivisionID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcDivisionID.SelectedValueChanged
        If tdbcDivisionID.SelectedValue Is Nothing Then
            txtDivisionName.Text = ""
        Else
            txtDivisionName.Text = tdbcDivisionID.Columns(1).Value.ToString
        End If
    End Sub

    Private Sub tdbcDivisionID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcDivisionID.LostFocus
        If tdbcDivisionID.FindStringExact(tdbcDivisionID.Text) = -1 Then
            tdbcDivisionID.Text = ""
        End If
    End Sub

#End Region


#Region "Button Click"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'Đưa vể đầu tiên hàm In trước khi gọi AllowPrint()
        If Not AllowNewD99C2003(report, Me) Then Exit Sub
        If Not AllowPrint() Then Exit Sub
        btnPrint.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        'Dim report As New D99C1003
		
		'************************************
        Dim conn As New SqlConnection(gsConnectionString)
        Dim sReportName As String = ""
        Dim sSubReportName As String = ""
        Dim sReportCaption As String = ""
        Dim sPathReport As String = ""
        Dim sSQL As String = ""
        Dim sSQLSub As String = ""
        Dim sTime As String = ""

        sReportName = tdbcReportID.Columns("ReportID").Value.ToString
        sSubReportName = "D02R0000"
        sReportCaption = rl3("Bao_cao_nghiep_vu_tac_dongF") & " - " & sReportName
        Dim sCustom As String = ""
        If tdbcReportID.Columns("Customized").Value.ToString = "1" Then
            sCustom = "1"
            '    sPathReport = Application.StartupPath & "\XCustom\" & sReportName & ".rpt"
            'Else
            '    sPathReport = Application.StartupPath & "\XReports\"
            '    If D02Options.ReportLanguage = 0 Then
            '        sPathReport = sPathReport & sReportName & ".rpt"
            '    ElseIf D02Options.ReportLanguage = 1 Then
            '        sPathReport = sPathReport & "VE-XReports\" & sReportName & ".rpt"
            '    ElseIf D02Options.ReportLanguage = 2 Then
            '        sPathReport = sPathReport & "E-XReports\" & sReportName & ".rpt"
            '    End If
        End If
        sSQLSub = "Select * From D91V0016 Where DivisionID = " & SQLString(ReturnValueC1Combo(tdbcDivisionID))
        sPathReport = UnicodeGetReportPath(gbUnicode, D02Options.ReportLanguage, sCustom) & sReportName & ".rpt"
        UnicodeSubReport(sSubReportName, sSQLSub, ReturnValueC1Combo(tdbcDivisionID).ToString, gbUnicode)
        sSQL = SQLStoreD02P3040()
        '   ExecuteSQL(sSQL)
        '    sSQL = "SELECT * FROM D02V3040 ORDER BY Level01, Level02, Level03, Level04, Level05 "
        If tdbcPeriodFrom.Text <> tdbcPeriodTo.Text Then
            If gbUnicode Then
                sTime = rl3("Ky") & " " & tdbcPeriodFrom.Text & " - " & tdbcPeriodTo.Text
            Else
                sTime = rl3("Ky_V") & " " & tdbcPeriodFrom.Text & " - " & tdbcPeriodTo.Text
            End If

        Else
            If gbUnicode Then
                sTime = rl3("Ky") & " " & tdbcPeriodFrom.Text
            Else
                sTime = rl3("Ky_V") & " " & tdbcPeriodFrom.Text
            End If
        End If
        With report
            .OpenConnection(conn)
            .AddParameter("Time", IIf(gbUnicode = False, ConvertUnicodeToVni(sTime), sTime), ReportDataType.lmReportString)
            .AddParameter("Titles", IIf(gbUnicode = False, ConvertUnicodeToVni(txtReportName2.Text), txtReportName2.Text), ReportDataType.lmReportString) ' TxtReportName2
            .AddParameter("Notes", IIf(gbUnicode = False, ConvertUnicodeToVni(txtNotes.Text), txtNotes.Text), ReportDataType.lmReportString)
            Dim dtParameter As DataTable
            dtParameter = ReturnDataTable("Select ThousandSeparator, DecimalSeparator, D90_ConvertedDecimals From D91T0025 WITH (NOLOCK) ")
            If dtParameter.Rows.Count > 0 Then
                .AddParameter("DecimalSeparator", IIf(IsDBNull(dtParameter.Columns("DecimalSeparator").ToString) Or Trim(dtParameter.Columns("DecimalSeparator").ToString) = "", ",", dtParameter.Columns("DecimalSeparator").ToString), ReportDataType.lmReportString)
                .AddParameter("ThousandSeparator", IIf(IsDBNull(dtParameter.Columns("ThousandSeparator").ToString) Or Trim(dtParameter.Columns("ThousandSeparator").ToString) = "", ".", dtParameter.Columns("ThousandSeparator").ToString), ReportDataType.lmReportString)
                .AddParameter("DecimalConverted", IIf(IsDBNull(dtParameter.Columns("D90_ConvertedDecimals").ToString) Or Trim(dtParameter.Columns("D90_ConvertedDecimals").ToString) = "", 0, dtParameter.Columns("D90_ConvertedDecimals").ToString), ReportDataType.lmReportNumber)
            Else
                .AddParameter("DecimalSeparator", ",", ReportDataType.lmReportString)
                .AddParameter("ThousandSeparator", ".", ReportDataType.lmReportString)
                .AddParameter("DecimalConverted", 0, ReportDataType.lmReportString)
            End If

            .AddParameter("BracketNegative", Val(tdbcReportID.Columns("BracketNegative").Value), ReportDataType.lmReportNumber)
            .AddParameter("DecimalOriginal", Val(tdbcReportID.Columns("DecimalPlaces").Value), ReportDataType.lmReportNumber)
            .AddParameter("AmountFormat", Val(tdbcReportID.Columns("AmountFormat").Value), ReportDataType.lmReportNumber)
            .AddSub(sSQLSub, sSubReportName & ".rpt")
            .AddMain(sSQL)
            .PrintReport(sPathReport, sReportCaption)
        End With
        Me.Cursor = Cursors.Default
        btnPrint.Enabled = True
    End Sub

    Private Function AllowPrint() As Boolean
        If tdbcDivisionID.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(rl3("Don_vi"))
            tdbcDivisionID.Focus()
            Return False
        End If
        If tdbcReportID.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(rl3("Ma__bao_cao"))
            tdbcReportID.Focus()
            Return False
        End If
        If txtReportName2.Text.Trim = "" Then
            D99C0008.MsgNotYetEnter(rl3("Ten_bao_cao"))
            txtReportName2.Focus()
            Return False
        End If
        If tdbcPeriodFrom.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(rl3("Tu_ky"))
            tdbcPeriodFrom.Focus()
            Return False
        End If
        If tdbcPeriodTo.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(rl3("Den_ky"))
            tdbcPeriodTo.Focus()
            Return False
        End If
        If Number(tdbcPeriodFrom.Columns("TranYear").Value.ToString) > Number(tdbcPeriodTo.Columns("TranYear").Value.ToString) Then
            D99C0008.MsgL3(rl3("Tu_ky") & " > " & rl3("Den_ky"))
            tdbcPeriodFrom.Focus()
            Return False
        ElseIf Number(tdbcPeriodFrom.Columns("TranYear").Value.ToString) = Number(tdbcPeriodTo.Columns("TranYear").Value.ToString) Then
            If Number(tdbcPeriodFrom.Columns("TranMonth").Value.ToString) > Number(tdbcPeriodTo.Columns("TranMonth").Value.ToString) Then
                D99C0008.MsgL3(rl3("Tu_ky") & " > " & rl3("Den_ky"))
                tdbcPeriodFrom.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P3040
    '# Created User: Thiên Huỳnh
    '# Created Date: 02/01/2009 03:57:36
    '# Modified User: 
    '# Modified Date: 
    '# Description: 
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P3040() As String
        Dim sSQL As String = ""
        sSQL &= "Exec D02P3040 "
        sSQL &= SQLString(tdbcReportID.Columns("ReportCode").Value.ToString) & COMMA 'ReportCode, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcReportID.Columns("Selection01").Value.ToString) & COMMA 'Sel01Type, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection01IDFrom.Text) & COMMA 'Sel01IDFrom, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection01IDTo.Text) & COMMA 'Sel01IDTo, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcReportID.Columns("Selection02").Value.ToString) & COMMA 'Sel02Type, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection02IDFrom.Text) & COMMA 'Sel02IDFrom, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection02IDTo.Text) & COMMA 'Sel02IDTo, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcReportID.Columns("Selection03").Value.ToString) & COMMA 'Sel03Type, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection03IDFrom.Text) & COMMA 'Sel03IDFrom, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection03IDTo.Text) & COMMA 'Sel03IDTo, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcReportID.Columns("Selection04").Value.ToString) & COMMA 'Sel04Type, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection04IDFrom.Text) & COMMA 'Sel04IDFrom, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection04IDTo.Text) & COMMA 'Sel04IDTo, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcReportID.Columns("Selection05").Value.ToString) & COMMA 'Sel05Type, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection05IDFrom.Text) & COMMA 'Sel05IDFrom, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcSelection05IDTo.Text) & COMMA 'Sel05IDTo, varchar[20], NOT NULL
        sSQL &= SQLNumber(tdbcPeriodFrom.Columns("TranMonth").Value.ToString) & COMMA 'FromMonth, int, NOT NULL
        sSQL &= SQLNumber(tdbcPeriodFrom.Columns("TranYear").Value.ToString) & COMMA 'FromYear, int, NOT NULL
        sSQL &= SQLNumber(tdbcPeriodTo.Columns("TranMonth").Value.ToString) & COMMA 'ToMonth, int, NOT NULL
        sSQL &= SQLNumber(tdbcPeriodTo.Columns("TranYear").Value.ToString) & COMMA 'ToYear, int, NOT NULL
        sSQL &= SQLNumber(chkNoShowLiquidatedAseets.Checked) & COMMA 'NoShowLiquidatedAssets, tinyint, NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar(50), NOT NULL
        'Bổ xung ngày 17/07/2012 theo THIHUAN
        sSQL &= "N" & SQLString(sFind) & COMMA
        'Bổ xung ngày 10/10/2012 theo incident 51651 của Bảo Trân bởi Văn Vinh
        sSQL &= SQLString(ReturnValueC1Combo(tdbcDivisionID))  'DivisionID1 VARCHAR(20), NOT NULL
        Return sSQL
    End Function

#End Region
    'Bổ xung ngày 17/07/2012 theo THIHUAN
    'Thêm button Lọc, Incident 47149
    Private sFind As String = ""
	Public WriteOnly Property strNewFind() As String
		Set(ByVal Value As String)
			sFind = Value
            btnPrint_Click(Nothing, Nothing)
            sFind = ""
		End Set
	End Property

    Private WithEvents Finder As New D99C1001
	Dim gbEnabledUseFind As Boolean = False
    'Cần sửa Tìm kiếm như sau:
	'Bỏ sự kiện Finder_FindClick.
	'Sửa tham số Me.Name -> Me
	'Phải tạo biến properties có tên chính xác strNewFind và strNewServer
	'Sửa gdtCaptionExcel thành dtCaptionCols: biến toàn cục trong form
	'Nếu có F12 dùng D09U1111 thì Sửa dtCaptionCols thành ResetTableByGrid(usrOption, dtCaptionCols.DefaultView.ToTable)
    Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Dim sSQL As String
        gbEnabledUseFind = True
        sSQL = "Select * From D02V1234 " & vbCrLf
        sSQL &= "Where FormID = " & SQLString(Me.Name) & vbCrLf
        sSQL &= " And Language = " & SQLString(gsLanguage) & vbCrLf
        ShowFindDialog(Finder, sSQL, Me, gbUnicode)
       
    End Sub

#Region "Active Find - List All (Client)"

    'Private Sub Finder_FindClick(ByVal ResultWhereClause As Object) Handles Finder.FindClick
    '    If ResultWhereClause Is Nothing Or ResultWhereClause.ToString = "" Then
    '        sFind = ""
    '        Exit Sub
    '    End If

    '    sFind = ResultWhereClause.ToString()
    '    'Dim sDau As String = L3Left(sFind, 3)
    '    'Dim sCuoi As String = L3Right(sFind, sFind.Length - 3)
    '    'sFind = sDau & "T4." & sCuoi
    'End Sub
#End Region

End Class