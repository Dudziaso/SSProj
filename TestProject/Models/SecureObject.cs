using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class SecureObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string ObjectNumber { get; set; }
        public DateTime PaidUntil { get; set; }
        public ApplicationUser UserProfile { get; set; }
        public virtual ICollection<SecurityTeam> SecurityTeama { get; set; }
        public SecureObject()
        {
            this.SecurityTeama = new HashSet<SecurityTeam>();
        }
    }
}
