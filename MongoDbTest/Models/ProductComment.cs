using System;

namespace Models
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ProductComment.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：商品评论
    /// 创建标识：yjq 2017/5/18 15:08:32
    /// </summary>
    public sealed class ProductComment
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}