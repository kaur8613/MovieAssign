using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAssign
{
    class User
    {
        private int decide;
        private string username;
      

        public User()
        {
            this.decide = 0;
            this.username = "";
          
        }

      
        public string Username { get => username; set => username = value; } // indexers
        public int Decide { get => decide; set => decide = value; }
    }
}
