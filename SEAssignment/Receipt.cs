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
    public partial class Receipt : Form
    {
        ///SQL Connections
        DAO dao = new DAO();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlDataReader dr;
        public Receipt()
        {
            InitializeComponent();
        }

        public int VehicleNo { get; set; }

        private void Receipt_Load(object sender, EventArgs e)
        {
            txtCardNo.Visible = false;
            txtCashAmnt.Visible = false;
            txtName.Visible = false;
            lblCardNo.Visible = false;
            lblName.Visible = false;
            lblCash.Visible = false;
            TReceipt();
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            lblCardNo.Visible = true;
            txtCardNo.Visible = true;
            lblName.Visible = true;
            txtName.Visible = true;
            lblCash.Visible = false;
            txtCashAmnt.Visible = false;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            lblCash.Visible = true;
            txtCashAmnt.Visible =  true;
            lblCardNo.Visible = false;
            txtCardNo.Visible = false;
            lblName.Visible = false;
            txtName.Visible = false;
        }

        public void TReceipt() //gets receipt for each vehicle
        {
            SqlDataReader dr;

            string select = "SELECT * FROM Vehicle WHERE VehicleId = @id";

            SqlCommand cmd = new SqlCommand(select, dao.OpenCon());

            cmd.Parameters.AddWithValue("@id", VehicleNo);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string rege = dr["Registration"].ToString();
                string tick = dr["Ticket"].ToString();
                string tin = dr["Time In"].ToString();
                string tio = dr["Time Out"].ToString();
                
                txtRegNo.Text = rege;
                txtTicketNo.Text = tick;
                txtTimein.Text = tin;
                txtTimeout.Text = tio;


            }
            dao.CloseCon();


        }
    }
}
