using System;
using System.Collections.Generic;
using System.Text;

namespace Guolian.RTUDataQueryService.Models
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class EquipmentInformation
    {
 
        public int ID { get; set; }

        public string 名称 { get; set; }

        public string 用户站类型 { get; set; }
    
        public string 用户站参数 { get; set; }

       
        public string 管理ID { get; set; }

        public string 通讯设备ID { get; set; }

       public string 传输设备ID { get; set; }

        public int 数据采集方式 { get; set; }

        public string 是否启用 { get; set; }

        public string 衍生相关量 { get; set; }

        public string 显示量 { get; set; }


    }
}
