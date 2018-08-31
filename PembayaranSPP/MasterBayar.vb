Imports System.IO
Imports System.DateTime
Imports System.Data.OleDb
Public Class MasterBayar
    Dim objkon As New koneksi
    Dim sql As String
    Dim cmd As OleDbCommand
    Dim baca As OleDbDataReader
    Private Sub txtnama_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnama.SelectedIndexChanged
        Try
            Dim rd As OleDbDataReader
            objkon.konek()
            sql = "select * from tbmahasiswa where nama_mahasiswa='" & txtnama.SelectedItem & "'"
            cmd = New OleDbCommand(sql, objkon.kon)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                txtkodemahasiswa.Text = rd.Item("kode_mahasiswa")
                txtnim.Text = rd.Item("nim")
                txtjurusan.Text = rd.Item("jurusan")
                txttahun.Text = rd.Item("tahun_akademik")
                txtspp.Text = rd.Item("spp")
            End If
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub tampilkombo()
        Try
            objkon.konek()
            sql = "Select nama_mahasiswa from tbmahasiswa"
            cmd = New OleDbCommand(sql, objkon.kon)
            baca = cmd.ExecuteReader
            Do While baca.Read
                txtnama.Items.Add(baca("nama_mahasiswa"))
            Loop
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub MasterBayar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tampilkombo()
        kunciTextBox()
        tampil_grid()
        notabayar()
        kodebayar()
        btnsimpan.Enabled = False
    End Sub

    Sub kodebayar()
        Dim kd_lama As String = ""
        Dim kd_baru As String = ""
        Dim NoBarang As String = ""
        Try
            objkon.konek()
            sql = "Select * from tbbayar order by kode_bayar desc"
            cmd = New OleDbCommand(sql, objkon.kon)
            baca = cmd.ExecuteReader
            If baca.Read Then
                kd_lama = Mid(baca.Item("kode_bayar"), 3, 3)
            Else
                txtkodebayar.Text = "KB001"
            End If
            kd_baru = Val(kd_lama) + 1
            NoBarang = "KB" & Mid("000", 1, 3 - kd_baru.Length) & kd_baru
            txtkodebayar.Text = NoBarang
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub notabayar()
        Try
            objkon.konek()
            sql = "Select * from tbbayar "
            cmd = New OleDbCommand(sql, objkon.kon)
            baca = cmd.ExecuteReader
            baca.Read()
            If Not baca.HasRows Then
                txtnotabayar.Text = Format(Now, "yyMMdd") + "0001"
            Else
                If Microsoft.VisualBasic.Left(baca.Item("nota_bayar"), 6) = Format(Now, "yyMMdd") Then
                    txtnotabayar.Text = baca.Item("nota_bayar") + 1
                Else
                    txtnotabayar.Text = Format(Now, "yyMMdd") + "0001"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tampil_grid()
        Try
            objkon.konek()
            Dim qry As New OleDbDataAdapter("Select tbbayar.kode_bayar, tbbayar.nota_bayar, tbmahasiswa.nim, tbmahasiswa.nama_mahasiswa, tbmahasiswa.jurusan, tbmahasiswa.tahun_akademik, tbmahasiswa.spp, tbbayar.tanggal_bayar, tbbayar.semester, tbbayar.uang_bayar, tbbayar.sisa_bayar, tbbayar.keterangan  from tbbayar INNER JOIN tbmahasiswa ON tbbayar.kode_mahasiswa = tbmahasiswa.kode_mahasiswa", objkon.kon)
            Dim dt As New DataSet
            qry.Fill(dt, "tbbayar, tbmahasiswa")
            DataGridView1.DataSource = dt.Tables("tbbayar, tbmahasiswa")
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub kunciTextBox()
        txtnim.Enabled = False
        txtkodemahasiswa.Enabled = False
        txttahun.Enabled = False
        txtkodekasir.Enabled = False
        txtnamakasir.Enabled = False
        txtjurusan.Enabled = False
        btnsimpan.Enabled = False
        txtspp.Enabled = False
        txtkodebayar.Enabled = False
        txtnotabayar.Enabled = False
        txtsemester.Enabled = False
        txttanggal.Enabled = False
        txtnama.Enabled = False
        txtketerangan.Enabled = False
        txtbayar.Enabled = False
        txtsisa.Enabled = False
    End Sub

    Sub bersih()
        txtnama.Text = "Mahasiswa"
        txtnama.Enabled = False
        txtnim.Text = ""
        txtnim.Enabled = False
        txtjurusan.Enabled = False
        txtjurusan.Text = ""
        txttanggal.Enabled = False
        txttahun.Enabled = False
        txttahun.Text = ""
        txtspp.Text = ""
        txtsemester.Text = "Semester"
        txtsemester.Enabled = False
        txtbayar.Text = ""
        txtbayar.Enabled = False
        txtsisa.Text = ""
        txtsisa.Enabled = False
        txtketerangan.Text = ""
        txtketerangan.Enabled = False

        btnsimpan.Enabled = False
        btnedit.Enabled = False
        btnedit.Text = "Bayar Ulang"
        btntambah.Text = "BAYAR"
    End Sub


    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtkodebayar.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtnotabayar.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtnim.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtnama.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtjurusan.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        txttahun.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        txtspp.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        txttanggal.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
        txtsemester.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value
        txtbayar.Text = DataGridView1.Rows(e.RowIndex).Cells(9).Value
        txtsisa.Text = DataGridView1.Rows(e.RowIndex).Cells(10).Value
        txtketerangan.Text = DataGridView1.Rows(e.RowIndex).Cells(11).Value
        objkon.kon.Close()
        objkon.kon.Dispose()
        btnbatal.Enabled = True
        btnsimpan.Enabled = False
        btnsimpan.Text = "SIMPAN"
        btnedit.Enabled = True
        btnedit.Text = "Bayar Ulang"
    End Sub

    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        bersih()
        kodebayar()
        notabayar()
        bukaTextBox()
        btnsimpan.Enabled = True
    End Sub

    Sub bukaTextBox()
        txtnama.Enabled = True
        txtsemester.Enabled = True
        txtbayar.Enabled = True
        txttanggal.Enabled = True
    End Sub

    Sub bukaTextBoxedit()
        txtbayar.Enabled = True
        txttanggal.Enabled = True
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        Dim qry As String = "INSERT INTO tbbayar values ('" & txtkodebayar.Text & "', '" & txtkodemahasiswa.Text & "', '" & txtkodekasir.Text & "', '" & txtnotabayar.Text & "', '" & txttanggal.Value & "', '" & txtsemester.Text & "', '" & txtbayar.Text & "', '" & txtsisa.Text & "', '" & txtketerangan.Text & "' )"
        Try
            objkon.konek()
            Dim cmd As New OleDbCommand(qry, objkon.kon)
            cmd.ExecuteNonQuery()

            If txtsisa.Text <= 0 Then
                Dim editstatus As String = "update tbbayar set keterangan='LUNAS' where kode_bayar='" & txtkodebayar.Text & "'"
                cmd = New OleDbCommand(editstatus, objkon.kon)
                cmd.ExecuteNonQuery()
            ElseIf txtsisa.Text >= 0 Then
                Dim editstatus As String = "update tbbayar set keterangan='BELUM LUNAS' where kode_bayar='" & txtkodebayar.Text & "'"
                cmd = New OleDbCommand(editstatus, objkon.kon)
                cmd.ExecuteNonQuery()
            End If

            MsgBox("Data Berhasil Disimpan!", MsgBoxStyle.Information, "Perhatian")
            kunciTextBox()
            tampil_grid()
            bersih()
            objkon.kon.Close()

        Catch ex As Exception
            MsgBox("Gagal Input")
            MsgBox(ex.Message)
        End Try

        If MessageBox.Show("cetak bukti pembayaran...?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

            bukti_bayar.Show()
            bukti_bayar.CRV.RefreshReport()
            bukti_bayar.CRV.SelectionFormula = "{tbbayar.kode_bayar} ='" & txtkodebayar.Text & "'"
            bukti_bayar.CRV.ReportSource = "cetak_pembayaran.rpt"

        End If
    End Sub

    Private Sub txtbayar_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbayar.KeyPress
        Dim bayar As Integer
        Dim spp As Integer
        bayar = Val(txtbayar.Text)
        spp = Val(txtspp.Text)
        If e.KeyChar = Chr(13) Then
            If txtbayar.Text = "" Then
                MsgBox("Tidak boleh kosong")
                txtbayar.Focus()
            Else
                txtsisa.Text = spp - bayar
                btntambah.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtsemester_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsemester.SelectedIndexChanged
        Try
            Dim rd As OleDbDataReader
            objkon.konek()
            sql = "select * from tbbayar inner join tbmahasiswa on tbbayar.kode_mahasiswa = tbmahasiswa.kode_mahasiswa where semester ='" & txtsemester.SelectedItem & "' AND nama_mahasiswa ='" & txtnama.SelectedItem & "' "
            cmd = New OleDbCommand(sql, objkon.kon)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                MsgBox("Mahasiswa Sudah bayar semester ini")
                bersih()
            End If
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        btnsimpan.Enabled = True
        If btnedit.Text = "SIMPAN" Then
            Dim Pesan As String = MsgBox("Yakin Akan Merubah Data dengan Kode Bayar = " & txtkodebayar.Text & " ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Konfirmasi")
            If Pesan = vbYes Then
                Try
                    If txtkodebayar.Text = "" Then
                        MsgBox("Silahkan pilih data yang akan di Rubah")
                        Exit Sub
                    End If
                    Dim qry As String = "update tbbayar set kode_mahasiswa = '" & txtkodemahasiswa.Text & "',kode_kasir = '" & txtkodekasir.Text & "' , nota_bayar = '" & txtnotabayar.Text & "', tanggal_bayar = '" & txttanggal.Text & "', semester = '" & txtsemester.Text & "', uang_bayar = '" & txtbayar.Text & "', sisa_bayar = '" & txtsisa.Text & "', keterangan = '" & txtketerangan.Text & "' where kode_bayar = '" & txtkodebayar.Text & "'"
                    objkon.konek()
                    Dim cmd As New OleDbCommand(qry, objkon.kon)
                    cmd.ExecuteNonQuery()

                    If txtsisa.Text <= 0 Then
                        Dim editstatus As String = "update tbbayar set keterangan='LUNAS' where kode_bayar='" & txtkodebayar.Text & "'"
                        cmd = New OleDbCommand(editstatus, objkon.kon)
                        cmd.ExecuteNonQuery()
                    ElseIf txtsisa.Text >= 0 Then
                        Dim editstatus As String = "update tbbayar set keterangan='BELUM LUNAS' where kode_bayar='" & txtkodebayar.Text & "'"
                        cmd = New OleDbCommand(editstatus, objkon.kon)
                        cmd.ExecuteNonQuery()
                    End If
                    bersih()
                    objkon.kon.Close()
                    MsgBox("Data Berhasil Dirubah", MsgBoxStyle.Information, "Perhatian")
                    tampil_grid()
                Catch ex As Exception
                    bersih()
                    MsgBox("Data Gagal Di Rubah")
                    MsgBox(ex.Message)
                End Try
                btnedit.Enabled = False
            Else
                bersih()
            End If
        Else
            btnedit.Text = "SIMPAN"
            bukaTextBoxedit()
        End If
    End Sub

    Private Sub btnbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbatal.Click
        kunciTextBox()
        bersih()
    End Sub
End Class