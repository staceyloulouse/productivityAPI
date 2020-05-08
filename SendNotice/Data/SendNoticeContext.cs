using Microsoft.EntityFrameworkCore;
using SendNotice.Models;

namespace SendNotice.Data
{
    public class SendNoticeContext : DbContext
    {
         public SendNoticeContext(DbContextOptions<SendNoticeContext> opt) : base(opt)
        {
            
        }
        public DbSet<Notice> Notices { get; set; }  
        public DbSet<Unit> Units { get; set; }
    }
}
