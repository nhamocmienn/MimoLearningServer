using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<StudySection> StudySections { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<WrongAnswer> WrongAnswers { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: User <-> Class
            modelBuilder.Entity<User>()
                .HasMany(u => u.JoinedClasses)
                .WithMany(c => c.Members)
                .UsingEntity(j => j.ToTable("ClassMembers"));

            // Class.Creator -> User
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedClasses)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subject.Creator -> User
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Creator)
                .WithMany(u => u.CreatedSubjects)
                .HasForeignKey(s => s.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subject.Class (nullable)
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.SetNull);

            // Lesson.Subject
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Subject)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lesson.Creator
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Creator)
                .WithMany(u => u.CreatedLessons)
                .HasForeignKey(l => l.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudySection.Lesson (nullable)
            modelBuilder.Entity<StudySection>()
                .HasOne(s => s.Lesson)
                .WithMany(l => l.StudySections)
                .HasForeignKey(s => s.LessonId)
                .OnDelete(DeleteBehavior.SetNull);

            // Question.StudySection
            modelBuilder.Entity<Question>()
                .HasOne(q => q.StudySection)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.StudySectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // WrongAnswer.Question
            modelBuilder.Entity<WrongAnswer>()
                .HasOne(w => w.Question)
                .WithMany(q => q.WrongAnswers)
                .HasForeignKey(w => w.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ❗ WrongAnswer.StudySectionID (KHÔNG có navigation, chỉ FK) → phải chặn cascade!
            modelBuilder.Entity<WrongAnswer>()
                .HasOne<StudySection>()
                .WithMany()
                .HasForeignKey(w => w.StudySectionID)
                .OnDelete(DeleteBehavior.Restrict);

            // Document.Lesson
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Lesson)
                .WithMany(l => l.Documents)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
