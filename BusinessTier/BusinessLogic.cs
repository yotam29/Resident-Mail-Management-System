namespace Week8;
using System.Data;
using MySql.Data.MySqlClient;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
class BusinessLogic
{
   
    static void Main(string[] args)
    {
        bool _continue = true;
        User user;
        GuiTier appGUI = new GuiTier();
        DataTier database = new DataTier();
        string semester;

        // start GUI
        user = appGUI.Login();

       
        if (database.LoginCheck(user)){
            
            while(_continue){
                int option  = appGUI.Dashboard(user);
                switch(option)
                {
                    //  email notifaction
                    case 1:
                        DataTable tableEmail = database.Email();
                        if(tableEmail != null)
                            appGUI.DisplayResidents(tableEmail);
                             string serviceConnectionString = "endpoint=https://yotamweek10communicationservice.communication.azure.com/;accesskey=6JDbNNs14u2wfYLDIbaftJ+s98Qdl3mmoy6LeqJy9Guvh3Z9PAo3Whs+O8uOhQ3LwM5bveQ09FPJSGmN9yobQw==";
        EmailClient emailClient = new EmailClient(serviceConnectionString);
        var subject = "New package";
        var emailContent = new EmailContent(subject);
        // use Multiline String @ to design html content
        emailContent.Html= @"
                    <html>
                        <body>
                            <h1 style=color:red>Hello dear resident your package arrived to the staff office </h1>
                            <h4>Come to pick it up ASAP/h4>
                            <p>Thank you for your cooperation </p>
                        </body>
                    </html>";
                    // mailfrom domain of your email service on Azure
        var sender = "DoNotReply@40321d49-6907-41f1-aab4-33a014265500.azurecomm.net";

        Console.WriteLine("Please input an email address: ");
        string? inputEmail = Console.ReadLine();
        var emailRecipients = new EmailRecipients(new List<EmailAddress> {
            new EmailAddress(inputEmail) { DisplayName = "Testing" },
        });

        var emailMessage = new EmailMessage(sender, emailContent, emailRecipients);

        try
        {
            SendEmailResult sendEmailResult = emailClient.Send(emailMessage);

            string messageId = sendEmailResult.MessageId;
            if (!string.IsNullOrEmpty(messageId))
            {
                Console.WriteLine($"Email sent, MessageId = {messageId}");
            }
            else
            {
                Console.WriteLine($"Failed to send email.");
                return;
            }

    // wait max 2 minutes to check the send status for mail.
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            do
            {
                SendStatusResult sendStatus = emailClient.GetSendStatus(messageId);
                Console.WriteLine($"Send mail status for MessageId : <{messageId}>, Status: [{sendStatus.Status}]");

                if (sendStatus.Status != SendStatus.Queued)
                {
                    break;
                }
                await Task.Delay(TimeSpan.FromSeconds(10));
               
            } while (!cancellationToken.IsCancellationRequested);

            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"Looks like we timed out for email");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in sending email, {ex}");
        }
                        break;
                    // Add A Course
                    case 2:

                        break;
                    // Log Out
                    case 3:
                        _continue = false;
                        Console.WriteLine("Log out, Goodbye.");
                        break;
                    // default: wrong input
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
        }
        else{
                Console.WriteLine("Login Failed, Goodbye.");
        } 


    }    
}
