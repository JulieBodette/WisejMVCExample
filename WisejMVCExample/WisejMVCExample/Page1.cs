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
	}
}
