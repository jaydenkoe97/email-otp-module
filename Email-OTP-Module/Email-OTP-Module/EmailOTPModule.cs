using System.Text.RegularExpressions;
using System.Timers;
using static Email_OTP_Module.Constants;
using static Email_OTP_Module.Wrapper.ConsoleWrapper;
using Timer = System.Timers.Timer;

namespace Email_OTP_Module
{
    public class EmailOTPModule
    {
        public string currentOtp = null;
        private Timer timeoutTimer;
        private int attempts = 0;
        private bool otpVerified = false;

        // This method starts the module
        public void Start()
        {
            WriteLine("Email OTP Module started.");
        }

        // This method stops the module
        public void Close()
        {
            WriteLine("Email OTP Module closed.");
            Environment.Exit(0);
        }

        // This method generates and sends an OTP to the user's email address
        public int GenerateOTPEmail(string userEmail)
        {
            // Validate email
            if (!IsValidEmail(userEmail))
                return STATUS_EMAIL_INVALID;

            // Generate a 6-digit OTP code
            currentOtp = GenerateOTP();

            // Simulate sending an email
            string emailBody = $"Your OTP Code is {currentOtp}. The code is valid for 1 minute.";
            if (SendEmail(userEmail, emailBody))
            {
                WriteLine($"Email sent to {userEmail} with OTP {currentOtp}");
                return STATUS_EMAIL_OK;
            }
            else
                return STATUS_EMAIL_FAIL;
        }

        // This method checks the OTP entered by the user (with a timeout and limited attempts)
        public int CheckOTP()
        {
            // Set up a timer to enforce the 1-minute timeout
            timeoutTimer = new Timer(60000);
            timeoutTimer.Elapsed += TimeoutExpired;
            timeoutTimer.Start();

            // Allow the user to enter the OTP
            while (attempts < 10 && !otpVerified)
            {
                Write("Please enter OTP: ");
                string userOTP = ReadLine();

                if (string.IsNullOrEmpty(userOTP))
                {
                    attempts++;
                    WriteLine("OTP cannot be empty. Please try again.");
                    continue;
                }

                // Validate that the OTP consists of exactly 6 digits
                if (IsValidOtp(userOTP))
                {
                    if (userOTP == currentOtp)
                    {
                        otpVerified = true;
                    }
                    else
                    {
                        attempts++;
                        WriteLine("Invalid OTP. Please try again.");
                    }
                }
                else
                {
                    attempts++;
                    WriteLine("Invalid OTP format. Please enter exactly 6 digits.");
                }
            }

            timeoutTimer.Stop();
            return otpVerified ? STATUS_OTP_OK : STATUS_OTP_FAIL;
        }

        // This method to validate the email format
        public bool IsValidEmail(string email)
        {
            // Check if email ends with "dso.org.sg"
            return email.EndsWith("dso.org.sg", StringComparison.OrdinalIgnoreCase);
        }

        // This method to generate a random 6-digit OTP
        public string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        // This method to validate if user entered OTP is exactly 6 digits
        public bool IsValidOtp(string otp)
        {
            // Check if the OTP is exactly 6 digits
            return otp.Length == 6 && Regex.IsMatch(otp, @"^\d{6}$");
        }

        // Simulate sending an email (this could be an actual email API in a real application)
        private bool SendEmail(string email, string emailBody)
        {
            // Simulate email sending logic (return true if successful, false if failed)
            if (email.Contains("@"))
            {
                // Simulate a successful email send for valid emails
                WriteLine($"Sending email to {email}: {emailBody}");
                return true;
            }
            else
                // Simulate email send failure for invalid email addresses
                return false;
        }

        // This method is triggered when the OTP input timeout occurs (1 minute)
        private void TimeoutExpired(object sender, ElapsedEventArgs e)
        {
            WriteLine("OTP entry timed out after 1 minute.");
            Close();
        }
    }
}
