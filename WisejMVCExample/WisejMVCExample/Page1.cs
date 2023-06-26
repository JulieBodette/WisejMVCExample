using System;
using System.Collections.Generic;
using Wisej.Web;

namespace WisejMVCExample
{
	public partial class Page1 : Page
	{
		List<StudentModel> studentdata = new List<StudentModel>();
		public Page1()
		{
			InitializeComponent();
		}

		private void Page1_Load(object sender, System.EventArgs e)
		{
			studentdata = StudentModel.GetStudents();
			dataGridView1.DataSource = studentdata;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//read the data from the view
			int id = Int32.Parse(txtId.Text);
			string email = txtEmail.Text;
			string name = txtName.Text;
			int age = Int32.Parse(txtAge.Text);


			StudentModel model = new StudentModel() { Name = name, Id = id, Age = age, Email = email };
			string errorMessage = model.AddStudent();
			AlertBox.Show(errorMessage);

			//clear the textboxes
			txtId.Text = "";
			txtEmail.Text = "";
			txtName.Text = "";
			txtAge.Text = "";

			//Get the data from the database and show it in the view so that the new student is seen
			studentdata = StudentModel.GetStudents();
			dataGridView1.DataSource = studentdata;
		}
	}
}
