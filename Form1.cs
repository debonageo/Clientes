using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Clientes
{
    public partial class FormClientes : Form
    {


        public FormClientes()
        {
            InitializeComponent();

        }
        private MySqlConnectionStringBuilder conexaoBanco()
        {
            //Define parametros para conexão com o Banco ↓

            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "clientes";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            conexaoBD.SslMode = 0;
            return conexaoBD;
        }
        private void FormClientes_Load(object sender, EventArgs e)
        {
            //Carrega formulário e chama função para atualizar grid na inicialização ↓

            atualizaGrid();


        }
        private void atualizaGrid()
        {
            //Conecta com o Banco e atualiza grid ↓

            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM cliente";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dgClientes.Rows.Clear();
                while (reader.Read())
                {

                    DataGridViewRow row = (DataGridViewRow)dgClientes.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32(0);
                    row.Cells[1].Value = reader.GetString(1);
                    row.Cells[2].Value = reader.GetString(2);
                    row.Cells[3].Value = reader.GetString(3);
                    dgClientes.Rows.Add(row);

                }

                realizaConexaoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir a conexão ! ");
                Console.WriteLine(ex.Message);
            }
        }
        private void limparCampos()
        {

            //Função para limpar campos dos textbox e definir focu no campo nome 

            tbId.Clear();
            tbNome.Clear();
            tbEmail.Clear();
            tbTelefone.Clear();
            tbNome.Focus();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            
            //Verifica se todos os campos foram preenchidos ↓

            if (tbNome.Text != "" && tbEmail.Text != "" && tbTelefone.Text != "")
            {

                //Cria novo objeto da Classe cliente ↓

                Cliente cliente = new Cliente();
                cliente.setNome(tbNome.Text);
                cliente.setEmail(tbEmail.Text);
                cliente.setTelefone(tbTelefone.Text);
                string nome = cliente.getNome();
                string email = cliente.getEmail();
                string telefone = cliente.getTelefone();

                //Conecta com o Banco e carrega dados do novo cliente ↓

                MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
                MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
                try
                {
                    realizaConexaoBD.Open();
                    MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                    comandoMySql.CommandText = "INSERT INTO cliente (nome, email, telefone)" + "VALUES ('" + nome + "','" + email + "','" + telefone + "')";
                    comandoMySql.ExecuteNonQuery();
                    realizaConexaoBD.Close();
                    MessageBox.Show("Inserido com sucesso");

                    atualizaGrid();
                    limparCampos();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inserir dados");

            }

        }



        private void btExcluir_Click(object sender, EventArgs e)
        {

            //Exclui cadastro de cliente selecionado ↓

            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "DELETE FROM cliente WHERE Id = " + dgClientes.CurrentRow.Cells[0].Value;
                comandoMySql.ExecuteNonQuery();
                realizaConexaoBD.Close();
                MessageBox.Show("Excluido com sucesso");

                atualizaGrid();
                limparCampos();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }



        private void dgClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //Seleciona cadastro e carrega informações nos textbox para alteração ↓

            DataGridView senderGrid = (DataGridView)sender;

            try
            {

                if (dgClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {

                    tbId.Text = dgClientes.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    tbNome.Text = dgClientes.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                    tbEmail.Text = dgClientes.Rows[e.RowIndex].Cells["email"].Value.ToString();
                    tbTelefone.Text = dgClientes.Rows[e.RowIndex].Cells["telefone"].Value.ToString();

                }
            }
            catch
            {
                MessageBox.Show("Nada foi selecionada");
            }
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {

            //Verifica se todos os campos foram preenchidos ↓

            if (tbNome.Text != "" && tbEmail.Text != "" && tbTelefone.Text != "")
            {

                //Carrega dados do cliente para atualizar no Banco ↓

                Cliente cliente = new Cliente();
                cliente.setNome(tbNome.Text);
                cliente.setEmail(tbEmail.Text);
                cliente.setTelefone(tbTelefone.Text);
                string nome = cliente.getNome();
                string email = cliente.getEmail();
                string telefone = cliente.getTelefone();



                MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
                MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
                try
                {
                    realizaConexaoBD.Open();

                    MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();

                    comandoMySql.CommandText = "UPDATE cliente SET nome = '" + nome + "  ', email = '" + email + "', telefone = '" + telefone + "' WHERE Id = " + dgClientes.CurrentRow.Cells[0].Value;
                    comandoMySql.ExecuteNonQuery();

                    realizaConexaoBD.Close();
                    MessageBox.Show("Alterado com sucesso");


                    atualizaGrid();
                    limparCampos();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inserir dados");

            }


        }

        private void btLimpar_Click(object sender, EventArgs e)
        {

            //Chama função limpar campos após pressionar o button Limpar ↓

            limparCampos();

        }
                        
    }
}


        
    

