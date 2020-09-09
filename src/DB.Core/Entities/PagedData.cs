using System;
using System.Collections.Generic;

namespace DB.Core.Entities
{
    public class PagedData<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public List<T> TList { get; set; }
    }
}
