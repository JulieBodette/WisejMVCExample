using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;

namespace WisejMVCExample
{
	public class StudentModel
	{
		[Range(1, 999, ErrorMessage = "Id must be between 1 and 999")]
		public int Id { get; set; }

		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		public string Name { get; set; }
		public int Age { get; set; }

		static public string ValidateData(StudentModel model, string validmessage)
		{
			string message = "";
			ValidationContext context = new ValidationContext(model, null, null);
			List<ValidationResult> validationResults = new List<ValidationResult>();
			bool valid = Validator.TryValidateObject(model, context, validationResults, true);
			if (!valid)
			{
				foreach (ValidationResult validationResult in
				validationResults)
				{
					message += validationResult.ErrorMessage + " \n";
				}
			}
			else
			{
				message = validmessage;
			}
			return message;

		}

		public static string CnnVal(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public static List<StudentModel> GetStudents()
		{
			using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(StudentModel.CnnVal("Students")))
			{
				var output = connection.Query<StudentModel>("select * from Students").ToList();
				return output;
			}
		}

		// Adds the model to the database. Returns a string which contains an error message if the model is invalid.
		public string AddStudent()
		{
			string validMessage = "The data is valid!";

			// ValidateData returns a string with the validation errors. Returns validMessage if the data is valid.
			string message = StudentModel.ValidateData(this, validMessage);

			// if the data in the model is valid, add the student to the database
			if (message == validMessage)
			{

				//add the data to the database
				using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(StudentModel.CnnVal("Students")))
				{
					connection.Execute("INSERT INTO Students VALUES (@Id, @Email, @Name, @Age)", this);
					//note: The order and exact case-sensitive text of the values @Id, @Email, @Name, @Age MUST match the database
				}
			}

			return message;
		}
	}
}
