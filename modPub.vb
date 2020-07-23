Module modPub
    Public Function ConStr() As String
        Dim _ConStr As String = ""
        Try
            _ConStr = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", _
                                My.Settings.data_source, _
                                My.Settings.initial_catalog,
                                My.Settings.user_id,
                                My.Settings.password)

        Catch ex As Exception
            MsgBox(ex.message)
        End Try
        Return _ConStr
    End Function

    Public Path As String = System.IO.Path.GetDirectoryName( _
    System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName)
    Public guser_id As Integer
    Public gacct_type As Integer
    Public guser_name As String

    Public Function IsInt(ByVal Value As Object) As Boolean
        Try
            Return IsNumeric(Value)
        Catch e As FormatException
            Return False
        End Try
    End Function
End Module
