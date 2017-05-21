using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：MongoDbBaseRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/5/19 14:31:55
    /// </summary>
    public class MongoDbBaseRepository<T> where T : class
    {
        private readonly MongoDbConnection _mongodbConnection;

        private IMongoDatabase _mongoDatabase;

        public MongoDbBaseRepository(MongoDbConnection mongodbConnection)
        {
            if (mongodbConnection == null)
            {
                throw new ArgumentNullException("mongodbConnection");
            }
            _mongodbConnection = mongodbConnection;
        }

        public MongoDbConnection MongoDbConnection { get { return _mongodbConnection; } }

        public IMongoDatabase MongoDatabase
        {
            get
            {
                if (_mongoDatabase == null)
                {
                    MongoUrl mongoUrl = new MongoUrl(MongoDbConnection.ConnectionString);
                    MongoClient mongoClient = new MongoClient(mongoUrl);
                    _mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
                }
                return _mongoDatabase;
            }
        }

        public virtual IMongoCollection<T> Collection
        {
            get
            {
                return MongoDatabase.GetCollection<T>(typeof(T).Name);
            }
        }

        public void Insert(T entity)
        {
            Collection.InsertOne(entity);
        }

        public Task InsertAsync(T entity)
        {
            return Collection.InsertOneAsync(entity);
        }

        public void InsertMany(List<T> entities)
        {
            Collection.InsertMany(entities);
        }

        public Task InsertManyAsync(List<T> entities)
        {
            return Collection.InsertManyAsync(entities);
        }

        public bool Update(Expression<Func<T, bool>> filter, IDictionary<string, object> updateDic)
        {
            if (updateDic == null)
            {
                throw new ArgumentNullException("updateDic");
            }
            UpdateDefinition<T> updateDefin = null;

            foreach (var item in updateDic)
            {
                if (updateDefin == null)
                {
                    updateDefin = Builders<T>.Update.Set(item.Key, item.Value);
                }
                else
                {
                    updateDefin = updateDefin.Set(item.Key, item.Value);
                }
            }
            var updateResult = Collection.UpdateOne(filter, updateDefin);
            return updateResult.IsModifiedCountAvailable;
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> filter, IDictionary<string, object> updateDic)
        {
            if (updateDic == null)
            {
                throw new ArgumentNullException("updateDic");
            }
            UpdateDefinition<T> updateDefin = null;

            foreach (var item in updateDic)
            {
                if (updateDefin == null)
                {
                    updateDefin = Builders<T>.Update.Set(item.Key, item.Value);
                }
                else
                {
                    updateDefin = updateDefin.Set(item.Key, item.Value);
                }
            }
            var updateResult = await Collection.UpdateOneAsync(filter, updateDefin);
            return updateResult.IsModifiedCountAvailable;
        }

    }
}
