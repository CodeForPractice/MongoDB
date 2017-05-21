using System;
using System.ComponentModel;
using System.Reflection;

namespace Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：Extension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Extension
    /// 创建标识：yjq 2017/5/21 1:29:05
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="enumField"></param>
        /// <returns></returns>
        public static string Desc(this Enum enumField)
        {
            Type enumType = enumField.GetType();
            MemberInfo[] memberInfos = enumType.GetMember(enumField.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                var description = memberInfos[0].GetCustomAttribute<DescriptionAttribute>();
                return description?.Description;
            }
            return string.Empty;
        }
    }
}