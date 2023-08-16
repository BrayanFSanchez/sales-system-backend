using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SalesSystem.BLL.Services.Contract;
using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class RoleService: IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> List()
        {
            try
            {
                var roleList = await _roleRepository.Consult();
                return _mapper.Map<List<RoleDTO>>(roleList.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
