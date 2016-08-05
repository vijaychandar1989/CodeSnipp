 Private Sub MessageBox1(ByVal msg As String)
        Dim lbl As Label = New Label
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert('" + msg + "')</script>"
        Page.Controls.Add(lbl)
    End Sub