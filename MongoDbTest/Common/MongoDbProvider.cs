using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：MongoDbProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/5/19 13:08:15
    /// </summary>
    public sealed class MongoDbProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">包含库名的连接字符串</param>
        /// <returns></returns>
        public IMongoDatabase GetDataBase(string connectionString)
        {
            MongoUrl mongoUrl = new MongoUrl(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            return mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
