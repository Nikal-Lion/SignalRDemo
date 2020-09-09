using System;
using System.Collections.Generic;
using System.Text;
using DB.Core.Entities;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DB.Core.EF.Util.DataRepo
{
    public class Repository
    {
        /// <summary>
        /// 更新多条推送消息
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="msgFunc"></param>
        /// <returns></returns>
        public async static Task<int> UpdateMany(
            Expression<Func<PushMessageDTO, bool>> exp,
            Expression<Func<PushMessageDTO, Message>> msgFunc)
        {
            using var context = new Context.DataContext();
            //TODO Update many 

            await context.SaveChangesAsync();

            return -1;
        }
        /// <summary>
        /// 更新单个推送实体
        /// </summary>
        /// <param name="obj">更新对象</param>
        /// <param name="updateColumn">更新字段</param>
        /// <returns></returns>
        internal async static Task<int> Update(PushMessageDTO obj, string updateColumn)
        {

            using var context = new Context.DataContext();
            //TODO Update 

            await context.SaveChangesAsync();

            return -1;

        }
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="log"></param>
        /// <returns></returns>
        public async static Task<int> Insert<T>(T log)
        {
            using var context = new Context.DataContext();
            
            context.Entry(log).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await context.SaveChangesAsync();

            return -1;
        }
        public async static Task<int> Update<T>(T updateObj, string column) where T : class, new()
        {
            using var context = new Context.DataContext();
           
            var entry = context.Entry(updateObj);
            entry.Property(column).IsModified = true;

            await context.SaveChangesAsync();

            return -1;
        }
    }
}
