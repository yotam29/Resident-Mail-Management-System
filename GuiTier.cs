namespace Week8;
using System.Data;
class GuiTier{
    User user = new User();
    DataTier database = new DataTier();

    // print login page
    public User Login(){
        Console.WriteLine("------Welcome to System Management Packaging------");
        Console.WriteLine("Please input staff ID (username): ");
        user.userID = Console.ReadLine()!;
        Console.WriteLine("Please input your staff password: ");
        user.userPassword = Console.ReadLine()!;
        return user;
    }
    // print Dashboard after user logs in successfully
    public int Dashboard(User user){
        DateTime localDate = DateTime.Now;
        Console.WriteLine("---------------Dashboard-------------------");
        Console.WriteLine($"Hello: {user.userID}; Date/Time: {localDate.ToString()}");
        Console.WriteLine("Please select an option to continue:");
        Console.WriteLine("1.send email to resident");
        Console.WriteLine("2. Show record ");
        Console.WriteLine("3. Log Out");
        int option = Convert.ToInt16(Console.ReadLine());
        return option;
    }

    // show Residents records from database
    public void DisplayResidents(DataTable tableResidents){
        Console.WriteLine("---------------Resident List-------------------");
        foreach(DataRow row in tableResidents.Rows){
           Console.WriteLine($"Resident ID: {row["id"]} \t Date: {row["pdate"]} ");
        }
    }

    public void DisplaySendEmail(DataTable tableSendEmail)
    {
        Console.WriteLine("---------------Package Kist-------------------");
        foreach(DataRow row in tableSendEmail.Rows){
            Console.WriteLine($"The package ID: {row["package_id"]} \t The Agency: {row["agencey"]}");
        }
    }

}
