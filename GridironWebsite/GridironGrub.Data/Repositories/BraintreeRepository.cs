using Braintree;
using GridironGrub.Models;
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
    public class BraintreeRepository
    {
        private BraintreeGateway _gateway;
        private ApplicationDbContext _db;

        public BraintreeRepository()
        {
            _gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "s5ntjy6sc7w9pxp3",
                PublicKey = "fgcwzdqrdkvm35nz",
                PrivateKey = "e6080f4add343059c14aeda6597daead"
            };
            _db = new ApplicationDbContext();
        }

        public string getClientToken(string id)
        {
            ////Get Braintree CustomerId
            //ApplicationUser seat = _db.Users.Where(u => u.Id == id).FirstOrDefault();
            //string customerId = seat.SeatInfo.BraintreeCustomerId;

            ////Creat Braintree Customer
            //Result<Customer> resultCreateCustomer = null;
            //if (customerId == null)
            //{
            //    var request = new CustomerRequest();

            //    resultCreateCustomer = _gateway.Customer.Create(request);

            //    bool success = resultCreateCustomer.IsSuccess();
            //    customerId = resultCreateCustomer.Target.Id;

            //    seat.SeatInfo.BraintreeCustomerId = customerId;

            //    _db.SaveChanges();
            //}

            string clientToken = _gateway.ClientToken.generate(
                new ClientTokenRequest
                {
                    //CustomerId = customerId
                }
            );

            return clientToken;
        }

        public Result<Transaction> postPurchaseNonce(string id, string nonce)
        {
            decimal total = getTotal(id);

            Result<Transaction> result = null;

            if (total > 0)
            {
                TransactionRequest request = new TransactionRequest
                {
                    Amount = total,
                    PaymentMethodNonce = nonce
                };

                result = _gateway.Transaction.Sale(request);
            }

            if (result != null)
            {
                completePurchase(id);
            }

            return result;
        }

        private decimal getTotal(string id)
        {
            int seatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            Order order = _db.Orders.Where(o => o.SeatInfoId == seatInfoId && !o.IsRetired && o.TimeOrdered == null).FirstOrDefault();

            if (order != null)
            {
                return order.Total;
            }
            else return 0;
        }

        private void completePurchase(string id)
        {
            int seatInfoId = _db.Users.Where(u => u.Id == id).FirstOrDefault().SeatInfo.Id;
            Order order = _db.Orders.Where(o => o.SeatInfoId == seatInfoId && !o.IsRetired && o.TimeOrdered == null).FirstOrDefault();

            if(order != null)
            {
                if(order.ReceiptEmail != null)
                {
                    sendReceipt(order.Id, order.ReceiptEmail);
                }
                order.TimeOrdered = DateTime.Now;
                _db.SaveChanges();
            }
        }

        public async void sendReceipt(int orderId, string email)
        {
            // Create the email object first, then add the properties.
            SendGridMessage myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress("cpena@dmshouston.com", "Chris Pena");
            string recipient = email;
            myMessage.AddTo(recipient);
            myMessage.Subject = "Testing the SendGrid Library";

            //Add the HTML and Text bodies
            myMessage.Html = GetMessageBody(orderId);
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

        public string GetMessageBody(int orderId)
        {
            Order order = _db.Orders.Where(o => o.Id == orderId).Include(o => o.OrderItems).FirstOrDefault();
            string header = null;
            string item = null;
            string temp = null;
            string footer = null;
            string message = null;

            try
            {
                StreamReader headerSR = new StreamReader("/GridIronGrub/Receipt/header.txt");
                headerSR = File.OpenText("/GridIronGrub/Receipt/header.txt");
                header = headerSR.ReadToEnd();
                headerSR.Close();

                StreamReader itemSR = new StreamReader("/GridIronGrub/Receipt/item.txt");
                itemSR = File.OpenText("/GridIronGrub/Receipt/item.txt");
                item = itemSR.ReadToEnd();
                itemSR.Close();

                StreamReader footerSR = new StreamReader("/GridIronGrub/Receipt/footer.txt");
                footerSR = File.OpenText("/GridIronGrub/Receipt/footer.txt");
                footer = footerSR.ReadToEnd();
                footerSR.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            message = header;

            if (order.OrderItems != null)
            {
                foreach (OrderItem orderItem in order.OrderItems)
                {
                    temp = item.Replace("{{item.name}}", orderItem.Item.Name);
                    temp = temp.Replace("{{item.price}}", orderItem.Item.Price.ToString());
                    temp = temp.Replace("{{item.quantity}}", orderItem.Quantity.ToString());
                    message += temp;
                }
            }

            temp = footer.Replace("{{cart.subtotal}}", order.Subtotal.ToString());
            temp = temp.Replace("{{cart.taxes}}", order.Taxes.ToString());
            temp = temp.Replace("{{cart.convenienceFee}}", order.ConvenienceFee.ToString());
            temp = temp.Replace("{{cart.outOfAreaFee}}", order.OutOfAreaFee.ToString());
            temp = temp.Replace("{{cart.total}}", (order.Total).ToString());

            message += temp;

            message = message.Replace("\r\n\t", "");
            message = message.Replace("\r\n", "");

            return message;
        }
    }
}
