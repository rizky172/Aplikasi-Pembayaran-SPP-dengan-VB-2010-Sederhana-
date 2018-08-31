Public Class Laporan

    Private Sub menukasir_Click(sender As System.Object, e As System.EventArgs) Handles menukasir.Click
        CRV.ReportSource = "kasir.rpt"
        CRV.RefreshReport()
    End Sub

    Private Sub menubayar_Click(sender As System.Object, e As System.EventArgs) Handles menubayar.Click
        CRV.ReportSource = "pembayaran.rpt"
        CRV.RefreshReport()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        CRV.ReportSource = "mahasiswa.rpt"
        CRV.RefreshReport()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        End
    End Sub

    Private Sub txtcari_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcari.KeyPress
        If e.KeyChar = Chr(13) Then
            CRV.RefreshReport()
            CRV.SelectionFormula = "{tbkasir.kode_kasir} LIKE'*" & txtcari.Text & "*'"
            CRV.ReportSource = "kasir.rpt"

        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            CRV.RefreshReport()
            CRV.SelectionFormula = "{tbbayar.kode_bayar} LIKE'*" & TextBox3.Text & "*'"
            CRV.ReportSource = "pembayaran.rpt"

        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            CRV.RefreshReport()
            CRV.SelectionFormula = "{tbmahasiswa.kode_mahasiswa} LIKE'*" & TextBox2.Text & "*'"
            CRV.ReportSource = "mahasiswa.rpt"

        End If
    End Sub
End Class