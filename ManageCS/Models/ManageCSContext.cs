using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ManageCS.Models
{
    public partial class ManageCSContext : DbContext
    {
        public ManageCSContext()
        {
        }

        public ManageCSContext(DbContextOptions<ManageCSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<EquipmentDetail> EquipmentDetails { get; set; } = null!;
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;
        public virtual DbSet<EquipmentUnit> EquipmentUnits { get; set; } = null!;
        public virtual DbSet<Lecture> Lectures { get; set; } = null!;
        public virtual DbSet<Monitor> Monitors { get; set; } = null!;
        public virtual DbSet<MonitorType> MonitorTypes { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<OrganizationLevel> OrganizationLevels { get; set; } = null!;
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; } = null!;
        public virtual DbSet<PassingPlan> PassingPlans { get; set; } = null!;
        public virtual DbSet<PlanType> PlanTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<TableScore> TableScores { get; set; } = null!;
        public virtual DbSet<TrainingPlan> TrainingPlans { get; set; } = null!;
        public virtual DbSet<UserHistory> UserHistories { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<UserState> UserStates { get; set; } = null!;
        public virtual DbSet<Year> Years { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Attendance");

                entity.Property(e => e.Buoidihoc).HasColumnName("buoidihoc");

                entity.Property(e => e.Comat).HasColumnName("comat");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(100)
                    .HasColumnName("plan_name");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("student_id")
                    .IsFixedLength();

                entity.Property(e => e.StudentName)
                    .HasMaxLength(100)
                    .HasColumnName("student_name");

                entity.HasOne(d => d.Plan)
                    .WithMany()
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__Attendanc__plan___7D39791C");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Attendanc__stude__7E2D9D55");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("class_code");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("class_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.OrganzationName)
                    .HasMaxLength(100)
                    .HasColumnName("organzation_name");

                entity.Property(e => e.QuantityStudent).HasColumnName("quantity_Student");

                entity.Property(e => e.YearName)
                    .HasMaxLength(100)
                    .HasColumnName("year_name");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Class__organizat__1229A90A");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountYear).HasColumnName("count_year");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(100)
                    .HasColumnName("course_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.HasMany(d => d.Classes)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseClass",
                        l => l.HasOne<Class>().WithMany().HasForeignKey("ClassId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_Cl__class__7FC0E00C"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_Cl__cours__7ECCBBD3"),
                        j =>
                        {
                            j.HasKey("CourseId", "ClassId").HasName("PK__Course_C__F0C1B036423A0E00");

                            j.ToTable("Course_Class");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");

                            j.IndexerProperty<int>("ClassId").HasColumnName("class_id");
                        });
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(100)
                    .HasColumnName("organization_name");

                entity.Property(e => e.Quality)
                    .HasMaxLength(20)
                    .HasColumnName("quality");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .HasColumnName("type_name");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("unit_name");

                entity.Property(e => e.YearUse).HasColumnName("yearUse");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Equipment__organ__39836D4D");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Equipment__type___388F4914");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK__Equipment__unit___379B24DB");
            });

            modelBuilder.Entity<EquipmentDetail>(entity =>
            {
                entity.ToTable("Equipment_Detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentDetails)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK__Equipment__equip__3C5FD9F8");
            });

            modelBuilder.Entity<EquipmentType>(entity =>
            {
                entity.ToTable("Equipment_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(50)
                    .HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EquipmentUnit>(entity =>
            {
                entity.ToTable("Equipment_Unit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.ToTable("Lecture");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.LectureName)
                    .HasMaxLength(100)
                    .HasColumnName("lecture_name");

                entity.Property(e => e.Sotiet).HasColumnName("sotiet");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Lecture__subject__3572E547");
            });

            modelBuilder.Entity<Monitor>(entity =>
            {
                entity.ToTable("Monitor");

                entity.Property(e => e.Id)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("class_name");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .HasColumnName("course_name");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("fullName");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.MonitorTypeid).HasColumnName("monitor_typeid");

                entity.Property(e => e.MonitorTypename)
                    .HasMaxLength(50)
                    .HasColumnName("monitor_typename");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(50)
                    .HasColumnName("organization_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .HasColumnName("position");

                entity.Property(e => e.Rank)
                    .HasMaxLength(50)
                    .HasColumnName("rank");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Monitors)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Monitor__class_i__727BF387");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Monitors)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Monitor__course___7187CF4E");

                entity.HasOne(d => d.MonitorType)
                    .WithMany(p => p.Monitors)
                    .HasForeignKey(d => d.MonitorTypeid)
                    .HasConstraintName("FK__Monitor__monitor__737017C0");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Monitors)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Monitor__organiz__7093AB15");
            });

            modelBuilder.Entity<MonitorType>(entity =>
            {
                entity.ToTable("Monitor_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.MonitorTypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("monitorType_code");

                entity.Property(e => e.MonitorTypeName)
                    .HasMaxLength(100)
                    .HasColumnName("monitorType_name");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.OrganizationCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("organization_code");

                entity.Property(e => e.OrganizationLevelId).HasColumnName("organization_levelId");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(50)
                    .HasColumnName("organization_name");

                entity.Property(e => e.OrganizationParentId).HasColumnName("organization_parentId");

                entity.Property(e => e.OrganizationTypeId).HasColumnName("organization_typeId");

                entity.HasOne(d => d.OrganizationLevel)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.OrganizationLevelId)
                    .HasConstraintName("FK__Organizat__organ__48BAC3E5");

                entity.HasOne(d => d.OrganizationType)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.OrganizationTypeId)
                    .HasConstraintName("FK__Organizat__organ__47C69FAC");
            });

            modelBuilder.Entity<OrganizationLevel>(entity =>
            {
                entity.ToTable("Organization_Level");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<OrganizationType>(entity =>
            {
                entity.ToTable("Organization_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PassingPlan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PassingPlan");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Note)
                    .HasMaxLength(100)
                    .HasColumnName("note");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.HasOne(d => d.Plan)
                    .WithMany()
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__PassingPl__plan___4E7E8A33");
            });

            modelBuilder.Entity<PlanType>(entity =>
            {
                entity.ToTable("Plan_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("activatedDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(50)
                    .HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("Semester");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SemesterName)
                    .HasMaxLength(20)
                    .HasColumnName("semester_name");

                entity.Property(e => e.YearId).HasColumnName("year_id");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => d.YearId)
                    .HasConstraintName("FK__Semester__year_i__7775B2CE");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("class_name");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .HasColumnName("course_name");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("fullName");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(50)
                    .HasColumnName("organization_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .HasColumnName("position");

                entity.Property(e => e.Rank)
                    .HasMaxLength(50)
                    .HasColumnName("rank");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Student__class_i__1F83A428");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Student__course___1E8F7FEF");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Student__organiz__1D9B5BB6");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("class_name");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.Property(e => e.SemesterName)
                    .HasMaxLength(50)
                    .HasColumnName("semester_name");

                entity.Property(e => e.SoTiet).HasColumnName("soTiet");

                entity.Property(e => e.TimeStart)
                    .HasColumnType("date")
                    .HasColumnName("timeStart");

                entity.Property(e => e.YearId).HasColumnName("year_id");

                entity.Property(e => e.YearName)
                    .HasMaxLength(50)
                    .HasColumnName("year_name");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Subject__class_i__24485945");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK__Subject__semeste__2354350C");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.YearId)
                    .HasConstraintName("FK__Subject__year_id__226010D3");
            });

            modelBuilder.Entity<TableScore>(entity =>
            {
                entity.ToTable("TableScore");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Diemchuyencan).HasColumnName("diemchuyencan");

                entity.Property(e => e.Diemtbmon).HasColumnName("diemtbmon");

                entity.Property(e => e.Diemthi).HasColumnName("diemthi");

                entity.Property(e => e.Diemthuongxuyen).HasColumnName("diemthuongxuyen");

                entity.Property(e => e.Sotinchi).HasColumnName("sotinchi");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("student_id")
                    .IsFixedLength();

                entity.Property(e => e.StudentName)
                    .HasMaxLength(100)
                    .HasColumnName("student_name");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TableScores)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__TableScor__stude__31A25463");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TableScores)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__TableScor__subje__3296789C");
            });

            modelBuilder.Entity<TrainingPlan>(entity =>
            {
                entity.ToTable("TrainingPlan");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(100)
                    .HasColumnName("equipment_name");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.OrganzationName)
                    .HasMaxLength(100)
                    .HasColumnName("organzation_name");

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.Property(e => e.SemesterName)
                    .HasMaxLength(100)
                    .HasColumnName("semester_name");

                entity.Property(e => e.SoTiet).HasColumnName("soTiet");

                entity.Property(e => e.Sobuoi).HasColumnName("sobuoi");

                entity.Property(e => e.Start)
                    .HasColumnType("datetime")
                    .HasColumnName("start");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("subject_name");

                entity.Property(e => e.TimeEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("timeEnd");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(100)
                    .HasColumnName("type_name");

                entity.Property(e => e.YearId).HasColumnName("year_id");

                entity.Property(e => e.YearName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("year_name");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK__TrainingP__equip__46DD686B");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__TrainingP__organ__45E94432");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK__TrainingP__semes__47D18CA4");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__TrainingP__subje__4400FBC0");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__TrainingP__type___44F51FF9");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.YearId)
                    .HasConstraintName("FK__TrainingP__year___430CD787");

                entity.HasMany(d => d.Subjects)
                    .WithMany(p => p.Plans)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlanSubject",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Plan_Subj__subje__4BA21D88"),
                        r => r.HasOne<TrainingPlan>().WithMany().HasForeignKey("PlanId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Plan_Subj__plan___4C9641C1"),
                        j =>
                        {
                            j.HasKey("PlanId", "SubjectId").HasName("PK__Plan_Sub__AB9FC07BADD1588E");

                            j.ToTable("Plan_Subject");

                            j.IndexerProperty<int>("PlanId").HasColumnName("plan_id");

                            j.IndexerProperty<int>("SubjectId").HasColumnName("subject_id");
                        });
            });

            modelBuilder.Entity<UserHistory>(entity =>
            {
                entity.ToTable("UserHistory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionContent)
                    .HasMaxLength(50)
                    .HasColumnName("action_content");

                entity.Property(e => e.ActionName)
                    .HasMaxLength(50)
                    .HasColumnName("action_name");

                entity.Property(e => e.ActionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("action_time");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserHisto__user___257C74A0");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogin");

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreditCard)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("creditCard")
                    .IsFixedLength();

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("fullName");

                entity.Property(e => e.LevelId).HasColumnName("level_id");

                entity.Property(e => e.LevelName)
                    .HasMaxLength(100)
                    .HasColumnName("level_name");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(100)
                    .HasColumnName("organization_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.ResetToken)
                    .IsUnicode(false)
                    .HasColumnName("reset_token");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.StateName)
                    .HasMaxLength(100)
                    .HasColumnName("state_name");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__UserLogin__level__1CE72E9F");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__UserLogin__organ__1ECF7711");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UserLogin__role___1BF30A66");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__UserLogin__state__1DDB52D8");
            });

            modelBuilder.Entity<UserState>(entity =>
            {
                entity.ToTable("UserState");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("State_Code")
                    .IsFixedLength();

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .HasColumnName("State_Name");
            });

            modelBuilder.Entity<Year>(entity =>
            {
                entity.ToTable("Year");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("date")
                    .HasColumnName("endTime");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(50)
                    .HasColumnName("isActive");

                entity.Property(e => e.StartTime)
                    .HasColumnType("date")
                    .HasColumnName("startTime");

                entity.Property(e => e.YearName)
                    .HasMaxLength(20)
                    .HasColumnName("year_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
