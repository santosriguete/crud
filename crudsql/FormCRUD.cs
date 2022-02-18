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

namespace CRUDSQL2022
{
    public partial class FormCRUD : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\crudsql\\DbPessoa.mdf;Integrated Security=True");
        public FormCRUD()
        {
            InitializeComponent();
        }

        public void CarregaDgv()
        {
            String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\crudsql\\DbPessoa.mdf;Integrated Security=True";
            String query = "SELECT * FROM Pessoa";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable pessoa = new DataTable();
            da.Fill(pessoa);
            dgvPessoa.DataSource = pessoa;
            con.Close();
        }

        private void FormCRUD_Load(object sender, EventArgs e)
        {
            CarregaDgv();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Inserir", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
            cmd.Parameters.AddWithValue("@endereco", SqlDbType.NChar).Value = txtEndereco.Text.Trim();
            cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
            cmd.Parameters.AddWithValue("@data", SqlDbType.NChar).Value = txtData.Text.Trim();
            cmd.ExecuteNonQuery();
            CarregaDgv();
            MessageBox.Show("Pessoa cadastrada com sucesso!", "Cadastro de Pessoas");
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtData.Text = "";
            con.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Atualizar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.Parameters.AddWithValue("@endereco", SqlDbType.NChar).Value = txtEndereco.Text.Trim();
                cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
                cmd.Parameters.AddWithValue("@data", SqlDbType.NChar).Value = txtData.Text.Trim();
                cmd.ExecuteNonQuery();
                CarregaDgv();
                MessageBox.Show("Pessoa atualizada com sucesso!", "Atualização de Pessoas");
                txtNome.Text = "";
                txtEndereco.Text = "";
                txtCelular.Text = "";
                txtData.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Excluir", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                CarregaDgv();
                MessageBox.Show("Pessoa excluída com sucesso!", "Exclusão de Pessoas");
                txtNome.Text = "";
                txtEndereco.Text = "";
                txtCelular.Text = "";
                txtData.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Localizar", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNome.Text = dr["nome"].ToString();
                    txtEndereco.Text = dr["endereco"].ToString();
                    txtCelular.Text = dr["celular"].ToString();
                    txtData.Text = dr["data"].ToString();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!");
                }
            }
            finally
            {

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtData.Text = "";
        }

        private void dgvPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                DataGridViewRow = this.dgvPessoa.Rows[e.RowIndex];
                txtNome.Text = Row.Cells[1].Value.ToString();
                txtEndereco.Text = Row.Cells[2].Value.ToString();
                txtCelular.Text = Row.cells[3].Value.ToString();
                txtData.Text = row.cells[4].Value.ToString();
            }
        }

        private void lblData_Click(object sender, EventArgs e)
        {

        }
    }
}
