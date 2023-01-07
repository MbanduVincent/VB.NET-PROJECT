Imports MySql.Data.MySqlClient


Public Class Form1
    Dim str As String = "server=localhost; uid=root; pwd=; database=student"
    Dim con As New MySqlConnection(str)

    Sub Load()
        Dim query As String = "select * from Student"
        Dim adpt As New MySqlDataAdapter(query, con)
        Dim ds As New DataSet()
        adpt.Fill(ds, "Student")
        DataGridView1.DataSource = ds.Tables(0)
        con.Close()
        txtRegno.Clear()
        txtname.Clear()
        txtcourse.Clear()
        txtSearch.Clear()
    End Sub
   
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load()
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim row As DataGridViewRow = DataGridView1.CurrentRow
        Try
            txtRegno.Text = row.Cells(0).Value.ToString()
            txtname.Text = row.Cells(1).Value.ToString()
            txtcourse.Text = row.Cells(2).Value.ToString()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim cmd As MySqlCommand
        con.Open()
        Try
            cmd = con.CreateCommand
            cmd.CommandText = "insert into Student(RegNo,Name,Course) values (@RegNo,@Name,@Course);"
            cmd.Parameters.AddWithValue("@Regno", txtRegno.Text)
            cmd.Parameters.AddWithValue("@Name", txtname.Text)
            cmd.Parameters.AddWithValue("@Course", txtcourse.Text)
            cmd.ExecuteNonQuery()
            Load()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click

    End Sub

   
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Load()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim cmd As MySqlCommand
        con.Open()
        Try
            cmd = con.CreateCommand()
            cmd.CommandText = "Update Student set RegNo=@RegNo,Name=@Name,Course=@Course where RegNo=@RegNo"
            cmd.Parameters.AddWithValue("@Regno", txtRegno.Text)
            cmd.Parameters.AddWithValue("@Name", txtname.Text)
            cmd.Parameters.AddWithValue("@Course", txtcourse.Text)
            cmd.ExecuteNonQuery()
            Load()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim cmd As MySqlCommand
        con.Open()
        Try
            cmd = con.CreateCommand()
            cmd.CommandText = "delete from Student where RegNo=@RegNo"
            cmd.Parameters.AddWithValue("@RegNo", txtRegno.Text)
            cmd.ExecuteNonQuery()
            Load()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        Try
            con.Open()
            adapter = New MySqlDataAdapter("select * from Student where name like '%" & txtSearch.Text & "%'", con)
            adapter.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()
            txtRegno.Clear()
            txtname.Clear()
            txtcourse.Clear()
        Catch ex As Exception

        End Try
    End Sub
End Class
