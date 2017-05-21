using System.ComponentModel;

namespace Models
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProductSaleState.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：商品销售状态
    /// 创建标识：yjq 2017/5/21 0:36:02
    /// </summary>
    public enum ProductSaleState
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        WaitingCheck = 1,

        /// <summary>
        /// 上架
        /// </summary>
        [Description("上架")]
        OnSale = 2,

        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        OffShelves = 3,

        /// <summary>
        /// 已销售
        /// </summary>
        [Description("已销售")]
        Saled = 4
    }
}