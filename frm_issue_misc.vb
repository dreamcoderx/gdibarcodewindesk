Imports System.Data.SqlClient
Public Class frm_issue_misc
    Dim dtColName As New DataTable

    Private Sub frm_issue_misc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_cbo()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As New SqlCommand

        Try
            Cursor.Current = Cursors.WaitCursor
            If Not conecDB_OK() Then Return
            With cmd
                .Connection = connDB
                .CommandText = "issue_misc_proc"
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0
                .Parameters.AddWithValue("@doc_no", txt_doc_no.Text)
                .Parameters.AddWithValue("@item_code", cboItemCode.SelectedValue)
                .Parameters.AddWithValue("@qty_issued", txtQuantity.Text)
                .Parameters.AddWithValue("@uom_issued", cboUom.Text)
                .Parameters.AddWithValue("@batch_code", DBNull.Value)
                .Parameters.AddWithValue("@prod_date", DBNull.Value)
                .Parameters.AddWithValue("@exp_date", DBNull.Value)
                .Parameters.AddWithValue("@po_number", DBNull.Value)
                .Parameters.AddWithValue("@user_id", guser_id)
                .Parameters.AddWithValue("@serial_alpha", DBNull.Value)
                .Parameters.AddWithValue("@serial_no", DBNull.Value)
                .Parameters.AddWithValue("@remarks", txtRemarks.Text)
                .Parameters.Add("@rv", SqlDbType.Int).Direction = ParameterDirection.Output
                .Parameters.Add("@msg", SqlDbType.NVarChar, 350).Direction = ParameterDirection.Output
                .ExecuteNonQuery()

                If .Parameters("@rv").Value = 0 Then
                    MessageBox.Show("Transaction Successful!")
                Else
                    MessageBox.Show(.Parameters("@msg").Value.ToString)
                End If

            End With
        Catch exs As SqlException
            MsgBox(exs.Message, MsgBoxStyle.Critical, "SQL EXCEPTION")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "VB EXCEPTION")
        Finally
            cmd.Dispose()
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub load_cbo()
        Dim cmd As New SqlCommand
        Try
            Cursor.Current = Cursors.WaitCursor
            If Not conecDB_OK() Then Return
            With cmd
                .Connection = connDB
                .CommandText = "SELECT [ITEM_CODE],[DESCRIPTION],[SOH], [UOM],[S_UOM],[COEFFICIENT],[P_UOM_NAME] FROM [tblItemCode] order by description"
                .CommandType = CommandType.Text

                Dim dap As New SqlDataAdapter(cmd)
                dap.Fill(dtColName)
                cboItemCode.DisplayMember = "ITEM_CODE"
                cboItemCode.ValueMember = "ITEM_CODE"
                cboItemCode.DataSource = dtColName

                cboDescription.DisplayMember = "DESCRIPTION"
                cboDescription.ValueMember = "ITEM_CODE"
                cboDescription.DataSource = dtColName



            End With
        Catch exs As SqlException
            MsgBox(exs.Message, MsgBoxStyle.Critical, "SQL EXCEPTION")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "VB EXCEPTION")
        Finally
            cmd.Dispose()
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cboItemCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboItemCode.SelectedIndexChanged
        Try
            cboUom.Items.Clear()
            cboUom.Items.Add(dtColName.Rows(cboItemCode.SelectedIndex).Item("UOM"))
            If Not dtColName.Rows(cboItemCode.SelectedIndex).Item("S_UOM") Is DBNull.Value Then
                cboUom.Items.Add(dtColName.Rows(cboItemCode.SelectedIndex).Item("S_UOM"))
            End If
            cboUom.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub
End Class