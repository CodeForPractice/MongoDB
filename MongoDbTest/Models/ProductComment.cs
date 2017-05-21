using Infrastructure;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public ProductComment(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            Id = ObjectId.GenerateNewId();
            Content = content;
            CheckState = CommentCheckState.WaitingCheck;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 评论ID
        /// </summary>
        [BsonElement(elementName: "_id")]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论审核状态
        /// </summary>
        public CommentCheckState CheckState { get; set; }

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

        public override string ToString()
        {
            return $"评论信息:{Content},审核状态:{CheckState.Desc()}";
        }
    }
}