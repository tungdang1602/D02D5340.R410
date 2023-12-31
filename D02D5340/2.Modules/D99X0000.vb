'#######################################################################################
'#                                     CHÚ Ý (Áp dụng Unicode)
'#--------------------------------------------------------------------------------------
'# Không được thay đổi bất cứ dòng code này trong module này, nếu muốn thay đổi bạn phải
'# liên lạc với Trưởng nhóm để được giải quyết.
'# Ngày cập nhật cuối cùng: 06/01/2014
'# Người cập nhật cuối cùng: Minh Hòa
'# Bổ sung WITH (NOLOCK) vào table, trong bảng D91T0000 23/9/2013
'# Bổ sung Filter theo MM/dd/yyyy HH:mm:ss 27/9/2013
'# Sửa hàm FilterChangeGrid 01/10/2013
'# Sửa hàm LoadTDBCStandardReport, LoadTDBCCustomizeReport 15/10/2013
'# Tách hàm ReturnTableStandardReportNew 28/10/2013
'# Sửa hàm HotKeyF8: không copy cột có Expression 4/11/2013
'# Sửa hàm CheckStore: cho xóa cùng lúc nhiều phiếu import: 11/12/2013
'# Sửa hàm Load Tài khoản và Khoản mục: bổ sung phân quyền Đơn vị: 12/12/2013
'# Sửa hàm CheckObligatoryParameter: bổ sung nếu vi phạm thì Xóa các tham số Registry trong exe con
'# Bỏ chuỗi kiểm tra trong hàm load Tài khoản và Khoản mục (And (StrDivisionID ...))
'#######################################################################################

Imports System.Threading
Imports System.Globalization
Imports System
Imports System.IO

#Region "Khai báo Enum"
''' <summary>
''' Enum miêu tả các kiểu dữ liệu số được sử dụng dưới SQL
''' </summary>
Public Enum EnumDataType
    ''' <summary>
    ''' Kiểu dữ liệu chuỗi nhưng chỉ nhập số VD: số điện thoại, Số CMND, ...
    ''' </summary>
    ''' <remarks></remarks>
    DataString = 0
    ''' <summary>
    ''' Kiểu dữ liệu TinyInt
    ''' </summary>
    TinyInt = 1
    ''' <summary>
    ''' Kiểu dữ liệu Int
    ''' </summary>
    Int = 2
    ''' <summary>
    ''' Kiểu dữ liệu Money
    ''' </summary>
    Money = 3
    ''' <summary>
    ''' Kiểu dữ liệu SmallMoney
    ''' </summary>
    SmallMoney = 4
    ''' <summary>
    ''' Kiểu dữ liệu Decimal(28,8)
    ''' </summary>
    Number = 5
End Enum

''' <summary>
''' Enum miêu tả trạng thái của ngôn ngữ hiện tại
''' </summary>
Public Enum EnumLanguage
    ''' <summary>
    ''' Ngôn ngữ đang dùng: Tiếng Việt
    ''' </summary>
    Vietnamese = 0
    ''' <summary>
    ''' Ngôn ngữ đang dùng: Tiếng Anh
    ''' </summary>
    English = 10000
    ''' <summary>
    ''' Ngôn ngữ đang dùng: Tiếng Pháp (chưa sử dụng)
    ''' </summary>
    French = 20000
    ''' <summary>
    ''' Ngôn ngữ đang dùng: Tiếng Trung Quốc (chưa sử dụng)
    ''' </summary>
    Chinese = 30000
    ''' <summary>
    ''' Ngôn ngữ đang dùng: Tiếng Nhật (chưa sử dụng)
    ''' </summary>
    Japanese = 40000
End Enum

''' <summary>
''' Enum miêu tả trạng thái của form
''' </summary>
Public Enum EnumFormState
    ''' <summary>
    ''' Form ở trạng thái: Thêm mới
    ''' </summary>
    FormAdd = 0
    ''' <summary>
    ''' Form ở trạng thái: Sửa
    ''' </summary>
    FormEdit = 1
    ''' <summary>
    ''' Form ở trạng thái: Sửa khác
    ''' </summary>
    FormEditOther = 2
    ''' <summary>
    ''' Form ở trạng thái: Xem
    ''' </summary>
    FormView = 3
    ''' <summary>
    ''' Form ở trạng thái: Thêm hóa đơn
    ''' </summary>
    FormAddInvoice = 4
    ''' <summary>
    ''' Form ở trạng thái: Thêm phiếu
    ''' </summary>
    FormAddVoucher = 5
    ''' <summary>
    ''' Form ở trạng thái: Sửa hóa đơn
    ''' </summary>
    FormEditInvoice = 6
    ''' <summary>
    ''' Form ở trạng thái: Sửa phiếu
    ''' </summary>
    FormEditVoucher = 7
    ''' <summary>
    ''' Form ở trạng thái: Copy
    ''' </summary>
    FormCopy = 8
    ''' <summary>
    ''' Form ở trạng thái: Khác
    ''' </summary>
    FormOther = 99
End Enum

''' <summary>
''' Enum miêu tả trạng thái phân quyền của form
''' </summary>
Public Enum EnumPermission
    ''' <summary>
    ''' Chưa có quyền
    ''' </summary>
    None = 0
    ''' <summary>
    ''' Quyền: Xem
    ''' </summary>
    View = 1
    ''' <summary>
    ''' Quyền: Xem, Thêm mới
    ''' </summary>
    Add = 2
    ''' <summary>
    ''' Quyền: Xem, Thêm mới, Sửa
    ''' </summary>
    EditAdd = 3
    ''' <summary>
    ''' Quyền: Xem, Thêm mới, Sửa, Xóa
    ''' </summary>
    DeleteEditAdd = 4
End Enum

''' <summary>
''' Enum miêu tả kiểm tra phím nhấn
''' </summary>
Public Enum EnumKey
    ''' <summary>
    ''' Kiểm tra các số: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
    ''' </summary>
    Number = 0
    ''' <summary>
    ''' Kiểm tra các số: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 và dấu chấm (.)
    ''' </summary>
    NumberDot = 1
    ''' <summary>
    ''' Kiểm tra các số: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 và dấu trừ (-)
    ''' </summary>
    NumberSign = 2
    ''' <summary>
    ''' Kiểm tra các số: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, dấu chấm (.) và dấu trừ (-)
    ''' </summary>
    NumberDotSign = 3
    ''' <summary>
    ''' Kiểm tra có trong chuỗi truyền vào
    ''' </summary>
    Custom = 4

End Enum
#End Region

''' <summary>
''' Module này liên quan đến các biến toàn cục, hàm toàn cục.
''' </summary>
''' 
Module D99X0000

#Region "Khai báo hằng toàn cục"

    ''' <summary>
    ''' Dấu phẩy ", " dùng cho các lệnh SQL
    ''' </summary>
    Public Const COMMA As String = ", "
    ''' <summary>
    ''' Split thứ 0
    ''' </summary>
    Public Const SPLIT0 As Integer = 0
    ''' <summary>
    ''' Split thứ 1
    ''' </summary>
    Public Const SPLIT1 As Integer = 1
    ''' <summary>
    ''' Split thứ 2
    ''' </summary>
    Public Const SPLIT2 As Integer = 2
    ''' <summary>
    ''' Split thứ 3
    ''' </summary>
    Public Const SPLIT3 As Integer = 3
    ''' <summary>
    ''' Màu BackColor theo chuẩn của Diginet
    ''' </summary>
    Public Const COLOR_BACKCOLOR As Long = &HE0E0E0

    ''' <summary>
    ''' Hằng số mặc định cho ngày là __/__/____
    ''' </summary>
    Public Const MaskFormatDate As String = "__/__/____"
    Public Const MaskFormatDateShort As String = "  /  /"

    ''' <summary>
    ''' Dùng cắt chuỗi Tìm kiếm
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LM_DLM As String = "@@"

    Public Const MaxMoney As Object = 922337203685470
    Public Const MinMoney As Object = -922337203685470
    Public Const MaxSmallMoney As Object = 214748 '214748.3647
    Public Const MaxInt As Object = 2147483647
    Public Const MaxTinyInt As Object = 255
    Public Const MaxDecimal As Decimal = 999999999999999999D 'Dùng để kiểm tra Max Decimal(28,8) dưới SQL Server
#End Region

#Region "Khai báo biến toàn cục"

    ''' <summary>
    ''' Có module Admin không, 1: có, 0: không
    ''' </summary>
    ''' <remarks></remarks>
    Public giModuleAdmin As Int16
    ''' <summary>
    ''' Đơn vị hiện tại
    ''' </summary>
    Public gsDivisionID As String = ""
    ''' <summary>
    ''' Tháng kế toán hiện tại
    ''' </summary>
    Public giTranMonth As Integer = 0
    ''' <summary>
    ''' Năm kế toán hiện tại
    ''' </summary>
    Public giTranYear As Integer = 0
    ''' <summary>
    ''' User hiện tại đang login vào hệ thống Lemon3
    ''' </summary>
    Public gsUserID As String
    ''' <summary>
    ''' Database hiện tại đang dùng
    ''' </summary>
    Public gsCompanyID As String
    ''' <summary>
    ''' User kết nối vào Server đang dùng
    ''' </summary>
    Public gsConnectionUser As String
    ''' <summary>
    ''' Chuỗi kết nối toàn cục đến Server chính
    ''' </summary>
    Public gsConnectionString As String
    ''' <summary>
    ''' Chuỗi kết nối toàn cục đến Server Ứng dụng
    ''' </summary>
    Public gsConnectionStringApp As String
    Public gsServerApp As String 'Dùng để link server dưới SQL
    Public gsCompanyIDApp As String
    ''' <summary>
    ''' Chuỗi kết nối toàn cục đến Server Báo cáo
    ''' </summary>
    Public gsConnectionStringReport As String
    Public gsServerReport As String 'Dùng để link server dưới SQL
    Public gsCompanyIDReport As String

    ''' <summary>
    ''' Server đang dùng
    ''' </summary>
    Public gsServer As String
    
    ''' <summary>
    ''' Password kết nối đến Server
    ''' </summary>
    Public gsPassword As String
    ''' <summary>
    ''' Enum ngôn ngữ hiện tại
    ''' </summary>
    Public geLanguage As EnumLanguage
    ''' <summary>
    ''' Chuỗi '01' hoặc '84' miêu tả ngôn ngữ hiện tại
    ''' </summary>
    Public gsLanguage As String
    ''' <summary>
    ''' = True nếu đã khóa sổ, ngược lại chưa khóa sổ
    ''' </summary>
    Public gbClosed As Boolean
    ''' <summary>
    ''' Biến dùng cho việc sinh khóa IGE
    ''' </summary>
    Public gsStringKey As String = ""
    ''' <summary>
    ''' Đường dẫn file help toàn cục
    ''' </summary>
    Public gsHelpFileName As String
    ''' <summary>
    ''' SqlConnection toàn cục kết nối đến hệ thống
    ''' </summary>
    Public gConn As SqlConnection
    ''' <summary>
    ''' Biến lưu trữ màu sắc toàn cục cho module
    ''' </summary>
    Public OptSettings As lmOptions
    ''' <summary>
    ''' Dùng cho việc sinh IGE
    ''' </summary>
    Public gnLastKey As Long
    ''' <summary>
    ''' Dùng cho việc sinh IGE (Số phiếu khi người dùng click vào chìa khóa)
    ''' </summary>
    Public gnNewLastKey As Long
    ''' <summary>
    ''' Biến lưu Đơn vị hiện tại có bị khóa không
    ''' </summary>
    Public gbLockedDivisionID As Boolean
    ''' <summary>
    ''' Biến lưu giá trị tìm kiếm lần đầu tiên
    ''' </summary>
    Public gbEnabledUseFind As Boolean
    ''' <summary>
    ''' Dùng để kiểm tra 10 khoản mục theo thiết lập D91
    ''' </summary>
    ''' <remarks></remarks>
    Public gbArrAnaValidate(9) As Boolean
    ''' <summary>
    ''' Dùng để kiểm tra chiều dài 10 khoản mục
    ''' </summary>
    ''' <remarks></remarks>
    Public giArrAnaLength(9) As Integer
    ''' <summary>
    ''' Dùng để kiểm tra Khoản mục có được sử dụng ở D91 không
    ''' </summary>
    ''' <remarks></remarks>
    Public gbArrAnaVisiable(9) As Boolean
    Public gsArrAnaCategoryName(9) As String 'Dung luu AnaCategoryName de goi D91F1302
    ''' <summary>
    ''' Dùng để lưu lại tên control khi nhấn phím F11
    ''' </summary>-
    ''' <remarks></remarks>
    Public gcControl As Control
    Public gsControlName As String = ""
    Public gsControlNameParent As String = ""
    Public giControlIndex As Int32 = 0

    '----------------------------------------------------
    ''' <summary>
    ''' Màu nền của Control bắt buộc nhập
    ''' </summary>
    ''' <remarks></remarks>
    Public COLOR_BACKCOLOROBLIGATORY As System.Drawing.Color = Color.Beige
    'Public COLOR_BACKCOLOROBLIGATORY As Integer = Convert.ToInt32("F5F5DC", 16) '&F5F5DC --> Color.Beige
    '----------------------------------------------------

    Public MsgAnnouncement As String
    Public AllCode As String
    Public AllName As String
    Public NewCode As String
    Public NewName As String

    ''' <summary> 
    ''' Dùng để kiểm tra Quy cách có được sử dụng ở D07 không
    ''' </summary> 
    ''' <remarks></remarks> 
    Public gbArrSpecVisiable(9) As Boolean

    ''' <summary>
    ''' Mảng tìm kiếm
    ''' </summary>
    ''' <remarks></remarks>
    Public arrAdvanced() As String
    Public xSortOrder(,) As String
    Public strSQLAdvanced As String = ""
    Public strSQLAdvancedForReport As String = "" ' Tìm kiếm client nhưng trả ra giá trị cho báo cáo tìm kiếm server
    Public blnOrder As Boolean
    Public lsValueListFind As List(Of String)

    Public gbInputbyUnicode As Boolean = False 'Kiểm tra xem có sử dụng Unicode 
    Public gsCreateBy As String = "" 'Lấy giá trị mặc định của Người tạo
    Public gbLockL3UserID As Boolean = False
    Public gbIsDAGroup As Boolean = False
    Public giNumberOfPrint As Integer = 0 'Đếm số lần in ra giấy
    Public gsGetDate As String = "" 'Lấy giá trị GetDate() dưới Server
    Private dtPer As DataTable ' Bảng dữ liệu chứa phân quyền của các màn hình
    Private sMyModuleID As String 'Kiểm tra phân quyền của màn hình khác màn hình của module gốc hiện tại
    Public iSizeFont As Single = 8.25 ' Font chuẩn của màn hình 1024 x 768

    'Minh Hòa Update 10/08/2012: Khai báo thời gian Timeout của connection
    Public gsConnectionTimeout As String = "Connection Timeout = 0"
    Public gsConnectionTimeout15 As String = "Connection Timeout = 15"
    Public gsConnectionTimeout60 As String = "Connection Timeout = 60"

    
#Region "Lưu trữ Resource Custom"
    Public gbResourceError As Boolean = False 'Cờ bật nếu chưa có mã ResourceID trong file Resource thì chỉ thông báo 1 lần
    Public gsFileResCustom As String = Application.StartupPath & "\ResourceCustom.xml"
    Public dtCustom As DataTable = Nothing
    Public giReplacResource As Integer = -1 'Cờ kiểm tra có thay thế tên resource không
#End Region


#End Region

#Region "Khai báo biến toàn cục liên quan đến Webservice"

    ''' <summary>
    ''' Class của WebService
    ''' </summary>
    ''' <remarks></remarks>
    Public CallWebService As New D99D0041.WS.Service ' Dùng D99D0041 mới
    ''' <summary>
    ''' Thời gian TimeOut để connect WebService
    ''' </summary>
    ''' <remarks></remarks>
    Public Const nWSTimeOut As Integer = 10000000
    ''' <summary>
    ''' Mode sử dụng 
    ''' 0: Trực tiếp; 1: Online
    ''' </summary>
    ''' <remarks></remarks>
    Public giAppMode As Integer = 0
    ''' <summary>
    ''' Đường dẫn đến máy WebService
    ''' </summary>
    ''' <remarks></remarks>
    Public gsAppServer As String

    ''' <summary>
    ''' 5 thông số của WebService
    ''' </summary>
    ''' <remarks></remarks>
    Public gsWSSPara01 As String = ""
    Public gsWSSPara02 As String = ""
    Public gsWSSPara03 As String = ""
    Public gsWSSPara04 As String = ""
    Public gsWSSPara05 As String = ""
#End Region

#Region "Public Sub and Function"

#Region "Kiểm tra thay đổi tên resource"

    Public Function ReplaceResourceCustom(ByVal sResourceName As String, Optional ByVal bAllFor As Boolean = False) As String
        If giReplacResource = -1 Then
            If CheckReplaceResourceCustom() Then
                giReplacResource = 1
            Else
                giReplacResource = 0
            End If
        End If

        If giReplacResource = 1 Then
            Return DoneReplacResource(sResourceName, bAllFor)
        End If

        Return sResourceName

    End Function


    Private Function CheckReplaceResourceCustom() As Boolean
        'Chỉ áp dụng cho module thuộc nhóm G4, G0, D89
        Dim bFlag As Boolean = False
        Dim sModuleID As String = My.Application.Info.AssemblyName.Substring(0, 3)

        If sModuleID = "D89" Then
            bFlag = True
        Else
            'Có phải module nhóm G4 không
            Dim dtModule As DataTable
            dtModule = ReturnDataTable("select ModuleID from LEMONSYS.dbo.D00T0130 WITH(NOLOCK) where ModuleGroup ='G4' Or ModuleGroup ='G0'")
            bFlag = dtModule.Select("ModuleID=" & SQLString(sModuleID)).Length > 0
            dtModule.Dispose()
        End If

        If bFlag Then
            'Có thiết lập thay đổi tên resource hay không
            If ExistRecord("Select IsCustomResource From D09T0000 WITH(NOLOCK) where IsCustomResource =1") Then
                Return True 'Có
            End If
        End If
        Return False 'Không

    End Function

    Private Function DoneReplacResource(ByVal sResourceName As String, Optional ByVal bAllFor As Boolean = False) As String
        If Not (File.Exists(gsFileResCustom)) Then
            CreateFileXML()
        End If

        If dtCustom Is Nothing Then
            dtCustom = ReadFileXML()
        End If

        Dim sResullt As String = sResourceName
        For Each dr1 As DataRow In dtCustom.Rows
            Dim sLemon3 As String = dr1.Item("Lemon3Name" & IIf(geLanguage = EnumLanguage.Vietnamese, "", "01").ToString).ToString
            If sResourceName.ToLower.Contains(sLemon3.ToLower) Then
                Dim sCustom As String = dr1.Item("CustomName" & IIf(geLanguage = EnumLanguage.Vietnamese, "", "01").ToString).ToString

                sResullt = sResullt.Replace(sLemon3, sCustom) ' sResourceName.Replace(sLemon3, sCustom)
                If sResullt.EndsWith("NV") Then
                    sResullt = sResullt.Substring(0, sResullt.Length - 2) & "nv"
                End If
                sResullt = sResullt.Replace(sLemon3.ToLower, sCustom.ToLower)
                sResullt = sResullt.Replace(sLemon3.ToUpper, sCustom.ToUpper)
                sResullt = sResullt.Replace(sLemon3.Substring(0, 1).ToUpper & sLemon3.Substring(1, sLemon3.Length - 1), sCustom.Substring(0, 1).ToUpper & sCustom.Substring(1, sCustom.Length - 1))
                If bAllFor = False Then Exit For
            End If
        Next

        Return sResullt
    End Function

#Region "Tạo và đọc file XML"

    Private Sub CreateFileXML()
        dtCustom = ReturnDataTable("select Lemon3Name, CustomName, Lemon3Name01, CustomName01 from D09T1000 WITH(NOLOCK) ")

        If dtCustom.Rows.Count > 0 Then
            Dim filew As New FileStream(gsFileResCustom, FileMode.CreateNew)
            dtCustom.WriteXml(filew)
            filew.Flush()
        Else
            giReplacResource = 0
        End If

    End Sub

    Private Function ReadFileXML() As DataTable
        If (File.Exists(gsFileResCustom)) Then
            Dim filew1 As New FileStream(gsFileResCustom, FileMode.Open, FileAccess.Read)
            Dim ds1 As New DataSet
            ds1.ReadXml(filew1)
            Return ds1.Tables(0)
        End If

        Return Nothing
    End Function
#End Region

#End Region

#Region "Kiểm tra tham số bắt buộc nhập khi mở exe con"
    Public Sub CheckObligatoryParameter(ByVal ArrName() As String, ByVal ArrValue() As String)
        CheckObligatoryParameter(ArrName, ArrValue, "")
    End Sub

    Public Sub CheckObligatoryParameter(ByVal ArrName() As String, ByVal ArrValue() As String, ByVal sEXECHILD As String)
        'Update 12/07/2013: Hàm chung kiểm tra tham số truyền vào exe con phải có giá trị
        For i As Integer = 0 To ArrValue.Length - 1
            If ArrValue(i) = "" Then
                'Giá trị tham số không hợp lệ. Bạn không thể mở được chương trình
                MessageBox.Show(rl3("MSG000039") & " (" & ArrName(i) & "='')." & vbCrLf & _
               rl3("MSG000040"), My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                If sEXECHILD <> "" Then D99C0007.RegDeleteExe(EXECHILD) 'Xóa(Registry)
                End
            End If
        Next
    End Sub
#End Region

#Region "Xóa dữ liệu trên lưới"

    ''' <summary>
    ''' Sau khi Delete thành công dưới server thì gọi hàm này, không gọi hàm LoadGrid
    ''' </summary>
    ''' <param name="tdbg">Lưới đang delete</param>
    ''' <param name="dt">Datatable đổ nguồn cho lưới</param>
    ''' <param name="bEnabledUseFind">Cờ bật cho tìm kiếm hoặc Liệt kê tất cả</param>
    ''' <remarks>Gọi hàm tại sự kiện của menu Delete, sau khi thực thi dữ liệu thành công</remarks>
    Public Sub DeleteGridEvent(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal dt As DataTable, ByRef bEnabledUseFind As Boolean, Optional ByVal bDeleteAll As Boolean = False, Optional ByVal sFieldID As String = "")
        'Gọi hàm: DeleteGridEvent(tdbg, dtGrid, gbEnabledUseFind)
        'Update 01/06/2011: sửa lỗi khi xóa thanh cuộn đưa lên dòng đầu tiên
        Dim iRow As Integer
        iRow = tdbg.Row 'Bookmark

        If bDeleteAll Then 'Xóa tất cả các dòng trên lưới
            'Update 12/09/2011
            tdbg.Delete(0, tdbg.RowCount)
        Else ' Xóa 1 dòng hiện tại
            If sFieldID = "" Then
                tdbg.Delete()
            Else ' Dùng cho trường hợp lưới có check Hiển thị chi tiết, 1 phiếu có nhiều hóa đơn, khi xóa phiếu thì xóa các hóa đơn
                Dim dr() As DataRow = dt.Select(sFieldID & " = " & SQLString(tdbg.Columns(sFieldID).Text))
                For i As Integer = 0 To dr.Length - 1
                    dt.Rows.Remove(dr(i))
                Next
            End If
        End If
        dt.AcceptChanges()
        'Update 20/12/2012: sửa lỗi khi xóa dòng cuối thanh cuộn đưa lên dòng đầu tiên
        If dt.Rows.Count > 0 Then tdbg.Row = L3Int(IIf(iRow >= tdbg.RowCount, tdbg.RowCount - 1, iRow))

        bEnabledUseFind = dt.Rows.Count > 0
    End Sub
    '
    '    Public Sub DeleteGridEvent(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal dt As DataTable, ByRef bEnabledUseFind As Boolean, Optional ByVal bDeleteAll As Boolean = False)
    '        'Gọi hàm: DeleteGridEvent(tdbg, dtGrid, gbEnabledUseFind)
    '        'Update 01/06/2011: sửa lỗi khi xóa thanh cuộn đưa lên dòng đầu tiên
    '        Dim iRow As Integer
    '        iRow = tdbg.Row 'Bookmark
    '
    '        If bDeleteAll Then 'Xóa tất cả các dòng trên lưới
    '            'Update 12/09/2011
    '            tdbg.Delete(0, tdbg.RowCount)
    '        Else ' Xóa 1 dòng hiện tại
    '            tdbg.Delete()
    '        End If
    '        dt.AcceptChanges()
    '        If dt.Rows.Count > 0 Then tdbg.Row = iRow
    '
    '        bEnabledUseFind = dt.Rows.Count > 0
    '    End Sub

    ''' <summary>
    ''' Xóa nhiều dòng rời rạc trên lưới Truy vấn
    ''' </summary>
    ''' <param name="tdbg">Lưới</param>
    ''' <param name="dt">DataSource của lưới</param>
    ''' <param name="bEnabledUseFind"></param>
    ''' <remarks>Sau khi Delete thành công dưới server thì gọi hàm này, không gọi hàm LoadGrid</remarks>
    Public Sub DeleteGridEventMultiRows(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal dt As DataTable, ByRef bEnabledUseFind As Boolean)
        'Gọi hàm: DeleteGridEvent(tdbg, dtGrid, gbEnabledUseFind)
        'Update 01/06/2011: sửa lỗi khi xóa thanh cuộn đưa lên dòng đầu tiên
        Dim iRow As Integer
        iRow = tdbg.Row
        '****************************
        'Lấy tập hợp các dòng rời rạc vừa chọn
        Dim SelectedRows As C1.Win.C1TrueDBGrid.SelectedRowCollection = tdbg.SelectedRows
        Dim i As Integer
        Dim myAL As New ArrayList() 'Tạo mảng lưu lại chỉ số vừa chọn
        If SelectedRows.Count > 1 Then
            For i = 0 To SelectedRows.Count - 1
                myAL.Add(SelectedRows.Item(i))
            Next
            myAL.Sort() 'Sắp xếp tăng dần
            For i = myAL.Count - 1 To 0 Step -1
                tdbg.Delete(L3Int(myAL.Item(i)))
            Next
        Else
            tdbg.Delete(tdbg.Row)
        End If
        '****************************
        dt.AcceptChanges()

        If dt.Rows.Count > 0 Then tdbg.Row = iRow
        bEnabledUseFind = dt.Rows.Count > 0
    End Sub

    ''' <summary>
    ''' Xóa nhiều dòng trên lưới nhập liệu
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <remarks></remarks>
    Public Sub DeleteMultiRows(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Try
            If Not c1Grid.AllowDelete Or c1Grid.RowCount < 1 Then Exit Sub
            If D99C0008.MsgAskDeleteRow() = Windows.Forms.DialogResult.Yes Then
                'Update 05/03/2010
                If c1Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.FloatingEditor Then
                    c1Grid.UpdateData()
                End If

                Dim tdbgSelectedRow As C1.Win.C1TrueDBGrid.SelectedRowCollection = c1Grid.SelectedRows
                Dim i As Integer
                Dim myAL As New ArrayList() 'Tạo mảng lưu lại chỉ số vừa chọn 
                If tdbgSelectedRow.Count > 1 Then
                    For i = 0 To tdbgSelectedRow.Count - 1
                        myAL.Add(tdbgSelectedRow.Item(i))
                    Next
                    myAL.Sort() 'Sắp xếp tăng dần 
                    For i = myAL.Count - 1 To 0 Step -1
                        c1Grid.Delete(CInt(myAL.Item(i)))
                    Next
                Else
                    c1Grid.Delete(c1Grid.Bookmark)
                End If
            End If
        Catch ex As Exception
            D99C0008.Msg("Lỗi DeleteMultiRows: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Hàm liên quan đến FilterBar"

    ''' <summary>
    ''' Hàm lấy dữ liệu từ FilterBar change
    ''' </summary>
    ''' <param name="tdbg"> Lưới truyền vào</param>
    ''' <param name="sFilter"> Giá trị FilterBar</param>
    ''' <remarks>Gọi hàm tại sự kiện tdbg_FilterChange</remarks>
    Public Sub FilterChangeGrid(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByRef sFilter As System.Text.StringBuilder, Optional ByRef sFilterServer As System.Text.StringBuilder = Nothing)
        sFilter = New System.Text.StringBuilder("")
        sFilterServer = New System.Text.StringBuilder("")
        Dim dc As C1.Win.C1TrueDBGrid.C1DataColumn
        For Each dc In tdbg.Columns
            Select Case dc.DataType.Name
                Case "DateTime"
                    If dc.FilterText.Length >= 10 Then
                        If sFilter.Length > 0 Then sFilter.Append(" AND ")
                        Dim sClause As String = ""

                        If dc.FilterText.Length = 10 Then
                            sClause = "([" & dc.DataField & "] >= #" & DateSave(CDate(dc.FilterText)) & "#"
                            sClause &= " And [" & dc.DataField & "] < #" & DateSave(CDate(dc.FilterText).AddDays(1)) & "# )"
                            sFilter.Append(sClause)
                            '****************************************
                            If sFilterServer.Length > 0 Then sFilterServer.Append(" AND ")
                            sClause = "( CONVERT(date,[" & dc.DataField & "]) = " & SQLDateSave(dc.FilterText) & ")"
                            sFilterServer.Append(sClause)
                        Else
                            sClause = "([" & dc.DataField & "] >= #" & DateSaveTime(CDate(dc.FilterText)) & "#"
                            sClause &= " And [" & dc.DataField & "] < #" & DateSaveTime(CDate(dc.FilterText).AddDays(1)) & "# )"
                            sFilter.Append(sClause)
                            '****************************************
                            If sFilterServer.Length > 0 Then sFilterServer.Append(" AND ")
                            sClause = "( CONVERT(date,[" & dc.DataField & "]) = " & SQLDateTimeSave(dc.FilterText) & ")"
                            sFilterServer.Append(sClause)
                        End If

                    End If

                Case "Boolean"
                    If dc.FilterText.Length > 0 Then
                        Dim sTemp As String = "[" & dc.DataField + "] = " + "'" + dc.FilterText + "'"
                        If sFilter.Length > 0 Then sFilter.Append(" AND ")
                        sFilter.Append(sTemp)
                        '******************************************
                        If sFilterServer.Length > 0 Then sFilterServer.Append(" AND ")
                        sFilterServer.Append(sTemp)
                    End If

                Case "String"
                    If dc.FilterText.Length > 0 Then
                        Dim sTemp As String = dc.FilterText 'Chặn các ký tự đặc biệt
                        sTemp = sTemp.Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]")
                        sTemp = sTemp.Replace("'", "''")

                        If sFilter.Length > 0 Then sFilter.Append(" AND ")
                        sFilter.Append(("[" & dc.DataField + "] like " + "'%" + sTemp + "%'"))
                        '******************************************
                        If sFilterServer.Length > 0 Then sFilterServer.Append(" AND ")
                        sFilterServer.Append(("[" & dc.DataField + "] like " + "N'%" + sTemp + "%'"))
                    End If

                Case "Byte", "Integer", "Int16", "Int32", "Int64", "Decimal", "Double", "Single"
                    If dc.FilterText.Length > 0 Then
                        Dim sTemp As String = ""
                        'Kiểm tra TH cột check Chọn
                        If tdbg.Columns(dc.DataField).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox Then
                            sTemp = "[" & dc.DataField + "] = " + SQLNumber(L3Bool(dc.FilterText))
                        Else
                            If (dc.FilterText.Contains("%") And dc.Editor IsNot Nothing) Then 'cột Tỷ lệ theo gắn C1NumbericEdit
                                sTemp = "[" & dc.DataField + "] = " + SQLNumber(Number(dc.FilterText) / 100, "#,##0.0000")
                            ElseIf IsNumeric(dc.FilterText) Then
                                If dc.NumberFormat = "Percent" Then 'Định dạng %
                                    sTemp = "[" & dc.DataField + "] = " + SQLNumber(Number(dc.FilterText) / 100, "#,##0.0000")
                                Else
                                    sTemp = "[" & dc.DataField + "] = " + Number(dc.FilterText).ToString
                                End If
                            Else
                                sTemp = "[" & dc.DataField + "] > " & IIf(dc.DataType.Name.Equals("Byte"), MaxInt, MaxDecimal).ToString
                            End If
                        End If
                        If sFilter.Length > 0 Then sFilter.Append(" And ")
                        sFilter.Append(sTemp)
                        '******************************************
                        If sFilterServer.Length > 0 Then sFilterServer.Append(" AND ")
                        sFilterServer.Append(sTemp)
                    End If

            End Select
        Next

    End Sub


    ''' <summary>
    ''' Hàm lấy refresh dữ liệu từ FilterBar
    ''' </summary>
    ''' <param name="tdbg"> Lưới truyền vào</param>
    ''' <param name="sFilter"> Giá trị FilterBar</param>
    ''' <param name="bRefreshFilter"> Cờ bật khi giá trị FilterBar được gán = ""</param>
    ''' <param name="sFilterServer"> Giá trị FilterBar dùng cho Server để truyền vào khi in báo cáo</param>
    ''' <remarks>Gọi hàm tại hàm LoadTDBG</remarks>
    Public Sub ResetFilter(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByRef sFilter As System.Text.StringBuilder, ByRef bRefreshFilter As Boolean, Optional ByRef sFilterServer As System.Text.StringBuilder = Nothing)
        If tdbg.DataSource IsNot Nothing AndAlso sFilter.ToString = "" Then Exit Sub ' Nếu không nhấn FilterBar thì thoát
        If tdbg.FilterActive Then tdbg.UpdateData() ' Nếu con trỏ đứng tại FilterBar thì Update dữ liệu cho lưới.

        'Set lại các giá trị FilterText
        Dim dc As C1.Win.C1TrueDBGrid.C1DataColumn
        bRefreshFilter = True 'Bật cờ không làm gì khi vào sự kiện FilterChange của Grid
        For Each dc In tdbg.Columns
            'Update 19/08/2011: Kiểm tra có dữ liệu thì mới set '', nếu set hết tất cả các cột thì sẽ chậm
            ' dc.FilterText = ""
            If dc.FilterText <> "" Then dc.FilterText = ""
        Next dc
        'sFilter.Remove(0, sFilter.ToString().Trim().Length)
        sFilter = New System.Text.StringBuilder()
        sFilterServer = New System.Text.StringBuilder()

        bRefreshFilter = False

    End Sub

#End Region

#Region "Từ ngữ chung"

    ''' <summary>
    ''' Từ ngữ chung
    ''' </summary>
    ''' <remarks>Gọi hàm chỗ LoadOthers</remarks>
    <DebuggerStepThrough()> _
    Public Sub GeneralItems()
        MsgAnnouncement = rl3("Thong_bao")
        AllCode = "'%'"
        NewCode = "'+'"

        If gbUnicode Then
            AllName = "N'" & rl3("Tat_caU") & "'"
            NewName = "N'" & rl3("Them_moiU") & "'"
        Else
            AllName = "'" & rl3("Tat_caV") & "'"
            NewName = "'" & rl3("Them_moi") & "'"
        End If
    End Sub
#End Region

#Region "Enable control"

    'Example: ReadOnlyControl(txtTranTypeID, c1dateFrom, tdbcVoucherTypeID)

    Public Sub ReadOnlyControl(ByVal ParamArray obj() As Control)
        For i As Integer = 0 To obj.Length - 1
            If TypeOf (obj(i)) Is C1.Win.C1Input.C1DateEdit Then
                Dim ctrl As C1.Win.C1Input.C1DateEdit = CType(obj(i), C1.Win.C1Input.C1DateEdit)
                ctrl.ReadOnly = True
            ElseIf TypeOf (obj(i)) Is TextBox Then
                Dim ctrl As TextBox = CType(obj(i), TextBox)
                ctrl.ReadOnly = True
            ElseIf TypeOf (obj(i)) Is C1.Win.C1List.C1Combo Then
                Dim ctrl As C1.Win.C1List.C1Combo = CType(obj(i), C1.Win.C1List.C1Combo)
                ctrl.ReadOnly = True
            End If

            obj(i).TabStop = False
        Next
    End Sub

    '<DebuggerStepThrough()> _
    'Public Sub ReadOnlyControl(ByVal obj As System.Windows.Forms.TextBox)
    '    obj.ReadOnly = True
    '    obj.TabStop = False
    'End Sub

    '<DebuggerStepThrough()> _
    'Public Sub ReadOnlyControl(ByVal obj As C1.Win.C1List.C1Combo)
    '    obj.ReadOnly = True
    '    obj.TabStop = False
    'End Sub

    '<DebuggerStepThrough()> _
    'Public Sub ReadOnlyControl(ByVal obj As C1.Win.C1Input.C1DateEdit)
    '    obj.ReadOnly = True
    '    obj.TabStop = False
    'End Sub

    Public Sub UnReadOnlyControl(ByVal bOBligatory As Boolean, ByVal ParamArray obj() As Control)
        For i As Integer = 0 To obj.Length - 1
            If TypeOf (obj(i)) Is C1.Win.C1Input.C1DateEdit Then
                Dim ctrl As C1.Win.C1Input.C1DateEdit = CType(obj(i), C1.Win.C1Input.C1DateEdit)
                ctrl.ReadOnly = False
                If bOBligatory Then ctrl.BackColor = COLOR_BACKCOLOROBLIGATORY
            ElseIf TypeOf (obj(i)) Is TextBox Then
                Dim ctrl As TextBox = CType(obj(i), TextBox)
                ctrl.ReadOnly = False
                If bOBligatory Then ctrl.BackColor = COLOR_BACKCOLOROBLIGATORY
            ElseIf TypeOf (obj(i)) Is C1.Win.C1List.C1Combo Then
                Dim ctrl As C1.Win.C1List.C1Combo = CType(obj(i), C1.Win.C1List.C1Combo)
                ctrl.ReadOnly = False
                If bOBligatory Then ctrl.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
            End If

            obj(i).TabStop = True
        Next
    End Sub

    '<DebuggerStepThrough()> _
    Public Sub UnReadOnlyControl(ByVal obj As System.Windows.Forms.TextBox, Optional ByVal bOBligatory As Boolean = False)
        obj.ReadOnly = False
        obj.TabStop = True
        If bOBligatory Then obj.BackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub


    <DebuggerStepThrough()> _
    Public Sub UnReadOnlyControl(ByVal obj As C1.Win.C1List.C1Combo, Optional ByVal bOBligatory As Boolean = False)
        obj.ReadOnly = False
        obj.TabStop = True
        If bOBligatory Then obj.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub

    <DebuggerStepThrough()> _
    Public Sub UnReadOnlyControl(ByVal obj As C1.Win.C1Input.C1DateEdit, Optional ByVal bOBligatory As Boolean = False)
        obj.ReadOnly = False
        obj.TabStop = True
        If bOBligatory Then obj.BackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub

    Public Sub ReadOnlyControl(ByVal bReadOnly As Boolean, ByVal ParamArray obj() As Control)
        For i As Integer = 0 To obj.Length - 1
            Dim bOBLIGATORY As Boolean = Not bReadOnly And obj(i).BackColor = COLOR_BACKCOLOROBLIGATORY

            If TypeOf (obj(i)) Is C1.Win.C1Input.C1DateEdit Then
                Dim ctrl As C1.Win.C1Input.C1DateEdit = CType(obj(i), C1.Win.C1Input.C1DateEdit)
                ctrl.ReadOnly = bReadOnly
            ElseIf TypeOf (obj(i)) Is TextBox Then
                Dim ctrl As TextBox = CType(obj(i), TextBox)
                ctrl.ReadOnly = bReadOnly
            ElseIf TypeOf (obj(i)) Is C1.Win.C1List.C1Combo Then
                Dim ctrl As C1.Win.C1List.C1Combo = CType(obj(i), C1.Win.C1List.C1Combo)
                ctrl.ReadOnly = bReadOnly
                ctrl.EditorBackColor = ctrl.EditorBackColor
            End If

            If bOBLIGATORY Then obj(i).BackColor = COLOR_BACKCOLOROBLIGATORY
            obj(i).TabStop = Not bReadOnly
        Next
    End Sub
#End Region

#Region "Độ phân giải màn hình"

    ''' <summary>
    ''' Điều chỉnh độ phân giải của form KHÔNG có popupmenu
    ''' </summary>
    ''' <param name="MainCtrl">truyền vào tên form</param>
    ''' <remarks>cách gọi tại form_load: SetResolutionForm (Me)</remarks>
    <DebuggerStepThrough()> _
    Public Function SetResolutionForm(ByVal MainCtrl As Control, ByVal ParamArray ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip) As Double
        Return SetResolutionForm(MainCtrl, Nothing, Nothing, Nothing, Nothing, ContextMenuStrip)
    End Function

    ''' <summary>
    ''' Điều chỉnh độ phân giải của form CÓ 1 C1ContextMenu
    ''' </summary>
    ''' <param name="MainCtrl">Tên form</param>
    ''' <param name="ctrlMenu">C1ContextMenu</param>
    ''' <remarks>cách gọi tại form_load: SetResolutionForm (Me, C1ContextMenu)</remarks>
    <DebuggerStepThrough()> _
    Public Function SetResolutionForm(ByVal MainCtrl As Control, ByVal ctrlMenu As C1.Win.C1Command.C1ContextMenu, ByVal ParamArray ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip) As Double
        Return SetResolutionForm(MainCtrl, ctrlMenu, Nothing, Nothing, Nothing, ContextMenuStrip)
    End Function

    ''' <summary>
    ''' Điều chỉnh độ phân giải của form CÓ 2 C1ContextMenu
    ''' </summary>
    ''' <param name="MainCtrl">Tên form</param>
    ''' <param name="ctrlMenu1">C1ContextMenu</param>
    ''' <param name="ctrlMenu2">C1ContextMenu1</param>
    ''' <remarks>cách gọi tại form_load: SetResolutionForm (Me, C1ContextMenu, C1ContextMenu1)</remarks>
    <DebuggerStepThrough()> _
    Public Function SetResolutionForm(ByVal MainCtrl As Control, ByVal ctrlMenu1 As C1.Win.C1Command.C1ContextMenu, ByVal ctrlMenu2 As C1.Win.C1Command.C1ContextMenu, ByVal ParamArray ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip) As Double
        Return SetResolutionForm(MainCtrl, ctrlMenu1, ctrlMenu2, Nothing, Nothing, ContextMenuStrip)
    End Function

    ''' <summary>
    ''' Điều chỉnh độ phân giải của form CÓ 3 C1ContextMenu
    ''' </summary>
    ''' <param name="MainCtrl">Tên form</param>
    ''' <param name="ctrlMenu1">C1ContextMenu</param>
    ''' <param name="ctrlMenu2">C1ContextMenu1</param>
    ''' <param name="ctrlMenu3">C1ContextMenu2</param>
    ''' <remarks>cách gọi tại form_load: SetResolutionForm (Me, C1ContextMenu, C1ContextMenu1, C1ContextMenu2)</remarks>
    <DebuggerStepThrough()> _
    Public Function SetResolutionForm(ByVal MainCtrl As Control, ByVal ctrlMenu1 As C1.Win.C1Command.C1ContextMenu, ByVal ctrlMenu2 As C1.Win.C1Command.C1ContextMenu, ByVal ctrlMenu3 As C1.Win.C1Command.C1ContextMenu, ByVal ParamArray ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip) As Double
        Return SetResolutionForm(MainCtrl, ctrlMenu1, ctrlMenu2, ctrlMenu3, Nothing, ContextMenuStrip)
    End Function

    '''' <summary>
    '''' Điều chỉnh độ phân giải của form CÓ 4 popupmenu
    '''' </summary>
    '''' <param name="MainCtrl">Tên form</param>
    '''' <param name="ctrlMenu1">C1ContextMenu</param>
    '''' <param name="ctrlMenu2">C1ContextMenu1</param>
    '''' <param name="ctrlMenu3">C1ContextMenu2</param>
    '''' <param name="ctrlMenu4">C1ContextMenu3</param>
    '''' <remarks>cách gọi tại form_load: SetResolutionForm (Me, C1ContextMenu, C1ContextMenu1, C1ContextMenu2, C1ContextMenu3)</remarks>
    '<DebuggerStepThrough()> _
    Public Function SetResolutionForm(ByVal MainCtrl As Control, ByVal ctrlMenu1 As C1.Win.C1Command.C1ContextMenu, ByVal ctrlMenu2 As C1.Win.C1Command.C1ContextMenu, ByVal ctrlMenu3 As C1.Win.C1Command.C1ContextMenu, ByVal ctrlMenu4 As C1.Win.C1Command.C1ContextMenu, ByVal ParamArray ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip) As Double
        Dim x_original As Integer = 1024
        Dim y_original As Integer = 768
        Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.Bounds 'Screen.PrimaryScreen.WorkingArea
        Dim tile_x As Double
        Dim tile_y As Double
        Dim y_Location As Integer = 35

        tile_x = workingRectangle.Width / x_original
        tile_y = workingRectangle.Height / y_original
        If tile_x <= 1 And tile_y <= 1 Then
            'Định lại vị trí cho màn hình thiết kế theo chuẩn lớn nhất (1024 x 680)
            If MainCtrl.Width = workingRectangle.Width Then
                MainCtrl.Location = New System.Drawing.Point(0, y_Location)
            End If
            Return tile_x
        End If
        '----------------------------------------------
        'Kiểm tra tại Tùy chọn của D00 có cho phép điều chỉnh độ phân giải không
        If Not AllowAdjustResolution() Then Return tile_x
        '----------------------------------------------

        iSizeFont = CSng(8.25) + CSng(tile_x)

        Dim iMaxWidthForm As Integer = 1024
        Dim iMaxHeightForm As Integer = 680
        Dim bMaxForm As Boolean
        bMaxForm = (MainCtrl.Height = iMaxHeightForm) And (MainCtrl.Width = iMaxWidthForm)

        MainCtrl.Size = New System.Drawing.Size(CInt(MainCtrl.Width * tile_x), CInt(MainCtrl.Height * tile_y))
        If bMaxForm Then ' Là form lớn nhất theo chuẩn 1024 x 680
            MainCtrl.Location = New System.Drawing.Point(0, CInt(y_Location + tile_y * 4))
        Else
            MainCtrl.Location = New System.Drawing.Point(CInt((workingRectangle.Width - MainCtrl.Width) / 2), CInt((workingRectangle.Height - MainCtrl.Height) / 2))
        End If
        For Each ctrl As Control In MainCtrl.Controls
            SetResolutionControl(ctrl, tile_x, tile_y)
        Next
        '------------------------------------------------
        'Set độ phân giải cho Popupmenu
        If ctrlMenu1 IsNot Nothing Then SetResolutionC1ContextMenu(ctrlMenu1)
        If ctrlMenu2 IsNot Nothing Then SetResolutionC1ContextMenu(ctrlMenu2)
        If ctrlMenu3 IsNot Nothing Then SetResolutionC1ContextMenu(ctrlMenu3)
        If ctrlMenu4 IsNot Nothing Then SetResolutionC1ContextMenu(ctrlMenu4)
        If ContextMenuStrip Is Nothing Then Exit Function
        For i As Integer = 0 To ContextMenuStrip.Length - 1
            ContextMenuStrip(i).Font = New System.Drawing.Font(ContextMenuStrip(i).Font.Name, iSizeFont)
        Next
        Return tile_x
    End Function

    Private Sub SetResolutionControl(ByVal ctrl As Control, ByVal tile_x As Double, ByVal tile_y As Double)
        If ctrl.HasChildren = True Then
            For Each ctrl1 As Control In ctrl.Controls
                SetResolutionControl(ctrl1, tile_x, tile_y)
            Next
        End If
        'Nếu Groupbox là đường phân cách
        If ctrl.GetType.Name = "GroupBox" And ctrl.HasChildren = False Then
            ctrl.Location = New System.Drawing.Point(CInt(ctrl.Location.X * tile_x), CInt(ctrl.Location.Y * tile_y))
            ctrl.Font = New System.Drawing.Font(ctrl.Font.Name, iSizeFont, ctrl.Font.Style)
            ctrl.Width = CInt(ctrl.Width * tile_x)
            Exit Sub
        End If
        If TypeOf (ctrl) Is ToolStrip Then 'Bổ sung 09/07/2013
            ctrl.Font = New System.Drawing.Font(ctrl.Font.Name, iSizeFont)
            Exit Sub
        End If
        'Set lại kích cỡ, vị trí control'Nếu đang ở Usercontrol thì không thay đổi vị trí If bF12 = False Then
        'Anchor = 5: Top, left
        If ctrl.Anchor = 5 Then ctrl.Location = New System.Drawing.Point(CInt(ctrl.Location.X * tile_x), CInt(ctrl.Location.Y * tile_y))

        ctrl.Font = New System.Drawing.Font(ctrl.Font.Name, iSizeFont, ctrl.Font.Style)

        If ctrl.GetType.Name <> "Label" Then
            ctrl.Width = CInt(ctrl.Width * tile_x)
        Else
            Dim lb As Label = CType(ctrl, Label)
            If lb.AutoSize = False And lb.Name.ToLower <> "lblimage" Then
                lb.Width = CInt(lb.Width * tile_x)
            End If
        End If
        ctrl.Height = CInt(ctrl.Height * tile_y)

        'Set lại các đặc tính riêng của từng control
        Select Case ctrl.GetType.Name
            Case "Button"
                If ctrl.Name.Contains("btnSetNewKey") = False Then Exit Select

                Dim sButton As New System.Windows.Forms.Button
                sButton = CType(ctrl, System.Windows.Forms.Button)
                sButton.BackgroundImage = sButton.Image 'My.Resources.KEY
                sButton.BackgroundImageLayout = ImageLayout.Stretch
                sButton.Image = Nothing
            Case "C1TrueDBGrid"
                Dim tdbg1 As New C1.Win.C1TrueDBGrid.C1TrueDBGrid
                tdbg1 = CType(ctrl, C1.Win.C1TrueDBGrid.C1TrueDBGrid)
                tdbg1.RowHeight = CInt(tdbg1.RowHeight * tile_y)
                For i As Integer = 0 To tdbg1.Splits.ColCount - 1
                    With tdbg1.Splits(i)
                        For j As Integer = 0 To tdbg1.Columns.Count - 1
                            .DisplayColumns(j).Width = CInt(.DisplayColumns(j).Width * tile_x)
                            .DisplayColumns(j).Style.Font = New System.Drawing.Font(.DisplayColumns(j).Style.Font.Name, iSizeFont, .DisplayColumns(j).Style.Font.Style)

                            .DisplayColumns(j).HeadingStyle.Font = New System.Drawing.Font(.DisplayColumns(j).HeadingStyle.Font.Name, iSizeFont, .DisplayColumns(j).HeadingStyle.Font.Style)
                            .DisplayColumns(j).FooterStyle.Font = New System.Drawing.Font(.DisplayColumns(j).FooterStyle.Font.Name, iSizeFont, .DisplayColumns(j).FooterStyle.Font.Style)
                            .DisplayColumns(j).Style.VerticalAlignment = C1.Win.C1TrueDBGrid.AlignVertEnum.Center
                        Next

                        .CaptionHeight = CInt(.CaptionHeight * tile_x)
                        .ColumnCaptionHeight = CInt(.ColumnCaptionHeight * tile_x)
                        .ColumnFooterHeight = CInt(.ColumnFooterHeight * tile_x)
                        .SplitSize = CInt(.SplitSize * tile_x)
                    End With
                Next
            Case "C1Combo"
                Dim combo1 As New C1.Win.C1List.C1Combo
                combo1 = CType(ctrl, C1.Win.C1List.C1Combo)
                combo1.ItemHeight = CInt(combo1.ItemHeight * tile_y)
                combo1.DropDownWidth = CInt(combo1.DropDownWidth * tile_y)
                For i As Integer = 0 To combo1.Splits.Count - 1
                    With combo1.Splits(i)
                        For j As Integer = 0 To combo1.Columns.Count - 1
                            .DisplayColumns(j).HeadingStyle.Font = New System.Drawing.Font(.DisplayColumns(j).HeadingStyle.Font.Name, iSizeFont, .DisplayColumns(j).HeadingStyle.Font.Style)
                            .DisplayColumns(j).Width = CInt(.DisplayColumns(j).Width * tile_x)
                        Next
                    End With
                Next
                combo1.Splits(0).ColumnCaptionHeight = CInt(combo1.Splits(0).ColumnCaptionHeight * tile_x)
                combo1.VScrollBar.Width = CInt(combo1.VScrollBar.Width * tile_x)
                combo1.HScrollBar.Height = CInt(combo1.HScrollBar.Height * tile_y)
            Case "C1TrueDBDropdown"
                Dim dropdown1 As New C1.Win.C1TrueDBGrid.C1TrueDBDropdown
                dropdown1 = CType(ctrl, C1.Win.C1TrueDBGrid.C1TrueDBDropdown)
                dropdown1.RowHeight = CInt(dropdown1.RowHeight * tile_y)
                dropdown1.ColumnCaptionHeight = CInt(dropdown1.ColumnCaptionHeight * tile_x)
                For i As Integer = 0 To dropdown1.DisplayColumns.Count - 1
                    dropdown1.DisplayColumns(i).HeadingStyle.Font = New System.Drawing.Font(dropdown1.DisplayColumns(i).HeadingStyle.Font.Name, iSizeFont, dropdown1.DisplayColumns(i).HeadingStyle.Font.Style)
                    dropdown1.DisplayColumns(i).Width = CInt(dropdown1.DisplayColumns(i).Width * tile_x)
                Next
            Case "C1DateEdit"
                Dim sc1dateEdit As C1.Win.C1Input.C1DateEdit
                Dim sMonthCalendar As New System.Windows.Forms.MonthCalendar
                sc1dateEdit = CType(ctrl, C1.Win.C1Input.C1DateEdit)

                sc1dateEdit.Calendar.Font = New System.Drawing.Font(sc1dateEdit.Calendar.Font.Name, iSizeFont, sc1dateEdit.Calendar.Font.Style)
                sc1dateEdit.Calendar.Height = CInt(tile_y * sc1dateEdit.Calendar.Height)
                sc1dateEdit.Calendar.Width = CInt(tile_x * sc1dateEdit.Calendar.Height)
            Case "StatusStrip"
                Dim sStatusStrip As New System.Windows.Forms.StatusStrip
                sStatusStrip = CType(ctrl, System.Windows.Forms.StatusStrip)
                For i As Integer = 0 To sStatusStrip.Items.Count - 1
                    sStatusStrip.Items(i).Width = CInt(sStatusStrip.Items(i).Width * tile_x)
                Next
            Case "PictureBox"
                Dim sPicture As New System.Windows.Forms.PictureBox
                sPicture = CType(ctrl, System.Windows.Forms.PictureBox)
                sPicture.BackgroundImageLayout = ImageLayout.Stretch
                sPicture.SizeMode = PictureBoxSizeMode.StretchImage
            Case "ComboBox"
                Dim combo1 As New System.Windows.Forms.ComboBox

                combo1 = CType(ctrl, System.Windows.Forms.ComboBox)
                combo1.ItemHeight = CInt(combo1.ItemHeight * tile_y)
                combo1.DropDownWidth = CInt(combo1.DropDownWidth * (tile_y + 0.1))
                'Height của combo = kích thước của Font
                combo1.Font = New System.Drawing.Font(combo1.Font.Name, CSng(Int(iSizeFont + tile_x * 2)), combo1.Font.Style)
        End Select
    End Sub

    ''' <summary>
    ''' Điều chỉnh độ phân giải của menu
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks>Cách gọi tại form_load: SetResolutionC1ContextMenu (C1ContextMenu)</remarks>
    Private Sub SetResolutionC1ContextMenu(ByVal ctrl As C1.Win.C1Command.C1ContextMenu)
        ctrl.Font = New System.Drawing.Font(ctrl.Font.Name, iSizeFont)
    End Sub

    ''' <summary>
    ''' Lấy Điều chỉnh độ phân giải ở Tùy chọn của D00
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AllowAdjustResolution() As Boolean
        Dim sAdjustResolution As String = D99C0007.GetModulesSetting("D00", ModuleOption.lmOthers, "AdjustResolution", "0")
        If sAdjustResolution = "0" Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Quy cách"

    Public Function ReturnTableSpecCaption(Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "Select SpecTypeID, Caption" & UnicodeJoin(bUseUnicode) & " as Caption, Disabled From D07T0410 WITH(NOLOCK) Order by SpecTypeID"
        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary> 
    ''' Gán caption cho 10 cột quy cách 
    ''' </summary> 
    ''' <param name="tdbg"> Lưới cần gán caption </param> 
    ''' <param name="COL_Spec01ID"> Cột bắt đầu </param> 
    ''' <param name="Split"> Split cần gán </param> 
    ''' <param name="IsVisibleColumn"> = True: nếu lưới không có nút Quy cách </param>
    ''' <remarks> tdbg.Columns(iIndex).Tag chứa giá trị Boolean chỉ cột này có hiển thị hay không </remarks> 
    <DebuggerStepThrough()> _
    Public Function LoadTDBGridSpecificationCaption(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Spec01ID As Integer, ByVal Split As Integer, Optional ByVal IsVisibleColumn As Boolean = False, Optional ByVal bUnicode As Boolean = False, Optional ByRef dt As DataTable = Nothing) As Boolean
        Dim bUseSpec As Boolean = False

        ''Dim sSQL As String = "Select SpecTypeID, Caption, Disabled From D07T0410 WITH(NOLOCK) Where Disabled = 0 Order by SpecTypeID"
        'Dim sSQL As String = "Select SpecTypeID, Caption" & UnicodeJoin(bUnicode) & " as Caption, Disabled From D07T0410 WITH(NOLOCK) Order by SpecTypeID"
        If dt Is Nothing Then dt = ReturnTableSpecCaption(bUnicode)
        Dim iIndex As Integer = COL_Spec01ID
        Dim i As Integer

        If dt.Rows.Count > 0 Then
            For i = 0 To 9
                tdbg.Columns(iIndex).Caption = dt.Rows(i).Item("Caption").ToString
                tdbg.Columns(iIndex).Tag = Not (Convert.ToBoolean(dt.Rows(i).Item("Disabled")))

                gbArrSpecVisiable(iIndex - COL_Spec01ID) = Convert.ToBoolean(tdbg.Columns(iIndex).Tag)
                If Not bUseSpec And Convert.ToBoolean(tdbg.Columns(iIndex).Tag) = True Then
                    bUseSpec = True
                End If
                tdbg.Splits(Split).DisplayColumns(iIndex).HeadingStyle.Font = FontUnicode(bUnicode, tdbg.Splits(Split).DisplayColumns(iIndex).HeadingStyle.Font.Style) 'New System.Drawing.Font("Lemon3", 8.249999!)

                If IsVisibleColumn Then ' Lưới không có nút thì hiển thị cột Quy cách
                    tdbg.Splits(Split).DisplayColumns(iIndex).Visible = Convert.ToBoolean(tdbg.Columns(iIndex).Tag)
                End If

                iIndex += 1
            Next
        End If
        '   dt = Nothing'Lỗi load nhiều lần

        Return bUseSpec
    End Function

    ''' <summary> 
    ''' Đổ nguồn cho 10 quy cách 
    ''' </summary> 
    ''' <param name="tdbdSpec01ID"></param> 
    ''' <param name="tdbdSpec02ID"></param> 
    ''' <param name="tdbdSpec03ID"></param> 
    ''' <param name="tdbdSpec04ID"></param> 
    ''' <param name="tdbdSpec05ID"></param> 
    ''' <param name="tdbdSpec06ID"></param> 
    ''' <param name="tdbdSpec07ID"></param> 
    ''' <param name="tdbdSpec08ID"></param> 
    ''' <param name="tdbdSpec09ID"></param> 
    ''' <param name="tdbdSpec10ID"></param> 
    ''' <param name="tdbg"></param> 
    ''' <param name="COL_Spec01ID"> Cột bắt đầu </param> 
    ''' <remarks></remarks> 
    <DebuggerStepThrough()> _
    Public Sub LoadTDBDropDownSpecification(ByVal tdbdSpec01ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec02ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec03ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec04ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec05ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec06ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec07ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec08ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec09ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdSpec10ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, _
    ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Spec01ID As Integer, Optional ByVal bUnicode As Boolean = False, Optional ByVal bAddNew As Boolean = False, Optional ByRef dt As DataTable = Nothing)
        If dt Is Nothing Then dt = ReturnTableSpecification(bAddNew, bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID).Tag) = True Then LoadDataSource(tdbdSpec01ID, ReturnTableFilter(dt, "SpecTypeID='01' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 1).Tag) = True Then LoadDataSource(tdbdSpec02ID, ReturnTableFilter(dt, "SpecTypeID='02' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 2).Tag) = True Then LoadDataSource(tdbdSpec03ID, ReturnTableFilter(dt, "SpecTypeID='03' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 3).Tag) = True Then LoadDataSource(tdbdSpec04ID, ReturnTableFilter(dt, "SpecTypeID='04' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 4).Tag) = True Then LoadDataSource(tdbdSpec05ID, ReturnTableFilter(dt, "SpecTypeID='05' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 5).Tag) = True Then LoadDataSource(tdbdSpec06ID, ReturnTableFilter(dt, "SpecTypeID='06' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 6).Tag) = True Then LoadDataSource(tdbdSpec07ID, ReturnTableFilter(dt, "SpecTypeID='07' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 7).Tag) = True Then LoadDataSource(tdbdSpec08ID, ReturnTableFilter(dt, "SpecTypeID='08' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 8).Tag) = True Then LoadDataSource(tdbdSpec09ID, ReturnTableFilter(dt, "SpecTypeID='09' or SpecTypeID='+'"), bUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Spec01ID + 9).Tag) = True Then LoadDataSource(tdbdSpec10ID, ReturnTableFilter(dt, "SpecTypeID='10' or SpecTypeID='+'"), bUnicode)
    End Sub

    Public Function ReturnTableSpecification(Optional ByVal bAddnew As Boolean = False, Optional ByVal bUnicode As Boolean = False, Optional ByVal sSpecTypeID As String = "") As DataTable
        Dim sSQL As String = "--Do nguon Quy cach" & vbCrLf
        If bAddnew Then
            sSQL &= "Select '+' as SpecID, " & NewName & " As SpecName, '+' as SpecTypeID, 0 AS DisplayOrder" & vbCrLf
            sSQL &= "Union All" & vbCrLf
        End If
        sSQL &= "Select SpecID, Description" & UnicodeJoin(bUnicode) & " As SpecName, SpecTypeID, 1 AS DisplayOrder From D07T1410 WITH(NOLOCK) Where Disabled = 0 " & IIf(sSpecTypeID <> "", " and SpecTypeID = " & SQLString(sSpecTypeID), "").ToString & " Order by DisplayOrder, SpecTypeID, SpecID"
        Return ReturnDataTable(sSQL)
    End Function
#End Region

#Region "Set ngày Lemon3 không phụ thuộc vào Window"
    ''' <summary>
    ''' Set ngày, số của Lemon3 không phụ thuộc vào Window. Nơi gọi tại dòng đầu tiên của hàm Main()
    ''' </summary>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub SetSysDateTime()
        'Set lại ngày, số chương trình (để format ngày cho form Thông tin hệ thống, số) không phụ thuộc vào cách thiết lập ngày, số của hệ thống 
        Dim culMyDate_Num As CultureInfo = New CultureInfo("en-GB", False)

        'CType(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo) 
        'Minh Hòa update 19/02/2013: không hiển thị thời gian theo AM, PM
        'culMyDate_Num.DateTimeFormat.LongTimePattern = "hh:mm:ss tt"
        culMyDate_Num.DateTimeFormat.LongTimePattern = "HH:mm:ss"

        culMyDate_Num.NumberFormat.NumberDecimalSeparator = "."
        culMyDate_Num.NumberFormat.NumberGroupSeparator = ","
        culMyDate_Num.NumberFormat.PercentDecimalSeparator = "."
        culMyDate_Num.NumberFormat.PercentGroupSeparator = ","
        culMyDate_Num.NumberFormat.CurrencyDecimalSeparator = "."
        culMyDate_Num.NumberFormat.CurrencyGroupSeparator = ","
        culMyDate_Num.NumberFormat.PercentSymbol = "%"

        Thread.CurrentThread.CurrentCulture = culMyDate_Num

    End Sub
#End Region

#Region "Ghi FooterText của lưới"
    ''' <summary>
    ''' Tính tổng Footer cho các cột số trên lưới
    ''' </summary>
    ''' <param name="tdbg">Tập các cột cần tính tổng (Khai báo biến chung trong form)  VD: Dim iColumnsSum() As Integer = {COL_OQuantity, COL_CQuantity, COL_OAmount, COL_CAmount}</param>
    ''' <param name="iColumns"></param>
    ''' <param name="COL_OrderNo">Cột STT nếu có</param>
    ''' <remarks>Gọi hàm FooterSum(tdbg, iCols, COL_OrderNum) </remarks>
    ''' Chú ý: phải đặt hàm này sau hàm tdbg_NumberFormat
    ''' Hàm này được đặt ở 4 chỗ:
    ''' 1. LoadGrid 
    ''' 2. tdbg_AfterColUpdate 
    ''' 3. tdbg_AfterDelete 
    ''' 4. tdbg_KeyDown (nếu có Shift+Insert hoặc F7, F8, ....)
    <DebuggerStepThrough()> _
    Public Sub FooterSum(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iColumns() As Integer, Optional ByVal COL_OrderNo As Integer = -1, Optional ByVal bUseFilterBar As Boolean = False)
        Dim dblSum(iColumns.Length) As Double
        If bUseFilterBar = False Then tdbg.UpdateData()

        For i As Integer = 0 To tdbg.RowCount - 1
            If COL_OrderNo <> -1 Then tdbg(i, COL_OrderNo) = i + 1 'Có cột STT 
            For j As Integer = 0 To iColumns.Length - 1
                dblSum(j) += Number(tdbg(i, iColumns(j)))
            Next j
        Next i
        For j As Integer = 0 To iColumns.Length - 1
            If tdbg.Columns(iColumns(j)).NumberFormat Is Nothing Then Exit Sub
            tdbg.Columns(iColumns(j)).FooterText = Format(dblSum(j), tdbg.Columns(iColumns(j)).NumberFormat)
        Next
    End Sub

    ''' <summary>
    ''' Tính tổng Footer cho các cột số trên lưới
    ''' </summary>
    ''' <param name="tdbg">Tập các cột cần tính tổng (Khai báo biến chung trong form)  VD: Dim iColumnsSum() As String = {COLS_OQuantity, COLS_CQuantity, COLS_OAmount, COLS_CAmount}</param>
    ''' <param name="iColumns"></param>
    ''' <remarks>Gọi hàm FooterSum(tdbg, iCols, COL_OrderNum) </remarks>
    ''' Chú ý: phải đặt hàm này sau hàm tdbg_NumberFormat
    ''' Hàm này được đặt ở 4 chỗ:
    ''' 1. LoadGrid 
    ''' 2. tdbg_AfterColUpdate 
    ''' 3. tdbg_AfterDelete 
    ''' 4. tdbg_KeyDown (nếu có Shift+Insert hoặc F7, F8, ....)
    <DebuggerStepThrough()> _
    Public Sub FooterSum(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iColumns() As String, Optional ByVal bUseFilterBar As Boolean = False)
        Dim dblSum(iColumns.Length) As Double
        If bUseFilterBar = False Then tdbg.UpdateData()
        For i As Integer = 0 To tdbg.RowCount - 1
            For j As Integer = 0 To iColumns.Length - 1
                dblSum(j) += Number(tdbg(i, iColumns(j)))
            Next j
        Next i
        For j As Integer = 0 To iColumns.Length - 1
            If tdbg.Columns(iColumns(j)).NumberFormat Is Nothing Then Exit Sub
            tdbg.Columns(iColumns(j)).FooterText = Format(dblSum(j), tdbg.Columns(iColumns(j)).NumberFormat)
        Next
    End Sub

    ''' <summary>
    ''' Ghi FooterText của dòng Tổng cộng
    ''' </summary>
    ''' <param name="tdbg"></param>
    ''' <param name="sColumnName">Tên cột gắn chữ Tổng cộng</param>
    ''' <remarks>Cách gọi: FooterTotalGrid(tdbg, "ObjectID")</remarks>
    <DebuggerStepThrough()> _
    Public Sub FooterTotalGrid(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal sColumnName As String)
        tdbg.Columns(sColumnName).FooterText = rl3("Tong_cong") & Space(1) & "(" & tdbg.RowCount & ")"
    End Sub

    ''' <summary>
    ''' Ghi FooterText của dòng Tổng cộng
    ''' </summary>
    ''' <param name="tdbg"></param>
    ''' <param name="iColumnName">Index của cột gắn chữ Tổng cộng</param>
    ''' <remarks>Cách gọi: FooterTotalGrid(tdbg, COL_ObjectID)</remarks>
    <DebuggerStepThrough()> _
    Public Sub FooterTotalGrid(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iColumnName As Integer)
        tdbg.Columns(iColumnName).FooterText = rl3("Tong_cong") & Space(1) & "(" & tdbg.RowCount & ")"
    End Sub
#End Region

#Region "Nhóm truy cập dữ liệu"

    ''' <summary>
    ''' Load combo Nhóm Truy cập dữ liệu
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="bUseUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadTDBC_DAGroupID(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        Dim sSQL As String = "--Do nguon Nhom truy cap du lieu" & vbCrLf
        'Load tdbcDAGroupID
        sSQL &= "SELECT DAGroupID, " & IIf(bUseUnicode, "DAGroupNameU", "DAGroupName").ToString & " As DAGroupName" & vbCrLf
        sSQL &= " FROM 	LEMONSYS.dbo.D00T0080 WITH(NOLOCK) " & vbCrLf
        sSQL &= "WHERE	 Disabled=0 And (DAGroupID IN (Select DAGroupID " & vbCrLf
        sSQL &= "From lemonsys.dbo.D00V0080 " & vbCrLf
        sSQL &= "Where UserID=" & SQLString(gsUserID) & ")  " & vbCrLf
        sSQL &= "OR 'LEMONADMIN' =" & SQLString(gsUserID) & ")  " & vbCrLf
        sSQL &= "ORDER BY  DAGroupID" & vbCrLf
        LoadDataSource(tdbc, sSQL, bUseUnicode)
    End Sub

    Public Sub CheckIsDAGroupID()
        Dim sSQL As String
        sSQL = "--Do nguon nhom truy cap du lieu cho User" & vbCrLf
        sSQL &= "SELECT TOP 1 T81.DAGroupID AS DAGroupID" & vbCrLf
        sSQL &= "FROM LEMONSYS..D00T0030 T30 WITH(NOLOCK) INNER JOIN LEMONSYS..D00T0070 T70 WITH(NOLOCK) ON T30.UserGroupID = T70.UserGroupID" & vbCrLf
        sSQL &= "INNER JOIN LEMONSYS..D00T0081 T81 WITH(NOLOCK) ON T81.UserGroupID = T70.UserGroupID" & vbCrLf
        sSQL &= "WHERE T30.UserID = " & SQLString(gsUserID)
        gbIsDAGroup = ExistRecord(sSQL)
    End Sub
#End Region

#Region "Loại chứng từ"

    ''' <summary>
    ''' Trả về table cho combo Loại chứng từ
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableVoucherTypeID(ByVal sModuleID As String, Optional ByVal sEditTransTypeID As String = "", Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "--Do nguon cho combo loai phieu" & vbCrLf
        sSQL &= "Select VoucherTypeID, " & IIf(bUseUnicode, "VoucherTypeNameU", "VoucherTypeName").ToString & " as VoucherTypeName, Auto, S1Type, S1, S2Type, S2, " & vbCrLf
        sSQL &= "S3, S3Type, OutputOrder, OutputLength, Separator " & vbCrLf
        sSQL &= "From D91T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Use" & sModuleID & " = 1 And Disabled = 0 " & vbCrLf
        sSQL &= "AND( VoucherDivisionID='' Or VoucherDivisionID = " & SQLString(gsDivisionID) & ") " & vbCrLf

        'Load cho trường hợp Sửa, Xem
        If sEditTransTypeID <> "" Then
            sSQL &= "Or VoucherTypeID = " & SQLString(sEditTransTypeID) & vbCrLf
        End If
        sSQL &= "Order By VoucherTypeID"

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Trả về table cho combo Loại chứng từ (để Lọc)
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableVoucherTypeIDAll(ByVal sModuleID As String, Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sUnicode As String = ""
        Dim sAll As String = ""
        UnicodeAllString(sUnicode, sAll, bUseUnicode)

        Dim sSQL As String = "--Do nguon cho combo loai phieu" & vbCrLf
        sSQL &= "Select VoucherTypeID, VoucherTypeName" & sUnicode & " as VoucherTypeName, 1 as DisplayOrder From D91T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Use" & sModuleID & " = 1 And Disabled = 0 " & vbCrLf
        sSQL &= "AND( VoucherDivisionID='' Or VoucherDivisionID = " & SQLString(gsDivisionID) & ") " & vbCrLf
        sSQL &= "Union All" & vbCrLf
        sSQL &= "Select '%' as VoucherTypeID, " & sAll & " as VoucherTypeName, 0 as DisplayOrder " & vbCrLf
        sSQL &= "Order By DisplayOrder, VoucherTypeID"

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Load Combo Loại chứng từ
    ''' </summary>
    ''' <param name="tdbc">tdbcVoucherTypeID</param>
    ''' <param name="sModuleID">Dxx</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadVoucherTypeID(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal sEditTransTypeID As String = "", Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableVoucherTypeID(sModuleID, sEditTransTypeID, bUseUnicode), bUseUnicode)
    End Sub


    ''' <summary>
    ''' Load 2 combo Loại chứng từ có cùng nguồn
    ''' </summary>
    ''' <param name="tdbc1"></param>
    ''' <param name="tdbc2"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadVoucherTypeID(ByVal tdbc1 As C1.Win.C1List.C1Combo, ByVal tdbc2 As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal sEditTransTypeID As String = "", Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableVoucherTypeID(sModuleID, sEditTransTypeID, bUseUnicode)
        LoadDataSource(tdbc1, dt, bUseUnicode)
        LoadDataSource(tdbc2, dt.DefaultView.ToTable, bUseUnicode)
    End Sub


    ''' <summary>
    ''' Load Combo Loại chứng từ (để Lọc)
    ''' </summary>
    ''' <param name="tdbc">tdbcVoucherTypeID</param>
    ''' <param name="sModuleID">Dxx</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadVoucherTypeIDAll(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableVoucherTypeIDAll(sModuleID, bUseUnicode), bUseUnicode)
    End Sub


    ''' <summary>
    ''' Load 2 combo Loại chứng từ (để Lọc) có cùng nguồn
    ''' </summary>
    ''' <param name="tdbc1"></param>
    ''' <param name="tdbc2"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadVoucherTypeIDAll(ByVal tdbc1 As C1.Win.C1List.C1Combo, ByVal tdbc2 As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableVoucherTypeIDAll(sModuleID, bUseUnicode)
        LoadDataSource(tdbc1, dt, bUseUnicode)
        LoadDataSource(tdbc2, dt.Copy, bUseUnicode)
    End Sub

#End Region

#Region "Loại đối tượng"
    ''' <summary>
    ''' Trả ra Table cho Loại đối tượng
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableObjectTypeID(Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "--Do nguon Loai doi tuong" & vbCrLf
        sSQL &= "Select ObjectTypeID, " & IIf(geLanguage = EnumLanguage.Vietnamese, "ObjectTypeName", "ObjectTypeName01").ToString & UnicodeJoin(bUseUnicode) & " as ObjectTypeName "
        sSQL &= "From D91T0005 WITH(NOLOCK) Order By ObjectTypeID"
        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Trả ra Table cho Loại đối tượng có chưa %
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableObjectTypeIDAll(Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sUnicode As String = ""
        Dim sLanguage As String = ""
        UnicodeAllString(sUnicode, sLanguage, bUseUnicode)
        '***************
        Dim sSQL As String = "--Do nguon Loai doi tuong" & vbCrLf
        sSQL &= "Select ObjectTypeID, " & IIf(geLanguage = EnumLanguage.Vietnamese, "ObjectTypeName", "ObjectTypeName01").ToString & sUnicode & " as ObjectTypeName, 1 as DisplayOrder  "
        sSQL &= "From D91T0005 WITH(NOLOCK) "
        sSQL &= "Union All" & vbCrLf
        sSQL &= "Select '%' as ObjectTypeID, " & sLanguage & " as ObjectTypeName, 0 as DisplayOrder " & vbCrLf
        sSQL &= "Order By DisplayOrder, ObjectTypeID"
        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Load Combo Loại đối tượng
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadObjectTypeID(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableObjectTypeID(bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Loại đối tượng có chứa %
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadObjectTypeIDAll(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableObjectTypeIDAll(bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Loại đối tượng (áp dụng cho màn hình có nhiều combo có cùng nguồn dữ liệu)
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="dt">Bảng dữ liệu của Loại đối tượng</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadObjectTypeID(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal dt As DataTable, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Loại đối tượng
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadObjectTypeID(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableObjectTypeID(bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Loại đối tượng có chứa %
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadObjectTypeIDAll(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableObjectTypeIDAll(bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Loại đối tượng (áp dụng cho màn hình có nhiều Dropdown có cùng nguồn dữ liệu)
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="dt">Bảng dữ liệu của Loại đối tượng</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadObjectTypeID(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal dt As DataTable, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, dt.Copy, bUseUnicode)
    End Sub

#End Region

#Region "Người lập"

    ''' <summary>
    ''' Lấy giá trị mặc định của người lập
    ''' </summary>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCreateBy()
        Dim sSQL As String = "-- Lay gia tri mac dinh cua nguoi lap" & vbCrLf
        sSQL = "Select Top 1 ObjectID, LockL3UserID  From Object WITH(NOLOCK) Where ObjectTypeID = 'NV' "
        sSQL &= "AND L3UserID = " & SQLString(gsUserID)
        Dim dt As DataTable = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            gsCreateBy = dt.Rows(0).Item("ObjectID").ToString
            gbLockL3UserID = L3Bool(dt.Rows(0).Item("LockL3UserID"))
        End If
    End Sub

    ''' <summary>
    ''' Table load nguồn cho combo Người lập
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableCreateBy(Optional ByVal bUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "--Do nguon combo Nguoi lap" & vbCrLf

        sSQL &= " SELECT Object.ObjectID as EmployeeID, Object.ObjectName" & UnicodeJoin(bUnicode) & " as EmployeeName" & vbCrLf
        sSQL &= " FROM 	Object  WITH(NOLOCK) " & vbCrLf
        sSQL &= " WHERE Disabled = 0 And  Object.ObjectTypeID = 'NV'" & vbCrLf
        sSQL &= " And (	DAG ='' Or DAG In (Select DAGroupID From LemonSys.dbo.D00V0080 " & vbCrLf
        sSQL &= " Where UserID= " & SQLString(gsUserID) & " ) Or 'LEMONADMIN' = " & SQLString(gsUserID) & " ) " & vbCrLf
        sSQL &= " ORDER BY ObjectID"

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Load Combo Người lập
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboCreateBy(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableCreateBy(bUnicode), bUnicode)
    End Sub

    ''' <summary>
    '''  Load Combo Người lập theo table có sẵn
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboCreateBy(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal dt As DataTable, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbc, dt, bUnicode)
    End Sub

    ''' <summary>
    ''' Gán text cho combo Người lập
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    Public Sub GetTextCreateBy(ByVal tdbc As C1.Win.C1List.C1Combo)
        If tdbc.FindStringExact(gsCreateBy) = -1 Then
            tdbc.Text = ""
        Else
            tdbc.Text = gsCreateBy
            'Update 31/07/2012: Kiểm tra Khóa người dùng Lemon3
            'Nếu D91 thiết lập "Khóa người dùng Lemon3" (gbLockL3UserID = True) và combo Người lập có giá trị thì Lock Combo lại.
            If gbLockL3UserID Then tdbc.ReadOnly = (tdbc.Text <> "")
        End If
    End Sub
#End Region

#Region "Tài khoản (Chỉ gồm Tài khoản trong bảng)"

    '''' <summary>
    '''' Trả ra Table Tài khoản
    '''' </summary>
    '''' <param name="sClauseWhere">Nếu có điều kiện lọc thì truyền vào (ví dụ: GroupID in ('1', '13')) </param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '<DebuggerStepThrough()> _
    Public Function ReturnTableAccountID(Optional ByVal sClauseWhere As String = "", Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "--Do nguon Tai khoan " & vbCrLf
        sSQL &= "Select AccountID, " & IIf(geLanguage = EnumLanguage.Vietnamese, "AccountName", "AccountName01").ToString & UnicodeJoin(bUseUnicode) & " as AccountName, GroupID " & vbCrLf
        sSQL &= "From D90T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Disabled = 0 And OffAccount = 0 " & vbCrLf
        If sClauseWhere <> "" Then
            sSQL &= "And (" & sClauseWhere & ") " & vbCrLf
        End If
        sSQL &= "Order By AccountID"

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Trả ra Table Tài khoản có chứa %
    ''' </summary>
    ''' <param name="sClauseWhere">Nếu có điều kiện lọc thì truyền vào (ví dụ: GroupID in ('1', '13'))</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnTableAccountIDAll(Optional ByVal sClauseWhere As String = "", Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sUnicode As String = ""
        Dim sLanguage As String = ""
        UnicodeAllString(sUnicode, sLanguage, bUseUnicode)
        '***************
        Dim sSQL As String = "--Do nguon Tai khoan co % " & vbCrLf
        sSQL &= "Select '%' as AccountID, " & sLanguage & " as AccountName, '' As GroupID,0 AS DisplayOrder " & vbCrLf
        sSQL &= "Union All " & vbCrLf
        sSQL &= "Select AccountID, " & IIf(geLanguage = EnumLanguage.Vietnamese, "AccountName", "AccountName01").ToString & sUnicode & " as AccountName, GroupID, 1 AS DisplayOrder "
        sSQL &= "From D90T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Disabled = 0 And OffAccount = 0 " & vbCrLf
        If sClauseWhere <> "" Then
            sSQL &= "And (" & sClauseWhere & ") " & vbCrLf
        End If
        sSQL &= "Order By DisplayOrder, AccountID"

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Load Combo Tài khoản
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountID(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountID(, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Tài khoản có chứa %
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAll(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountIDAll(, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Combo Tài khoản
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountID(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountID(, bUseUnicode)
        LoadDataSource(tdbcFrom, dt, bUseUnicode)
        LoadDataSource(tdbcTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Combo Tài khoản có chứa %
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAll(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAll(, gbUnicode)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
        LoadDataSource(tdbcTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Tài khoản có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountID(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountID(sClauseWhere, gbUnicode), gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Tài khoản có chứa % có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAll(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountIDAll(sClauseWhere, gbUnicode), gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Tài khoản có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sClauseWhere">Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountID(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountID(sClauseWhere, gbUnicode)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
        LoadDataSource(tdbcTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Tài khoản có chứa % có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sClauseWhere">Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAll(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAll(sClauseWhere, gbUnicode)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
        LoadDataSource(tdbcTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load combo Tài khoản (áp dụng cho màn hình có nhiều combo có cùng nguồn dữ liệu )
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="dt">Bảng dữ liệu tài khoản</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
        Public Sub LoadAccountID(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal dt As DataTable, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountID(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountID(, gbUnicode), gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản có chứa %
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAll(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountIDAll(, gbUnicode), gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountID(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountID(, gbUnicode)
        LoadDataSource(tdbdFrom, dt, gbUnicode)
        LoadDataSource(tdbdTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản có chứa %
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAll(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAll(, gbUnicode)
        LoadDataSource(tdbdFrom, dt, gbUnicode)
        LoadDataSource(tdbdTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountID(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountID(sClauseWhere, gbUnicode), gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản có chứa % có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAll(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountIDAll(sClauseWhere, gbUnicode), gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <param name="sClauseWhere"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountID(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountID(sClauseWhere, gbUnicode)
        LoadDataSource(tdbdFrom, dt, gbUnicode)
        LoadDataSource(tdbdTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản có chứa % có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <param name="sClauseWhere"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAll(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAll(sClauseWhere, gbUnicode)
        LoadDataSource(tdbdFrom, dt, gbUnicode)
        LoadDataSource(tdbdTo, dt.Copy, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản (áp dụng cho màn hình có nhiều Dropdown có cùng nguồn dữ liệu)
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="dt">Bảng dữ liệu Tài khoản</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountID(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal dt As DataTable, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, dt.Copy, gbUnicode)
    End Sub

#End Region

#Region "Tài khoản (Bao gồm Tài khoản trong bảng và ngoại bảng)"

    ''' <summary>
    ''' Trả ra Table Tài khoản (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="sClauseWhere"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableAccountIDAndOffAccount(Optional ByVal sClauseWhere As String = "", Optional ByVal bUseUnicode As Boolean = False) As DataTable

        Dim sSQL As String = "--Do nguon Tai khoan (gom TK trong va ngoai bang) " & vbCrLf
        sSQL &= "Select AccountID, " & IIf(geLanguage = EnumLanguage.Vietnamese, "AccountName", "AccountName01").ToString & UnicodeJoin(bUseUnicode) & " as AccountName, GroupID " & vbCrLf
        sSQL &= "From D90T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Disabled = 0 " & vbCrLf
        If sClauseWhere <> "" Then
            sSQL &= "And (" & sClauseWhere & ") " & vbCrLf
        End If
        sSQL &= "Order By AccountID "

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Trả ra Table Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="sClauseWhere"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableAccountIDAndOffAccountAll(Optional ByVal sClauseWhere As String = "", Optional ByVal bUseUnicode As Boolean = False) As DataTable

        Dim sUnicode As String = ""
        Dim sLanguage As String = ""
        UnicodeAllString(sUnicode, sLanguage, bUseUnicode)
        Dim sSQL As String = "--Do nguon Tai khoan co % (gom TK trong va ngoai bang) " & vbCrLf
        sSQL &= "Select '%' as AccountID, " & sLanguage & " as AccountName, '' as GroupID, 0 as DisplayOrder " & vbCrLf
        sSQL &= "Union All " & vbCrLf
        sSQL &= "Select AccountID, " & IIf(geLanguage = EnumLanguage.Vietnamese, "AccountName", "AccountName01").ToString & sUnicode & " as AccountName, GroupID, 1 as DisplayOrder " & vbCrLf
        sSQL &= "From D90T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Disabled = 0 " & vbCrLf
        If sClauseWhere <> "" Then
            sSQL &= "And (" & sClauseWhere & ") " & vbCrLf
        End If
        sSQL &= "Order By DisplayOrder, AccountID "

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Load Combo Tài khoản (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccount(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountIDAndOffAccount(, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountIDAndOffAccountAll(, bUseUnicode), bUseUnicode)
    End Sub

    Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountIDAndOffAccountAll(sClauseWhere, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Combo Tài khoản (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccount(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccount(, bUseUnicode)
        LoadDataSource(tdbcFrom, dt, bUseUnicode)
        LoadDataSource(tdbcTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Combo Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccountAll(, bUseUnicode)
        LoadDataSource(tdbcFrom, dt, bUseUnicode)
        LoadDataSource(tdbcTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Tài khoản (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccount(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableAccountIDAndOffAccount(sClauseWhere, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Tài khoản (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sClauseWhere"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccount(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccount(sClauseWhere, bUseUnicode)
        LoadDataSource(tdbcFrom, dt, bUseUnicode)
        LoadDataSource(tdbcTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sClauseWhere"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccountAll(sClauseWhere, bUseUnicode)
        LoadDataSource(tdbcFrom, dt, bUseUnicode)
        LoadDataSource(tdbcTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccount(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountIDAndOffAccount(, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountIDAndOffAccountAll(, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccount(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccount(, bUseUnicode)
        LoadDataSource(tdbdFrom, dt, bUseUnicode)
        LoadDataSource(tdbdTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng)
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccountAll(, bUseUnicode)
        LoadDataSource(tdbdFrom, dt, bUseUnicode)
        LoadDataSource(tdbdTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where 
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccount(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountIDAndOffAccount(sClauseWhere, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where 
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="sClauseWhere"> Điều kiện where (ví dụ: GroupID in ('1', '13'))</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableAccountIDAndOffAccountAll(sClauseWhere, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <param name="sClauseWhere"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccount(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccount(sClauseWhere, bUseUnicode)
        LoadDataSource(tdbdFrom, dt, bUseUnicode)
        LoadDataSource(tdbdTo, dt.Copy, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 Dropdown Tài khoản có chứa % (bao gồm Tài khoản trong và ngoại bảng) có truyền thêm điều kiện Where
    ''' </summary>
    ''' <param name="tdbdFrom"></param>
    ''' <param name="tdbdTo"></param>
    ''' <param name="sClauseWhere"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadAccountIDAndOffAccountAll(ByVal tdbdFrom As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdTo As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sClauseWhere As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        dt = ReturnTableAccountIDAndOffAccountAll(sClauseWhere, bUseUnicode)
        LoadDataSource(tdbdFrom, dt, bUseUnicode)
        LoadDataSource(tdbdTo, dt.Copy, bUseUnicode)
    End Sub

#End Region

#Region "Loại tiền"

    ''' <summary>
    ''' Trả ra Table Loại tiền
    ''' </summary>
    ''' <returns></returns>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableCurrencyID(Optional ByVal bUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "--Do nguon cho loai tien" & vbCrLf
        sSQL &= "Select CurrencyID, " & IIf(bUnicode, "CurrencyNameU", "CurrencyName").ToString & " As CurrencyName, ExchangeRate, Operator, MethodID, OriginalDecimal, ExchangeRateDecimal, UnitPriceDecimals  "
        sSQL &= "From D91V0010 "
        sSQL &= "Where Disabled =0 "
        sSQL &= "Order By CurrencyID "

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Trả về table Loại tiền có chứa %
    ''' </summary>
    ''' <returns></returns>
    ''' <param name="bUseUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableCurrencyIDAll(Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sUnicode As String = ""
        Dim sAll As String = ""
        UnicodeAllString(sUnicode, sAll, bUseUnicode)

        Dim sSQL As String = "--Do nguon cho loai tien" & vbCrLf
        sSQL &= "Select CurrencyID, CurrencyName" & sUnicode & " As CurrencyName, ExchangeRate, Operator, MethodID, OriginalDecimal, ExchangeRateDecimal,UnitPriceDecimals "
        sSQL &= ", 1 AS DisplayOrder "
        sSQL &= "From D91V0010 Where Disabled =0 " & vbCrLf
        sSQL &= "Union All" & vbCrLf
        sSQL &= "Select '%' as CurrencyID, " & sAll & " As CurrencyName, 1 as ExchangeRate, 0 as Operator, '' as MethodID, 0 as OriginalDecimal, 0 as  ExchangeRateDecimal, 0 as UnitPriceDecimals" & vbCrLf
        sSQL &= ", 0 AS DisplayOrder "
        sSQL &= "Order By DisplayOrder, CurrencyID "

        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Load Combo Loại tiền
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCurrencyID(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableCurrencyID(bUnicode), bUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Loại tiền có chứa %
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCurrencyIDAll(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableCurrencyIDAll(bUnicode), bUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Loại tiền
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCurrencyID(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableCurrencyID(bUnicode), bUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Loại tiền có chứa %
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCurrencyIDAll(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbd, ReturnTableCurrencyIDAll(bUnicode), bUnicode)
    End Sub

    ''' <summary>
    ''' Load Combo Loại tiền lấy tỷ giá theo ngày (module D01, D05, ... )
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCurrencyIDToDate(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sDate As String, Optional ByVal bUnicode As Boolean = False)
        Dim sSQL As String = ""
        sSQL = "Select CurrencyID, " & IIf(bUnicode, "CurrencyNameU", "CurrencyName").ToString & " As CurrencyName, "
        sSQL &= "ISNULL(( SELECT TOP 1 D01.ExchangeRate FROM D01T0060 D01 WITH(NOLOCK) WHERE (D01.CurrencyID = D91.CurrencyID) "
        sSQL &= "AND D01.HisCurrencyDate <= " & SQLDateSave(sDate) & " "
        sSQL &= " ORDER BY HisCurrencyDate DESC),ExchangeRate) AS ExchangeRate, "
        sSQL &= "Operator, OriginalDecimal, ExchangeRateDecimal "
        sSQL &= "From D91V0010 D91 "
        sSQL &= "Where D91.Disabled =0 "
        sSQL &= "Order By D91.CurrencyID "

        LoadDataSource(tdbc, sSQL, bUnicode)
    End Sub

    ''' <summary>
    ''' Load Dropdown Loại tiền lấy tỷ giá theo ngày (module D01, D05, ... )
    ''' </summary>
    ''' <param name="tdbd"></param>
    ''' <param name="bUnicode"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCurrencyIDToDate(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sDate As String, Optional ByVal bUnicode As Boolean = False)
        Dim sSQL As String = ""
        sSQL = "Select CurrencyID, " & IIf(bUnicode, "CurrencyNameU", "CurrencyName").ToString & " As CurrencyName, "
        sSQL &= "ISNULL(( SELECT TOP 1 D01.ExchangeRate FROM D01T0060 D01 WITH(NOLOCK) WHERE (D01.CurrencyID = D91.CurrencyID) "
        sSQL &= "AND D01.HisCurrencyDate <= " & SQLDateSave(sDate) & " "
        sSQL &= " ORDER BY HisCurrencyDate DESC),ExchangeRate) AS ExchangeRate, "
        sSQL &= "Operator, OriginalDecimal, ExchangeRateDecimal "
        sSQL &= "From D91V0010 D91 "
        sSQL &= "Where D91.Disabled =0 "
        sSQL &= "Order By D91.CurrencyID "

        LoadDataSource(tdbd, sSQL, bUnicode)
    End Sub
#End Region

#Region "Mẫu chuẩn"

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD89P2000
    '# Created User: Nguyễn Thị Ánh
    '# Created Date: 15/10/2013 02:31:11
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD89P2000(ByVal sModuleID As String, ByVal IsCustomized As Integer, Optional ByVal sReportTypeID As String = "") As String
        Dim sSQL As String = ""
        If IsCustomized = 0 Then
            sSQL &= ("-- -- Do nguon combo Mau chuan" & vbCrLf)
        Else
            sSQL &= ("-- -- Do nguon combo Dac thu" & vbCrLf)
        End If
        sSQL &= "Exec D89P2000 "
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[50], NOT NULL
        sSQL &= SQLString(sModuleID) & COMMA 'ModuleID, varchar[20], NOT NULL
        sSQL &= SQLString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLNumber(IsCustomized) & COMMA 'IsCustomized, tinyint, NOT NULL
        sSQL &= SQLString(sReportTypeID)
        Return sSQL
    End Function

    ''' <summary>
    ''' Load data of tdbcReport standard
    ''' </summary>
    ''' <param name="sModuleID">xx</param>
    ''' <param name="sReportTypeID">Report type</param>
    ''' <remarks></remarks>
    Public Function ReturnTableStandardReport(ByVal sModuleID As String, Optional ByVal sReportTypeID As String = "", Optional ByVal bUnicode As Boolean = False, Optional ByVal IsCustomized As Integer = 0, Optional ByVal bUseD89P2000 As Boolean = False) As DataTable
        If bUseD89P2000 Then Return ReturnDataTable(SQLStoreD89P2000(sModuleID, IsCustomized, sReportTypeID))
        '*******************
        Dim sSQL As String = "--Do nguon Mau chuan" & vbCrLf

        sSQL &= "SELECT ReportID, ReportType, "
        If bUnicode = False Then
            sSQL &= IIf(geLanguage = EnumLanguage.Vietnamese, "ReportName", "ReportName01").ToString & " as ReportName" & vbCrLf
        Else
            sSQL &= IIf(geLanguage = EnumLanguage.Vietnamese, "ReportNameU", "ReportName01U").ToString & " as ReportName" & vbCrLf
        End If
        sSQL &= "FROM D91T0100 WITH(NOLOCK) " & vbCrLf
        sSQL &= "WHERE	ModuleID = " & SQLString(sModuleID)
        If sReportTypeID <> "" Then
            sSQL &= " And ReportType = " & SQLString(sReportTypeID) & vbCrLf
        End If
        sSQL &= "ORDER BY 	ReportID" & vbCrLf
        Return ReturnDataTable(sSQL)
    End Function


    Public Sub LoadtdbcStandardReport(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sModuleID As String, ByVal sReportTypeID As String, Optional ByVal txtTitle As TextBox = Nothing, Optional ByVal bUnicode As Boolean = False, Optional ByVal bUseD89P2000 As Boolean = False)
        Dim dtReport As DataTable = ReturnTableStandardReport(sModuleID, sReportTypeID, bUnicode, , bUseD89P2000)
        LoadDataSource(tdbc, dtReport, bUnicode)

        tdbc.EditorBackColor = COLOR_BACKCOLOROBLIGATORY

        tdbc.SelectedIndex = 0

        If txtTitle IsNot Nothing Then
            txtTitle.Enabled = True
            txtTitle.ReadOnly = False
            txtTitle.TabStop = True
            txtTitle.BackColor = COLOR_BACKCOLOROBLIGATORY
            txtTitle.CharacterCasing = CharacterCasing.Upper
        End If

    End Sub

    Public Sub LoadtdbcStandardReport(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sReportTypeID As String, Optional ByVal txtTitle As TextBox = Nothing, Optional ByVal bUnicode As Boolean = False)
        LoadDataSource(tdbc, ReturnTableFilter(dt, "ReportType =" & SQLString(sReportTypeID)), bUnicode)

        tdbc.EditorBackColor = COLOR_BACKCOLOROBLIGATORY

        tdbc.SelectedIndex = 0

        If txtTitle IsNot Nothing Then
            txtTitle.Enabled = True
            txtTitle.ReadOnly = False
            txtTitle.TabStop = True
            txtTitle.BackColor = COLOR_BACKCOLOROBLIGATORY
            txtTitle.CharacterCasing = CharacterCasing.Upper
        End If

    End Sub

#End Region

#Region "In theo năm tài chính"

    Public Function UsePrintOffset() As Boolean
        Return L3Bool(ReturnScalar("Select PeriodOffset From D91T9102 WITH(NOLOCK) "))
    End Function
#End Region


#Region "Đặc thù"
    ''' <summary>
    ''' Load data of tdbcCustomize
    ''' </summary>
    ''' <param name="tdbc">combo name</param>
    ''' <param name="sModuleID">xx</param>
    ''' <param name="sReportTypeID"></param>
    ''' <param name="txtTitle">textbox đi theo combo Đặc thù</param>
    ''' <remarks></remarks>
    Public Sub LoadtdbcCustomizeReport(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sModuleID As String, ByVal sReportTypeID As String, Optional ByVal txtTitle As TextBox = Nothing, Optional ByVal bUseUnicode As Boolean = False, Optional ByVal bUseD89P2000 As Boolean = False)
        If bUseD89P2000 Then
            LoadDataSource(tdbc, ReturnTableStandardReport(sModuleID, sReportTypeID, gbUnicode, 1, True), gbUnicode)
        Else
            Dim sSQL As String = "-- Do nguon Bao cao dac thu" & vbCrLf
            sSQL &= "Select ReportID, "
            'Update 17/02/2011: Dùng biến chung gbUnicode
            If gbUnicode = False Then
                sSQL &= IIf(geLanguage = EnumLanguage.Vietnamese, "Title", "Title01").ToString & " as Title " & vbCrLf
            Else
                sSQL &= IIf(geLanguage = EnumLanguage.Vietnamese, "TitleU", "Title01U").ToString & " as Title " & vbCrLf
            End If

            sSQL &= "From D89T1000 WITH(NOLOCK) Where Disabled =0 And ModuleID = " & SQLString(sModuleID) & " And ReportTypeID = " & SQLString(sReportTypeID) & _
                                    " AND (DAGroupID = '' OR DAGroupID IN " & _
                                    " (SELECT DAGroupID FROM LEMONSYS.DBO.D00V0080 WHERE UserID = " & SQLString(gsUserID) & ")" & _
                                    " OR " & SQLString(gsUserID) & " = 'LEMONADMIN')" & vbCrLf
            sSQL &= "Order by ReportID"
            LoadDataSource(tdbc, sSQL, gbUnicode)
        End If

        If txtTitle IsNot Nothing Then txtTitle.CharacterCasing = CharacterCasing.Upper
    End Sub
#End Region

#Region "Đơn vị"

    ''' <summary>
    ''' Lấy trạng thái của ModuleAdmin của D00 
    ''' </summary>
    ''' <param name="ModuleID">Mã module(Dxx)</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub GetModuleAdmin(ByVal ModuleID As String)
        giModuleAdmin = 0

        Dim Sql As String = "--Lay trang thai ModuleAdmin cua D00" & vbCrLf
        Sql &= "SELECT ModuleAdmin FROM Lemonsys.dbo.D00T0031 D03 WITH(NOLOCK) INNER JOIN Lemonsys.dbo.D00T0131 D13 WITH(NOLOCK) ON D03.ACCESSID = D13.ACCESSID "
        Sql &= "WHERE UserID = " & SQLString(gsUserID) & " AND CompanyID = " & SQLString(gsCompanyID) & " AND ModuleID = " & SQLString(ModuleID)

        giModuleAdmin = Convert.ToInt16(ReturnScalar(Sql))

    End Sub

    ''' <summary>
    ''' Load dữ liệu cho combo Đơn vị ở Báo cáo
    ''' </summary>
    ''' <param name="tdbc">Tên Combo Đơn vị</param>
    ''' <param name="sModuleID">Mã module (Dxx)</param>
    ''' <param name="bG4">True: là nhóm G4</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboDivisionIDReport(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal bG4 As Boolean = False, Optional ByVal bUseUnicode As Boolean = False)
        Dim sUnicode As String = ""
        Dim sAll As String = ""
        UnicodeAllString(sUnicode, sAll, bUseUnicode)

        Dim sSQL As String = "--Do nguon Don vi Bao cao" & vbCrLf
        sSQL &= "Select Distinct T99.DivisionID as DivisionID, T16.DivisionName" & sUnicode & " as DivisionName "
        sSQL &= ", 1 AS DisplayOrder "
        sSQL &= " From " & sModuleID & "T9999 T99 WITH(NOLOCK) Inner Join D91T0016 T16 WITH(NOLOCK) On T99.DivisionID = T16.DivisionID "
        If bG4 Then ' Nếu là nhóm G4 thì lấy table D91T0061
            sSQL &= " Inner Join D91T0061 T60 WITH(NOLOCK) On T99.DivisionID = T60.DivisionID "
        Else
            sSQL &= " Inner Join D91T0060 T60 WITH(NOLOCK) On T99.DivisionID = T60.DivisionID "
        End If
        sSQL &= " Where T16.Disabled = 0 And T60.UserID = '" & gsUserID & "' "
        If giModuleAdmin = 1 Then
            sSQL &= " Union All " & vbCrLf
            sSQL &= " Select '%' as DivisionID, " & sAll & " as DivisionName "
            sSQL &= ", 0 AS DisplayOrder "
        End If
        sSQL &= " Order By DisplayOrder, T99.DivisionID"

        LoadDataSource(tdbc, sSQL, bUseUnicode)
    End Sub

    ''' <summary>
    ''' Trả về 1 table để load dữ liệu cho Đơn vị
    ''' </summary>
    ''' <param name="sModuleID">Mã module (Dxx)</param>
    ''' <param name="bG4">True: là nhóm G4</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableDivisionID(ByVal sModuleID As String, Optional ByVal bG4 As Boolean = False, Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "--Do nguon Don vi" & vbCrLf
        sSQL &= "Select Distinct T99.DivisionID as DivisionID, " & IIf(bUseUnicode, "T16.DivisionNameU", "T16.DivisionName").ToString & " as DivisionName"
        sSQL &= " From " & sModuleID & "T9999 T99 WITH(NOLOCK) Inner Join D91T0016 T16 WITH(NOLOCK) On T99.DivisionID = T16.DivisionID "
        If bG4 Then ' Nếu là nhóm G4 thì lấy table D91T0061
            sSQL &= " Inner Join D91T0061 T60 WITH(NOLOCK) On T99.DivisionID = T60.DivisionID "
        Else
            sSQL &= " Inner Join D91T0060 T60 WITH(NOLOCK) On T99.DivisionID = T60.DivisionID "
        End If
        sSQL &= " Where T16.Disabled = 0 And T60.UserID = '" & gsUserID & "'"
        sSQL &= " Order By T99.DivisionID"

        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)
        Return dt
    End Function

    ''' <summary>
    ''' Trả về 1 table để load dữ liệu cho Đơn vị có chứa %
    ''' </summary>
    ''' <param name="sModuleID">Mã module (Dxx)</param>
    ''' <param name="bG4">True: là nhóm G4</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableDivisionIDAll(ByVal sModuleID As String, Optional ByVal bG4 As Boolean = False, Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sUnicode As String = ""
        Dim sAll As String = ""
        UnicodeAllString(sUnicode, sAll, bUseUnicode)

        Dim sSQL As String = "--Do nguon Don vi" & vbCrLf
        sSQL &= "Select Distinct T99.DivisionID as DivisionID, T16.DivisionName" & sUnicode & "  as DivisionName"
        sSQL &= ", 1 AS DisplayOrder "
        sSQL &= " From " & sModuleID & "T9999 T99 WITH(NOLOCK) Inner Join D91T0016 T16 WITH(NOLOCK) On T99.DivisionID = T16.DivisionID "
        If bG4 Then ' Nếu là nhóm G4 thì lấy table D91T0061
            sSQL &= " Inner Join D91T0061 T60 WITH(NOLOCK) On T99.DivisionID = T60.DivisionID "
        Else
            sSQL &= " Inner Join D91T0060 T60 WITH(NOLOCK) On T99.DivisionID = T60.DivisionID "
        End If
        sSQL &= " Where T16.Disabled = 0 And T60.UserID = '" & gsUserID & "'"
        sSQL &= " Union All " & vbCrLf
        sSQL &= " Select '%' as DivisionID," & sAll & " as DivisionName "
        sSQL &= ", 0 AS DisplayOrder "
        sSQL &= " Order By DisplayOrder, T99.DivisionID"

        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)
        Return dt
    End Function

    ''' <summary>
    ''' Load dữ liệu cho Combo Đơn vị (không ở Báo cáo) 
    ''' </summary>
    ''' <param name="tdbcDivisionID"></param>
    ''' <param name="sModuleID">Mã module (Dxx)</param>
    ''' <param name="bG4">True: là nhóm G4</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboDivisionID(ByVal tdbcDivisionID As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal bG4 As Boolean = False, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbcDivisionID, ReturnTableDivisionID(sModuleID, bG4, bUseUnicode), bUseUnicode)
    End Sub

    ''' <summary>
    ''' Load dữ liệu cho Combo Đơn vị có chứa % (không ở Báo cáo) 
    ''' </summary>
    ''' <param name="tdbcDivisionID"></param>
    ''' <param name="sModuleID">Mã module (Dxx)</param>
    ''' <param name="bG4">True: là nhóm G4</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboDivisionIDAll(ByVal tdbcDivisionID As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal bG4 As Boolean = False, Optional ByVal bUseUnicode As Boolean = False)
        LoadDataSource(tdbcDivisionID, ReturnTableDivisionIDAll(sModuleID, bG4, bUseUnicode), bUseUnicode)
    End Sub

#End Region

#Region "Kỳ kế toán"

    ''' <summary>
    ''' Load dữ liệu vào bảng Table gồm tất cả các kỳ kế toán của Báo cáo
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <returns></returns>
    ''' <remarks>Khai báo 1 DataTable toàn cục của form để lấy hàm này</remarks>
    Public Function LoadTablePeriodReport(ByVal sModuleID As String, Optional ByVal bCallOption As Boolean = False) As DataTable
        'Update 14/01/2011: Lấy quyền cho kỳ 13
        Dim bUsePeriod13 As Boolean = False
        If bCallOption Then
            bUsePeriod13 = ReturnPermission(sModuleID & "F5707") > 0
        Else
            If giTranMonth = 13 Then
                bUsePeriod13 = ReturnPermission(sModuleID & "F5707") > 0
            End If
        End If

        Dim sSQL As String = "--Load cac ky cua Bao cao" & vbCrLf
        sSQL &= " Select REPLACE(STR(TranMonth, 2), ' ', '0') + '/' + STR(TranYear, 4) AS Period, TranMonth, TranYear, DivisionID,TranMonth+ TranYear*100  as TempCol "
        sSQL &= " From " & sModuleID & "T9999 WITH(NOLOCK) " & vbCrLf
        If bUsePeriod13 Then
            sSQL &= " UNION ALL"
            sSQL &= " Select '13' + '/' + STR(TranYear, 4)AS Period,"
            sSQL &= " 13 as TranMonth, TranYear, DivisionID, 13+ TranYear*100  as TempCol  "
            sSQL &= " From " & sModuleID & "T9999 WITH(NOLOCK) "
            sSQL &= " Where TranMonth = 12" & vbCrLf
        End If
        sSQL &= " UNION ALL"
        sSQL &= " Select DISTINCT "
        sSQL &= " REPLACE(STR(TranMonth, 2), ' ', '0') + '/' + STR(TranYear, 4) AS Period, TranMonth, TranYear, '%' AS DivisionID,TranMonth+ TranYear*100  as TempCol "
        sSQL &= " From " & sModuleID & "T9999 WITH(NOLOCK) " & vbCrLf
        If bUsePeriod13 Then
            sSQL &= " UNION ALL"
            sSQL &= " Select DISTINCT '13' + '/' + STR(TranYear, 4)AS Period,"
            sSQL &= " 13 as TranMonth, TranYear, '%' as DivisionID, 13+ TranYear*100  as TempCol  "
            sSQL &= " From " & sModuleID & "T9999 WITH(NOLOCK) "
            sSQL &= " Where TranMonth = 12" & vbCrLf
        End If
        sSQL &= " Order By DivisionID, TranYear DESC, TranMonth DESC"

        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)

        Return dt

    End Function

    ''' <summary>
    ''' Load combo Kỳ kế toán mà trên màn hình có combo Đơn vị
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <param name="dt">dt này được lấy từ hàm LoadTablePeriodReport để tại Form_load</param>
    ''' <param name="sDivisionID">Giá trị của combo Đơn vị trên màn hình</param>
    ''' <remarks></remarks>
    Public Sub LoadCboPeriodReport(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        LoadDataSource(tdbc, ReturnTableFilter(dt, "DivisionID =" & SQLString(sDivisionID)))
    End Sub

    ''' <summary>
    ''' Load 2 combo Kỳ kế toán màn trên màn hình có combo Đơn vị
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="dt"></param>
    ''' <param name="sDivisionID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboPeriodReport(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        Dim dtF As DataTable
        dtF = ReturnTableFilter(dt, "DivisionID =" & SQLString(sDivisionID))
        LoadDataSource(tdbcFrom, dtF)
        LoadDataSource(tdbcTo, dtF.Copy)
    End Sub

    ''' <summary>
    ''' Load 4 combo Kỳ kế toán màn trên màn hình có combo Đơn vị
    ''' </summary>
    ''' <param name="tdbcFrom1"></param>
    ''' <param name="tdbcTo1"></param>
    ''' <param name="tdbcFrom2"></param>
    ''' <param name="tdbcTo2"></param>
    ''' <param name="dt"></param>
    ''' <param name="sDivisionID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
   Public Sub LoadCboPeriodReport(ByVal tdbcFrom1 As C1.Win.C1List.C1Combo, ByVal tdbcTo1 As C1.Win.C1List.C1Combo, ByVal tdbcFrom2 As C1.Win.C1List.C1Combo, ByVal tdbcTo2 As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        Dim dtF As DataTable
        dtF = ReturnTableFilter(dt, "DivisionID =" & SQLString(sDivisionID))
        LoadDataSource(tdbcFrom1, dtF)
        LoadDataSource(tdbcTo1, dtF.Copy)
        LoadDataSource(tdbcFrom2, dtF.Copy)
        LoadDataSource(tdbcTo2, dtF.Copy)
    End Sub

    ''' <summary>
    ''' Trả ra bảng dữ liệu cho Period ở Private
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnTablePeriod(ByVal sModuleID As String, Optional ByVal sDivisionID As String = "") As DataTable
        If sDivisionID = "" Then sDivisionID = gsDivisionID
        'Update 14/01/2011: Lấy quyền cho kỳ 13
        Dim bUsePeriod13 As Boolean = False
        If giTranMonth = 13 Then
            bUsePeriod13 = ReturnPermission(sModuleID & "F5707") > 0
        End If
        Dim sSQL As String = "--Do nguon Ky" & vbCrLf
        sSQL &= " Select REPLACE(STR(TranMonth, 2), ' ', '0') + '/' + STR(TranYear, 4) AS Period, TranMonth, TranYear "
        sSQL &= " From " & sModuleID & "T9999 WITH(NOLOCK) "
        sSQL &= " Where DivisionID = " & SQLString(sDivisionID)
        If bUsePeriod13 Then
            sSQL &= " UNION ALL"
            sSQL &= " Select DISTINCT '13' + '/' + STR(TranYear, 4)AS Period,"
            sSQL &= " 13 as TranMonth, TranYear  "
            sSQL &= " From " & sModuleID & "T9999 WITH(NOLOCK) " & vbCrLf
            sSQL &= " Where DivisionID = " & SQLString(sDivisionID)
            sSQL &= " And TranMonth = 12" & vbCrLf
        End If

        sSQL &= " Order By TranYear DESC, TranMonth DESC"
        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)

        Return dt
    End Function

    ''' <summary>
    ''' Load combo Kỳ kế toán mà trên màn hình không có combo Đơn vị
    ''' </summary>
    ''' <param name="tdbc"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboPeriodReport(ByVal tdbc As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal sDivisionID As String = "")
        LoadDataSource(tdbc, ReturnTablePeriod(sModuleID, sDivisionID))
    End Sub

    ''' <summary>
    ''' Load 2 combo Kỳ kế toán mà trên màn hình không có combo Đơn vị
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboPeriodReport(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal sDivisionID As String = "")
        Dim dt As DataTable
        dt = ReturnTablePeriod(sModuleID, sDivisionID)
        LoadDataSource(tdbcFrom, dt)
        LoadDataSource(tdbcTo, dt.Copy)
    End Sub

    ''' <summary>
    ''' Load 4 combo Kỳ kế toán mà trên màn hình không có combo Đơn vị
    ''' </summary>
    ''' <param name="tdbcFrom1"></param>
    ''' <param name="tdbcTo1"></param>
    ''' <param name="tdbcFrom2"></param>
    ''' <param name="tdbcTo2"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboPeriodReport(ByVal tdbcFrom1 As C1.Win.C1List.C1Combo, ByVal tdbcTo1 As C1.Win.C1List.C1Combo, ByVal tdbcFrom2 As C1.Win.C1List.C1Combo, ByVal tdbcTo2 As C1.Win.C1List.C1Combo, ByVal sModuleID As String, Optional ByVal sDivisionID As String = "")
        Dim dt As DataTable
        dt = ReturnTablePeriod(sModuleID, sDivisionID)
        LoadDataSource(tdbcFrom1, dt)
        LoadDataSource(tdbcTo1, dt.Copy)
        LoadDataSource(tdbcFrom2, dt.Copy)
        LoadDataSource(tdbcTo2, dt.Copy)
    End Sub

#End Region

#Region "Năm"

    Public Function LoadTableYearReport(ByVal sModuleID As String) As DataTable
        Dim sSQL As String = ""
        sSQL = "SELECT 	Distinct 	TranYear as Year , DivisionID, 1 AS DisplayOrder" & vbCrLf
        sSQL &= "FROM 	" & sModuleID & "T9999 WITH(NOLOCK) " & vbCrLf
        sSQL &= "UNION  SELECT 	Distinct 	TranYear as Year , '%' as DivisionID, 0 AS DisplayOrder" & vbCrLf
        sSQL &= "FROM 	" & sModuleID & "T9999 WITH(NOLOCK) " & vbCrLf
        sSQL &= "ORDER BY DisplayOrder, DivisionID,	TranYear desc"
        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Trả ra bảng dữ liệu cho combo Năm theo Đơn vị truyền vào
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <param name="sDivisionID">Giá trị của Combo Đơn vị</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnTableYear(ByVal sModuleID As String, ByVal sDivisionID As String) As DataTable
        Dim sSQL As String = "--Do nguon Nam" & vbCrLf
        sSQL &= " SELECT DISTINCT TranYear AS Year, DivisionID "
        sSQL &= " FROM " & sModuleID & "T9999  WITH(NOLOCK) "
        If sDivisionID = "" Then
            sSQL &= " WHERE DivisionID = " & SQLString("%")
        Else
            sSQL &= " WHERE DivisionID = " & SQLString(sDivisionID)
        End If
        sSQL &= " ORDER BY 	TranYear DESC"
        Return ReturnDataTable(sSQL)
    End Function


    ''' <summary>
    ''' Trả ra bảng dữ liệu cho combo Năm theo Đơn vị mặc định
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnTableYear(ByVal sModuleID As String) As DataTable
        Return ReturnTableYear(sModuleID, gsDivisionID)
    End Function

    '''' <summary>
    '''' Load  combo Năm theo Đơn vị mặc định
    '''' </summary>
    '''' </summary>
    '''' <param name="tdbcFrom"></param>
    '''' <param name="sModuleID"></param>
    '''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboYear(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal sModuleID As String)
        Dim dt As DataTable
        dt = ReturnTableYear(sModuleID)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load  combo Năm theo Đơn vị truyền vào
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="sModuleID"></param>
    ''' <param name="sDivisionID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboYear(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal sModuleID As String, ByVal sDivisionID As String)
        Dim dt As DataTable
        dt = ReturnTableYear(sModuleID, sDivisionID)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
    End Sub

    Public Sub LoadCboYear(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        Dim dtF As DataTable
        dtF = ReturnTableFilter(dt, "DivisionID = " & SQLString(sDivisionID), True)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Load  combo Năm theo Đơn vị mặc định
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboYear(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sModuleID As String)
        Dim dt As DataTable
        dt = ReturnTableYear(sModuleID)
        LoadDataSource(tdbcFrom, dt.DefaultView.ToTable, gbUnicode)
        LoadDataSource(tdbcTo, dt.DefaultView.ToTable, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Load  combo Năm theo Đơn vị truyền vào
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboYear(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sModuleID As String, ByVal sDivisionID As String)
        Dim dt As DataTable
        dt = ReturnTableYear(sModuleID, sDivisionID)
        LoadDataSource(tdbcFrom, dt.DefaultView.ToTable, gbUnicode)
        LoadDataSource(tdbcTo, dt.DefaultView.ToTable, gbUnicode)
    End Sub

    Public Sub LoadCboYear(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        Dim dtF As DataTable
        dtF = ReturnTableFilter(dt, "DivisionID = " & SQLString(sDivisionID), True)
        LoadDataSource(tdbcFrom, dtF.DefaultView.ToTable, gbUnicode)
        LoadDataSource(tdbcTo, dtF.DefaultView.ToTable, gbUnicode)
    End Sub
#End Region

#Region "Quý"
    Public Function LoadTableQuarterReport(ByVal sModuleID As String) As DataTable
        Dim sSQL As New StringBuilder()
        sSQL.Append("SELECT Distinct Right(('0'+ RTrim(LTrim(Str(Quarter)))),2) + '/' + LTrim(Str(TranYear)) As ")
        sSQL.Append(" Period, Quarter, TranYear ,DivisionID, 1 AS DisplayOrder" & vbCrLf)
        sSQL.Append(" FROM " & sModuleID & "T9999  WITH(NOLOCK) " & vbCrLf)
        sSQL.Append("UNION Select DISTINCT Right(('0'+ RTrim(LTrim(Str(Quarter)))),2) + '/' +  LTrim(Str(TranYear)) AS ")
        sSQL.Append(" Period, Quarter, TranYear, '%' AS DivisionID, 0 AS DisplayOrder" & vbCrLf)
        sSQL.Append(" FROM " & sModuleID & "T9999  WITH(NOLOCK) " & vbCrLf)
        sSQL.Append("ORDER BY DisplayOrder, DIVISIONID, TranYear desc, Quarter Desc" & vbCrLf)

        Return ReturnDataTable(sSQL.ToString)
    End Function

    ''' <summary>
    ''' Trả ra bảng dữ liệu cho combo Quý theo Đơn vị truyền vào
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <param name="sDivisionID">Giá trị của Combo Đơn vị</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnTableQuarter(ByVal sModuleID As String, ByVal sDivisionID As String) As DataTable
        Dim sSQL As New System.Text.StringBuilder()
        sSQL.Append("--Do nguon Quy" & vbCrLf)
        sSQL.Append("SELECT Distinct Right(('0'+ RTrim(LTrim(Str(Quarter)))),2) + '/' + LTrim(Str(TranYear)) As ")
        sSQL.Append(" Period, Quarter, TranYear, DivisionID " & vbCrLf)
        sSQL.Append(" FROM " & sModuleID & "T9999 WITH(NOLOCK)  " & vbCrLf)
        If sDivisionID = "" Then
            sSQL.Append(" WHERE DivisionID = " & SQLString("%"))
        Else
            sSQL.Append(" WHERE DivisionID = " & SQLString(sDivisionID))
        End If
        sSQL.Append(" ORDER BY TranYear desc, Quarter Desc" & vbCrLf)

        Return ReturnDataTable(sSQL.ToString)
    End Function


    ''' <summary>
    ''' Trả ra bảng dữ liệu cho combo Quý theo Đơn vị mặc định
    ''' </summary>
    ''' <param name="sModuleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnTableQuarter(ByVal sModuleID As String) As DataTable
        Return ReturnTableQuarter(sModuleID, gsDivisionID)
    End Function

    '''' <summary>
    '''' Load  combo Quý theo Đơn vị mặc định
    '''' </summary>
    '''' </summary>
    '''' <param name="tdbcFrom"></param>
    '''' <param name="sModuleID"></param>
    '''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboQuarter(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal sModuleID As String)
        Dim dt As DataTable
        dt = ReturnTableQuarter(sModuleID)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load  combo Quý theo Đơn vị truyền vào
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="sModuleID"></param>
    ''' <param name="sDivisionID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboQuarter(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal sModuleID As String, ByVal sDivisionID As String)
        Dim dt As DataTable
        dt = ReturnTableQuarter(sModuleID, sDivisionID)
        LoadDataSource(tdbcFrom, dt, gbUnicode)
    End Sub

    Public Sub LoadCboQuarter(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        Dim dtF As DataTable
        dtF = ReturnTableFilter(dt, "DivisionID = " & SQLString(sDivisionID), True)
        LoadDataSource(tdbcFrom, dtF, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Load  combo Quý theo Đơn vị mặc định
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboQuarter(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sModuleID As String)
        Dim dt As DataTable
        dt = ReturnTableQuarter(sModuleID)
        LoadDataSource(tdbcFrom, dt.DefaultView.ToTable, gbUnicode)
        LoadDataSource(tdbcTo, dt.DefaultView.ToTable, gbUnicode)
    End Sub

    ''' <summary>
    ''' Load 2 combo Load combo Quý theo Đơn vị truyền vào
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <param name="sModuleID"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadCboQuarter(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal sModuleID As String, ByVal sDivisionID As String)
        Dim dt As DataTable
        dt = ReturnTableQuarter(sModuleID, sDivisionID)
        LoadDataSource(tdbcFrom, dt.DefaultView.ToTable, gbUnicode)
        LoadDataSource(tdbcTo, dt.DefaultView.ToTable, gbUnicode)
    End Sub

    Public Sub LoadCboQuarter(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, ByVal dt As DataTable, ByVal sDivisionID As String)
        Dim dtF As DataTable
        dtF = ReturnTableFilter(dt, "DivisionID = " & SQLString(sDivisionID), True)
        LoadDataSource(tdbcFrom, dtF.DefaultView.ToTable, gbUnicode)
        LoadDataSource(tdbcTo, dtF.DefaultView.ToTable, gbUnicode)
    End Sub
#End Region


#Region "Các Function convert đúng kiểu dữ liệu"

    ''' <summary>
    ''' Trả về Số Double
    ''' </summary>
    ''' <param name="Text">Chuỗi truyền vào</param>
    <DebuggerStepThrough()> _
    Public Function Number(ByVal Text As String) As Double
        Text = Text.Replace(",", "")
        Text = Text.Replace(" ", "")
        Text = Text.Replace("%", "")
        If Text = "" Then Return 0
        Return Convert.ToDouble(Text)
    End Function

    ''' <summary>
    ''' Trả về Số Double đã format
    ''' </summary>
    ''' <param name="Text">Chuỗi truyền vào</param>
    ''' <param name="StringFormat">Format số</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function Number(ByVal Text As Object, Optional ByVal StringFormat As String = "") As Double
        If StringFormat <> "" Then Text = SQLNumber(Text, StringFormat)
        Return Number(Text.ToString)
    End Function


    <DebuggerStepThrough()> _
    Public Function L3Int(ByVal Text As String) As Integer
        Text = Text.Replace(",", "")
        Text = Text.Replace(" ", "")
        Text = Text.Replace("False", "0")
        Text = Text.Replace("True", "1")
        If Text = "" Then Return 0
        Return CType(Text, Integer)
    End Function

    <DebuggerStepThrough()> _
    Public Function L3Int(ByVal Text As Object) As Integer
        Return L3Int(Text.ToString)
    End Function

    <DebuggerStepThrough()> _
  Public Function L3Byte(ByVal Text As String) As Byte
        Text = Text.Replace(",", "")
        Text = Text.Replace(" ", "")
        Text = Text.Replace("False", "0")
        Text = Text.Replace("True", "1")
        If Text = "" Then Return 0
        Return CType(Text, Byte)
    End Function

    <DebuggerStepThrough()> _
    Public Function L3Byte(ByVal Text As Object) As Byte
        Return L3Byte(Text.ToString)
    End Function

    <DebuggerStepThrough()> _
    Public Function L3Bool(ByVal Text As String) As Boolean
        Text = Text.Replace(" ", "")
        If Text = "" Then Return False
        Return CType(Text, Boolean)
    End Function

    <DebuggerStepThrough()> _
    Public Function L3Bool(ByVal Text As Object) As Boolean
        'Update 14/07/2010: kiểm tra nothing
        If Text Is Nothing Then Return False
        Return L3Bool(Text.ToString)
    End Function

    ''' <summary>
    ''' Kiểm tra số hợp lệ không hợp lệ thì gán = 0 và Trả về False
    ''' </summary>
    ''' <param name="sValue">Giá trị truyền vào và trả ra</param>
    ''' <returns></returns>
    ''' <remarks>Nếu số không hợp lệ thì trả về =0</remarks>
    <DebuggerStepThrough()> _
    Public Function L3IsNumeric(ByRef sValue As String) As Boolean
        'Date 16/01/2008
        If Not IsNumeric(sValue) Then
            sValue = "0"
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Kiểm tra số hợp lệ không hợp lệ thì gán = 0 và Trả về False
    ''' </summary>
    ''' <param name="sValue">Giá trị truyền vào và trả ra</param>
    ''' <param name="_EnumDataType">Kiểu dữ liệu dùng để kiểm tra</param>
    ''' <returns></returns>
    ''' <remarks>Nếu số không hợp lệ thì trả về =0</remarks>
    <DebuggerStepThrough()> _
    Public Function L3IsNumeric(ByRef sValue As String, ByVal _EnumDataType As EnumDataType) As Boolean
        'Date 16/01/2008
        If Not IsNumeric(sValue) Then
            sValue = "0"
            Return False
        End If

        'Date 11/11/2008
        Dim dbValue As Double = Double.Parse(sValue)
        Select Case _EnumDataType
            Case EnumDataType.TinyInt
                If dbValue > MaxTinyInt Or dbValue < MaxTinyInt * (-1) Then
                    sValue = "0"
                    Return False
                End If
            Case EnumDataType.Int
                If dbValue > MaxInt Or dbValue < MaxInt * (-1) Then
                    sValue = "0"
                    Return False
                End If
            Case EnumDataType.Money
                If dbValue > MaxMoney Or dbValue < MaxMoney * (-1) Then
                    sValue = "0"
                    Return False
                End If
            Case EnumDataType.SmallMoney
                If dbValue > MaxSmallMoney Or dbValue < MaxSmallMoney * (-1) Then
                    sValue = "0"
                    Return False
                End If
            Case EnumDataType.Number
                If dbValue > MaxDecimal Or dbValue < MaxDecimal * (-1) Then
                    sValue = "0"
                    Return False
                End If
        End Select
        dbValue = Nothing

        Return True
    End Function

    Public Function L3Right(ByVal sString As String, ByVal iLen As Integer) As String
        If sString = "" Then Return ""
        If sString.Length < iLen Then Return ""
        Return sString.Substring(sString.Length - iLen)
    End Function

    Public Function L3Left(ByVal sString As String, ByVal iLen As Integer) As String
        If sString = "" Then Return ""
        If sString.Length < iLen Then Return ""
        Return sString.Substring(0, iLen)
    End Function

#End Region

#Region "Ngày trên lưới"
    ''' <summary>
    ''' Kiểm tra ngày trên lưới
    ''' </summary>
    ''' <param name="sDate">Giá trị ngày trên lưới</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function L3DateValue(ByVal sDate As String) As String
        If sDate = MaskFormatDate Then Return MaskFormatDate
        If sDate.IndexOf("/") = -1 Then Return MaskFormatDate

        Dim dDate As Object = ConvertDate(sDate).ToString
        If IsDate(dDate) Then
            Return dDate.ToString
        Else
            Return MaskFormatDate
        End If
    End Function

    ''' <summary>
    ''' Format dạng ngày trên lưới
    ''' </summary>
    ''' <param name="sDate"></param>
    ''' <returns>Chuỗi ngày dạng dd/MM/yyyy</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ConvertDate(ByVal sDate As String) As Object
        Try
            Dim arr() As String
            Dim nDay As Integer
            Dim nMonth As Integer
            Dim byPos As Double
            Dim sResult As String
            Dim sSeparator As String = "/"

            arr = Microsoft.VisualBasic.Split(sDate, sSeparator)
            nDay = Convert.ToInt32(arr(0))
            nMonth = Convert.ToInt32(arr(1))

            'Ngày
            If nDay < 1 Or nDay > 31 Then
                Return MaskFormatDate
            Else
                sResult = nDay.ToString("00")
            End If
            'Tháng
            If nMonth < 1 Or nMonth > 12 Then
                Return MaskFormatDate
            Else
                sResult &= sSeparator & nMonth.ToString("00")
            End If

            'Năm
            byPos = arr(2).IndexOf("_")
            Select Case byPos
                Case -1
                    If CInt(arr(2)) < 1900 Or CInt(arr(2)) > 2079 Then
                        Return MaskFormatDate
                    Else
                        sResult &= sSeparator & arr(2).ToString
                    End If
                Case 2
                    sResult &= sSeparator & (Year(Today.Date)).ToString.Substring(0, 2) & arr(2).Trim.Substring(0, 2)
                Case Else
                    sResult &= sSeparator & Year(Today.Date)
            End Select
            Return sResult

        Catch ex As Exception
            Return MaskFormatDate
        End Try
    End Function
#End Region

#Region "Các hàm liên quan đến table"

    '''' <summary>
    '''' BỎ: Trả về một table sau khi đã được lọc
    '''' </summary>
    '''' <param name="dt">Bảng dữ liệu trước khi lọc</param>
    '''' <param name="sWhereClause">Điều kiện lọc (ví dụ:  ObjectTypeID = 'KH')</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '<DebuggerStepThrough()> _
    'Public Function ReturnTableFilter1(ByVal dt As DataTable, ByVal sWhereClause As String) As DataTable
    '    Dim dt1 As DataTable
    '    Dim dt2 As DataTable

    '    dt2 = dt.Copy

    '    dt2.DefaultView.RowFilter = sWhereClause
    '    dt1 = dt2.DefaultView.ToTable

    '    Return dt1

    'End Function

    ''' <summary>
    ''' Trả về một table sau khi đã được lọc
    ''' </summary>
    ''' <param name="dt">Bảng dữ liệu trước khi lọc</param>
    ''' <param name="sWhereClause">Điều kiện lọc (ví dụ:  ObjectTypeID = 'KH')</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnTableFilter(ByVal dt As DataTable, ByVal sWhereClause As String, Optional ByVal bCopy As Boolean = False) As DataTable
        If bCopy Then 'Nếu muốn giữ lại DataTable gốc (không thao tác trực tiếp lên DataTable gốc) 
            Dim dt2 As DataTable
            dt2 = dt.Copy
            dt2.DefaultView.RowFilter = sWhereClause

            Return dt2.DefaultView.ToTable
        Else 'Thao tác trực tiếp trên DataTable gốc (thay đổi DefaultView - dữ liệu nhìn thấy) nhưng dữ liệu gốc vẫn giữ nguyên (số lượng dòng vẫn giữ nguyên có thể dùng để RowFilter tiếp) 
            dt.DefaultView.RowFilter = sWhereClause
            Return dt.DefaultView.ToTable
        End If
    End Function

    ''' <summary>
    ''' Trả về vị trí row của bảng sau khi tìm kiếm
    ''' </summary>
    ''' <param name="dt">Bảng dữ liệu</param>
    ''' <param name="sKeysField">Khóa của bảng</param>
    ''' <param name="sValue">Giá trị cần tìm</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnRowTable(ByVal dt As DataTable, ByVal sKeysField As String, ByVal sValue As String) As Integer
        Return ReturnRowTable(dt, sKeysField, sValue, "ASC")
    End Function

    '''<summary>
    ''' Trả về vị trí row của bảng sau khi tìm kiếm
    ''' </summary>
    ''' <param name="dt">Bảng dữ liệu</param>
    ''' <param name="sKeysField">Khóa của bảng</param>
    ''' <param name="sValue">Giá trị cần tìm</param>
    ''' <param name="sOrder">Thứ tự sắp xếp: ASC hay DESC </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnRowTable(ByVal dt As DataTable, ByVal sKeysField As String, ByVal sValue As String, ByVal sOrder As String) As Integer
        dt.DefaultView.Sort = sKeysField & Space(1) & sOrder
        Return dt.DefaultView.Find(sValue)
    End Function

    ''' <summary>
    ''' Kiểm tra xem có tồn tại ít nhất một hàng trong lệnh SQL không
    ''' </summary>
    ''' <param name="SQL">Lệnh SQL cần kiểm tra</param>
    <DebuggerStepThrough()> _
    Public Function ExistRecord(ByVal SQL As String, Optional ByVal sConnectionStringNew As String = "") As Boolean
        Dim dt As New DataTable
        dt = ReturnDataTable(SQL, sConnectionStringNew)
        Return dt.Rows.Count > 0

    End Function

    ''' <summary>
    ''' Trả về một DataSet từ chuỗi SQL truyền vào
    ''' </summary>
    ''' <param name="SQL">Chuỗi SQL cần thực thi</param>
    ''' <returns>Trả về một DataSet nếu thực thi chuỗi SQL thành công, ngược lại sẽ báo lỗi bằng MsgErr</returns>
    Public Function ReturnDataSet(ByVal SQL As String, Optional ByVal MyMsgErr As String = "", Optional ByVal bUseClipboard As Boolean = True, Optional ByVal sConnectionStringNew As String = "") As DataSet
        'Minh Hòa Update 10/08/2012: Đếm số lần bị lỗi
        Dim iCountError As Integer = 0

        Dim ds As DataSet = New DataSet()
        'If giAppMode = 0 Then

        Dim conn As SqlConnection
        If sConnectionStringNew <> "" Then
            conn = New SqlConnection(sConnectionStringNew)
        Else
            conn = New SqlConnection(gsConnectionString)
        End If
        Dim cmd As SqlCommand = New SqlCommand(SQL, conn)
        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Try

ErrorHandles:
            conn.Open()
            'cmd.CommandTimeout = 0
            If iCountError > 0 Then 'Minh Hòa Update 10/08/2012: nếu có lỗi trước đó thi gán CommandTimeout = 30
                cmd.CommandTimeout = 30
            Else
                cmd.CommandTimeout = 0
            End If

            da.Fill(ds)
            conn.Close()

            If iCountError > 0 Then
                'Minh Hòa Update 10/08/2012: Nếu có lỗi trước đó thì trả lại thời gian ConnectionTimeout =0
                conn.ConnectionString = conn.ConnectionString.Replace(gsConnectionTimeout15, gsConnectionTimeout)

            End If

            Return ds
        Catch ex As SqlException
            '******************************************
            'Minh Hòa Update 10/08/2012: Kiểm tra nếu không kết nối được với server thì thông báo để kết nối lại.
            If ex.Number = 10054 OrElse ex.Number = 1231 _
            OrElse ex.Message.Contains("Could not open a connection to SQL Server") _
            OrElse ex.Message.Contains("The server was not found or was not accessible") _
            OrElse ex.Message.Contains("A transport-level error") Then 'Lỗi không kết nối được server
                If CheckConnectFailed(conn, iCountError, "FromDataSet") Then
                    GoTo ErrorHandles
                End If
            Else
                conn.Close()
                If bUseClipboard Then
                    Clipboard.Clear()
                    Clipboard.SetText(SQL)
                End If
                If MyMsgErr <> "" Then
                    MsgErr(MyMsgErr)
                Else
                    MsgErr("Error when excute SQL in function ReturnDataSet(). Paste your SQL code from Clipboard" & vbCrLf & ex.Number & "- " & ex.Message)
                End If
                Return Nothing
            End If
            '******************************************
            '            Catch
            '                conn.Close()
            '                Clipboard.Clear()
            '                Clipboard.SetText(SQL)
            '                MsgErr("Error when excute SQL in function ReturnDataSet(). Paste your SQL code from Clipboard")
            '                Return Nothing
        End Try

        'End If

        Return Nothing
    End Function



    ''' <summary>
    ''' Trả về một DataSet từ chuỗi SQL truyền vào
    ''' </summary>
    ''' <param name="SQL">Chuỗi SQL cần thực thi</param>
    ''' <param name="TableName">Mảng chứa tên các Table của DataSet</param>
    ''' <returns>Trả về một DataSet nếu thực thi chuỗi SQL thành công, ngược lại sẽ báo lỗi bằng MsgErr</returns>
    <DebuggerStepThrough()> _
    Public Function ReturnDataSet(ByVal SQL As String, ByVal TableName As String(), Optional ByVal sConnectionStringNew As String = "") As DataSet
        Dim ds As DataSet = ReturnDataSet(SQL, sConnectionStringNew)
        If ds Is Nothing Then Return Nothing
        For i As Integer = 0 To TableName.Length - 1
            ds.Tables(i).TableName = TableName(i)
        Next
        Return ds
    End Function


    ''' <summary>
    ''' Trả về một DataTable từ chuỗi SQL
    ''' </summary>
    ''' <param name="SQL">Chuỗi SQL thực thực</param>
    ''' <returns>DataTable nếu thực thi thành công lệnh SQL, ngược lại trả về nothing</returns>
    ''' <remarks></remarks>
    Public Function ReturnDataTable(ByVal SQL As String, Optional ByVal sConnectionStringNew As String = "") As DataTable
        Dim ds As DataSet = ReturnDataSet(SQL, , , sConnectionStringNew)
        If ds Is Nothing Then Return Nothing
        Return ds.Tables(0)
    End Function

    ''' <summary>
    ''' Trả về một DataTable từ chuỗi SQL
    ''' </summary>
    ''' <param name="SQL">Chuỗi SQL thực thực</param>
    ''' <param name="MyMsgErr">Chuỗi thông báo khi có lỗi</param>
    ''' <param name="bUseClipboard">Có sử dụng Clipboard không</param>
    ''' <returns>DataTable nếu thực thi thành công lệnh SQL, ngược lại trả về nothing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnDataTable(ByVal SQL As String, ByVal MyMsgErr As String, ByVal bUseClipboard As Boolean, Optional ByVal sConnectionStringNew As String = "") As DataTable
        'Create 30/07/2010: thông báo lỗi được truyền vào
        Dim ds As DataSet = ReturnDataSet(SQL, MyMsgErr, bUseClipboard, sConnectionStringNew)
        If ds Is Nothing Then Return Nothing
        Return ds.Tables(0)
    End Function


    ''' <summary>
    ''' Thực thi câu lệnh SQL, trả về kết quả là hàng đâu tiên và cột đầu tiên theo dạng chuỗi
    ''' </summary>
    ''' <param name="SQL">Chuỗi SQL cần thực thi</param>
    ''' <returns>Trả về một chuỗi nếu thực thi lệnh SQL thành công, ngược lại sẽ hiển thị MsgErr</returns>
    ''' <remarks>Thường dùng để kiểm tra</remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnScalar(ByVal SQL As String, Optional ByVal sConnectionStringNew As String = "") As String

        Try
            Dim dt As DataTable = ReturnDataTable(SQL, sConnectionStringNew)
            If dt.Rows.Count > 0 Then 'Có dữ liệu trong bảng
                Return dt.Rows(0).Item(0).ToString
            Else
                Return ""
            End If
            dt = Nothing
        Catch
            Clipboard.Clear()
            Clipboard.SetText(SQL)
            MsgErr("Error when excute function ReturnScalar(). Paste your SQL code from Clipboard")
            Return ""
        End Try

    End Function

#Region "Ngắt 30 dòng Lưu dữ liệu 1 lần"
    ''' <summary>
    ''' Lưu dữ liệu nhiều
    ''' </summary>
    ''' <param name="strSQL">Danh sách SQL cần thực thi</param>
    ''' <returns>Thành công = True, Ngược lại = False</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSQL(ByVal strSQL() As StringBuilder, Optional ByVal MyMsgErr As String = "", Optional ByVal bUseClipboard As Boolean = True, Optional ByVal sConnectionStringNew As String = "") As Boolean
        If strSQL Is Nothing OrElse strSQL.ToString.Length = 0 Then Return False

        Dim conn As SqlConnection

        If sConnectionStringNew <> "" Then
            conn = New SqlConnection(sConnectionStringNew)
        Else
            conn = New SqlConnection(gsConnectionString)
        End If

        Dim trans As SqlTransaction = Nothing
        Try
            conn.Open()
            trans = conn.BeginTransaction

            For i As Integer = 0 To strSQL.Length - 1
                If ExecuteSQL(strSQL(i).ToString, MyMsgErr, bUseClipboard, conn, trans) = False Then Return False
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            D99C0008.MsgL3(ex.Message)
            Return False
        Finally
            conn.Close()
        End Try
    End Function

    ''' <summary>
    ''' Thêm SQL vào danh sách sRet() nếu =30 dòng
    ''' </summary>
    ''' <param name="sRet">danh sách SQL</param>
    ''' <param name="sSQL">SQL cần thêm vào</param>
    ''' <param name="iCount">Số dòng đang nối</param>
    ''' <returns>danh sách sRet(), refresh lại biến sSQL="" và biến đếm iCount =0</returns>
    ''' <remarks></remarks>
    Public Function ReturnSQL(ByVal sRet() As StringBuilder, ByRef sSQL As StringBuilder, ByRef iCount As Integer, Optional ByVal iNumberExecSQL As Integer = 30) As StringBuilder()
        If iCount = iNumberExecSQL Then '30
            sRet = AddValueInArrStringBuilder(sRet, sSQL, , True)
            sSQL = New StringBuilder
            iCount = 0
        End If
        Return sRet
    End Function

    ''' <summary>
    ''' Thêm sSQL vào danh sách arSource()
    ''' </summary>
    ''' <param name="arSource">danh sách arSource()</param>
    ''' <param name="sSQL"></param>
    ''' <returns>danh sách arSource()</returns>
    ''' <remarks></remarks>
    Public Function AddValueInArrStringBuilder(ByVal arSource() As StringBuilder, ByVal sSQL As Object, Optional ByVal posLast As Boolean = True, Optional ByVal bAddNew As Boolean = False) As StringBuilder()
        If sSQL Is Nothing OrElse sSQL.ToString = "" Then Return arSource
        If arSource Is Nothing Then
            ReDim arSource(0)
        Else
            If bAddNew Then ReDim Preserve arSource(arSource.Length) 'Thêm mới phần tử
        End If

        If posLast Then 'Thêm vào cuối
            If arSource(arSource.Length - 1) Is Nothing Then arSource(arSource.Length - 1) = New StringBuilder 'nếu nothing dùng lệnh Append sẽ lỗi
            arSource(arSource.Length - 1).Append(sSQL)
        Else 'Thêm vào đầu
            If arSource(0) Is Nothing Then arSource(0) = New StringBuilder 'nếu nothing dùng lệnh Append sẽ lỗi
            arSource(0).Insert(0, sSQL)
        End If
        Return arSource
    End Function

    ''' <summary>
    ''' Nối thêm mảng arDes() vào mảng arSource()
    ''' </summary>
    ''' <param name="arSource">mảng arSource()</param>
    ''' <param name="arDes">mảng arDes()</param>
    ''' <returns>mảng arDes()</returns>
    ''' <remarks></remarks>
    Public Function AddValueInArrStringBuilder(ByVal arSource() As StringBuilder, ByVal arDes() As StringBuilder) As StringBuilder()
        If arDes Is Nothing Then Return arSource
        For i As Integer = 0 To arDes.Length - 1
            arSource = AddValueInArrStringBuilder(arSource, arDes(i))
        Next
        Return arSource
    End Function
#End Region

    '''' <summary>
    '''' Thực thi một chuỗi SQL có dùng Transaction (2 Đối số: truyền chuỗi và chuỗi kết nối mới (nếu có))
    '''' </summary>
    ''''<returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
    '<DebuggerStepThrough()> _
    Public Function ExecuteSQL(ByVal strSQL As String, Optional ByVal sConnectionStringNew As String = "") As Boolean
        Return ExecuteSQL(strSQL, "", True, Nothing, Nothing, sConnectionStringNew)
    End Function

    '''' <summary>
    '''' Thực thi một chuỗi SQL có dùng Transaction (4 Đối số: truyền chuỗi, message thông báo lỗi, có dùng clipboard không và chuỗi kết nối mới (nếu có))
    '''' </summary>
    ''''<returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
    '<DebuggerStepThrough()> _
    Public Function ExecuteSQL(ByVal strSQL As String, ByVal MyMsgErr As String, ByVal bUseClipboard As Boolean, Optional ByVal sConnectionStringNew As String = "") As Boolean
        Return ExecuteSQL(strSQL, MyMsgErr, bUseClipboard, Nothing, Nothing, sConnectionStringNew)
    End Function

    '''' <summary>
    '''' Thực thi một chuỗi SQL có dùng Transaction (4 Đối số:  truyền chuỗi, connection đang kết nối, Transaction đang kết nối và chuỗi kết nối mới (nếu có))
    '''' </summary>
    ''''<returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
    '<DebuggerStepThrough()> _
    Public Function ExecuteSQL(ByVal strSQL As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, Optional ByVal sConnectionStringNew As String = "") As Boolean
        Return ExecuteSQL(strSQL, "", True, Nothing, Nothing, sConnectionStringNew)
    End Function

    '''' <summary>
    '''' Thực thi một chuỗi SQL có dùng Transaction (HÀM CHÍNH)
    '''' </summary>
    ''''<returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
    '<DebuggerStepThrough()> _
    Public Function ExecuteSQL(ByVal strSQL As String, ByVal MyMsgErr As String, ByVal bUseClipboard As Boolean, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, Optional ByVal sConnectionStringNew As String = "") As Boolean
        If Trim(strSQL) = "" Then Return False

        'Minh Hòa Update 10/08/2012: Đếm số lần bị lỗi
        Dim iCountError As Integer = 0

        'Update 18/10/2010: Chỉ kiểm tra cho trường hợp nhập liệu Unicode
        'Khi Lưu xuống database nếu chiều dài dữ liệu vượt quá giới hạn cho phép thì không thông báo
        'Bỏ 29/03/2013
        'If gbUnicode Then strSQL = "SET ANSI_WARNINGS OFF " & vbCrLf & strSQL
        Dim bOpenConn As Boolean = conn Is Nothing

        If bOpenConn Then
            If sConnectionStringNew <> "" Then
                conn = New SqlConnection(sConnectionStringNew)
            Else
                conn = New SqlConnection(gsConnectionString)
            End If
            trans = Nothing
        End If

        Dim cmd As New SqlCommand(strSQL, conn)
        Try
            Try
ErrorHandles:
                If bOpenConn OrElse iCountError > 0 Then
                    conn.Open()
                    trans = conn.BeginTransaction
                End If

                'cmd.CommandTimeout = 0
                If iCountError > 0 Then 'Minh Hòa Update 10/08/2012: nếu có lỗi trước đó thi gán CommandTimeout = 30
                    cmd.CommandTimeout = 30
                Else
                    cmd.CommandTimeout = 0
                End If
                cmd.Transaction = trans
                cmd.ExecuteNonQuery()

                If bOpenConn Then
                    trans.Commit()
                    conn.Close()
                End If

                If iCountError > 0 Then 'Minh Hòa Update 10/08/2012: Nếu có lỗi trước đó thì trả lại thời gian ConnectionTimeout =0
                    conn.ConnectionString = conn.ConnectionString.Replace(gsConnectionTimeout15, gsConnectionTimeout)
                End If

                Return True
            Catch ex As SqlException 'Bug Lỗi trả ra từ Server
                'Minh Hòa Update 10/08/2012: Kiểm tra nếu không kết nối được với server thì thông báo để kết nối lại.
                If ex.Number = 10054 OrElse ex.Number = 1231 _
                    OrElse ex.Message.Contains("Could not open a connection to SQL Server") _
                    OrElse ex.Message.Contains("The server was not found or was not accessible") _
                    OrElse ex.Message.Contains("A transport-level error") Then 'Lỗi không kết nối được server

                    If CheckConnectFailed(conn, iCountError) Then
                        GoTo ErrorHandles
                    End If
                Else
                    trans.Rollback()
                    conn.Close()

                    If bUseClipboard Then
                        Clipboard.Clear()
                        Clipboard.SetText(strSQL)
                    End If
                    If MyMsgErr <> "" Then MsgErr(MyMsgErr)

                    'Update 15/09/2011: Bổ sung kiểm tra mã lỗi từ Server trả ra
                    If ex.Number >= 50000 Then 'Mã lỗi thực trả ra từ Server
                        MsgErr(ex.Errors.Item(0).Message)
                    Else
                        MsgErr("Error when execute SQL in function ExecuteSQL(). Paste your SQL code from Clipboard" & vbCrLf & ex.Errors.Item(0).Message)
                    End If

                    WriteLogFile(strSQL)
                    Return False
                End If
            End Try
        Catch ex1 As Exception 'Bug Lỗi trả ra từ Client

            MsgErr("Error when execute SQL " & vbCrLf & ex1.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Thực thi một chuỗi SQL không dùng Transaction (2 Đối số:  chuỗi SQL và chuỗi kết nối mới (nếu có))
    ''' </summary>
    ''' <param name="strSQL">Chuỗi SQL để thực thi</param>
    ''' <returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSQLNoTransaction(ByVal strSQL As String, Optional ByVal sConnectionStringNew As String = "") As Boolean
        Return ExecuteSQLNoTransaction(strSQL, "", True, sConnectionStringNew)
    End Function

    ''' <summary>
    ''' Thực thi một chuỗi SQL không dùng Transaction (HÀM CHÍNH)
    ''' </summary>
    ''' <param name="strSQL">Chuỗi SQL để thực thi</param>
    ''' <param name="MyMsgErr">Chuỗi thông báo khi có lỗi</param>
    ''' <param name="bUseClipboard">Có sử dụng Clipboard không</param>
    ''' <returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSQLNoTransaction(ByVal strSQL As String, ByVal MyMsgErr As String, ByVal bUseClipboard As Boolean, Optional ByVal sConnectionStringNew As String = "") As Boolean
        If Trim(strSQL) = "" Then Return False
        'Minh Hòa Update 10/08/2012: Đếm số lần bị lỗi
        Dim iCountError As Integer = 0

        'Update 18/10/2010: Chỉ kiểm tra cho trường hợp nhập liệu Unicode
        'Khi Lưu xuống database nếu chiều dài dữ liệu vượt quá giới hạn cho phép thì không thông báo
        'Bỏ 29/03/2013
        'If gbUnicode Then strSQL = "SET ANSI_WARNINGS OFF " & vbCrLf & strSQL

        'ErrorHandles:
        Dim conn As New SqlConnection(gsConnectionString)
        If sConnectionStringNew <> "" Then
            conn = New SqlConnection(sConnectionStringNew)
        Else
            conn = New SqlConnection(gsConnectionString)
        End If
        Dim cmd As New SqlCommand(strSQL, conn)
        Try
            Try
ErrorHandles:
                conn.Open()
                'cmd.CommandTimeout = 0
                If iCountError > 0 Then 'Minh Hòa Update 10/08/2012: nếu có lỗi trước đó thi gán CommandTimeout = 30
                    cmd.CommandTimeout = 30
                Else
                    cmd.CommandTimeout = 0
                End If
                cmd.ExecuteNonQuery()
                conn.Close()

                If iCountError > 0 Then 'Minh Hòa Update 10/08/2012: Nếu có lỗi trước đó thì trả lại thời gian ConnectionTimeout =0
                    conn.ConnectionString = conn.ConnectionString.Replace(gsConnectionTimeout15, gsConnectionTimeout)
                End If

                Return True

            Catch ex As SqlException 'Bug Lỗi trả ra từ Server
                'Minh Hòa Update 10/08/2012: Kiểm tra nếu không kết nối được với server thì thông báo để kết nối lại.
                If ex.Number = 10054 OrElse ex.Number = 1231 _
                    OrElse ex.Message.Contains("Could not open a connection to SQL Server") _
                    OrElse ex.Message.Contains("The server was not found or was not accessible") _
                    OrElse ex.Message.Contains("A transport-level error") Then 'Lỗi không kết nối được server
                    If CheckConnectFailed(conn, iCountError, "FromExecNoTrans") Then
                        GoTo ErrorHandles
                    End If
                Else
                    conn.Close()
                    If bUseClipboard Then
                        Clipboard.Clear()
                        Clipboard.SetText(strSQL)
                    End If
                    If MyMsgErr <> "" Then MsgErr(MyMsgErr)

                    'Update 15/09/2011: Bổ sung kiểm tra mã lỗi từ Server trả ra
                    If ex.Number >= 50000 Then 'Mã lỗi thực trả ra từ Server
                        MsgErr(ex.Errors.Item(0).Message)
                    Else
                        MsgErr("Error when execute SQL in function ExecuteSQL(). Paste your SQL code from Clipboard" & vbCrLf & ex.Errors.Item(0).Message)
                    End If
                    WriteLogFile(strSQL)
                    Return False
                End If
            End Try
        Catch ex1 As Exception 'Bug Lỗi trả ra từ Client
            MsgErr("Error when execute SQL " & vbCrLf & ex1.Message)
            Return False
        End Try

    End Function

    Public Function CheckConnectFailed(ByRef conn As SqlConnection, ByRef iCountError As Integer, Optional ByVal TypeCall As String = "", Optional ByVal bSetConnString As Boolean = False) As Boolean
        Dim sCountError As String = ""

        If TypeCall = "FromDataSet" Then 'Từ DataSet
            sCountError = " (" & iCountError & ")1"
        ElseIf TypeCall = "FromExecNoTrans" Then 'Từ ExecuteSQLNoTransaction
            sCountError = " (" & iCountError & ")2"
        Else 'Từ ExecuteSQL 
            sCountError = " (" & iCountError & ")"
        End If

        If MessageBox.Show("Kh¤ng kÕt nçi ¢§íc mÀy chï " & gsServer & sCountError & "." & vbCrLf & "BÁn câ muçn tiÕp tóc kÕt nçi kh¤ng?", "Læi", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
            'If MessageBox.Show("Connection failed (" & iCountError & ")" & vbCrLf & "Do you want to re_connect?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = DialogResult.Yes Then
            'Gắn lại thời gian ConnectionTimeout để chạy nhanh hơn
            If iCountError = 0 Then
                'If bSetConnString Then
                conn.ConnectionString = conn.ConnectionString.Replace(gsConnectionTimeout, gsConnectionTimeout15)
                '            Else
                '                gsConnectionString = gsConnectionString.Replace(gsConnectionTimeout, gsConnectionTimeout15)
                '            End If
            End If

            iCountError += 1
            'conn.Dispose()
            'GoTo ErrorHandles
            Return True
        Else
            MsgErr("Ch§¥ng trØnh kÕt thòc.")
            conn.Close()
            conn.Dispose()
            End
        End If
    End Function

#End Region

#Region "Chuyển về kiểu dữ liệu lưu xuống Server SQL"


    ''' <summary>
    ''' Thay các dấu ' thành '' dùng trong các lệnh SQL
    ''' </summary>
    ''' <param name="Text">Chuỗi Text truyền vào</param>
    ''' <remarks>Chú ý là kết quả trả về có dấu ' ở hai đầu chuỗi</remarks>
    <DebuggerStepThrough()> _
    Public Function SQLString(ByVal Text As String) As String
        Text = Text.Trim()
        Return "'" & Text.Replace("'", "''") & "'"
    End Function

    ''' <summary>
    ''' Thay các dấu ' thành '' dùng trong các lệnh SQL
    ''' </summary>
    ''' <param name="Text">Chuỗi Text truyền vào</param>
    ''' <remarks>Chú ý là kết quả trả về có dấu ' ở hai đầu chuỗi</remarks>
    <DebuggerStepThrough()> _
    Public Function SQLString(ByVal Text As Object) As String
        If Text Is Nothing Then Return "''"
        If IsDBNull(Text) Then Return "''"
        Return SQLString(Text.ToString())
    End Function

    ''' <summary>
    ''' Thay các dấu ' thành '' dùng trong các lệnh SQL
    ''' </summary>
    ''' <param name="Bool">Giá trị bool, nếu True sẽ chuyển thành 1, ngược lại chuyển thành 0</param>
    ''' <remarks>Chú ý là kết quả trả về có dấu ' ở hai đầu chuỗi</remarks>
    <DebuggerStepThrough()> _
    Public Function SQLString(ByVal Bool As Boolean) As String
        If Bool Then
            Return SQLString("1")
        Else
            Return SQLString("0")
        End If
    End Function

    ''' <summary>
    ''' Chuyển chuỗi sang UNICODE
    ''' </summary>
    ''' <param name="Text">Chuỗi Text truyền vào</param>
    ''' <remarks>Chú ý là kết quả trả về có dấu ' ở hai đầu chuỗi</remarks>
    <DebuggerStepThrough()> _
       Public Function SQLString(ByVal Text As Object, ByVal ConvertToVNI As Boolean) As String
        If Text Is Nothing Then Return "''"
        If IsDBNull(Text) Then Return "''"
        Return SQLString(Text.ToString(), ConvertToVNI)
    End Function

    ''' <summary>
    ''' Chuyển chuỗi sang UNICODE
    ''' </summary>
    ''' <param name="Text">Chuỗi Text truyền vào</param>
    ''' <remarks>Chú ý là kết quả trả về có dấu ' ở hai đầu chuỗi</remarks>
    <DebuggerStepThrough()> _
  Public Function SQLString(ByVal Text As String, ByVal ConvertToVNI As Boolean) As String
        Text = Text.Trim()
        If ConvertToVNI Then Text = ConvertUnicodeToVni(Text)
        Return "'" & Text.Replace("'", "''") & "'"
    End Function

    ''' <summary>
    ''' Khi lưu giá trị combo bổ sung ngày 19/07/2010
    ''' </summary>
    ''' <param name="combo">combo name</param>
    ''' <returns></returns>
    ''' <remarks>chặn khi giá trị nothing</remarks>
    Public Function SQLString(ByVal combo As C1.Win.C1List.C1Combo) As String
        If combo.SelectedValue Is Nothing OrElse combo.Text = "" Then Return "''"
        Return SQLString(combo.SelectedValue)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số Decimal để lưu vào database
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function SQLDecimal(ByVal Number As String) As String
        If Number = "" Then Return "0"
        Dim dNumber As Decimal = CType(Number, Decimal)
        Return Format(dNumber, "")
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số Decimal để lưu vào database
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function SQLDecimal(ByVal Number As Object) As String
        If IsDBNull(Number) Then Return "0"
        If Number.ToString = "" Then Return "0"
        If Number.ToString = "True" OrElse Number.ToString = "False" Then Return SQLNumber(Convert.ToBoolean(Number))
        Return SQLDecimal(CType(Number, String))
    End Function


    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLNumber(ByVal Number As String) As String
        If Number = "" Then Return "0"
        Dim dNumber As Double = CType(Number, Double)
        Return Format(dNumber, "")
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLNumber(ByVal Number As Boolean) As String
        Return IIf(Number, "1", "0").ToString
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLNumber(ByVal Number As Object) As String
        If IsDBNull(Number) Then Return "0"
        If Number.ToString = "" Then Return "0"
        If Number.ToString = "True" OrElse Number.ToString = "False" Then Return SQLNumber(Convert.ToBoolean(Number))
        Return SQLNumber(CType(Number, String))
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    ''' <param name="StringFormat">Format số trước khi insert</param>
    <DebuggerStepThrough()> _
    Public Function SQLNumber(ByVal Number As String, ByVal StringFormat As String) As String
        If Number = "" Then Return "0"
        Dim dNumber As Double = CType(Number, Double)
        If StringFormat <> "" AndAlso L3Left(StringFormat, 1) = "N" AndAlso L3Int(StringFormat.Substring(1, StringFormat.Length - 1)) < 0 Then
            StringFormat = "N0"
        End If
        Return Format(dNumber, StringFormat)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    ''' <param name="StringFormat">Format số trước khi insert</param>
    <DebuggerStepThrough()> _
    Public Function SQLNumber(ByVal Number As Object, ByVal StringFormat As String) As String
        If Number Is Nothing Then Return "0"
        If IsDBNull(Number) Then Return "0"
        If Number.ToString = "" Then Return "0"
        Dim dNumber As Double = CType(Number, Double)
        If StringFormat <> "" AndAlso L3Left(StringFormat, 1) = "N" AndAlso L3Int(StringFormat.Substring(1, StringFormat.Length - 1)) < 0 Then
            StringFormat = "N0"
        End If
        Return Format(dNumber, StringFormat)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLMoney(ByVal Number As String) As String
        Return SQLNumber(Number)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLMoney(ByVal Number As Object) As String
        Return SQLNumber(Number)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLMoney(ByVal Number As String, ByVal sFormat As String) As String
        'Chỉ gọi hàm SQLMoney khi Lưu, còn khi load dữ liệu thì gọi hàm SQLNumber
        Return CDbl(SQLNumber(Number, sFormat)).ToString
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu số để lưu vào database
    ''' </summary>
    ''' <param name="Number">Số insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLMoney(ByVal Number As Object, ByVal sFormat As String) As String
        Return CDbl(SQLNumber(Number, sFormat)).ToString
    End Function

    ''' <summary>
    ''' Trả về số dạng Double để tính toán
    ''' </summary>
    ''' <param name="Number">Số để tính toán</param>
    ''' <param name="sFormat">Chuỗi format</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ConvertMoney(ByVal Number As String, ByVal sFormat As String) As Double
        Return CDbl(SQLNumber(Number, sFormat))
    End Function

    ''' <summary>
    ''' Trả về số dạng Double để tính toán
    ''' </summary>
    ''' <param name="Number">Số để tính toán</param>
    ''' <param name="sFormat">Chuỗi format</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ConvertMoney(ByVal Number As Object, ByVal sFormat As String) As Double
        Return CDbl(SQLNumber(Number, sFormat))
    End Function


    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để gọi sự kiện FilterChange của lưới ở dạng MM/dd/yyyy
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function DateSave(ByVal [Date] As String) As String
        If [Date] = "" Then Return "NULL"
        Dim dDate As Date = CType([Date], Date)
        Return dDate.ToString("MM/dd/yyyy")

    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để gọi sự kiện FilterChange của lưới ở dạng MM/dd/yyyy
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function DateSave(ByVal [Date] As Object) As String
        If IsDBNull([Date]) Then Return "NULL"
        Return DateSave([Date].ToString)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để lưu vào database ở dạng MM/dd/yyyy
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function SQLDateSave(ByVal [Date] As String) As String
        If [Date] = "" Then Return "NULL"
        If [Date] = MaskFormatDateShort Then Return "NULL"
        If [Date] = MaskFormatDate Then Return "NULL"
        Dim dDate As Date = CType([Date], Date)
        Return SQLString(dDate.ToString("MM/dd/yyyy"))

    End Function

    Public Function DateSaveTime(ByVal [Date] As String) As String
        If [Date] = "" Then Return "NULL"
        Dim dDate As Date = CType([Date], Date)
        Return dDate.ToString("MM/dd/yyyy HH:mm:ss")

    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để gọi sự kiện FilterChange của lưới ở dạng MM/dd/yyyy
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function DateSaveTime(ByVal [Date] As Object) As String
        If IsDBNull([Date]) Then Return "NULL"
        Return DateSaveTime([Date].ToString)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để lưu vào database ở dạng MM/dd/yyyy
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLDateSave(ByVal [Date] As Object) As String
        If IsDBNull([Date]) Then Return "NULL"
        If [Date].ToString = MaskFormatDateShort Then Return "NULL"
        If [Date].ToString = MaskFormatDate Then Return "NULL"
        Return SQLDateSave([Date].ToString)
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để lưu vào database ở dạng MM/dd/yyyy HH:mm:ss
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    <DebuggerStepThrough()> _
   Public Function SQLDateTimeSave(ByVal [Date] As String) As String
        If [Date] = "" Then Return "NULL"
        Dim dDate As Date = CType([Date], Date)
        Return SQLString(dDate.ToString("MM/dd/yyyy HH:mm:ss"))
    End Function

    ''' <summary>
    ''' Trả về chuỗi kiểu ngày để lưu vào database ở dạng MM/dd/yyyy HH:mm:ss
    ''' </summary>
    ''' <param name="Date">Ngày insert vào database</param>
    <DebuggerStepThrough()> _
    Public Function SQLDateTimeSave(ByVal [Date] As Object) As String
        If IsDBNull([Date]) Then Return "NULL"
        Return SQLDateTimeSave([Date].ToString)
    End Function

    ''' <summary>
    ''' Trả về kiểu chuỗi ngày format theo dạng dd/MM/yyyy
    ''' </summary>
    ''' <param name="Date">Kiểu ngày truyền vào</param>
    <DebuggerStepThrough()> _
    Public Function SQLDateShow(ByVal [Date] As Object) As String
        If IsDBNull([Date]) Then Return ""
        Return SQLDateShow([Date].ToString)
    End Function

    ''' <summary>
    ''' Trả về kiểu chuỗi ngày format theo dạng dd/MM/yyyy
    ''' </summary>
    ''' <param name="Date">Kiểu ngày truyền vào</param>
    <DebuggerStepThrough()> _
    Public Function SQLDateShow(ByVal [Date] As String) As String
        If [Date].Trim = "" Then Return ""
        Return Format(Convert.ToDateTime([Date]), "dd/MM/yyyy")
    End Function

    Public Function ReturnValueC1Combo(ByVal tdbc As C1.Win.C1List.C1Combo, Optional ByVal sField As String = "") As String
        If tdbc.SelectedValue Is Nothing OrElse tdbc.Text = "" Then Return ""
        If sField = "" Then
            Return tdbc.SelectedValue.ToString
        Else
            Return tdbc.Columns(sField).Text
        End If
    End Function

    ''' <summary>
    ''' Lấy giá trị trong dropdown
    ''' </summary>
    ''' <param name="tdbd">dropdown name</param>
    ''' <param name="sField">value trả về</param>
    ''' <param name="sWhere">Điều kiện lọc. Mặc định lọc theo ValueMember</param>
    ''' <returns>giá trị của sField ở dòng đầu tiên</returns>
    ''' <remarks>Trả về rỗng nếu không tồn tại</remarks>
    Public Function ReturnValueC1DropDown(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sField As String, Optional ByVal sWhere As String = "") As String
        'Dim dt As DataTable = CType(tdbd.DataSource, DataTable)
        'If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return ""
        'If sWhere.Equals("") Then sWhere = tdbd.ValueMember & "= " & SQLString(tdbd.Columns(tdbd.ValueMember).Text)
        'Dim dr() As DataRow = dt.Select(sWhere)
        'If dr.Length = 0 Then Return ""
        'Return dr(0).Item(sField).ToString
        Dim dr As DataRow = ReturnDataRow(tdbd, sWhere)
        If dr Is Nothing Then Return ""
        Return dr.Item(sField).ToString
    End Function

    '''' <summary>
    '''' Lấy dòng dữ liệu trong dropdown (BỎ HÀM NÀY, THAY THẾ BẰNG HÀM ReturnDataRow)
    '''' </summary>
    '''' <param name="tdbd">dropdown name</param>
    '''' <param name="sWhere">Điều kiện lọc. Mặc định lọc theo ValueMember</param>
    '''' <returns>dòng dữ liệu đầu tiên tìm được</returns>
    '''' <remarks>Trả về nothing nếu không tồn tại</remarks>
    'Public Function ReturnValueC1DropDowns(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal sWhere As String = "") As DataRow
    '    Dim dt As DataTable = CType(tdbd.DataSource, DataTable)
    '    If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return Nothing
    '    If sWhere.Equals("") Then sWhere = tdbd.ValueMember & "= " & SQLString(tdbd.Columns(tdbd.ValueMember).Text)
    '    Dim dr() As DataRow = dt.Select(sWhere)
    '    If dr.Length = 0 Then Return Nothing
    '    Return dr(0)
    'End Function

    ''' <summary>
    ''' Lấy dòng dữ liệu trong dropdown
    ''' </summary>
    ''' <param name="tdbd">dropdown name</param>
    ''' <param name="sWhere">Điều kiện lọc. Mặc định lọc theo ValueMember</param>
    ''' <returns>dòng dữ liệu đầu tiên tìm được</returns>
    ''' <remarks>Trả về nothing nếu không tồn tại</remarks>
    Public Function ReturnDataRow(ByVal tdbd As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal sWhere As String = "") As DataRow
        Dim dt As DataTable = CType(tdbd.DataSource, DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return Nothing
        If sWhere.Equals("") Then sWhere = tdbd.ValueMember & "= " & SQLString(tdbd.Columns(tdbd.ValueMember).Text)
        'Dim dr() As DataRow = dt.Select(sWhere)
        'If dr.Length = 0 Then Return Nothing
        'Return dr(0)
        Return ReturnDataRow(dt, sWhere)
    End Function


    ''' <summary>
    ''' Lấy dòng dữ liệu trong Table
    ''' </summary>
    ''' <param name="dtSource">Nguồn dữ liệu cần lọc. Truyền vào Datable or Combo.DataSource or Dropdown.DataSource</param>
    ''' <param name="sWhere">Điều kiện lọc</param>
    ''' <returns>dòng dữ liệu đầu tiên tìm được</returns>
    ''' <remarks>Trả về nothing nếu không tồn tại</remarks>
    Public Function ReturnDataRow(ByVal dtSource As Object, ByVal sWhere As String) As DataRow
        Dim dt As DataTable = CType(dtSource, DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return Nothing
        Dim dr() As DataRow = dt.Select(sWhere)
        If dr.Length = 0 Then Return Nothing
        Return dr(0)
    End Function



#End Region

#Region "Tìm kiếm trên Finder"

    ''' <summary>
    ''' Load lại dữ liệu cho lưới sau khi tìm kiếm
    ''' </summary>
    ''' <param name="C1Grid"></param>
    ''' <param name="dtRoot">Bảng dữ liệu trước khi tìm kiếm</param>
    ''' <param name="sClauseFind">Điều kiện sau khi tìm kiếm</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub LoadGridFind(ByVal C1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal dtRoot As DataTable, ByVal sClauseFind As String)
        'Dim dtF As DataTable
        'dtF = dtRoot.Copy
        ''dtF.DefaultView.RowFilter = sClauseFind.Replace("N'", "'")
        'dtF.DefaultView.RowFilter = sClauseFind
        'LoadDataSource(C1Grid, dtF)

        dtRoot.DefaultView.RowFilter = sClauseFind
        LoadDataSource(C1Grid, dtRoot)

    End Sub

    '''' <summary>
    '''' Hiển thị form tìm kiếm
    '''' </summary>
    '''' <param name="Finder">WithEvents của finder</param>
    '''' <param name="SQL">Chuỗi SQL đổ nguồn cho tìm kiếm</param>
    ' <DebuggerStepThrough()> _
    Public Sub ShowFindDialog(ByVal Finder As D99C1001, ByVal SQL As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As New DataTable
        Dim i As Integer
        dt = ReturnDataTable(SQL)
        If dt.Rows.Count > 0 Then
            Dim sFormID As String = ""
            Dim sMode As String = ""
            sFormID = dt.Rows(0).Item("FormID").ToString
            sMode = dt.Rows(0).Item("Mode").ToString

            Dim sDescription As String = ""
            For i = 0 To dt.Rows.Count - 1
                sDescription = dt.Rows(i).Item("Description").ToString.Trim
                If L3Int(dt.Rows(i).Item("DataType")) = 0 Then
                    'Update 16/07/2013: Nếu có thay đổi tên resource
                    If giReplacResource <> 0 Then
                        sDescription = ReplaceResourceCustom(sDescription)
                    End If
                End If
                Finder.AddFieldName(dt.Rows(i).Item("FieldName").ToString.Trim, sDescription, CType(dt.Rows(i).Item("DataType"), D99D0041.FinderTypeEnum), Convert.ToInt16(dt.Rows(i).Item("Length")), dt.Rows(i).Item("DataType").ToString)
            Next
            'Update 14/09/2010: bổ sung tham số Unicode
            Finder.UseUnicode = bUseUnicode
            Finder.Language = geLanguage
            Finder.AllowSortOrder = False
            Finder.ShowFormFind(sFormID, sMode)
        Else
            D99C0008.MsgL3("Không có dữ liệu cho tìm kiếm")
        End If
        dt = Nothing
    End Sub

    '''' <summary>
    '''' Hiển thị form tìm kiếm
    '''' </summary>
    '''' <param name="Finder">WithEvents của finder</param>
    '''' <param name="SQL">Chuỗi SQL đổ nguồn cho tìm kiếm trên Client</param>
    '<DebuggerStepThrough()> _
    Public Sub ShowFindDialogClient(ByVal Finder As D99C1001, ByVal SQL As String, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As New DataTable
        Dim i As Integer
        dt = ReturnDataTable(SQL)
        If dt.Rows.Count > 0 Then
            Dim sFormID As String = ""
            Dim sMode As String = ""
            sFormID = dt.Rows(0).Item("FormID").ToString
            sMode = dt.Rows(0).Item("Mode").ToString

            Dim sDescription As String = ""
            For i = 0 To dt.Rows.Count - 1
                sDescription = dt.Rows(i).Item("Description").ToString.Trim
                If L3Int(dt.Rows(i).Item("DataType")) = 0 Then
                    'Update 16/07/2013: Nếu có thay đổi tên resource
                    If giReplacResource <> 0 Then
                        sDescription = ReplaceResourceCustom(sDescription)
                    End If
                End If
                Finder.AddFieldName(dt.Rows(i).Item("FieldName").ToString.Trim, sDescription, CType(dt.Rows(i).Item("DataType"), D99D0041.FinderTypeEnum), Convert.ToInt16(dt.Rows(i).Item("Length")), dt.Rows(i).Item("DataType").ToString)
            Next

            'For i = 0 To dt.Rows.Count - 1
            '    Finder.AddFieldName(dt.Rows(i).Item("FieldName").ToString.Trim, dt.Rows(i).Item("Description").ToString.Trim, CType(dt.Rows(i).Item("DataType"), D99D0041.FinderTypeEnum), Convert.ToInt16(dt.Rows(i).Item("Length")), dt.Rows(i).Item("DataType").ToString)
            'Next
            Finder.UseUnicode = bUseUnicode
            Finder.Language = geLanguage
            Finder.AllowSortOrder = False
            '*********************
            'Update 04/10/2010 Thêm 1 đối số Unicode có sử dụng view DxxV1234
            Finder.ShowFormFindClient(sFormID, sMode, bUseUnicode)
        Else
            D99C0008.MsgL3("Không có dữ liệu cho tìm kiếm")
        End If
        dt = Nothing
    End Sub

    ''' <summary>
    ''' Hiển thị form tìm kiếm theo table
    ''' </summary>
    ''' <param name="Finder">WithEvents của finder</param>
    ''' <param name="dt">table chứa caption của các cột tìm kiếm</param>
    ''' <param name="FormID">Form đang tìm kiếm (me.name)</param>
    ''' <param name="Mode">Master hay Detail</param>
    ''' <param name="bUseUnicode">Nhập liệu Unicode</param>
    ''' <param name="ArrFieldExclude">Tập các Field loại trừ cần truyền vào, VD: Dim ArrFieldExclude() As String = {"OrderNum"}</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub ShowFindDialogClient(ByVal Finder As D99C1001, ByVal dt As DataTable, ByVal FormID As String, ByVal Mode As String, Optional ByVal bUseUnicode As Boolean = False, _
     Optional ByVal ArrFieldExclude() As String = Nothing)
        Dim i As Integer
        If dt.Rows.Count > 0 Then
            Dim dr1 As DataRow
            For i = 0 To dt.Rows.Count - 1
                dr1 = dt.Rows(i)

                'Update 17/11/2010: Không tìm kiếm các cột trên Field truyền vào
                If ArrFieldExclude IsNot Nothing Then
                    For j As Integer = 0 To ArrFieldExclude.Length - 1
                        If dr1("FieldName").ToString.Trim = ArrFieldExclude(j) Then
                            GoTo NoFind
                        End If
                    Next
                End If
                'Không thêm cột STT và cột có FieldName = '' vào danh sách
                If dr1("FieldName").ToString.Trim <> "OrderNum" AndAlso dr1("FieldName").ToString.Trim <> "" Then
                    Finder.AddFieldName(dr1("FieldName").ToString.Trim, dr1("Description").ToString.Trim, DataTypeChangEnum(dr1("DataType").ToString), Convert.ToInt16(dr1("DataWidth")), dt.Rows(i).Item("DataType").ToString)
                End If
NoFind:
            Next
            Finder.UseUnicode = bUseUnicode
            Finder.Language = geLanguage
            Finder.AllowSortOrder = False
            Finder.ShowFormFindClient(FormID, Mode)
        Else
            D99C0008.MsgL3("Không có dữ liệu cho tìm kiếm")
        End If
    End Sub


    ''' <summary>
    ''' Hiển thị form tìm kiếm theo table
    ''' </summary>
    ''' <param name="Finder">WithEvents của finder</param>
    ''' <param name="dt">table chứa caption của các cột tìm kiếm</param>
    ''' <param name="FormID">Form đang tìm kiếm (me.name)</param>
    ''' <param name="Mode">Master hay Detail</param>
    ''' <param name="bUseUnicode">Nhập liệu Unicode</param>
    ''' <param name="ArrFieldExclude">Tập các Field loại trừ cần truyền vào, VD: Dim ArrFieldExclude() As String = {"OrderNum"}</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub ShowFindDialogClientServer(ByVal Finder As D99C1001, ByVal dt As DataTable, ByVal FormID As String, ByVal Mode As String, Optional ByVal bUseUnicode As Boolean = False, _
    Optional ByVal ArrFieldExclude() As String = Nothing)
        Dim i As Integer
        If dt.Rows.Count > 0 Then
            Dim dr1 As DataRow
            For i = 0 To dt.Rows.Count - 1
                dr1 = dt.Rows(i)

                'Update 17/11/2010: Không tìm kiếm các cột trên Field truyền vào
                If ArrFieldExclude IsNot Nothing Then
                    For j As Integer = 0 To ArrFieldExclude.Length - 1
                        If dr1("FieldName").ToString.Trim = ArrFieldExclude(j) Then
                            GoTo NoFind
                        End If
                    Next
                End If

                'Không thêm cột STT và cột có FieldName = '' vào danh sách
                If dr1("FieldName").ToString.Trim <> "OrderNum" AndAlso dr1("FieldName").ToString.Trim <> "" Then
                    Finder.AddFieldName(dr1("FieldName").ToString.Trim, dr1("Description").ToString.Trim, DataTypeChangEnum(dr1("DataType").ToString), Convert.ToInt16(dr1("DataWidth")), dt.Rows(i).Item("DataType").ToString)
                End If
NoFind:
            Next
            Finder.UseUnicode = bUseUnicode
            Finder.Language = geLanguage
            Finder.AllowSortOrder = False
            Finder.ShowFormFindClientServer(FormID, Mode)
        Else
            D99C0008.MsgL3("Không có dữ liệu cho tìm kiếm")
        End If
    End Sub
#End Region

#Region "Thông tin hệ thống"

    ''' <summary>
    ''' Hiển thị form Thông tin hệ thống theo dạng mới Online
    ''' </summary>
    ''' <param name="sCreateUserID"></param>
    ''' <param name="sCreateDate"></param>
    ''' <param name="sLastModifyUserID"></param>
    ''' <param name="sLastModifyDate"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub ShowSysInfoDialog(ByVal sCreateUserID As String, ByVal sCreateDate As String, ByVal sLastModifyUserID As String, ByVal sLastModifyDate As String)
        Dim SysInfo As New D99C0010
        'If geLanguage = EnumLanguage.Vietnamese Then
        '    SysInfo.ShowSysInforForm("Th¤ng tin hÖ thçng", sCreateUserID, sCreateDate, sLastModifyUserID, sLastModifyDate, "Người tạo", "Ngày tạo", "Người cập nhật cuối cùng", "Ngày cập nhật cuối cùng", "Đó&ng")
        'Else
        '    SysInfo.ShowSysInforForm("System Information", sCreateUserID, sCreateDate, sLastModifyUserID, sLastModifyDate, "Created by", "Created date", "Last modified by", "Last modified date", "&Close")
        'End If
        SysInfo.ShowSysInforForm(rl3("Thong_tin_he_thong"), sCreateUserID, sCreateDate, sLastModifyUserID, sLastModifyDate, rl3("Nguoi_tao"), rl3("Ngay_tao"), rl3("Nguoi_cap_nhat_cuoi_cung"), rl3("Ngay_cap_nhat_cuoi_cung"), rl3("Do_ng"))
    End Sub
#End Region

#Region "Kiểm tra có tồn tại khóa"
    ''' <summary>
    ''' Kiểm tra có tồn tại tập hợp key của bảng hay không ?
    ''' </summary>
    ''' <param name="TableName">Bảng cần kiểm tra</param>
    ''' <param name="Field">Khóa cần kiểm tra</param>
    ''' <param name="TextFrom">Giá trị cần kiểm tra</param>
    ''' <param name="TextTo">Giá trị cần kiểm tra</param>
    ''' <returns>Trả về True nếu có khóa, ngược lại trả về False</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function IsExistKeyBetween(ByVal TableName As String, ByVal Field As String, ByVal TextFrom As String, ByVal TextTo As String, Optional ByVal sNote As String = "", Optional ByVal sConnectionStringNew As String = "") As Boolean
        Dim sSQL As String = IIf(sNote <> "", "--" & sNote & vbCrLf, "").ToString 'Append 27/07/2012
        sSQL &= "Select Top 1 1 From " & TableName & " WITH(NOLOCK) Where " & Field & " Between " & SQLString(TextFrom) & " And " & SQLString(TextTo)
        Return ExistRecord(sSQL, sConnectionStringNew)
    End Function

    ''' <summary>
    ''' Kiểm tra có tồn tại key của bảng hay không ?
    ''' </summary>
    ''' <param name="TableName">Bảng cần kiểm tra</param>
    ''' <param name="Field">Khóa cần kiểm tra</param>
    ''' <param name="Text">Giá trị cần kiểm tra</param>
    ''' <returns>Trả về True nếu có khóa, ngược lại trả về False</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function IsExistKey(ByVal TableName As String, ByVal Field As String, ByVal Text As String, Optional ByVal sNote As String = "", Optional ByVal sConnectionStringNew As String = "") As Boolean
        Dim sSQL As String = IIf(sNote <> "", "--" & sNote & vbCrLf, "").ToString 'Append 27/07/2012
        sSQL &= "Select Top 1 1 From " & TableName & " WITH(NOLOCK) Where " & Field & " = " & SQLString(Text)
        Return ExistRecord(sSQL, sConnectionStringNew)
    End Function

    ''' <summary>
    ''' Kiểm tra có tồn tại key của bảng hay không ?
    ''' </summary>
    ''' <param name="TableName">Bảng cần kiểm tra</param>
    ''' <param name="Field">Mảng Khóa cần kiểm tra</param>
    ''' <param name="Text">Mảng Giá trị cần kiểm tra</param>
    ''' <returns>Trả về True nếu có khóa, ngược lại trả về False</returns>
    ''' <remarks>Dim FieldList () as string ={"Field1", "Field2"}; Dim TextList () as string ={"Text1", "Text2"} Gọi IsExistKey ("DxxTxxxx", FieldList,)</remarks>
    <DebuggerStepThrough()> _
    Public Function IsExistKey(ByVal TableName As String, ByVal Field As String(), ByVal Text As String(), Optional ByVal sNote As String = "", Optional ByVal sConnectionStringNew As String = "") As Boolean
        Dim sSQL As String = IIf(sNote <> "", "--" & sNote & vbCrLf, "").ToString 'Append 27/07/2012
        sSQL &= "Select Top 1 1 From " & TableName & " WITH(NOLOCK) Where "
        If Field.Length <> Text.Length OrElse Field.Length = 0 OrElse Text.Length = 0 Then Return True
        For i As Integer = 0 To Field.Length - 1
            sSQL &= Field(i) + " = " + SQLString(Text(i)) & " And "
        Next
        sSQL = sSQL.Substring(0, sSQL.Length - " And ".Length)
        Return ExistRecord(sSQL, sConnectionStringNew)
    End Function
#End Region

#Region "Phím nóng"


    '''' <summary>
    '''' Nhấn F7 trên lưới để copy ô trên xuống ô dưới
    '''' </summary>
    '''' <param name="c1Grid">Lưới C1</param>
    '''' <remarks></remarks>
    '<DebuggerStepThrough()> _
    'Public Sub HotKeyF7(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
    '    Try
    '        If c1Grid.RowCount < 1 Then Exit Sub

    '        If c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns.Item(c1Grid.Col).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far Then ' Số
    '            If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse Val(c1Grid(c1Grid.Row, c1Grid.Col).ToString) = 0 Then
    '                c1Grid.Columns(c1Grid.Col).Text = c1Grid(c1Grid.Row - 1, c1Grid.Col).ToString()
    '                c1Grid.UpdateData()
    '            End If
    '        Else ' Chuỗi hoặc Ngày
    '            If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDateShort OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDate Then
    '                c1Grid.Columns(c1Grid.Col).Text = c1Grid(c1Grid.Row - 1, c1Grid.Col).ToString()
    '                c1Grid.UpdateData()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        D99C0008.Msg("Lỗi HotKeyF7: " & ex.Message)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Nhấn F7 trên lưới để copy ô trên xuống ô dưới cho nhiều cột có liên quan
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <param name="iColumns">Tập cột cần truyền vào, VD: Dim iColumns() As Integer = {COL_ObjectName, COL_ObjectAddress}</param>
    ''' <remarks>Nhấn F7 tại cột ObjectID thì gọi HotKeyF7 (tdbg, iColumns)</remarks>
    Public Sub HotKeyF7(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ParamArray iColumns() As Integer)
        Try
            If c1Grid.RowCount < 1 Then Exit Sub
            c1Grid.UpdateData() 'Bổ sung 24/09/2012
            If c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns.Item(c1Grid.Col).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far Then ' Số
                If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse Val(c1Grid(c1Grid.Row, c1Grid.Col).ToString) = 0 Then
                    c1Grid.Columns(c1Grid.Col).Text = c1Grid(c1Grid.Row - 1, c1Grid.Col).ToString()
                    For i As Integer = 0 To iColumns.Length - 1
                        c1Grid.Columns(iColumns(i)).Text = c1Grid(c1Grid.Row - 1, iColumns(i)).ToString()
                    Next i
                    c1Grid.UpdateData()
                End If
            Else ' Chuỗi hoặc Ngày
                If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDateShort OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDate Then
                    c1Grid.Columns(c1Grid.Col).Text = c1Grid(c1Grid.Row - 1, c1Grid.Col).ToString()
                    For i As Integer = 0 To iColumns.Length - 1
                        c1Grid.Columns(iColumns(i)).Text = c1Grid(c1Grid.Row - 1, iColumns(i)).ToString()
                    Next i
                    c1Grid.UpdateData()
                End If
            End If

        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyF7: " & ex.Message)
        End Try

    End Sub

    '''' <summary>
    '''' Nhấn F8 trên lưới để copy dòng trên xuống dòng dưới tính từ phải qua
    '''' </summary>
    '''' <param name="c1Grid">Lưới C1</param>
    '''' <remarks></remarks>
    '<DebuggerStepThrough()> _
    'Public Sub HotKeyF8(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
    '    Try
    '        If c1Grid.RowCount < 1 Then Exit Sub

    '        If c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns.Item(c1Grid.Col).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far Then ' Số
    '            If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse Val(c1Grid(c1Grid.Row, c1Grid.Col).ToString) = 0 Then
    '                For i As Integer = c1Grid.Col To c1Grid.Columns.Count - 1
    '                    c1Grid.Columns(i).Text = c1Grid(c1Grid.Row - 1, i).ToString()
    '                    c1Grid.UpdateData()
    '                Next
    '            End If
    '        Else ' Chuỗi hoặc Ngày
    '            If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDateShort OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDate Then
    '                For i As Integer = c1Grid.Col To c1Grid.Columns.Count - 1
    '                    c1Grid.Columns(i).Text = c1Grid(c1Grid.Row - 1, i).ToString()
    '                    c1Grid.UpdateData()
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception
    '        D99C0008.Msg("Lỗi HotKeyF8: " & ex.Message)
    '    End Try


    'End Sub

    ''' <summary>
    ''' Nhấn F8 trên lưới để copy dòng trên xuống dòng dưới tính từ phải qua, TRỪ những dòng trong tập hợp iColExclude
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <param name="iColExclude">Tập cột loại trừ cần truyền vào, VD: Dim iColExclude() As Integer = {COL_TransID, COL_VoucherID}</param>
    ''' <remarks>Nhấn F8 thì gọi HotKeyF8 (tdbg, iColExclude)</remarks>
    Public Sub HotKeyF8(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ParamArray iColExclude() As Integer)
        Try
            If c1Grid.RowCount < 1 Then Exit Sub
            c1Grid.UpdateData() 'Bổ sung 24/09/2012
            Dim bExclude As Boolean = False
            Dim dtGrid As DataTable = CType(c1Grid.DataSource, DataTable)
            If c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns.Item(c1Grid.Col).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far Then ' Số
                If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse Val(c1Grid(c1Grid.Row, c1Grid.Col).ToString) = 0 Then
                    For i As Integer = c1Grid.Col To c1Grid.Columns.Count - 1
                        bExclude = False
                        For j As Integer = 0 To iColExclude.Length - 1
                            If i = iColExclude(j) Then
                                bExclude = True
                                Exit For
                            End If
                        Next j
                        If Not bExclude Then
                            'update 4/11/2013 -  Loại trừ các cột có Expression
                            If dtGrid.Columns.Contains(c1Grid.Columns(i).DataField) Then
                                If dtGrid.Columns(c1Grid.Columns(i).DataField).Expression <> "" Then Continue For
                            End If
                            c1Grid.Columns(i).Text = c1Grid(c1Grid.Row - 1, i).ToString()
                            c1Grid.UpdateData()
                        End If
                    Next
                End If
            Else ' Chuỗi hoặc Ngày
                If c1Grid(c1Grid.Row, c1Grid.Col).ToString = "" OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDateShort OrElse c1Grid(c1Grid.Row, c1Grid.Col).ToString = MaskFormatDate Then
                    For i As Integer = c1Grid.Col To c1Grid.Columns.Count - 1
                        bExclude = False
                        For j As Integer = 0 To iColExclude.Length - 1
                            If i = iColExclude(j) Then
                                bExclude = True
                                Exit For
                            End If
                        Next j
                        If Not bExclude Then
                            'update 4/11/2013 -  Loại trừ các cột có Expression
                            If dtGrid.Columns.Contains(c1Grid.Columns(i).DataField) Then
                                If dtGrid.Columns(c1Grid.Columns(i).DataField).Expression <> "" Then Continue For
                            End If
                            c1Grid.Columns(i).Text = c1Grid(c1Grid.Row - 1, i).ToString()
                            c1Grid.UpdateData()
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyF8: " & ex.Message)
        End Try



    End Sub

    ''' <summary>
    ''' Nhấn F11 trên lưới: Nhấn lần lẻ thì con trỏ nhảy đến lưới, lần chẳn con trỏ nhảy từ lưới về control trước đó
    ''' </summary>
    ''' <param name="frm">Form cần làm phím nóng, gán là Me </param>
    ''' <param name="c1Grid">Lưới C1</param>
    ''' <remarks>Tùy từng trường hợp MarqueeStyle</remarks>
    <DebuggerStepThrough()> _
    Public Sub HotKeyF11(ByVal frm As Form, ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, Optional ByVal iSplitFocus As Integer = 0, Optional ByVal iColFocus As Integer = -1)
        Try
            If c1Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder Or c1Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRowRaiseCell Then
                If (frm.ActiveControl.GetType.Name <> "GridEditor") And (frm.ActiveControl.GetType.Name <> "C1TrueDBGrid") Then 'Khong phai luoi

                    If (frm.ActiveControl.GetType.Name = "EditorCtrl") Or (frm.ActiveControl.GetType.Name = "C1TrueDBList") Then 'C1Combo
                        gcControl = frm.GetNextControl(frm.ActiveControl.Parent, False)
                    Else
                        gcControl = frm.GetNextControl(frm.ActiveControl, False)
                    End If

                    If iColFocus <> -1 Then
                        c1Grid.SplitIndex = iSplitFocus
                        c1Grid.Col = iColFocus
                    End If

                    c1Grid.Focus()
                Else
                    frm.SelectNextControl(gcControl, True, True, True, False)
                End If

            Else
                If (frm.ActiveControl.GetType.Name <> "GridEditor") And (frm.ActiveControl.GetType.Name <> "C1TrueDBGrid") Then 'Khong phai luoi

                    If (frm.ActiveControl.GetType.Name = "EditorCtrl") Or (frm.ActiveControl.GetType.Name = "C1TrueDBList") Then 'C1Combo
                        gcControl = frm.GetNextControl(frm.ActiveControl.Parent, False)
                    Else
                        gcControl = frm.GetNextControl(frm.ActiveControl, False)
                    End If
                    If iColFocus <> -1 Then
                        c1Grid.SplitIndex = iSplitFocus
                        c1Grid.Col = iColFocus
                    End If
                    c1Grid.Focus()
                Else
                    c1Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder
                    c1Grid.UpdateData()
                    frm.SelectNextControl(gcControl, True, True, True, False)
                    c1Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.FloatingEditor
                End If
            End If
        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyF11: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Nhấn phím Enter ở cột cuối cùng trên lưới thì nó sẽ nhảy xuống dòng kế tiếp
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <param name="nFirstCol">Cột đầu tiên Focus tới</param>
    ''' <param name="e"></param>
    ''' <param name="iSplitFocus">vị trí của split cần Focus</param>
    ''' <remarks>Gọi hàm tại sự kiện KeyDown của lưới</remarks>
    '''  Cách gọi :
    ''' If e.KeyCode = Keys.Enter Then            
    '''        If tdbg2.Col = iLastCol Then  HotKeyEnterGrid(tdbg, COL_VoucherNum, e)
    '''        Exit Sub
    ''' End If
    ''' Biến iLastCol = CountCol(tdbg, tdbg.Splits.Count - 1) được gọi tại
    ''' Form_Load đối với những lưới không có nhiều cột (không có nút nhấn để bật tắt các cột)
    ''' Sự kiện Click của nút đối với những lưới có nhiều cột (có nút nhấn để bật tắt các cột)
    ''' Lưu ý: đối với những lưới có ít cột mà các cột này không động thì gán chết cột cuối cùng cần hiển thị iLastCol = COL_Disabled
    <DebuggerStepThrough()> _
    Public Sub HotKeyEnterGrid(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal nFirstCol As Integer, ByVal e As System.Windows.Forms.KeyEventArgs, Optional ByVal iSplitFocus As Integer = 0)
        Try
            'Không gọi hàm CountCol tại đây (do khi gọi ) mà gọi trong code từng form
            With c1Grid
                .UpdateData()

                .SplitIndex = iSplitFocus
                If c1Grid.AllowAddNew = True Then
                    .Row = .Row + 1
                Else
                    .Row = CInt(IIf(.RowCount = .Row + 1, 0, .Row + 1))
                End If
                .Col = nFirstCol
                e.SuppressKeyPress = True
                e.Handled = True
            End With
        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyEnterGrid: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' (BỎ) Nhấn phím Shift+Insert ở trên lưới thì sẽ chèn thêm 1 dòng mới vào vị trí con trỏ đang đứng 
    ''' </summary>
    ''' <param name="C1Grid"></param>
    ''' <param name="SplitIndex"></param>
    ''' <param name="ColFirst"></param>
    ''' <param name="ColumnCount">Tổng số cột của lưới</param>
    ''' <remarks>Khai báo 1 biến Dim bAddNew As Boolean=False ở đầu form. Gọi hàm tại sự kiện KeyDown của lưới, đặt biến bAddNew =True  trước hàm HotKeyShiftInsert và viết thêm thông tin tại sự kiện RowColChange như sau:</remarks>
    '''     If bAddNew =True And c1Grid.AddNewMode = C1.Win.C1TrueDBGrid.AddNewModeEnum.AddNewCurrent Then
    '''     bAddNew=False         
    '''     c1Grid.Columns(COL_VoucherNum).Text = "" ' Gán 1 cột bất kỳ ="" cho lưới
    '''     End If
    <DebuggerStepThrough()> _
    Public Sub HotKeyShiftInsert(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal SplitIndex As Integer, ByVal ColFirst As Integer, ByVal ColumnCount As Integer)
        Dim Col As Integer
        Dim row As Integer
        Dim iBookmark As Int32 = 0

        If Not c1Grid.AllowAddNew Or c1Grid.RowCount < 1 Then Exit Sub

        Try
            'b1: Giữ lại Bookmark tại vị trí hiện tại
            iBookmark = c1Grid.Bookmark

            'b2: Insert 1 dòng mới
            c1Grid.SplitIndex = SplitIndex
            c1Grid.Col = ColFirst
            c1Grid.MoveLast()
            c1Grid.Row = c1Grid.Row + 1


            'b3: Đẩy dữ liệu xuống 1dòng so với vị trí hiện tại
            For row = c1Grid.RowCount - 1 To iBookmark Step -1
                If row = iBookmark Then
                    For Col = 0 To ColumnCount - 1
                        c1Grid(row, Col) = ""
                    Next Col
                Else
                    For Col = 0 To ColumnCount - 1
                        c1Grid(row, Col) = c1Grid(row - 1, Col)
                    Next
                End If
            Next

            c1Grid.Row = iBookmark
            c1Grid.Focus()
        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyShiftInsert: " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    '''  Nhấn phím Shift+Insert ở trên lưới thì sẽ chèn thêm 1 dòng mới vào vị trí con trỏ đang đứng (hàm  mới)
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <param name="COL_OrderNum">Cột STT trên lưới (nếu có)</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub HotKeyShiftInsert(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, Optional ByVal COL_OrderNum As Integer = -1)
        Dim iBookmark As Int32 = 0
        If Not c1Grid.AllowAddNew Or c1Grid.RowCount < 1 Then Exit Sub
        c1Grid.UpdateData()
        Try
            Dim dt As DataTable = CType(c1Grid.DataSource, DataTable)
            Dim dr As DataRow = dt.NewRow
            dt.Rows.InsertAt(dr, c1Grid.Row)
            Dim bm As Integer = c1Grid.Row - 1
            If COL_OrderNum <> -1 Then
                For i As Integer = bm To c1Grid.RowCount - 1
                    c1Grid(i, COL_OrderNum) = i + 1
                Next
            End If
            c1Grid.Row = bm
            c1Grid.Focus()
        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyShiftInsert: " & ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Nhấn phím Ctrl+Insert: Thêm dòng mới
    ''' Nhấn phím Ctrl+Delete: Xóa dòng hiện tại
    ''' Nhấn phím Ctrl+Home: Con trỏ nhảy đến cột nhập liệu đầu tiên trên cùng 1 dòng
    ''' Nhấn phím Ctrl+End: Con trỏ nhảy đến cột nhập liệu cuối cùng trên cùng 1 dòng
    ''' Nhấn phím Ctrl+PageUp: Con trỏ nhảy đến dòng đầu tiên trên cùng 1 cột
    ''' Nhấn phím Ctrl+PageDown: Con trỏ nhảy đến dòng cuối cùng trên cùng 1 cột
    ''' Nhấn phím Ctrl+Right: Con trỏ nhảy đến cột đầu tiên của split liền sau
    ''' Nhấn phím Ctrl+Left: Con trỏ nhảy đến cột đầu tiên của split liền trước
    ''' Nhấn phím Ctrl+D: Copy diễn giải Master xuống Detail
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="c1Grid"></param>
    ''' <param name="ColFirst"></param>
    ''' <param name="SplitFirst"></param>
    ''' <param name="SplitLast"></param>
    ''' <param name="ColFocusKeyHome"></param>
    ''' <param name="ColFocusKeyLeft"></param>
    ''' <param name="ColFocusKeyRight"></param>
    ''' <param name="ColDescription"></param>
    ''' <param name="ValueDescription"></param>
    ''' <remarks>Gọi hàm tại sự kiện KeyDown của lưới </remarks>
    <DebuggerStepThrough()> _
    Public Sub HotKeyDownGrid(ByVal e As System.Windows.Forms.KeyEventArgs, _
    ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, _
    ByVal ColFirst As Integer, _
    Optional ByVal SplitFirst As Integer = 0, _
    Optional ByVal SplitLast As Integer = 0, _
    Optional ByVal ColFocusKeyHome As Boolean = True, _
    Optional ByVal ColFocusKeyLeft As Boolean = True, _
    Optional ByVal ColFocusKeyRight As Boolean = True, _
    Optional ByVal ColDescription As Integer = -1, _
    Optional ByVal ValueDescription As String = "")
        Try

            If e.Control Then
                With c1Grid
                    Select Case e.KeyCode

                        Case Keys.Insert 'Nhấn các phím Ctrl+Insert: Thêm dòng mới
                            If Not c1Grid.AllowAddNew Or c1Grid.RowCount < 1 Then Exit Sub

                            If .Row = .VisibleRows - 1 Then .FirstRow = .Row
                            .SplitIndex = SplitFirst
                            .Col = ColFirst
                            .MoveLast()
                            .Row = CInt(IIf(.RowCount - 1 = .Row, .RowCount, .Row + 1))


                        Case Keys.Delete 'Nhấn các phím Ctrl+Delete: Xóa dòng hiện tại
                            If Not c1Grid.AllowDelete Or c1Grid.RowCount < 1 Then Exit Sub

                            If .Bookmark <> .Row + .FirstRow Then Exit Sub

                            If D99C0008.MsgAskDeleteRow() = Windows.Forms.DialogResult.Yes Then
                                .Delete(.Bookmark)
                                .UpdateData()
                            Else
                                e.Handled = True
                            End If

                        Case Keys.Home ' Nhấn phím Ctrl+Home: Trở về cột nhập liệu đầu tiên trên cùng 1 dòng

                            .SplitIndex = SplitFirst
                            .Col = ColVisible(c1Grid, .SplitIndex, True)

                        Case Keys.End ' Nhấn phím Ctrl+End: Trở về cột nhập liệu cuối cùng trên cùng 1 dòng

                            If SplitLast = 0 Then
                                .SplitIndex = .Splits.Count - 1
                            Else
                                .SplitIndex = SplitLast
                            End If
                            .Row = .Bookmark
                            .Col = CountCol(c1Grid, .SplitIndex)
                        Case Keys.PageUp ' Nhấn phím Ctrl+PageUp: Trở về dòng đầu tiên trên cùng 1 cột
                            e.SuppressKeyPress = True
                            e.Handled = True
                            .MoveFirst()
                        Case Keys.PageDown ' Nhấn phím Ctrl+PageDown: Trở về dòng cuối cùng trên cùng 1 cột
                            e.SuppressKeyPress = True
                            e.Handled = True
                            .MoveLast()
                        Case Keys.Right ' Nhấn phím Ctrl+Right: Con trỏ nhảy đến cột đầu tiên của split liền sau
                            e.Handled = True
                            If .SplitIndex < .Splits.Count - 1 Then .SplitIndex += 1
                            .Col = ColVisible(c1Grid, .SplitIndex, ColFocusKeyRight)

                            .UpdateData()

                        Case Keys.Left ' Nhấn phím Ctrl+Left: Con trỏ nhảy đến cột đầu tiên của split liền trước
                            e.Handled = True
                            If .SplitIndex > 0 Then .SplitIndex -= 1
                            .Col = ColVisible(c1Grid, .SplitIndex, ColFocusKeyLeft)

                            .UpdateData()

                        Case Keys.D ' Nhấn phím Ctrl+D: Copy diễn giải Master xuống Detail
                            .UpdateData()   'Update 30/10/2012
                            If ValueDescription.Trim = "" Or .RowCount < 1 Or ColDescription < 0 Then Exit Sub

                            Dim i As Integer
                            Dim nvMsg As DialogResult
                            nvMsg = D99C0008.MsgCopyDesctiption

                            Select Case nvMsg
                                Case Windows.Forms.DialogResult.Yes
                                    For i = 0 To .RowCount - 1
                                        c1Grid(i, ColDescription) = Trim(ValueDescription)
                                    Next i
                                    .UpdateData()
                                Case Windows.Forms.DialogResult.No
                                    For i = 0 To .RowCount - 1
                                        If c1Grid(i, ColDescription).ToString = "" Then
                                            c1Grid(i, ColDescription) = Trim(ValueDescription)
                                        End If
                                    Next i
                                    .UpdateData()
                                Case Else
                                    Exit Sub
                            End Select
                    End Select


                End With
            End If
        Catch ex As Exception
            D99C0008.Msg("Lỗi HotKeyDownGrid: " & ex.Message)
        End Try

    End Sub

    Public Sub HotKeyCtrlVOnGrid(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal e As System.Windows.Forms.KeyEventArgs, Optional ByVal COL_OrderNum As Integer = -1)
        If e.Control = True And e.KeyCode = Keys.V Then
            If tdbg.Col = COL_OrderNum Then
                e.Handled = True
                Exit Sub
            Else
                If tdbg.FilterActive Then
                    If tdbg.Columns(tdbg.Col).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox Then Exit Sub
                    tdbg.Columns(tdbg.Col).FilterText = Clipboard.GetText()
                    tdbg.EditActive = True
                ElseIf tdbg.AllowUpdate And tdbg.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder Then
                    tdbg.Columns(tdbg.Col).Text = Clipboard.GetText()
                    tdbg.EditActive = True
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Trả về cột cuối cùng có hiển thị
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <param name="Split"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CountCol(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, _
    ByVal Split As Integer) As Integer
        For i As Integer = c1Grid.Columns.Count - 1 To 0 Step -1
            'Bổ sung 24/09/2012: Đk cột lock
            If c1Grid.Splits(Split).DisplayColumns(i).Visible And c1Grid.Splits(Split).DisplayColumns(i).Locked = False Then Return i
        Next i
        Return 0
    End Function

    ''' <summary>
    ''' Khi gõ phím Enter cũng giống như phím Tab
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub UseEnterAsTab(ByVal frm As Form, Optional ByVal bForward As Boolean = True)
        Try
            If (frm.ActiveControl.GetType.Name <> "GridEditor") And (frm.ActiveControl.GetType.Name <> "C1TrueDBGrid") Then 'Khong phai luoi 
                frm.SelectNextControl(frm.ActiveControl, bForward, True, True, False)
            End If
        Catch ex As Exception
            D99C0008.Msg("Lỗi UseEnterAsTab: " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Kiểm tra khi nhấn trên FilterBar của cột checkBox và STT (tăng tự động client)
    ''' </summary>
    ''' <param name="KeyChar">Ký tự cần kiểm tra</param>
    ''' <param name="bOrderNum">Optional cột STT. Mặc định là False</param>
    ''' <returns>Trả về False nếu phím nhấn hợp lệ, ngược lại trả về True</returns>
    ''' <remarks>bOrderNum = FALSE : kiểm tra cột CheckBox. Ngược lại là cột STT</remarks>
    Public Function CheckKeyPress(ByVal KeyChar As Char, Optional ByVal bOrderNum As Boolean = False) As Boolean
        Select Case KeyChar
            Case ChrW(Keys.Space)
                Return bOrderNum
            Case ChrW(Keys.Enter), ChrW(Keys.Tab)
                Return False
            Case Else
                Return True
        End Select
        Return False
    End Function

    '''' <summary>
    '''' Kiểm tra phím nhấn
    '''' </summary>
    '''' <param name="KeyChar">Ký tự cần kiểm tra</param>
    '''' <param name="TypeCheck">Loại dữ liệu cần kiểm tra</param>
    '''' <param name="CheckString">Chuỗi cần kiểm tra. Chỉ chấp nhận nếu tham số đầu tiên là Custom</param>
    '''' <returns>Trả về True nếu phím nhấn hợp lệ, ngược lại trả về False</returns>
    '<DebuggerStepThrough()> _
    'Public Function CheckKeyPress(ByVal KeyChar As Char, ByVal TypeCheck As EnumKey, Optional ByVal CheckString As String = "") As Boolean
    '    Dim sCheck As String = ""
    '    If KeyChar = Chr(91) Then Return True ' [
    '    If KeyChar = Chr(93) Then Return True ']
    '    If KeyChar = Chr(8) Then Return False
    '    If KeyChar = Chr(9) Then Return False
    '    If KeyChar = Chr(13) Then Return False
    '    Select Case TypeCheck
    '        Case EnumKey.Number : sCheck = "0123456789"
    '        Case EnumKey.NumberDot : sCheck = "0123456789."
    '        Case EnumKey.NumberSign : sCheck = "0123456789-"
    '        Case EnumKey.NumberDotSign : sCheck = "0123456789.-"
    '        Case EnumKey.Custom : sCheck = CheckString
    '    End Select
    '    Return sCheck.IndexOf(KeyChar) < 0
    'End Function
    Public Function CheckKeyPress(ByVal KeyChar As Char, ByVal TypeCheck As EnumKey, Optional ByVal CheckString As String = "") As Boolean
        Dim sCheck As String = ""
        Select Case KeyChar
            Case Chr(91), Chr(93) '[ (91), ] (93)
                If TypeCheck = EnumKey.Custom Then
                    If CheckString.Contains(KeyChar) Then Return False
                Else
                    Return True
                End If
            Case Chr(8), Chr(9), Chr(13), Chr(3), Chr(22), Chr(24) 'BS, Tab, Enter, Ctrl + C; Ctrl + X; Ctrl + V
                Return False
        End Select
        Select Case TypeCheck
            Case EnumKey.Number : sCheck = "0123456789"
            Case EnumKey.NumberDot : sCheck = "0123456789."
            Case EnumKey.NumberSign : sCheck = "0123456789-"
            Case EnumKey.NumberDotSign : sCheck = "0123456789.-"
            Case EnumKey.Custom : sCheck = CheckString
        End Select
        Return sCheck.IndexOf(KeyChar) < 0
    End Function

    ''' <summary>
    ''' Nhấn phím enter sẽ được hiểu bằng phím Tab
    ''' </summary>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub FormKeyEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim con As Form = CType(sender, Form)
        If e.KeyCode = Keys.Enter Then
            UseEnterAsTab(con)
        End If
    End Sub

    ''' <summary>
    ''' (BỎ) Nhấn phím enter sẽ được hiểu bằng phím Tab
    ''' </summary>
    ''' <remarks>Chỉ có tác dụng trên: TextBox, CheckBox, C1Combo</remarks>
    <DebuggerStepThrough()> _
    Public Sub FormKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim con As Form = CType(sender, Form)
        If e.KeyChar = Chr(13) Then
            Select Case con.ActiveControl.GetType.ToString
                Case "System.Windows.Forms.TextBox"
                    SendKeys.Send("{TAB}")
                Case "System.Windows.Forms.CheckBox"
                    SendKeys.Send("{TAB}")
                Case "C1.Win.C1List.EditorCtrl"
                    SendKeys.Send("{TAB}")
                Case "C1.Win.C1Input.C1DateEdit"
                    SendKeys.Send("{TAB}")
            End Select
        End If
    End Sub
#End Region

#Region "Kiểm tra nhập Mã (BỎ)"
    '''' <summary>
    ''''  ''' Kiểm tra chỉ cho phép nhập ký tự theo chuẩn 
    '''' Set UpperCase, Length=20
    '''' </summary>
    '''' <remarks>Gọi hàm này tại form_load, bổ sung thêm trường hợp nhập công thức không kiểm tra []</remarks>
    'Dim bCheckFormula As Boolean = False

    'Public Sub CheckIdTextBox(ByRef txtID As TextBox, Optional ByVal iLength As Integer = 20, Optional ByVal bFormula As Boolean = False)
    '    bCheckFormula = bFormula
    '    txtID.CharacterCasing = CharacterCasing.Upper
    '    txtID.MaxLength = iLength
    '    AddHandler txtID.KeyPress, New KeyPressEventHandler(AddressOf txtID_KeyPress)
    '    AddHandler txtID.KeyDown, New KeyEventHandler(AddressOf txtID_KeyDown)
    '    AddHandler txtID.MouseDown, New MouseEventHandler(AddressOf txtID_MouseDown)
    'End Sub


    'Private Sub txtID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Dim txt As TextBox = CType(sender, TextBox)
    '    e.Handled = CheckIdCharactor(e.KeyChar, txt.MaxLength, bCheckFormula)
    'End Sub

    'Private Sub txtID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    'Không cho paste ký tự đặc biệt bằng phím
    '    If e.Control = True And e.KeyCode = Keys.V Then
    '        If Clipboard.GetText.Trim <> "" Then Clipboard.SetText(Clipboard.GetText.Trim)
    '        If CheckIdCharactor(Clipboard.GetText) Then Clipboard.Clear()
    '    End If
    'End Sub

    'Private Sub txtID_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    'Không cho paste ký tự đặc biệt bằng chuột
    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        If Clipboard.GetText.Trim <> "" Then Clipboard.SetText(Clipboard.GetText.Trim)
    '        Dim txt As TextBox = CType(sender, TextBox)
    '        If CheckIdCharactor(Clipboard.GetText, txt.MaxLength, bCheckFormula) Then Clipboard.Clear()
    '    End If
    'End Sub


    'Public Function CheckIdCharactor(ByVal str As String, Optional ByVal iLength As Integer = 20, Optional ByVal bFormula As Boolean = False) As Boolean
    '    If str.Length > iLength Then
    '        Return True
    '    End If
    '    'BackSpace: 8
    '    For Each c As Char In str
    '        If (c < Chr(33) Or c > Chr(127)) And c <> Chr(8) Then
    '            Return True
    '        End If
    '    Next
    '    'Chuỗi không cho gõ ký tự đặc biệt này
    '    If bFormula = False Then
    '        If str.IndexOf(Chr(91)) >= 0 Then Return True ' [
    '        If str.IndexOf(Chr(93)) >= 0 Then Return True ']
    '    End If
    '    If str.IndexOf(Chr(94)) >= 0 Then Return True '^
    '    If str.IndexOf(Chr(39)) >= 0 Then Return True ' '
    '    If str.IndexOf(Chr(37)) >= 0 Then Return True ' %
    '    Return False
    'End Function
#End Region

#Region "Kiểm tra nhập Mã - Công thức"

#Region "TextBox"

    ''' <summary>
    ''' Kiểm tra TextBox Nhập Mã/Công thức
    ''' </summary>
    ''' <param name="txtID">Control cần kiểm tra</param>
    ''' <param name="iLength">Chiều dài nhập liệu</param>
    ''' <param name="bFormula">Theo kiểu công thức (Default: False - cho Mã)</param>
    ''' <remarks>Cho một textbox, đối số bFormula : Kiểu kiểm tra là Mã hay Công thức</remarks>
    Public Sub CheckIdTextBox(ByRef txtID As TextBox, Optional ByVal iLength As Integer = 20, Optional ByVal bFormula As Boolean = False)
        'If txtID.ReadOnly OrElse txtID.Enabled = False Then Exit Sub'Có trường hợp do checkbox mà sáng /mờ textbox

        txtID.CharacterCasing = CharacterCasing.Upper
        txtID.MaxLength = iLength
        txtID.Tag = bFormula.ToString
        AddHandler txtID.KeyDown, AddressOf txtID_KeyDown
        AddHandler txtID.Validating, AddressOf txtID_Validating
    End Sub

    ''' <summary>
    ''' Kiểm tra nhiều TextBox Nhập Mã/Công thức
    ''' </summary>
    ''' <param name="txtID">Control cần kiểm tra</param>
    ''' <param name="iLength">Chiều dài nhập liệu</param>
    ''' <param name="bFormula">Theo kiểu công thức (Default: False - cho Mã)</param>
    ''' <remarks>Cho một textbox, đối số bFormula : Kiểu kiểm tra là Mã hay Công thức</remarks>
    Public Sub CheckIdTextBox(ByRef txtID() As TextBox, Optional ByVal iLength As Integer = 20, Optional ByVal bFormula As Boolean = False)
        For i As Integer = 0 To txtID.Length - 1
            CheckIdTextBox(txtID(i), iLength, bFormula)
        Next
    End Sub
#End Region

#Region "Lưới"

    ''' <summary>
    ''' Kiểm tra cột Mã trên lưới dạng Integer
    ''' </summary>
    ''' <param name="tdbg">grid name</param>
    ''' <param name="iCol">col id</param>
    ''' <param name="bFormula">Enter ID or Formula (Default : False - ID)</param>
    ''' <remarks>Put DxxFxxxx_Load events (Exp: CheckIdTDBGrid(tdbg, COL_???, ???))</remarks>
    Public Sub CheckIdTDBGrid(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As Integer, ByVal bFormula As Boolean)
        Dim txtIDGrid As New TextBox
        txtIDGrid.BorderStyle = BorderStyle.None
        CheckIdTextBox(txtIDGrid, tdbg.Columns(iCol).DataWidth, bFormula)
        tdbg.Columns(iCol).Editor = txtIDGrid

        AddHandler tdbg.RowColChange, AddressOf tdbg_RowColChange
        AddHandler txtIDGrid.LostFocus, AddressOf txtID_LostFocus
    End Sub


    Private Sub tdbg_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs)
        Dim tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid = CType(sender, C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        Dim txtID As TextBox = CType(tdbg.Columns(tdbg.Col).Editor, TextBox)
        If txtID Is Nothing OrElse txtID.Tag Is Nothing Then Exit Sub
        If tdbg.Row Mod 2 = 0 Then 'Dòng chẵn
            txtID.BackColor = tdbg.EvenRowStyle.BackColor
        Else
            txtID.BackColor = tdbg.OddRowStyle.BackColor
        End If

    End Sub

    ''' <summary>
    ''' Kiểm tra cột Mã trên lưới dạng String
    ''' </summary>
    ''' <param name="tdbg">grid name</param>
    ''' <param name="iCol">col id</param>
    ''' <param name="bFormula">Enter ID or Formula (Default : False - ID)</param>
    ''' <remarks>Put DxxFxxxx_Load events (Exp: CheckIdTDBGrid(tdbg, COL_???, ???))</remarks>
    Public Sub CheckIdTDBGrid(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As String, ByVal bFormula As Boolean)
        CheckIdTDBGrid(tdbg, IndexOfColumn(tdbg, iCol), bFormula)
    End Sub

    ''' <summary>
    ''' Kiểm tra nhiều cột Mã trên lưới dạng Integer
    ''' </summary>
    ''' <param name="tdbg">grid name</param>
    ''' <param name="iCol">col id</param>
    ''' <param name="bFormula">Enter ID or Formula (Default : False - ID)</param>
    ''' <remarks>Put DxxFxxxx_Load events with arrCol is Integer(Exp: CheckIdTDBGrid(tdbg, arrCol, ???))</remarks>
    Public Sub CheckIdTDBGrid(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol() As Integer, ByVal bFormula As Boolean)
        For i As Integer = 0 To iCol.Length - 1
            CheckIdTDBGrid(tdbg, iCol(i), bFormula)
        Next
    End Sub

    ''' <summary>
    ''' Tạo cột Mã trên lưới
    ''' </summary>
    ''' <param name="tdbg">grid name</param>
    ''' <param name="iCol">col id</param>
    ''' <param name="bFormula">Enter ID or Formula (Default : False - ID)</param>
    ''' <remarks>Put DxxFxxxx_Load events with arrCol is String(Exp: CheckIdTDBGrid(tdbg, arrCol, ???))</remarks>
    Public Sub CheckIdTDBGrid(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol() As String, ByVal bFormula As Boolean)
        For i As Integer = 0 To iCol.Length - 1
            CheckIdTDBGrid(tdbg, IndexOfColumn(tdbg, iCol(i)), bFormula)
        Next
    End Sub

    ''' <summary>
    ''' Kiểm tra Mã nhập vào lưới
    ''' </summary>
    ''' <param name="tdbg">grid name</param>
    ''' <param name="iCol">col id</param>
    ''' <returns>True/False</returns>
    ''' <remarks>Put tdbg_BeforeColUpdate events (Exp: e.Cancel = L3IsID(tdbg, e.ColIndex, ???))</remarks>
    Public Function L3IsID(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As Integer) As Boolean
        Return L3IsIDFormula(tdbg, iCol, False)
    End Function

    ''' <summary>
    ''' Kiểm tra Công thức nhập vào lưới
    ''' </summary>
    ''' <param name="tdbg">grid name</param>
    ''' <param name="iCol">col id</param>
    ''' <returns>True/False</returns>
    ''' <remarks>Put tdbg_BeforeColUpdate events (Exp: e.Cancel = L3IsID(tdbg, e.ColIndex, ???))</remarks>
    Public Function L3IsFormula(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As Integer) As Boolean
        Return L3IsIDFormula(tdbg, iCol, True)
    End Function

#End Region

#Region "Các hàm private kiểm tra nhập Mã, Công thức"
    ''' <summary>
    ''' Thay đổi vị trí Select của chuỗi Vni
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="posFrom">vị trí bắt đầu</param>
    ''' <param name="posTo">Số ký tự được Select</param>
    ''' <remarks>Không cần kiểm tra khi Unicode</remarks>
    Private Sub ChangePositionIndexVNI(ByVal str As String, ByRef posFrom As Integer, ByRef posTo As Integer)
        If str = "" OrElse posFrom < 0 OrElse posFrom >= str.Length - 1 Then Exit Sub

        Dim arrChar() As String = {"Â", "Á", "À", "Å", "Ä", "Ã", "Ù", "Ø", "Û", "Õ", "Ï", "É", "È", "Ú", "Ü", "Ë", "Ê"}
        Dim c As String = (str.Substring(posFrom, 1)).ToUpper
        '"Ö", "Ô"
        Select Case c
            Case "Ö", "Ô" 'Ö: Ư; Ô: Ơ - không tăng vị trí, ngược lại thì tăng thêm 1 vị trí
                If L3FindArrString(arrChar, (str.Substring(posFrom + 1, 1)).ToUpper) Then posTo = 2
            Case Else 'kiểm tra trong danh sách arrChar
                If L3FindArrString(arrChar, c) Then
                    If posFrom > 0 Then posFrom -= 1
                    posTo = 2
                End If
        End Select
    End Sub


    'Kiểm tra Button Đóng có đặt Tên "Close"
    Private Function CheckContinue(ByVal ctrl As Control) As Boolean
        Try
            Dim form As Form = CType(ctrl.TopLevelControl, Form)
            If form.Controls.ContainsKey("btnClose") Then
                Dim btnClose As Control = CType(form.Controls("btnClose"), System.Windows.Forms.Button)
                If btnClose Is Nothing Then Return True 'không có nút đóng
                If btnClose.Focused Then Return False
                Dim arr() As String = ctrl.Tag.ToString.Split(";"c)
                If arr.Length > 1 Then Return False
                '************
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function

    Private Sub txtID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim txtID As TextBox = CType(sender, TextBox)
        If txtID.ReadOnly OrElse txtID.Enabled = False Then Exit Sub
        'Nếu nhấn đóng thì không cần hiện thông báo
        If CheckContinue(txtID) = False Then Exit Sub
        '************
        Dim posFrom As Integer
        If txtID.Tag Is Nothing OrElse L3Bool(txtID.Tag) = False Then
            posFrom = IndexIdCharactor(txtID.Text)
        Else
            posFrom = IndexFormulaCharactor(txtID.Text)
        End If
        Dim posTo As Integer = 1
        If txtID.Font.Name.Contains("Lemon3") Then ChangePositionIndexVNI(txtID.Text, posFrom, posTo)
        Select Case posFrom
            Case -1 'thỏa điều kiện
            Case -2 'Vượt chiều dài
                'D99C0008.MsgL3("Chiều dài vượt quá quy định.")
                'txtID.SelectAll()
                'e.Cancel = True
            Case Else 'vi phạm
                D99C0008.MsgL3(rl3("Ma_co_ky_tu_khong_hop_le"))
                txtID.Focus()
                e.Cancel = True
                txtID.Select(posFrom, posTo)
        End Select
    End Sub

    Private Sub txtID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Modifiers <> Keys.Alt Then Exit Sub
        If e.KeyCode = Keys.N Then
            Dim txtID As TextBox = CType(sender, TextBox)
            txtID.Tag = txtID.Tag.ToString & ";True"
        End If
    End Sub

    Private Sub txtID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtID As TextBox = CType(sender, TextBox)
        'Nếu không phải trên lưới thì không cần sự kiện này
        Dim tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid = CType(txtID.Parent, C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        tdbg.Focus()
    End Sub



#Region "Dùng cho lưới"

    Private Function L3IsIDFormula(ByRef tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As Integer, ByVal bFormula As Boolean) As Boolean
        tdbg.Focus() 'bắt buộc : focus cột kế tiếp khi Enter
        If CheckContinue(tdbg) = False Then Return False
        If bFormula Then
            Return CheckFormulaCharactor(tdbg.Columns(iCol).Text)
        Else
            Return CheckIdCharactor(tdbg.Columns(iCol).Text)
        End If
    End Function

    ''' <summary>
    ''' Kiểm tra Mã hợp lệ 
    ''' </summary>
    ''' <param name="str">Chuỗi kiểm tra</param>
    ''' <returns>Vị trí ký tự vi phạm</returns>
    ''' <remarks></remarks>
    Private Function IndexIdCharactor(ByVal str As String) As Integer
        '  If str.Length > iLength Then Return -2 'vượt chiều dài
        'BackSpace: 8
        For Each c As Char In str
            Select Case AscW(c)
                Case 13, 10 'Mutiline của textbox và phím Enter
                    Continue For
                Case Is < 33, Is > 127, 37, 39, 91, 93, 94 'Các ký tự đặc biệt: 37(%) 39(') 91([) 93(]) 94(^)
                    Return str.IndexOf(c)
            End Select
        Next
        Return -1
    End Function

    ''' <summary>
    ''' Kiểm tra Mã hợp lệ 
    ''' </summary>
    ''' <param name="str">Chuỗi kiểm tra</param>
    ''' <returns>Vị trí ký tự vi phạm</returns>
    ''' <remarks></remarks>
    Public Function CheckIdCharactor(ByVal str As String) As Boolean
        Return IndexIdCharactor(str) >= 0
    End Function

    '''' Kiểm tra công thức hợp lệ
    '''' </summary>
    '''' <param name="str">Chuỗi kiểm tra</param>
    '''' <returns>Vị trí ký tự vi phạm</returns>
    '''' <remarks></remarks>
    Private Function IndexFormulaCharactor(ByVal str As String) As Integer
        '  If str.Length > iLength Then Return -2 'vượt chiều dài
        'BackSpace: 8
        For Each c As Char In str
            Select Case AscW(c)
                Case 13, 10 'Mutiline của textbox và phím Enter
                    Continue For
                Case Is < 33, Is > 127, 94 ''Các ký tự đặc biệt: 94(^)
                    Return str.IndexOf(c)
            End Select
        Next
        Return -1
    End Function

    Private Function CheckFormulaCharactor(ByVal str As String) As Boolean
        Return IndexFormulaCharactor(str) >= 0
    End Function



#End Region


#End Region

#End Region

#Region "Nhấn Head_Click trên lưới"
    ''' <summary>
    ''' Copy nhiều cột liên quan nhưng không liên tục
    ''' </summary>
    ''' <param name="c1Grid"></param>
    ''' <param name="ColCopy">Cột cần copy</param>
    ''' <param name="iColumns">Tập cột liên quan cần copy</param>
    ''' <remarks>Gọi hàm tại sự kiên tdbg_HeadClick như sau:</remarks>
    ''' Case COL_NewSalaryLevelID2 
    ''' Dim iCols() As Integer = {COL_NewSaCoefficient2, COL_NewSaCoefficient22, COL_NewSaCoefficient23, COL_NewSaCoefficient24, COL_NewSaCoefficient25}
    ''' CopyColumnArr(tdbg, tdbg.Col, iCols()) 
    <DebuggerStepThrough()> _
    Public Sub CopyColumnArr(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ColCopy As Integer, ByVal iColumns() As Integer)

        Try
            If c1Grid.RowCount < 2 OrElse c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns(ColCopy).Locked Then Exit Sub
            ' Copy nhieu cot lien quan 
            c1Grid.UpdateData()

            Dim Flag As DialogResult

            Dim sValue As String = ""
            Dim RowCopy As Integer = c1Grid.Row '0

            sValue = c1Grid(RowCopy, ColCopy).ToString

            Flag = MessageBox.Show(rl3("Copy_cot_du_lieu_cho") & vbCrLf & rl3("____-_Tat_ca_cac_cot_(nhan_Yes)") & vbCrLf & rl3("____-_Nhung_dong_con_trong_(nhan_No)"), MsgAnnouncement, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If Flag = Windows.Forms.DialogResult.No Then ' Copy nhung dong con trong 
                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    If c1Grid(i, ColCopy).ToString = "" OrElse c1Grid(i, ColCopy).ToString = MaskFormatDateShort OrElse c1Grid(i, ColCopy).ToString = MaskFormatDate OrElse (L3IsNumeric(c1Grid(i, ColCopy).ToString) And Val(c1Grid(i, ColCopy).ToString) = 0) Then
                        c1Grid(i, ColCopy) = sValue
                        For j As Integer = 0 To iColumns.Length - 1
                            c1Grid(i, iColumns(j)) = c1Grid(RowCopy, iColumns(j))
                        Next j
                    End If
                Next

            ElseIf Flag = Windows.Forms.DialogResult.Yes Then ' Copy hết 
                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    c1Grid(i, ColCopy) = sValue
                    For j As Integer = 0 To iColumns.Length - 1
                        c1Grid(i, iColumns(j)) = c1Grid(RowCopy, iColumns(j))
                    Next j
                Next

            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Copy giá trị trong 1 cột (khi nhấn Head_Click)
    ''' </summary>
    ''' <param name="c1Grid">Lưới C1</param>
    ''' <param name="ColCopy">Cột cần copy</param>
    ''' <param name="sValue">Giá trị cần copy</param>
    ''' <param name="RowCopy">Dòng đang copy</param>
    ''' <remarks>Chỉ dùng copy những cột dữ liệu không liên quan đến các cột khác, copy cả giá trị ''</remarks>
    <DebuggerStepThrough()> _
    Public Sub CopyColumns(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ColCopy As Integer, ByVal sValue As String, ByVal RowCopy As Int32)
        Try
            'If sValue = "" Or c1Grid.RowCount < 2 Then Exit Sub
            c1Grid.UpdateData()
            If c1Grid.RowCount < 2 OrElse c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns(ColCopy).Locked Then Exit Sub

            sValue = c1Grid(RowCopy, ColCopy).ToString

            Dim Flag As DialogResult
            Flag = MessageBox.Show(rl3("Copy_cot_du_lieu_cho") & vbCrLf & rl3("____-_Tat_ca_cac_cot_(nhan_Yes)") & vbCrLf & rl3("____-_Nhung_dong_con_trong_(nhan_No)"), MsgAnnouncement, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If Flag = Windows.Forms.DialogResult.No Then ' Copy nhung dong con trong

                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    If c1Grid(i, ColCopy).ToString = "" OrElse c1Grid(i, ColCopy).ToString = MaskFormatDateShort OrElse c1Grid(i, ColCopy).ToString = MaskFormatDate OrElse (L3IsNumeric(c1Grid(i, ColCopy).ToString) And Val(c1Grid(i, ColCopy).ToString) = 0) Then c1Grid(i, ColCopy) = sValue
                Next
                'c1Grid(RowCopy, ColCopy) = sValue

            ElseIf Flag = Windows.Forms.DialogResult.Yes Then ' Copy het
                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    c1Grid(i, ColCopy) = sValue
                Next
                'c1Grid(0, ColCopy) = sValue
            Else
                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub CopyColumns(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ColCopy As Integer, ByVal sValue As String, ByVal RowCopy As Int32, ByVal Col_Where As Integer, ByVal sValueWhere As String)
        Try
            'If sValue = "" Or c1Grid.RowCount < 2 Then Exit Sub
            c1Grid.UpdateData()
            If c1Grid.RowCount < 2 OrElse c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns(ColCopy).Locked Then Exit Sub

            sValue = c1Grid(RowCopy, ColCopy).ToString

            Dim Flag As DialogResult
            Flag = MessageBox.Show(rl3("Copy_cot_du_lieu_cho") & vbCrLf & rl3("____-_Tat_ca_cac_cot_(nhan_Yes)") & vbCrLf & rl3("____-_Nhung_dong_con_trong_(nhan_No)"), MsgAnnouncement, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If Flag = Windows.Forms.DialogResult.No Then ' Copy nhung dong con trong

                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    If c1Grid(i, Col_Where).ToString <> sValueWhere Then Continue For
                    If c1Grid(i, ColCopy).ToString = "" OrElse c1Grid(i, ColCopy).ToString = MaskFormatDateShort OrElse c1Grid(i, ColCopy).ToString = MaskFormatDate OrElse (L3IsNumeric(c1Grid(i, ColCopy).ToString) And Val(c1Grid(i, ColCopy).ToString) = 0) Then c1Grid(i, ColCopy) = sValue
                Next
                'c1Grid(RowCopy, ColCopy) = sValue

            ElseIf Flag = Windows.Forms.DialogResult.Yes Then ' Copy het
                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    If c1Grid(i, Col_Where).ToString <> sValueWhere Then Continue For
                    c1Grid(i, ColCopy) = sValue
                Next
                'c1Grid(0, ColCopy) = sValue
            Else
                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Copy giá trị trong 1 cột có liên quan đến các cột kế nó (khi nhấn Head_Click)
    ''' </summary>
    ''' <param name="c1Grid">Lưới C1</param>
    ''' <param name="ColCopy">Cột cần copy</param>
    ''' <param name="RowCopy">Dòng cần copy</param>
    ''' <param name="ColumnCount">Số cột liên quan khi cần copy</param>
    ''' <remarks>Chỉ copy những cột ở vị trí liên tục nhau</remarks>
    <DebuggerStepThrough()> _
    Public Sub CopyColumns(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ColCopy As Integer, ByVal RowCopy As Integer, ByVal ColumnCount As Integer, ByVal sValue As String)
        Dim i, j As Integer
        Try
            If c1Grid.RowCount < 2 OrElse c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns(ColCopy).Locked Then Exit Sub

            If ColumnCount = 1 Then ' Copy trong 1 cot
                CopyColumns(c1Grid, ColCopy, sValue, RowCopy)
            ElseIf ColumnCount > 1 Then ' Copy nhieu cot lien quan
                c1Grid.UpdateData()
                sValue = c1Grid(RowCopy, ColCopy).ToString

                Dim Flag As DialogResult
                'Flag = D99C0008.MsgCopyColumn()

                Flag = MessageBox.Show(rl3("Copy_cot_du_lieu_cho") & vbCrLf & rl3("____-_Tat_ca_cac_cot_(nhan_Yes)") & vbCrLf & rl3("____-_Nhung_dong_con_trong_(nhan_No)"), MsgAnnouncement, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                If Flag = Windows.Forms.DialogResult.No Then ' Copy nhung dong con trong
                    For i = RowCopy + 1 To c1Grid.RowCount - 1
                        j = 1
                        If c1Grid(i, ColCopy).ToString = "" OrElse c1Grid(i, ColCopy).ToString = MaskFormatDateShort OrElse c1Grid(i, ColCopy).ToString = MaskFormatDate OrElse (L3IsNumeric(c1Grid(i, ColCopy).ToString) And Val(c1Grid(i, ColCopy).ToString) = 0) Then
                            c1Grid(i, ColCopy) = sValue
                            While j < ColumnCount
                                c1Grid(i, ColCopy + j) = c1Grid(RowCopy, ColCopy + j)
                                j += 1
                            End While
                        End If
                    Next
                ElseIf Flag = Windows.Forms.DialogResult.Yes Then ' Copy hết
                    For i = RowCopy + 1 To c1Grid.RowCount - 1
                        j = 1
                        c1Grid(i, ColCopy) = sValue
                        While j < ColumnCount
                            c1Grid(i, ColCopy + j) = c1Grid(RowCopy, ColCopy + j)
                            j += 1
                        End While
                    Next
                    'c1Grid(0, ColCopy) = sValue
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Copy giá trị trong 1 cột (khi nhấn Head_Click và nhấn Ctrl + S)
    ''' </summary>
    ''' <param name="c1Grid">Lưới</param>
    ''' <param name="ColCopy">Cột cần copy</param>
    ''' <param name="COL_Choose">Cột kiểm tra được chọn thì copy</param>
    ''' <param name="iColRelative">Số cột liên quan khi cần copy</param>
    ''' <remarks>Luôn gán AllowSort = False</remarks>
    Public Sub CopyColumnsArr(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal ColCopy As Integer, Optional ByVal COL_Choose As Integer = -1, Optional ByVal iColRelative() As Integer = Nothing)
        Try
            If c1Grid.RowCount < 2 OrElse c1Grid.Splits(c1Grid.SplitIndex).DisplayColumns(ColCopy).Locked Then Exit Sub
            c1Grid.AllowSort = False
            c1Grid.UpdateData()

            Dim RowCopy As Integer = c1Grid.Row
            Dim sValue As String = c1Grid(RowCopy, ColCopy).ToString

            Dim Flag As DialogResult
            Flag = MessageBox.Show(rl3("Copy_cot_du_lieu_cho") & vbCrLf & rl3("____-_Tat_ca_cac_cot_(nhan_Yes)") & vbCrLf & rl3("____-_Nhung_dong_con_trong_(nhan_No)"), MsgAnnouncement, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If Flag = Windows.Forms.DialogResult.No Then ' Copy nhung dong con trong

                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    If c1Grid(i, ColCopy).ToString = "" OrElse c1Grid(i, ColCopy).ToString = MaskFormatDateShort OrElse c1Grid(i, ColCopy).ToString = MaskFormatDate OrElse (L3IsNumeric(c1Grid(i, ColCopy).ToString) And Val(c1Grid(i, ColCopy).ToString) = 0) Then
                        If COL_Choose = -1 OrElse L3Bool(c1Grid(i, COL_Choose)) Then 'Không cần kiểm tra điều kiện
                            c1Grid(i, ColCopy) = sValue
                            If iColRelative Is Nothing Then Continue For
                            For j As Integer = 0 To iColRelative.Length - 1
                                c1Grid(i, iColRelative(j)) = c1Grid(RowCopy, iColRelative(j))
                            Next j

                        End If
                    End If
                Next
            ElseIf Flag = Windows.Forms.DialogResult.Yes Then ' Copy het
                For i As Integer = RowCopy + 1 To c1Grid.RowCount - 1
                    If COL_Choose = -1 OrElse L3Bool(c1Grid(i, COL_Choose)) Then 'Không cần kiểm tra điều kiện
                        c1Grid(i, ColCopy) = sValue
                        If iColRelative Is Nothing Then Continue For
                        For j As Integer = 0 To iColRelative.Length - 1
                            c1Grid(i, iColRelative(j)) = c1Grid(RowCopy, iColRelative(j))
                        Next j
                    End If
                Next
            Else
                Exit Sub
            End If
        Catch ex As Exception
            D99C0008.MsgL3(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' HeadClick trên cột Chọn kiểu String
    ''' </summary>
    ''' <param name="tdbg">Lưới</param>
    ''' <param name="COL_Choose">cột chọn kiểu string</param>
    ''' <param name="bSelected"> bSelected tự định nghĩa trong từng form; Giá trị mặc định phụ thuộc khi load lưới. Thông thường là False</param>
    ''' <remarks>Trả về bSelected và Luôn gán AllowSort = False</remarks>
    Public Sub L3HeadClick(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Choose As String, ByRef bSelected As Boolean)
        L3HeadClick(tdbg, COL_Choose, bSelected, "", "")
    End Sub


    ''' <summary>
    ''' HeadClick trên cột Chọn kiểu Integer
    ''' </summary>
    ''' <param name="tdbg">Lưới</param>
    ''' <param name="COL_Choose">cột chọn kiểu Integer</param>
    ''' <param name="bSelected"> bSelected tự định nghĩa trong từng form; Giá trị mặc định phụ thuộc khi load lưới. Thông thường là False</param>
    ''' <remarks>Trả về bSelected</remarks>
    Public Sub L3HeadClick(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Choose As Integer, ByRef bSelected As Boolean)
        L3HeadClick(tdbg, tdbg.Columns(COL_Choose).DataField, bSelected)
    End Sub

    ''' <summary>
    ''' HeadClick trên cột Chọn kiểu String  có kiểm tra điều kiện để chọn
    ''' </summary>
    ''' <param name="tdbg">Lưới</param>
    ''' <param name="COL_Choose">cột chọn kiểu string</param>
    ''' <param name="bSelected"> bSelected tự định nghĩa trong từng form; Giá trị mặc định phụ thuộc khi load lưới. Thông thường là False</param>
    ''' <param name="Col_Where">cột để kiểm tra điều kiện</param>
    ''' <param name="sValueWhere">Giá trị của cột điều kiện</param>
    ''' <remarks>Trả về bSelected và Luôn gán AllowSort = False</remarks>
    Public Sub L3HeadClick(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Choose As String, ByRef bSelected As Boolean, ByVal Col_Where As String, ByVal sValueWhere As String)
        'Update 18/6/2013 by Minh Hòa
        Dim bSelect As Boolean = Not bSelected
        tdbg.AllowSort = False
        tdbg.UpdateData()

        Dim i As Integer = tdbg.RowCount - 1

        If Col_Where <> "" Then
            While i >= 0
                If tdbg(i, Col_Where).ToString = sValueWhere Then
                    tdbg(i, COL_Choose) = bSelect
                End If
                i -= 1
            End While
        Else
            While i >= 0
                tdbg(i, COL_Choose) = bSelect
                i -= 1
            End While
        End If
        bSelected = bSelect
    End Sub


    ''' <summary>
    ''' HeadClick trên cột Chọn kiểu Integer có kiểm tra điều kiện để chọn
    ''' </summary>
    ''' <param name="tdbg">Lưới</param>
    ''' <param name="COL_Choose">cột chọn kiểu Integer</param>
    ''' <param name="bSelected"> bSelected tự định nghĩa trong từng form; Giá trị mặc định phụ thuộc khi load lưới. Thông thường là False</param>
    ''' <remarks>Trả về bSelected</remarks>
    Public Sub L3HeadClick(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Choose As Integer, ByRef bSelected As Boolean, ByVal Col_Where As Integer, ByVal sValueWhere As String)
        'Update 18/6/2013 by Minh Hòa
        L3HeadClick(tdbg, tdbg.Columns(COL_Choose).DataField, bSelected, tdbg.Columns(Col_Where).DataField, sValueWhere)
    End Sub
#End Region

#Region "Set thuộc tính cho lưới"

    ''' <summary>
    ''' Tính lại STT trên lưới
    ''' </summary>
    ''' <param name="c1Grid">lưới cần</param>
    ''' <param name="COL_OrderNum">tên cột thứ tự</param>
    ''' <param name="col_Value">tên cột đang đứng để tăng số thứ tự</param>
    ''' <param name="bUseFilterBar"> = True khi lưới có Filter Bar</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub UpdateTDBGOrderNum(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_OrderNum As Integer, Optional ByVal col_Value As Integer = -1, Optional ByVal bUseFilterBar As Boolean = False)
        Try
            'Update cho trường hợp AfterDelete
            If col_Value = -1 Then
                If bUseFilterBar Then 'Dùng cho lưới có Filter Bar
                    For i As Integer = 0 To c1Grid.RowCount - 1
                        c1Grid(i, COL_OrderNum) = i + 1
                    Next
                Else
                    Dim bFocus As Boolean = c1Grid.Focused
                    c1Grid.Enabled = False
                    For i As Integer = 0 To c1Grid.RowCount - 1
                        c1Grid(i, COL_OrderNum) = i + 1
                    Next
                    c1Grid.Enabled = True
                    If bFocus Then c1Grid.Focus()
                End If
            Else 'Update cho trường hợp AfterColUpdate
                If c1Grid.Columns(col_Value).Text.ToString <> "" Then
                    c1Grid.Columns(COL_OrderNum).Text = CStr(c1Grid.Row + 1)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Reset thanh kẻ Split của lưới C1 về 0
    ''' </summary>
    ''' <param name="c1Grid">Lưới C1</param>
    <DebuggerStepThrough()> _
    Public Sub ResetSplitDividerSize(ByVal ParamArray c1Grid() As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        For index As Integer = 0 To c1Grid.Length - 1
            c1Grid(index).SplitDividerSize = New Size(1, 1)
            For i As Int32 = 0 To c1Grid(index).Splits.Count - 1
                c1Grid(index).Splits(i).BorderStyle = Border3DStyle.Flat
            Next
        Next
    End Sub

    ''' <summary>
    ''' Set màu cho và Footer cho lưới Danh mục
    ''' </summary>
    ''' <param name="c1Grid">Lưới C1</param>
    ''' <param name="FisrtSplits">Split đầu tiên của lưới</param>
    ''' <param name="LastSplits">Split cuối cùng của lưới</param>
    ''' <remarks>Chỉ dùng cho các lưới dạng Danh mục</remarks>
    <DebuggerStepThrough()> _
    Public Sub ResetColorGrid(ByRef c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, Optional ByVal FisrtSplits As Integer = 0, Optional ByVal LastSplits As Integer = 0)
        If c1Grid.FilterBar Then
            c1Grid.FilterBarStyle.BackColor = Color.Moccasin
            c1Grid.FilterBarStyle.GradientMode = C1.Win.C1TrueDBGrid.GradientModeEnum.Vertical
        End If

        For i As Integer = FisrtSplits To LastSplits
            With c1Grid.Splits(i)
                If c1Grid.FilterBar Then .FilterBorderStyle = Border3DStyle.Raised
                .HighLightRowStyle.BackColor = Color.Green
                .SelectedStyle.BackColor = SystemColors.Highlight
                .MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRowRaiseCell

                If c1Grid.ColumnFooters Then
                    .FooterStyle.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
                    .FooterStyle.ForeColor = Color.Blue
                    .FooterStyle.Font = New System.Drawing.Font("Lemon3", 8.249999!)
                End If
            End With
        Next
    End Sub

    Public Sub ResetColorGrid(ByVal ParamArray c1Grid() As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        For i As Integer = 0 To c1Grid.Length - 1
            ResetColorGrid(c1Grid(i), 0, c1Grid(i).Splits.Count - 1)
        Next
    End Sub

    ''' <summary>
    ''' Set Footer cho lưới
    ''' </summary>
    ''' <param name="c1Grid">Lưới C1</param>
    ''' <param name="FisrtSplits">Split đầu tiên của lưới</param>
    ''' <param name="LastSplits">Split cuối cùng của lưới</param>
    ''' <remarks>Chủ yếu dùng cho các lưới dạng Nhập liệu</remarks>
    <DebuggerStepThrough()> _
    Public Sub ResetFooterGrid(ByRef c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, Optional ByVal FisrtSplits As Integer = 0, Optional ByVal LastSplits As Integer = 0)
        If c1Grid.FilterBar Then
            c1Grid.FilterBarStyle.BackColor = Color.Moccasin
            c1Grid.FilterBarStyle.GradientMode = C1.Win.C1TrueDBGrid.GradientModeEnum.Vertical
        End If

        For i As Integer = FisrtSplits To LastSplits
            With c1Grid.Splits(i)
                If c1Grid.FilterBar Then .FilterBorderStyle = Border3DStyle.Raised
                If c1Grid.ColumnFooters Then
                    .FooterStyle.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
                    .FooterStyle.ForeColor = Color.Blue
                    .FooterStyle.Font = New System.Drawing.Font("Lemon3", 8.249999!)
                End If
            End With
        Next
    End Sub

    Public Sub ResetFooterGrid(ByVal ParamArray c1Grid() As C1.Win.C1TrueDBGrid.C1TrueDBGrid)
        For i As Integer = 0 To c1Grid.Length - 1
            ResetFooterGrid(c1Grid(i), 0, c1Grid(i).Splits.Count - 1)
        Next
    End Sub

#End Region

#Region "Lấy chuỗi người dùng để sinh IGE"
    ''' <summary>
    ''' Lấy chuỗi người dùng để sinh IGE
    ''' </summary>
    <DebuggerStepThrough()> _
    Public Sub LoadUserKey()
        Dim sSQL As String = ""
        sSQL = SQLStoreD91P9110()
        Dim dt As New DataTable
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then gsStringKey = dt.Rows(0).Item("StringKey").ToString
        dt.Dispose()

        'Sinh IGE theo kiểu mới 05/11/2009: biến gsStringKey (lấy từ store D91P9110) không còn ý nghĩa nữa
    End Sub
#End Region


#Region "Tạo IGE cho số phiếu tự động, không lấy trong DLL"

#Region "Sinh số phiếu tự động theo kiểu mới, gọi store D91P9111, nếu trùng phiếu thì tự động tăng (Chỉ gọi lúc lưu)"


    ''' <summary>
    ''' Insert dữ liệu vào bảng D91T9111 cho trường hợp số phiếu KHÔNG sinh tự động
    ''' </summary>
    ''' <param name="sVoucherNo">Số phiếu hiện tại</param>
    ''' <param name="sVoucherTableName">Bảng nghiệp vụ chứa số phiếu</param>
    ''' <param name="sVoucherIGE">Giá trị khóa chính của bảng nghiệp vụ</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub InsertVoucherNoD91T9111(ByVal sVoucherNo As String, ByVal sVoucherTableName As String, ByVal sVoucherIGE As String)
        Dim sSQL As String
        sSQL = "INSERT INTO D91T9111 (VoucherNo, VoucherIGE, VoucherTableName)" & vbCrLf
        sSQL &= "SELECT " & SQLString(sVoucherNo) & COMMA
        sSQL &= SQLString(sVoucherIGE) & COMMA
        sSQL &= SQLString(sVoucherTableName) & vbCrLf
        sSQL &= "WHERE NOT EXISTS (" & vbCrLf
        sSQL &= "SELECT TOP 1 1 "
        sSQL &= "FROM D91T9111 WITH(NOLOCK) "
        sSQL &= "WHERE	VoucherNo = " & SQLString(sVoucherNo)
        sSQL &= ")"

        ExecuteSQLNoTransaction(sSQL)
    End Sub

    ''' <summary>
    ''' Xóa số phiếu của bảng D91T9111, Gọi tại form Truy vấn sau khi xóa thành công (Xóa Phiếu)
    ''' </summary>
    ''' <param name="sVoucherNo">Số phiếu khi xóa</param>
    ''' <param name="sVoucherTableName">Bảng nghiệp vụ chứa số phiếu</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub DeleteVoucherNoD91T9111(ByVal sVoucherNo As String, ByVal sVoucherTableName As String)
        Dim sSQL As String = "--- Xoa so phieu bang D91T9111" & vbCrLf
        sSQL &= "Exec D91P9113 "
        sSQL &= SQLString(sVoucherTableName) & COMMA 'VoucherTableName, varchar[50], NOT NULL
        sSQL &= SQLString(sVoucherNo) 'VoucherNo, varchar[20], NOT NULL
        ExecuteSQLNoTransaction(sSQL)

    End Sub

    ''' <summary>
    ''' Xóa số phiếu của bảng D91T9111, Gọi tại form Truy vấn sau khi xóa thành công (Xóa Hóa đơn)
    ''' </summary>
    ''' <param name="sVoucherNo">Số phiếu khi xóa</param>
    ''' <param name="sVoucherTableName">Bảng nghiệp vụ chứa số phiếu</param>
    ''' <param name="FieldNameVoucherNo"> FieldName của Số phiếu (VD: VoucherNo)</param>
    ''' <remarks>Áp dụng cho TH 1 phiếu có nhiều hóa đơn</remarks>
    <DebuggerStepThrough()> _
    Public Sub DeleteVoucherNoD91T9111(ByVal sVoucherNo As String, ByVal sVoucherTableName As String, ByVal FieldNameVoucherNo As String)
        Dim sSQL As String = "--- Xoa so phieu bang D91T9111" & vbCrLf
        sSQL &= "IF NOT EXISTS (SELECT TOP 1 1 FROM " & sVoucherTableName & " WITH(NOLOCK) WHERE " & FieldNameVoucherNo & " = " & SQLString(sVoucherNo) & ")" & vbCrLf
        sSQL &= "Exec D91P9113 "
        sSQL &= SQLString(sVoucherTableName) & COMMA 'VoucherTableName, varchar[50], NOT NULL
        sSQL &= SQLString(sVoucherNo) 'VoucherNo, varchar[20], NOT NULL
        ExecuteSQLNoTransaction(sSQL)
    End Sub


    ''' <summary>
    ''' Sinh số phiếu theo kiểu mới
    ''' </summary>
    ''' <param name="c1Combo">Combo số phiếu</param>
    ''' <param name="sVoucherTableName">Bảng nghiệp vụ chứa số phiếu</param>
    ''' <param name="sVoucherIGE">Giá trị khóa chính của bảng nghiệp vụ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateIGEVoucherNoNew(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal sVoucherTableName As String, ByVal sVoucherIGE As String) As String
        Return CreateIGEVoucherNoNew(sVoucherTableName, sVoucherIGE, L3Int(c1Combo.Columns("S1Type").Text), c1Combo.Columns("S1").Text, L3Int(c1Combo.Columns("S2Type").Text), c1Combo.Columns("S2").Text, L3Int(c1Combo.Columns("S3Type").Text), c1Combo.Columns("S3").Text, L3Int(c1Combo.Columns("OutputOrder").Text), L3Int(c1Combo.Columns("OutputLength").Text), c1Combo.Columns("Separator").Text)

        ''*********************************
        ''Kiểm tra IGE của khóa chính phải có
        'If sVoucherIGE = "" Then
        '    D99C0008.MsgL3("Lỗi: Khóa chính của nghiệp vụ này chưa được tạo của table " & sVoucherTableName & "." & vbCrLf & "Kết thúc chương trình.", L3MessageBoxIcon.Err)
        '    WriteLogFile("Loi sinh so phieu cua table " & sVoucherTableName, "LogCreateIGEVoucherNoNew.log")
        '    End
        'End If
        ''*********************************
        'Dim strS1 As String = ""
        'Dim strS2 As String = ""
        'Dim strS3 As String = ""
        'If c1Combo.Columns("S1Type").Text <> "0" Then
        '    strS1 = FindSxType(c1Combo.Columns("S1Type").Text, c1Combo.Columns("S1").Text.Trim)
        'End If
        'If c1Combo.Columns("S2Type").Text <> "0" Then
        '    strS2 = FindSxType(c1Combo.Columns("S2Type").Text, c1Combo.Columns("S2").Text.Trim)
        'End If
        'If c1Combo.Columns("S3Type").Text <> "0" Then
        '    strS3 = FindSxType(c1Combo.Columns("S3Type").Text, c1Combo.Columns("S3").Text.Trim)
        'End If

        'Dim sSQL As String = SQLStoreD91P9111(sVoucherIGE, sVoucherTableName, strS1.Trim, strS2.Trim, strS3.Trim, L3Int(c1Combo.Columns("OutputLength").Text), L3Int(c1Combo.Columns("OutputOrder").Text), c1Combo.Columns("Separator").Text.Trim)
        'Dim dt As DataTable
        'dt = ReturnDataTable(sSQL)
        'If dt.Rows.Count > 0 Then
        '    Return dt.Rows(0).Item("VoucherNo").ToString
        'Else
        '    D99C0008.MsgL3("Lỗi: Sinh số phiếu tự động của table " & sVoucherTableName & "." & vbCrLf & "Kết thúc chương trình.", L3MessageBoxIcon.Err)
        '    WriteLogFile("Loi sinh so phieu cua table " & sVoucherTableName & vbCrLf & sSQL, "LogCreateIGEVoucherNoNew.log")
        '    End
        'End If
        ''**************************************

    End Function

    ''' <summary>
    ''' Sinh số phiếu theo kiểu mới
    ''' </summary>
    ''' <param name="sVoucherTableName">Bảng nghiệp vụ chứa số phiếu</param>
    ''' <param name="sVoucherIGE">Giá trị khóa chính của bảng nghiệp vụ</param>
    ''' <param name="S1Type"></param>
    ''' <param name="s1"></param>
    ''' <param name="S2Type"></param>
    ''' <param name="S2"></param>
    ''' <param name="S3Type"></param>
    ''' <param name="S3"></param>
    ''' <param name="OutputOrder"></param>
    ''' <param name="OutputLength"></param>
    ''' <param name="Seperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateIGEVoucherNoNew(ByVal sVoucherTableName As String, ByVal sVoucherIGE As String, _
                ByVal S1Type As Integer, ByVal s1 As String, ByVal S2Type As Integer, ByVal S2 As String, ByVal S3Type As Integer, ByVal S3 As String, _
                ByVal OutputOrder As Integer, ByVal OutputLength As Integer, ByVal Seperator As String) As String

        '*********************************
        'Kiểm tra IGE của khóa chính phải có
        If sVoucherIGE = "" Then
            D99C0008.MsgL3("Do vấn đề về kỹ thuật (Khóa chính chưa được tạo) nên việc tạo phiếu bị lỗi." & vbCrLf & "Chương trình kết thúc.", L3MessageBoxIcon.Err)
            WriteLogFile("Loi Sinh so phieu (tao phieu) cua table TableName " & sVoucherTableName, "LogCreateIGEVoucherNoNew.log")
            End
        End If
        '*********************************

        Dim strS1 As String = ""
        Dim strS2 As String = ""
        Dim strS3 As String = ""

        If Not IsDBNull(S1Type) AndAlso S1Type <> 0 Then strS1 = FindSxType(S1Type.ToString, s1.Trim)
        If Not IsDBNull(S2Type) AndAlso S2Type <> 0 Then strS2 = FindSxType(S2Type.ToString, S2.Trim)
        If Not IsDBNull(S3Type) AndAlso S3Type <> 0 Then strS3 = FindSxType(S3Type.ToString, S3.Trim)

        Dim sSQL As String = SQLStoreD91P9111(sVoucherIGE, sVoucherTableName, strS1.Trim, strS2.Trim, strS3.Trim, OutputLength, OutputOrder, Seperator.Trim)
        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("VoucherNo").ToString
        Else
            D99C0008.MsgL3("Do vấn đề về kỹ thuật (Khóa chính chưa được tạo) nên việc tạo phiếu bị lỗi." & vbCrLf & "Chương trình kết thúc.", L3MessageBoxIcon.Err)
            WriteLogFile("Loi Sinh so phieu (tao phieu) cua store D91P9111 " & sSQL, "LogCreateIGEVoucherNoNew.log")

            End
        End If

    End Function


    ''' <summary>
    ''' Kiểm tra trùng số phiếu theo kiểu mới
    ''' </summary>
    ''' <param name="ModuleID">Tên module: Dxx</param>
    ''' <param name="TableName">Tên bảng để lưu Số phiếu</param>
    ''' <param name="VoucherID">Giá trị của Khóa chính trong bảng lưu Số phiếu</param>
    ''' <param name="VoucherNo">Số phiếu</param>
    ''' <returns>True: vi phạm, không làm gì cả</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CheckDuplicateVoucherNoNew(ByVal ModuleID As String, ByVal TableName As String, ByVal VoucherID As String, ByVal VoucherNo As String) As Boolean
        '*********************************
        'Kiểm tra IGE của khóa chính phải có
        If VoucherID = "" Then
            '            D99C0008.MsgL3("Khóa chính của nghiệp vụ này chưa được tạo", L3MessageBoxIcon.Exclamation)
            '            Return True
            D99C0008.MsgL3("Do vấn đề về kỹ thuật (Khóa chính chưa được tạo) nên việc kiểm tra trùng phiếu bị lỗi." & vbCrLf & "Chương trình kết thúc.", L3MessageBoxIcon.Err)
            WriteLogFile("Loi Sinh so phieu (kiem tra trung phieu) cua table TableName " & TableName, "LogCreateIGEVoucherNoNew.log")
            End

        End If
        '*********************************
        Dim sSQL As String = ""
        sSQL = SQLStoreD91P9114(ModuleID.Substring(1, 2), TableName, VoucherID, VoucherNo)
        Dim dt As New DataTable
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Select Case CInt(dt.Rows(0).Item("Status"))
                Case 1
                    'MessageBox.Show(dt.Rows(0).Item("Message").ToString, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    D99C0008.MsgL3(ConvertVietwareFToUnicode(dt.Rows(0).Item("Message").ToString), L3MessageBoxIcon.Exclamation)
                    dt = Nothing
                    Return True
            End Select
        End If
        dt = Nothing
        Return False
    End Function

#End Region

#Region "Sinh số phiếu cho Master"

    ''' <summary>
    ''' Tạo IGE cho số phiếu theo chuẩn của C1Combo VoucherType
    ''' </summary>
    ''' <param name="c1Combo">C1Combo theo chuẩn</param>
    ''' <param name="FlagSave">Lưu lại số phiếu không?</param>
    ''' <returns>Số phiếu cần tạo</returns>
    ''' <remarks>Chú ý C1Combo chuẩn được thiết kế giống module D28</remarks>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNo(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal FlagSave As Boolean) As String
        Return CreateIGEVoucherNo(c1Combo, FlagSave, "D91T0001")
    End Function

    ''' <summary>
    ''' Tạo IGE cho số phiếu theo chuẩn của C1Combo VoucherType
    ''' </summary>
    ''' <param name="c1Combo">C1Combo theo chuẩn</param>
    ''' <param name="FlagSave">Lưu lại số phiếu không?</param>
    ''' <param name="TableName">Table cần lưu trữ</param>
    ''' <returns>Số phiếu cần tạo</returns>
    ''' <remarks>Chú ý C1Combo chuẩn được thiết kế giống module D28</remarks>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNo(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal FlagSave As Boolean, ByVal TableName As String) As String

        Dim strS1 As String = ""
        Dim strS2 As String = ""
        Dim strS3 As String = ""
        If c1Combo.Columns("S1Type").Text <> "0" Then
            strS1 = FindSxType(c1Combo.Columns("S1Type").Text, c1Combo.Columns("S1").Text.Trim)
        End If
        If c1Combo.Columns("S2Type").Text <> "0" Then
            strS2 = FindSxType(c1Combo.Columns("S2Type").Text, c1Combo.Columns("S2").Text.Trim)
        End If
        If c1Combo.Columns("S3Type").Text <> "0" Then
            strS3 = FindSxType(c1Combo.Columns("S3Type").Text, c1Combo.Columns("S3").Text.Trim)
        End If

        Return IGEVoucherNo(FlagSave, gnNewLastKey, strS1.Trim, strS2.Trim, strS3.Trim, CType(c1Combo.Columns("OutputOrder").Text, D99D0041.OutOrderEnum), Convert.ToInt16(c1Combo.Columns("OutputLength").Text), c1Combo.Columns("Separator").Text.Trim, TableName)


    End Function

    ''' <summary>
    ''' Tạo IGE cho số phiếu theo chuẩn của C1Combo VoucherType
    ''' </summary>
    ''' <param name="S1Type">Truyền S1Type theo chuẩn</param>
    ''' <param name="S1">Truyền chuỗi S1</param>
    ''' <param name="S2Type">Truyền S2Type theo chuẩn</param>
    ''' <param name="S2">Truyền chuỗi S2</param>
    ''' <param name="S3Type">Truyền S3Type theo chuẩn</param>
    ''' <param name="S3">Truyền chuỗi S3</param>
    ''' <param name="OutputOrder">Thứ tự hiện thị</param>
    ''' <param name="OutputLength">Chiều dài</param>
    ''' <param name="Separator">Dấu phân cách</param>
    ''' <param name="FlagSave">Có lưu key không ?</param>
    ''' <returns>Trả về IGE VoucherNo theo chuẩn Lemon3</returns>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNo(ByVal S1Type As String, ByVal S1 As String, ByVal S2Type As String, ByVal S2 As String, ByVal S3Type As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String, ByVal FlagSave As Boolean) As String
        Return CreateIGEVoucherNo(S1Type, S1.Trim, S2Type, S2.Trim, S3Type, S3.Trim, OutputOrder, OutputLength, Separator.Trim, FlagSave, "D91T0001")
    End Function

    ''' <summary>
    ''' Tạo IGE cho số phiếu theo chuẩn của C1Combo VoucherType
    ''' </summary>
    ''' <param name="S1Type">Truyền S1Type theo chuẩn</param>
    ''' <param name="S1">Truyền chuỗi S1</param>
    ''' <param name="S2Type">Truyền S2Type theo chuẩn</param>
    ''' <param name="S2">Truyền chuỗi S2</param>
    ''' <param name="S3Type">Truyền S3Type theo chuẩn</param>
    ''' <param name="S3">Truyền chuỗi S3</param>
    ''' <param name="OutputOrder">Thứ tự hiện thị</param>
    ''' <param name="OutputLength">Chiều dài</param>
    ''' <param name="Separator">Dấu phân cách</param>
    ''' <param name="FlagSave">Có lưu key không ?</param>
    ''' <param name="TableName">Tên table lưu xuống</param>
    ''' <returns>Trả về IGE VoucherNo theo chuẩn Lemon3</returns>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNo(ByVal S1Type As String, ByVal S1 As String, ByVal S2Type As String, ByVal S2 As String, ByVal S3Type As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String, ByVal FlagSave As Boolean, ByVal TableName As String) As String

        Dim strS1 As String = ""
        Dim strS2 As String = ""
        Dim strS3 As String = ""
        If S1Type <> "0" Then
            strS1 = FindSxType(S1Type, S1)
        End If
        If S2Type <> "0" Then
            strS2 = FindSxType(S2Type, S2)
        End If
        If S3Type <> "0" Then
            strS3 = FindSxType(S3Type, S3)
        End If

        Return IGEVoucherNo(FlagSave, gnNewLastKey, strS1.Trim, strS2.Trim, strS3.Trim, OutputOrder, OutputLength, Separator.Trim, TableName)

    End Function

    ''' <summary>
    ''' Tạo IGE cho số phiếu theo chuẩn của C1Combo VoucherType
    ''' </summary>
    ''' <param name="S1">Truyền chuỗi S1</param>
    ''' <param name="S2">Truyền chuỗi S2</param>
    ''' <param name="S3">Truyền chuỗi S3</param>
    ''' <param name="OutputOrder">Thứ tự hiện thị</param>
    ''' <param name="OutputLength">Chiều dài</param>
    ''' <param name="Separator">Dấu phân cách</param>
    ''' <param name="FlagSave">Có lưu key không ?</param>
    ''' <param name="TableName">Tên table lưu xuống</param>
    ''' <returns>Trả về IGE VoucherNo theo chuẩn Lemon3</returns>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNo(ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String, ByVal FlagSave As Boolean, ByVal TableName As String) As String
        Return IGEVoucherNo(FlagSave, gnNewLastKey, S1.Trim, S2.Trim, S3.Trim, OutputOrder, OutputLength, Separator.Trim, TableName)

    End Function

    ''' <summary>
    ''' Tạo IGE cho số phiếu theo chuẩn của C1Combo VoucherType
    ''' </summary>
    ''' <param name="S1">Truyền chuỗi S1</param>
    ''' <param name="S2">Truyền chuỗi S2</param>
    ''' <param name="S3">Truyền chuỗi S3</param>
    ''' <param name="OutputOrder">Thứ tự hiện thị</param>
    ''' <param name="OutputLength">Chiều dài</param>
    ''' <param name="Separator">Dấu phân cách</param>
    ''' <param name="FlagSave">Có lưu key không ?</param>
    ''' <returns>Trả về IGE VoucherNo theo chuẩn Lemon3</returns>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNo(ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String, ByVal FlagSave As Boolean) As String
        Return CreateIGEVoucherNo(S1.Trim, S2.Trim, S3.Trim, OutputOrder, OutputLength, Separator, FlagSave, "D91T0001")
    End Function


    Private Function IGEVoucherNo(ByVal bFlagSave As Boolean, _
                                                    ByVal nNewLastKey As Long, _
                                                    ByVal sStringKey1 As String, _
                                                    ByVal sStringKey2 As String, _
                                                    ByVal sStringKey3 As String, _
                                                    ByVal nOutputOrder As OutOrderEnum, _
                                                    ByVal nOutputLength As Integer, _
                                                    ByVal sSeperatorCharacter As String, _
                                                    Optional ByVal sTableName As String = "D91T0001") As String
        Try
            Dim sIGEVoucherNo As String = ""
            Dim KeyString As String = ""
            Dim LastKey As Long = 0

            KeyString = sStringKey1 & sStringKey2 & sStringKey3

            'Get LastKey from table TableName
            If nNewLastKey <> 0 Then
                LastKey = CLng(nNewLastKey)
            Else
                LastKey = GetLastKey(KeyString, sTableName)
            End If

            'Kiem tra chieu dai và lấy chuỗi string của Lastkey
            Dim LastKeyString As String
            LastKeyString = CheckLengthKey(LastKey, sStringKey1, sStringKey2, sStringKey3, sSeperatorCharacter, nOutputLength)
            'Update 21/12/2012: Nếu chiều dài không hợp lệ thì thoát
            If LastKeyString = "" Then
                Return ""
            Else
                sIGEVoucherNo = Generate(sStringKey1, sStringKey2, sStringKey3, nOutputOrder, sSeperatorCharacter, LastKeyString)
            End If

            If sIGEVoucherNo = "" Then
                D99C0008.MsgL3("Lỗi sinh số phiếu tự động (" & sTableName & ")", L3MessageBoxIcon.Err)
                WriteLogFile("Loi sinh so phieu cua table " & sTableName, "LogIGEVoucherNo.log")
                If bFlagSave Then
                    End
                Else
                    Return ""
                End If
            End If

            If bFlagSave Then SaveLastKey(sTableName, KeyString, LastKey)

            Return sIGEVoucherNo

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Announcement", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            WriteLogFile("Loi sinh so phieu (end) cua table " & sTableName & "(" & Now.Year & Now.Month.ToString("00") & Now.Date.ToString("00") & ")", "LogIGEVoucherNo.log")
            If bFlagSave Then
                End
            Else
                Return ""
            End If
        End Try

    End Function
#End Region

#Region "Sinh số phiếu cho Detail"

    ''' <summary>
    ''' KHÔNG có textbox Số phiếu: Sinh số phiếu tự động trên lưới khi lưu, Số phiếu sinh = Số dòng trên lưới và có kiểm tra trùng phiếu
    ''' (sinh theo table D91T0001)
    ''' </summary>
    ''' <param name="c1Combo"></param>
    ''' <param name="OldVoucherNo"></param>
    ''' <param name="NumberIGE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNosDetail(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal OldVoucherNo As String, ByVal NumberIGE As Integer, ByVal TableKey As String, ByVal FieldIDKey As String) As String
        Return CreateIGEVoucherNosDetail(c1Combo, OldVoucherNo, NumberIGE, TableKey, FieldIDKey, "", "D91T0001")
    End Function


    ''' <summary>
    ''' CÓ textbox Số phiếu: Sinh số phiếu tự động trên lưới khi lưu, Số phiếu sinh = Số dòng trên lưới và có kiểm tra trùng phiếu
    ''' (sinh theo table D91T0001)
    ''' </summary>
    ''' <param name="c1Combo"></param>
    ''' <param name="OldVoucherNo"></param>
    ''' <param name="NumberIGE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNosDetail(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal OldVoucherNo As String, ByVal NumberIGE As Integer, ByVal TableKey As String, ByVal FieldIDKey As String, ByVal sVoucherNo As String) As String
        Return CreateIGEVoucherNosDetail(c1Combo, OldVoucherNo, NumberIGE, TableKey, FieldIDKey, sVoucherNo, "D91T0001")
    End Function

    ''' <summary>
    ''' CÓ textbox Số phiếu: Sinh số phiếu tự động trên lưới khi lưu, Số phiếu sinh = Số dòng trên lưới và có kiểm tra trùng phiếu
    ''' (sinh theo table bất kỳ truyền vào)
    ''' </summary>
    ''' <param name="c1Combo">Combo Loại phiếu load theo chuẩn</param>
    ''' <param name="OldVoucherNo">Số phiếu cũ</param>
    ''' <param name="NumberIGE">Số dòng cần sinh Số phiếu</param>
    ''' <param name="TableKey">Table nghiệp vụ chứa số phiếu</param>
    ''' <param name="FieldIDKey">Khóa chính của bảng nghiệp vụ, để kiểm tra trùng phiếu</param>
    ''' <param name="TableVoucherNo">Table sinh số phiếu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CreateIGEVoucherNosDetail(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal OldVoucherNo As String, ByVal NumberIGE As Integer, ByVal TableKey As String, ByVal FieldIDKey As String, ByVal sVoucherNo As String, ByVal TableVoucherNo As String) As String
        '***********************************************
        Dim sIGEVoucherNo As String = ""

        Dim sS1 As String = ""
        Dim sS2 As String = ""
        Dim sS3 As String = ""
        Dim sSeperator As String = ""
        Dim iOutputLength As Integer = 0
        Dim iOutputOrder As D99D0041.OutOrderEnum

        Try
            iOutputLength = Convert.ToInt32(c1Combo.Columns("OutputLength").Text)
            iOutputOrder = CType(c1Combo.Columns("OutputOrder").Text, D99D0041.OutOrderEnum)
            sSeperator = c1Combo.Columns("Separator").Text

            If c1Combo.Columns("S1Type").Text <> "0" Then
                sS1 = FindSxType(c1Combo.Columns("S1Type").Text, c1Combo.Columns("S1").Text.Trim)
            End If
            If c1Combo.Columns("S2Type").Text <> "0" Then
                sS2 = FindSxType(c1Combo.Columns("S2Type").Text, c1Combo.Columns("S2").Text.Trim)
            End If
            If c1Combo.Columns("S3Type").Text <> "0" Then
                sS3 = FindSxType(c1Combo.Columns("S3Type").Text, c1Combo.Columns("S3").Text.Trim)
            End If
            sS1 = sS1.Trim
            sS2 = sS2.Trim
            sS3 = sS3.Trim

            Dim iLastKey As Long
            Dim sStringLastKey As String

            If OldVoucherNo = "" Then
                Dim sKeyString As String
                Dim bKey As Boolean = False

                Do
                    '**********************************************
                    'Giống hàm IGEVoucherNo
                    sKeyString = sS1 & sS2 & sS3
                    'Lấy LastKey
                    iLastKey = GetLastKey(sKeyString, TableVoucherNo)
                    '--------------------------------------------------
                    If sVoucherNo = "" Then ' Không textbox SỐ phiếu
                        'Kiểm tra chiều dài và lấy chuỗi string của Lastkey
                        sStringLastKey = CheckLengthKey(iLastKey, sS1, sS2, sS3, sSeperator, iOutputLength)
                        If sStringLastKey <> "" Then
                            'Chiều dài hợp lệ thì sinh IGE
                            sIGEVoucherNo = Generate(sS1, sS2, sS3, iOutputOrder, sSeperator, sStringLastKey)
                        End If

                        If sIGEVoucherNo = "" Then ' Sinh số phiếu bị lỗi thì trả về rỗng và thoát
                            If sStringLastKey <> "" Then
                                D99C0008.MsgL3("Lỗi sinh số phiếu tự động", L3MessageBoxIcon.Err)
                                WriteLogFile("Loi sinh so phieu tu dong cua table " & TableVoucherNo)
                            Else
                                WriteLogFile("Loi sinh so phieu (chieu dai qua gioi han) cua table " & TableVoucherNo)
                            End If
                            Return ""
                        End If

                    Else ' Có textbox SỐ phiếu
                        sIGEVoucherNo = sVoucherNo
                    End If
                    '**********************************************


                    Dim nNewLastKey As Long
                    nNewLastKey = (iLastKey - 1) + NumberIGE
                    'Lưu Lastkey với số dòng tương ứng truyền vào NumberIGE
                    SaveLastKey(TableVoucherNo, sKeyString, nNewLastKey)

                    'Kiểm tra trùng khóa
                    Dim sKeyFrom As String, sKeyTo As String
                    Dim iZeroLen As Integer ' Chiều dài của số 0 chèn vào trước LastKey
                    'Dim StringLastKey As String

                    sKeyFrom = sIGEVoucherNo
                    If NumberIGE = 1 Then ' 1 dòng
                        sKeyTo = sKeyFrom
                    Else ' Nhiều dòng
                        iZeroLen = iOutputLength - nNewLastKey.ToString.Length - (sKeyString.Length)
                        '----------------------------
                        If sSeperator <> "" Then
                            If sS1 <> "" Then iZeroLen -= 1
                            If sS2 <> "" Then iZeroLen -= 1
                            If sS3 <> "" Then iZeroLen -= 1
                        End If

                        If iZeroLen < 0 Then
                            AnnouncementLength()
                            Return ""
                        Else
                            sStringLastKey = Strings.StrDup(iZeroLen, "0") & nNewLastKey
                        End If
                        '----------------------------
                        sKeyTo = Generate(sS1, sS2, sS3, iOutputOrder, sSeperator, sStringLastKey)

                    End If

                    bKey = CheckDupKeyPrimary(TableKey, FieldIDKey, sKeyFrom, sKeyTo)

                    'Hop le thi lay du lieu va thoat
                    If Not bKey Then
                        Return sIGEVoucherNo
                    End If

                Loop Until bKey = False
                Return ""

            Else
                Dim iLength As Integer ' Chiều dài của chuỗi nằm trước số N
                Dim iLengthLast As Integer ' Chiều dài của chuỗi nằm sau số N

                'Lấy LastKey
                Select Case iOutputOrder
                    Case OutOrderEnum.lmSSSN
                        iLength = sS1.Length + sS2.Length + sS3.Length
                        iLengthLast = 0
                        If sSeperator <> "" Then
                            If sS1 <> "" Then iLength += 1
                            If sS2 <> "" Then iLength += 1
                            If sS3 <> "" Then iLength += 1
                        End If
                    Case OutOrderEnum.lmSSNS
                        iLength = sS1.Length + sS2.Length
                        iLengthLast = sS3.Length
                        If sSeperator <> "" Then
                            If sS1 <> "" Then iLength += 1
                            If sS2 <> "" Then iLength += 1

                            If sS3 <> "" Then iLengthLast += 1
                        End If
                    Case OutOrderEnum.lmSNSS
                        iLength = sS1.Length
                        iLengthLast = sS2.Length + sS3.Length
                        If sSeperator <> "" Then
                            If sS1 <> "" Then iLength += 1

                            If sS2 <> "" Then iLengthLast += 1
                            If sS3 <> "" Then iLengthLast += 1

                        End If
                    Case OutOrderEnum.lmNSSS
                        iLength = 0
                        iLengthLast = sS1.Length + sS2.Length + sS3.Length
                        If sSeperator <> "" Then
                            If sS1 <> "" Then iLengthLast += 1
                            If sS2 <> "" Then iLengthLast += 1
                            If sS3 <> "" Then iLengthLast += 1

                        End If
                End Select
                iLastKey = Convert.ToInt32(OldVoucherNo.Substring(iLength, OldVoucherNo.Length - (iLength + iLengthLast))) + 1
                'Lấy chuỗi LastKey
                sStringLastKey = CheckLengthKey(iLastKey, sS1, sS2, sS3, sSeperator, iOutputLength)
                'Sinh chuỗi mới
                sIGEVoucherNo = Generate(sS1, sS2, sS3, iOutputOrder, sSeperator, sStringLastKey)

                Return sIGEVoucherNo
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Announcement", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return ""
        End Try

    End Function
#End Region

#End Region

#Region "Tạo chỉ số tự động mới cho số phiếu"

    ''' <summary>
    ''' Tạo chỉ số tự động mới
    ''' </summary>
    ''' <param name="c1Combo">Combo tdbcVoucherTypeID theo chuẩn</param>
    ''' <param name="txtVoucherNo">Textbox VoucherNo truyền vào</param>
    ''' <remarks>Chú ý: phải định nghĩa theo chuẩn của combo tdbcVoucherTypeID</remarks>
    <DebuggerStepThrough()> _
    Public Sub GetNewVoucherNo(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal txtVoucherNo As TextBox)
        GetNewVoucherNo(c1Combo, txtVoucherNo, "D91T0001")
    End Sub

    ''' <summary>
    ''' Tạo chỉ số tự động mới
    ''' </summary>
    ''' <param name="c1Combo">Combo tdbcVoucherTypeID theo chuẩn</param>
    ''' <param name="txtVoucherNo">Textbox VoucherNo truyền vào</param>
    ''' <remarks>Chú ý: phải định nghĩa theo chuẩn của combo tdbcVoucherTypeID</remarks>
    <DebuggerStepThrough()> _
    Public Sub GetNewVoucherNo(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal txtVoucherNo As TextBox, ByVal sTableName As String)
        Dim strS1 As String = ""
        Dim strS2 As String = ""
        Dim strS3 As String = ""
        If c1Combo.Columns("S1Type").Text <> "0" Then
            strS1 = FindSxType(c1Combo.Columns("S1Type").Text, c1Combo.Columns("S1").Text.Trim)
        End If
        If c1Combo.Columns("S2Type").Text <> "0" Then
            strS2 = FindSxType(c1Combo.Columns("S2Type").Text, c1Combo.Columns("S2").Text.Trim)
        End If
        If c1Combo.Columns("S3Type").Text <> "0" Then
            strS3 = FindSxType(c1Combo.Columns("S3Type").Text, c1Combo.Columns("S3").Text.Trim)
        End If
        Dim frm As New D99F1111
        frm.TableName = sTableName
        frm.NewKeyString = strS1 & strS2 & strS3
        frm.ShowDialog()
        If frm.Result = True Then
            Dim sVoucherNo As String
            sVoucherNo = GetTempNewVoucherNo(gnNewLastKey, strS1, strS2, strS3, CType(c1Combo.Columns("OutputOrder").Text, D99D0041.OutOrderEnum), Convert.ToInt32(c1Combo.Columns("OutputLength").Text), c1Combo.Columns("Separator").Text.Trim, sTableName)
            If sVoucherNo <> "" Then
                txtVoucherNo.Text = sVoucherNo
            Else
                gnNewLastKey = 0
            End If
            frm.Dispose()
        Else
            frm.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Tạo chỉ số tự động mới
    ''' </summary>
    ''' <param name="txtVoucherNo">Textbox cần sinh tự động</param>
    ''' <param name="S1">Chuỗi 1</param>
    ''' <param name="S2">Chuỗi 2</param>
    ''' <param name="S3">Chuỗi 3</param>
    ''' <param name="OutputOrder"></param>
    ''' <param name="OutputLength"></param>
    ''' <param name="Separator"></param>
    ''' <remarks>Tạo theo chuỗi mới truyền vào</remarks>
    <DebuggerStepThrough()> _
    Public Sub GetNewVoucherNo(ByVal txtVoucherNo As TextBox, ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String)
        GetNewVoucherNo(txtVoucherNo, S1, S2, S3, OutputOrder, OutputLength, Separator, "D91T0001")

    End Sub

    <DebuggerStepThrough()> _
    Public Sub GetNewVoucherNo(ByVal txtVoucherNo As TextBox, ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String, ByVal Table As String)
        'Gọi form D99F1111 để lấy được gnNewLastKey

        Dim frm As New D99F1111
        frm.NewKeyString = S1 & S2 & S3
        frm.TableName = Table
        frm.ShowDialog()
        If frm.Result = True Then
            Dim sVoucherNo As String
            sVoucherNo = GetTempNewVoucherNo(gnNewLastKey, S1, S2, S3, OutputOrder, OutputLength, Separator.Trim, Table)
            If sVoucherNo <> "" Then
                txtVoucherNo.Text = sVoucherNo
            Else
                gnNewLastKey = 0
            End If
            frm.Dispose()
        Else
            frm.Dispose()
        End If
    End Sub

    <DebuggerStepThrough()> _
    Private Function GetTempNewVoucherNo(ByVal NewLastKey As Long, ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal OutputOrder As D99D0041.OutOrderEnum, ByVal OutputLength As Integer, ByVal Separator As String, ByVal Table As String) As String
        Dim sVoucherNo As String = ""
        Dim iLengthLastKey As Int32 = 0
        Dim sNewLastKey As String = ""

        sNewLastKey = GetNewLastKey(NewLastKey, S1, S2, S3, Separator, OutputLength)
        If sNewLastKey = "" Then Return ""

        Select Case OutputOrder
            Case D99D0041.OutOrderEnum.lmSSSN
                If Separator <> "" Then
                    If S1 <> "" Then
                        sVoucherNo &= S1 & Separator
                    End If
                    If S2 <> "" Then
                        sVoucherNo &= S2 & Separator
                    End If
                    If S3 <> "" Then
                        sVoucherNo &= S3 & Separator
                    End If
                    sVoucherNo &= sNewLastKey
                Else
                    sVoucherNo = S1 & S2 & S3 & sNewLastKey
                End If

            Case D99D0041.OutOrderEnum.lmSSNS
                If Separator <> "" Then
                    If S1 <> "" Then
                        sVoucherNo &= S1 & Separator
                    End If
                    If S2 <> "" Then
                        sVoucherNo &= S2 & Separator
                    End If
                    sVoucherNo &= sNewLastKey
                    If S3 <> "" Then
                        sVoucherNo &= Separator & S3
                    End If

                Else
                    sVoucherNo = S1 & S2 & sNewLastKey & S3
                End If

            Case D99D0041.OutOrderEnum.lmSNSS
                If Separator <> "" Then
                    If S1 <> "" Then
                        sVoucherNo &= S1 & Separator
                    End If
                    sVoucherNo &= sNewLastKey
                    If S2 <> "" Then
                        sVoucherNo &= Separator & S2
                    End If
                    If S3 <> "" Then
                        sVoucherNo &= Separator & S3
                    End If

                Else
                    sVoucherNo = S1 & sNewLastKey & S2 & S3
                End If

            Case D99D0041.OutOrderEnum.lmNSSS
                If Separator <> "" Then
                    sVoucherNo &= sNewLastKey
                    If S1 <> "" Then
                        sVoucherNo &= Separator & S1
                    End If
                    If S2 <> "" Then
                        sVoucherNo &= Separator & S2
                    End If
                    If S3 <> "" Then
                        sVoucherNo &= Separator & S3
                    End If

                Else
                    sVoucherNo = sNewLastKey & S1 & S2 & S3
                End If

        End Select

        Return sVoucherNo

    End Function

    <DebuggerStepThrough()> _
    Private Function GetNewLastKey(ByVal nLastKey As Long, ByVal sStringKey1 As String, ByVal sStringKey2 As String, ByVal sStringKey3 As String, ByVal sSeperatorCharacter As String, ByVal nOutputLength As Long) As String
        Dim nKeyLength As Integer = 0
        Dim nLastKeyLength As Integer = 0

        If sSeperatorCharacter.Trim <> "" Then
            If sStringKey1 <> "" Then
                nKeyLength += sStringKey1.Length + sSeperatorCharacter.Length
            End If
            If sStringKey2 <> "" Then
                nKeyLength += sStringKey2.Length + sSeperatorCharacter.Length
            End If
            If sStringKey3 <> "" Then
                nKeyLength += sStringKey3.Length + sSeperatorCharacter.Length
            End If
        Else
            If sStringKey1 <> "" Then nKeyLength += sStringKey1.Length
            If sStringKey2 <> "" Then nKeyLength += sStringKey2.Length
            If sStringKey3 <> "" Then nKeyLength += sStringKey3.Length
        End If

        If (nKeyLength + nLastKey.ToString.Length) > nOutputLength Then
            If geLanguage = EnumLanguage.Vietnamese Then
                D99C0008.MsgL3("Chiều dài thiết lập vượt quá giới hạn cho phép." & vbCrLf & "Bạn phải thiết lập lại.", L3MessageBoxIcon.Exclamation)
            Else
                D99C0008.MsgL3("The lenght setup is off limits." & vbCrLf & "You should set again.", L3MessageBoxIcon.Exclamation)
            End If
            Return ""
        End If

        nLastKeyLength = Convert.ToInt32(nOutputLength) - nKeyLength - nLastKey.ToString.Length
        Return Strings.StrDup(nLastKeyLength, "0") & nLastKey

    End Function
#End Region


#Region "Lưu mã số tự động của số phiếu xuống database "

    ''' <summary>
    ''' Lưu Khóa tự động khi sinh phiếu xuống database
    ''' </summary>
    ''' <param name="c1Combo"></param>
    ''' <remarks>Chú ý: biến gnNewLastKey</remarks>
    <DebuggerStepThrough()> _
    Public Sub SaveNewLastKey(ByVal c1Combo As C1.Win.C1List.C1Combo)
        SaveNewLastKey(c1Combo, "D91T0001")
    End Sub

    ''' <summary>
    ''' Lưu Khóa tự động khi sinh phiếu xuống database
    ''' </summary>
    ''' <param name="c1Combo"></param>
    ''' <remarks>Chú ý: biến gnNewLastKey</remarks>
    <DebuggerStepThrough()> _
    Public Sub SaveNewLastKey(ByVal c1Combo As C1.Win.C1List.C1Combo, ByVal sTableName As String)
        Dim strS1 As String = ""
        Dim strS2 As String = ""
        Dim strS3 As String = ""
        If c1Combo.Columns("S1Type").Text <> "0" Then
            strS1 = FindSxType(c1Combo.Columns("S1Type").Text, c1Combo.Columns("S1").Text.Trim)
        End If
        If c1Combo.Columns("S2Type").Text <> "0" Then
            strS2 = FindSxType(c1Combo.Columns("S2Type").Text, c1Combo.Columns("S2").Text.Trim)
        End If
        If c1Combo.Columns("S3Type").Text <> "0" Then
            strS3 = FindSxType(c1Combo.Columns("S3Type").Text, c1Combo.Columns("S3").Text.Trim)
        End If

        SaveNewLastKey(strS1, strS2, strS3, sTableName)

    End Sub

    ''' <summary>
    ''' Lưu Khóa tự động khi sinh phiếu xuống database
    ''' </summary>
    ''' <param name="S1"></param>
    ''' <param name="S2"></param>
    ''' <param name="S3"></param>
    ''' <remarks>Chú ý: biến gnNewLastKey</remarks>
    <DebuggerStepThrough()> _
    Public Sub SaveNewLastKey(ByVal S1 As String, ByVal S2 As String, ByVal S3 As String)
        SaveNewLastKey(S1, S2, S3, "D91T0001")
    End Sub

    ''' <summary>
    ''' Lưu Khóa tự động khi sinh phiếu xuống database
    ''' </summary>
    ''' <param name="S1"></param>
    ''' <param name="S2"></param>
    ''' <param name="S3"></param>
    ''' <param name="sTableName"> Bảng mặc đinh lưu D91T0001</param>
    ''' <remarks>Chú ý: biến gnNewLastKey</remarks>
    <DebuggerStepThrough()> _
    Public Sub SaveNewLastKey(ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal sTableName As String)
        Dim sKeyString As String = S1 & S2 & S3
        Dim sSQL As String
        Dim nNewLastKey As Long = 0

        If sTableName = "" Then sTableName = "D91T0001"

        If gnNewLastKey <> 0 Then 'Có click vào chọn khóa mới
            nNewLastKey = gnNewLastKey
        Else
            sSQL = "Select LastKey From D91T0000 Where TableName = " & SQLString(sTableName) & " And KeyString = " & SQLString(sKeyString)
            Dim dt As DataTable = ReturnDataTable(sSQL)
            If dt.Rows.Count > 0 Then
                nNewLastKey = Convert.ToInt64(dt.Rows(0).Item("LastKey").ToString) + 1
            Else
                sSQL = "INSERT INTO D91T0000 (TableName, KeyString, LastKey) VALUES ('" & sTableName & "', '" & sKeyString & "',1)"
                ExecuteSQLNoTransaction(sSQL)
            End If
        End If

        If nNewLastKey > 0 Then
            sSQL = "Update D91T0000 set LastKey = " & SQLNumber(nNewLastKey) & " Where TableName =  " & SQLString(sTableName) & "  And KeyString = " & SQLString(sKeyString)
            ExecuteSQLNoTransaction(sSQL)
        End If
    End Sub
#End Region

#Region "Kiểm tra"
    ''' <summary>
    ''' Gán giá trị Đến = Từ sau khi LostFocus ra khỏi combo Từ
    ''' </summary>
    ''' <param name="tdbcFrom">Combo Từ</param>
    ''' <param name="tdbcTo">Combo Đến</param>
    ''' <param name="bPressF2">Gán = True tại các combo có nhấn F2 (vd: combo Tiêu thức)</param>
    ''' <remarks>Gọi hàm tại LostFocus của combo Từ, sau khi kiểm tra giá trị có trong danh sách</remarks>
    Public Sub SetValueTo(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByRef bPressF2 As Boolean = False)
        'Nếu nhấn F2 Tìm kiếm thì không gán lại giá trị Đến = Từ
        If bPressF2 Then
            bPressF2 = False
            Exit Sub
        End If
        'Mặc định gán Đến = Từ
        If tdbcFrom.Tag Is Nothing OrElse tdbcFrom.Tag.ToString <> tdbcFrom.Text Then
            If (tdbcFrom.SelectedValue IsNot Nothing AndAlso tdbcFrom.Text <> "") Then
                tdbcTo.Text = tdbcFrom.Text
                tdbcFrom.Tag = tdbcFrom.Text
            End If
        End If
    End Sub



    ''' <summary>
    ''' Từ quý phải nhỏ hơn bằng Đến quý
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <returns>True : Valid; False: Invalid</returns>
    ''' <remarks></remarks>
    Public Function CheckQuaterFromTo(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal tabSelection As System.Windows.Forms.TabControl = Nothing, Optional ByVal Index As Integer = -1) As Boolean
        'Chưa nhập giá trị Từ Đến
        If (tdbcFrom.SelectedValue Is Nothing OrElse tdbcFrom.Text = "") And (tdbcTo.SelectedValue Is Nothing OrElse tdbcTo.Text = "") Then
            D99C0008.MsgNotYetChoose(rl3("Quy"))
            If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
            tdbcFrom.Focus()
            Return False
        ElseIf tdbcTo.SelectedValue Is Nothing OrElse tdbcTo.Text = "" Then ' Chưa nhập Đến thì gán giá trị của Từ vào
            tdbcTo.Text = tdbcFrom.Text
        ElseIf tdbcFrom.SelectedValue Is Nothing OrElse tdbcFrom.Text = "" Then ' Chưa nhập Từ thì gán giá trị của Đến vào
            tdbcFrom.Text = tdbcTo.Text
        Else
            Dim iFrom As Integer = 0, iTo As Integer = 0
            iFrom = L3Int(tdbcFrom.Columns("TranYear").Text) * 10 + L3Int(tdbcFrom.Columns("Quarter").Text)
            iTo = L3Int(tdbcTo.Columns("TranYear").Text) * 10 + L3Int(tdbcTo.Columns("Quarter").Text)
            If iFrom > iTo Then
                D99C0008.MsgL3(rl3("MSG000041"))
                If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
                tdbcTo.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Từ năm phải nhỏ hơn bằng Đến năm
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <returns>True : Valid; False: Invalid</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function CheckYearFromTo(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal tabSelection As System.Windows.Forms.TabControl = Nothing, Optional ByVal Index As Integer = -1) As Boolean
        'Chưa nhập giá trị Từ Đến
        If (tdbcFrom.SelectedValue Is Nothing OrElse tdbcFrom.Text = "") And (tdbcTo.SelectedValue Is Nothing OrElse tdbcTo.Text = "") Then
            D99C0008.MsgNotYetChoose(rl3("Nam"))
            If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
            tdbcFrom.Focus()
            Return False
        ElseIf tdbcTo.SelectedValue Is Nothing OrElse tdbcTo.Text = "" Then ' Chưa nhập Đến thì gán giá trị của Từ vào
            tdbcTo.Text = tdbcFrom.Text
        ElseIf tdbcFrom.SelectedValue Is Nothing OrElse tdbcFrom.Text = "" Then ' Chưa nhập Từ thì gán giá trị của Đến vào
            tdbcFrom.Text = tdbcTo.Text
        Else
            Dim iFrom As Integer = 0, iTo As Integer = 0
            iFrom = L3Int(tdbcFrom.Columns("Year").Text)
            iTo = L3Int(tdbcTo.Columns("Year").Text)
            If iFrom > iTo Then
                D99C0008.MsgL3(rl3("MSG000015"))
                If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
                tdbcTo.Focus()
                Return False
            End If
        End If
        Return True

    End Function
    ''' <summary>
    ''' Từ kỳ phải nhỏ hơn bằng Đến kỳ
    ''' </summary>
    ''' <param name="tdbcFrom"></param>
    ''' <param name="tdbcTo"></param>
    ''' <returns>True : Valid; False: Invalid</returns>
    ''' <remarks></remarks>
    Public Function CheckValidPeriodFromTo(ByVal tdbcFrom As C1.Win.C1List.C1Combo, ByVal tdbcTo As C1.Win.C1List.C1Combo, Optional ByVal tabSelection As System.Windows.Forms.TabControl = Nothing, Optional ByVal Index As Integer = -1) As Boolean
        'Chưa nhập giá trị Từ Đến
        If (tdbcFrom.SelectedValue Is Nothing OrElse tdbcFrom.Text = "") And (tdbcTo.SelectedValue Is Nothing OrElse tdbcTo.Text = "") Then
            D99C0008.MsgNotYetChoose(rl3("Ky"))
            If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
            tdbcFrom.Focus()
            Return False
        ElseIf tdbcTo.SelectedValue Is Nothing OrElse tdbcTo.Text = "" Then ' Chưa nhập Đến thì gán giá trị của Từ vào
            tdbcTo.Text = tdbcFrom.Text
        ElseIf tdbcFrom.SelectedValue Is Nothing OrElse tdbcFrom.Text = "" Then ' Chưa nhập Từ thì gán giá trị của Đến vào
            tdbcFrom.Text = tdbcTo.Text
        Else
            Dim iFrom As Integer = 0, iTo As Integer = 0
            iFrom = L3Int(tdbcFrom.Columns("TranYear").Text) * 100 + L3Int(tdbcFrom.Columns("TranMonth").Text)
            iTo = L3Int(tdbcTo.Columns("TranYear").Text) * 100 + L3Int(tdbcTo.Columns("TranMonth").Text)
            If iFrom > iTo Then
                D99C0008.MsgL3(rl3("MSG000014"))
                If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
                tdbcTo.Focus()
                Return False
            End If
        End If
        Return True

    End Function

    ''' <summary>
    ''' Từ ngày phải nhỏ hơn bằng Đến ngày
    ''' </summary>
    ''' <param name="c1dateFrom"></param>
    ''' <param name="c1dateTo"></param>
    ''' <returns>True : Valid; False: Invalid</returns>
    ''' <remarks></remarks>
    Public Function CheckValidDateFromTo(ByVal c1dateFrom As C1.Win.C1Input.C1DateEdit, ByVal c1dateTo As C1.Win.C1Input.C1DateEdit, Optional ByVal tabSelection As System.Windows.Forms.TabControl = Nothing, Optional ByVal Index As Integer = -1) As Boolean
        'Chưa nhập giá trị Từ Đến
        If c1dateFrom.Text = "" And c1dateTo.Text = "" Then
            D99C0008.MsgNotYetChoose(rl3("Ngay"))
            If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
            c1dateFrom.Focus()
            Return False
        ElseIf c1dateTo.Text = "" Then '  Chưa nhập Đến thì gán giá trị của Từ vào 
            c1dateTo.Text = c1dateFrom.Text
        ElseIf c1dateFrom.Text = "" Then '  Chưa nhập Từ thì gán giá trị của Đến vào 
            c1dateFrom.Text = c1dateTo.Text
        Else
            If CDate(c1dateFrom.Text) > CDate(c1dateTo.Text) Then
                D99C0008.MsgL3(rl3("MSG000013"))
                If tabSelection IsNot Nothing AndAlso Index <> -1 Then tabSelection.SelectedIndex = Index
                c1dateTo.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Kiểm tra giá trị có trong Dropdown
    ''' </summary>
    ''' <param name="tdbDD"></param>
    ''' <param name="sValue"></param>
    ''' <returns>True: có trong đanh sách</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CheckDropdownInList(ByVal tdbDD As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sValue As String) As Boolean
        'Dim dt As DataTable = CType(tdbDD.DataSource, DataTable).Copy
        'Dim row As Int32 = ReturnRowTable(dt, tdbDD.DisplayMember, sValue)
        'Return row >= 0
        'Minh Hòa Update 25/01/2013
        Return CheckDropdownInList(tdbDD, tdbDD.DisplayMember, sValue)
    End Function

    ''' <summary>
    ''' Kiểm tra giá trị có trong Dropdown có truyền KeysField
    ''' </summary>
    ''' <param name="tdbDD"></param>
    ''' <param name="sKeysField"></param>
    ''' <param name="sValue"></param>
    ''' <returns>True: có trong đanh sách</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CheckDropdownInList(ByVal tdbDD As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal sKeysField As String, ByVal sValue As String) As Boolean
        '        Dim dt As DataTable = CType(tdbDD.DataSource, DataTable).DefaultView.ToTable
        '        Dim row As Int32 = ReturnRowTable(dt, sKeysField, sValue)
        '        Return row >= 0

        'Minh Hòa Update 25/01/2013
        Dim dt As DataTable = CType(tdbDD.DataSource, DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return False
        Dim sWhere As String = sKeysField & " = " & SQLString(sValue)
        Dim dr() As DataRow = dt.Select(sWhere)
        Return dr.Length > 0

    End Function


    ''' <summary>
    ''' Kiểm tra Ngày phiếu có phù hợp với Kỳ kế toán hiện tại không, gọi tại nút Lưu
    ''' </summary>
    ''' <param name="VoucherDate">Ngày phiếu</param>
    ''' <returns>False: vi phạm, không làm gì cả </returns>
    ''' <remarks>Cần xem kỹ cách dùng theo tài liệu DC25</remarks>
    Public Function CheckVoucherDateInPeriod(ByVal VoucherDate As String) As Boolean
        Dim sSQL As String = ""
        sSQL = SQLStoreD91P9105(VoucherDate)
        Dim dt As DataTable = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Select Case Convert.ToInt16(dt.Rows(0).Item("Status"))
                Case 1
                    'If MessageBox.Show(dt.Rows(0).Item("Message").ToString, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    If D99C0008.Msg(ConvertVietwareFToUnicode(dt.Rows(0).Item("Message").ToString), MsgAnnouncement, L3MessageBoxButtons.YesNo, L3MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                        dt = Nothing
                        Return False
                    End If
                Case 2
                    'MessageBox.Show(dt.Rows(0).Item("Message").ToString, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    D99C0008.MsgL3(ConvertVietwareFToUnicode(dt.Rows(0).Item("Message").ToString), L3MessageBoxIcon.Exclamation)
                    dt = Nothing
                    Return False
            End Select
        End If
        dt = Nothing
        Return True
    End Function

    ''' <summary>
    ''' Kiểm tra Ngày phiếu có phù hợp với Kỳ kế toán hiện tại không, gọi tại nơi trước khi New Form
    ''' </summary>
    ''' <returns>False: vi phạm, không làm gì cả </returns>
    ''' <remarks>Cần xem kỹ cách dùng theo tài liệu DC25</remarks>
    Public Function CheckVoucherDateInPeriodFormLoad() As Boolean
        Dim sSQL As String = SQLStoreD91P9105(Now.Date.ToString)
        'Gọi tại Form_Load, lấy ngày hiện tại để đưa vào Store
        Dim dt As DataTable = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            If Convert.ToInt16(dt.Rows(0).Item("Status")) > 0 Then
                'Gọi tại Form_Load thì cảnh báo cho người dùng
                If D99C0008.MsgAsk(rl3("Ngay") & Space(1) & Now.Date & Space(1) & rl3("khong_thuoc_ky") & Space(1) & giTranMonth & "/" & giTranYear & "." & Space(1) & rl3("MSG000021"), MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    dt = Nothing
                    Return False
                End If
            End If
        End If
        dt = Nothing
        Return True
    End Function

    ''' <summary>
    ''' Kiểm tra có quyền nhập Ngày phiếu lớn hơn Ngày GetDate không?
    ''' </summary>
    ''' <param name="VoucherDate">Ngày phiếu</param>
    ''' <param name="FormPermission">Form phân quyền là DxxF5704</param>
    ''' <returns>False: vi phạm, không làm gì cả </returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CheckVoucherDateWithGetDate(ByVal VoucherDate As String, ByVal FormPermission As String) As Boolean
        Dim iPer As Integer = ReturnPermission(FormPermission)
        If iPer > 0 Then Return True

        Dim sSQL As String = "Select TOP 1 1 Where " & SQLDateSave(VoucherDate) & " > GETDATE() "
        If ExistRecord(sSQL) Then
            D99C0008.MsgL3(rl3("MSG000033"), L3MessageBoxIcon.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Kiểm tra trùng số phiếu
    ''' </summary>
    ''' <param name="ModuleID">Tên module: Dxx</param>
    ''' <param name="TableName">Tên bảng để lưu Số phiếu</param>
    ''' <param name="VoucherID">Giá trị của Khóa chính trong bảng lưu Số phiếu</param>
    ''' <param name="VoucherNo">Số phiếu</param>
    ''' <returns>True: vi phạm, không làm gì cả</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CheckDuplicateVoucherNo(ByVal ModuleID As String, ByVal TableName As String, ByVal VoucherID As String, ByVal VoucherNo As String) As Boolean
        Dim sSQL As String = ""
        sSQL = SQLStoreD91P9102(ModuleID.Substring(1, 2), TableName, VoucherID, VoucherNo)
        Dim dt As New DataTable
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Select Case Convert.ToInt16(dt.Rows(0).Item("Status"))
                Case 1
                    'If MessageBox.Show(dt.Rows(0).Item("Message").ToString, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    If D99C0008.Msg(ConvertVietwareFToUnicode(dt.Rows(0).Item("Message").ToString), MsgAnnouncement, L3MessageBoxButtons.YesNo, L3MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                        dt = Nothing
                        Return True
                    End If
                Case 2
                    'MessageBox.Show(dt.Rows(0).Item("Message").ToString, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    D99C0008.MsgL3(ConvertVietwareFToUnicode(dt.Rows(0).Item("Message").ToString), L3MessageBoxIcon.Exclamation)
                    dt = Nothing
                    Return True
            End Select
        End If
        dt = Nothing
        Return False
    End Function

    '''' <summary>
    '''' Kiểm tra dữ liệu bằng Store (Hàm cũ)
    '''' </summary>
    '''' <param name="SQL">Store cần kiểm tra</param>
    '''' <returns>Trả về True nếu kiểm tra không có lỗi, ngược lại trả về False</returns>
    '''' <remarks>Chú ý: Kết quả trả ra của Store phải có dạng là 1 hàng và 2 cột là Status và Message</remarks>
    '<DebuggerStepThrough()> _
    'Public Function CheckStore(ByVal SQL As String, ByVal bMsgAsk As Boolean, Optional ByVal sConnectionStringNew As String = "") As Boolean
    '    'Update 11/10/2010: sửa lại hàm checkstore có trả ra field FontMessage
    '    'Cách kiểm tra của hàm CheckStore này sẽ như sau:
    '    'Nếu store trả ra Status <> 0 thì xuất Message theo dạng FontMessage
    '    'Nếu đối số thứ 2 không truyền vào có nghĩa là False thì xuất Message chỉ có 1 nút Ok
    '    'Nếu đối số thứ 2 có truyền vào có nghĩa là True thì xuất Message có 2 nút Yes, No

    '    Dim dt As New DataTable
    '    Dim sMsg As String
    '    dt = ReturnDataTable(SQL, sConnectionStringNew)
    '    If dt.Rows.Count > 0 Then
    '        If dt.Rows(0).Item("Status").ToString = "0" Then
    '            dt = Nothing
    '            Return True
    '        End If

    '        sMsg = dt.Rows(0).Item("Message").ToString
    '        Dim bFontMessage As Boolean = False
    '        If dt.Columns.Contains("FontMessage") Then bFontMessage = True

    '        If Not bMsgAsk Then 'OKOnly
    '            If Not bFontMessage Then
    '                MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Else
    '                Select Case dt.Rows(0).Item("FontMessage").ToString
    '                    Case "0" 'VietwareF
    '                        MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Case "1" 'Unicode
    '                        D99C0008.MsgL3(sMsg, L3MessageBoxIcon.Exclamation)
    '                    Case "2" 'Convert Vni To Unicode
    '                        D99C0008.MsgL3(ConvertVniToUnicode(sMsg), L3MessageBoxIcon.Exclamation)
    '                End Select
    '            End If
    '            dt = Nothing
    '            Return False
    '        Else 'YesNo
    '            If Not bFontMessage Then
    '                If MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
    '                    dt = Nothing
    '                    Return True
    '                Else
    '                    dt = Nothing
    '                    Return False
    '                End If
    '            Else
    '                Select Case dt.Rows(0).Item("FontMessage").ToString
    '                    Case "0" 'VietwareF
    '                        If MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
    '                            dt = Nothing
    '                            Return True
    '                        Else
    '                            dt = Nothing
    '                            Return False
    '                        End If
    '                    Case "1" 'Unicode
    '                        If D99C0008.MsgAsk(sMsg, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
    '                            dt = Nothing
    '                            Return True
    '                        Else
    '                            dt = Nothing
    '                            Return False
    '                        End If
    '                    Case "2" 'Convert Vni To Unicode
    '                        If D99C0008.MsgAsk(ConvertVniToUnicode(sMsg), MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
    '                            dt = Nothing
    '                            Return True
    '                        Else
    '                            dt = Nothing
    '                            Return False
    '                        End If
    '                End Select
    '            End If
    '        End If
    '        dt = Nothing
    '    Else
    '        'D99C0008.MsgL3("Không có dòng nào trả ra từ Store")
    '        Return False
    '    End If
    '    Return True
    'End Function


    '''' <summary>
    '''' Kiểm tra dữ liệu bằng Store (HÀM MỚI) dạng thông báo do store tự trả ra
    '''' </summary>
    '''' <param name="SQL">Store cần kiểm tra</param>
    '''' <returns>Trả về True nếu kiểm tra không có lỗi, ngược lại trả về False</returns>
    '''' <remarks>Chú ý: Kết quả trả ra của Store phải có dạng là 1 hàng và 4 cột là Status, Message, FontMessage, MsgAsk</remarks>
    'Public Function CheckStore(ByVal SQL As String, Optional ByVal sConnectionStringNew As String = "") As Boolean
    '    'Update 1/03/2010: sửa lại hàm checkstore có trả ra field FontMessage
    '    'Cách kiểm tra của hàm CheckStore này sẽ như sau:
    '    'Nếu store trả ra Status <> 0 thì xuất Message theo dạng FontMessage
    '    'Nếu store trả ra MsgAsk = 0 thì xuất Message nút Ok,  MsgAsk = 1 thì xuất Message nút Yes, No

    '    Dim dt As New DataTable
    '    Dim sMsg As String
    '    Dim bMsgAsk As Boolean = False
    '    dt = ReturnDataTable(SQL, sConnectionStringNew)
    '    If dt.Rows.Count > 0 Then
    '        If dt.Rows(0).Item("Status").ToString = "0" Then
    '            dt = Nothing
    '            Return True
    '        End If

    '        sMsg = dt.Rows(0).Item("Message").ToString
    '        Dim bFontMessage As Boolean = False
    '        If dt.Columns.Contains("FontMessage") Then bFontMessage = True
    '        If dt.Columns.Contains("MsgAsk") Then
    '            If L3Byte(dt.Rows(0).Item("MsgAsk")) = 1 Then
    '                bMsgAsk = True
    '            End If
    '        End If

    '        If Not bMsgAsk Then 'OKOnly
    '            If Not bFontMessage Then
    '                MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Else
    '                Select Case dt.Rows(0).Item("FontMessage").ToString
    '                    Case "0" 'VietwareF
    '                        MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Case "1" 'Unicode
    '                        D99C0008.MsgL3(sMsg, L3MessageBoxIcon.Exclamation)
    '                    Case "2" 'Convert Vni To Unicode
    '                        D99C0008.MsgL3(ConvertVniToUnicode(sMsg), L3MessageBoxIcon.Exclamation)
    '                End Select
    '            End If
    '            dt = Nothing
    '            Return False
    '        Else 'YesNo
    '            If Not bFontMessage Then
    '                If MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
    '                    dt = Nothing
    '                    Return True
    '                Else
    '                    dt = Nothing
    '                    Return False
    '                End If
    '            Else
    '                Select Case dt.Rows(0).Item("FontMessage").ToString
    '                    Case "0" 'VietwareF
    '                        If MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
    '                            dt = Nothing
    '                            Return True
    '                        Else
    '                            dt = Nothing
    '                            Return False
    '                        End If
    '                    Case "1" 'Unicode
    '                        If D99C0008.MsgAsk(sMsg, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
    '                            dt = Nothing
    '                            Return True
    '                        Else
    '                            dt = Nothing
    '                            Return False
    '                        End If
    '                    Case "2" 'Convert Vni To Unicode
    '                        If D99C0008.MsgAsk(ConvertVniToUnicode(sMsg), MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
    '                            dt = Nothing
    '                            Return True
    '                        Else
    '                            dt = Nothing
    '                            Return False
    '                        End If
    '                End Select
    '            End If
    '        End If
    '        dt = Nothing
    '    Else
    '        'D99C0008.MsgL3("Không có dòng nào trả ra từ Store")
    '        Return False
    '    End If
    '    Return True
    'End Function

    Public Function CheckStore(ByVal SQL As String, ByVal bMsgAsk As Boolean, Optional ByVal sConnectionStringNew As String = "", Optional ByRef dt As DataTable = Nothing, Optional ByVal bDeleteAll As Boolean = False) As Boolean
        If dt Is Nothing OrElse dt.Columns.Count = 0 Then dt = ReturnDataTable(SQL, sConnectionStringNew)
        If dt.Rows.Count = 0 Then Return False

        If dt.Rows(0).Item("Status").ToString = "0" Then
            Return True
        Else
            '11/12/2013: ID 59551 : Cho xóa cùng lúc nhiều phiếu import
            If bDeleteAll Then Return False
        End If

        Dim sMsg As String = dt.Rows(0).Item("Message").ToString
        Dim bFontMessage As Boolean = False
        If dt.Columns.Contains("FontMessage") Then bFontMessage = True
        If bFontMessage Then
            'Update 22/07/2013: Chuyển đổi các chuỗi về Unicode
            Select Case L3Int(dt.Rows(0).Item("FontMessage"))
                Case 0 'VietwareF
                    sMsg = ConvertVietwareFToUnicode(sMsg)
                Case 1 'Unicode

                Case 2 'Vni
                    sMsg = ConvertVniToUnicode(sMsg)
            End Select
        Else 'Dạng cũ VietwareF
            sMsg = ConvertVietwareFToUnicode(sMsg)
        End If

        If giReplacResource <> 0 Then
            sMsg = ReplaceResourceCustom(sMsg, True)
        End If

        If bMsgAsk Then 'YesNo
            'If MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
            If D99C0008.MsgAsk(sMsg, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        Else 'OKOnly
            'MessageBox.Show(sMsg, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            D99C0008.MsgL3(sMsg, L3MessageBoxIcon.Exclamation)
            Return False
        End If
        Return True
    End Function

    Public Function CheckStore(ByVal SQL As String, Optional ByVal sConnectionStringNew As String = "", Optional ByRef dt As DataTable = Nothing, Optional ByVal bDeleteAll As Boolean = False) As Boolean
        If dt Is Nothing OrElse dt.Columns.Count = 0 Then dt = ReturnDataTable(SQL, sConnectionStringNew)
        If dt.Rows.Count = 0 Then Return False

        If dt.Rows(0).Item("Status").ToString = "0" Then Return True
        Dim bMsgAsk As Boolean = False
        If dt.Columns.Contains("MsgAsk") Then
            If L3Byte(dt.Rows(0).Item("MsgAsk")) = 1 Then bMsgAsk = True
        End If
        '11/12/2013: ID 59551 : Cho xóa cùng lúc nhiều phiếu import
        Return CheckStore("", bMsgAsk, , dt, bDeleteAll)
    End Function

#End Region

#Region "Phân quyền"

    ''' <summary>
    ''' Kiểm tra phân quyền cho C1ContextMenu và lưới C1Grid
    ''' </summary>
    ''' <param name="FormName">Tên form cần phân quyền</param>
    ''' <param name="C1CommandHolder">C1CommandHolder lưu trữ các C1Command</param>
    ''' <param name="GridRowCount">Số dòng của lưới. Truyền thông số như sau: tdbg.Splits(Split0).Rows.Count</param>
    ''' <param name="UsedFind">Truyền biến toàn cục gbEnabledUseFind</param>
    ''' <param name="CheckCloseBook">Có kiểm tra khóa sổ không ?</param>
    ''' <param name="FormPermissionImport">Form phân quyền của menu Import dữ liệu</param>
    ''' <remarks>Hàm này chỉ có tác dụng nếu các C1ContextMenu có tên lần lượt như sau: 
    ''' mnuView, mnuAdd, mnuEdit, mnuDelete, mnuFind, mnuListAll, mnuSysInfo, mnuPrint
    ''' </remarks>
    Public Sub CheckMenu(ByVal FormName As String, ByVal C1CommandHolder As C1.Win.C1Command.C1CommandHolder, ByVal GridRowCount As Integer, ByVal UsedFind As Boolean, ByVal CheckCloseBook As Boolean, Optional ByVal CheckPeriod13 As Boolean = True, Optional ByVal FormPermissionImport As String = "")
        'Update 14/01/2011: kiểm tra kỳ 13, form nào kiểm tra Khóa sổ thì kiểm tra kỳ 13
        'Trừ trường hợp Khóa phiếu không kiểm tra kỳ 13 
        Dim bPeriod13 As Boolean = True
        If CheckPeriod13 And giTranMonth = 13 Then
            bPeriod13 = False
        End If

        'CheckPeriod13
        Dim per As Integer = ReturnPermission(FormName)
        Dim perImport As Integer = ReturnPermission(FormPermissionImport)

        For Each c As C1.Win.C1Command.C1Command In C1CommandHolder.Commands
            Select Case c.Name
                Case "mnuView" : c.Enabled = (per - 1 >= 0) And GridRowCount > 0
                Case "mnuAdd"
                    If CheckCloseBook Then
                        c.Enabled = (per - 2 >= 0) And Not gbClosed And bPeriod13
                    Else
                        c.Enabled = (per - 2 >= 0)
                    End If
                Case "mnuEdit"
                    If CheckCloseBook Then
                        c.Enabled = (per - 3 >= 0) And GridRowCount > 0 And Not gbClosed And bPeriod13
                    Else
                        c.Enabled = (per - 3 >= 0) And GridRowCount > 0
                    End If
                Case "mnuEditOther" ' update 2/12/2013 - Quyền sửa khác bằng quyền sửa
                    If CheckCloseBook Then
                        c.Enabled = (per - 3 >= 0) And GridRowCount > 0 And Not gbClosed And bPeriod13
                    Else
                        c.Enabled = (per - 3 >= 0) And GridRowCount > 0
                    End If
                Case "mnuDelete"
                    If CheckCloseBook Then
                        c.Enabled = (per - 4 >= 0) And GridRowCount > 0 And Not gbClosed And bPeriod13
                    Else
                        c.Enabled = (per - 4 >= 0) And GridRowCount > 0
                    End If
                Case "mnuFind" : c.Enabled = UsedFind Or GridRowCount > 0
                Case "mnuListAll" : c.Enabled = UsedFind Or GridRowCount > 0
                Case "mnuSysInfo" : c.Enabled = GridRowCount > 0
                Case "mnuPrint" : c.Enabled = GridRowCount > 0
                Case "mnuLocked", "mnuLockVoucher" 'Khóa phiếu
                    If CheckCloseBook Then
                        c.Enabled = GridRowCount > 0 And Not gbClosed
                    Else
                        c.Enabled = GridRowCount > 0
                    End If
                Case "mnuShowDetail" : c.Enabled = GridRowCount > 0
                    'Update 05/03/2010
                Case "mnuExportToExcel" : c.Enabled = GridRowCount > 0
                    'Update 19/03/2013
                Case "mnuImportData"
                    If CheckCloseBook Then
                        c.Enabled = (perImport - 2 >= 0) And Not gbClosed And bPeriod13
                    Else
                        c.Enabled = (perImport - 2 >= 0)
                    End If
            End Select
        Next
    End Sub


    Public Sub CheckMenu(ByVal FormName As String, ByVal tableToolStrip As ToolStrip, ByVal GridRowCount As Integer, ByVal UsedFind As Boolean, ByVal CheckCloseBook As Boolean, ByVal ContextMenuStrip As System.Windows.Forms.ContextMenuStrip, Optional ByVal CheckPeriod13 As Boolean = True, Optional ByVal FormPermissionImport As String = "")
        'Update 14/01/2011: kiểm tra kỳ 13, form nào kiểm tra Khóa sổ thì kiểm tra kỳ 13
        'Trừ trường hợp Khóa phiếu không kiểm tra kỳ 13 
        Dim bPeriod13 As Boolean = True
        If CheckPeriod13 And giTranMonth = 13 Then bPeriod13 = False

        Dim per As Integer = ReturnPermission(FormName)
        Dim perImport As Integer = ReturnPermission(FormPermissionImport)

        If tableToolStrip IsNot Nothing Then
            For Each c As ToolStripItem In tableToolStrip.Items
                Dim bExiststandard As Boolean = True
                Dim bEnabled As Boolean = PermissionMenu(c.Name, GridRowCount, UsedFind, CheckCloseBook, bPeriod13, per, bExiststandard, perImport)
                If bExiststandard Then c.Enabled = bEnabled
                'c.Enabled = PermissionMenu(c.Name, GridRowCount, UsedFind, CheckCloseBook, bPeriod13, per)
            Next

            'Update 27/04/2012: Kiểm tra xem có tồn tại nút Thực hiện không
            ' Kiểm tra danh sách menu con của menu Action (Thực hiện) 
            Dim tsdActive As ToolStripDropDownButton = CType(tableToolStrip.Items("tsdActive"), ToolStripDropDownButton)
            If tsdActive IsNot Nothing Then  ' Tồn tại nút Thực hiện
                For i As Integer = 0 To tsdActive.DropDownItems.Count - 1
                    With tsdActive.DropDownItems(i)
                        If TypeOf (tsdActive.DropDownItems(i)) Is ToolStripSeparator Then Continue For
                        'Append 05/10/2012
                        Dim bExiststandard As Boolean = True
                        Dim bEnabled As Boolean = PermissionMenu(.Name, GridRowCount, UsedFind, CheckCloseBook, bPeriod13, per, bExiststandard, perImport)
                        If bExiststandard Then .Enabled = bEnabled
                        ' .Enabled = PermissionMenu(.Name, GridRowCount, UsedFind, CheckCloseBook, bPeriod13, per)
                    End With
                Next
            End If
        End If
        'Update 19/07/2011:  Kiểm tra danh sách Menu con của menu ContextMenuStrip
        If ContextMenuStrip Is Nothing Then Exit Sub 'Không Tồn tại 
        For i As Integer = 0 To ContextMenuStrip.Items.Count - 1
            With ContextMenuStrip.Items(i)
                If TypeOf (ContextMenuStrip.Items(i)) Is ToolStripSeparator Then Continue For

                Dim bExiststandard As Boolean = True
                Dim bEnabled As Boolean = PermissionMenu(.Name, GridRowCount, UsedFind, CheckCloseBook, bPeriod13, per, bExiststandard, perImport)
                If bExiststandard Then .Enabled = bEnabled
                '.Enabled = PermissionMenu(.Name, GridRowCount, UsedFind, CheckCloseBook, bPeriod13, per)
            End With
        Next


    End Sub

    Public Sub CheckMenu(ByVal FormName As String, ByVal ContextMenuStrip As System.Windows.Forms.ContextMenuStrip, ByVal GridRowCount As Integer, ByVal UsedFind As Boolean, Optional ByVal FormPermissionImport As String = "")
        Dim per As Integer = ReturnPermission(FormName)
        Dim perImport As Integer = ReturnPermission(FormPermissionImport)

        'Update 19/07/2011:  Kiểm tra danh sách Menu con của menu ContextMenuStrip
        'If ContextMenuStrip Is Nothing Then Exit Sub 'Không Tồn tại 
        For i As Integer = 0 To ContextMenuStrip.Items.Count - 1
            With ContextMenuStrip.Items(i)
                If TypeOf (ContextMenuStrip.Items(i)) Is ToolStripSeparator Then Continue For
                'Append 05/10/2012
                Dim bExiststandard As Boolean = True
                Dim bEnabled As Boolean = PermissionMenu(.Name, GridRowCount, UsedFind, False, False, per, bExiststandard, perImport)
                If bExiststandard Then .Enabled = bEnabled
            End With
        Next

    End Sub

    Private Function PermissionMenu(ByVal sNameMenu As String, ByVal GridRowCount As Integer, ByVal UsedFind As Boolean, ByVal CheckCloseBook As Boolean, ByVal bPeriod13 As Boolean, ByVal per As Integer, ByRef bExiststandard As Boolean, ByVal perImport As Integer) As Boolean
        bExiststandard = True 'Mặc định có tồn tại menu chuẩn 'Append 05/10/2012
        Select Case sNameMenu
            Case "tsbView", "tsmView", "mnsView" : Return (per - 1 >= 0) And GridRowCount > 0
            Case "tsbAdd", "tsmAdd", "mnsAdd"
                If CheckCloseBook Then
                    Return (per - 2 >= 0) And Not gbClosed And bPeriod13
                Else
                    Return (per - 2 >= 0)
                End If
            Case "tsbInherit", "tsmInherit", "mnsInherit"
                If CheckCloseBook Then
                    Return (per - 2 >= 0) And Not gbClosed And bPeriod13 And GridRowCount > 0
                Else
                    Return (per - 2 >= 0) And GridRowCount > 0
                End If
            Case "tsbEdit", "tsmEdit", "mnsEdit"
                If CheckCloseBook Then
                    Return (per - 3 >= 0) And GridRowCount > 0 And Not gbClosed And bPeriod13
                Else
                    Return (per - 3 >= 0) And GridRowCount > 0
                End If
            Case "tsbEditOther", "tsmEditOther", "mnsEditOther"  ' update 2/12/2013 - Quyền sửa khác bằng quyền sửa
                If CheckCloseBook Then
                    Return (per - 3 >= 0) And GridRowCount > 0 And Not gbClosed And bPeriod13
                Else
                    Return (per - 3 >= 0) And GridRowCount > 0
                End If
            Case "tsbDelete", "tsmDelete", "mnsDelete"
                If CheckCloseBook Then
                    Return (per - 4 >= 0) And GridRowCount > 0 And Not gbClosed And bPeriod13
                Else
                    Return (per - 4 >= 0) And GridRowCount > 0
                End If
                'Update 06/05/2011: Khi chuẩn hóa xong menu theo dạng mới, cần Bỏ điều kiện GridRowCount > 0 cho Find và ListAll
            Case "tsbFind", "tsmFind", "mnsFind" : Return UsedFind Or GridRowCount > 0
            Case "tsbListAll", "tsmListAll", "mnsListAll" : Return UsedFind Or GridRowCount > 0
            Case "tsbSysInfo", "tsmSysInfo", "mnsSysInfo" : Return GridRowCount > 0
            Case "tsbPrint", "tsmPrint", "mnsPrint" : Return GridRowCount > 0
            Case "tsbLocked", "tsmLocked", "mnsLocked", "tsbLockVoucher", "tsmLockVoucher", "mnsLockVoucher" 'Khóa phiếu
                If CheckCloseBook Then
                    Return GridRowCount > 0 And Not gbClosed
                Else
                    Return GridRowCount > 0
                End If
            Case "tsbShowDetail", "tsmShowDetail", "mnsShowDetail" : Return GridRowCount > 0
                'Update 05/03/2010
            Case "tsbExportToExcel", "tsmExportToExcel", "mnsExportToExcel" : Return GridRowCount > 0
                ' update 1/3/2013
            Case "tsmExportOut", "mnsExportOut" : Return GridRowCount > 0
            Case "tsmExportDataScript", "mnsExportDataScript" : Return GridRowCount > 0
                'Update 19/03/2013
            Case "tsbImportData", "tsmImportData", "mnsImportData"
                If CheckCloseBook Then
                    Return (perImport - 2 >= 0) And Not gbClosed And bPeriod13
                Else
                    Return (perImport - 2 >= 0)
                End If
            Case Else
                bExiststandard = False 'Append 05/10/2012
        End Select
        'Return True'Cancel 02/10/2012
    End Function

    ''' <summary>
    ''' Trả về là quyền của màn hình truyền vào
    ''' </summary>
    ''' <param name="FormName">Màn hình cần lấy quyền</param>
    Public Function ReturnPermission(ByVal FormName As String) As Integer
        Try
            If dtPer Is Nothing Or sMyModuleID <> FormName.Substring(0, 3) Then
                Dim sConnectionStringLEMONSYS As String = "Data Source=" & gsServer & ";Initial Catalog=LEMONSYS;User ID=" & gsConnectionUser & ";Password=" & gsPassword & ";Connect Timeout = 0"
                Dim sSQL As String = ""
                sSQL &= "Use LEMONSYS" & vbCrLf ''Minh Hòa Update 10/08/2012: chuyển về chung dataset để xử lý việc rớt mạng
                sSQL &= "Select ScreenID, Permission From D00V0001"
                sSQL &= " Where "
                sSQL &= "UserID = " & SQLString(gsUserID) & " And "
                sSQL &= "CompanyID = " & SQLString(gsCompanyID) & " And "
                sSQL &= "ModuleID = " & SQLString(FormName.Substring(0, 3))
                sSQL &= " Order by ScreenID"

                'Minh Hòa Update 10/08/2012: chuyển về chung dataset để xử lý việc rớt mạng
                Dim ds As DataSet = ReturnDataSet(sSQL)
                dtPer = ds.Tables(0)
                sMyModuleID = FormName.Substring(0, 3)


                '                Dim conn As SqlConnection = New SqlConnection(sConnectionStringLEMONSYS)
                '                Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
                '                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                '                Dim ds As DataSet = New DataSet()
                '                Try
                '                    conn.Open()
                '                    cmd.CommandTimeout = 0
                '                    da.Fill(ds)
                '                    conn.Close()
                '                    dtPer = ds.Tables(0)
                '
                '                    sMyModuleID = FormName.Substring(0, 3)
                '                Catch
                '                    conn.Close()
                '                    Clipboard.Clear()
                '                    Clipboard.SetText(sSQL)
                '                    MsgErr("Error when excute SQL in function ReturnDataSet(). Paste your SQL code from Clipboard")
                '                    Return Nothing
                '                End Try
            End If

            If dtPer Is Nothing OrElse dtPer.Rows.Count <= 0 Then
                Return -1
            Else
                Dim iRowFind As Integer = -1
                iRowFind = ReturnRowTable(dtPer, "ScreenID", FormName)
                If iRowFind >= 0 Then
                    Return CInt(dtPer.Rows(iRowFind).Item("Permission"))
                Else
                    Return -1
                End If

            End If

        Catch
            Return -1
        End Try
    End Function

#End Region


#Region "Khoản mục"

    ''' <summary>
    ''' Bỏ không dùng nữa: Kiểm tra khoản mục theo D91
    ''' </summary>
    ''' <param name="c1Grid">Lưới cần kiểm tra</param>
    ''' <param name="COL_Ana01ID">Cột Ana01ID của lưới</param>
    ''' <param name="Split">Kiểm tra ở split thứ mấy</param>
    ''' <param name="tdbdAna01ID">Truyền Dropdown tdbdAna01ID</param>
    ''' <param name="tdbdAna02ID">Truyền Dropdown tdbdAna02ID</param>
    ''' <param name="tdbdAna03ID">Truyền Dropdown tdbdAna03ID</param>
    ''' <param name="tdbdAna04ID">Truyền Dropdown tdbdAna04ID</param>
    ''' <param name="tdbdAna05ID">Truyền Dropdown tdbdAna05ID</param>
    ''' <param name="tdbdAna06ID">Truyền Dropdown tdbdAna06ID</param>
    ''' <param name="tdbdAna07ID">Truyền Dropdown tdbdAna07ID</param>
    ''' <param name="tdbdAna08ID">Truyền Dropdown tdbdAna08ID</param>
    ''' <param name="tdbdAna09ID">Truyền Dropdown tdbdAna09ID</param>
    ''' <param name="tdbdAna10ID">Truyền Dropdown tdbdAna10ID</param>
    ''' <returns>Trả kết quả về True nếu kiểm tra 1 trong 10 khoản mục bị lỗi theo D91</returns>
    ''' <remarks>
    ''' <code>
    ''' If CheckAnaError(tdbg, COL_Ana01ID, SPLIT2, tdbdAna01ID, tdbdAna02ID, tdbdAna03ID, tdbdAna04ID, tdbdAna05ID, tdbdAna06ID, tdbdAna07ID, tdbdAna08ID, tdbdAna09ID, tdbdAna10ID) Then
    '''    Return False
    ''' End If
    ''' </code>
    ''' </remarks>
    <DebuggerStepThrough()> _
    Public Function CheckAnaError(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Ana01ID As Integer, ByVal Split As Integer, ByVal tdbdAna01ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna02ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna03ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna04ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna05ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna06ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna07ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna08ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna09ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna10ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown) As Boolean
        Dim listTBDD As New System.Collections.Generic.List(Of C1.Win.C1TrueDBGrid.C1TrueDBDropdown)
        listTBDD.Add(tdbdAna01ID) : listTBDD.Add(tdbdAna02ID) : listTBDD.Add(tdbdAna03ID) : listTBDD.Add(tdbdAna04ID) : listTBDD.Add(tdbdAna05ID) : listTBDD.Add(tdbdAna06ID) : listTBDD.Add(tdbdAna07ID) : listTBDD.Add(tdbdAna08ID) : listTBDD.Add(tdbdAna09ID) : listTBDD.Add(tdbdAna10ID)
        For i As Integer = 0 To c1Grid.RowCount - 1
            For j As Integer = COL_Ana01ID To COL_Ana01ID + 9
                If c1Grid(i, j).ToString <> "" Then
                    If gbArrAnaValidate(j - COL_Ana01ID) Then 'Kiểm tra trong danh sách
                        Dim TDBD As C1.Win.C1TrueDBGrid.C1TrueDBDropdown = listTBDD.Item(j - COL_Ana01ID)
                        Dim dt As DataTable = CType(TDBD.DataSource, DataTable)
                        Dim dr() As DataRow = dt.Select("AnaID = " & SQLString(c1Grid(i, j)))
                        If dr.Length = 0 Then 'Không có trong danh sách
                            D99C0008.MsgInList()
                            c1Grid.Focus()
                            c1Grid.Bookmark = i
                            c1Grid.Col = j
                            c1Grid.SplitIndex = Split
                            Return True
                        End If
                    Else 'Kiểm tra theo độ dài, không kiểm tra trong danh sách
                        If c1Grid(i, j).ToString.Length > giArrAnaLength(j - COL_Ana01ID) Then
                            If geLanguage = EnumLanguage.Vietnamese Then
                                D99C0008.MsgL3("Chiều dài khoản mục vượt giá trị cho phép")
                            Else
                                D99C0008.MsgL3("Lenght of Ana over limit value")
                            End If

                            c1Grid.Focus()
                            c1Grid.Bookmark = i
                            c1Grid.Col = j
                            c1Grid.SplitIndex = Split
                            Return True
                        End If
                    End If
                End If
            Next
        Next
        Return False
    End Function

    '''' <summary>
    '''' Hàm mới: Đổ nguồn cho 10 khoản mục kiểm tra KM nào có dùng thì đổ nguồn cho Dropdown
    '''' </summary>
    '''' <remarks>Truyền 10 khoản mục cần đổ nguồn vào</remarks>
    '<DebuggerStepThrough()> _
    Public Sub LoadTDBDropDownAna(ByVal tdbdAna01ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna02ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna03ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna04ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna05ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna06ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna07ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna08ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna09ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna10ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, _
  ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Ana01ID As Integer, Optional ByVal bUseUnicode As Boolean = False, Optional ByVal bAddNew As Boolean = False, Optional ByRef dt As DataTable = Nothing)
        If dt Is Nothing Then dt = ReturnTableAnaID(bAddNew, bUseUnicode)

        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID).Tag) Then LoadDataSource(tdbdAna01ID, ReturnTableFilter(dt, "AnaCategoryID='K01' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 1).Tag) Then LoadDataSource(tdbdAna02ID, ReturnTableFilter(dt, "AnaCategoryID='K02' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 2).Tag) Then LoadDataSource(tdbdAna03ID, ReturnTableFilter(dt, "AnaCategoryID='K03' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 3).Tag) Then LoadDataSource(tdbdAna04ID, ReturnTableFilter(dt, "AnaCategoryID='K04' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 4).Tag) Then LoadDataSource(tdbdAna05ID, ReturnTableFilter(dt, "AnaCategoryID='K05' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 5).Tag) Then LoadDataSource(tdbdAna06ID, ReturnTableFilter(dt, "AnaCategoryID='K06' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 6).Tag) Then LoadDataSource(tdbdAna07ID, ReturnTableFilter(dt, "AnaCategoryID='K07' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 7).Tag) Then LoadDataSource(tdbdAna08ID, ReturnTableFilter(dt, "AnaCategoryID='K08' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 8).Tag) Then LoadDataSource(tdbdAna09ID, ReturnTableFilter(dt, "AnaCategoryID='K09' or AnaCategoryID='+'"), bUseUnicode)
        If Convert.ToBoolean(tdbg.Columns(COL_Ana01ID + 9).Tag) Then LoadDataSource(tdbdAna10ID, ReturnTableFilter(dt, "AnaCategoryID='K10' or AnaCategoryID='+'"), bUseUnicode)
    End Sub

    Public Function ReturnTableAnaID(Optional ByVal bAddnew As Boolean = False, Optional ByVal bUnicode As Boolean = False, Optional ByVal sAnaCategoryID As String = "") As DataTable
        Dim sSQL As String = "--Do nguon Khoan muc " & vbCrLf
        If bAddnew Then
            sSQL &= "Select '+' as AnaID, " & NewName & " As AnaName, '+' as AnaCategoryID, 0 AS DisplayOrder " & vbCrLf
            sSQL &= "Union All " & vbCrLf
        End If
        sSQL &= "Select AnaID, AnaName" & UnicodeJoin(bUnicode) & " as AnaName, AnaCategoryID, 1 AS DisplayOrder " & vbCrLf
        sSQL &= "From D91T0051 WITH(NOLOCK) Where Disabled = 0 " & vbCrLf
        If sAnaCategoryID = "" Then
            sSQL &= "And AnaCategoryID like 'K%' " & vbCrLf
        Else
            sSQL &= "And AnaCategoryID = " & SQLString(sAnaCategoryID) & vbCrLf
        End If
        sSQL &= " Order by DisplayOrder, AnaID"
        Return ReturnDataTable(sSQL)
    End Function

    '''' <summary>
    '''' Đổ nguồn cho 10 khoản mục
    '''' </summary>
    '''' <remarks>Truyền 10 khoản mục cần đổ nguồn vào</remarks>
    '<DebuggerStepThrough()> _
    Public Sub LoadTDBDropDownAna(ByVal tdbdAna01ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna02ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna03ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna04ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna05ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna06ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna07ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna08ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna09ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal tdbdAna10ID As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, Optional ByVal bUseUnicode As Boolean = False)
        Dim dt As DataTable
        Dim sSQL As String = ""

        sSQL = "Select AnaID, AnaName" & UnicodeJoin(bUseUnicode) & " as AnaName, AnaCategoryID " & vbCrLf
        sSQL &= "From D91T0051 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where Disabled = 0 And AnaCategoryID like 'K%' " & vbCrLf
        sSQL &= "Order by AnaID"
        dt = ReturnDataTable(sSQL)

        LoadDataSource(tdbdAna01ID, ReturnTableFilter(dt, "AnaCategoryID='K01'"), bUseUnicode)
        LoadDataSource(tdbdAna02ID, ReturnTableFilter(dt, "AnaCategoryID='K02'"), bUseUnicode)
        LoadDataSource(tdbdAna03ID, ReturnTableFilter(dt, "AnaCategoryID='K03'"), bUseUnicode)
        LoadDataSource(tdbdAna04ID, ReturnTableFilter(dt, "AnaCategoryID='K04'"), bUseUnicode)
        LoadDataSource(tdbdAna05ID, ReturnTableFilter(dt, "AnaCategoryID='K05'"), bUseUnicode)
        LoadDataSource(tdbdAna06ID, ReturnTableFilter(dt, "AnaCategoryID='K06'"), bUseUnicode)
        LoadDataSource(tdbdAna07ID, ReturnTableFilter(dt, "AnaCategoryID='K07'"), bUseUnicode)
        LoadDataSource(tdbdAna08ID, ReturnTableFilter(dt, "AnaCategoryID='K08'"), bUseUnicode)
        LoadDataSource(tdbdAna09ID, ReturnTableFilter(dt, "AnaCategoryID='K09'"), bUseUnicode)
        LoadDataSource(tdbdAna10ID, ReturnTableFilter(dt, "AnaCategoryID='K10'"), bUseUnicode)
    End Sub

    Public Function ReturnTableAnaCaption(ByVal ModuleID As String, Optional ByVal bUseUnicode As Boolean = False) As DataTable
        Dim sSQL As String = "Select AnaCategoryID, AnaCategoryShort" & UnicodeJoin(bUseUnicode) & " as AnaCategoryShort, AnaCategoryName" & UnicodeJoin(bUseUnicode) & " As AnaCategoryName, AnaCategoryLength, AnaCategoryValidate, AnaCategoryStatus, Use" & ModuleID & " as UseModule From D91T0050 WITH(NOLOCK) Where System = 1  And AnaTypeID = 'K' Order by AnaCategoryID"
        Return ReturnDataTable(sSQL)
    End Function

    ''' <summary>
    ''' Gán caption của 10 Khoản mục cho lưới
    ''' </summary>
    ''' <param name="tdbg">Lưới cần gán caption</param>
    ''' <param name="COL_Ana01ID">Cột bắt đầu</param>
    ''' <param name="Split">Split cần gán</param>
    ''' <param name="IsVisibleColumn"> = True: nếu lưới không có nút Khoản mục </param>
    ''' <remarks>Columns.Tag chứa biến Boolean chỉ cột này có hiện thị không</remarks>
    <DebuggerStepThrough()> _
    Public Function LoadTDBGridAnalysisCaption(ByVal ModuleID As String, ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Ana01ID As Integer, ByVal Split As Integer, Optional ByVal IsVisibleColumn As Boolean = False, Optional ByVal bUseUnicode As Boolean = False, Optional ByRef dt As DataTable = Nothing) As Boolean
        'Dim sSQL As String = "Select AnaCategoryID, AnaCategoryShort" & IIf(bUseUnicode, "U", "").ToString & " as AnaCategoryShort, AnaCategoryName" & UnicodeJoin(gbUnicode) & " As AnaCategoryName, AnaCategoryLength, AnaCategoryValidate, AnaCategoryStatus, Use" & ModuleID & " as UseModule From D91T0050 WITH(NOLOCK) Where System = 1  And AnaTypeID = 'K' Order by AnaCategoryID"
        If dt Is Nothing Then dt = ReturnTableAnaCaption(ModuleID, bUseUnicode)
        Dim bUseAna As Boolean = False

        Dim iIndex As Integer = COL_Ana01ID
        Dim i As Integer
        If dt.Rows.Count > 0 Then
            For i = 0 To 9
                tdbg.Columns(iIndex).Caption = dt.Rows(i).Item("AnaCategoryShort").ToString
                tdbg.Columns(iIndex).Tag = Convert.ToBoolean(dt.Rows(i).Item("UseModule")) And Convert.ToBoolean(dt.Rows(i).Item("AnaCategoryStatus"))
                'Modify update: 31/03/2009
                gbArrAnaVisiable(iIndex - COL_Ana01ID) = Convert.ToBoolean(tdbg.Columns(iIndex).Tag)
                If Not bUseAna And Convert.ToBoolean(tdbg.Columns(iIndex).Tag) = True Then
                    bUseAna = True
                End If
                Dim fontStyle As FontStyle = tdbg.Splits(Split).DisplayColumns(iIndex).HeadingStyle.Font.Style
                tdbg.Splits(Split).DisplayColumns(iIndex).HeadingStyle.Font = FontUnicode(bUseUnicode, fontStyle) 'New System.Drawing.Font("Lemon3", 8.249999!)
                'AnaCategoryValidate= True --> Nhập trong danh sách --> Không kiểm tra chiều dài
                'AnaCategoryValidate= False --> Nhập ngoài danh sách --> Kiểm tra chiều dài
                gbArrAnaValidate(iIndex - COL_Ana01ID) = Convert.ToBoolean(dt.Rows(i).Item("AnaCategoryValidate"))
                giArrAnaLength(iIndex - COL_Ana01ID) = Convert.ToInt16(dt.Rows(i).Item("AnaCategoryLength"))
                gsArrAnaCategoryName(i) = dt.Rows(i).Item("AnaCategoryName").ToString
                If IsVisibleColumn Then 'Lưới không có nút thì hiển thị cột KM
                    tdbg.Splits(Split).DisplayColumns(iIndex).Visible = Convert.ToBoolean(tdbg.Columns(iIndex).Tag)
                End If

                iIndex += 1
            Next
        End If
        ' dt = Nothing

        Return bUseAna
    End Function
#End Region

#Region "Thông tin phụ"

    Public Function LoadCaptionInfo(ByVal dt As DataTable, ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iSplit As Integer, Optional ByVal bVisible As Boolean = True) As String
        Dim iDisplayRefCol As String = "" 'không hiển thị
        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim sFieldName As String = dt.Rows(i).Item("FieldName").ToString
                Try
                    If tdbg.Columns.IndexOf(tdbg.Columns(sFieldName)) > -1 Then
                        'Kiểm tra tồn tại cột trên lưới
                        tdbg.Splits(iSplit).DisplayColumns(sFieldName).HeadingStyle.Font = FontUnicode(gbUnicode)
                        tdbg.Columns(sFieldName).Caption = dt.Rows(i).Item("Caption" & gsLanguage).ToString
                        If bVisible Then tdbg.Splits(iSplit).DisplayColumns(sFieldName).Visible = L3Bool(dt.Rows(i).Item("DefaultUse"))
                        tdbg.Columns(sFieldName).Tag = L3Bool(dt.Rows(i).Item("DefaultUse"))
                        If L3Bool(dt.Rows(i).Item("DefaultUse")) Then tdbg.Columns(sFieldName).DefaultValue = dt.Rows(i).Item("DefaultValue").ToString
                        If iDisplayRefCol = "" And L3Bool(dt.Rows(i).Item("DefaultUse")) Then iDisplayRefCol = sFieldName
                        Select Case L3Int(dt.Rows(i).Item("DataType"))
                            Case 0 'Số
                                tdbg.Columns(sFieldName).NumberFormat = "N" & L3Int(dt.Rows(i).Item("DecimalNum"))

                            Case 1 'Chuỗi
                                tdbg.Columns(sFieldName).DataWidth = L3Int(dt.Rows(i).Item("DecimalNum"))
                            Case 2 'Ngày
                        End Select
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            dt.Dispose()
        Catch ex As Exception
            D99C0008.MsgL3(ex.Message)
        End Try
        Return iDisplayRefCol
    End Function
    ''' <summary>
    ''' Load tiêu đề Thông tin phụ
    ''' </summary>
    ''' <param name="sSQL">Câu đổ nguồn</param>
    ''' <param name="tdbg"></param>
    ''' <param name="iSplit"></param>
    ''' <returns>Cột hiển thị đầu tiên dạng chuỗi</returns>
    ''' <remarks></remarks>
    Public Function LoadCaptionInfo(ByVal sSQL As String, ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iSplit As Integer, Optional ByVal bVisible As Boolean = True) As String
        ' Dim sSQL As String = "Select Caption01" & UnicodeJoin(gbUnicode) & " as Caption01" & ", Caption84" & UnicodeJoin(gbUnicode) & " as Caption84" & ", DefaultValue" & UnicodeJoin(gbUnicode) & " as DefaultValue, DefaultUse, DataType, DecimalNum, FieldName  From D07N0037('D??T????')  Order By DataType, DisplayOrder"
        Dim dt As DataTable = ReturnDataTable(sSQL)
        Return LoadCaptionInfo(dt, tdbg, iSplit, bVisible)
    End Function

    ''' <summary>
    ''' Load thông tin phụ trên master
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="pnl">panel chứa các control thông tin phụ</param>
    ''' <param name="bShow3dot">Nếu caption >10 thì hiển thị thêm ...</param>
    ''' <remarks></remarks>
    Public Sub LoadCaptionInfo(ByVal dt As DataTable, ByVal pnl As Control, Optional ByVal bShow3dot As Boolean = False)
        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim sFieldName As String = dt.Rows(i).Item("FieldName").ToString
                Try
                    Dim lbl As Label = CType(pnl.Controls("lbl" & sFieldName), Label)
                    If lbl IsNot Nothing Then 'Bị lỗi tên Field của label
                        lbl.Text = dt.Rows(i).Item("Caption" & gsLanguage).ToString
                        lbl.Font = FontUnicode(gbUnicode, pnl.Controls("lbl" & sFieldName).Font.Style)
                        If Len(lbl.Text) > 10 And bShow3dot Then lbl.Text = Microsoft.VisualBasic.Left(lbl.Text, 10) & "..."
                    End If
                    Select Case L3Int(dt.Rows(i).Item("DataType"))
                        Case 0 'Số
                            Dim txtNumber As TextBox = CType(pnl.Controls("txt" & sFieldName), TextBox)
                            If txtNumber Is Nothing Then
                                Dim cneNumber As C1.Win.C1Input.C1NumericEdit = CType(pnl.Controls("cne" & sFieldName), C1.Win.C1Input.C1NumericEdit)
                                cneNumber.Tag = L3Int(dt.Rows(i).Item("DecimalNum"))
                                cneNumber.Enabled = L3Bool(dt.Rows(i).Item("DefaultUse"))
                                If cneNumber.Enabled Then cneNumber.Value = dt.Rows(i).Item("DefaultValue")
                            Else 'dùng textbox nhập số
                                txtNumber.Tag = L3Int(dt.Rows(i).Item("DecimalNum"))
                                txtNumber.Enabled = L3Bool(dt.Rows(i).Item("DefaultUse"))
                                If txtNumber.Enabled Then txtNumber.Text = dt.Rows(i).Item("DefaultValue").ToString
                            End If
                        Case 1 'Chuỗi
                            Dim txtNumber As TextBox = CType(pnl.Controls("txt" & sFieldName), TextBox)
                            txtNumber.MaxLength = L3Int(dt.Rows(i).Item("DecimalNum"))
                            txtNumber.Enabled = L3Bool(dt.Rows(i).Item("DefaultUse"))
                            If txtNumber.MaxLength = 0 And txtNumber.Enabled Then ReadOnlyControl(txtNumber)
                            If txtNumber.Enabled Then txtNumber.Text = dt.Rows(i).Item("DefaultValue").ToString
                        Case 2 'Ngày
                            Dim c1date As C1.Win.C1Input.C1DateEdit = CType(pnl.Controls("c1date" & sFieldName), C1.Win.C1Input.C1DateEdit)
                            c1date.Enabled = L3Bool(dt.Rows(i).Item("DefaultUse"))
                            If c1date.Enabled Then c1date.Value = dt.Rows(i).Item("DefaultValue").ToString
                    End Select
                Catch ex As Exception
                    Continue For
                End Try
            Next
            dt.Dispose()
        Catch ex As Exception
            D99C0008.MsgL3(ex.Message)
        End Try
    End Sub

    Public Sub LoadCaptionInfo(ByVal sSQL As String, ByVal pnl As Control, Optional ByVal bShow3dot As Boolean = False)
        Dim dt As DataTable = ReturnDataTable(sSQL)
        LoadCaptionInfo(dt, pnl, bShow3dot)
    End Sub
#End Region

#Region "Mã phân tích"
    Public Function LoadCaptionICode(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iSplit As Integer, ByVal COL_ICode01ID As String, Optional ByVal bVisible As Boolean = True) As String
        Dim sSQL As String = "Select TypeCodeID, Caption" & UnicodeJoin(gbUnicode) & " as Caption, Disabled" & vbCrLf
        sSQL &= " From D07T0033 WITH(NOLOCK) Order by TypeCodeID "
        Dim dt As DataTable = ReturnDataTable(sSQL)

        Dim iDisplayICode As String = "" 'không hiển thị
        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim sFieldName As String = COL_ICode01ID.Replace("01", dt.Rows(i).Item("TypeCodeID").ToString)
                Try
                    If tdbg.Columns.IndexOf(tdbg.Columns(sFieldName)) > -1 Then 'Tồn tại cột ICodexxID
                        'Kiểm tra tồn tại cột trên lưới
                        tdbg.Splits(iSplit).DisplayColumns(sFieldName).HeadingStyle.Font = FontUnicode(gbUnicode)
                        tdbg.Columns(sFieldName).Caption = dt.Rows(i).Item("Caption").ToString
                        tdbg.Columns(sFieldName).Tag = Not L3Bool(dt.Rows(i).Item("Disabled"))
                        If bVisible Then tdbg.Splits(iSplit).DisplayColumns(sFieldName).Visible = L3Bool(tdbg.Columns(sFieldName).Tag)
                        If iDisplayICode = "" And L3Bool(tdbg.Columns(sFieldName).Tag) Then iDisplayICode = sFieldName
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
            dt.Dispose()
        Catch ex As Exception
            D99C0008.MsgL3(ex.Message)
        End Try
        Return iDisplayICode
    End Function
#End Region

#Region "Linh tinh"

#Region "Search value in Array"
    Dim sValue As String = ""
    Private Function ContainsValue(ByVal s As String) As Boolean
        'AndAlso prevents evaluation of the second Boolean
        'expression if the string is so short that an error
        'would occur.
        'Return s.Contains(sValue)
        Return s.Equals(sValue)

    End Function

    Public Function L3FindInteger(ByVal arrCol() As Integer, ByVal iValue As Integer) As Boolean
        For i As Integer = 0 To arrCol.Length - 1
            If CInt(arrCol(i)) = iValue Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function L3FindArrString(ByVal ArrString() As String, ByVal sValueFind As String) As Boolean
        sValue = sValueFind
        If Array.Exists(ArrString, AddressOf ContainsValue) Then
            Return True
        End If

        Return False
    End Function

    Dim iValue As Integer
    Private Function ContainsValueInt(ByVal s As Object) As Boolean
        'AndAlso prevents evaluation of the second Boolean
        'expression if the string is so short that an error
        'would occur.
        'Return s.Contains(sValue)
        Return s.Equals(iValue)

    End Function

    Private Function ContainsValue(ByVal s As Object) As Boolean
        'AndAlso prevents evaluation of the second Boolean
        'expression if the string is so short that an error
        'would occur.
        'Return s.Contains(sValue)
        Return s.Equals(sValue)

    End Function

    Public Function L3FindArr(ByVal ArrString() As Object, ByVal sValueFind As String) As Boolean
        If ArrString Is Nothing Then Return False

        sValue = sValueFind
        If Array.Exists(ArrString, AddressOf ContainsValue) Then
            Return True
        End If

        Return False
    End Function

    Public Function L3FindArr(ByVal ArrString() As Object, ByVal sValueFind As Integer) As Boolean
        If ArrString Is Nothing Then Return False

        iValue = sValueFind
        If Array.Exists(ArrString, AddressOf ContainsValueInt) Then
            Return True
        End If

        Return False
    End Function
#End Region

    Public Function IndexOfColumn(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal sField As String) As Integer
        Try
            Return tdbg.Columns.IndexOf(tdbg.Columns(sField))
        Catch ex As Exception
            Return -1 'Không tồn tại field
        End Try
    End Function

    ''' <summary>
    ''' Ghi nhận Audit khi tạo kỳ 
    ''' </summary>
    ''' <param name="sModuleID">ModuleID: Truyền Dxx</param>
    ''' <param name="sNewPeriod">Kỳ mới được tạo</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Sub RunAuditLogNewPeriod(ByVal sModuleID As String, ByVal sNewPeriod As String)
        Dim sSQL As String = ""

        sSQL &= "Exec D91P9106 "
        sSQL &= SQLDateTimeSave(Now) & COMMA 'AuditDate, datetime, NOT NULL
        sSQL &= SQLString("NewPeriod" & sModuleID) & COMMA 'AuditCode, varchar[20], NOT NULL
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLString(sModuleID.Substring(1, 2)) & COMMA 'ModuleID, varchar[2], NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
        sSQL &= SQLString("01") & COMMA 'EventID, varchar[20], NOT NULL
        sSQL &= SQLString(rl3("Tao_ky_V") & ": " & sNewPeriod) & COMMA 'Desc1, varchar[250], NOT NULL
        sSQL &= SQLString("Module " & sModuleID) & COMMA 'Desc2, varchar[250], NOT NULL
        sSQL &= SQLString("") & COMMA 'Desc3, varchar[250], NOT NULL
        sSQL &= SQLString("") & COMMA 'Desc4, varchar[250], NOT NULL
        sSQL &= SQLString("") & COMMA 'Desc5, varchar[250], NOT NULL
        sSQL &= SQLNumber(0) & COMMA 'IsAuditDetail, varchar[250], NOT NULL
        sSQL &= SQLString("")  'AuditItemID, varchar[250], NOT NULL

        'sSQL = SQLStoreD91P9106("NewPeriod", "01", sModuleID.Substring(1, 2), rl3("Tao_ky_V") & ": " & sNewPeriod, "Module " & sModuleID, , , , , )
        ExecuteSQLNoTransaction(sSQL)
    End Sub

    ''' <summary>
    ''' Lấy giá trị hàm GetDate()của Server để truyền vào CreateDate cho trường hợp lưu chi tiết dưới lưới
    ''' </summary>
    ''' <remarks>Gọi tại sự kiện nút Lưu, trước khi tạo câu SQL để Insert</remarks>
    <DebuggerStepThrough()> _
    Public Sub GetDateServer()
        Dim sSQL As String
        sSQL = "Select Getdate() as DateServer "
        gsGetDate = ReturnScalar(sSQL)
    End Sub

    ''' <summary>
    ''' Message hỏi trước khi đóng màn hình có nút Lưu
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Vào sự kiện FormClosing viết đoạn code sau </remarks>
    ''' If Not bKeyPress Then Exit Sub' Gán bKeyPress = True ở sự kiện Form_KeyPress
    ''' If _FormState = EnumFormState.FormEdit Then 
    '''If Not gbSavedOK Then 
    '''If Not AskMsgBeforeClose() Then e.Cancel = True : Exit Sub 
    '''End If 
    '''ElseIf _FormState = EnumFormState.FormAdd Then 
    '''If btnSave.Enabled Then 
    '''If Not AskMsgBeforeClose() Then e.Cancel = True : Exit Sub 
    '''End If 
    '''End If 
    <DebuggerStepThrough()> _
    Public Function AskMsgBeforeClose() As Boolean
        Dim bResult As Boolean = False
        If geLanguage = EnumLanguage.Vietnamese Then
            bResult = CBool(IIf(D99C0008.MsgAsk("Dữ liệu chưa được lưu. Bạn có muốn đóng không?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes, True, False))
        Else
            bResult = CBool(IIf(D99C0008.MsgAsk("Data has not been saved. Do you want to close?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes, True, False))
        End If
        Return bResult
    End Function

    ' Message hỏi trước khi di chuyển sang dòng khac ở màn hình có nút Lưu
    Public Function AskMsgBeforeRowChange() As Boolean
        Dim bResult As Boolean = False
        bResult = L3Bool(IIf(D99C0008.MsgAsk(rL3("Du_lieu_chua_duoc_luu") & Space(1) & rL3("MSG000028"), MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes, True, False))
        Return bResult
    End Function


    ''' <summary>
    ''' Trả về số file Ghi chú
    ''' </summary>
    ''' <param name="TableName">Bảng của nghiệp vụ cần Ghi chú</param>
    ''' <param name="Key1ID">Giá trị của Khóa chính trong bảng (IGE)</param>
    ''' <param name="Key2ID">Giá trị của Khóa thứ 2</param>
    ''' <param name="Key3ID">Khóa thứ 3</param>
    ''' <param name="Key4ID">Khóa thứ 4</param>
    ''' <param name="Key5ID">Khóa thứ 5</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnNotesNumber(ByVal TableName As String, ByVal Key1ID As String, Optional ByVal Key2ID As String = "", Optional ByVal Key3ID As String = "", Optional ByVal Key4ID As String = "", Optional ByVal Key5ID As String = "", Optional ByVal sDivisionID As String = "") As Integer
        'Modifydate: 26/10/2007: có thể ghi chú cho mỗi Đơn vị khác nhau 
        If sDivisionID = "" Then sDivisionID = gsDivisionID

        Dim sSQL As String = ""
        sSQL &= "Exec D91P2010 "
        sSQL &= SQLString(sDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLString(TableName) & COMMA 'TableName, varchar[20], NOT NULL
        sSQL &= SQLString(Key1ID) & COMMA 'Key1ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key2ID) & COMMA 'Key2ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key3ID) & COMMA 'Key3ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key4ID) & COMMA 'Key4ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key5ID) 'Key5ID, varchar[20], NOT NULL

        Dim dt As DataTable = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Return CInt(dt.Rows(0).Item("Count"))
        End If

        Return 0
    End Function

    ''' <summary>
    ''' Trả về số file Đính kèm
    ''' </summary>
    ''' <param name="TableName">Bảng của nghiệp vụ cần Đính kèm</param>
    ''' <param name="Key1ID">Giá trị của Khóa chính trong bảng (IGE)</param>
    ''' <param name="Key2ID">Giá trị của Khóa thứ 2</param>
    ''' <param name="Key3ID">Khóa thứ 3</param>
    ''' <param name="Key4ID">Khóa thứ 4</param>
    ''' <param name="Key5ID">Khóa thứ 5</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function ReturnAttachmentNumber(ByVal TableName As String, ByVal Key1ID As String, Optional ByVal Key2ID As String = "", Optional ByVal Key3ID As String = "", Optional ByVal Key4ID As String = "", Optional ByVal Key5ID As String = "", Optional ByVal sDivisionID As String = "") As Integer
        'Modifydate: 26/10/2007: có thể ghi chú cho mỗi Đơn vị khác nhau 
        If sDivisionID = "" Then sDivisionID = gsDivisionID

        Dim sSQL As String = ""
        sSQL &= "Exec D91P1010 "
        sSQL &= SQLString(sDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLString(TableName) & COMMA 'TableName, varchar[20], NOT NULL
        sSQL &= SQLString(Key1ID) & COMMA 'Key1ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key2ID) & COMMA 'Key2ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key3ID) & COMMA 'Key3ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key4ID) & COMMA 'Key4ID, varchar[20], NOT NULL
        sSQL &= SQLString(Key5ID) 'Key5ID, varchar[20], NOT NULL

        Dim dt As DataTable = ReturnDataTable(sSQL)

        If dt.Rows.Count > 0 Then
            Return CInt(dt.Rows(0).Item("Count"))
        End If

        Return 0
    End Function

    ''' <summary>
    ''' Hiển thị báo lỗi trong quá trình coding
    ''' </summary>
    ''' <param name="Text">Chuỗi báo lỗi</param>
    '<DebuggerStepThrough()> _
    Public Sub MsgErr(ByVal Text As String)
        'If geLanguage = EnumLanguage.Vietnamese Then
        '    MessageBox.Show(Text, "Læi", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        'Else
        '    MessageBox.Show(Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        'End If
        D99C0008.Msg(ConvertVietwareFToUnicode(Text), rL3("Loi"), L3MessageBoxButtons.OK, L3MessageBoxIcon.Err)
    End Sub

    ''' <summary>
    ''' Thêm các số 0 vào chuỗi format
    ''' </summary>
    ''' <param name="NumZero">Số cần tạo</param>
    ''' <returns>Các số không tương ứng và dấu chấm (nếu có)</returns>
    <DebuggerStepThrough()> _
    Public Function InsertZero(ByVal NumZero As Integer) As String
        Dim sRet As String = ""
        If NumZero = 0 Then
            sRet = ""
        Else
            sRet = "."
            For i As Integer = 0 To NumZero - 1
                sRet = sRet & "0"
            Next i
        End If
        Return sRet
    End Function



    ''' <summary>
    ''' Trả về chuỗi nối Tìm kiếm + Filter Server để In
    ''' </summary>
    ''' <param name="sFindServer">sFind</param>
    ''' <param name="sFilterServer">sFilterServer</param>
    ''' <returns>sFind + sFilterServer</returns>
    ''' <remarks></remarks>
    Public Function GetFindServerToPrint(ByVal sFindServer As String, ByVal sFilterServer As System.Text.StringBuilder) As String
        Dim strFind As String = sFindServer
        If sFilterServer.ToString.Equals("") = False And strFind.Equals("") = False Then strFind &= " And "
        strFind &= sFilterServer.ToString

        Return strFind
    End Function

    Public Function GetIPAddress(ByVal sServerName As String) As String
        'Lấy tên máy đang chạy: System.Net.Dns.GetHostName
        'Chuyển đổi tên máy thành IP để kết nối nhanh
        If sServerName.Contains("\") Then
            Dim sServerNameLast As String = sServerName.Substring(sServerName.IndexOf("\") + 1)
            sServerName = sServerName.Substring(0, sServerName.IndexOf("\"))
            Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(sServerName)
            Return h.AddressList.GetValue(0).ToString & "\" & sServerNameLast
        Else
            Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(sServerName)
            Return h.AddressList.GetValue(0).ToString
        End If
    End Function

#End Region

#Region "Format số"

    Public Function ReturnNumDigits(ByVal sNumberFormat As Object) As Integer
        If sNumberFormat Is Nothing OrElse sNumberFormat.ToString.Contains("Event") OrElse sNumberFormat.ToString.Equals("") Then Return 0
        Dim num_digits As Object = 0
        'Nếu sNumberFormat is string thì lấy số thập phân sau dấu "."
        If IsNumeric(sNumberFormat) Then 'TH gán mảng để xử lý khi NumberFormat của cột là FormatText Event
            num_digits = sNumberFormat
        ElseIf sNumberFormat.ToString.Contains("#") Then
            'Fix 06/06/2011
            If sNumberFormat.ToString.Contains(".") Then num_digits = sNumberFormat.ToString.Substring(sNumberFormat.ToString.IndexOf("."c) + 1).Length
        Else
            num_digits = sNumberFormat.ToString.Substring(1) 'Lay gia tri "N2"-> get so 2
        End If

        Return L3Int(num_digits)
    End Function

    '''' <summary>
    '''' Rounds a number to a specified number of digits
    '''' </summary>
    '''' <param name="num">Required. Expression to be formatted</param>
    '''' <param name="num_digit">Required. Numberic value indicating how many places are displayed to the right of the digis</param>
    '''' <returns>returns an expression foramtted as a currency value using the currency symbol define in the system control panel</returns>
    '''' <remarks>This function is only used when num_digit less than zero </remarks>
    Private Function RoundNumber(ByVal num As Double, ByVal num_digit As Integer) As Double
        Dim n As Double
        n = num * Math.Pow(10, num_digit)
        n = Math.Sign(n) * Math.Abs(Math.Floor(n + 0.5))
        Return n / Math.Pow(10, num_digit)
    End Function

    '''' <summary>
    '''' Rounds a number to a specified number of digits
    '''' </summary>
    '''' <param name="num">Required. Expression to be formatted</param>
    '''' <param name="num_digit">Required. Numberic value indicating how many places are displayed to the right of the digis</param>
    '''' <returns>returns an expression foramtted as a currency value using the currency symbol define in the system control panel</returns>
    '''' <remarks></remarks>
    Public Function FormatRoundNumber(ByVal num As String, ByVal num_digits As Integer) As String
        Dim dNum As Double = Number(num)
        Return FormatRoundNumber(dNum, num_digits)
    End Function

    Public Function FormatRoundNumber(ByVal dnum As Double, ByVal num_digits As Integer) As String
        If num_digits >= 0 Then
            Return FormatNumber(dnum, num_digits)
        Else
            ' Return FormatNumber(RoundNumber(dnum, num_digits), num_digits)
            'Update by Minh Hòa 22/03/2012: VD: FormatRoundNumber(20600, -3) --> 21000
            'Return RoundNumber(dnum, num_digits).ToString
            'Update by Hoàng Nhân 17/04/2013: VD: FormatRoundNumber(20600, -3) --> 21,000
            Return FormatNumber(RoundNumber(dnum, num_digits), 0)

        End If
    End Function
#End Region

#Region "Đóng các exe con đang gọi"

    Const RunningExe As String = "Software\DigiNet Corporation\Lemon3\RunningExe\"

    Private Function IsExistRegistryKey(ByVal sSubKey As String) As Microsoft.Win32.RegistryKey
        Return Microsoft.Win32.Registry.CurrentUser.OpenSubKey(sSubKey, True)
    End Function

    ''' <summary>
    ''' Lưu xuống Registry Exe hiện tại
    ''' </summary>
    ''' <param name="sCurrentExe">Exe hiện tại (ModuleD38)</param>
    ''' <param name="sChildExe">Exe con được gọi (EXECHILD)</param>
    ''' <remarks></remarks>
    Public Sub SaveRunningExeSettings(ByVal sCurrentExe As String, ByVal sChildExe As String)
        Dim RegistryKey As Microsoft.Win32.RegistryKey = IsExistRegistryKey(RunningExe & sCurrentExe)
        If RegistryKey Is Nothing Then RegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RunningExe & sCurrentExe, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
        RegistryKey.SetValue(sChildExe, sChildExe)

    End Sub

    ''' <summary>
    ''' Xóa nhánh Exe trong Registry
    ''' </summary>
    ''' <param name="sCurrentExe">exe đang gọi</param>
    ''' <remarks></remarks>
    Private Sub DeleteRunningExeSettings(ByVal sCurrentExe As String)
        Try
            Dim RegistryKey As Microsoft.Win32.RegistryKey = IsExistRegistryKey(RunningExe & sCurrentExe)
            If RegistryKey Is Nothing Then RegistryKey.DeleteSubKey(sCurrentExe, True)
        Catch ex As Exception
            D99C0008.MsgL3(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Đóng exe con liên quan exe đang gọi
    ''' </summary>
    ''' <param name="sCurrentExe"></param>
    ''' <remarks></remarks>
    Public Sub KillChildProcess(ByVal sCurrentExe As String)
        Dim arrSubKey() As String = Nothing
        Do
            Dim RegistryKey As Microsoft.Win32.RegistryKey = IsExistRegistryKey(RunningExe)
            If RegistryKey Is Nothing OrElse RegistryKey.SubKeyCount = 0 Then Exit Sub
            If arrSubKey IsNot Nothing Then sCurrentExe = arrSubKey(0)
            For Each subKeyName As String In RegistryKey.GetSubKeyNames()
                If subKeyName.Equals(sCurrentExe) Then
                    KillChildProcess(RegistryKey, subKeyName, arrSubKey)
                    Exit For
                End If
            Next
            arrSubKey = RemoveValueArray(arrSubKey, sCurrentExe)
        Loop While arrSubKey IsNot Nothing
    End Sub

    ''' <summary>
    ''' Xóa phần tử trong Array
    ''' </summary>
    ''' <param name="arrSubKey">mảng cần xóa</param>
    ''' <param name="sValue">Giá trị cần xóa trong mảng</param>
    ''' <returns>Mảng sau khi xóa</returns>
    ''' <remarks></remarks>
    Private Function RemoveValueArray(ByVal arrSubKey() As String, ByVal sValue As String) As String()
        If arrSubKey Is Nothing Then Return arrSubKey
        Dim arrTemp() As String = Nothing
        Dim index As Integer = Array.IndexOf(arrSubKey, sValue)
        If index >= 0 Then Array.Clear(arrSubKey, index, 1)
        For i As Integer = 0 To arrSubKey.Length - 1
            If arrSubKey(i) IsNot Nothing Then
                AddValueArray(arrTemp, arrSubKey(i))
            End If
        Next
        Return arrTemp
    End Function

    ''' <summary>
    ''' Thêm phần tử vào Array
    ''' </summary>
    ''' <param name="arrSubKey">Array cần thêm</param>
    ''' <param name="valueName">Giá trị cần thêm</param>
    ''' <remarks></remarks>
    Private Sub AddValueArray(ByRef arrSubKey() As String, ByVal valueName As String)
        If arrSubKey Is Nothing Then
            ReDim Preserve arrSubKey(0)
            arrSubKey.SetValue(valueName, arrSubKey.Length - 1)
        Else
            If L3FindArrString(arrSubKey, valueName) = False Then
                ReDim Preserve arrSubKey(arrSubKey.Length)
                arrSubKey.SetValue(valueName, arrSubKey.Length - 1)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Các bước đóng các exe con liên quan
    ''' </summary>
    ''' <param name="RegistryKey">Nhánh chính</param>
    ''' <param name="subKeyName">exe đang đóng</param>
    ''' <param name="arrSubKey">Danh sách các exe được gọi bởi exe đang đóng</param>
    ''' <remarks></remarks>
    Private Sub KillChildProcess(ByVal RegistryKey As Microsoft.Win32.RegistryKey, ByVal subKeyName As String, ByRef arrSubKey() As String)
        Dim tempKey As Microsoft.Win32.RegistryKey = RegistryKey.OpenSubKey(subKeyName, True)

        For Each valueName As String In tempKey.GetValueNames()
            Try

                tempKey.DeleteValue(valueName)
                If Process.GetProcessesByName(valueName).Length < 1 Then Continue For 'Bổ sung ngày 24/05/2011
                'Bước 1: Thêm exe vào mảng arrSubKey
                AddValueArray(arrSubKey, valueName)
                '***********************
                'Bước 2: Đóng exe
                Dim p As System.Diagnostics.Process = Nothing
                p = Process.GetProcessesByName(valueName)(0)
                If p Is Nothing Then Continue For
                p.Kill()
                Threading.Thread.Sleep(100)
                '*****************
            Catch ex As Exception
                D99C0008.MsgL3(ex.Message)
            End Try
        Next

        'Bước 3:  Xóa nhánh ở Registry
        RegistryKey.DeleteSubKey(subKeyName)
        '***********************
        'Bước 4:  Xóa exe hiện tại trong mảng
        arrSubKey = RemoveValueArray(arrSubKey, subKeyName)
        '***********************
    End Sub


#End Region
    Public Sub KillProcess(ByVal sProcessName As String)
        Dim p As New System.Diagnostics.Process
        Try
            p = Process.GetProcessesByName(sProcessName)(0)
        Catch ex As Exception
        End Try
        If p IsNot Nothing Then p.Kill()
    End Sub
#End Region

#Region "Các hàm chung cho sinh IGE"

    <DebuggerStepThrough()> _
   Public Function GetLastKey(Optional ByVal sStringCreateKey As String = "", Optional ByVal sTable As String = "D91T0001") As Long
        'Kiểm tra bảng D91T0000
        'Nếu tìm thấy then lấy LastKey
        'Nếu không tìm thấy thì insert 1 dòng mới vào
        Dim sSQL As String
        sSQL = "SELECT LastKey FROM D91T0000 WHERE TableName ='" & sTable & "'" _
          & " AND KeyString = '" & sStringCreateKey & "'"
        Dim sLastKey As String
        sLastKey = ReturnScalar(sSQL)

        If sLastKey <> "" Then ' có dữ liệu
            Return CLng(sLastKey) + 1
        Else ' Không có dữ liệu
            sSQL = "INSERT INTO D91T0000 VALUES ('" & sTable & "', '" & sStringCreateKey & "',0)"
            ExecuteSQLNoTransaction(sSQL)
            Return 1
        End If

    End Function

    <DebuggerStepThrough()> _
    Public Function CheckLengthKey(ByVal nLastKey As Long, ByVal sStringKey1 As String, ByVal sStringKey2 As String, ByVal sStringKey3 As String, ByVal sSeperatorCharacter As String, ByVal nOutputLength As Integer) As String
        Dim nKeyLength As Integer = 0
        If sSeperatorCharacter <> "" Then
            If sStringKey1 <> "" Then
                nKeyLength = nKeyLength + sStringKey1.Length + sSeperatorCharacter.Length
            End If
            If sStringKey2 <> "" Then
                nKeyLength = nKeyLength + sStringKey2.Length + sSeperatorCharacter.Length
            End If
            If sStringKey3 <> "" Then
                nKeyLength = nKeyLength + sStringKey3.Length + sSeperatorCharacter.Length
            End If
        Else
            If sStringKey1 <> "" Then nKeyLength = nKeyLength + sStringKey1.Length
            If sStringKey2 <> "" Then nKeyLength = nKeyLength + sStringKey2.Length
            If sStringKey3 <> "" Then nKeyLength = nKeyLength + sStringKey3.Length
        End If

        If (nKeyLength + nLastKey.ToString.Length) > nOutputLength Then
            AnnouncementLength()
            Return ""
        End If

        Dim nLastKeyLength As Integer = 0
        nLastKeyLength = CInt(nOutputLength) - nKeyLength - nLastKey.ToString.Length
        'LastKeyString = Strings.StrDup(nLastKeyLength, "0") & nLastKey
        Return Strings.StrDup(nLastKeyLength, "0") & nLastKey

    End Function

    <DebuggerStepThrough()> _
    Public Sub AnnouncementLength()
        If geLanguage = EnumLanguage.Vietnamese Then
            D99C0008.MsgL3("Chiều dài thiết lập vượt quá giới hạn cho phép." & vbCrLf & "Bạn phải thiết lập lại.", L3MessageBoxIcon.Exclamation)
        Else
            D99C0008.MsgL3("The lenght setup is off limits." & vbCrLf & "You should set again.", L3MessageBoxIcon.Exclamation)
        End If
    End Sub

    <DebuggerStepThrough()> _
    Public Function Generate(ByVal sS1 As String, ByVal sS2 As String, ByVal sS3 As String, ByVal sOrder As OutOrderEnum, ByVal sCharacter As String, ByVal sLastKeyString As String) As String
        Dim strIDKey As String = ""
        Dim strIncrement As String

        strIncrement = sLastKeyString

        If strIncrement = "" Then Return ""

        Select Case sOrder
            Case OutOrderEnum.lmSSSN
                strIDKey = ConcatenateKeys(sS1, sS2, sS3, strIncrement, sCharacter)
            Case OutOrderEnum.lmSSNS
                strIDKey = ConcatenateKeys(sS1, sS2, strIncrement, sS3, sCharacter)
            Case OutOrderEnum.lmSNSS
                strIDKey = ConcatenateKeys(sS1, strIncrement, sS2, sS3, sCharacter)
            Case OutOrderEnum.lmNSSS
                strIDKey = ConcatenateKeys(strIncrement, sS1, sS2, sS3, sCharacter)
        End Select

        Return strIDKey

    End Function

    <DebuggerStepThrough()> _
    Public Function ConcatenateKeys(ByVal Key1 As String, ByVal Key2 As String, ByVal Key3 As String, ByVal Key4 As String, ByVal sCharacter As String) As String

        Dim sKey1 As String, sKey2 As String, sKey3 As String, sKey4 As String
        sKey1 = Key1 : sKey2 = Key2 : sKey3 = Key3 : sKey4 = Key4

        If sCharacter <> "" Then 'Có dấu
            If sKey1 <> "" Then sKey1 = sKey1 & sCharacter
            If sKey2 <> "" Then sKey2 = sKey2 & sCharacter
            If sKey3 <> "" Then sKey3 = sKey3 & sCharacter
            If sKey4 <> "" Then sKey4 = sKey4 & sCharacter
        End If

        ConcatenateKeys = sKey1 & sKey2 & sKey3 & sKey4

        If sCharacter <> "" Then
            Return Left(ConcatenateKeys, Len(ConcatenateKeys) - Len(sCharacter))
        Else
            Return ConcatenateKeys
        End If

    End Function

    <DebuggerStepThrough()> _
    Public Sub SaveLastKey(ByVal sTable As String, ByVal sString As String, ByVal nLastKey As Long)
        Dim strSQL As String
        strSQL = "UPDATE D91T0000 Set LastKey =" & nLastKey _
        & " WHERE TableName = '" & sTable & "' AND KeyString = '" & sString & "'"

        ExecuteSQLNoTransaction(strSQL)
    End Sub

    ''' <summary>
    ''' Kiểm tra trùng Khóa
    ''' </summary>
    ''' <param name="Table_Name"></param>
    ''' <param name="Field_Name"></param>
    ''' <param name="Field_Values1"></param>
    ''' <param name="Field_Values2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> _
    Public Function CheckDupKeyPrimary(ByVal Table_Name As String, ByVal Field_Name As String, ByVal Field_Values1 As String, ByVal Field_Values2 As String) As Boolean
        Dim sSQL As String
        sSQL = "Select Top 1 1 From " & Table_Name & " WITH(NOLOCK) " & vbCrLf
        sSQL = sSQL & "Where " & Field_Name & " Between '" & Field_Values1 & "' And '" & Field_Values2 & "'"

        Return ExistRecord(sSQL)
    End Function

#End Region

#Region "Private Sub and Function"

    Private Function DataTypeChangEnum(ByVal sDataType As String) As D99D0041.FinderTypeEnum
        Select Case sDataType
            Case "S"
                Return FinderTypeEnum.lmFinderString
            Case "D"
                Return FinderTypeEnum.lmFinderDate
            Case "N1"
                Return FinderTypeEnum.lmFinderTinyInt
            Case "N2"
                Return FinderTypeEnum.lmFinderInt
            Case "N", "Percent" 'Update 20/11/2012: thêm định dạng %
                Return FinderTypeEnum.lmFinderMoney
        End Select

    End Function

    'Trả về cột đầu tiên không bị khóa và có hiển thị - Trường hợp True của Col First 
    Private Function ColVisible(ByVal c1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, _
    ByVal Split As Integer, _
    Optional ByVal ColFirst As Boolean = False) As Integer
        Dim i As Integer
        If ColFirst = False Then
            For i = c1Grid.Columns.Count - 1 To 0 Step -1
                If c1Grid.Splits(Split).DisplayColumns(i).Visible = True And c1Grid.Splits(Split).DisplayColumns(i).Locked = False Then
                    Return i
                End If

            Next i
        Else
            For i = 0 To c1Grid.Columns.Count - 1
                If c1Grid.Splits(Split).DisplayColumns(i).Visible = True And c1Grid.Splits(Split).DisplayColumns(i).Locked = False Then Return i
            Next i
        End If
        Return 0
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Description: Kiểm tra Ngày phiếu có phù hợp với Kỳ kế toán hiện tại không
    '#---------------------------------------------------------------------------------------------------
    <DebuggerStepThrough()> _
    Private Function SQLStoreD91P9105(ByVal VoucherDate As String) As String
        Dim sSQL As String = "-- Purpose: Kiem tra ngay va ky ke toan" & vbCrLf
        sSQL &= "Exec D91P9105 "
        sSQL &= SQLDateSave(VoucherDate) & COMMA 'VoucherDate, DateTime, NOT NULL
        sSQL &= SQLNumber(giTranMonth) & COMMA 'TranMonth, Int, NOT NULL
        sSQL &= SQLNumber(giTranYear) & COMMA 'TranYear, Int, NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, VarChar[20], NOT NULL
        Return sSQL
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Description: Kiểm tra trùng phiếu
    '#---------------------------------------------------------------------------------------------------
    <DebuggerStepThrough()> _
    Private Function SQLStoreD91P9102(ByVal ModuleID As String, ByVal TableName As String, ByVal VoucherID As String, ByVal VoucherNo As String) As String
        Dim sSQL As String = "---- Kiem tra trung phieu" & vbCrLf
        sSQL &= "Exec D91P9102 "
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, VarChar[20], NOT NULL
        sSQL &= SQLString(ModuleID) & COMMA 'ModuleID, VarChar[20], NOT NULL
        sSQL &= SQLString(TableName) & COMMA 'TableName, VarChar[20], NOT NULL
        sSQL &= SQLString(VoucherID) & COMMA 'VoucherID, VarChar[20], NOT NULL
        sSQL &= SQLString(VoucherNo) & COMMA 'VoucherNo, VarChar[20], NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, VarChar[20], NOT NULL
        Return sSQL
    End Function


    '#---------------------------------------------------------------------------------------------------
    '# Description: Sinh số phiếu theo kiểu mới
    '#---------------------------------------------------------------------------------------------------
    <DebuggerStepThrough()> _
    Public Function SQLStoreD91P9111(ByVal VoucherIGE As String, ByVal VoucherTableName As String, ByVal S1 As String, ByVal S2 As String, ByVal S3 As String, ByVal OutputLength As Integer, ByVal OutputOrder As Integer, ByVal Separator As String) As String
        Dim sSQL As String = "----Tang so phieu tu dong va kiem tra trung phieu" & vbCrLf
        sSQL &= "SET NOCOUNT ON " & vbCrLf
        sSQL &= "DECLARE @VoucherNo AS VARCHAR(20) " & vbCrLf
        sSQL &= "Exec D91P9111 "
        sSQL &= SQLString("D91T0001") & COMMA 'TableName, varchar[8], NOT NULL
        sSQL &= SQLString(S1) & COMMA 'StringKey1, varchar[20], NOT NULL
        sSQL &= SQLString(S2) & COMMA 'StringKey2, varchar[20], NOT NULL
        sSQL &= SQLString(S3) & COMMA 'StringKey3, varchar[20], NOT NULL
        sSQL &= SQLNumber(OutputLength) & COMMA 'OutputLen, int, NOT NULL
        sSQL &= SQLNumber(OutputOrder) & COMMA 'OutputOrder, int, NOT NULL
        If Separator <> "" Then
            sSQL &= SQLNumber(1) & COMMA 'Seperated, int, NOT NULL
            sSQL &= SQLString(Separator) & COMMA 'Seperator, char, NOT NULL
        Else
            sSQL &= SQLNumber(0) & COMMA 'Seperated, int, NOT NULL
            sSQL &= SQLString("") & COMMA 'Seperator, char, NOT NULL
        End If
        sSQL &= SQLString("") & COMMA 'Temp1, varchar[20], NOT NULL
        sSQL &= SQLString("") & COMMA 'Temp2, varchar[20], NOT NULL
        sSQL &= SQLString("") & COMMA 'Temp3, varchar[20], NOT NULL
        sSQL &= SQLString(VoucherIGE) & COMMA 'VoucherIGE, varchar[20], NOT NULL
        sSQL &= SQLString(VoucherTableName) & COMMA  'VoucherTableName, varchar[20], NOT NULL
        sSQL &= " @VoucherNo  OUTPUT " & vbCrLf 'KeyString, varchar[20], NOT NULL
        sSQL &= "SELECT @VoucherNo AS VoucherNo "
        Return sSQL

    End Function


    '#---------------------------------------------------------------------------------------------------
    '# Description: Kiểm tra trùng phiếu theo kiểu mới
    '#---------------------------------------------------------------------------------------------------
    <DebuggerStepThrough()> _
    Private Function SQLStoreD91P9114(ByVal ModuleID As String, ByVal TableName As String, ByVal VoucherID As String, ByVal VoucherNo As String) As String
        Dim sSQL As String = "---- Kiem tra trung phieu " & vbCrLf
        sSQL &= "Exec D91P9114 "
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, VarChar[20], NOT NULL
        sSQL &= SQLString(ModuleID) & COMMA 'ModuleID, VarChar[20], NOT NULL
        sSQL &= SQLString(TableName) & COMMA 'TableName, VarChar[20], NOT NULL
        sSQL &= SQLString(VoucherID) & COMMA 'VoucherID, VarChar[20], NOT NULL
        sSQL &= SQLString(VoucherNo) & COMMA 'VoucherNo, VarChar[20], NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, VarChar[20], NOT NULL
        Return sSQL
    End Function

    <DebuggerStepThrough()> _
    Public Sub WriteLogFile(ByVal Text As String, Optional ByVal FileName As String = "Log.log")
        Dim sLog As String = ""
        Dim sFileName As String = My.Application.Info.DirectoryPath & "\" & FileName '"\Log.log"
        If (My.Computer.FileSystem.FileExists(sFileName) = False) Then My.Computer.FileSystem.WriteAllText(sFileName, "", True)
        Dim lFileSize As Long = My.Computer.FileSystem.GetFileInfo(sFileName).Length
        If lFileSize > 10 * 1028 * 1028 Then My.Computer.FileSystem.DeleteFile(sFileName, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
        sLog &= Space(20) & Now & vbCrLf
        sLog &= Text & vbCrLf
        sLog &= "--------------------------------------------------------------------------" & vbCrLf
        'Update 02/07/2010: set thuộc tính ReadOnly = False
        My.Computer.FileSystem.GetFileInfo(sFileName).IsReadOnly = False
        My.Computer.FileSystem.WriteAllText(sFileName, sLog, True)
    End Sub

    <DebuggerStepThrough()> _
    Public Function FindSxType(ByVal nType As String, ByVal s As String) As String
        Select Case nType.Trim
            Case "1" ' Theo tháng
                Return giTranMonth.ToString("00")
            Case "2" ' Theo năm (YYYY)
                Return giTranYear.ToString
            Case "3" ' Theo loại chứng từ
                Return s
            Case "4" ' Theo đơn vị
                Return gsDivisionID
            Case "5" ' Theo hằng
                Return s

                'Modify date: 02/02/2007: bổ sung thêm 3 loại 
            Case "6" ' Theo năm (YY)
                Return giTranYear.ToString.Substring(2, 2)
            Case "7" ' Theo tháng năm (MMYY)
                Return giTranMonth.ToString("00") & giTranYear.ToString.Substring(2, 2)
            Case "8" ' Theo năm tháng (YYMM)
                Return giTranYear.ToString.Substring(2, 2) & giTranMonth.ToString("00")

            Case Else
                Return ""
        End Select
    End Function

    <DebuggerStepThrough()> _
    Private Function SQLStoreD91P9110() As String
        Dim sSQL As String = "---- Sinh khoa" & vbCrLf
        sSQL &= "Exec D91P9110 "
        sSQL &= SQLString(gsUserID) 'MachineID, VarChar[50], NOT NULL
        Return sSQL
    End Function

#End Region

#Region "Check DLL khi chạy Debug"

#If DEBUG Then

    Public Sub CheckDLL()
        Dim sLocalDLL As String
        Dim sServerDLL As String
        '----- Check D00D0041.dll
        sLocalDLL = "C:\LEMON3\D00D0041.dll"
        sServerDLL = "\\\iserver\\DRD1\\PROG\\Library\\VB .Net\\DLL2005\\D00D0041.dll"
        If CheckDLL(sLocalDLL, sServerDLL) Then End
        '----- Check D99D0041.dll
        sLocalDLL = "C:\LEMON3\D99D0041.dll"
        sServerDLL = "\\iserver\\DRD1\\PROG\\Library\\VB .Net\\DLL2005\\D99D0041.dll"
        If CheckDLL(sLocalDLL, sServerDLL) Then End
        Dim sModulesPath As String = ""
        sModulesPath = Application.StartupPath
        If sModulesPath.EndsWith("\bin\Debug") Then
            Dim sModule As String = ""
            sModule = sModulesPath.Substring(0, sModulesPath.Length - "\bin\Debug".Length)
            Dim sLocalModule As String = ""
            Dim sServerModule As String
            For i As Integer = 0 To 2
                sLocalModule = sModule & "\2.Modules\" & "D99X000" & i.ToString() & ".vb"
                sServerModule = "\\iserver\\DRD1\\PROG\\Library\\VB .Net\\DLL2005\\D99X000" & i.ToString() & ".vb"
                Dim fInfoLocal, fInfoServer As System.IO.FileInfo
                Dim lLengthLoccal, lLengthServer As Long
                Dim dLastWriteTimeLocal, dLastWriteTimeServer As Date
                '-----------------------------------------------------------------
                fInfoLocal = New System.IO.FileInfo(sLocalModule)
                lLengthLoccal = fInfoLocal.Length
                dLastWriteTimeLocal = fInfoLocal.LastWriteTime.Date
                '-----------------------------------------------------------------
                fInfoServer = New System.IO.FileInfo(sServerModule)
                lLengthServer = fInfoServer.Length
                dLastWriteTimeServer = fInfoServer.LastWriteTime.Date
                '-----------------------------------------------------------------
                If lLengthLoccal <> lLengthServer OrElse dLastWriteTimeLocal <> dLastWriteTimeServer Then
                    D99C0008.MsgL3("Bạn phải cập nhật lại file: " & sLocalModule & " ở đường dẫn " & sServerModule)
                    End
                End If
            Next
        End If
    End Sub

    Private Function CheckDLL(ByVal DLLLocal As String, ByVal DLLServer As String) As Boolean
        If Not System.IO.File.Exists(DLLLocal) Then
            D99C0008.MsgL3("Không tìm thấy DLL ở đường dẫn: " & DLLLocal)
            End
        End If
        If Not System.IO.File.Exists(DLLServer) Then
            D99C0008.MsgL3("Không tìm thấy DLL ở đường dẫn: " & DLLServer)
            End
        End If
        Dim assemblyNameLocal As System.Reflection.AssemblyName = System.Reflection.AssemblyName.GetAssemblyName(DLLLocal)
        Dim assemblyNameServer As System.Reflection.AssemblyName = System.Reflection.AssemblyName.GetAssemblyName(DLLServer)
        Dim sLocalVersion As String = assemblyNameLocal.Version.ToString
        Dim sServerVersion As String = assemblyNameServer.Version.ToString
        If sLocalVersion <> sServerVersion Then
            D99C0008.MsgL3("Bạn phải update lại toàn bộ DLL và Module .NET 2005 trên thư mục quy định")
            End
        End If
    End Function

#End If
#End Region

    

End Module
