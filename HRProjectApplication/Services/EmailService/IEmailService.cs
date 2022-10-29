using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.EmailService
{
    public interface IEmailService
    {
        void SendUserInfoEmail(AppUser user, string managerEmail, string pass);
        void SendPasswordResetEmail(AppUser user, string link);
    }
}
