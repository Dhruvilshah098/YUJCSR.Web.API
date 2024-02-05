using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Common.Constants
{
    public class ErrorMessage
    {
        public const string Save_Message = "Error occured while saving the record(s). Please contact administrator with errorId";
        public const string Update_Message = "Error occured while updating the record(s). Please contact administrator with errorId";
        public const string NoRecordFound_Message = "No record(s) found";
        public const string GenericError_Message = "Error Occured";
        public const string Login_Failure = "Invalid Credential !!! ";

        public const string Tenant_Exist = "The specified EmailID or Mobile number is already exist.";
        public const string LoginID_Empty = "LoginID must not be empty";
        public const string Exam_Min_Price = "The minimum price should be INR 10 per Exam";
        public const string Exam_Min_Duration = "The minimum duration for exam should be 5 min.";
        public const string Exam_Min_Character_length = "The minimum characters should be 100 characters.";
        public const string Exam_Name_Exists = "Another Exam with same name is created by you";
    }
}
