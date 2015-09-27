using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestProject.Models
{

    public class SecurityTeamsVievModel
    {
        public SecurityTeam SecurityTeam { get; set; }
        public IEnumerable<SelectListItem> AllSecurityTeams { get; set; }

        private List<int> _selectedSecureObjects;
        public List<int> SelectedSecureObjects
        {
            get
            {
                if (_selectedSecureObjects == null)
                {

                    _selectedSecureObjects = SecurityTeam.SecureObjects.Select(m => m.Id).ToList();
       
                }
                return _selectedSecureObjects;
            }
            set { _selectedSecureObjects = value; }
        }
    }
}
