using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SEAssignment
{
    public class Vehicle
    {
        DAO dao = new DAO();
        public int Registration { get; set; }

        public string Ticket { get; set; }

        public string Timein { get; set; }

        public string Timeout { get; set; }

        public void AddVehicle()
        {
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspAddVehicle";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@reg", Registration);
            cmd.Parameters.AddWithValue("@tic", Ticket);
            cmd.Parameters.AddWithValue("@ti", Timein);

            dao.OpenCon();
            cmd.ExecuteNonQuery();
            dao.CloseCon();

        }
    }
}
