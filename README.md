Assumptions:
1. We assume that SendEmail is a pre-existing function that can send an email to the user.
2. The STATUS_* constants are predefined integer values to represent different statuses for Email and OTP.
3. The input stream for OTP will be simulated using the console (as we are working in a .NET Console app).
4. The email validation is simple — we only check if the email ends with domain "dso.org.sg".

Testing the Module via Console App:
1. Email Input Validation: Test different email formats to ensure only domain "dso.org.sg" emails are accepted.
2. OTP Generation: Ensure that a random 6-digit OTP is generated and sent to the user's email.
3. OTP Entry: Simulate entering the OTP and validate correct/incorrect OTP entries.
4. Timeout: Test if the timeout correctly triggers after 1 minute.

Testing the Module via Unit Test:
1. Run the EmailOTPModuleTests in Visual Studio.
2. The Unit Test cover the GenerateOTPEmail, CheckOTP, IsValidEmail, IsValidOtp function with one positive and one negative scenarios.  
