using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectInfra
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Company company = new Company()
            { Id = 1, CompanyName = "BilgeAdam", CreateDate = DateTime.Now, Status = Status.Active, Address = "Istanbul", NumberOfEmployees = 5, ContactPerson = "Enis Keskin", PhoneNumber = "02122123434", TaxNumber = 123456789, TaxOffice = "Sarıyer", WebSite = "www.bilgeadam.com" };

            AppUser user = new AppUser();
            user.Status = Status.Active;
            user.UserName = "orhanb";
            user.NormalizedUserName = "ORHANB";
            user.Id = "92777eed-2f34-45af-8f32-e33d1ff9c3d9";
            user.Name = "Orhan";
            user.Surname = "Bursa";
            user.BirthDate = DateTime.Now;
            user.CompanyId = 1;
            user.CreateDate = DateTime.Now;
            user.Gender = Gender.Male;
            user.Email = "bursaorhan@outlook.com";
            user.EmailConfirmed = true;
            user.PhoneNumber = "05069806185";

            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
            user.PasswordHash = hasher.HashPassword(user, "123");

            AppUser user1 = new AppUser();
          user1.Status = Status.Active;
          user1.UserName = "sinank";
          user1.NormalizedUserName = "SINANK";
          user1.Id = "92777eed-2f34-45af-8f32-e33d1ff9c3d8";
          user1.Name = "Sinan";
          user1.Surname = "Kocaaga";
          user1.BirthDate = DateTime.Now;
          user1.CompanyId = 1;
          user1.CreateDate = DateTime.Now;
          user1.Gender = Gender.Male;
          user1.Email = "esinankocaaga@gmail.com";
          user1.EmailConfirmed = true;
          user1.PhoneNumber = "07340725825";

            
            user1.PasswordHash = hasher.HashPassword(user1, "123");

            AppUser user2 = new AppUser();
           
          user2.Status = Status.Active;
          user2.UserName = "ecek";
          user2.NormalizedUserName = "ECEK";
          user2.Id = "92777eed-2f34-45af-8f32-e33d1ff9c3d7";
          user2.Name = "Ece";
          user2.Surname = "Kocaaga";
          user2.BirthDate = DateTime.Now;
          user2.CompanyId = 1;
          user2.CreateDate = DateTime.Now;
          user2.Gender = Gender.Female;
          user2.Email = "kocaagaece@gmail.com";
          user2.EmailConfirmed = true;
          user2.PhoneNumber = "05369372279";
           
          
            
         user2.PasswordHash = hasher.HashPassword(user2, "123");

            

            builder.Entity<AppUser>().HasData(user);
            builder.Entity<AppUser>().HasData(user1);
            builder.Entity<AppUser>().HasData(user2);
            builder.Entity<Company>().HasData(company);

            base.OnModelCreating(builder);
        }
    }
}
