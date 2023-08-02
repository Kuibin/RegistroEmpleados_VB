'importamos para poder usar
Imports capaEntidad
Imports capDatos

Public Class CNEmpleado

    Dim datosEmpleado As New CDEmpleado()

    Public Function validarEmpleado(ByVal empleado As CEEmpleado) As Boolean
        Dim Resultado As Boolean = True

        'validamos campos que no esten vacios
        If empleado.Nombre = "" Then
            Resultado = False
            MessageBox.Show("Nombre Obligatorio")
        End If
        If empleado.Apellido = "" Then
            Resultado = False
            MessageBox.Show("Apellido Obligatorio")
        End If

        Return Resultado
    End Function

    'para testear
    Public Sub mysqlPrueba()
        datosEmpleado.probarConexion()
    End Sub

    Public Sub Insertar(ByVal empleado As CEEmpleado)
        datosEmpleado.Insertar(empleado)
    End Sub
    Public Sub Actualizar(ByVal empleado As CEEmpleado)
        datosEmpleado.Actualizar(empleado)
    End Sub
    Public Sub Eliminar(ByVal empleado As CEEmpleado)
        'validacion para preguntar al usuario si desea realizar la accion
        If MessageBox.Show("¿Desea Eliminar al usuario?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            datosEmpleado.Eliminar(empleado)
        End If
    End Sub

    Public Function Listar() As DataSet
        Return datosEmpleado.Listar
    End Function
End Class
