using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euchre.Models
{
    internal class Account
    {
        #region Static Vars
        public static List<Account> Accounts = new List<Account>();
        private static int nextID = 0;
        #endregion

        #region Properties
        public String Username { get; set; }
        private String Password { get; set; }
        public int ID { get; set; }
        public Player Player { get; set; }
        public int Wins {  get; set; }
        #endregion

        #region Constructor
        public Account(String username, String password, Player player)
        {
            Username = username;
            Password = password;
            Player = player;
            ID = nextID;
            nextID++;
            Account.Accounts.Add(this);
        }
        #endregion

        #region Methods
        #endregion

        #region static methods
        /// <summary>
        /// Returns an account if the username and password match.
        /// </summary>
        /// <param name="username">Input username to search by</param>
        /// <param name="password">Input password to serach by</param>
        /// <returns>Uesr account or null if none found</returns>
        public static Account ValidateLogin(String username, String password)
        {
            foreach(Account account in Account.Accounts)
            {
                if(account.Username == username && account.Password == password) return account;
            }
            return null;
        }
        /// <summary>
        /// Returns an account based on passed ID
        /// </summary>
        /// <param name="id">ID to find account by</param>
        /// <returns>User account or null if none found</returns>
        public static Account FindByID(int id)
        {
            foreach(Account account in Account.Accounts)
            {
                if (account.ID == id) return account;
            }
            return null;
        }
        #endregion
    }
}
