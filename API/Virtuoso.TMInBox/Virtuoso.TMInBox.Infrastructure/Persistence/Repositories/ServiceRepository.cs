using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtuoso.TMInBox.Core.Interfaces.Repository;
using Virtuoso.TMInBox.Core.Models;

namespace Virtuoso.TMInBox.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository:GenericRepository<Service, int>, IServiceRepository
    {
        public ServiceRepository(TMInBoxDbContext context) : base(context) { }
    }
}
