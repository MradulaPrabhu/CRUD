using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;

namespace CRUD
{
    public partial class Home : System.Web.UI.Page
    {
         
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                btnDelete.Enabled = false;
                FillGridView();
            }
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
          
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            hfProductID.Value = "";
            txtPrice.Text = txtProduct.Text = "";
            lblErrorMessage.Text = lblSuccessMessage.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProduct.Text.Trim() == "" && txtPrice.Text.Trim() == "")
                lblErrorMessage.Text = "Nothing to Save";
            else if (txtProduct.Text.Trim() == "" || txtPrice.Text.Trim() == "")
            {
                if (txtProduct.Text.Trim() == "")
                    lblErrorMessage.Text = "Please enter Product name";
                else
                    lblErrorMessage.Text = "Please enter Price of the product";

            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("CreateOrUpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", hfProductID.Value == "" ? 0 : Convert.ToInt32(hfProductID.Value));
                cmd.Parameters.AddWithValue("@Name", txtProduct.Text.Trim());
                cmd.Parameters.AddWithValue("@Price", Convert.ToInt32(txtPrice.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                string contactID = hfProductID.Value;
                Clear();
                if (contactID == "")
                    lblSuccessMessage.Text = "Saved Successfully";
                else
                    lblSuccessMessage.Text = "Updated Successfully";
                FillGridView();
            }

        }
        void FillGridView()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter("ViewProduct", con);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDA.Fill(dtbl);
            con.Close();
            gvProduct.DataSource = dtbl;
            gvProduct.DataBind();

        }
        protected void lnk_OnClick (object Sender ,EventArgs e)
        {
            int ProductID = Convert.ToInt32((Sender as LinkButton).CommandArgument);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter("ViewProductByID", con);
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDA.SelectCommand.Parameters.AddWithValue("@ProductID", ProductID);
            DataTable dtbl = new DataTable();
            sqlDA.Fill(dtbl);
            con.Close();
            hfProductID.Value = ProductID.ToString();
            txtProduct.Text = dtbl.Rows[0]["NAME"].ToString();
            txtPrice.Text =   dtbl.Rows[0]["PRICE"].ToString();
            btnSave.Text = "UpDate";
            btnDelete.Enabled = true;




        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("DeleteProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", hfProductID.Value == "" ? 0 : Convert.ToInt32(hfProductID.Value));
            cmd.ExecuteNonQuery();
            con.Close();
            Clear();
            FillGridView();
            lblSuccessMessage.Text = "Deleted Successfully";


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}