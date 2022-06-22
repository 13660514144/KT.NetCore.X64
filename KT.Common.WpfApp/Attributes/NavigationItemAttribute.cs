using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WpfApp.Attributes
{
    /// <summary>
    /// 导航菜单特性
    /// AllowMultiple特性影响编译器，AttributeTargets修饰的对象 AllowMultiple：能否重复修饰 Inherited:是否可继承
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NavigationItemAttribute : Attribute
    {

    }
}
