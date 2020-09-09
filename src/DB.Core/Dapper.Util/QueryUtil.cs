using Dapper;
using DB.Core.Entities;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

namespace DB.Core.Dapper.Util
{
    public class QueryUtil
    {
        //private static readonly string Conn = "Connect string";
        private static IDbConnection _context;

        public QueryUtil(IDbConnection context)
        {
            _context = context;
        }
        /// <summary>
        /// 统计未读推送信息
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async static Task<int> Count(string account, string readTime)
        {
            await _context.QueryFirstAsync<int>(
                $"select count(1) from {nameof(PushMessageDTO)} where ToUser=@ToUser and ReadTime=@ReadTime",
                new
                {
                    ToUser = account,
                    ReadTime = readTime
                }
            );

            return -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<T> First<T>(ObjectId id)
        {
            await _context.QueryAsync<T>($"select top 1 b.* from ObjectId a inner join {nameof(T)} b on a.ObjId = b.Id where a.Id=@ID", new { ID = id.Id });
            return default(T);
        }
        /// <summary>
        /// 查找Objective集合
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="msgFunc"></param>
        /// <param name="flag"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pushFunc"></param>
        public async static Task<PagedData<PushMessageDTO>> FindPageObjectList(
            Expression<Func<Message, bool>> exp,
            Expression<Func<Message, string>> msgFunc,
            bool flag,
            int pageIndex,
            int pageSize,
            Expression<Func<Message, PushMessageDTO>> pushFunc)
        {
            PagedData<PushMessageDTO> data = new PagedData<PushMessageDTO>();
            //data.TList
            //            @"SELECT  *
            //FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY InsertDate) AS RowNum, *
            //          FROM      Posts
            //          WHERE     InsertDate >= '1900-01-01'
            //        ) AS result
            //WHERE   RowNum >= 1 // *your pagination parameters
            //    AND RowNum < 20  //*
            //ORDER BY RowNum"

            //TODO tranlate Expression to raw sql 
            //string where = exp.ToString().Replace(" AndAlso ", " and ");

            string properties = string.Join(",", typeof(PushMessageDTO).GetProperties(System.Reflection.BindingFlags.Public).ToList());

            data.TList.AddRange(await _context.QueryAsync<PushMessageDTO>($"select * from (select  ROW_NUMBER() OVER ( ORDER BY CreateTime ) AS RowNum ,{properties} from {nameof(PushMessageDTO)} where 1=1 {exp.ToString()} where RowNum >= {pageIndex} and RowNum < {pageSize}) order by RowNum"));
            return null;
        }
    }
}
