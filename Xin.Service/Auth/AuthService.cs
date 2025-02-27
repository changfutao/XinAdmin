using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;

namespace Xin.Service.Auth
{
    public class AuthService: IAuthService
    {
        private readonly IFreeSql<SqlServerFlag> _fsql;

        public AuthService(IFreeSql<SqlServerFlag> fsql)
        {
            _fsql = fsql;
        }

    }
}
