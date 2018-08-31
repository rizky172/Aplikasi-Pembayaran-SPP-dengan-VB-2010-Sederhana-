Imports System.Data.OleDb
Public Class MasterKasir
    Dim objkon As New koneksi
    Dim sql As String
    Dim cmd As OleDbCommand
    Dim baca As OleDbDataReader
    Private Sub MasterKasir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tampil_grid()
        kunciTextBox()
        kodekasir()
        btnsimpan.Enabled = False
    End Sub

    Private Sub tampil_grid()
        Try
            objkon.konek()
            Dim qry As New OleDbDataAdapter("Select * from tbkasir", objkon.kon)
            Dim dt As New DataSet
            qry.Fill(dt, "tbkasir")
            DataGridView1.DataSource = dt.Tables("tbkasir")
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub kunciTextBox()
        txtnama.Enabled = False
        txttlp.Enabled = False
        txtkode.Enabled = False
        txtalamat.Enabled = False
        txtpass.Enabled = False
    End Sub

    Sub bukaTextBox()
        txtnama.Enabled = True
        txttlp.Enabled = True
        txttlp.Enabled = True
        txtalamat.Enabled = True
        txtpass.Enabled = True
    End Sub

    Sub bersih()
        txtnama.Text = ""
        txtnama.Enabled = False
        txttlp.Text = ""
        txttlp.Enabled = False
        txtalamat.Text = ""
        txtalamat.Enabled = False
        txtpass.Text = ""
        txtpass.Enabled = False
        btnsimpan.Enabled = False
        btnhapus.Enabled = False
        btnedit.Enabled = False
        btnedit.Text = "EDIT"
        btntambah.Text = "TAMBAH"
    End Sub

    Sub kodekasir()
        Dim kd_lama As String = ""
        Dim kd_baru As String = ""
        Dim NoBarang As String = ""
        Try
            objkon.konek()
            sql = "Select * from tbkasir order by kode_kasir desc"
            cmd = New OleDbCommand(sql, objkon.kon)
            baca = cmd.ExecuteReader
            If baca.Read Then
                kd_lama = Mid(baca.Item("kode_kasir"), 3, 3)
            Else
                txtkode.Text = "KS001"
            End If
            kd_baru = Val(kd_lama) + 1
            NoBarang = "KS" & Mid("000", 1, 3 - kd_baru.Length) & kd_baru
            txtkode.Text = NoBarang
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtkode.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtnama.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txttlp.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtalamat.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtpass.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        objkon.kon.Close()
        objkon.kon.Dispose()
        btnhapus.Enabled = True
        btnsimpan.Enabled = False
        btnsimpan.Text = "SIMPAN"
        btnedit.Enabled = True
        btnedit.Text = "EDIT"
    End Sub

    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        bersih()
        bukaTextBox()
        btnsimpan.Enabled = True
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        Dim qry As String = "INSERT INTO tbkasir values ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txttlp.Text & "', '" & txtalamat.Text & "', '" & txtpass.Text & "' )"
        Try
            objkon.konek()
            Dim cmd As New OleDbCommand(qry, objkon.kon)
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil Disimpan!", MsgBoxStyle.Information, "Perhatian")


            kunciTextBox()
            tampil_grid()
            bersih()
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox("Gagal Input")
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnhapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhapus.Click
        Try
            Dim Hapus As String = "delete from tbkasir where kode_kasir = '" & txtkode.Text & "' "
            Dim Pesan As String = MsgBox("Yakin Akan Menghapus Data dengan Kode Kasir = " & txtkode.Text & " ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Konfirmasi")
            If Pesan = vbYes Then
                objkon.konek()
                Dim cmd As New OleDbCommand(Hapus, objkon.kon)
                cmd.ExecuteNonQuery()
                bersih()
                objkon.kon.Close()
                tampil_grid()
                MsgBox("Data Sudah Dihapus...!")

            Else
                bersih()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            objkon.kon.Close()
        End Try
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        btnsimpan.Enabled = True
        If btnedit.Text = "SIMPAN PERUBAHAN" Then
            Dim Pesan As String = MsgBox("Yakin Akan Merubah Data dengan Kode Mahasiswa = " & txtkode.Text & " ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Konfirmasi")
            If Pesan = vbYes Then
                Try
                    If txtkode.Text = "" Then
                        MsgBox("Silahkan pilih data yang akan di Rubah")
                        Exit Sub
                    End If
                    Dim qry As String = "update tbkasir set nama_kasir = '" & txtnama.Text & "',telepon = '" & txttlp.Text & "' , alamat_kasir = '" & txtalamat.Text & "', password = '" & txtpass.Text & "' where kode_kasir = '" & txtkode.Text & "'"
                    objkon.konek()
                    Dim cmd As New OleDbCommand(qry, objkon.kon)
                    cmd.ExecuteNonQuery()
                    bersih()
                    objkon.kon.Close()
                    MsgBox("Data Berhasil Dirubah", MsgBoxStyle.Information, "Perhatian")
                    tampil_grid()
                Catch ex As Exception
                    bersih()
                    MsgBox("Data Gagal Di edit")
                    MsgBox(ex.Message)
                End Try
                btnedit.Enabled = False
            Else
                bersih()
            End If
        Else
            btnedit.Text = "SIMPAN PERUBAHAN"
            bukaTextBox()
        End If
    End Sub

    Private Sub btnbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbatal.Click
        kunciTextBox()
        bersih()
    End Sub
End Class