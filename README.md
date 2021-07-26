# Hash Guard
![Logo](https://i.ibb.co/qysxSy7/password-manager-logo-small.png)
#### Description
 My own password manager made with my custom encryption algorithm (PDEA)

***

## About
- **Version:** Prototype
  - This is a prototype at the moment so if you plan on using it for a password manager then I would be careful when updating to newer versions as there may be some compatability issues. these compatability issues should only be an issue in the prototype stage as when it is in full release I will make sure to keep things backwards compatible
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
## Releases
- **Prototype 1**: [Here](https://github.com/DylanMcBean/Hash-Guard/releases/tag/p1.0)
  - This is the first prototype build of Hash Guard.
- **Update 1**: [Here](https://github.com/DylanMcBean/Hash-Guard/releases/tag/v1.2)
  - Gave functionality to the Save/Load buttons for backup so the user can create backups incase of lose of data.
  - Changed text size of text showing who's account is currently active so it no longer cuts of characters half way.

## Change-Log
**To Be Done**
