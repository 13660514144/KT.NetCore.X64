using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KT.Quanta.Kone.ToolApp.Enums
{
    public class PublicBaseEnum
    {
        public PublicBaseEnum()
        {
        }
        public PublicBaseEnum(int code, string value, string text)
        {
            Code = code;
            Value = value;
            Text = text;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 显示文本
        /// 尽量不要使作，设置区域语言时无法配置
        /// </summary>
        public string Text { get; set; }

        public static IEnumerable<T> GetAll<T>() where T : PublicBaseEnum, new()
        {
            var type = typeof(T);
            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as PublicBaseEnum;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static T FromCode<T>(int value) where T : PublicBaseEnum, new()
        {
            var matchingItem = Parse<T, int>(value, "code", item => item.Code == value);
            return matchingItem;
        }

        public static T FromValue<T>(string value) where T : PublicBaseEnum, new()
        {
            var matchingItem = Parse<T, string>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromText<T>(string displayName) where T : PublicBaseEnum, new()
        {
            var matchingItem = Parse<T, string>(displayName, "text", item => item.Text == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : PublicBaseEnum, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                //var message = $"'{value}' is not a valid {description} in {typeof(T)}";
                //throw new ApplicationException(message);
                return null;
            }

            return matchingItem;
        }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((PublicBaseEnum)other).Value);
        }
    }
}
