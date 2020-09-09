namespace DB.Core.Entities
{
    public class MessageDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool PushCorpWeixin { get; set; }
    }
}