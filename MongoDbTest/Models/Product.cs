using Infrastructure;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：Product.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：商品
    /// 创建标识：yjq 2017/5/18 14:59:35
    /// </summary>
    public sealed class Product
    {
        public Product()
        {
        }

        public Product(string name, decimal price) : this(name, price, ProductSaleState.WaitingCheck)
        {
        }

        public Product(string name, decimal price, ProductSaleState saleState)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Price = price;
            SaleState = saleState;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        [BsonElement(elementName: "_id")]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 商品名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public ProductSaleState SaleState { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 商品评论
        /// </summary>
        public List<ProductComment> Comments { get; set; }

        public override string ToString()
        {
            return $"{Id}:{Name},价格{Price}元,审核状态{SaleState.Desc()}";
        }

        public void ShowComments()
        {
            if (Comments != null)
            {
                foreach (var item in Comments)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            
        }

        public void Comment(string content)
        {
            if (Comments == null)
            {
                Comments = new List<Models.ProductComment>();
            }
            Comments.Add(new Models.ProductComment(content));
        }
    }
}