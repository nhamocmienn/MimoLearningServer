using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ClassRepository
{
    public class ClassRepository
    {
        private readonly MyDbContext _context;

        public ClassRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Classes.Include(c => c.Creator).ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes.Include(c => c.Creator).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Class cls)
        {
            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class cls)
        {
            _context.Classes.Update(cls);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Class cls)
        {
            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();
        }
    }
}
