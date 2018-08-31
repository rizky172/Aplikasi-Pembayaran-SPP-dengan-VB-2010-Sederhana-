Imports System.IO
Imports System.DateTime
Imports System.Data.OleDb
Public Class MasterInfoBayar
    Dim objkon As New koneksi
    Dim sql As String
    Dim cmd As OleDbCommand
    Dim baca As OleDbDataReader

    Private Sub MasterInfoBayar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tampilgrid()
        kodeinfo()
        kunciTextBox()
    End Sub

    Sub bersih()
        txtnama.Text = ""
        txtnama.Enabled = False
        txtkodebayar.Text = ""
        txtkodebayar.Enabled = False
        txtjurusan.Text = ""
        txtjurusan.Enabled = False
        txtnim.Text = ""
        txtnim.Enabled = False
        txttahun.Enabled = False
        txttahun.Text = ""
        txtsemester.Text = ""
        txtsemester.Enabled = False
        txtketerangan.Text = ""
        txtketerangan.Enabled = False

        btnsimpan.Enabled = False
    End Sub

    Sub kunciTextBox()
        txtnim.Enabled = False
        txttahun.Enabled = False
        txtjurusan.Enabled = False
        btnsimpan.Enabled = False
        txtkodebayar.Enabled = False
        txtsemester.Enabled = False
        txtnama.Enabled = False
        txtketerangan.Enabled = False
        txtkode.Enabled = False
    End Sub

    Private Sub kodeinfo()
        Dim kd_lama As String = ""
        Dim kd_baru As String = ""
        Dim NoBarang As String = ""
        Try
            objkon.konek()
            sql = "Select * from tbinfobayar order by kode_info desc"
            cmd = New OleDbCommand(sql, objkon.kon)
            baca = cmd.ExecuteReader
            If baca.Read Then
                kd_lama = Mid(baca.Item("kode_info"), 3, 3)
            Else
                txtkode.Text = "KI001"
            End If
            kd_baru = Val(kd_lama) + 1
            NoBarang = "KI" & Mid("000", 1, 3 - kd_baru.Length) & kd_baru
            txtkode.Text = NoBarang
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub tampilgrid()
        Try
            objkon.konek()
            Dim qry As New OleDbDataAdapter("Select tbinfobayar.kode_info, tbbayar.kode_bayar, tbmahasiswa.nim, tbmahasiswa.nama_mahasiswa, tbmahasiswa.jurusan, tbmahasiswa.tahun_akademik, tbbayar.semester, tbbayar.keterangan from (tbinfobayar INNER JOIN tbbayar ON tbinfobayar.kode_bayar = tbbayar.kode_bayar) INNER JOIN tbmahasiswa ON tbbayar.kode_mahasiswa = tbmahasiswa.kode_mahasiswa", objkon.kon)
            Dim dt As New DataSet
            qry.Fill(dt, "tbinfobayar, tbbayar, tbmahasiswa")
            DataGridView1.DataSource = dt.Tables("tbinfobayar, tbbayar, tbmahasiswa")
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtnim_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcarinim.TextChanged
        Try
            objkon.konek()
            Dim qry As New OleDbDataAdapter("Select tbmahasiswa.nim, tbmahasiswa.nama_mahasiswa, tbmahasiswa.jurusan, tbbayar.semester from (tbinfobayar INNER JOIN tbbayar ON tbinfobayar.kode_bayar = tbbayar.kode_bayar) INNER JOIN tbmahasiswa ON tbbayar.kode_mahasiswa = tbmahasiswa.kode_mahasiswa where nim like '%" & txtcarinim.Text & "%' ", objkon.kon)
            Dim dt As New DataSet
            qry.Fill(dt, "tbinfobayar, tbbayar, tbmahasiswa")
            DataGridView1.DataSource = dt.Tables("tbinfobayar, tbbayar, tbmahasiswa")
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtkode.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtkodebayar.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtnim.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtnama.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtjurusan.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        txttahun.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        txtsemester.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        txtketerangan.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
    End Sub

    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        Dim cari As New CariData
        cari.Show()
        Me.Hide()
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        kodeinfo()
        Dim qry As String = "INSERT INTO tbinfobayar values ('" & txtkode.Text & "', '" & txtkodebayar.Text & "')"
        Try
            objkon.konek()
            Dim cmd As New OleDbCommand(qry, objkon.kon)
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil Disimpan!", MsgBoxStyle.Information, "Perhatian")
            kunciTextBox()
            bersih()
            tampilgrid()
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox("Gagal Input")
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnbelum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbelum.Click
        Dim cari As New DataBelumLunas
        cari.Show()
        Me.Hide()
    End Sub
End Class