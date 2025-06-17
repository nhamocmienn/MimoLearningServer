namespace WebAPI.Helpers
{
    public static class OtpHelper
    {
        private static Dictionary<string, string> _otpStore = new();

        public static string GenerateOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            _otpStore[email] = otp;
            return otp;
        }

        public static bool VerifyOtp(string email, string otp)
        {
            return _otpStore.TryGetValue(email, out var stored) && stored == otp;
        }

        public static void ClearOtp(string email)
        {
            _otpStore.Remove(email);
        }
    }
}
