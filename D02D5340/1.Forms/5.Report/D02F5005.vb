'#-------------------------------------------------------------------------------------
'# Created Date: 02/01/2009 12:04:36 PM
'# Created User: Thiên Huỳnh
'# Modify Date: 02/01/2009 12:04:36 PM
'# Modify User: Thiên Huỳnh
'#-------------------------------------------------------------------------------------
Public Class D02F5005
	Dim report As D99C2003

    Dim dtSelection As DataTable
    Dim dtSelectionCaption As DataTable
    Dim dtPeriod As DataTable

#Region "Form Load"

    Private Sub Loadlanguage()
        '================================================================ 
        Me.Text = rl3("Bao_cao_chi_phi_XDCB_do_dangF") & " - D02F5005" & UnicodeCaption(gbUnicode) 'BÀo cÀo chi phÛ XDCB dê dang - D02F5005
        '================================================================ 
        lblDivisionID.Text = rl3("Don_vi") 'Đơn vị
        lblReportID.Text = rl3("Ma__bao_cao") 'Mã  báo cáo
        lblReEportName2.Text = rl3("Ten_bao_cao") 'Tên báo cáo
        lblNotes.Text = rl3("Ghi_chu") 'Ghi chú

        lblSelection01IDFrom.Text = rl3("Tieu_thuc_V") & " 1" 'Tieâu thöùc 1
        lblSelection02IDFrom.Text = rl3("Tieu_thuc_V") & " 2" 'Tieâu thöùc 2"
        lblSelection03IDFrom.Text = rl3("Tieu_thuc_V") & " 3" 'Tieâu thöùc 3"
        lblSelection04IDFrom.Text = rl3("Tieu_thuc_V") & " 4" 'Tieâu thöùc 4"
        lblSelection05IDFrom.Text = rl3("Tieu_thuc_V") & " 5" 'Tieâu thöùc 5"
        '================================================================
        optPeriod.Text = rl3("Ky") 'Kỳ
        optDate.Text = rl3("Ngay") 'NGÀY
        '================================================================ 
        btnPrint.Text = rl3("_In") '&In
        btnClose.Text = rl3("Do_ng") 'Đó&ng
        '================================================================ 
        chkExceptSetup.Text = rl3("Khong_hien_thi_nhung_ma_XDCB_da_hinh_thanh") 'Không hiển thị những mã XDCB đã hình thành
        chkExecptVoucherSetup.Text = rl3("Khong_hien_thi_cac_chung_tu_hinh_thanh_tai_san") 'Không hiển thị các chứng từ hình thành tài sản
        chkNotCompleteSplit.Text = rl3("Khong_hien_thi_nhung_ma_XDCB_da_tach_het") 'Không hiển thị những mã XDCB đã tách hết
        chkIsVoucherNotCIP.Text = rl3("Hien_thi_nhung_phieu_chua_nhap_ma_XDCB") 'Hiển thị những phiếu chưa nhập mã XDCB
        '================================================================ 
        chkCheckTime.Text = rL3("Thoi_gian") 'Thời gian
        '================================================================ 
        lblDivision.Text = "1. " & rl3("Don_vi") 'Đơn vị
        lblReport.Text = "2. " & rl3("Bao_cao") 'Báo cáo
        lblGroup.Text = "3. " & rl3("Tieu_thuc_loc") 'Tiêu thức lọc 'Update 10/12/2012 theo incident 53040 cua Bảo Trân bởi Văn Vinh
        lblTimeInfo.Text = "4. " & rl3("Thong_tin_thoi_gian") 'Thông tin thời gian
        '================================================================ 
        tdbcDivisionID.Columns("DivisionID").Caption = rl3("Ma") 'Mã 
        tdbcDivisionID.Columns("DivisionName").Caption = rl3("Ten") 'Tên
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
        tdbcSelection01IDFrom.Columns("Description").Caption = rL3("Dien_giai") 'Diễn giải


        '================================================================ ID : 245805 Bổ sung check không hiển thị XDCB đã quyết toán trên báo cáo chi phí XDCB dở dang
        chkNotCIPFinalization.Text = rL3("Khong_hien_thi_nhung_ma_XDCB_da_quyet_toan_het") 'Không hiển thị những mã XDCB đã quyết toán hết

    End Sub

    Private Sub SetBackColorObligatory()
        tdbcDivisionID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcReportID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        txtReportName2.BackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodFrom.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodTo.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub

    Private Sub D02F5005_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            UseEnterAsTab(Me)
            Exit Sub
        End If
    End Sub

    Private Sub D02F5005_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	LoadInfoGeneral()
        Me.Cursor = Cursors.WaitCursor
        dtPeriod = LoadTablePeriodReport("D02")
        InputbyUnicode(Me, gbUnicode)
        Loadlanguage()
        SetBackColorObligatory()
        LoadTDBCombo()
        LoadDefault()
        SetResolutionForm(Me)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadDefault()
        tdbcDivisionID.SelectedValue = gsDivisionID
        If chkCheckTime.Checked = False Then
            tdbcPeriodFrom.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
            tdbcPeriodTo.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
        End If
        

        LoadtdbcSelection(tdbcReportID.Columns("Selection01").Text, tdbcSelection01IDFrom, tdbcSelection01IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection02").Text, tdbcSelection02IDFrom, tdbcSelection02IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection03").Text, tdbcSelection03IDFrom, tdbcSelection03IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection04").Text, tdbcSelection04IDFrom, tdbcSelection04IDTo)
        LoadtdbcSelection(tdbcReportID.Columns("Selection05").Text, tdbcSelection05IDFrom, tdbcSelection05IDTo)

        'Load caption selection
        LoadCaptionSelection()
        'ReadOnlyControl(c1dateDateFrom)
        'ReadOnlyControl(c1dateDateTo)
    End Sub

#End Region

#Region "Load Combo"

    Private Sub LoadTDBCombo()
        Dim sSQL As String = ""
        ''Load dtSelection,dtSelectionCaption
        dtSelection = CreatedtSelection()
        dtSelectionCaption = CreatedtSelectionCaption()

        LoadCboDivisionIDReport(tdbcDivisionID, D02, , gbUnicode)

        'Load tdbcReportID
        sSQL = "Select ReportCode, ReportName1" & UnicodeJoin(gbUnicode) & " as ReportName1, ReportName2" & UnicodeJoin(gbUnicode) & " as ReportName2, ReportID, Selection01,  Selection02, Selection03, Selection04, Selection05,BracketNegative, DecimalPlaces, AmountFormat, Customized From D02T3030  WITH (NOLOCK)  Where Disabled = 0 Order By ReportCode, ReportName1"
        LoadDataSource(tdbcReportID, sSQL, gbUnicode)
    End Sub

    Private Function CreatedtSelection() As DataTable
        Dim sSQL As String = ""

        sSQL = " Select 1 as DisplayOrder,Code, Description" & UnicodeJoin(gbUnicode) & " as Description, Type From D02V3037 " _
        & "Union All   Select 0 as DisplayOrder,'%' as Code , " & AllName & " As Description, '%' As Type " _
        & "Order by DisplayOrder,Code"
        Return ReturnDataTable(sSQL)
    End Function

    Private Function CreatedtSelectionCaption() As DataTable
        Dim sSQL As String = ""
        sSQL = "Select  Code, Description" & UnicodeJoin(gbUnicode) & " as Description ,Language From D02V3036 Where Language = " & SQLString(gsLanguage)
        Return ReturnDataTable(sSQL)
    End Function

    Private Sub LoadtdbcSelection(ByVal ID As String, ByVal tdbcSelectionIDFrom As C1.Win.C1List.C1Combo, ByVal tdbcSelectionIDTo As C1.Win.C1List.C1Combo)
        Dim dt As DataTable

        dt = ReturnTableFilter(dtSelection, " Type = " & SQLString(ID) & " Or Type = '%'")
        LoadDataSource(tdbcSelectionIDFrom, dt, gbUnicode)
        LoadDataSource(tdbcSelectionIDTo, dt.DefaultView.ToTable, gbUnicode)

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

    Private Sub LoadtdbcPeriod(ByVal ID As String)
        LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, dtPeriod, ID)
        chkCheckTime.Checked = True
    End Sub

#End Region

#Region "Events tdbcDivisionID with txtDivisionName"

    Private Sub tdbcDivisionID_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcDivisionID.Close
        If tdbcDivisionID.FindStringExact(tdbcDivisionID.Text) = -1 Then
            tdbcDivisionID.Text = ""
            txtDivisionName.Text = ""
        End If
    End Sub

    Private Sub tdbcDivisionID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcDivisionID.SelectedValueChanged
        If Not (tdbcDivisionID.Tag Is Nothing OrElse tdbcDivisionID.Tag.ToString = "") Then
            tdbcDivisionID.Tag = ""
            Exit Sub
        End If
        If tdbcDivisionID.SelectedValue Is Nothing Then
            LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, dtPeriod, "-1")
            Exit Sub
        End If
        txtDivisionName.Text = tdbcDivisionID.Columns(1).Text
        LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, dtPeriod, tdbcDivisionID.Text)
        chkCheckTime.Checked = True
    End Sub

    Private Sub tdbcDivisionID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcDivisionID.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
            tdbcDivisionID.Text = ""
            txtDivisionName.Text = ""
        End If
    End Sub

#End Region

#Region "Events tdbcReportID with txtReportName"

    Private Sub tdbcReportID_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcReportID.Close
        If tdbcReportID.FindStringExact(tdbcReportID.Text) = -1 Then
            tdbcReportID.Text = ""
            txtReportName.Text = ""
            txtReportName2.Text = ""
        End If
        txtReportName2.Focus()
    End Sub

    Private Sub tdbcReportID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcReportID.SelectedValueChanged
        txtReportName.Text = tdbcReportID.Columns("ReportName1").Value.ToString
        txtReportName2.Text = tdbcReportID.Columns("ReportName2").Value.ToString
    End Sub

    Private Sub tdbcReportID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcReportID.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
            tdbcReportID.Text = ""
            txtReportName.Text = ""
            txtReportName2.Text = ""
        End If
    End Sub

    Private Sub tdbcReportID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcReportID.LostFocus
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

    Private Sub LoadCaptionSelection()

        If tdbcReportID.Text.Trim <> "" Then
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection01").Text)).Rows.Count > 0 Then
                lblSelection01IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection01").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection01IDFrom.Text = rl3("Tieu_thuc_V") & " 1"
                If gbUnicode And geLanguage = EnumLanguage.Vietnamese Then lblSelection01IDFrom.Text = rl3("Tieu_thuc") & " 1"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection02").Text)).Rows.Count > 0 Then
                lblSelection02IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection02").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection02IDFrom.Text = rl3("Tieu_thuc_V") & " 2"
                If gbUnicode And geLanguage = EnumLanguage.Vietnamese Then lblSelection02IDFrom.Text = rl3("Tieu_thuc") & " 2"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection03").Text)).Rows.Count > 0 Then
                lblSelection03IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection03").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection03IDFrom.Text = rl3("Tieu_thuc_V") & " 3"
                If gbUnicode And geLanguage = EnumLanguage.Vietnamese Then lblSelection03IDFrom.Text = rl3("Tieu_thuc") & " 3"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection04").Text)).Rows.Count > 0 Then
                lblSelection04IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection04").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection04IDFrom.Text = rl3("Tieu_thuc_V") & " 4"
                If gbUnicode And geLanguage = EnumLanguage.Vietnamese Then lblSelection04IDFrom.Text = rl3("Tieu_thuc") & " 4"
            End If
            If ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection05").Text)).Rows.Count > 0 Then
                lblSelection05IDFrom.Text = ReturnTableFilter(dtSelectionCaption, " Code = " & SQLString(tdbcReportID.Columns("Selection05").Text)).Rows(0).Item("Description").ToString
            Else
                lblSelection05IDFrom.Text = rl3("Tieu_thuc_V") & " 5"
                If gbUnicode And geLanguage = EnumLanguage.Vietnamese Then lblSelection05IDFrom.Text = rl3("Tieu_thuc") & " 5"
            End If
            lblSelection01IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection02IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection03IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection04IDFrom.Font = FontUnicode(gbUnicode)
            lblSelection05IDFrom.Font = FontUnicode(gbUnicode)
        End If
    End Sub
#End Region

#Region "Events tdbcSelection01IDFrom"

    Private Sub tdbcSelection01IDFrom_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection01IDFrom.Close
        If tdbcSelection01IDFrom.FindStringExact(tdbcSelection01IDFrom.Text) = -1 Then tdbcSelection01IDFrom.Text = ""
    End Sub

    Private Sub tdbcSelection01IDFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection01IDFrom.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection01IDFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection01IDTo"

    Private Sub tdbcSelection01IDTo_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection01IDTo.Close
        If tdbcSelection01IDTo.FindStringExact(tdbcSelection01IDTo.Text) = -1 Then tdbcSelection01IDTo.Text = ""
    End Sub

    Private Sub tdbcSelection01IDTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection01IDTo.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection01IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection02IDFrom"

    Private Sub tdbcSelection02IDFrom_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection02IDFrom.Close
        If tdbcSelection02IDFrom.FindStringExact(tdbcSelection02IDFrom.Text) = -1 Then tdbcSelection02IDFrom.Text = ""
    End Sub

    Private Sub tdbcSelection02IDFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection02IDFrom.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection02IDFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection02IDTo"

    Private Sub tdbcSelection02IDTo_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection02IDTo.Close
        If tdbcSelection02IDTo.FindStringExact(tdbcSelection02IDTo.Text) = -1 Then tdbcSelection02IDTo.Text = ""
    End Sub

    Private Sub tdbcSelection02IDTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection02IDTo.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection02IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection03IDFrom"

    Private Sub tdbcSelection03IDFrom_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection03IDFrom.Close
        If tdbcSelection03IDFrom.FindStringExact(tdbcSelection03IDFrom.Text) = -1 Then tdbcSelection03IDFrom.Text = ""
    End Sub

    Private Sub tdbcSelection03IDFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection03IDFrom.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection03IDFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection03IDTo"

    Private Sub tdbcSelection03IDTo_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection03IDTo.Close
        If tdbcSelection03IDTo.FindStringExact(tdbcSelection03IDTo.Text) = -1 Then tdbcSelection03IDTo.Text = ""
    End Sub

    Private Sub tdbcSelection03IDTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection03IDTo.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection03IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection04IDFrom"

    Private Sub tdbcSelection04IDFrom_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection04IDFrom.Close
        If tdbcSelection04IDFrom.FindStringExact(tdbcSelection04IDFrom.Text) = -1 Then tdbcSelection04IDFrom.Text = ""
    End Sub

    Private Sub tdbcSelection04IDFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection04IDFrom.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection04IDFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection04IDTo"

    Private Sub tdbcSelection04IDTo_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection04IDTo.Close
        If tdbcSelection04IDTo.FindStringExact(tdbcSelection04IDTo.Text) = -1 Then tdbcSelection04IDTo.Text = ""
    End Sub

    Private Sub tdbcSelection04IDTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection04IDTo.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection04IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection05IDFrom"

    Private Sub tdbcSelection05IDFrom_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection05IDFrom.Close
        If tdbcSelection05IDFrom.FindStringExact(tdbcSelection05IDFrom.Text) = -1 Then tdbcSelection05IDFrom.Text = ""
    End Sub

    Private Sub tdbcSelection05IDFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection05IDFrom.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection05IDFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcSelection05IDTo"

    Private Sub tdbcSelection05IDTo_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcSelection05IDTo.Close
        If tdbcSelection05IDTo.FindStringExact(tdbcSelection05IDTo.Text) = -1 Then tdbcSelection05IDTo.Text = ""
    End Sub

    Private Sub tdbcSelection05IDTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcSelection05IDTo.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcSelection05IDTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcPeriodFrom"

    Private Sub tdbcPeriodFrom_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcPeriodFrom.Close
        If tdbcPeriodFrom.FindStringExact(tdbcPeriodFrom.Text) = -1 Then tdbcPeriodFrom.Text = ""
    End Sub

    Private Sub tdbcPeriodFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcPeriodFrom.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcPeriodFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcPeriodTo"

    Private Sub tdbcPeriodTo_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcPeriodTo.Close
        If tdbcPeriodTo.FindStringExact(tdbcPeriodTo.Text) = -1 Then tdbcPeriodTo.Text = ""
    End Sub

    Private Sub tdbcPeriodTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbcPeriodTo.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then tdbcPeriodTo.Text = ""
    End Sub

#End Region

#Region "Button Click"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not AllowPrint() Then Exit Sub
        btnPrint.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        'Dim report As New D99C1003
        'Đưa vể đầu tiên hàm In trước khi gọi AllowPrint()
		If Not AllowNewD99C2003(report, Me) Then Exit Sub
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
        sReportCaption = rl3("Bao_cao_chi_phi_XDCB_do_dangF") & " - " & sReportName

        If tdbcReportID.Columns("Customized").Value.ToString = "1" Then
            sPathReport = gsApplicationSetup & "\XCustom\" & sReportName & ".rpt"
        Else
            sPathReport = UnicodeGetReportPath(gbUnicode, D02Options.ReportLanguage, "") & sReportName & ".rpt"
            'sPathReport = Application.StartupPath & "\XReports\"
            'If D02Options.ReportLanguage = 0 Then
            '    sPathReport = sPathReport & sReportName & ".rpt"
            'ElseIf D02Options.ReportLanguage = 1 Then
            '    sPathReport = sPathReport & "VE-XReports\" & sReportName & ".rpt"
            'ElseIf D02Options.ReportLanguage = 2 Then
            '    sPathReport = sPathReport & "E-XReports\" & sReportName & ".rpt"
            'End If
        End If
        sSQL = SQLStoreD02P3030()

        If tdbcPeriodFrom.Text <> tdbcPeriodTo.Text Then
            sTime = rl3("Ky_V") & " " & tdbcPeriodFrom.Text & " - " & tdbcPeriodTo.Text
        Else
            sTime = rl3("Ky_V") & " " & tdbcPeriodFrom.Text
        End If

        sSQLSub = "Select * From D91V0016 Where DivisionID = " & SQLString(tdbcDivisionID.Text)
        UnicodeSubReport(sSubReportName, sSQLSub, tdbcDivisionID.Text, gbUnicode)
        With report
            .OpenConnection(conn)
            .AddParameter("Time", IIf(gbUnicode = False, ConvertUnicodeToVni(sTime), sTime), ReportDataType.lmReportString)
            .AddParameter("Titles", IIf(gbUnicode = False, ConvertUnicodeToVni(txtReportName2.Text), txtReportName2.Text), ReportDataType.lmReportString) ' TxtReportName2
            .AddParameter("Notes", IIf(gbUnicode = False, ConvertUnicodeToVni(txtNotes.Text), txtNotes.Text), ReportDataType.lmReportString)
            Dim dtParameter As DataTable
            dtParameter = ReturnDataTable("Select ThousandSeparator, DecimalSeparator, D90_ConvertedDecimals From D91T0025  WITH (NOLOCK) ")

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
        If chkCheckTime.Checked = False Then
            If optPeriod.Checked Then
                If tdbcPeriodTo.Text.Trim = "" Then
                    D99C0008.MsgNotYetChoose(rL3("Den_ky"))
                    tdbcPeriodTo.Focus()
                    Return False
                End If
            Else
                If c1dateDateTo.Value.ToString = "" Then
                    D99C0008.MsgNotYetEnter(rL3("Ngay_den"))
                    c1dateDateTo.Focus()
                    Return False
                End If
            End If
        Else
            If optPeriod.Checked Then
                If tdbcPeriodFrom.Text.Trim = "" Then
                    D99C0008.MsgNotYetChoose(rL3("Tu_ky"))
                    tdbcPeriodFrom.Focus()
                    Return False
                End If
                If tdbcPeriodTo.Text.Trim = "" Then
                    D99C0008.MsgNotYetChoose(rL3("Den_ky"))
                    tdbcPeriodTo.Focus()
                    Return False
                End If
                If Number(tdbcPeriodFrom.Columns("TranYear").Value.ToString) > Number(tdbcPeriodTo.Columns("TranYear").Value.ToString) Then
                    D99C0008.MsgL3(rL3("Tu_ky") & " > " & rL3("Den_ky"))
                    tdbcPeriodFrom.Focus()
                    Return False
                ElseIf Number(tdbcPeriodFrom.Columns("TranYear").Value.ToString) = Number(tdbcPeriodTo.Columns("TranYear").Value.ToString) Then
                    If Number(tdbcPeriodFrom.Columns("TranMonth").Value.ToString) > Number(tdbcPeriodTo.Columns("TranMonth").Value.ToString) Then
                        D99C0008.MsgL3(rL3("Tu_ky") & " > " & rL3("Den_ky"))
                        tdbcPeriodFrom.Focus()
                        Return False
                    End If
                End If
            Else
                If c1dateDateFrom.Value.ToString = "" Then
                    D99C0008.MsgNotYetEnter(rL3("Ngay_tu"))
                    c1dateDateFrom.Focus()
                    Return False
                End If
                If c1dateDateTo.Value.ToString = "" Then
                    D99C0008.MsgNotYetEnter(rL3("Ngay_den"))
                    c1dateDateTo.Focus()
                    Return False
                End If
                If DateDiff(DateInterval.Day, CType(c1dateDateFrom.Value, Date), CType(c1dateDateTo.Value, Date)) < 0 Then
                    D99C0008.Msg(rL3("MSG000013"))
                    c1dateDateFrom.Focus()
                    Return False
                End If
            End If
        End If
       
        
        
        Return True
    End Function



    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P3030
    '# Created User: Thiên Huỳnh
    '# Created Date: 02/01/2009 03:57:36
    '# Modified User: 
    '# Modified Date: 
    '# Description: 
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P3030() As String
        Dim sSQL As String = ""
        sSQL &= "Exec D02P3030 "
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
        sSQL &= SQLNumber(IIf(chkExceptSetup.Checked, 1, 0)) & COMMA 'ExceptSetup, int, NOT NULL
        sSQL &= SQLNumber(IIf(chkExecptVoucherSetup.Checked, 1, 0)) & COMMA 'ExceptVoucherSetup, int, NOT NULL
        sSQL &= SQLString(tdbcDivisionID.Text) & COMMA  'DivisionID, varchar[20], NOT NULL
        If optPeriod.Checked Then
            sSQL &= SQLNumber("0") & COMMA 'Mode, tinyint, NOT NULL
        Else
            sSQL &= SQLNumber("1") & COMMA 'Mode, tinyint, NOT NULL
        End If
        sSQL &= SQLDateSave(c1dateDateFrom.Value) & COMMA 'DateFrom, datetime, NOT NULL
        sSQL &= SQLDateSave(c1dateDateTo.Value) & COMMA 'DateTo, datetime, NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
        sSQL &= SQLNumber(chkNotCompleteSplit.Checked) 'NotCompleteSplit, tinyint, NOT NULL
        sSQL &= COMMA & SQLNumber(gbUnicode) & COMMA
        'Them ngay 5/12/2012 theo incident 52570 của Thị Hiệp bởi Văn Vinh
        sSQL &= SQLNumber(chkIsVoucherNotCIP.Checked) & COMMA 'IsVoucherNotCIP, tinyint, NOT NULL
        'ID : 245805 Bổ sung check không hiển thị XDCB đã quyết toán trên báo cáo chi phí XDCB dở dang
        sSQL &= SQLNumber(chkNotCIPFinalization.Checked) 'NotCIPFinalization, tinyint, NOT NULL

        Return sSQL
    End Function
#End Region
   
    Private Sub optPeriod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPeriod.Click
        VisibleControl()

    End Sub

    Private Sub optDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDate.Click
        VisibleControl()
    End Sub

    Private Sub VisibleControl()
        If chkCheckTime.Checked Then
            SplitContainer1.Panel1Collapsed = False
            optPeriod.Text = rL3("Ky")
            optDate.Text = rL3("Ngay")
            If optPeriod.Checked Then
                c1dateDateFrom.Value = ""
                c1dateDateTo.Value = ""
                tdbcPeriodFrom.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
                tdbcPeriodTo.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
                UnReadOnlyControl(tdbcPeriodFrom, True)
                UnReadOnlyControl(tdbcPeriodTo, True)
                ReadOnlyControl(c1dateDateFrom)
                ReadOnlyControl(c1dateDateTo)
            Else
                tdbcPeriodFrom.SelectedValue = ""
                tdbcPeriodTo.SelectedValue = ""
                c1dateDateFrom.Value = Now
                c1dateDateTo.Value = Now
                ReadOnlyControl(tdbcPeriodFrom)
                ReadOnlyControl(tdbcPeriodTo)
                UnReadOnlyControl(c1dateDateFrom, True)
                UnReadOnlyControl(c1dateDateTo, True)
            End If
        Else
            SplitContainer1.Panel1Collapsed = True
            optPeriod.Text = rL3("Den_ky")
            optDate.Text = rL3("Den_ngay")
            tdbcPeriodFrom.SelectedIndex = CType(tdbcPeriodFrom.DataSource, DataTable).Rows.Count - 1
            c1dateDateFrom.Value = "01/" & tdbcPeriodFrom.SelectedValue.ToString

            If optPeriod.Checked Then
                c1dateDateFrom.Value = ""
                c1dateDateTo.Value = ""
                tdbcPeriodTo.SelectedValue = giTranMonth.ToString("00") & "/" & giTranYear
                UnReadOnlyControl(tdbcPeriodFrom, True)
                UnReadOnlyControl(tdbcPeriodTo, True)
                ReadOnlyControl(c1dateDateFrom)
                ReadOnlyControl(c1dateDateTo)
            Else
                tdbcPeriodFrom.SelectedValue = ""
                tdbcPeriodTo.SelectedValue = ""
                c1dateDateTo.Value = Now
                ReadOnlyControl(tdbcPeriodFrom)
                ReadOnlyControl(tdbcPeriodTo)
                UnReadOnlyControl(c1dateDateFrom, True)
                UnReadOnlyControl(c1dateDateTo, True)
            End If
        End If
        
    End Sub

    Private Sub chkCheckTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckTime.CheckedChanged
        VisibleControl()
    End Sub
End Class