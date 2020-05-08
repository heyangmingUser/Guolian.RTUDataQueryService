using Dapper;
using Guolian.RTUDataQueryService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guolian.RTUDataQueryService
{
    public class QueryService
    {
        private readonly ConnectConfigs config;
        readonly ILogger<QueryService> log;//日志记录
        ILoggerFactory _loggerFactory;

        public QueryService(IOptions<ConnectConfigs> options, ILoggerFactory loggerFactory) 
        {
            config = options.Value;
            _loggerFactory = loggerFactory;
            log = loggerFactory.CreateLogger<QueryService>();
        }

        /// <summary>
        /// 获取所有管理信息，无上级Id的或者上级Id为0的
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetManagementInformation() 
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try 
                {
                    var informations = await connection.QueryAsync<ManagementInformation>("select * from 管理信息 where  上级ID=0 or 上级ID=null");
                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex) 
                {
                    log.LogError("查询父管理信息异常:{0}", ex);
                }
               
                return null;
            }
        }
        /// <summary>
        /// 根据父Id获取子管理信息
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetManagementInformation(int superiorId)
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryAsync<ManagementInformation>($"select * from 管理信息 where 上级ID='{superiorId}'");
                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }
                
                return null;
            }
        }
        /// <summary>
        /// 获取设备信息管理区域对应的设备信息
        /// </summary>
        /// <param name="managementId">管理主键</param>
        /// <returns></returns>
        public async Task<object> GetEquipmentInformation(int managementId)
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryAsync<EquipmentInformation>($"select * from 设备信息 where 管理ID='{managementId}'");
                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }

                return null;
            }
        }
        /// <summary>
        ///通过设备信息主键获取设备的信息和辅助信息
        /// </summary>
        /// <param name="EquipmentId"></param>
        /// <returns></returns>
        public async Task<object> GetEquipmentInformationByID(int EquipmentId)
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryFirstOrDefaultAsync<EquipmentInformation>($"select * from 设备信息 where ID='{EquipmentId}'");
                    var assists = await connection.QueryAsync<EquipmentAssistInformation>($"select * from 设备辅助信息 where 设备ID='{EquipmentId}'");
                    if (informations != null )
                    {
                        return (informations, assists);
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }

                return null;
            }
        }

        /// <summary>
        /// 获取指定设备的不同年份的历史记录
        /// </summary>
        /// <param name="EquipmentId"></param>
        /// <returns></returns>
        public async Task<object> GetHistory(int EquipmentId,string Year)
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                var eq = EquipmentId.ToString();
                int i = 6-eq.Length;
                if (eq.Length > 6)
                {
                    eq = eq.Substring(0, 6);
                }
                else 
                {
                    for(int j = 0; j<i; j++)
                    {
                        eq = "0" + eq;
                    }
                }
                try
                {
                    string sql = $"历史记录_{eq}_{Year}";
                    var informations = await connection.QueryAsync<History>($"select * from {sql}");
                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }
                return null;
            }
        }
        /// <summary>
        /// 获取指定设备的全局状态（将所有测点的最新数据实时更新到一张表中）
        /// </summary>
        /// <param name="EquipmentId"></param>
        /// <returns></returns>
        public async Task<object> GetEquipmentGlobalState(int EquipmentId)
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryAsync<GlobalState>($"select * from 全局状态 where 设备ID='{EquipmentId}'");
                 
                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }
                return null;
            }
        }
        /// <summary>
        /// 获取所有测点的全局状态
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetEquipmentGlobalState() 
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryAsync<GlobalState>($"select * from 全局状态 ");

                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }
                return null;
            }
        }
        /// <summary>
        /// 获取所有测点的最后状态（最后一次有效数据记录）
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetEquipmentFinalState()
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryAsync<FinalState>($"select * from 最后状态 ");

                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }
                return null;
            }
        }
        /// <summary>
        /// 获取指定设备的最后状态
        /// </summary>
        /// <param name="EquipmentId"></param>
        /// <returns></returns>
        public async Task<object> GetEquipmentFinalState(int EquipmentId)
        {
            using (IDbConnection connection = new SqlConnection(config.Name))
            {
                try
                {
                    var informations = await connection.QueryAsync<FinalState>($"select * from 最后状态  where 设备ID='{EquipmentId}'");

                    if (informations != null && informations.Count() > 0)
                    {
                        return informations;
                    }
                }
                catch (Exception ex)
                {
                    log.LogError("查询子管理信息异常:{0}", ex);
                }
                return null;
            }
        }
    }
}
