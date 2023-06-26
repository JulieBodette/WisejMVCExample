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

		/// Add a new student to the database using the values from the text fields.
		private void button1_Click(object sender, System.EventArgs e)
		{
			//check to make sure that all the text fields have text (ie they are not the empty string)
			if (txtId.Text != "" && txtEmail.Text != "" && txtName.Text != "" && txtAge.Text != "")
			{
				AlertBox.Show("all text boxes have text");

				//read the data from the text fields in the view
				int id = Int32.Parse(txtId.Text);
				string email = txtEmail.Text;
				string name = txtName.Text;
				int age = Int32.Parse(txtAge.Text);

				// create a StudentModel based on the data in the text fields.
				StudentModel model = new StudentModel() { Name = name, Id = id, Age = age, Email = email };
				// Attempt to add the student to the database- show a sucess or failure message.
				string message = model.AddStudent();
				AlertBox.Show(message);

				//clear the textboxes
				txtId.Text = "";
				txtEmail.Text = "";
				txtName.Text = "";
				txtAge.Text = "";

				//Get the data from the database and show it in the view so that the new student is seen
				studentdata = StudentModel.GetStudents();
				dataGridView1.DataSource = studentdata;
			}
			else //if at least one of the text fields is blank
			{
				AlertBox.Show("Please enter values for all fields.");
			}
		}
	}
}
