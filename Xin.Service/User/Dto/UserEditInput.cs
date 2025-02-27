using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.User.Dto
{
    public class UserEditInput: UserAddInput
    {
        [Required(ErrorMessage ="Id必填")]
        public long Id { get; set; }
    }
}
