using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Infrastructure;

namespace Models
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProductDto.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ProductDto
    /// 创建标识：yjq 2017/5/21 17:45:24
    /// </summary>
    public sealed class ProductDto
    {
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
    }
}