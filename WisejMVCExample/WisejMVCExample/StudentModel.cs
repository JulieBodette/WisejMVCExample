using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
	}
}
