Public Class frmSettings
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDataSource.Text = My.Settings.data_source
        txtInitialCatalog.Text = My.Settings.initial_catalog
        txtUserName.Text = My.Settings.user_id
        txtPassword.Text = My.Settings.password
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            My.Settings.data_source = txtDataSource.Text
            My.Settings.initial_catalog = txtInitialCatalog.Text
            My.Settings.user_id = txtUserName.Text
            My.Settings.password = txtPassword.Text
            MessageBox.Show("SETTINGS SAVED!")
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class