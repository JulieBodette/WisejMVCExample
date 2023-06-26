using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;

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

		static public string ValidateData(StudentModel model)
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
				message += "The data is valid!";
			}
			return message;

		}

		public static string CnnVal(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public string AddStudent()
		{
			//returns a string with the validation errors
			string errorMessage = StudentModel.ValidateData(this);

			//To Do: Add code to break here if the data is not valid

			//add the data to the database
			//StudentModel needs to have the method that interacts with the database itself???
			//Do I need to move this code?
			using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(StudentModel.CnnVal("Students")))
			{
				connection.Execute("INSERT INTO Students VALUES (@Id, @Email, @Name, @Age)", this);
				//note: The order and exact case-sensitive text of the values @Id, @Email, @Name, @Age MUST match the database
			}


			return errorMessage;
		}
	}
}
