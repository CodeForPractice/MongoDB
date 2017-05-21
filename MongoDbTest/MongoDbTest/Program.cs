using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Models;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace MongoDbTest
{
    class Program
    {
        private static string _MongoDbConnectionStr = "mongodb://yjq:123456@localhost:27017/admin";

        static void Main(string[] args)
        {
            var productCollection = GetCollection<Product>();
            //添加一个待审核的商品
            //Product product = new Product("苹果", (decimal)5.20);
            //productCollection.InsertOne(product);
            //Console.WriteLine($"添加商品：{product.ToString()}成功。");

            //批量增加商品
            //List<Product> productAddList = new List<Product>();
            //for (int i = 0; i < 100; i++)
            //{
            //    productAddList.Add(GetRandomProduct());
            //}
            //productCollection.InsertMany(productAddList);
            //var productList = productCollection.Find(new BsonDocument()).ToList();
            //foreach (var item in productList)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //#region 全部商品
            //FilterDefinition<Product> filter = new BsonDocument();
            //long productAllCount = productCollection.Count(filter);
            //var productList = productCollection.Find(filter).Skip(0).Limit(20).ToList();
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"总记录数为{productAllCount.ToString()}");
            //Console.ForegroundColor = ConsoleColor.Blue;
            //Console.WriteLine("前20条商品信息为：");
            //Console.ForegroundColor = ConsoleColor.White;
            //foreach (var item in productList)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //#endregion

            //#region 待审核lambda表达式查询
            //Expression<Func<Product, bool>> expression = m => m.SaleState == ProductSaleState.WaitingCheck;
            //long productAllCount = productCollection.Count(expression);
            //var productList = productCollection.Find(expression).Skip(0).Limit(20).ToList();
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"总记录数为{productAllCount.ToString()}");
            //Console.ForegroundColor = ConsoleColor.Blue;
            //Console.WriteLine("前20条商品信息为：");
            //Console.ForegroundColor = ConsoleColor.White;
            //foreach (var item in productList)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //#endregion

            //#region 待审核 filter构建
            //var filter = Builders<Product>.Filter.Eq("SaleState", ProductSaleState.WaitingCheck);
            //long productAllCount = productCollection.Count(filter);
            //var productList = productCollection.Find(filter).Skip(0).Limit(20).ToList();
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"总记录数为{productAllCount.ToString()}");
            //Console.ForegroundColor = ConsoleColor.Blue;
            //Console.WriteLine("前20条商品信息为：");
            //Console.ForegroundColor = ConsoleColor.White;
            //foreach (var item in productList)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //#endregion


            //#region 更新销售状态并添加一条评论
            //var beforeUpdateProduct = productCollection.Find(m => m.SaleState == ProductSaleState.WaitingCheck).FirstOrDefault();
            //Console.WriteLine($"更新前信息{beforeUpdateProduct?.ToString()}");
            ////注意线程安全，这里只是做演示
            //beforeUpdateProduct.Comment("哇，这个好好吃啊！");
            //var updateFilter = Builders<Product>.Update.Set(m => m.SaleState, ProductSaleState.OnSale).Set(m => m.ModifyTime, DateTime.Now).Set(m => m.Comments, beforeUpdateProduct.Comments);
            //var updateResult = productCollection.UpdateOne(m => m.Id == beforeUpdateProduct.Id, updateFilter);
            //if (updateResult.IsModifiedCountAvailable)
            //{
            //    var afterUpdateProduct = productCollection.Find(m => m.Id == beforeUpdateProduct.Id).FirstOrDefault();
            //    Console.WriteLine("更新销售状态成功=====");
            //    Console.WriteLine($"更新后信息{afterUpdateProduct?.ToString()}");
            //    Console.WriteLine("评论信息：");
            //    afterUpdateProduct.ShowComments();
            //}
            //else
            //{
            //    Console.WriteLine("更新失败=====");
            //}
            //#endregion

            //#region 查询有评论待审核的商品
            //var commentWaitingCheckProducts = productCollection.Find(m => m.Comments.Where(k => k.CheckState == CommentCheckState.WaitingCheck).Any()).ToEnumerable();
            //foreach (var item in commentWaitingCheckProducts)
            //{
            //    Console.WriteLine(item.ToString());
            //} 
            //#endregion

            var projection = Builders<Product>.Projection.Expression(m => new ProductDto
            {
                Comments = m.Comments,
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                SaleState = m.SaleState
            });

            var commentWaitingCheckProducts = productCollection.Find(m => m.Comments.Where(k => k.CheckState == CommentCheckState.WaitingCheck).Any()).Project(projection).ToEnumerable();
            foreach (var item in commentWaitingCheckProducts)
            {
                Console.WriteLine(item.ToString());
            }

            Console.Read();


        }

        private static IMongoCollection<T> GetCollection<T>(string collectionName = null)
        {
            MongoUrl mongoUrl = new MongoUrl(_MongoDbConnectionStr);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            return database.GetCollection<T>(collectionName ?? typeof(T).Name);
        }

        private static string[] _ProductNames = new string[] { "苹果", "香蕉", "菠萝", "哈密瓜", "西瓜", "黄瓜", "草莓", "桃子", "芒果", "猕猴桃", "梨" };
        private static Random rn = new Random();
        private static Product GetRandomProduct()
        {
            var i = rn.Next(_ProductNames.Length);
            decimal price = i * 15;
            var enumValue = rn.Next(1, 5);
            return new Product(_ProductNames[i], price, (ProductSaleState)enumValue);
        }
    }
}
