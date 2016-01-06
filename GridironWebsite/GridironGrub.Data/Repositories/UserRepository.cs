using GridironGrub.Models;
using GridironGrub.Models.ViewModels.User;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class UserRepository
    {
        private ApplicationDbContext _db;

        public UserRepository()
        {
            _db = new ApplicationDbContext();
        }

        public InfoIdVM getUserInfoIds(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();
            InfoIdVM InfoIds = new InfoIdVM
            {
                CustInfoId = user.CustomerInfoId,
                SeatInfoId = user.SeatInfoId,
                AdminInfoId = user.AdminInfoId,
                ManagerInfoId = user.ManagerInfoId,
                VendorInfoId = user.VendorInfoId,
                RunnerInfoId = user.RunnerInfoId
            };

            return InfoIds;
        }

        public RolesVM GetRoles(string id)
        {
            RolesVM vm = new RolesVM
            {
                IsAdmin = IsAdmin(id),
                IsManager = IsManager(id),
                IsVendor = IsVendor(id),
                IsRunner = IsRunner(id),
                IsSeat = IsSeat(id),
                IsCustomer = IsCustomer(id)
            };

            return vm;
        }

        public RolesAsStringsVM GetRolesAsStrings(string id)
        {
            RolesAsStringsVM vm = new RolesAsStringsVM
            {
                IsAdmin = IsAdmin(id) ? "true" : "false",
                IsManager = IsManager(id) ? "true" : "false",
                IsVendor = IsVendor(id) ? "true" : "false",
                IsRunner = IsRunner(id) ? "true" : "false",
                IsSeat = IsSeat(id) ? "true" : "false",
                IsCustomer = IsCustomer(id) ? "true" : "false"
            };

            return vm;
        }

        public bool IsAdmin(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            if(user.AdminInfoId != null)
            {
                if(!user.AdminInfo.IsRetired)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsManager(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user.ManagerInfoId != null)
            {
                if (!user.ManagerInfo.IsRetired)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsVendor(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user.VendorInfoId != null)
            {
                if (!user.VendorInfo.IsRetired)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsRunner(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user.RunnerInfoId != null)
            {
                if (!user.RunnerInfo.IsRetired)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsSeat(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user.SeatInfoId != null)
            {
                if (!user.SeatInfo.IsRetired)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCustomer(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user.CustomerInfoId != null)
            {
                if (!user.CustomerInfo.IsRetired)
                {
                    return true;
                }
            }

            return false;
        }

        public async void SendPasswordReset(string email)
        {
            // Create the email object first, then add the properties.
            SendGridMessage myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress("cpena@dmshouston.com", "Chris Pena");
            string recipient = email;
            myMessage.AddTo(recipient);
            myMessage.Subject = "Testing the SendGrid Library";

            //Add the HTML and Text bodies
            myMessage.Html = GetMessageBody(email);
            myMessage.Text = "Hello World plain text!";
            //myMessage.AddAttachment(@"C:\file1.txt");

            // Create credentials, specifying your user name and password.
            NetworkCredential credentials = new NetworkCredential("udmschrispena", "1q0p2w9o");

            // Create an Web transport for sending email.
            SendGrid.Web transportWeb = new SendGrid.Web("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");

            // Send the email.
            // You can also use the **DeliverAsync** method, which returns an awaitable task.
            await transportWeb.DeliverAsync(myMessage);
        }

        public string GetMessageBody(string email)
        {
            string userId = _db.Users.Where(u => u.Email == email).FirstOrDefault().Id;
            string message = null;

            try
            {
                StreamReader resetSR = new StreamReader("/GridIronGrub/ResetPassword/reset.txt");
                resetSR = File.OpenText("/GridIronGrub/ResetPassword/reset.txt");
                message = resetSR.ReadToEnd();
                resetSR.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            message = message.Replace("{{user}}", userId);

            return message;
        }

        public void ClearPassword(string id)
        {
            ApplicationUser user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

            user.PasswordHash = null;

            _db.SaveChanges();
        }
    }
}
