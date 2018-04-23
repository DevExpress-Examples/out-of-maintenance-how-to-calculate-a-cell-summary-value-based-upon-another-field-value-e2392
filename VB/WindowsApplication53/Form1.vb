Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid

Namespace WindowsApplication53
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			PopulateTable()
			pivotGridControl1.RefreshData()
			pivotGridControl1.BestFit()
		End Sub
		Private Sub PopulateTable()
			Dim myTable As DataTable = dataSet1.Tables("Data")
			myTable.Rows.Add(New Object() {"Aaa", DateTime.Today, 7, "Income"})
			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddDays(1), 4, "Outlay" })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today, 12, "Outlay" })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddDays(1), 14, "Outlay" })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today, 11, "Income" })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddDays(1), 10, "Income" })

			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1), 4, "Income" })
			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2, "Income" })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddYears(1), 3, "Income" })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1, "Outlay" })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddYears(1), 8, "Income" })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22, "Outlay" })
		End Sub

		Private Sub pivotGridControl1_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs) Handles pivotGridControl1.CustomSummary
			If e.DataField.Name = "fieldValue" Then
				Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
				Dim customValue As Decimal = 0
				For i As Integer = 0 To ds.RowCount - 1
					Dim currentType As Object = ds.GetValue(i, "Type")
					If currentType IsNot Nothing AndAlso currentType.ToString() = "Outlay" Then
						customValue -= Convert.ToDecimal(ds.GetValue(i, e.DataField))
					Else
						customValue += Convert.ToDecimal(ds.GetValue(i, e.DataField))
					End If
				Next i
				e.CustomValue = customValue
			End If
		End Sub

	End Class
End Namespace