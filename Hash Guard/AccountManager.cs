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

        public void ProcessAccounts(String sortBy)
        {
            accountsAmounts = new Dictionary<string, List<int>>();

            switch (sortBy) 
            {
                case "A-Z":
                    accounts.Sort(delegate (Account x, Account y)
                    {
                        return $"{(x.favorite ? '0' : '1') + x.name + x.username}".CompareTo($"{(y.favorite ? '0' : '1') + y.name + y.username}");
                    });
                    break;
                case "Most Recent":
                    accounts.Sort(delegate (Account x, Account y)
                    {
                        return ((y.favorite ? 0 : 1) + y.lastVisited).CompareTo((x.favorite ? 0 : 1) + x.lastVisited);
                    });
                    break;
                case "Most Visited":
                    accounts.Sort(delegate (Account x, Account y)
                    {
                        return ((y.favorite ? 0 : 1) + y.accountVisits).CompareTo((x.favorite ? 0 : 1) + x.accountVisits);
                    });
                    break;
            }

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

        public void ProcessAccountsSearch(String searchString, String sortBy)
        {
            accountsAmounts = new Dictionary<string, List<int>>();

            switch (sortBy)
            {
                case "A-Z":
                    accounts.Sort(delegate (Account x, Account y)
                    {
                        return $"{(x.favorite ? '0' : '1') + x.name + x.username}".CompareTo($"{(y.favorite ? '0' : '1') + y.name + y.username}");
                    });
                    break;
                case "Most Recent":
                    accounts.Sort(delegate (Account x, Account y)
                    {
                        return ((y.favorite ? 0 : 1) + y.lastVisited).CompareTo((x.favorite ? 0 : 1) + x.lastVisited);
                    });
                    break;
                case "Most Visited":
                    accounts.Sort(delegate (Account x, Account y)
                    {
                        return ((y.favorite ? 0 : 1) + y.accountVisits).CompareTo((x.favorite ? 0 : 1) + x.accountVisits);
                    });
                    break;
            }

            searchString = searchString.ToLower();
            foreach (Account account in accounts)
            {
                if (account.name.ToLower().Contains(searchString) || account.url.ToLower().Contains(searchString) || account.email.ToLower().Contains(searchString) || account.username.ToLower().Contains(searchString) || account.notes.ToLower().Contains(searchString))
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
