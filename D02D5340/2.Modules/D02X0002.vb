Imports System.Text

Module D02X0002
    'Them ngay 18/2/2013 theo ID 54104 của Bảo Trân bởi Văn Vinh
    ''' <summary>
    ''' Tìm kiếm mở rộng theo Tiêu thức
    ''' </summary>
    ''' <param name="sSQLSelection">Required. Câu đổ nguồn của combo</param>
    ''' <param name="tdbcFrom">Required. Tiêu thức Từ</param>
    ''' <param name="tdbcTo">Optional. Tiêu thức Đến</param>
    ''' <param name="iModeSelect">Optional. Default. 0: In theo giá trị Từ Đến. 1: In nhiều giá trị</param>
    ''' <returns>Chuỗi tìm kiếm. Khác rỗng khi lấy tập hợp</returns>
    ''' <remarks></remarks>
    Public Function HotKeyF2D91F6020(ByVal sSQLSelection As String, ByRef tdbcFrom As C1.Win.C1List.C1Combo, Optional ByRef tdbcTo As C1.Win.C1List.C1Combo = Nothing, Optional ByVal iModeSelect As Integer = 0) As String
        'Dim sKeyID As String = ""
        'Dim f As New D91F6020
        'With f
        '    .SQLSelection = sSQLSelection 'Theo TL phân tích
        '    .ModeSelect = iModeSelect.ToString
        '    .FormPermision = "D02F7005"
        '    .ShowDialog()
        '    sKeyID = .OutPut01
        '    .Dispose()
        'End With

        Dim arrPro() As StructureProperties = Nothing
        SetProperties(arrPro, "FormIDPermission", "D02F7005")
        SetProperties(arrPro, "SQLSelection", sSQLSelection)
        SetProperties(arrPro, "ModeSelect", iModeSelect.ToString)
        Dim frm As Form = CallFormShowDialog("D91D0240", "D91F6020", arrPro)
        Dim sKeyID As String = GetProperties(frm, "ReturnField").ToString

        If sKeyID <> "" Then
            If sKeyID.Substring(0, 1) <> "(" And sKeyID.Substring(0, 1) <> "T" Then 'Lấy theo giá trị Từ đến: gán lại giá trị cho 2 combo tiêu thức từ đến, chuỗi tiêu thức gán bằng rỗng, sSQLOutput1= ""
                Dim arrResult() As String = sKeyID.Split(";"c)
                tdbcFrom.Text = arrResult(0)
                If tdbcTo IsNot Nothing Then
                    If arrResult.Length = 1 Then
                        tdbcTo.Text = arrResult(0)
                    Else
                        tdbcTo.Text = arrResult(1)
                    End If
                End If
                sKeyID = ""
            Else 'Lấy theo tập hợp: gán giá trị % cho 2 combo tiêu thức từ đến, chuỗi tiêu thức sSQLOutput1= sResult
                tdbcFrom.Text = "%"
                tdbcTo.Text = "%"
            End If
        End If
        Return sKeyID
    End Function

   End Module
