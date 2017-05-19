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

        public Product(string name, decimal price) : this()
        {
            Name = name;
            Price = price;
            CreateTime = DateTime.Now;
        }

        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        public List<ProductComment> Comments { get; set; }

        public override string ToString()
        {
            return $"{_id}:{Name}价格{Price}元,上架时间：{CreateTime.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
    }
}