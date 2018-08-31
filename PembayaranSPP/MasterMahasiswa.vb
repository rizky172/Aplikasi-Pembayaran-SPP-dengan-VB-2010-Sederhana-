Imports System.IO
Imports System.DateTime
Imports System.Data.OleDb
Public Class MasterMahasiswa
    Dim objkon As New koneksi
    Dim sql As String
    Dim cmd As OleDbCommand
    Dim baca As OleDbDataReader
    Private Sub MasterMahasiswa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtfoto.Image = Image.FromFile("kosong.jpg")
        tampil_grid() 'SEMENTARA BISA DI SKIP
        kunciTextBox() 'SEMENTARA BISA DI SKIP
        kodemahasiswa()
        btnsimpan.Enabled = False 'SEMENTARA BISA DI SKIP

    End Sub

    Private Sub tampil_grid()
        Try
            objkon.konek()
            Dim qry As New OleDbDataAdapter("Select * from tbmahasiswa", objkon.kon)
            Dim dt As New DataSet
            qry.Fill(dt, "tbmahasiswa")
            DataGridView1.DataSource = dt.Tables("tbmahasiswa")
            DataGridView1.Columns(0).Width = 100
            DataGridView1.Columns(1).Width = 70
            DataGridView1.Columns(2).Width = 100
            DataGridView1.Columns(3).Width = 100
            DataGridView1.Columns(4).Width = 50
            DataGridView1.Columns(5).Width = 90
            DataGridView1.Columns(6).Width = 90
            DataGridView1.Columns(7).Width = 90
            DataGridView1.Columns(8).Width = 100
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub kunciTextBox()
        txtnama.Enabled = False
        txtnim.Enabled = False
        txtkode.Enabled = False
        txttahun.Enabled = False
        txttanggal.Enabled = False
        btnbuka.Enabled = False
        txtjurusan.Enabled = False
        txtalamat.Enabled = False
        txtspp.Enabled = False
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        txtkode.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtnim.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtnama.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txttanggal.Value = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        kunciTextBox()
        objkon.konek()

        'menampikan foto
        Dim CariPhoto As String = "SELECT foto from tbmahasiswa where kode_mahasiswa= '" & txtkode.Text & "' "
        Dim cmdPhoto As New OleDbCommand(CariPhoto, objkon.kon)
        Dim readPhoto As OleDbDataReader = cmdPhoto.ExecuteReader
        readPhoto.Read()
        If readPhoto.HasRows Then
            Dim imgByte() As Byte
            imgByte = CType(readPhoto(0), Byte())
            Dim ms As New MemoryStream(imgByte)
            Dim gambar As New Bitmap(ms)
            txtfoto.Image = gambar
            txtfoto.Refresh()
            ms.Close()
        End If
        txtjurusan.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        txttahun.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        txtspp.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value
        txtalamat.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value
        objkon.kon.Close()
        objkon.kon.Dispose()
        btnhapus.Enabled = True
        btnsimpan.Enabled = False
        btnsimpan.Text = "SIMPAN"
        btnedit.Enabled = True
        btnedit.Text = "EDIT"
    End Sub

    Sub bersih()
        txtnama.Text = ""
        txtnama.Enabled = False
        txtnim.Text = ""
        txtnim.Enabled = False
        txtjurusan.Enabled = False
        txtjurusan.Text = "Jurusan"
        txttanggal.Enabled = False
        txtalamat.Text = ""
        txttahun.Enabled = False
        txttahun.Text = "Pilihan"
        txtalamat.Enabled = False
        btnbuka.Enabled = False
        txtspp.Text = ""

        txtfoto.Image = Image.FromFile("kosong.jpg")
        txtfoto.SizeMode = PictureBoxSizeMode.StretchImage
        btnsimpan.Enabled = False
        btnhapus.Enabled = False
        btnedit.Enabled = False
        btnedit.Text = "EDIT"
        btntambah.Text = "TAMBAH"
    End Sub

    Sub bukaTextBox()
        txtnama.Enabled = True
        txtnim.Enabled = True
        txtjurusan.Enabled = True
        txttahun.Enabled = True
        txttanggal.Enabled = True
        txtalamat.Enabled = True
        btnbuka.Enabled = True
    End Sub

    Sub kodemahasiswa()
        Dim kd_lama As String = ""
        Dim kd_baru As String = ""
        Dim NoBarang As String = ""
        Try
            objkon.konek()
            sql = "Select * from tbmahasiswa order by kode_mahasiswa desc"
            cmd = New OleDbCommand(sql, objkon.kon)
            baca = cmd.ExecuteReader
            If baca.Read Then
                kd_lama = Mid(baca.Item("kode_mahasiswa"), 3, 3)
            Else
                txtkode.Text = "MH001"
            End If
            kd_baru = Val(kd_lama) + 1
            NoBarang = "MH" & Mid("000", 1, 3 - kd_baru.Length) & kd_baru
            txtkode.Text = NoBarang
            objkon.kon.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnhapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhapus.Click
        Try
            Dim Hapus As String = "delete from tbmahasiswa where kode_mahasiswa = '" & txtkode.Text & "' "
            Dim Pesan As String = MsgBox("Yakin Akan Menghapus Data dengan Kode Mahasiswa = " & txtkode.Text & " ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Konfirmasi")
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

    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        bersih()
        kodemahasiswa()
        bukaTextBox()
        btnsimpan.Enabled = True
        btntambah.Text = "BATAL"
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        If btnsimpan.Text = "BATAL" Then
            btnsimpan.Text = "SIMPAN"
            btnedit.Enabled = True
            btnedit.Text = "EDIT"
            btnhapus.Enabled = True
            btnsimpan.Enabled = False
            tampil_grid()
            kunciTextBox()
        Else
            btnsimpan.Text = "SIMPAN"
            Dim qry As String = "INSERT INTO tbmahasiswa values ('" & txtkode.Text & "','" & txtnim.Text & "','" & txtnama.Text & "','" & txttanggal.Value & "' ,@foto, '" & txtjurusan.Text & "', '" & txttahun.Text & "', '" & txtspp.Text & "','" & txtalamat.Text & "')"
            Try
                objkon.konek()
                Dim imgByte() As Byte
                Dim ms As New MemoryStream
                Dim gambar As New Bitmap(txtfoto.Image)
                gambar.Save(ms, Imaging.ImageFormat.Jpeg)
                imgByte = ms.ToArray

                Dim cmd As New OleDbCommand(qry, objkon.kon)
                cmd.Parameters.AddWithValue("@foto", imgByte)
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
        End If
    End Sub

    Private Sub btnbuka_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuka.Click
        OpenFileDialog1.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|GIF Files(*.gif)|*.gif|PNG Files(*.png)|*.png|BMP Files(*.bmp)|*.bmp|TIFF Files(*.tiff)|*.tiff"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtfoto.SizeMode = PictureBoxSizeMode.StretchImage
            txtfoto.Image = Image.FromFile(OpenFileDialog1.FileName)
            btnsimpan.Enabled = True
            btnsimpan.Focus()
            If btnedit.Text = "SIMPAN PERUBAHAN" Then
                btnsimpan.Enabled = False
            Else
                btnsimpan.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        btnsimpan.Enabled = True
        btnsimpan.Text = "BATAL"
        If btnedit.Text = "SIMPAN PERUBAHAN" Then
            Dim Pesan As String = MsgBox("Yakin Akan Merubah Data dengan Kode Mahasiswa = " & txtkode.Text & " ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Konfirmasi")
            If Pesan = vbYes Then
                Try
                    If txtkode.Text = "" Then
                        MsgBox("Silahkan pilih data yang akan di Rubah")
                        Exit Sub
                    End If
                    Dim qry As String = "update tbmahasiswa set nama_mahasiswa = '" & txtnama.Text & "',nim = '" & txtnim.Text & "' , tanggal_lahir = '" & txttanggal.Value & "', foto = @photo, jurusan = '" & txtjurusan.Text & "',  tahun_akademik = '" & txttahun.Text & "', spp = '" & txtspp.Text & "', alamat_mahasiswa = '" & txtalamat.Text & "' where kode_mahasiswa = '" & txtkode.Text & "'"
                    objkon.konek()
                    Dim imgByte() As Byte
                    Dim ms As New MemoryStream
                    Dim gambar As New Bitmap(txtfoto.Image)
                    gambar.Save(ms, Imaging.ImageFormat.Jpeg)
                    imgByte = ms.ToArray
                    Dim cmd As New OleDbCommand(qry, objkon.kon)
                    cmd.Parameters.AddWithValue("@photo", imgByte)
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


    Private Sub txttahun_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttahun.SelectedIndexChanged
        If txttahun.SelectedItem = "2015/2016" Then
            txtspp.Text = "2400000"
            ElseIf txttahun.SelectedItem = "2016/2017" Then
                txtspp.Text = "2500000"
            ElseIf txttahun.SelectedItem = "2017/2018" Then
                txtspp.Text = "2600000"
            End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        kunciTextBox()
        bersih()
    End Sub
End Class