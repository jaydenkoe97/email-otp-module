using Email_OTP_Module;
using static Email_OTP_Module.Constants;
using static Email_OTP_Module.Wrapper.ConsoleWrapper;

namespace EmailOTPModuleTests
{
    public class EmailOTPModuleTests
    {
        private readonly EmailOTPModule _emailOTPModule;

        public EmailOTPModuleTests()
        {
            _emailOTPModule = new EmailOTPModule();
        }

        // Test 1: GenerateOTPEmail returns STATUS_EMAIL_OK for valid email with domain dso.org.sg
        [Fact]
        public void GenerateOTPEmail_ValidEmail_ReturnsSTATUS_EMAIL_OK()
        {
            // Arrange
            string validEmail = "testuser@dso.org.sg";

            // Act
            var result = _emailOTPModule.GenerateOTPEmail(validEmail);

            // Assert
            Assert.Equal(STATUS_EMAIL_OK, result);
        }

        // Test 2: GenerateOTPEmail returns STATUS_EMAIL_INVALID for invalid email
        [Fact]
        public void GenerateOTPEmail_InvalidEmail_ReturnsSTATUS_EMAIL_INVALID()
        {
            // Arrange
            string invalidEmail = "testuser@gmail.com";

            // Act
            var result = _emailOTPModule.GenerateOTPEmail(invalidEmail);

            // Assert
            Assert.Equal(STATUS_EMAIL_INVALID, result);
        }

        // Test 3: CheckOTP returns STATUS_OTP_OK when OTP is correct
        [Fact]
        public void CheckOTP_CorrectOTP_ReturnsSTATUS_OTP_OK()
        {
            // Arrange
            string validEmail = "testuser@dso.org.sg";
            _emailOTPModule.GenerateOTPEmail(validEmail);

            ReadLine = () => _emailOTPModule.currentOtp;  // Simulate user entering the correct OTP

            // Act
            var result = _emailOTPModule.CheckOTP();

            // Assert
            Assert.Equal(STATUS_OTP_OK, result);
        }

        // Test 4: CheckOTP returns STATUS_OTP_FAIL after 10 attempts
        [Fact]
        public void CheckOTP_MaxAttempts_ReturnsSTATUS_OTP_FAIL()
        {
            // Arrange
            string validEmail = "testuser@dso.org.sg";
            _emailOTPModule.GenerateOTPEmail(validEmail);

            // Simulate user input with wrong OTP for 10 attempts
            string wrongOTP = "123456";  // Assuming this is an invalid OTP for testing
            ReadLine = () => wrongOTP;  // Simulate entering the wrong OTP

            // Act
            var result = _emailOTPModule.CheckOTP();

            // Assert
            Assert.Equal(STATUS_OTP_FAIL, result);
        }

        // Test 5: IsValidEmail returns true for valid email domain dso.org.sg
        [Fact]
        public void IsValidEmail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            string validEmail = "testuser@dso.org.sg";

            // Act
            var result = _emailOTPModule.IsValidEmail(validEmail);

            // Assert
            Assert.True(result);
        }

        // Test 6: IsValidEmail returns false for invalid email domain
        [Fact]
        public void IsValidEmail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            string invalidEmail = "testuser@gmail.com";

            // Act
            var result = _emailOTPModule.IsValidEmail(invalidEmail);

            // Assert
            Assert.False(result);
        }

        // Test 7: IsValidOtp returns true for valid OTP format
        [Fact]
        public void IsValidOtp_ValidOtp_ReturnsTrue()
        {
            // Act
            var result = _emailOTPModule.IsValidOtp("123456");

            // Assert
            Assert.True(result);
        }

        // Test 8: IsValidOtp returns false for invalid OTP format
        [Fact]
        public void IsValidOtp_InvalidOtp_ReturnsFalse()
        {
            // Act
            var result = _emailOTPModule.IsValidOtp("12345a");  // Invalid OTP (5 digits + 1 character)

            // Assert
            Assert.False(result);
        }
    }
}