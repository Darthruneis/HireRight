namespace HireRight.BusinessLogic
{
    public static class RegularExpressions
    {
        public const string PhoneNumber = 
            @"^(?:\+?(\d{1,3}))?([ ])?([(])?(\d{3})([)])?([-. ])?(\d{3})([-. ])?(\d{4})$";
        public const string EmailAddress = 
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    }
}