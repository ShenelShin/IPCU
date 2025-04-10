using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using IPCU.Models;

namespace IPCU.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmailService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
                var smtpPassword = _configuration["EmailSettings:SmtpPassword"];
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderName = _configuration["EmailSettings:SenderName"];

                var client = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }

        public async Task NotifyRolesAboutICRA(ICRA icra, string[] roleNames)
        {
            var usersToNotify = new List<ApplicationUser>();

            // Get all users in the specified roles
            foreach (var roleName in roleNames)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
                usersToNotify.AddRange(usersInRole);
            }

            // Remove duplicates (users who might be in multiple roles)
            usersToNotify = usersToNotify.Distinct().ToList();

            // Prepare email content
            var subject = $"URGENT: ICRA Class {icra.PreventiveMeasures} Requires Immediate Attention";

            // Add signature information section
            var signatureSection = string.Empty;

            signatureSection += "<h3>Signature Status:</h3><ul>";

            if (!string.IsNullOrEmpty(icra.EngineeringSign))
                signatureSection += $"<li><strong>Engineering Sign:</strong> {icra.EngineeringSign}</li>";
            else
                signatureSection += "<li><strong>Engineering Sign:</strong> <span class='highlight'>Not signed</span></li>";

            if (!string.IsNullOrEmpty(icra.ICPSign))
                signatureSection += $"<li><strong>ICP Sign:</strong> {icra.ICPSign}</li>";
            else
                signatureSection += "<li><strong>ICP Sign:</strong> <span class='highlight'>Not signed</span></li>";

            if (!string.IsNullOrEmpty(icra.UnitAreaRep))
                signatureSection += $"<li><strong>Unit Area Representative:</strong> {icra.UnitAreaRep}</li>";
            else
                signatureSection += "<li><strong>Unit Area Representative:</strong> <span class='highlight'>Not signed</span></li>";

            signatureSection += "</ul>";

            var message = $@"
    <html>
    <head>
        <style>
            body {{ font-family: Arial, sans-serif; }}
            .highlight {{ color: #ff0000; font-weight: bold; }}
            .content {{ padding: 20px; }}
            .header {{ background-color: #f0f0f0; padding: 10px; }}
        </style>
    </head>
    <body>
        <div class='header'>
            <h2>High-Risk ICRA Alert</h2>
        </div>
        <div class='content'>
            <p>A new ICRA form with <span class='highlight'>Class {icra.PreventiveMeasures} preventive measures</span> has been submitted and requires your immediate attention.</p>
            <h3>ICRA Details:</h3>
            <ul>
                <li><strong>Project Reference:</strong> {icra.ProjectReferenceNumber}</li>
                <li><strong>Project Name:</strong> {icra.ProjectNameAndDescription}</li>
                <li><strong>Site Location:</strong> {icra.SpecificSiteOfActivity}</li>
                <li><strong>Start Date:</strong> {(icra.ProjectStartDate.HasValue ? icra.ProjectStartDate.Value.ToShortDateString() : "Not specified")}</li>
                <li><strong>Contractor:</strong> {icra.ContractorRepresentativeName}</li>
                <li><strong>Contact:</strong> {icra.TelephoneOrMobileNumber}</li>
            </ul>
            {signatureSection}
            <p>Please review this ICRA submission as soon as possible.</p>
            <p><a href='{_configuration["ApplicationUrl"]}/ICRAs/Details/{icra.Id}'>Click here to view the full ICRA details</a></p>
        </div>
    </body>
    </html>";

            // Send emails to all users in the specified roles
            foreach (var user in usersToNotify)
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    await SendEmailAsync(user.Email, subject, message);
                }
            }

            // Also send to the email address specified in the ICRA form, if available
            if (!string.IsNullOrEmpty(icra.ConstructionEmail))
            {
                await SendEmailAsync(icra.ConstructionEmail, subject, message);
            }
        }
    }
}