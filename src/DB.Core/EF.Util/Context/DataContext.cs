using DB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB.Core.EF.Util.Context
{
    public class DataContext : DbContext
    {
        private static readonly string Conn = "Connect string";
        //public DataContext(DbContextOptions<DataContext> options)
        //    : base(options)
        //{
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Conn);
        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Message> Message { get; set; }
        public DbSet<CorpSendTextDTO> CorpSendTextDTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<PushMessageDTO> PushMessageDTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<MessageDTO> MessageDTO { get; set; }
    }
}
