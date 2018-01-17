// Class by Yifan
// Features: Equals(), GetHashCode()
using System;

namespace WAD_Server
{
    public class user : IEquatable<user>
    {
        private string firstName;
        private string middleName;
        private string lastName;
        private string email;
        private string password;
        private string dob;

        public void intializeUser(string fName, string mName, string lName, string Email, string pw, string date)
        {
            this.firstName = fName;
            this.middleName = mName;
            this.lastName = lName;
            this.email = Email;
            this.password = pw;
            this.dob = date;
        }

        // Compares user email with other email, params user object
        public bool Equals(user other)
        {
            return email.Equals(other.email);
        }

        // Overrides the hash code to return hash code for email
        public override int GetHashCode()
        {
            return email.GetHashCode();
        }

        public string getFirstName() { return firstName; }
        public void setFirstName(string fName) { this.firstName = fName; }
        public string getMiddleName() { return middleName; }
        public void setMiddleName(string mName) { this.middleName = mName; }
        public string getLastName() { return this.lastName; }
        public void setLastName(string lName) { this.lastName = lName; }
        public string getEmail() { return this.email; }
        public void setEmail(string Email) { this.email = Email; }
        public string getPassword() { return this.password; }
        public void setPassword(string pw) { this.password = pw; }
        public void setDOB(string DOB) { this.dob = DOB; }
        public string getDOB() { return this.dob; }
    }
}
