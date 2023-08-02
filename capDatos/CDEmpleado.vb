'import de uso
Imports MySql.Data.MySqlClient
Imports capaEntidad

Public Class CDEmpleado

    Private _conexionDB As String = "Server=127.0.0.1;User=root;Password=;Port=3306;database=pruebaTecnica_vb"

    Public Sub probarConexion()
        Dim Conexion As New MySqlConnection(_conexionDB)

        'usamos el manejador de excepciones
        Try
            Conexion.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'si no hubo problema 
        MessageBox.Show("Conectado!")
        Conexion.Close()

    End Sub

    Public Sub Insertar(ByVal empleado As CEEmpleado)
        Dim Conexion As New MySqlConnection(_conexionDB)
        Conexion.Open()
        Dim Query As String = "INSERT INTO `empleados`(`nombre`, `apellido`, `foto`) VALUES ('" & empleado.Nombre & "','" & empleado.Apellido & "','" & MySql.Data.MySqlClient.MySqlHelper.EscapeString(empleado.Foto) & "')"
        Dim Comando As New MySqlCommand(Query, Conexion)
        Comando.ExecuteNonQuery()
        Conexion.Close()
        MessageBox.Show("Registro Creado")
    End Sub

    Public Sub Actualizar(ByVal empleado As CEEmpleado)
        Dim Conexion As New MySqlConnection(_conexionDB)
        Conexion.Open()
        Dim Query As String = "UPDATE `empleados` SET `nombre`='" & empleado.Nombre & "',`apellido`='" & empleado.Apellido & "',`foto`='" & MySql.Data.MySqlClient.MySqlHelper.EscapeString(empleado.Foto) & "' WHERE `id`='" & empleado.Id & "'"
        Dim Comando As New MySqlCommand(Query, Conexion)
        Comando.ExecuteNonQuery()
        Conexion.Close()
        MessageBox.Show("Registro Actualizado")
    End Sub

    Public Sub Eliminar(ByVal empleado As CEEmpleado)
        Dim Conexion As New MySqlConnection(_conexionDB)
        Conexion.Open()
        Dim Query As String = "DELETE FROM `empleados` WHERE `id`='" & empleado.Id & "'"
        Dim Comando As New MySqlCommand(Query, Conexion)
        Comando.ExecuteNonQuery()
        Conexion.Close()
        MessageBox.Show("Registro Eliminado")
    End Sub

    'listamos datos de DB a mostrar
    Public Function Listar() As DataSet
        Dim Conexion As New MySqlConnection(_conexionDB)
        Conexion.Open()
        Dim Query As String = "SELECT * FROM `empleados` LIMIT 1000"
        Dim Adaptador As MySqlDataAdapter
        Dim dataSet As New DataSet

        Adaptador = New MySqlDataAdapter(Query, Conexion)
        Adaptador.Fill(dataSet, "empleado")

        Return dataSet
    End Function

End Class
