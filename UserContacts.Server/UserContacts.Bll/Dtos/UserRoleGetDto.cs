using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContacts.Bll.Dtos;

public class UserRoleGetDto
{
    public string UserRoleId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
}
