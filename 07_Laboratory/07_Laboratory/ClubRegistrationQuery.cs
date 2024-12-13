using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace _07_Laboratory
{
    public class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;
        private string connectionString;
        public DataTable dataTable;
        public BindingSource bindingSource;

        public string _FirstName;
        public string _MiddleName;
        public string _LastName;
        public string _Gender;
        public string _Program;
        public int _Age;



    }
}
