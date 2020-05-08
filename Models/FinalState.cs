using System;
using System.Collections.Generic;
using System.Text;

namespace Guolian.RTUDataQueryService.Models
{
   public class FinalState
    {
        public int id { get; set; }

        public int 设备ID { get; set; }

        public DateTime 记录时间 { get; set; }

        public DateTime 采集时间 { get; set; }

        public string 设备状态 { get; set; }
        public string 通讯状态 { get; set; }

        public string 数据来源 { get; set; }

        public double 五分钟雨量 { get; set; }
        public double 当前雨量 { get; set; }
        public double 小时雨量 { get; set; }
        public double 累计雨量 { get; set; }
        public double 前五分钟雨量 { get; set; }
        public double 年雨量 { get; set; }
        public double 电池电压 { get; set; }

        public double 采集水位 { get; set; }
        public double 水位 { get; set; }
        public string 门开关 { get; set; }

        public string 水位计断线报警 { get; set; }
        public string 电压低报警 { get; set; }
        public string 电压状态 { get; set; }
        public string 水位状态 { get; set; }
    }
}
