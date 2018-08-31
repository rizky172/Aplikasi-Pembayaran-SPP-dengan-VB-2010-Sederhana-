Public Class MenuUtama

    Private Sub menukasir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menukasir.Click
        MasterKasir.Show()
    End Sub

    Private Sub menumahasiswa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menumahasiswa.Click
        MasterMahasiswa.Show()
    End Sub

    Private Sub menubayar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menubayar.Click
        MasterBayar.Show()
    End Sub

    Private Sub menulaporan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menulaporan.Click
        Laporan.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub MenuUtama_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class