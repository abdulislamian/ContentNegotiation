using ContentNegotiation.Data;
using ContentNegotiation.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentNegotiation.Repository
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SQLStudentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> DeleteAsync(int id)
        {
            var existingStudent =  await dbContext.Students.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingStudent == null)
            {
                return null;
            }
            dbContext.Students.Remove(existingStudent);
            await dbContext.SaveChangesAsync();

            return existingStudent;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student?> UpdateAsync(int id, Student student)
        {
            var existingStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStudent == null)
            {
                return null;
            }

            existingStudent.Name= student.Name;
            existingStudent.Address = student.Address;

            await dbContext.SaveChangesAsync();

            return existingStudent;
        }
    }
}
