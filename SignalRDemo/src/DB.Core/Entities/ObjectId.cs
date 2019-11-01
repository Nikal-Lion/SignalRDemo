using System;

namespace DB.Core.Entities
{
    public class ObjectId
    {
        public static string GenerateNewId(DateTime time)
        {
            return string.Empty;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Id来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 对应表Id
        /// </summary>
        public string ObjId { get; set; }
    }
}