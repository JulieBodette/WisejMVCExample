using System;
using System.Collections.Generic;
using Wisej.Web;

namespace WisejMVCExample
{
	public partial class Page1 : Page
	{
		List<StudentModel> studentdata = new List<StudentModel>();
		StudentController controller = new StudentController();
		public Page1()
		{
			InitializeComponent();
		}

		private void Page1_Load(object sender, System.EventArgs e)
		{
			studentdata = controller.GetStudents();
			dataGridView1.DataSource = studentdata;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//read the data from the view
			int id = Int32.Parse(txtId.Text);
			string email = txtEmail.Text;
			string name = txtName.Text;
			int age = Int32.Parse(txtAge.Text);

			string errorMessage = controller.AddStudent(name, id, age, email);
			AlertBox.Show(errorMessage);

			//clear the textboxes
			txtId.Text = "";
			txtEmail.Text = "";
			txtName.Text = "";
			txtAge.Text = "";

			//TODO: Refresh the view to show the new data
		}
	}
}
