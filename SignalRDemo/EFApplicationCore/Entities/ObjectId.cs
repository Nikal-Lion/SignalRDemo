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
        /// ����
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Id��Դ
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// ��Ӧ��Id
        /// </summary>
        public string ObjId { get; set; }
    }
}