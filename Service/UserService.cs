using DataAccess;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _repository.GetByUsernameAndPasswordAsync(dto.Email, dto.Password);
            if (user == null) return null;

            return new LoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            };
        }

        public async Task<LoginResponseDto?> UpdateProfileAsync(UpdateProfileRequestDto dto)
        {
            var user = await _repository.GetByIdAsync(dto.Id);
            if (user == null) return null;

            user.Email = dto.Email;
            user.Password = dto.Password;

            await _repository.UpdateAsync(user);

            return new LoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

    }
}
