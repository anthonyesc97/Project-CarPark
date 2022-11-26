using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SEAssignment
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        ///SQL Connections
        DAO dao = new DAO();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlDataReader dr;
        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //starts timers
            tmrTimein.Start();
           
            //populates data table
            PopulateData();

        }

        private void tmrTimein_Tick(object sender, EventArgs e)
        {
            lblTimein.Text = DateTime.Now.ToString("T");
            lblTimeout.Text = DateTime.Now.ToString("T");
            lblCurrentTime.Text = DateTime.Now.ToString("dd:MM:yyyy");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            //adds vehicles
            Vehicle v = new Vehicle();
            v.Registration = int.Parse(txtRegNoIn.Text);
            v.Ticket = txtTicket.Text;
            v.Timein = DateTime.Now.ToString("HH:mm:ss");
            v.AddVehicle();

            txtRegNoIn.Clear();
            txtTicket.Clear();
            MessageBox.Show("Vehicle Added");


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {   //Clocks out vehicles
            string to = DateTime.Now.ToString("HH:mm:ss"); //this stores data value for time in
            string regout = txtRegto.Text;

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspClockOut";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@to", to);
            cmd.Parameters.AddWithValue("@reg", regout);
            //put fees balance here
            dao.OpenCon();
            cmd.ExecuteNonQuery();
            dao.CloseCon();


            MessageBox.Show("Vehicle Clocked Out");


        }

        void PopulateData()
        {
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspShowVehicles";
            cmd.CommandType = CommandType.StoredProcedure;

            dao.OpenCon();
            da.SelectCommand = cmd;
            da.Fill(dt);
            dgvVehicles.DataSource = dt;
            dao.CloseCon();
        }

        
        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receipt r = new Receipt();
            r.Show();
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            Receipt r = new Receipt();
            try
            {

                string id = dgvVehicles.SelectedRows[0].Cells[0].Value.ToString();
                int vecNo = int.Parse(id);
                r.VehicleNo = vecNo;
                r.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You Must Select a Row.\n" + ex.Message);

            }
        }
    }
}
