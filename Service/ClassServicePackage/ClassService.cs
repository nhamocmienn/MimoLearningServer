using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.ClassRepository;
using DTO;
using DTO.Class;
using Model;

namespace Service.ClassServicePackage
{
    public class ClassService
    {
        private readonly ClassRepository _repository;

        public ClassService(ClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClassDto>> GetAllAsync()
        {
            var classes = await _repository.GetAllAsync();
            return classes.Select(c => new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatorId = c.CreatorId,
                CreateDate = c.CreateDate
            }).ToList();
        }

        public async Task<ClassDto?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            return c == null ? null : new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatorId = c.CreatorId,
                CreateDate = c.CreateDate
            };
        }

        public async Task CreateAsync(CreateClassDto dto)
        {
            var c = new Class
            {
                Name = dto.Name,
                CreatorId = dto.CreatorId,
                CreateDate = DateTime.UtcNow
            };
            await _repository.AddAsync(c);
        }

        public async Task<bool> UpdateAsync(int id, UpdateClassDto dto)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null) return false;
            c.Name = dto.Name;
            await _repository.UpdateAsync(c);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null) return false;
            await _repository.DeleteAsync(c);
            return true;
        }
    }

}
