using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC53b3440528d230b3eb17b6d451d1de75";
            const string authToken = "4530d0adf788182cc17ad102442b3f64";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hey, Welcome to our new SMS platform. From KIBET",
                from: new Twilio.Types.PhoneNumber("+12067373930"),//short code on a live environment
                to: new Twilio.Types.PhoneNumber("+254719453783")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
