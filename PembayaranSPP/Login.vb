Imports System.IO
Imports System.Data.OleDb
Public Class Login
    Dim objkon As New koneksi
    Dim sql As String

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        objkon.konek()
        Dim qry As New OleDbDataAdapter("SELECT count(*), kode_kasir, nama_kasir From tbkasir WHERE kode_kasir = '" & txtuser.Text & "' and password = '" & txtpass.Text & "' group by kode_kasir, nama_kasir", objkon.kon)
        Try
            Dim dt As New DataSet
            qry.Fill(dt, "tbkasir")
            With dt.Tables(0).Rows(0)
                If .Item(0) > 0 Then
                    MenuUtama.Show()
                    If .Item(2) = "" Then
                        MenuUtama.menumahasiswa.Enabled = True
                        MenuUtama.menukasir.Enabled = True
                        MenuUtama.menubayar.Enabled = True
                        MenuUtama.menulaporan.Enabled = True

                    End If
                    MenuUtama.menukode.Text = .Item(1)
                    MenuUtama.menunama.Text = .Item(2)
                    MasterBayar.txtkodekasir.Text = .Item(1)
                    MasterBayar.txtnamakasir.Text = .Item(2)
                    txtuser.Text = ""
                    txtpass.Text = ""
                    Me.Hide()
                Else
                    MsgBox("Login Gagal")
                    txtuser.Text = ""
                    txtpass.Text = ""
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtlogo.Image = Image.FromFile("logo.jpg")
    End Sub
End Class