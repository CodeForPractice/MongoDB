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
        private static string _MongoDbConnectionStr = "mongodb://yjq:123456@localhost:27017/admin";

        static void Main(string[] args)
        {
            JQConfiguration.Create("MongDB")
                           .UseDefaultConfig();
            LogUtil.Debug("程序开始");

            MongoUrl mongoUrl = new MongoUrl(_MongoDbConnectionStr);

            var mongoClient = new MongoClient(mongoUrl);
            var dataBase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            Product product = new Product("芒果", (decimal)12.5);
            product._id = ObjectId.GenerateNewId();
            LogUtil.Warn(product._id.ToString());
            var productCollection = dataBase.GetCollection<Product>(typeof(Product).Name);
            productCollection.InsertOne(product);
            LogUtil.Info("新增一个商品成功");
            var findProduct =productCollection.Find(m => m.Name == "芒果").FirstOrDefault();
            LogUtil.Warn(findProduct?.ToString());
            var productList = productCollection.Find(m => m.CreateTime > DateTime.Now.AddHours(-9)).SortBy(m => m.CreateTime).Skip(1).Limit(10).ToList();
            foreach (var item in productList)
            {
                LogUtil.Info(item.ToString());
            }

            //var bsonDocumentCollection = dataBase.GetCollection<Product>(typeof(Product).Name);
            //Product product = new Product("苹果99", (decimal)12.5);
            //product.Comments = new List<ProductComment> {
            //    new ProductComment {  Content="这苹果很好吃", CreateTime=DateTime.Now, ProductId=0,Id=1}
            //};
            //bsonDocumentCollection.InsertOne(product);

            var filter = Builders<Product>.Filter.Eq(m => m.Name, "香蕉");

            //var productCollection = dataBase.GetCollection<Product>(typeof(Product).Name);

            //var productList = productCollection.Find(m => m.Name.Contains("苹果") && m.Comments.Count > 0).ToList();
            ////var productList =productCollection.Find(filter).ToList();
            //foreach (var item in productList)
            //{
            //    LogUtil.Info(item.ToString());
            //}
            //var update = Builders<Product>.Update.Set(m => m.Name, "猕猴桃").Set(m => m.Price, (decimal)9.89);
            ////productCollection.UpdateMany(m => m.Name.Contains("苹果"), update);
            //var updatedProduct = productCollection.FindOneAndUpdate(mbox => mbox.Name == "猕猴桃1", update);
            //LogUtil.Warn(updatedProduct.ToString());
            //foreach (var item in productList)
            //{
            //    LogUtil.Info(item.ToString());
            //}
            Console.Read();
            JQConfiguration.UnInstall();

        }
    }
}
