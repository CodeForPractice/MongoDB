using System.ComponentModel;

namespace Models
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：CommentCheckState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：评论审核状态
    /// 创建标识：yjq 2017/5/21 0:51:43
    /// </summary>
    public enum CommentCheckState
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        WaitingCheck = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Passed = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        NotPass = 3
    }
}