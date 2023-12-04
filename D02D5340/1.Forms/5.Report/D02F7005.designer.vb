<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class D02F7005
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(D02F7005))
        Me.grpDivision = New System.Windows.Forms.GroupBox()
        Me.tdbcDivisionID = New C1.Win.C1List.C1Combo()
        Me.lblDivisionID = New System.Windows.Forms.Label()
        Me.txtDivisionName = New System.Windows.Forms.TextBox()
        Me.grpReport = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtReportName2 = New System.Windows.Forms.TextBox()
        Me.tdbcReportID = New C1.Win.C1List.C1Combo()
        Me.lblReportID = New System.Windows.Forms.Label()
        Me.txtReportName = New System.Windows.Forms.TextBox()
        Me.lblReEportName2 = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.grpGroup = New System.Windows.Forms.GroupBox()
        Me.tdbcSelection04IDTo = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection05IDTo = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection03IDTo = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection02IDTo = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection01IDTo = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection05IDFrom = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection04IDFrom = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection03IDFrom = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection02IDFrom = New C1.Win.C1List.C1Combo()
        Me.tdbcSelection01IDFrom = New C1.Win.C1List.C1Combo()
        Me.lblSelection01IDFrom = New System.Windows.Forms.Label()
        Me.lblSelection02IDFrom = New System.Windows.Forms.Label()
        Me.lblSelection03IDFrom = New System.Windows.Forms.Label()
        Me.lblSelection04IDFrom = New System.Windows.Forms.Label()
        Me.lblSelection05IDFrom = New System.Windows.Forms.Label()
        Me.lblSelection01IDTo = New System.Windows.Forms.Label()
        Me.lblSelection02IDTo = New System.Windows.Forms.Label()
        Me.lblSelection03IDTo = New System.Windows.Forms.Label()
        Me.lblSelection05IDTo = New System.Windows.Forms.Label()
        Me.lblSelection04IDTo = New System.Windows.Forms.Label()
        Me.grpTimeInfo = New System.Windows.Forms.GroupBox()
        Me.tdbcPeriodTo = New C1.Win.C1List.C1Combo()
        Me.tdbcPeriodFrom = New C1.Win.C1List.C1Combo()
        Me.lblPeriodFrom = New System.Windows.Forms.Label()
        Me.lblPeriodTo = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.grpDivision.SuspendLayout()
        CType(Me.tdbcDivisionID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReport.SuspendLayout()
        CType(Me.tdbcReportID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGroup.SuspendLayout()
        CType(Me.tdbcSelection04IDTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection05IDTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection03IDTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection02IDTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection01IDTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection05IDFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection04IDFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection03IDFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection02IDFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcSelection01IDFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTimeInfo.SuspendLayout()
        CType(Me.tdbcPeriodTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcPeriodFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpDivision
        '
        Me.grpDivision.Controls.Add(Me.tdbcDivisionID)
        Me.grpDivision.Controls.Add(Me.lblDivisionID)
        Me.grpDivision.Controls.Add(Me.txtDivisionName)
        Me.grpDivision.Location = New System.Drawing.Point(3, 2)
        Me.grpDivision.Name = "grpDivision"
        Me.grpDivision.Size = New System.Drawing.Size(590, 54)
        Me.grpDivision.TabIndex = 0
        Me.grpDivision.TabStop = False
        Me.grpDivision.Text = "Đơn vị"
        '
        'tdbcDivisionID
        '
        Me.tdbcDivisionID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcDivisionID.AllowColMove = False
        Me.tdbcDivisionID.AllowSort = False
        Me.tdbcDivisionID.AlternatingRows = True
        Me.tdbcDivisionID.AutoCompletion = True
        Me.tdbcDivisionID.AutoDropDown = True
        Me.tdbcDivisionID.Caption = ""
        Me.tdbcDivisionID.CaptionHeight = 17
        Me.tdbcDivisionID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcDivisionID.ColumnCaptionHeight = 17
        Me.tdbcDivisionID.ColumnFooterHeight = 17
        Me.tdbcDivisionID.ColumnWidth = 100
        Me.tdbcDivisionID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcDivisionID.DisplayMember = "DivisionID"
        Me.tdbcDivisionID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcDivisionID.DropDownWidth = 300
        Me.tdbcDivisionID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcDivisionID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcDivisionID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcDivisionID.EmptyRows = True
        Me.tdbcDivisionID.ExtendRightColumn = True
        Me.tdbcDivisionID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcDivisionID.Images.Add(CType(resources.GetObject("tdbcDivisionID.Images"), System.Drawing.Image))
        Me.tdbcDivisionID.ItemHeight = 15
        Me.tdbcDivisionID.Location = New System.Drawing.Point(121, 20)
        Me.tdbcDivisionID.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcDivisionID.MaxDropDownItems = CType(8, Short)
        Me.tdbcDivisionID.MaxLength = 32767
        Me.tdbcDivisionID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcDivisionID.Name = "tdbcDivisionID"
        Me.tdbcDivisionID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcDivisionID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcDivisionID.Size = New System.Drawing.Size(128, 23)
        Me.tdbcDivisionID.TabIndex = 1
        Me.tdbcDivisionID.ValueMember = "DivisionID"
        Me.tdbcDivisionID.PropBag = resources.GetString("tdbcDivisionID.PropBag")
        '
        'lblDivisionID
        '
        Me.lblDivisionID.AutoSize = True
        Me.lblDivisionID.Location = New System.Drawing.Point(20, 24)
        Me.lblDivisionID.Name = "lblDivisionID"
        Me.lblDivisionID.Size = New System.Drawing.Size(38, 13)
        Me.lblDivisionID.TabIndex = 0
        Me.lblDivisionID.Text = "Đơn vị"
        Me.lblDivisionID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivisionName.Location = New System.Drawing.Point(255, 20)
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.ReadOnly = True
        Me.txtDivisionName.Size = New System.Drawing.Size(326, 22)
        Me.txtDivisionName.TabIndex = 2
        Me.txtDivisionName.TabStop = False
        '
        'grpReport
        '
        Me.grpReport.Controls.Add(Me.txtNotes)
        Me.grpReport.Controls.Add(Me.txtReportName2)
        Me.grpReport.Controls.Add(Me.tdbcReportID)
        Me.grpReport.Controls.Add(Me.lblReportID)
        Me.grpReport.Controls.Add(Me.txtReportName)
        Me.grpReport.Controls.Add(Me.lblReEportName2)
        Me.grpReport.Controls.Add(Me.lblNotes)
        Me.grpReport.Location = New System.Drawing.Point(3, 62)
        Me.grpReport.Name = "grpReport"
        Me.grpReport.Size = New System.Drawing.Size(590, 105)
        Me.grpReport.TabIndex = 1
        Me.grpReport.TabStop = False
        Me.grpReport.Text = "Báo cáo"
        '
        'txtNotes
        '
        Me.txtNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!)
        Me.txtNotes.Location = New System.Drawing.Point(123, 75)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(458, 22)
        Me.txtNotes.TabIndex = 7
        '
        'txtReportName2
        '
        Me.txtReportName2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!)
        Me.txtReportName2.Location = New System.Drawing.Point(123, 47)
        Me.txtReportName2.Name = "txtReportName2"
        Me.txtReportName2.Size = New System.Drawing.Size(458, 22)
        Me.txtReportName2.TabIndex = 5
        '
        'tdbcReportID
        '
        Me.tdbcReportID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcReportID.AllowColMove = False
        Me.tdbcReportID.AllowSort = False
        Me.tdbcReportID.AlternatingRows = True
        Me.tdbcReportID.AutoCompletion = True
        Me.tdbcReportID.AutoDropDown = True
        Me.tdbcReportID.AutoSelect = True
        Me.tdbcReportID.Caption = ""
        Me.tdbcReportID.CaptionHeight = 17
        Me.tdbcReportID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcReportID.ColumnCaptionHeight = 17
        Me.tdbcReportID.ColumnFooterHeight = 17
        Me.tdbcReportID.ColumnWidth = 100
        Me.tdbcReportID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcReportID.DisplayMember = "ReportCode"
        Me.tdbcReportID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcReportID.DropDownWidth = 300
        Me.tdbcReportID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcReportID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcReportID.EmptyRows = True
        Me.tdbcReportID.ExtendRightColumn = True
        Me.tdbcReportID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportID.Images.Add(CType(resources.GetObject("tdbcReportID.Images"), System.Drawing.Image))
        Me.tdbcReportID.ItemHeight = 15
        Me.tdbcReportID.Location = New System.Drawing.Point(123, 19)
        Me.tdbcReportID.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcReportID.MaxDropDownItems = CType(8, Short)
        Me.tdbcReportID.MaxLength = 32767
        Me.tdbcReportID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcReportID.Name = "tdbcReportID"
        Me.tdbcReportID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcReportID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcReportID.Size = New System.Drawing.Size(128, 23)
        Me.tdbcReportID.TabIndex = 1
        Me.tdbcReportID.ValueMember = "ReportCode"
        Me.tdbcReportID.PropBag = resources.GetString("tdbcReportID.PropBag")
        '
        'lblReportID
        '
        Me.lblReportID.AutoSize = True
        Me.lblReportID.Location = New System.Drawing.Point(20, 24)
        Me.lblReportID.Name = "lblReportID"
        Me.lblReportID.Size = New System.Drawing.Size(67, 13)
        Me.lblReportID.TabIndex = 0
        Me.lblReportID.Text = "Mã  báo cáo"
        Me.lblReportID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReportName
        '
        Me.txtReportName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtReportName.Location = New System.Drawing.Point(255, 19)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.ReadOnly = True
        Me.txtReportName.Size = New System.Drawing.Size(326, 22)
        Me.txtReportName.TabIndex = 2
        Me.txtReportName.TabStop = False
        '
        'lblReEportName2
        '
        Me.lblReEportName2.AutoSize = True
        Me.lblReEportName2.Location = New System.Drawing.Point(20, 52)
        Me.lblReEportName2.Name = "lblReEportName2"
        Me.lblReEportName2.Size = New System.Drawing.Size(68, 13)
        Me.lblReEportName2.TabIndex = 4
        Me.lblReEportName2.Text = "Tên báo cáo"
        Me.lblReEportName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Location = New System.Drawing.Point(20, 80)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(44, 13)
        Me.lblNotes.TabIndex = 6
        Me.lblNotes.Text = "Ghi chú"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpGroup
        '
        Me.grpGroup.Controls.Add(Me.tdbcSelection04IDTo)
        Me.grpGroup.Controls.Add(Me.tdbcSelection05IDTo)
        Me.grpGroup.Controls.Add(Me.tdbcSelection03IDTo)
        Me.grpGroup.Controls.Add(Me.tdbcSelection02IDTo)
        Me.grpGroup.Controls.Add(Me.tdbcSelection01IDTo)
        Me.grpGroup.Controls.Add(Me.tdbcSelection05IDFrom)
        Me.grpGroup.Controls.Add(Me.tdbcSelection04IDFrom)
        Me.grpGroup.Controls.Add(Me.tdbcSelection03IDFrom)
        Me.grpGroup.Controls.Add(Me.tdbcSelection02IDFrom)
        Me.grpGroup.Controls.Add(Me.tdbcSelection01IDFrom)
        Me.grpGroup.Controls.Add(Me.lblSelection01IDFrom)
        Me.grpGroup.Controls.Add(Me.lblSelection02IDFrom)
        Me.grpGroup.Controls.Add(Me.lblSelection03IDFrom)
        Me.grpGroup.Controls.Add(Me.lblSelection04IDFrom)
        Me.grpGroup.Controls.Add(Me.lblSelection05IDFrom)
        Me.grpGroup.Controls.Add(Me.lblSelection01IDTo)
        Me.grpGroup.Controls.Add(Me.lblSelection02IDTo)
        Me.grpGroup.Controls.Add(Me.lblSelection03IDTo)
        Me.grpGroup.Controls.Add(Me.lblSelection05IDTo)
        Me.grpGroup.Controls.Add(Me.lblSelection04IDTo)
        Me.grpGroup.Location = New System.Drawing.Point(3, 173)
        Me.grpGroup.Name = "grpGroup"
        Me.grpGroup.Size = New System.Drawing.Size(590, 173)
        Me.grpGroup.TabIndex = 2
        Me.grpGroup.TabStop = False
        Me.grpGroup.Text = "Chọn nhóm"
        '
        'tdbcSelection04IDTo
        '
        Me.tdbcSelection04IDTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection04IDTo.AllowColMove = False
        Me.tdbcSelection04IDTo.AllowSort = False
        Me.tdbcSelection04IDTo.AlternatingRows = True
        Me.tdbcSelection04IDTo.AutoCompletion = True
        Me.tdbcSelection04IDTo.AutoDropDown = True
        Me.tdbcSelection04IDTo.Caption = ""
        Me.tdbcSelection04IDTo.CaptionHeight = 17
        Me.tdbcSelection04IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection04IDTo.ColumnCaptionHeight = 17
        Me.tdbcSelection04IDTo.ColumnFooterHeight = 17
        Me.tdbcSelection04IDTo.ColumnWidth = 100
        Me.tdbcSelection04IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection04IDTo.DisplayMember = "Code"
        Me.tdbcSelection04IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection04IDTo.DropDownWidth = 300
        Me.tdbcSelection04IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection04IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection04IDTo.EmptyRows = True
        Me.tdbcSelection04IDTo.ExtendRightColumn = True
        Me.tdbcSelection04IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDTo.Images.Add(CType(resources.GetObject("tdbcSelection04IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection04IDTo.ItemHeight = 15
        Me.tdbcSelection04IDTo.Location = New System.Drawing.Point(350, 111)
        Me.tdbcSelection04IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection04IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection04IDTo.MaxLength = 32767
        Me.tdbcSelection04IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection04IDTo.Name = "tdbcSelection04IDTo"
        Me.tdbcSelection04IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection04IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection04IDTo.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection04IDTo.TabIndex = 15
        Me.tdbcSelection04IDTo.ValueMember = "Code"
        Me.tdbcSelection04IDTo.PropBag = resources.GetString("tdbcSelection04IDTo.PropBag")
        '
        'tdbcSelection05IDTo
        '
        Me.tdbcSelection05IDTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection05IDTo.AllowColMove = False
        Me.tdbcSelection05IDTo.AllowSort = False
        Me.tdbcSelection05IDTo.AlternatingRows = True
        Me.tdbcSelection05IDTo.AutoCompletion = True
        Me.tdbcSelection05IDTo.AutoDropDown = True
        Me.tdbcSelection05IDTo.Caption = ""
        Me.tdbcSelection05IDTo.CaptionHeight = 17
        Me.tdbcSelection05IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection05IDTo.ColumnCaptionHeight = 17
        Me.tdbcSelection05IDTo.ColumnFooterHeight = 17
        Me.tdbcSelection05IDTo.ColumnWidth = 100
        Me.tdbcSelection05IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection05IDTo.DisplayMember = "Code"
        Me.tdbcSelection05IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection05IDTo.DropDownWidth = 300
        Me.tdbcSelection05IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection05IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection05IDTo.EmptyRows = True
        Me.tdbcSelection05IDTo.ExtendRightColumn = True
        Me.tdbcSelection05IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDTo.Images.Add(CType(resources.GetObject("tdbcSelection05IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection05IDTo.ItemHeight = 15
        Me.tdbcSelection05IDTo.Location = New System.Drawing.Point(350, 140)
        Me.tdbcSelection05IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection05IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection05IDTo.MaxLength = 32767
        Me.tdbcSelection05IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection05IDTo.Name = "tdbcSelection05IDTo"
        Me.tdbcSelection05IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection05IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection05IDTo.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection05IDTo.TabIndex = 19
        Me.tdbcSelection05IDTo.ValueMember = "Code"
        Me.tdbcSelection05IDTo.PropBag = resources.GetString("tdbcSelection05IDTo.PropBag")
        '
        'tdbcSelection03IDTo
        '
        Me.tdbcSelection03IDTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection03IDTo.AllowColMove = False
        Me.tdbcSelection03IDTo.AllowSort = False
        Me.tdbcSelection03IDTo.AlternatingRows = True
        Me.tdbcSelection03IDTo.AutoCompletion = True
        Me.tdbcSelection03IDTo.AutoDropDown = True
        Me.tdbcSelection03IDTo.Caption = ""
        Me.tdbcSelection03IDTo.CaptionHeight = 17
        Me.tdbcSelection03IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection03IDTo.ColumnCaptionHeight = 17
        Me.tdbcSelection03IDTo.ColumnFooterHeight = 17
        Me.tdbcSelection03IDTo.ColumnWidth = 100
        Me.tdbcSelection03IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection03IDTo.DisplayMember = "Code"
        Me.tdbcSelection03IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection03IDTo.DropDownWidth = 300
        Me.tdbcSelection03IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection03IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection03IDTo.EmptyRows = True
        Me.tdbcSelection03IDTo.ExtendRightColumn = True
        Me.tdbcSelection03IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDTo.Images.Add(CType(resources.GetObject("tdbcSelection03IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection03IDTo.ItemHeight = 15
        Me.tdbcSelection03IDTo.Location = New System.Drawing.Point(350, 82)
        Me.tdbcSelection03IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection03IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection03IDTo.MaxLength = 32767
        Me.tdbcSelection03IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection03IDTo.Name = "tdbcSelection03IDTo"
        Me.tdbcSelection03IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection03IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection03IDTo.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection03IDTo.TabIndex = 11
        Me.tdbcSelection03IDTo.ValueMember = "Code"
        Me.tdbcSelection03IDTo.PropBag = resources.GetString("tdbcSelection03IDTo.PropBag")
        '
        'tdbcSelection02IDTo
        '
        Me.tdbcSelection02IDTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection02IDTo.AllowColMove = False
        Me.tdbcSelection02IDTo.AllowSort = False
        Me.tdbcSelection02IDTo.AlternatingRows = True
        Me.tdbcSelection02IDTo.AutoCompletion = True
        Me.tdbcSelection02IDTo.AutoDropDown = True
        Me.tdbcSelection02IDTo.Caption = ""
        Me.tdbcSelection02IDTo.CaptionHeight = 17
        Me.tdbcSelection02IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection02IDTo.ColumnCaptionHeight = 17
        Me.tdbcSelection02IDTo.ColumnFooterHeight = 17
        Me.tdbcSelection02IDTo.ColumnWidth = 100
        Me.tdbcSelection02IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection02IDTo.DisplayMember = "Code"
        Me.tdbcSelection02IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection02IDTo.DropDownWidth = 300
        Me.tdbcSelection02IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection02IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection02IDTo.EmptyRows = True
        Me.tdbcSelection02IDTo.ExtendRightColumn = True
        Me.tdbcSelection02IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDTo.Images.Add(CType(resources.GetObject("tdbcSelection02IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection02IDTo.ItemHeight = 15
        Me.tdbcSelection02IDTo.Location = New System.Drawing.Point(350, 54)
        Me.tdbcSelection02IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection02IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection02IDTo.MaxLength = 32767
        Me.tdbcSelection02IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection02IDTo.Name = "tdbcSelection02IDTo"
        Me.tdbcSelection02IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection02IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection02IDTo.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection02IDTo.TabIndex = 7
        Me.tdbcSelection02IDTo.ValueMember = "Code"
        Me.tdbcSelection02IDTo.PropBag = resources.GetString("tdbcSelection02IDTo.PropBag")
        '
        'tdbcSelection01IDTo
        '
        Me.tdbcSelection01IDTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection01IDTo.AllowColMove = False
        Me.tdbcSelection01IDTo.AllowSort = False
        Me.tdbcSelection01IDTo.AlternatingRows = True
        Me.tdbcSelection01IDTo.AutoCompletion = True
        Me.tdbcSelection01IDTo.AutoDropDown = True
        Me.tdbcSelection01IDTo.Caption = ""
        Me.tdbcSelection01IDTo.CaptionHeight = 17
        Me.tdbcSelection01IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection01IDTo.ColumnCaptionHeight = 17
        Me.tdbcSelection01IDTo.ColumnFooterHeight = 17
        Me.tdbcSelection01IDTo.ColumnWidth = 100
        Me.tdbcSelection01IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection01IDTo.DisplayMember = "Code"
        Me.tdbcSelection01IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection01IDTo.DropDownWidth = 300
        Me.tdbcSelection01IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection01IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection01IDTo.EmptyRows = True
        Me.tdbcSelection01IDTo.ExtendRightColumn = True
        Me.tdbcSelection01IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDTo.Images.Add(CType(resources.GetObject("tdbcSelection01IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection01IDTo.ItemHeight = 15
        Me.tdbcSelection01IDTo.Location = New System.Drawing.Point(350, 27)
        Me.tdbcSelection01IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection01IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection01IDTo.MaxLength = 32767
        Me.tdbcSelection01IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection01IDTo.Name = "tdbcSelection01IDTo"
        Me.tdbcSelection01IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection01IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection01IDTo.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection01IDTo.TabIndex = 3
        Me.tdbcSelection01IDTo.ValueMember = "Code"
        Me.tdbcSelection01IDTo.PropBag = resources.GetString("tdbcSelection01IDTo.PropBag")
        '
        'tdbcSelection05IDFrom
        '
        Me.tdbcSelection05IDFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection05IDFrom.AllowColMove = False
        Me.tdbcSelection05IDFrom.AllowSort = False
        Me.tdbcSelection05IDFrom.AlternatingRows = True
        Me.tdbcSelection05IDFrom.AutoCompletion = True
        Me.tdbcSelection05IDFrom.AutoDropDown = True
        Me.tdbcSelection05IDFrom.Caption = ""
        Me.tdbcSelection05IDFrom.CaptionHeight = 17
        Me.tdbcSelection05IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection05IDFrom.ColumnCaptionHeight = 17
        Me.tdbcSelection05IDFrom.ColumnFooterHeight = 17
        Me.tdbcSelection05IDFrom.ColumnWidth = 100
        Me.tdbcSelection05IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection05IDFrom.DisplayMember = "Code"
        Me.tdbcSelection05IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection05IDFrom.DropDownWidth = 300
        Me.tdbcSelection05IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection05IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection05IDFrom.EmptyRows = True
        Me.tdbcSelection05IDFrom.ExtendRightColumn = True
        Me.tdbcSelection05IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection05IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection05IDFrom.ItemHeight = 15
        Me.tdbcSelection05IDFrom.Location = New System.Drawing.Point(123, 140)
        Me.tdbcSelection05IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection05IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection05IDFrom.MaxLength = 32767
        Me.tdbcSelection05IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection05IDFrom.Name = "tdbcSelection05IDFrom"
        Me.tdbcSelection05IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection05IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection05IDFrom.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection05IDFrom.TabIndex = 17
        Me.tdbcSelection05IDFrom.ValueMember = "Code"
        Me.tdbcSelection05IDFrom.PropBag = resources.GetString("tdbcSelection05IDFrom.PropBag")
        '
        'tdbcSelection04IDFrom
        '
        Me.tdbcSelection04IDFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection04IDFrom.AllowColMove = False
        Me.tdbcSelection04IDFrom.AllowSort = False
        Me.tdbcSelection04IDFrom.AlternatingRows = True
        Me.tdbcSelection04IDFrom.AutoCompletion = True
        Me.tdbcSelection04IDFrom.AutoDropDown = True
        Me.tdbcSelection04IDFrom.Caption = ""
        Me.tdbcSelection04IDFrom.CaptionHeight = 17
        Me.tdbcSelection04IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection04IDFrom.ColumnCaptionHeight = 17
        Me.tdbcSelection04IDFrom.ColumnFooterHeight = 17
        Me.tdbcSelection04IDFrom.ColumnWidth = 100
        Me.tdbcSelection04IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection04IDFrom.DisplayMember = "Code"
        Me.tdbcSelection04IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection04IDFrom.DropDownWidth = 300
        Me.tdbcSelection04IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection04IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection04IDFrom.EmptyRows = True
        Me.tdbcSelection04IDFrom.ExtendRightColumn = True
        Me.tdbcSelection04IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection04IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection04IDFrom.ItemHeight = 15
        Me.tdbcSelection04IDFrom.Location = New System.Drawing.Point(123, 111)
        Me.tdbcSelection04IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection04IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection04IDFrom.MaxLength = 32767
        Me.tdbcSelection04IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection04IDFrom.Name = "tdbcSelection04IDFrom"
        Me.tdbcSelection04IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection04IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection04IDFrom.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection04IDFrom.TabIndex = 13
        Me.tdbcSelection04IDFrom.ValueMember = "Code"
        Me.tdbcSelection04IDFrom.PropBag = resources.GetString("tdbcSelection04IDFrom.PropBag")
        '
        'tdbcSelection03IDFrom
        '
        Me.tdbcSelection03IDFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection03IDFrom.AllowColMove = False
        Me.tdbcSelection03IDFrom.AllowSort = False
        Me.tdbcSelection03IDFrom.AlternatingRows = True
        Me.tdbcSelection03IDFrom.AutoCompletion = True
        Me.tdbcSelection03IDFrom.AutoDropDown = True
        Me.tdbcSelection03IDFrom.Caption = ""
        Me.tdbcSelection03IDFrom.CaptionHeight = 17
        Me.tdbcSelection03IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection03IDFrom.ColumnCaptionHeight = 17
        Me.tdbcSelection03IDFrom.ColumnFooterHeight = 17
        Me.tdbcSelection03IDFrom.ColumnWidth = 100
        Me.tdbcSelection03IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection03IDFrom.DisplayMember = "Code"
        Me.tdbcSelection03IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection03IDFrom.DropDownWidth = 300
        Me.tdbcSelection03IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection03IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection03IDFrom.EmptyRows = True
        Me.tdbcSelection03IDFrom.ExtendRightColumn = True
        Me.tdbcSelection03IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection03IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection03IDFrom.ItemHeight = 15
        Me.tdbcSelection03IDFrom.Location = New System.Drawing.Point(123, 82)
        Me.tdbcSelection03IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection03IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection03IDFrom.MaxLength = 32767
        Me.tdbcSelection03IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection03IDFrom.Name = "tdbcSelection03IDFrom"
        Me.tdbcSelection03IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection03IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection03IDFrom.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection03IDFrom.TabIndex = 9
        Me.tdbcSelection03IDFrom.ValueMember = "Code"
        Me.tdbcSelection03IDFrom.PropBag = resources.GetString("tdbcSelection03IDFrom.PropBag")
        '
        'tdbcSelection02IDFrom
        '
        Me.tdbcSelection02IDFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection02IDFrom.AllowColMove = False
        Me.tdbcSelection02IDFrom.AllowSort = False
        Me.tdbcSelection02IDFrom.AlternatingRows = True
        Me.tdbcSelection02IDFrom.AutoCompletion = True
        Me.tdbcSelection02IDFrom.AutoDropDown = True
        Me.tdbcSelection02IDFrom.Caption = ""
        Me.tdbcSelection02IDFrom.CaptionHeight = 17
        Me.tdbcSelection02IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection02IDFrom.ColumnCaptionHeight = 17
        Me.tdbcSelection02IDFrom.ColumnFooterHeight = 17
        Me.tdbcSelection02IDFrom.ColumnWidth = 100
        Me.tdbcSelection02IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection02IDFrom.DisplayMember = "Code"
        Me.tdbcSelection02IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection02IDFrom.DropDownWidth = 300
        Me.tdbcSelection02IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection02IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection02IDFrom.EmptyRows = True
        Me.tdbcSelection02IDFrom.ExtendRightColumn = True
        Me.tdbcSelection02IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection02IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection02IDFrom.ItemHeight = 15
        Me.tdbcSelection02IDFrom.Location = New System.Drawing.Point(123, 54)
        Me.tdbcSelection02IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection02IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection02IDFrom.MaxLength = 32767
        Me.tdbcSelection02IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection02IDFrom.Name = "tdbcSelection02IDFrom"
        Me.tdbcSelection02IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection02IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection02IDFrom.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection02IDFrom.TabIndex = 5
        Me.tdbcSelection02IDFrom.ValueMember = "Code"
        Me.tdbcSelection02IDFrom.PropBag = resources.GetString("tdbcSelection02IDFrom.PropBag")
        '
        'tdbcSelection01IDFrom
        '
        Me.tdbcSelection01IDFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcSelection01IDFrom.AllowColMove = False
        Me.tdbcSelection01IDFrom.AllowSort = False
        Me.tdbcSelection01IDFrom.AlternatingRows = True
        Me.tdbcSelection01IDFrom.AutoCompletion = True
        Me.tdbcSelection01IDFrom.AutoDropDown = True
        Me.tdbcSelection01IDFrom.Caption = ""
        Me.tdbcSelection01IDFrom.CaptionHeight = 17
        Me.tdbcSelection01IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection01IDFrom.ColumnCaptionHeight = 17
        Me.tdbcSelection01IDFrom.ColumnFooterHeight = 17
        Me.tdbcSelection01IDFrom.ColumnWidth = 100
        Me.tdbcSelection01IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection01IDFrom.DisplayMember = "Code"
        Me.tdbcSelection01IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection01IDFrom.DropDownWidth = 300
        Me.tdbcSelection01IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection01IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection01IDFrom.EmptyRows = True
        Me.tdbcSelection01IDFrom.ExtendRightColumn = True
        Me.tdbcSelection01IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection01IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection01IDFrom.ItemHeight = 15
        Me.tdbcSelection01IDFrom.Location = New System.Drawing.Point(123, 27)
        Me.tdbcSelection01IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection01IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection01IDFrom.MaxLength = 32767
        Me.tdbcSelection01IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection01IDFrom.Name = "tdbcSelection01IDFrom"
        Me.tdbcSelection01IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection01IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection01IDFrom.Size = New System.Drawing.Size(128, 23)
        Me.tdbcSelection01IDFrom.TabIndex = 1
        Me.tdbcSelection01IDFrom.ValueMember = "Code"
        Me.tdbcSelection01IDFrom.PropBag = resources.GetString("tdbcSelection01IDFrom.PropBag")
        '
        'lblSelection01IDFrom
        '
        Me.lblSelection01IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection01IDFrom.Location = New System.Drawing.Point(20, 24)
        Me.lblSelection01IDFrom.Name = "lblSelection01IDFrom"
        Me.lblSelection01IDFrom.Size = New System.Drawing.Size(97, 29)
        Me.lblSelection01IDFrom.TabIndex = 0
        Me.lblSelection01IDFrom.Text = "Tieâu thöùc 1"
        Me.lblSelection01IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection02IDFrom
        '
        Me.lblSelection02IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection02IDFrom.Location = New System.Drawing.Point(20, 49)
        Me.lblSelection02IDFrom.Name = "lblSelection02IDFrom"
        Me.lblSelection02IDFrom.Size = New System.Drawing.Size(97, 33)
        Me.lblSelection02IDFrom.TabIndex = 4
        Me.lblSelection02IDFrom.Text = "Tieâu thöùc 2"
        Me.lblSelection02IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection03IDFrom
        '
        Me.lblSelection03IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection03IDFrom.Location = New System.Drawing.Point(20, 77)
        Me.lblSelection03IDFrom.Name = "lblSelection03IDFrom"
        Me.lblSelection03IDFrom.Size = New System.Drawing.Size(97, 33)
        Me.lblSelection03IDFrom.TabIndex = 8
        Me.lblSelection03IDFrom.Text = "Tieâu thöùc 3"
        Me.lblSelection03IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection04IDFrom
        '
        Me.lblSelection04IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection04IDFrom.Location = New System.Drawing.Point(20, 108)
        Me.lblSelection04IDFrom.Name = "lblSelection04IDFrom"
        Me.lblSelection04IDFrom.Size = New System.Drawing.Size(97, 29)
        Me.lblSelection04IDFrom.TabIndex = 12
        Me.lblSelection04IDFrom.Text = "Tieâu thöùc 4"
        Me.lblSelection04IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection05IDFrom
        '
        Me.lblSelection05IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection05IDFrom.Location = New System.Drawing.Point(20, 135)
        Me.lblSelection05IDFrom.Name = "lblSelection05IDFrom"
        Me.lblSelection05IDFrom.Size = New System.Drawing.Size(97, 33)
        Me.lblSelection05IDFrom.TabIndex = 16
        Me.lblSelection05IDFrom.Text = "Tieâu thöùc 5"
        Me.lblSelection05IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection01IDTo
        '
        Me.lblSelection01IDTo.AutoSize = True
        Me.lblSelection01IDTo.Location = New System.Drawing.Point(293, 32)
        Me.lblSelection01IDTo.Name = "lblSelection01IDTo"
        Me.lblSelection01IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection01IDTo.TabIndex = 2
        Me.lblSelection01IDTo.Text = "---"
        Me.lblSelection01IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection02IDTo
        '
        Me.lblSelection02IDTo.AutoSize = True
        Me.lblSelection02IDTo.Location = New System.Drawing.Point(293, 59)
        Me.lblSelection02IDTo.Name = "lblSelection02IDTo"
        Me.lblSelection02IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection02IDTo.TabIndex = 6
        Me.lblSelection02IDTo.Text = "---"
        Me.lblSelection02IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection03IDTo
        '
        Me.lblSelection03IDTo.AutoSize = True
        Me.lblSelection03IDTo.Location = New System.Drawing.Point(293, 87)
        Me.lblSelection03IDTo.Name = "lblSelection03IDTo"
        Me.lblSelection03IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection03IDTo.TabIndex = 10
        Me.lblSelection03IDTo.Text = "---"
        Me.lblSelection03IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection05IDTo
        '
        Me.lblSelection05IDTo.AutoSize = True
        Me.lblSelection05IDTo.Location = New System.Drawing.Point(293, 145)
        Me.lblSelection05IDTo.Name = "lblSelection05IDTo"
        Me.lblSelection05IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection05IDTo.TabIndex = 18
        Me.lblSelection05IDTo.Text = "---"
        Me.lblSelection05IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection04IDTo
        '
        Me.lblSelection04IDTo.AutoSize = True
        Me.lblSelection04IDTo.Location = New System.Drawing.Point(293, 116)
        Me.lblSelection04IDTo.Name = "lblSelection04IDTo"
        Me.lblSelection04IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection04IDTo.TabIndex = 14
        Me.lblSelection04IDTo.Text = "---"
        Me.lblSelection04IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpTimeInfo
        '
        Me.grpTimeInfo.Controls.Add(Me.tdbcPeriodTo)
        Me.grpTimeInfo.Controls.Add(Me.tdbcPeriodFrom)
        Me.grpTimeInfo.Controls.Add(Me.lblPeriodFrom)
        Me.grpTimeInfo.Controls.Add(Me.lblPeriodTo)
        Me.grpTimeInfo.Location = New System.Drawing.Point(3, 352)
        Me.grpTimeInfo.Name = "grpTimeInfo"
        Me.grpTimeInfo.Size = New System.Drawing.Size(590, 54)
        Me.grpTimeInfo.TabIndex = 3
        Me.grpTimeInfo.TabStop = False
        Me.grpTimeInfo.Text = "Thông tin thời gian"
        '
        'tdbcPeriodTo
        '
        Me.tdbcPeriodTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcPeriodTo.AllowColMove = False
        Me.tdbcPeriodTo.AllowSort = False
        Me.tdbcPeriodTo.AlternatingRows = True
        Me.tdbcPeriodTo.AutoCompletion = True
        Me.tdbcPeriodTo.AutoDropDown = True
        Me.tdbcPeriodTo.AutoSelect = True
        Me.tdbcPeriodTo.Caption = ""
        Me.tdbcPeriodTo.CaptionHeight = 17
        Me.tdbcPeriodTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcPeriodTo.ColumnCaptionHeight = 17
        Me.tdbcPeriodTo.ColumnFooterHeight = 17
        Me.tdbcPeriodTo.ColumnHeaders = False
        Me.tdbcPeriodTo.ColumnWidth = 100
        Me.tdbcPeriodTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcPeriodTo.DisplayMember = "Period"
        Me.tdbcPeriodTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcPeriodTo.DropDownWidth = 128
        Me.tdbcPeriodTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcPeriodTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcPeriodTo.EmptyRows = True
        Me.tdbcPeriodTo.ExtendRightColumn = True
        Me.tdbcPeriodTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodTo.Images.Add(CType(resources.GetObject("tdbcPeriodTo.Images"), System.Drawing.Image))
        Me.tdbcPeriodTo.ItemHeight = 15
        Me.tdbcPeriodTo.Location = New System.Drawing.Point(350, 21)
        Me.tdbcPeriodTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcPeriodTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcPeriodTo.MaxLength = 32767
        Me.tdbcPeriodTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcPeriodTo.Name = "tdbcPeriodTo"
        Me.tdbcPeriodTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcPeriodTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcPeriodTo.Size = New System.Drawing.Size(128, 23)
        Me.tdbcPeriodTo.TabIndex = 3
        Me.tdbcPeriodTo.ValueMember = "Period"
        Me.tdbcPeriodTo.PropBag = resources.GetString("tdbcPeriodTo.PropBag")
        '
        'tdbcPeriodFrom
        '
        Me.tdbcPeriodFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcPeriodFrom.AllowColMove = False
        Me.tdbcPeriodFrom.AllowSort = False
        Me.tdbcPeriodFrom.AlternatingRows = True
        Me.tdbcPeriodFrom.AutoCompletion = True
        Me.tdbcPeriodFrom.AutoDropDown = True
        Me.tdbcPeriodFrom.AutoSelect = True
        Me.tdbcPeriodFrom.Caption = ""
        Me.tdbcPeriodFrom.CaptionHeight = 17
        Me.tdbcPeriodFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcPeriodFrom.ColumnCaptionHeight = 17
        Me.tdbcPeriodFrom.ColumnFooterHeight = 17
        Me.tdbcPeriodFrom.ColumnHeaders = False
        Me.tdbcPeriodFrom.ColumnWidth = 100
        Me.tdbcPeriodFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcPeriodFrom.DisplayMember = "Period"
        Me.tdbcPeriodFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcPeriodFrom.DropDownWidth = 128
        Me.tdbcPeriodFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcPeriodFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcPeriodFrom.EmptyRows = True
        Me.tdbcPeriodFrom.ExtendRightColumn = True
        Me.tdbcPeriodFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodFrom.Images.Add(CType(resources.GetObject("tdbcPeriodFrom.Images"), System.Drawing.Image))
        Me.tdbcPeriodFrom.ItemHeight = 15
        Me.tdbcPeriodFrom.Location = New System.Drawing.Point(123, 21)
        Me.tdbcPeriodFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcPeriodFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcPeriodFrom.MaxLength = 32767
        Me.tdbcPeriodFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcPeriodFrom.Name = "tdbcPeriodFrom"
        Me.tdbcPeriodFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcPeriodFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcPeriodFrom.Size = New System.Drawing.Size(128, 23)
        Me.tdbcPeriodFrom.TabIndex = 1
        Me.tdbcPeriodFrom.ValueMember = "Period"
        Me.tdbcPeriodFrom.PropBag = resources.GetString("tdbcPeriodFrom.PropBag")
        '
        'lblPeriodFrom
        '
        Me.lblPeriodFrom.AutoSize = True
        Me.lblPeriodFrom.Location = New System.Drawing.Point(20, 26)
        Me.lblPeriodFrom.Name = "lblPeriodFrom"
        Me.lblPeriodFrom.Size = New System.Drawing.Size(19, 13)
        Me.lblPeriodFrom.TabIndex = 0
        Me.lblPeriodFrom.Text = "Kỳ"
        Me.lblPeriodFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPeriodTo
        '
        Me.lblPeriodTo.AutoSize = True
        Me.lblPeriodTo.Location = New System.Drawing.Point(293, 26)
        Me.lblPeriodTo.Name = "lblPeriodTo"
        Me.lblPeriodTo.Size = New System.Drawing.Size(16, 13)
        Me.lblPeriodTo.TabIndex = 2
        Me.lblPeriodTo.Text = "---"
        Me.lblPeriodTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(435, 412)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 22)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "&In"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(517, 412)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 22)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Đó&ng"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(5, 412)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(76, 22)
        Me.btnFilter.TabIndex = 8
        Me.btnFilter.Text = "&Lọc"
        '
        'D02F7005
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 440)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.grpTimeInfo)
        Me.Controls.Add(Me.grpGroup)
        Me.Controls.Add(Me.grpReport)
        Me.Controls.Add(Me.grpDivision)
        Me.Controls.Add(Me.btnFilter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "D02F7005"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BÀo cÀo ph¡n tÛch tªi s¶n - D02F7005"
        Me.grpDivision.ResumeLayout(False)
        Me.grpDivision.PerformLayout()
        CType(Me.tdbcDivisionID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReport.ResumeLayout(False)
        Me.grpReport.PerformLayout()
        CType(Me.tdbcReportID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGroup.ResumeLayout(False)
        Me.grpGroup.PerformLayout()
        CType(Me.tdbcSelection04IDTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection05IDTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection03IDTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection02IDTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection01IDTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection05IDFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection04IDFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection03IDFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection02IDFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcSelection01IDFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTimeInfo.ResumeLayout(False)
        Me.grpTimeInfo.PerformLayout()
        CType(Me.tdbcPeriodTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcPeriodFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpDivision As System.Windows.Forms.GroupBox
    Private WithEvents tdbcDivisionID As C1.Win.C1List.C1Combo
    Private WithEvents lblDivisionID As System.Windows.Forms.Label
    Private WithEvents txtDivisionName As System.Windows.Forms.TextBox
    Private WithEvents grpReport As System.Windows.Forms.GroupBox
    Private WithEvents tdbcReportID As C1.Win.C1List.C1Combo
    Private WithEvents txtNotes As System.Windows.Forms.TextBox
    Private WithEvents txtReportName2 As System.Windows.Forms.TextBox
    Private WithEvents lblReportID As System.Windows.Forms.Label
    Private WithEvents txtReportName As System.Windows.Forms.TextBox
    Private WithEvents lblReEportName2 As System.Windows.Forms.Label
    Private WithEvents lblNotes As System.Windows.Forms.Label
    Private WithEvents grpGroup As System.Windows.Forms.GroupBox
    Private WithEvents tdbcSelection03IDFrom As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection02IDFrom As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection01IDFrom As C1.Win.C1List.C1Combo
    Private WithEvents lblSelection01IDFrom As System.Windows.Forms.Label
    Private WithEvents lblSelection02IDFrom As System.Windows.Forms.Label
    Private WithEvents lblSelection03IDFrom As System.Windows.Forms.Label
    Private WithEvents tdbcSelection04IDFrom As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection04IDTo As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection05IDTo As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection03IDTo As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection02IDTo As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection01IDTo As C1.Win.C1List.C1Combo
    Private WithEvents tdbcSelection05IDFrom As C1.Win.C1List.C1Combo
    Private WithEvents lblSelection04IDFrom As System.Windows.Forms.Label
    Private WithEvents lblSelection05IDFrom As System.Windows.Forms.Label
    Private WithEvents lblSelection01IDTo As System.Windows.Forms.Label
    Private WithEvents lblSelection02IDTo As System.Windows.Forms.Label
    Private WithEvents lblSelection03IDTo As System.Windows.Forms.Label
    Private WithEvents lblSelection05IDTo As System.Windows.Forms.Label
    Private WithEvents lblSelection04IDTo As System.Windows.Forms.Label
    Private WithEvents grpTimeInfo As System.Windows.Forms.GroupBox
    Private WithEvents tdbcPeriodTo As C1.Win.C1List.C1Combo
    Private WithEvents tdbcPeriodFrom As C1.Win.C1List.C1Combo
    Private WithEvents lblPeriodFrom As System.Windows.Forms.Label
    Private WithEvents lblPeriodTo As System.Windows.Forms.Label
    Private WithEvents btnPrint As System.Windows.Forms.Button
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnFilter As System.Windows.Forms.Button
End Class