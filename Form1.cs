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

            //Cria conexão com o banco, passando os dados.

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
            atualizaGrid();
        }
        private void atualizaGrid()
        {

            

            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM clientes";
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
                MessageBox.Show("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (tbNome.Text != "" && tbEmail.Text != "" && tbTelefone.Text != "" )

            //Verifica existencia de campos em branco. 
            {
                
                
                //Conecta com banco.


                MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
                MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
                try
                {
                    realizaConexaoBD.Open();

                    MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();

                    comandoMySql.CommandText = "INSERT INTO clientes (nome, email, telefone) " +
                        "VALUES('" + tbNome.Text + "', '" + tbEmail.Text + "', '" + tbTelefone.Text + "')";

                    comandoMySql.ExecuteNonQuery();

                    realizaConexaoBD.Close();
                    MessageBox.Show("Inserido com sucesso");

                    //Chama função atualiza grid e limpar campos.

                    atualizaGrid();
                    



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inserir dados");

                //Se existia campos em branco, apresenta a mensagem "Inserir dados".

            }
        
    }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {

        }

        
    }
}
