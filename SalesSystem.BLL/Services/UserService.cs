using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Contract;
using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public async Task<List<UserDTO>> List()
        {
            try
            {
                var queryUser = await _userRepository.Consult();
                var userList = queryUser.Include(role => role.IdRoleNavigation).ToList();

                return _mapper.Map<List<UserDTO>>(userList);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SessionDTO> ValidateCredentials(string email, string clue)
        {
            try
            {
                var queryUser = await _userRepository.Consult(u => 
                    u.Email == email &&
                    u.Clue == clue
                );

                if ( queryUser.FirstOrDefault() == null )
                {
                    throw new TaskCanceledException("User does not exist");
                }

                User returnUser = queryUser.Include(role => role.IdRoleNavigation).First();

                return _mapper.Map<SessionDTO>(returnUser);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO model)
        {
            try
            {
                var userCreated = await _userRepository.Create(_mapper.Map<User>(model));

                if ( userCreated.IdUser == 0 ) {
                    throw new TaskCanceledException("Could not create");
                }

                var query = await _userRepository.Consult(u => u.IdUser == userCreated.IdUser);

                userCreated = query.Include(role => role.IdRoleNavigation).First();

                return _mapper.Map<UserDTO>(userCreated);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Edit(UserDTO model)
        {
            try
            {
                var modelUser = _mapper.Map<User>(model);

                var userFound = await _userRepository.Get(u => u.IdUser == modelUser.IdUser);

                if(userFound == null)
                {
                    throw new TaskCanceledException("User does not exist");
                }

                userFound.FullName = modelUser.FullName;
                userFound.Email = modelUser.Email;
                userFound.IdRole = modelUser.IdRole;
                userFound.Clue = modelUser.Clue;
                userFound.IsActive = modelUser.IsActive;

                bool response = await _userRepository.Edit(userFound);

                if(!response)
                {
                    throw new TaskCanceledException("Could not edit");
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var userFound = await _userRepository.Get(u => u.IdUser == id);

                if(userFound == null)
                {
                    throw new TaskCanceledException("User does not exist");
                }

                bool response = await _userRepository.Delete(userFound);

                if (!response)
                {
                    throw new TaskCanceledException("Could not delete");
                }

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
