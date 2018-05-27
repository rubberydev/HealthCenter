
namespace HealthCenter.Models
{
    using System.Collections.Generic;

    public class Doctor
    {
        public List<object> Claims
        {
            get;
            set;
        }

        public List<object> Logins
        {
            get;
            set;
        }

        public List<Role> Roles
        {
            get;
            set;
        }

        public string DocumentNumber
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Speciality
        {
            get;
            set;
        }

        public string Telephone
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public bool EmailConfirmed
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public string SecurityStamp
        {
            get;
            set;
        }

        public object PhoneNumber
        {
            get;
            set;
        }

        public bool PhoneNumberConfirmed
        {
            get;
            set;
        }

        public bool TwoFactorEnabled
        {
            get;
            set;
        }

        public object LockoutEndDateUtc
        {
            get;
            set;
        }

        public bool LockoutEnabled
        {
            get;
            set;
        }

        public int AccessFailedCount
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
    }
}
