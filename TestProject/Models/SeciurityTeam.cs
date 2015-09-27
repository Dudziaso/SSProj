using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class SecurityTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public bool IsReady { get; set; }
        public DateTime LastSeen { get; set; }
        public virtual ICollection<SecureObject> SecureObjects { get; set; }
        public SecurityTeam()
        {
            this.SecureObjects = new HashSet<SecureObject>();
        }
    }
}
