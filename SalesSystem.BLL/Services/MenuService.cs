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
    public class MenuService: IMenuService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRole> _menuRoleRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<User> userRepository, IGenericRepository<MenuRole> menuRoleRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _menuRoleRepository = menuRoleRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> List(int idUser)
        {
            IQueryable<User> tbUser = await _userRepository.Consult(u => u.IdUser == idUser);
            IQueryable<MenuRole> tbMenuRole = await _menuRoleRepository.Consult();
            IQueryable<Menu> tbMenu = await _menuRepository.Consult();

            try
            {
                IQueryable<Menu> tbResult = (from u in tbUser
                                             join mr in tbMenuRole on u.IdRole equals mr.IdRole
                                             join m in tbMenu on mr.IdMenu equals m.IdMenu
                                             select m).AsQueryable();

                var menuList = tbResult.ToList();
                return _mapper.Map<List<MenuDTO>>(menuList);
            }
            catch
            {
                throw;
            }
        }
    }
}
