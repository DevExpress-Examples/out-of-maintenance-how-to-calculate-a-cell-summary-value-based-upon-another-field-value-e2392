using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;

namespace WindowsApplication53
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTable();
            pivotGridControl1.RefreshData();
            pivotGridControl1.BestFit();
        }
        private void PopulateTable()
        {
            DataTable myTable = dataSet1.Tables["Data"];
            myTable.Rows.Add(new object[] {"Aaa", DateTime.Today, 7, "Income"});
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(1), 4, "Outlay" });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today, 12, "Outlay" });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1), 14, "Outlay" });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today, 11, "Income" });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1), 10, "Income" });

            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1), 4, "Income" });
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2, "Income" });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 3, "Income" });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1, "Outlay" });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 8, "Income" });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22, "Outlay" });
        }

        private void pivotGridControl1_CustomSummary(object sender, DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs e)
        {
            if (e.DataField.Name == "fieldValue")
            {
                PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                decimal customValue = 0;
                for (int i = 0; i < ds.RowCount ; i++)
                {
                    object currentType = ds.GetValue(i, "Type");
                    if (currentType != null && currentType.ToString() == "Outlay")
                        customValue -= Convert.ToDecimal(ds.GetValue(i, e.DataField));
                    else
                        customValue += Convert.ToDecimal(ds.GetValue(i, e.DataField));
                }
                e.CustomValue = customValue;
            }
        }
       
    }
}