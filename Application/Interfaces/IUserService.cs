using Application.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Authenticate(UserLoginDto userLoginDto);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Create(UserRegistrationDto userRegisterDto);
        void Update(UserUpdateDto userUpdateDto, string password = null);
        void Delete(int id);
    }
}
