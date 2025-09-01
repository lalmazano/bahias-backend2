using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Context
{
    public class OperationContext : ModelContext
    {
        public OperationContext(DbContextOptions<OperationContext> options)
        : base(options)
        {
        }
    }
}