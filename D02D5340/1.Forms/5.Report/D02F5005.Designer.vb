<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class D02F5005
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(D02F5005))
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
        Me.c1dateDateTo = New C1.Win.C1Input.C1DateEdit()
        Me.c1dateDateFrom = New C1.Win.C1Input.C1DateEdit()
        Me.optDate = New System.Windows.Forms.RadioButton()
        Me.optPeriod = New System.Windows.Forms.RadioButton()
        Me.tdbcPeriodTo = New C1.Win.C1List.C1Combo()
        Me.tdbcPeriodFrom = New C1.Win.C1List.C1Combo()
        Me.lblPeriodTo = New System.Windows.Forms.Label()
        Me.lblteDateTo = New System.Windows.Forms.Label()
        Me.chkExceptSetup = New System.Windows.Forms.CheckBox()
        Me.chkExecptVoucherSetup = New System.Windows.Forms.CheckBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkCheckTime = New System.Windows.Forms.CheckBox()
        Me.chkIsVoucherNotCIP = New System.Windows.Forms.CheckBox()
        Me.chkNotCompleteSplit = New System.Windows.Forms.CheckBox()
        Me.lblTimeInfo = New System.Windows.Forms.Label()
        Me.lblGroup = New System.Windows.Forms.Label()
        Me.lblReport = New System.Windows.Forms.Label()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.chkNotCIPFinalization = New System.Windows.Forms.CheckBox()
        CType(Me.tdbcDivisionID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcReportID, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.c1dateDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1dateDateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcPeriodTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcPeriodFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMain.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpDivision
        '
        Me.grpDivision.Location = New System.Drawing.Point(71, 26)
        Me.grpDivision.Name = "grpDivision"
        Me.grpDivision.Size = New System.Drawing.Size(490, 2)
        Me.grpDivision.TabIndex = 1
        Me.grpDivision.TabStop = False
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
        Me.tdbcDivisionID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
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
        Me.tdbcDivisionID.Location = New System.Drawing.Point(141, 40)
        Me.tdbcDivisionID.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcDivisionID.MaxDropDownItems = CType(8, Short)
        Me.tdbcDivisionID.MaxLength = 32767
        Me.tdbcDivisionID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcDivisionID.Name = "tdbcDivisionID"
        Me.tdbcDivisionID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcDivisionID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcDivisionID.Size = New System.Drawing.Size(114, 21)
        Me.tdbcDivisionID.TabIndex = 3
        Me.tdbcDivisionID.ValueMember = "DivisionID"
        Me.tdbcDivisionID.PropBag = resources.GetString("tdbcDivisionID.PropBag")
        '
        'lblDivisionID
        '
        Me.lblDivisionID.AutoSize = True
        Me.lblDivisionID.Location = New System.Drawing.Point(12, 44)
        Me.lblDivisionID.Name = "lblDivisionID"
        Me.lblDivisionID.Size = New System.Drawing.Size(38, 13)
        Me.lblDivisionID.TabIndex = 2
        Me.lblDivisionID.Text = "Đơn vị"
        Me.lblDivisionID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivisionName.Location = New System.Drawing.Point(261, 40)
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.ReadOnly = True
        Me.txtDivisionName.Size = New System.Drawing.Size(301, 20)
        Me.txtDivisionName.TabIndex = 4
        Me.txtDivisionName.TabStop = False
        '
        'grpReport
        '
        Me.grpReport.Location = New System.Drawing.Point(80, 75)
        Me.grpReport.Name = "grpReport"
        Me.grpReport.Size = New System.Drawing.Size(480, 2)
        Me.grpReport.TabIndex = 6
        Me.grpReport.TabStop = False
        '
        'txtNotes
        '
        Me.txtNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!)
        Me.txtNotes.Location = New System.Drawing.Point(141, 144)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(421, 20)
        Me.txtNotes.TabIndex = 14
        '
        'txtReportName2
        '
        Me.txtReportName2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!)
        Me.txtReportName2.Location = New System.Drawing.Point(141, 116)
        Me.txtReportName2.Name = "txtReportName2"
        Me.txtReportName2.Size = New System.Drawing.Size(421, 20)
        Me.txtReportName2.TabIndex = 12
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
        Me.tdbcReportID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcReportID.ColumnWidth = 100
        Me.tdbcReportID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcReportID.DisplayMember = "ReportCode"
        Me.tdbcReportID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcReportID.DropDownWidth = 400
        Me.tdbcReportID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcReportID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcReportID.EmptyRows = True
        Me.tdbcReportID.ExtendRightColumn = True
        Me.tdbcReportID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportID.Images.Add(CType(resources.GetObject("tdbcReportID.Images"), System.Drawing.Image))
        Me.tdbcReportID.Location = New System.Drawing.Point(141, 88)
        Me.tdbcReportID.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcReportID.MaxDropDownItems = CType(8, Short)
        Me.tdbcReportID.MaxLength = 32767
        Me.tdbcReportID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcReportID.Name = "tdbcReportID"
        Me.tdbcReportID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcReportID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcReportID.Size = New System.Drawing.Size(114, 21)
        Me.tdbcReportID.TabIndex = 8
        Me.tdbcReportID.ValueMember = "ReportCode"
        Me.tdbcReportID.PropBag = resources.GetString("tdbcReportID.PropBag")
        '
        'lblReportID
        '
        Me.lblReportID.AutoSize = True
        Me.lblReportID.Location = New System.Drawing.Point(12, 93)
        Me.lblReportID.Name = "lblReportID"
        Me.lblReportID.Size = New System.Drawing.Size(67, 13)
        Me.lblReportID.TabIndex = 7
        Me.lblReportID.Text = "Mã  báo cáo"
        Me.lblReportID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReportName
        '
        Me.txtReportName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtReportName.Location = New System.Drawing.Point(261, 88)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.ReadOnly = True
        Me.txtReportName.Size = New System.Drawing.Size(301, 20)
        Me.txtReportName.TabIndex = 9
        Me.txtReportName.TabStop = False
        '
        'lblReEportName2
        '
        Me.lblReEportName2.AutoSize = True
        Me.lblReEportName2.Location = New System.Drawing.Point(12, 121)
        Me.lblReEportName2.Name = "lblReEportName2"
        Me.lblReEportName2.Size = New System.Drawing.Size(68, 13)
        Me.lblReEportName2.TabIndex = 11
        Me.lblReEportName2.Text = "Tên báo cáo"
        Me.lblReEportName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Location = New System.Drawing.Point(12, 149)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(44, 13)
        Me.lblNotes.TabIndex = 13
        Me.lblNotes.Text = "Ghi chú"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpGroup
        '
        Me.grpGroup.Location = New System.Drawing.Point(113, 180)
        Me.grpGroup.Name = "grpGroup"
        Me.grpGroup.Size = New System.Drawing.Size(450, 2)
        Me.grpGroup.TabIndex = 16
        Me.grpGroup.TabStop = False
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
        Me.tdbcSelection04IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection04IDTo.ColumnWidth = 100
        Me.tdbcSelection04IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection04IDTo.DisplayMember = "Code"
        Me.tdbcSelection04IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown
        Me.tdbcSelection04IDTo.DropDownWidth = 400
        Me.tdbcSelection04IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection04IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection04IDTo.EmptyRows = True
        Me.tdbcSelection04IDTo.ExtendRightColumn = True
        Me.tdbcSelection04IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDTo.Images.Add(CType(resources.GetObject("tdbcSelection04IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection04IDTo.Location = New System.Drawing.Point(397, 278)
        Me.tdbcSelection04IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection04IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection04IDTo.MaxLength = 32767
        Me.tdbcSelection04IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection04IDTo.Name = "tdbcSelection04IDTo"
        Me.tdbcSelection04IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection04IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection04IDTo.Size = New System.Drawing.Size(166, 21)
        Me.tdbcSelection04IDTo.TabIndex = 32
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
        Me.tdbcSelection05IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection05IDTo.ColumnWidth = 100
        Me.tdbcSelection05IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection05IDTo.DisplayMember = "Code"
        Me.tdbcSelection05IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown
        Me.tdbcSelection05IDTo.DropDownWidth = 400
        Me.tdbcSelection05IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection05IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection05IDTo.EmptyRows = True
        Me.tdbcSelection05IDTo.ExtendRightColumn = True
        Me.tdbcSelection05IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDTo.Images.Add(CType(resources.GetObject("tdbcSelection05IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection05IDTo.Location = New System.Drawing.Point(397, 307)
        Me.tdbcSelection05IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection05IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection05IDTo.MaxLength = 32767
        Me.tdbcSelection05IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection05IDTo.Name = "tdbcSelection05IDTo"
        Me.tdbcSelection05IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection05IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection05IDTo.Size = New System.Drawing.Size(166, 21)
        Me.tdbcSelection05IDTo.TabIndex = 36
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
        Me.tdbcSelection03IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection03IDTo.ColumnWidth = 100
        Me.tdbcSelection03IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection03IDTo.DisplayMember = "Code"
        Me.tdbcSelection03IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown
        Me.tdbcSelection03IDTo.DropDownWidth = 400
        Me.tdbcSelection03IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection03IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection03IDTo.EmptyRows = True
        Me.tdbcSelection03IDTo.ExtendRightColumn = True
        Me.tdbcSelection03IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDTo.Images.Add(CType(resources.GetObject("tdbcSelection03IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection03IDTo.Location = New System.Drawing.Point(397, 249)
        Me.tdbcSelection03IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection03IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection03IDTo.MaxLength = 32767
        Me.tdbcSelection03IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection03IDTo.Name = "tdbcSelection03IDTo"
        Me.tdbcSelection03IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection03IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection03IDTo.Size = New System.Drawing.Size(166, 21)
        Me.tdbcSelection03IDTo.TabIndex = 28
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
        Me.tdbcSelection02IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection02IDTo.ColumnWidth = 100
        Me.tdbcSelection02IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection02IDTo.DisplayMember = "Code"
        Me.tdbcSelection02IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown
        Me.tdbcSelection02IDTo.DropDownWidth = 400
        Me.tdbcSelection02IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection02IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection02IDTo.EmptyRows = True
        Me.tdbcSelection02IDTo.ExtendRightColumn = True
        Me.tdbcSelection02IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDTo.Images.Add(CType(resources.GetObject("tdbcSelection02IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection02IDTo.Location = New System.Drawing.Point(397, 221)
        Me.tdbcSelection02IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection02IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection02IDTo.MaxLength = 32767
        Me.tdbcSelection02IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection02IDTo.Name = "tdbcSelection02IDTo"
        Me.tdbcSelection02IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection02IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection02IDTo.Size = New System.Drawing.Size(166, 21)
        Me.tdbcSelection02IDTo.TabIndex = 24
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
        Me.tdbcSelection01IDTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection01IDTo.ColumnWidth = 100
        Me.tdbcSelection01IDTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection01IDTo.DisplayMember = "Code"
        Me.tdbcSelection01IDTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown
        Me.tdbcSelection01IDTo.DropDownWidth = 400
        Me.tdbcSelection01IDTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection01IDTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection01IDTo.EmptyRows = True
        Me.tdbcSelection01IDTo.ExtendRightColumn = True
        Me.tdbcSelection01IDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDTo.Images.Add(CType(resources.GetObject("tdbcSelection01IDTo.Images"), System.Drawing.Image))
        Me.tdbcSelection01IDTo.Location = New System.Drawing.Point(397, 194)
        Me.tdbcSelection01IDTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection01IDTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection01IDTo.MaxLength = 32767
        Me.tdbcSelection01IDTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection01IDTo.Name = "tdbcSelection01IDTo"
        Me.tdbcSelection01IDTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection01IDTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection01IDTo.Size = New System.Drawing.Size(166, 21)
        Me.tdbcSelection01IDTo.TabIndex = 20
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
        Me.tdbcSelection05IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection05IDFrom.ColumnWidth = 100
        Me.tdbcSelection05IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection05IDFrom.DisplayMember = "Code"
        Me.tdbcSelection05IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection05IDFrom.DropDownWidth = 400
        Me.tdbcSelection05IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection05IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection05IDFrom.EmptyRows = True
        Me.tdbcSelection05IDFrom.ExtendRightColumn = True
        Me.tdbcSelection05IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection05IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection05IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection05IDFrom.Location = New System.Drawing.Point(141, 304)
        Me.tdbcSelection05IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection05IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection05IDFrom.MaxLength = 32767
        Me.tdbcSelection05IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection05IDFrom.Name = "tdbcSelection05IDFrom"
        Me.tdbcSelection05IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection05IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection05IDFrom.Size = New System.Drawing.Size(152, 21)
        Me.tdbcSelection05IDFrom.TabIndex = 34
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
        Me.tdbcSelection04IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection04IDFrom.ColumnWidth = 100
        Me.tdbcSelection04IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection04IDFrom.DisplayMember = "Code"
        Me.tdbcSelection04IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection04IDFrom.DropDownWidth = 400
        Me.tdbcSelection04IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection04IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection04IDFrom.EmptyRows = True
        Me.tdbcSelection04IDFrom.ExtendRightColumn = True
        Me.tdbcSelection04IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection04IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection04IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection04IDFrom.Location = New System.Drawing.Point(141, 275)
        Me.tdbcSelection04IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection04IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection04IDFrom.MaxLength = 32767
        Me.tdbcSelection04IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection04IDFrom.Name = "tdbcSelection04IDFrom"
        Me.tdbcSelection04IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection04IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection04IDFrom.Size = New System.Drawing.Size(152, 21)
        Me.tdbcSelection04IDFrom.TabIndex = 30
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
        Me.tdbcSelection03IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection03IDFrom.ColumnWidth = 100
        Me.tdbcSelection03IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection03IDFrom.DisplayMember = "Code"
        Me.tdbcSelection03IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection03IDFrom.DropDownWidth = 400
        Me.tdbcSelection03IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection03IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection03IDFrom.EmptyRows = True
        Me.tdbcSelection03IDFrom.ExtendRightColumn = True
        Me.tdbcSelection03IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection03IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection03IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection03IDFrom.Location = New System.Drawing.Point(141, 246)
        Me.tdbcSelection03IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection03IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection03IDFrom.MaxLength = 32767
        Me.tdbcSelection03IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection03IDFrom.Name = "tdbcSelection03IDFrom"
        Me.tdbcSelection03IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection03IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection03IDFrom.Size = New System.Drawing.Size(152, 21)
        Me.tdbcSelection03IDFrom.TabIndex = 26
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
        Me.tdbcSelection02IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection02IDFrom.ColumnWidth = 100
        Me.tdbcSelection02IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection02IDFrom.DisplayMember = "Code"
        Me.tdbcSelection02IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection02IDFrom.DropDownWidth = 400
        Me.tdbcSelection02IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection02IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection02IDFrom.EmptyRows = True
        Me.tdbcSelection02IDFrom.ExtendRightColumn = True
        Me.tdbcSelection02IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection02IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection02IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection02IDFrom.Location = New System.Drawing.Point(141, 220)
        Me.tdbcSelection02IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection02IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection02IDFrom.MaxLength = 32767
        Me.tdbcSelection02IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection02IDFrom.Name = "tdbcSelection02IDFrom"
        Me.tdbcSelection02IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection02IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection02IDFrom.Size = New System.Drawing.Size(152, 21)
        Me.tdbcSelection02IDFrom.TabIndex = 22
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
        Me.tdbcSelection01IDFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcSelection01IDFrom.ColumnWidth = 100
        Me.tdbcSelection01IDFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcSelection01IDFrom.DisplayMember = "Code"
        Me.tdbcSelection01IDFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcSelection01IDFrom.DropDownWidth = 400
        Me.tdbcSelection01IDFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcSelection01IDFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcSelection01IDFrom.EmptyRows = True
        Me.tdbcSelection01IDFrom.ExtendRightColumn = True
        Me.tdbcSelection01IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcSelection01IDFrom.Images.Add(CType(resources.GetObject("tdbcSelection01IDFrom.Images"), System.Drawing.Image))
        Me.tdbcSelection01IDFrom.Location = New System.Drawing.Point(141, 191)
        Me.tdbcSelection01IDFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcSelection01IDFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcSelection01IDFrom.MaxLength = 32767
        Me.tdbcSelection01IDFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcSelection01IDFrom.Name = "tdbcSelection01IDFrom"
        Me.tdbcSelection01IDFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcSelection01IDFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcSelection01IDFrom.Size = New System.Drawing.Size(152, 21)
        Me.tdbcSelection01IDFrom.TabIndex = 18
        Me.tdbcSelection01IDFrom.ValueMember = "Code"
        Me.tdbcSelection01IDFrom.PropBag = resources.GetString("tdbcSelection01IDFrom.PropBag")
        '
        'lblSelection01IDFrom
        '
        Me.lblSelection01IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection01IDFrom.Location = New System.Drawing.Point(12, 188)
        Me.lblSelection01IDFrom.Name = "lblSelection01IDFrom"
        Me.lblSelection01IDFrom.Size = New System.Drawing.Size(120, 30)
        Me.lblSelection01IDFrom.TabIndex = 17
        Me.lblSelection01IDFrom.Text = "Tieâu thöùc 1"
        Me.lblSelection01IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection02IDFrom
        '
        Me.lblSelection02IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection02IDFrom.Location = New System.Drawing.Point(12, 213)
        Me.lblSelection02IDFrom.Name = "lblSelection02IDFrom"
        Me.lblSelection02IDFrom.Size = New System.Drawing.Size(120, 34)
        Me.lblSelection02IDFrom.TabIndex = 21
        Me.lblSelection02IDFrom.Text = "Tieâu thöùc 2"
        Me.lblSelection02IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection03IDFrom
        '
        Me.lblSelection03IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection03IDFrom.Location = New System.Drawing.Point(12, 241)
        Me.lblSelection03IDFrom.Name = "lblSelection03IDFrom"
        Me.lblSelection03IDFrom.Size = New System.Drawing.Size(120, 34)
        Me.lblSelection03IDFrom.TabIndex = 25
        Me.lblSelection03IDFrom.Text = "Tieâu thöùc 3"
        Me.lblSelection03IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection04IDFrom
        '
        Me.lblSelection04IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection04IDFrom.Location = New System.Drawing.Point(12, 272)
        Me.lblSelection04IDFrom.Name = "lblSelection04IDFrom"
        Me.lblSelection04IDFrom.Size = New System.Drawing.Size(120, 30)
        Me.lblSelection04IDFrom.TabIndex = 29
        Me.lblSelection04IDFrom.Text = "Tieâu thöùc 4"
        Me.lblSelection04IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection05IDFrom
        '
        Me.lblSelection05IDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection05IDFrom.Location = New System.Drawing.Point(12, 299)
        Me.lblSelection05IDFrom.Name = "lblSelection05IDFrom"
        Me.lblSelection05IDFrom.Size = New System.Drawing.Size(120, 34)
        Me.lblSelection05IDFrom.TabIndex = 33
        Me.lblSelection05IDFrom.Text = "Tieâu thöùc 5"
        Me.lblSelection05IDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection01IDTo
        '
        Me.lblSelection01IDTo.AutoSize = True
        Me.lblSelection01IDTo.Location = New System.Drawing.Point(337, 195)
        Me.lblSelection01IDTo.Name = "lblSelection01IDTo"
        Me.lblSelection01IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection01IDTo.TabIndex = 19
        Me.lblSelection01IDTo.Text = "---"
        Me.lblSelection01IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection02IDTo
        '
        Me.lblSelection02IDTo.AutoSize = True
        Me.lblSelection02IDTo.Location = New System.Drawing.Point(337, 219)
        Me.lblSelection02IDTo.Name = "lblSelection02IDTo"
        Me.lblSelection02IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection02IDTo.TabIndex = 23
        Me.lblSelection02IDTo.Text = "---"
        Me.lblSelection02IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection03IDTo
        '
        Me.lblSelection03IDTo.AutoSize = True
        Me.lblSelection03IDTo.Location = New System.Drawing.Point(337, 250)
        Me.lblSelection03IDTo.Name = "lblSelection03IDTo"
        Me.lblSelection03IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection03IDTo.TabIndex = 27
        Me.lblSelection03IDTo.Text = "---"
        Me.lblSelection03IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection05IDTo
        '
        Me.lblSelection05IDTo.AutoSize = True
        Me.lblSelection05IDTo.Location = New System.Drawing.Point(337, 308)
        Me.lblSelection05IDTo.Name = "lblSelection05IDTo"
        Me.lblSelection05IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection05IDTo.TabIndex = 35
        Me.lblSelection05IDTo.Text = "---"
        Me.lblSelection05IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelection04IDTo
        '
        Me.lblSelection04IDTo.AutoSize = True
        Me.lblSelection04IDTo.Location = New System.Drawing.Point(337, 279)
        Me.lblSelection04IDTo.Name = "lblSelection04IDTo"
        Me.lblSelection04IDTo.Size = New System.Drawing.Size(16, 13)
        Me.lblSelection04IDTo.TabIndex = 31
        Me.lblSelection04IDTo.Text = "---"
        Me.lblSelection04IDTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpTimeInfo
        '
        Me.grpTimeInfo.Location = New System.Drawing.Point(84, 416)
        Me.grpTimeInfo.Name = "grpTimeInfo"
        Me.grpTimeInfo.Size = New System.Drawing.Size(480, 2)
        Me.grpTimeInfo.TabIndex = 42
        Me.grpTimeInfo.TabStop = False
        Me.grpTimeInfo.Text = "Thời gian"
        '
        'c1dateDateTo
        '
        Me.c1dateDateTo.AutoSize = False
        Me.c1dateDateTo.CustomFormat = "dd/MM/yyyy"
        Me.c1dateDateTo.EmptyAsNull = True
        Me.c1dateDateTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.c1dateDateTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.c1dateDateTo.Location = New System.Drawing.Point(7, 34)
        Me.c1dateDateTo.Name = "c1dateDateTo"
        Me.c1dateDateTo.Size = New System.Drawing.Size(166, 22)
        Me.c1dateDateTo.TabIndex = 51
        Me.c1dateDateTo.Tag = Nothing
        Me.c1dateDateTo.TrimStart = True
        Me.c1dateDateTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        '
        'c1dateDateFrom
        '
        Me.c1dateDateFrom.AutoSize = False
        Me.c1dateDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.c1dateDateFrom.EmptyAsNull = True
        Me.c1dateDateFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.c1dateDateFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.c1dateDateFrom.Location = New System.Drawing.Point(7, 34)
        Me.c1dateDateFrom.Name = "c1dateDateFrom"
        Me.c1dateDateFrom.Size = New System.Drawing.Size(152, 22)
        Me.c1dateDateFrom.TabIndex = 49
        Me.c1dateDateFrom.Tag = Nothing
        Me.c1dateDateFrom.TrimStart = True
        Me.c1dateDateFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        '
        'optDate
        '
        Me.optDate.AutoSize = True
        Me.optDate.Location = New System.Drawing.Point(42, 489)
        Me.optDate.Name = "optDate"
        Me.optDate.Size = New System.Drawing.Size(50, 17)
        Me.optDate.TabIndex = 48
        Me.optDate.TabStop = True
        Me.optDate.Text = "Ngày"
        Me.optDate.UseVisualStyleBackColor = True
        '
        'optPeriod
        '
        Me.optPeriod.AutoSize = True
        Me.optPeriod.Checked = True
        Me.optPeriod.Location = New System.Drawing.Point(42, 455)
        Me.optPeriod.Name = "optPeriod"
        Me.optPeriod.Size = New System.Drawing.Size(37, 17)
        Me.optPeriod.TabIndex = 44
        Me.optPeriod.TabStop = True
        Me.optPeriod.Text = "Kỳ"
        Me.optPeriod.UseVisualStyleBackColor = True
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
        Me.tdbcPeriodTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
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
        Me.tdbcPeriodTo.Location = New System.Drawing.Point(7, 4)
        Me.tdbcPeriodTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcPeriodTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcPeriodTo.MaxLength = 32767
        Me.tdbcPeriodTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcPeriodTo.Name = "tdbcPeriodTo"
        Me.tdbcPeriodTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcPeriodTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcPeriodTo.Size = New System.Drawing.Size(166, 21)
        Me.tdbcPeriodTo.TabIndex = 47
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
        Me.tdbcPeriodFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
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
        Me.tdbcPeriodFrom.Location = New System.Drawing.Point(7, 3)
        Me.tdbcPeriodFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcPeriodFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcPeriodFrom.MaxLength = 32767
        Me.tdbcPeriodFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcPeriodFrom.Name = "tdbcPeriodFrom"
        Me.tdbcPeriodFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcPeriodFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcPeriodFrom.Size = New System.Drawing.Size(152, 21)
        Me.tdbcPeriodFrom.TabIndex = 45
        Me.tdbcPeriodFrom.ValueMember = "Period"
        Me.tdbcPeriodFrom.PropBag = resources.GetString("tdbcPeriodFrom.PropBag")
        '
        'lblPeriodTo
        '
        Me.lblPeriodTo.AutoSize = True
        Me.lblPeriodTo.Location = New System.Drawing.Point(165, 8)
        Me.lblPeriodTo.Name = "lblPeriodTo"
        Me.lblPeriodTo.Size = New System.Drawing.Size(16, 13)
        Me.lblPeriodTo.TabIndex = 46
        Me.lblPeriodTo.Text = "---"
        Me.lblPeriodTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblteDateTo
        '
        Me.lblteDateTo.AutoSize = True
        Me.lblteDateTo.Location = New System.Drawing.Point(165, 40)
        Me.lblteDateTo.Name = "lblteDateTo"
        Me.lblteDateTo.Size = New System.Drawing.Size(16, 13)
        Me.lblteDateTo.TabIndex = 50
        Me.lblteDateTo.Text = "---"
        Me.lblteDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkExceptSetup
        '
        Me.chkExceptSetup.AutoSize = True
        Me.chkExceptSetup.Location = New System.Drawing.Point(12, 335)
        Me.chkExceptSetup.Name = "chkExceptSetup"
        Me.chkExceptSetup.Size = New System.Drawing.Size(245, 17)
        Me.chkExceptSetup.TabIndex = 37
        Me.chkExceptSetup.Text = "Không hiển thị những mã XDCB đã hình thành"
        Me.chkExceptSetup.UseVisualStyleBackColor = True
        '
        'chkExecptVoucherSetup
        '
        Me.chkExecptVoucherSetup.AutoSize = True
        Me.chkExecptVoucherSetup.Location = New System.Drawing.Point(12, 358)
        Me.chkExecptVoucherSetup.Name = "chkExecptVoucherSetup"
        Me.chkExecptVoucherSetup.Size = New System.Drawing.Size(247, 17)
        Me.chkExecptVoucherSetup.TabIndex = 39
        Me.chkExecptVoucherSetup.Text = "Không hiển thị các chứng từ hình thành tài sản"
        Me.chkExecptVoucherSetup.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(430, 524)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 22)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "&In"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(512, 524)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Đó&ng"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.chkNotCIPFinalization)
        Me.grpMain.Controls.Add(Me.SplitContainer1)
        Me.grpMain.Controls.Add(Me.chkCheckTime)
        Me.grpMain.Controls.Add(Me.chkIsVoucherNotCIP)
        Me.grpMain.Controls.Add(Me.chkNotCompleteSplit)
        Me.grpMain.Controls.Add(Me.tdbcSelection01IDFrom)
        Me.grpMain.Controls.Add(Me.tdbcSelection02IDFrom)
        Me.grpMain.Controls.Add(Me.lblTimeInfo)
        Me.grpMain.Controls.Add(Me.tdbcSelection04IDTo)
        Me.grpMain.Controls.Add(Me.lblGroup)
        Me.grpMain.Controls.Add(Me.optDate)
        Me.grpMain.Controls.Add(Me.lblReport)
        Me.grpMain.Controls.Add(Me.tdbcSelection05IDTo)
        Me.grpMain.Controls.Add(Me.optPeriod)
        Me.grpMain.Controls.Add(Me.txtNotes)
        Me.grpMain.Controls.Add(Me.lblSelection02IDFrom)
        Me.grpMain.Controls.Add(Me.lblDivision)
        Me.grpMain.Controls.Add(Me.tdbcSelection03IDTo)
        Me.grpMain.Controls.Add(Me.txtReportName2)
        Me.grpMain.Controls.Add(Me.lblSelection04IDTo)
        Me.grpMain.Controls.Add(Me.tdbcDivisionID)
        Me.grpMain.Controls.Add(Me.tdbcSelection02IDTo)
        Me.grpMain.Controls.Add(Me.tdbcReportID)
        Me.grpMain.Controls.Add(Me.lblSelection05IDTo)
        Me.grpMain.Controls.Add(Me.lblReportID)
        Me.grpMain.Controls.Add(Me.tdbcSelection01IDTo)
        Me.grpMain.Controls.Add(Me.lblDivisionID)
        Me.grpMain.Controls.Add(Me.lblSelection03IDTo)
        Me.grpMain.Controls.Add(Me.txtReportName)
        Me.grpMain.Controls.Add(Me.tdbcSelection05IDFrom)
        Me.grpMain.Controls.Add(Me.grpTimeInfo)
        Me.grpMain.Controls.Add(Me.lblSelection02IDTo)
        Me.grpMain.Controls.Add(Me.lblReEportName2)
        Me.grpMain.Controls.Add(Me.tdbcSelection04IDFrom)
        Me.grpMain.Controls.Add(Me.txtDivisionName)
        Me.grpMain.Controls.Add(Me.lblSelection01IDTo)
        Me.grpMain.Controls.Add(Me.lblNotes)
        Me.grpMain.Controls.Add(Me.tdbcSelection03IDFrom)
        Me.grpMain.Controls.Add(Me.grpDivision)
        Me.grpMain.Controls.Add(Me.lblSelection05IDFrom)
        Me.grpMain.Controls.Add(Me.grpGroup)
        Me.grpMain.Controls.Add(Me.lblSelection04IDFrom)
        Me.grpMain.Controls.Add(Me.grpReport)
        Me.grpMain.Controls.Add(Me.chkExceptSetup)
        Me.grpMain.Controls.Add(Me.lblSelection03IDFrom)
        Me.grpMain.Controls.Add(Me.lblSelection01IDFrom)
        Me.grpMain.Controls.Add(Me.chkExecptVoucherSetup)
        Me.grpMain.Location = New System.Drawing.Point(6, -1)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(581, 519)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(130, 450)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.tdbcPeriodFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPeriodTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblteDateTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.c1dateDateFrom)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tdbcPeriodTo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.c1dateDateTo)
        Me.SplitContainer1.Size = New System.Drawing.Size(370, 63)
        Me.SplitContainer1.SplitterDistance = 185
        Me.SplitContainer1.TabIndex = 3
        '
        'chkCheckTime
        '
        Me.chkCheckTime.AutoSize = True
        Me.chkCheckTime.Location = New System.Drawing.Point(22, 430)
        Me.chkCheckTime.Name = "chkCheckTime"
        Me.chkCheckTime.Size = New System.Drawing.Size(70, 17)
        Me.chkCheckTime.TabIndex = 43
        Me.chkCheckTime.Text = "Thời gian"
        Me.chkCheckTime.UseVisualStyleBackColor = True
        '
        'chkIsVoucherNotCIP
        '
        Me.chkIsVoucherNotCIP.AutoSize = True
        Me.chkIsVoucherNotCIP.Location = New System.Drawing.Point(315, 335)
        Me.chkIsVoucherNotCIP.Name = "chkIsVoucherNotCIP"
        Me.chkIsVoucherNotCIP.Size = New System.Drawing.Size(227, 17)
        Me.chkIsVoucherNotCIP.TabIndex = 38
        Me.chkIsVoucherNotCIP.Text = "Hiển thị những phiếu chưa nhập mã XDCB"
        Me.chkIsVoucherNotCIP.UseVisualStyleBackColor = True
        '
        'chkNotCompleteSplit
        '
        Me.chkNotCompleteSplit.AutoSize = True
        Me.chkNotCompleteSplit.Location = New System.Drawing.Point(12, 381)
        Me.chkNotCompleteSplit.Name = "chkNotCompleteSplit"
        Me.chkNotCompleteSplit.Size = New System.Drawing.Size(234, 17)
        Me.chkNotCompleteSplit.TabIndex = 40
        Me.chkNotCompleteSplit.Text = "Không hiển thị những mã XDCB đã tách hết"
        Me.chkNotCompleteSplit.UseVisualStyleBackColor = True
        '
        'lblTimeInfo
        '
        Me.lblTimeInfo.AutoSize = True
        Me.lblTimeInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeInfo.Location = New System.Drawing.Point(12, 408)
        Me.lblTimeInfo.Name = "lblTimeInfo"
        Me.lblTimeInfo.Size = New System.Drawing.Size(75, 13)
        Me.lblTimeInfo.TabIndex = 41
        Me.lblTimeInfo.Text = "4. Thời gian"
        Me.lblTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGroup
        '
        Me.lblGroup.AutoSize = True
        Me.lblGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroup.Location = New System.Drawing.Point(12, 172)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(97, 13)
        Me.lblGroup.TabIndex = 15
        Me.lblGroup.Text = "3. Tiêu thức lọc"
        Me.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReport
        '
        Me.lblReport.AutoSize = True
        Me.lblReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReport.Location = New System.Drawing.Point(12, 67)
        Me.lblReport.Name = "lblReport"
        Me.lblReport.Size = New System.Drawing.Size(69, 13)
        Me.lblReport.TabIndex = 5
        Me.lblReport.Text = "2. Báo cáo"
        Me.lblReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = True
        Me.lblDivision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.Location = New System.Drawing.Point(12, 18)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(59, 13)
        Me.lblDivision.TabIndex = 0
        Me.lblDivision.Text = "1. Đơn vị"
        Me.lblDivision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkNotCIPFinalization
        '
        Me.chkNotCIPFinalization.AutoSize = True
        Me.chkNotCIPFinalization.Location = New System.Drawing.Point(315, 358)
        Me.chkNotCIPFinalization.Name = "chkNotCIPFinalization"
        Me.chkNotCIPFinalization.Size = New System.Drawing.Size(263, 17)
        Me.chkNotCIPFinalization.TabIndex = 49
        Me.chkNotCIPFinalization.Text = "Không hiển thị những mã XDCB đã quyết toán hết"
        Me.chkNotCIPFinalization.UseVisualStyleBackColor = True
        '
        'D02F5005
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 553)
        Me.Controls.Add(Me.grpMain)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "D02F5005"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BÀo cÀo chi phÛ XDCB dê dang - D02F5005"
        CType(Me.tdbcDivisionID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcReportID, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.c1dateDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1dateDateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcPeriodTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcPeriodFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
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
    Private WithEvents lblPeriodTo As System.Windows.Forms.Label
    Private WithEvents chkExceptSetup As System.Windows.Forms.CheckBox
    Private WithEvents chkExecptVoucherSetup As System.Windows.Forms.CheckBox
    Private WithEvents btnPrint As System.Windows.Forms.Button
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents optDate As System.Windows.Forms.RadioButton
    Private WithEvents optPeriod As System.Windows.Forms.RadioButton
    Private WithEvents c1dateDateFrom As C1.Win.C1Input.C1DateEdit
    Private WithEvents c1dateDateTo As C1.Win.C1Input.C1DateEdit
    Private WithEvents lblteDateTo As System.Windows.Forms.Label
    Private WithEvents grpMain As System.Windows.Forms.GroupBox
    Private WithEvents lblReport As System.Windows.Forms.Label
    Private WithEvents lblDivision As System.Windows.Forms.Label
    Private WithEvents lblTimeInfo As System.Windows.Forms.Label
    Private WithEvents lblGroup As System.Windows.Forms.Label
    Private WithEvents chkNotCompleteSplit As System.Windows.Forms.CheckBox
    Private WithEvents chkIsVoucherNotCIP As System.Windows.Forms.CheckBox
    Private WithEvents chkCheckTime As System.Windows.Forms.CheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents chkNotCIPFinalization As System.Windows.Forms.CheckBox
End Class