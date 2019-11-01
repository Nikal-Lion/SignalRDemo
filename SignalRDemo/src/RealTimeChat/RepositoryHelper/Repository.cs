using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RealTimeChat.Entities;

namespace RealTimeChat.RepositoryHelper
{
    public class Repository
    {
        public static Task Insert(Message message)
        {

            throw new Exception("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="msg"></param>
        /// <param name="flag"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sto"></param>
        public static Task<PagedData<PushMessageDTO>> FindPageObjectList(
            Expression<Func<Message, bool>> exp,
            Func<Message, string> func,
            bool flag,
            int pageIndex,
            int pageSize,
            Func<Message, PushMessageDTO> sto)
        {
            return null;
        }
        public static Task<int> Count(Expression<Func<PushMessageDTO, bool>> exp)
        {
            return null;
        }

        public static Task<int> UpdateMany(Expression<Func<PushMessageDTO, bool>> exp, Expression<Func<object, Message>> p2)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="updateColumn"></param>
        /// <returns></returns>
        internal static Task Update(PushMessageDTO dto, string updateColumn)
        {

            throw new NotImplementedException();
        }

        internal static Task<PushMessageDTO> First(ObjectId id)
        {
            var _id = id ?? new ObjectId();
            _id.ToString();
            throw new NotImplementedException();
        }
    }
}