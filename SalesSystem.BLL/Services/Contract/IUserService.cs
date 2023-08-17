using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> List();
        Task<SessionDTO> ValidateCredentials(string email, string clue);
        Task<UserDTO> Create(UserDTO model);
        Task<bool> Edit(UserDTO model);
        Task<bool> Delete(int id);
    }
}
