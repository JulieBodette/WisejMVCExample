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

		// Returns a string either indicating that the given StudentModel is valid, or with an error message explaining which part(s) is invalid
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

		// Looks up the database connection string. The database connection string is stored in Web.config.
		public static string CnnVal(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		// returns a list of StudentModel objects from the database
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
			string validMessage = "Added new student to the database.";

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
			// If the data is not valid, add a bit to the message informing the user that the student was not added to the database.
            else
            {
				message += " New student was not added to database.";
            }

            return message;
		}
	}
}
