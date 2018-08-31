Imports System.IO
Imports System.DateTime
Imports System.Data.OleDb
Public Class DataBelumLunas
    Dim objkon As New koneksi
    Dim sql As String
    Dim cmd As OleDbCommand
    Dim baca As OleDbDataReader
    Private Sub DataBelumLunas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            objkon.konek()
            Dim qry As New OleDbDataAdapter("Select tbbayar.kode_bayar, tbmahasiswa.nim, tbmahasiswa.nama_mahasiswa, tbmahasiswa.jurusan, tbmahasiswa.tahun_akademik, tbbayar.semester, tbbayar.keterangan from tbbayar INNER JOIN tbmahasiswa ON tbbayar.kode_mahasiswa = tbmahasiswa.kode_mahasiswa WHERE keterangan='BELUM LUNAS'", objkon.kon)
            Dim dt As New DataSet
            qry.Fill(dt, "tbinfobayar, tbbayar, tbmahasiswa")
            DataGridView1.DataSource = dt.Tables("tbinfobayar, tbbayar, tbmahasiswa")
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        MasterInfoBayar.txtkodebayar.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        MasterInfoBayar.txtnim.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        MasterInfoBayar.txtnama.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        MasterInfoBayar.txtjurusan.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        MasterInfoBayar.txttahun.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        MasterInfoBayar.txtsemester.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        MasterInfoBayar.txtketerangan.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value

        MasterInfoBayar.btnsimpan.Enabled = True
        Me.Hide()
        MasterInfoBayar.Show()
        MasterInfoBayar.txtnim.Focus()
    End Sub
End Class