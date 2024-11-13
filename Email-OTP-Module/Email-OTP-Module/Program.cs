using Email_OTP_Module;
using static Email_OTP_Module.Constants;
using static Email_OTP_Module.Wrapper.ConsoleWrapper;

class Program
{
    static void Main(string[] args)
    {
        string userEmail = null;
        bool isValidEmail = false;
        EmailOTPModule emailOtpModule = new EmailOTPModule();

        // Start the email OTP module
        emailOtpModule.Start();

        while (string.IsNullOrEmpty(userEmail) || !isValidEmail)
        {
            Write("Please enter email: ");
            userEmail = Console.ReadLine();

            if (string.IsNullOrEmpty(userEmail))
            {
                isValidEmail = false;
                WriteLine("Email cannot be empty. Please try again.");
                continue;
            }

            // Generating and sending OTP to a user email
            int emailStatus = emailOtpModule.GenerateOTPEmail(userEmail);

            // Check the status after sending the email
            if (emailStatus == STATUS_EMAIL_OK)
            {
                isValidEmail = true;
                WriteLine("OTP generated and email sent successfully.");

                // Now check the OTP entered by the user
                int otpCheckStatus = emailOtpModule.CheckOTP();

                // Handle the OTP verification result
                if (otpCheckStatus == STATUS_OTP_OK)
                    WriteLine("OTP verified successfully.");
                else if (otpCheckStatus == STATUS_OTP_FAIL)
                    WriteLine("OTP verification failed after 10 attempts.");
                else if (otpCheckStatus == STATUS_OTP_TIMEOUT)
                    WriteLine("OTP verification failed due to timeout.");
            }
            else if (emailStatus == STATUS_EMAIL_INVALID)
            {
                isValidEmail = false;
                WriteLine("The email address is invalid.");
            }
            else
                WriteLine("Sending to the email has failed.");
        }

        // Close the email OTP module
        emailOtpModule.Close();
    }
}
