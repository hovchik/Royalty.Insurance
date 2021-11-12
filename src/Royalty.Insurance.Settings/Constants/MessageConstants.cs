
namespace Royalty.Insurance.Settings
{
    public static class MessageConstants
    {
        public const string MessageHub = "/messagehub";
        public const string RegexToModifyUser = @"(?<=\@\[\{).+?(?=\}\])";

        public const int MessageMaxLength = 1024;//TODo: maybe move to config or no need?
    }
}
