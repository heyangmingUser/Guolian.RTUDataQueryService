using System;
using System.Collections.Generic;
using System.Text;

namespace Guolian.RTUDataQueryService.Models
{
    /// <summary>
    /// 管理信息
    /// </summary>
    public class ManagementInformation
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 级别Id
        /// </summary>
        public int? 级别ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string 名称 { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public int? 上级ID { get; set; }

        /// <summary>
        /// 级别描述
        /// </summary>
        public string 级别描述 { get; set; }
    }
}
