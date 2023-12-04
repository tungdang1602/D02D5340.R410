Imports System
Public Class D91F6020

    Private WithEvents backgroundWorker1 As System.ComponentModel.BackgroundWorker
    Private ChildName As String = "D91E0240"
    Dim exe As D91E0240
    Dim p As System.Diagnostics.Process

    Private _sSQLSelection As String 'Câu SQL để load lên lưới khi tìm kiếm theo Tiêu thức của báo cáo
    Public Property SQLSelection() As String
        Get
            Return _sSQLSelection
        End Get
        Set(ByVal Value As String)
            _sSQLSelection = Value
        End Set
    End Property


    Private _formPermision As String
    Public WriteOnly Property FormPermision() As String
        Set(ByVal Value As String)
            _formPermision = Value
        End Set
    End Property

    Private _outPut01 As String ' Kết quả tìm kiếm trả về
    Public ReadOnly Property OutPut01() As String
        Get
            Return _outPut01
        End Get
    End Property

    Private _modeSelect As String = "0"
    Public WriteOnly Property ModeSelect() As String         
        Set(ByVal Value As String )
            _modeSelect = Value
        End Set
    End Property

    Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles backgroundWorker1.DoWork
        'Tạo một process gắn với exe con, process này sẽ quan sát exe con.
        Dim p As System.Diagnostics.Process
        Try
            p = Process.GetProcessesByName(ChildName)(0)
            If p Is Nothing Then
                Me.Close()
                Exit Sub
            End If
            p.EnableRaisingEvents = True
            p.WaitForExit()
        Catch ex As Exception
            Me.Close()
        End Try

    End Sub

    Private Sub FormLock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Ẩn form trung gian
        Me.Size = New Size(0, 0)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        '----Truyền tham số exe con------
        exe = New D91E0240(gsServer, gsCompanyID, gsConnectionUser, gsPassword, gsUserID, IIf(geLanguage = EnumLanguage.Vietnamese, "0", "10000").ToString)
        exe.FormActive = D91E0240Form.D91F6020
        exe.SQLSelection = _sSQLSelection
        exe.ModeSelect = _modeSelect
        exe.FormPermision = _formPermision
        exe.Run()

        'Bắt đầu chạy cơ chế background
        backgroundWorker1 = New System.ComponentModel.BackgroundWorker
        backgroundWorker1.RunWorkerAsync()
    End Sub

    'sự kiện hoàn thành và dừng của Background
    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backgroundWorker1.RunWorkerCompleted
        _outPut01 = exe.Output01
        Me.Close()
    End Sub

End Class