'Hola equipo, este proyecto lo realize en capas con el Solucionador agregando Referencias para enlazarlas,
'es algo que sinceramente voy empezando a emplear.
'Por falta de tiempo para configurar y evitar los errores de SQL Server lanza al instalar en la region
'que no sea inglesa o española, se tomo como medida usar el confiable Mysql
'La aplicacion cumple con sus funciones para el tiempo de desarrollo y espero haber cometido los menores errores posibles
'Gracias

'importamos capa para trabajar dentro de este formulario
Imports capaEntidad
Imports capaNegocio

Public Class frEmpleados
    'Querys agregados para facil acceso al copiar a code y agregar funciones
    'SELECT * FROM `empleados` WHERE 1
    'INSERT INTO `empleados`(`id`, `nombre`, `apellido`, `foto`) VALUES ('[value-1]','[value-2]','[value-3]','[value-4]')
    'UPDATE `empleados` SET `id`='[value-1]',`nombre`='[value-2]',`apellido`='[value-3]',`foto`='[value-4]' WHERE 1
    'DELETE FROM `empleados` WHERE 0

    'variable global para ser accesible en cualquier metodo
    Dim negocioEmpleado As New CNEmpleado()

    Private Sub frEmpleados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cargamos datos a mostrar
        CargarGrid()
    End Sub

    Private Sub CargarGrid()
        gridDatos.DataSource = negocioEmpleado.Listar().Tables("empleado")
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

        Limpiar()

    End Sub

    Private Sub Limpiar()
        'vacio de todos los campos
        txtId.Value = 0
        txtNombre.Text = ""
        txtApellido.Text = ""
        picFoto.Image = Nothing
    End Sub

    Private Sub lnkFoto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkFoto.LinkClicked

        openFoto.ShowDialog()

        'comprobamos que no este vacia la seleccion
        If openFoto.FileName <> "" Then
            picFoto.Load(openFoto.FileName)
        End If

        'vaciamos despues de usar
        openFoto.FileName = ""

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'importamos capa de Entidad para trabajar dentro de este formulario
        Dim empleado As New CEEmpleado()
        Dim validacion As Boolean

        empleado.Id = txtId.Value
        empleado.Nombre = txtNombre.Text
        empleado.Apellido = txtApellido.Text
        empleado.Foto = picFoto.ImageLocation

        'validamos
        validacion = negocioEmpleado.validarEmpleado(empleado)

        If validacion = False Then Exit Sub 'salimos si no es validado

        'MessageBox.Show("¡Guardado!")

        If empleado.Id = 0 Then
            'guardamos datos
            negocioEmpleado.Insertar(empleado)
        Else
            'actualizamos si ya existe
            negocioEmpleado.Actualizar(empleado)
        End If

        'cargamos datos a mostrar
        CargarGrid()

        Limpiar()

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If txtId.Value = 0 Then Exit Sub

        Dim empleado As New CEEmpleado()
        empleado.Id = txtId.Value

        negocioEmpleado.Eliminar(empleado)
        CargarGrid()
        Limpiar()

    End Sub

    Private Sub gridDatos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridDatos.CellDoubleClick
        'damos valores de la seleccion del grid
        txtId.Value = gridDatos.CurrentRow.Cells("id").Value
        txtNombre.Text = gridDatos.CurrentRow.Cells("nombre").Value
        txtApellido.Text = gridDatos.CurrentRow.Cells("apellido").Value

        'comprobamos que no halla sido eliminada la foto
        If gridDatos.CurrentRow.Cells("foto").Value <> "" Then
            If System.IO.File.Exists(gridDatos.CurrentRow.Cells("foto").Value) Then
                picFoto.Load(gridDatos.CurrentRow.Cells("foto").Value)
            End If
        End If
    End Sub

End Class
