# Hash Guard
![Logo](https://i.ibb.co/qysxSy7/password-manager-logo-small.png)
#### Description
 My own password manager made with my custom encryption algorithm (PDEA)

***

## About
- **Online Ability**: None
  - Hash Guard does not store anything in the cloud or use the internet in anyway. everything is stored locally on your machine. This ensures that your data cannot be tampered with by anyone expect yourself to help keep all your information safe and secure.
- **Encryption Type**: Pdea
  - The encryption that is being used for Hash Guard is my own encryption that I designed that I believe to be rather secure, if you wish to read more about this encyption you can look at the github page [here](https://github.com/DylanMcBean/PDEA-Encryption).
- **Storage**: SQLite Database
  - The information from Hash Guard is stored locally in an [SQLite database](https://sqlite.org/index.html). when you create a new Hash Guard account and then create passwords accounts inside there, they are first encrypted and then stored in the database.
- **Other Requirements**: None
  - There are no other software requirements that you need to be able to use Hash Guard
- **Hash Guard Account Recovery**: Pending
  - As Hash Guard is still in a prototype stage there is currently no account recovery if you have forgotten your master password. we hope to address this in a later release by implimenting such a recovery process.
- **Issues**: Issues
  - If you have any issues or any suggestions that you wish to be implimented into Hash Guard you can leave them in the issues panel and we will look over them and gauge if we find these changes appripriate to be implimented.
***

## FAQ
- **Loading Backup**: How to load backup
  - To load a backup that you have made if you have lost the original database file, you simply create a new account with the same name and password and then load the backup and it will check if the credentials match and then load them into your account for you.

## Releases 
#### [Most Recent](https://github.com/DylanMcBean/Hash-Guard/releases/tag/v3)
- **Prototype 1**: [Here](https://github.com/DylanMcBean/Hash-Guard/releases/tag/p1.0)
  - This is the first prototype build of Hash Guard.
- **Update 1**: [Here](https://github.com/DylanMcBean/Hash-Guard/releases/tag/v1)
  - Gave functionality to the Save/Load buttons for backup so the user can create backups incase of lose of data.
  - Changed text size of text showing who's account is currently active so it no longer cuts of characters half way.
- **Update 1.1**: [Here](https://github.com/DylanMcBean/Hash-Guard/releases/tag/v2)
  - Gave each field its own copy button to make it much easier to copy the text from inside them without needing to select it all
  - Added a search bar that can make it easier and quicker to find accounts with specific information in them.  
    The only field this does not search in is the password field but all other fields can be searched
- **Update 1.2** [Here](https://github.com/DylanMcBean/Hash-Guard/releases/tag/v3)
  - made searching better with case insensative searching
  - *apply* button and *delete account* button did not have correct tool tip
  - when creating a new account url was never set so it had a null value causing some issues
  - Added a sort by feature that allows you to sort:
	   - **A-Z**: Sort accounts alphabetically.
	   - **Most Recent**: Sort the accounts from the most recently visited account down the the least recently visited.
	   - **Most Visited**: Sort the accounts by the one you visit the most often down to the one you visit the least often.
