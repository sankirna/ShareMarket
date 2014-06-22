using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.Core
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Collaborator> Collaborators { get; set; }
    }
}
