Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration

Public Class DataHandler

    ' This class is used to buffer database information locally within the application space
    ' so that DB calls are not required, which should improve the responsiveness of the app. 


    Public allData As New DataSet
    Private connectionString As String


    Public Sub New(connectionStringKey As String)

        connectionString = ConfigurationManager.ConnectionStrings(connectionStringKey).ConnectionString

    End Sub


    Public Function TestConnection() As Boolean

        Try
            Using synergyConnection As New SqlConnection(connectionString)
                synergyConnection.Open()
                If (synergyConnection.State & ConnectionState.Open) > 0 Then
                    Return True
                End If
                synergyConnection.Close()
            End Using
        Catch
            ' Problem opening connection. Don't re-throw error, just return false. 
        End Try

        Return False
    End Function


    Public Sub LoadDataTable(tableName As String, procedureKey As String,
                Optional params As Dictionary(Of String, String) = Nothing)

        ' Read data from the DB and load it into a local table owned by this object. 
        ' Expects a stored procedure, will not work for simple queries (TODO: enable this?)

        ' Note that dictionary parameters can be passed using the syntax:
        '   New Dictionary(Of String, String) From {{"a", "valA"}, {"b", "valB"}}

        Using synergyConnection As New SqlConnection(connectionString)
            synergyConnection.Open()

            Using command As New SqlCommand(
                        ConfigurationManager.AppSettings(procedureKey), synergyConnection)

                command.CommandType = CommandType.StoredProcedure

                If params IsNot Nothing Then
                    For Each parameter In params
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value)
                    Next
                End If

                Using adapter As New SqlDataAdapter

                    adapter.SelectCommand = command
                    adapter.Fill(allData, tableName)

                End Using
            End Using
        End Using

    End Sub

End Class
