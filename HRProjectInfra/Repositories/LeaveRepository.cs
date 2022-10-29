using HRProjectDomain.Entities;
using HRProjectDomain.Repositories;
using HRProjectInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectInfrastructure.Repositories
{
    public class LeaveRepository : BaseRepository<Leave>, ILeaveRepository
    {
        public LeaveRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
