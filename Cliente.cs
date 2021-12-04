using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes
{
    public class Cliente
    {
        //Atributos classe cliente ↓

        public int Id;
        public string Nome;
        public string Email;
        public string Telefone;

        //Metodos get e set ↓

        //set ↓

        public void setNome(string nome)
        {
            this.Nome = nome;
        }
        public void setTelefone(string telefone)
        {
            this.Telefone = telefone;
        }
        public void setEmail(string email)
        {
            this.Email = email;
        }

        //get ↓

        public string getNome()
        {
            return this.Nome;
        }
        public string getTelefone()
        {
            return this.Telefone;
        }
        public string getEmail()
        {
            return this.Email;
        }
    }

    
}

