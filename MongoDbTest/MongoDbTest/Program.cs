using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JQ.Configurations;
using JQ.Utils;
using MongoDB.Driver;
using Models;
using MongoDB.Bson;

namespace MongoDbTest
{
    class Program
    {
        private static string _MongoDbConnectionStr = "mongodb://yjq:123456@localhost:27017";

        static void Main(string[] args)
        {
            JQConfiguration.Create("MongDB")
                           .UseDefaultConfig();
            LogUtil.Debug("程序开始");

            var mongoClient = new MongoClient(_MongoDbConnectionStr);
            var dataBase = mongoClient.GetDatabase("admin");
            var bsonDocumentCollection = dataBase.GetCollection<Product>(typeof(Product).Name);
            Product product = new Product("苹果", (decimal)12.5);
            bsonDocumentCollection.InsertOne(product);

            var filter = Builders<Product>.Filter.Eq(m=>m.Name, "香蕉");
            
            var productCollection= dataBase.GetCollection<Product>(typeof(Product).Name);
            var productList =productCollection.Find(filter).ToList();
            foreach (var item in productList)
            {
                LogUtil.Info(item.ToString());
            }
            Console.Read();
            JQConfiguration.UnInstall();

        }
    }
}
