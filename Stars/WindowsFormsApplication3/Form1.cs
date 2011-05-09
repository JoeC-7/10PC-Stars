using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
	public partial class Form1 : Form
	{
		public Form1()
		{
            InitializeComponent();
            List<string[]> testParse = parseCSV("D:\\hygxyz.csv");
			DataTable newTable = new DataTable();
			foreach(string column in testParse[0])
			{
			    newTable.Columns.Add();
			}

			foreach (string[] row in testParse)
			{
			    newTable.Rows.Add(row);
			}
			dataGridView1.DataSource = newTable;
        }//Form1

		public List<string[]> parseCSV(string path)
		{
		  List<string[]> parsedData = new List<string[]>();

		  try
		  {
			using (StreamReader readFile = new StreamReader(path))
			{
			  string line;
			  string[] row;

			  while ((line = readFile.ReadLine()) != null)
			  {
				row = line.Split(',');
				parsedData.Add(row);
			  }
			}
		  }
		  catch (Exception e)
		  {
			MessageBox.Show(e.Message);
		  }

		  return parsedData; // as List
		}// parseCSV
    }// class Form1 : Form
}
