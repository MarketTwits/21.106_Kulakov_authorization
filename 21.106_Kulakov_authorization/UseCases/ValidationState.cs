using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21._106_Kulakov_authorization.UseCases
{
    public class ValidationState
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }

        public ValidationState()
        {
            IsValid = true;
            Errors = new List<string>();
        }
    }
    public class ValidationUseCase
    {
        public ValidationState ValidateData(string name, string password)
        {
            var state = new ValidationState();

            if (string.IsNullOrEmpty(name))
            {
                state.IsValid = false;
                state.Errors.Add("Name is required.");
            }
            if (password == null)
            {
                state.IsValid = false;
                state.Errors.Add("Age must be at least 18 years old.");
            }
            if(password.Length <= 3)
            {
                state.IsValid = false;
                state.Errors.Add("Password should be more then 4");
            }
            return state;
        }
    }
}
