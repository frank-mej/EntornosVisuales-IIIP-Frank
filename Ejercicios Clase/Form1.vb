﻿Imports System.Text.RegularExpressions
Public Class form1
    Dim conexion As New conexion()
    Private Sub frmUsuario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.conectar()
    End Sub

    'username@midominio.com
    Private Function validarCorreo(ByVal isCorreo As String) As Boolean
        Return Regex.IsMatch(isCorreo, "^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")
    End Function


    Private Sub limpiar()
        txtCodigo.Clear()
        txtNombre.Clear()
        txtApellido.Clear()
        txtUsuario.Clear()
        txtContraseña.Clear()
        txtCorreo.Clear()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        insertarUsuaurio()


        If validarCorreo(LCase(txtCorreo.Text)) = False Then
            MessageBox.Show("Correo invalido, *username@midominio.com*", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCorreo.Focus()
            txtCorreo.SelectAll()
        Else
            insertarUsuaurio()
            'MessageBox.Show("Correo valido", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            conexion.conexion.Close()
        End If


    End Sub
    Private Sub insertarUsuaurio()
        Dim idUsuario As Integer
        Dim nombre, apellido, userName, psw, correo, rol, estado As String
        idUsuario = txtCodigo.Text
        nombre = cadenaTexto(txtNombre.Text)
        apellido = cadenaTexto(txtApellido.Text)
        userName = txtUsuario.Text
        psw = txtContraseña.Text
        correo = txtCorreo.Text.ToLower
        estado = "activo"
        rol = cmbRol.Text
        Try
            If conexion.insertarUsuario(idUsuario, nombre, apellido, userName, psw, rol, estado, correo) Then
                MessageBox.Show("Guardado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                limpiar()
            Else
                MessageBox.Show("Error al guardar", "Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function cadenaTexto(ByVal text As String)

        Return StrConv(text, VbStrConv.ProperCase)

    End Function

    Private Sub eliminarUsuario()
        Dim idUsuario As Integer
        Dim rol As String
        idUsuario = txtCodigo.Text
        rol = cmbRol.Text
        Try
            If (conexion.eliminarUsuario(idUsuario, rol)) Then
                MsgBox("Dado de baja")
                'conexion.conexion.Close()
            Else
                MsgBox("Error al dar de baja usuario")
                'conexion.conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub buscarUsuario()
        Dim idUsuario As Integer
        idUsuario = txtCodigo.Text
        Try
            If (conexion.buscarUsuario(idUsuario)) Then
                MsgBox("Se ha encontrado")
            Else
                MsgBox("Error al buscar al usuario")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiar()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        eliminarUsuario()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        buscarUsuario()
    End Sub
End Class
