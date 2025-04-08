using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public  interface IJwtTokenService
    {
        string GenerateJwtToken(Supplier user);
    }
}
