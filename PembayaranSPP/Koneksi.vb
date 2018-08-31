Imports System.Data.OleDb
Public Class koneksi
    Public kon As OleDbConnection

    Public Function konek()

        kon = New OleDbConnection("provider=microsoft.ace.oledb.12.0;data source=pembayaranspp.accdb")
        Try
            kon.Open()


        Catch ex As Exception
            MsgBox("Koneksi Ke database GAGAL Silahkan periksa koneksi")

        End Try
        Return True
    End Function
End Class