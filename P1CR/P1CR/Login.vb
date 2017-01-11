﻿Public Class Login

    Dim p As Integer = 0
    Dim sqlStr2 As String


    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim table As New DataTable
        Dim sqlStr As String
        Dim userv As String
        Dim passv As String
        Dim statusv As Integer

        sqlStr = "Select  USERID, password, status from S_EMP where USERID='" + txtuser.Text + "'"
        Try
            Dim connectionString As String = oradb

            Dim dataAdapter As New Oracle.ManagedDataAccess.Client.OracleDataAdapter(sqlStr, connectionString)
            Dim commandbuilder As New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(dataAdapter)
            dataAdapter.Fill(table)



            If table.Rows.Count > 0 And p <= 3 Then
                userv = table.Rows(0).ItemArray(0).ToString()
                passv = table.Rows(0).ItemArray(1).ToString()
                statusv = table.Rows(0).ItemArray(2)

                'Validar los 3 intentos
                If txtuser.Text = userv Then
                    If statusv = "1" Then
                        p += 1
                        If txtpass.Text = passv Then
                            MessageBox.Show("¡Login exitoso!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            MainForm.Show()
                            Me.Close()

                        Else
                            MessageBox.Show("Contraseña incorrecta")
                        End If
                    Else
                        MessageBox.Show("Esta cuenta se encuentra inactiva")
                    End If
                Else
                    MessageBox.Show("Este usuario no existe")
                End If

            Else

                MessageBox.Show("Este usuario no existe =(")
            End If

            If p = 3 Then
                Dim conn As New Oracle.ManagedDataAccess.Client.OracleConnection(oradb)
                conn.Open()
                Dim cmd As New Oracle.ManagedDataAccess.Client.OracleCommand
                cmd.Connection = conn
                cmd.CommandText = "UPDATE S_EMP SET STATUS = 0 WHERE USERID ='" + txtuser.Text + "'"
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                conn.Dispose()
                MessageBox.Show("Ha sobrepasado los intentos permitidos, su cuenta ha sido desactivada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Close()
            End If
            'DataGridView1.DataSource = table
            'LAPTOP-ND4K7KG8 conn string = 
        Catch ex As Exception
            MessageBox.Show("La base de datos no se encuentra disponible")
        End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

End Class
