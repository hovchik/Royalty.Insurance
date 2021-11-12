
using System.Buffers;

namespace Royalty.Insurance.Settings
{
    public static class ResourceCommonMessage
    {
        public const string EmailNotFound = "Email does not exist";
        public const string UserOrPassword = "Email or password is incorrect";
        public const string ActivationPeriod = "Activation code is expired";
        public const string UserNotActive = "User is not active";
        public const string UserTemporaryPassword = "User password is temporary, please change it";
        public const string InvalidToken = "Invalid token";
        public const string InvalidRefreshToken = "Invalid Rrfresh token";
        public const string TokenExpired = "Token is expired";
        public const string UserNotFound = "User does not exist";
        public const string UserAlreadyDeactivated = "User is already deactivated";
        public const string SaveFailed = "Save is failed";
        public const string ErrorOccurred = "Error occurred during the process of the request";
        public const string RecordNotFound = "Record not found";
        public const string AlreadyExistingRecord = "Already existing record";
        public const string InsuredNotFound = "Insured not found";
        public const string Unauthorized = "Unauthorized Access";
        public const string MissingAuthorizationHeader = "Missing Authorization Header";
        public const string VerificationCodeIsInvalid = "Verification code is invalid";
        public const string AuthenticatorVerifiedMessage = "Your authenticator app has been verified";
        public const string AuthenticatorFailedMessage = "Your authenticator app has been failed to verify";
        public const string AuthenticatorAlreadyAdded = "Authenticator app has been already added";
        public const string EntityNotFound = "Entity does not exist";
        public const string CreatorCanAddMember = "Only group creator can add member";
        public const string CreatorCanRemoveMember = "Only group creator can remove member";
        public const string UserIsNotMember = "User who created group can not leave. Only creator can remove other memebers from the group";
        public const string UserNotIdentified = "User cannot be identified.";
        public const string AlreadyAddedMember = "Already added member";
        public const string GroupDoesNotExists = "Group does not exists";
        public const string UserIsNotAdmin = "User is not admin";
        public const string EmailActivationSubject = "Email Activation Code";
        public const string EmailActivationBody = "Email Activation Code is {0}";
        public const string EmailAddressIsNotValid = "Email address is not valid";
        public const string AccountIsBlocked = "Your account has been blocked, to unblock please contact the admin";
        public const string AccountBlocked = "Account blocked";
        public const string UserAccountIsBlocked = "{0} is blocked";
        public const string PasswordIsNotValid =
            "Invalid password, minimum length 8, minimum one non alphanumeric character, minimum  one uppercase character";
        public const string EmailForgetPasswordSubject = "Forget password Code";
        public const string EmailForgetPasswordBody = "To reset your password please use the code. The code is {0}";
        public const string LargeFileUploading = "Uploaded file is large."; // temporary message
        public static string MessageMaxLength = $"Message body max length is {MessageConstants.MessageMaxLength}";
        public static string UploadFailed = "Upload is failed";
        public const string CarrierNotFoundByCondition = "Cannot find any carrier by condition";
        public static string DeleteFailed = "Delete is failed";
        public const string UpdateFailed = "Update is Failed";
        public const string ResourceDoesNotExistsOrExpired = "Resource does not exists or expired";
        public const string FileAlreadyExists = "File already exists";
        public const string CanNotDeleteSystemTaskStatus = "System status can not be deleted";
        public const string CanNotSentEmail = "System can not sent email, please try again later";
        public const string CanNotDeleteTaskStatus =
            "Can not delete task status, it is already has been used in a task";

        public const string CanNotChangeTaskStatus = "Can not change completed task status";
        public const string OnlyAdmin = "Only admin can perform the operation";
        public const string ArgumentOutOfRange = "Argument out of range, name {0}";
        public const string GivenPropertyDoesNotExists = "The key does not exists in the document";
        public const string BodyIdQueryIdMatch = "Body id should match with query id";
        public const string ChargeTotalOverflow = "Refund charge total is greater than Sale";
        public const string TransactionRollBack = "Save transaction rollbacked";
        public const string PaymentDeclined = "Payment declined, please check input data.";
        public const string VinCheckFailed = "Incomplete VIN. Invalid Characters Present.";
        public const string InvalidCalculation = "Invalid calculation. Please check input values. Total is 0";
        public const string TemplateNotFound = "Template not found";

        public const string DocumentAlreadyHasInsured= "Document already has insured assigned.";
    }
}