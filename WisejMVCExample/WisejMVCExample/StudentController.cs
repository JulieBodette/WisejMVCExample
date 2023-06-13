using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;

namespace WisejMVCExample
{
	public class StudentController
	{
		public static string CnnVal(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public List<StudentModel> GetStudents()
		{
			using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(StudentController.CnnVal("Students")))
			{
				var output = connection.Query<StudentModel>("select * from Students").ToList();
				return output;
			}
		}

		//attempts to add a student to the database
		public string AddStudent(string name, int id, int age, string email)
		{

			//returns a string with the validation errors
			StudentModel model = new StudentModel() { Name = name, Id = id, Age = age, Email = email };
			string errorMessage = StudentModel.ValidateData(model);

			//To Do: Add code to break here if the data is not valid

			//add the data to the database
			//StudentModel needs to have the method that interacts with the database itself???
			//Do I need to move this code?
			using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(StudentController.CnnVal("Students")))
			{
				connection.Execute("INSERT INTO Students VALUES (@Id, @Email, @Name, @Age)", model);
				//note: The order and exact case-sensitive text of the values @Id, @Email, @Name, @Age MUST match the database
			}
			

			return errorMessage;
		}

	}
}
