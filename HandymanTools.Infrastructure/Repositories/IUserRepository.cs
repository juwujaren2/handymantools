using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        string GetPasswordByUserName(string userName);
    }
}
