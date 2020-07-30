using Application.Dtos.User;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        IRepository<User> _userRepository;
        IMapper _mapper;
        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Authenticate(UserLoginDto userLoginDto)
        {
            if (string.IsNullOrEmpty(userLoginDto.Username) || string.IsNullOrEmpty(userLoginDto.Password))
                return null;
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Username == userLoginDto.Username);
            if (user == null)
                return null;
            if (! AuthenticationHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return null;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users= await _userRepository.GetAllListAsync(u => true);
            return users.Select(_mapper.Map<User, UserDto>);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user= await _userRepository.GetAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Create(UserRegistrationDto userRegisterDto)
        {
            var user = _mapper.Map<User>(userRegisterDto);
            // validation
            if (string.IsNullOrWhiteSpace(userRegisterDto.Password))
                throw new AppException("Password is required");
            if (_userRepository.GetAll().Any(x => x.Username == user.Username))
                throw new AppException("Username " + user.Username + " is already taken");
            byte[] passwordHash, passwordSalt;
            AuthenticationHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userRepository.AddAsync(user);
            await _userRepository.Complete();
            return _mapper.Map<UserDto>(user);
        }

        public async void Update(UserUpdateDto userUpdateDto, string password = null)
        {
            var user = await _userRepository.GetAsync(userUpdateDto.Id);
            if (user == null)
                throw new AppException("User not found");
            if (!string.IsNullOrWhiteSpace(userUpdateDto.Username) && userUpdateDto.Username != user.Username)
            {
                if (_userRepository.GetAll().Any(x => x.Username == userUpdateDto.Username))
                    throw new AppException("Username " + userUpdateDto.Username + " is already taken");
                user.Username = userUpdateDto.Username;
            }
            if (!string.IsNullOrWhiteSpace(userUpdateDto.FirstName))
                user.FirstName = userUpdateDto.FirstName;
            if (!string.IsNullOrWhiteSpace(userUpdateDto.LastName))
                user.LastName = userUpdateDto.LastName;
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
               AuthenticationHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _userRepository.Update(user);
            await _userRepository.Complete();
        }

        public async void Delete(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(id);
                await _userRepository.Complete();
            }
        }
    }
}
