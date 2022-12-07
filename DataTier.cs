namespace Week8;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
class DataTier{
    public string connStr = "server=20.172.0.16;user=ysbalbul1;database=ysbalbul1;port=8080;password=ysbalbul1";

    // perform login check using Stored Procedure "LoginCount" in Database based on given user' staff username and Password
    public bool LoginCheck(User user){
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {  
            conn.Open();
            string procedure = "LoginCount";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure; // set the commandType as storedProcedure
            cmd.Parameters.AddWithValue("@inputUserID", user.userID);
            cmd.Parameters.AddWithValue("@inputUserPassword", user.userPassword);
            cmd.Parameters.Add("@userCount", MySqlDbType.Int32).Direction =  ParameterDirection.Output;
            MySqlDataReader rdr = cmd.ExecuteReader();
           
            int returnCount = (int) cmd.Parameters["@userCount"].Value;
            rdr.Close();
            conn.Close();

            if (returnCount ==1){
                return true;
            }
            else{
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return false;
        }
       
    }

 
    //checking emailes
        public DataTable SendEmail(){
        MySqlConnection conn = new MySqlConnection(connStr);
        
        try
        {  
            conn.Open();
            string procedure = "SendEmail";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable TableEmail = new DataTable();
            TableEmail.Load(rdr);
            rdr.Close();
            conn.Close();
            return TableEmail;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null!;
        }
    }

        //checking records
        public DataTable ShowRecords(){
        MySqlConnection conn = new MySqlConnection(connStr);
        
        try
        {  
            conn.Open();
            string procedure = "ShowRecords";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable TableRecords = new DataTable();
            TableRecords.Load(rdr);
            rdr.Close();
            conn.Close();
            return TableRecords;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            conn.Close();
            return null!;
        }
    }


}

