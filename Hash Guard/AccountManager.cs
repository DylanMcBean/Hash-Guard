using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Password_Manager
{
    [Serializable()]
    class AccountManager : ISerializable
    {
        public List<Account> accounts = new List<Account>();
        public Account currentAcount = null;
        public IDictionary<string, List<int>> accountsAmounts;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Accounts", accounts);
        }

        public AccountManager() { }

        public AccountManager(SerializationInfo info, StreamingContext context)
        {
            if (info == null) return;
            accounts = (List<Account>)info.GetValue("Accounts", typeof(List<Account>));
        }

        public void ProcessAccounts()
        {
            accountsAmounts = new Dictionary<string, List<int>>();

            accounts.Sort(delegate (Account x, Account y)
            {
                return $"{(x.favorite ? '0' : '1') + x.name + x.username}".CompareTo($"{(y.favorite ? '0' : '1') + y.name + y.username}");
            });

            foreach (Account account in accounts)
            {
                if (!accountsAmounts.Keys.Contains(account.mainName))
                {
                    accountsAmounts[account.mainName] = new List<int>();
                    accountsAmounts[account.mainName].Add(accounts.IndexOf(account));
                }
                else
                    accountsAmounts[account.mainName].Add(accounts.IndexOf(account));
            }
        }

        public void ProcessAccountsSearch(String searchString)
        {
            accountsAmounts = new Dictionary<string, List<int>>();

            accounts.Sort(delegate (Account x, Account y)
            {
                return $"{(x.favorite ? '0' : '1') + x.name + x.username}".CompareTo($"{(y.favorite ? '0' : '1') + y.name + y.username}");
            });

            foreach (Account account in accounts)
            {
                if (account.name.Contains(searchString) || account.url.Contains(searchString) || account.email.Contains(searchString) || account.username.Contains(searchString) || account.notes.Contains(searchString))
                {
                    if (!accountsAmounts.Keys.Contains(account.mainName))
                    {
                        accountsAmounts[account.mainName] = new List<int>();
                        accountsAmounts[account.mainName].Add(accounts.IndexOf(account));
                    }
                    else
                        accountsAmounts[account.mainName].Add(accounts.IndexOf(account));
                }
            }
        }
    }
}
