using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KT.Common.WebApi.Helpers
{
    /// <summary>
    /// Assembly扩展
    /// </summary>
    public static class AssemblyExtenstions
    {
        /// <summary>
        /// 获取所有与当前项目前辍相同的项目
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetOwnerAll()
        {
            //获取当前项目前辍
            var customStartWith = Assembly.GetExecutingAssembly()
                .GetName()
                .Name.Split('.')
                .FirstOrDefault() + ".";

            //获取当前类引用
            var assemblie = Assembly.GetEntryAssembly();

            //加载引用库
            var assemblies = GetChildAssemblies(customStartWith, assemblie);

            //将当前引用加入队列
            assemblies.AddIfNotExists(assemblie);

            return assemblies;
        }


        /// <summary>
        /// 加载子引用库
        /// </summary>
        /// <param name="customStartWith"></param>
        /// <param name="assemblies">当前类引用库</param>
        /// <returns>子引用</returns>
        private static List<Assembly> GetChildAssemblies(string customStartWith, Assembly assemblie)
        {
            var results = new List<Assembly>();

            //获取所有相同前辍的引用库
            var referenceAssemblieNames = assemblie.GetReferencedAssemblies();
            if (referenceAssemblieNames?.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var referenceAssemblieName in referenceAssemblieNames)
            {
                //获取所有相同前辍的引用库 
                if (!referenceAssemblieName.Name.StartsWith(customStartWith))
                {
                    continue;
                }
                var referenceAssemblie = Assembly.Load(referenceAssemblieName);
                results.AddIfNotExists(referenceAssemblie);

                //获取子引用
                var childAssemblies = GetChildAssemblies(customStartWith, referenceAssemblie);
                if (childAssemblies?.FirstOrDefault() == null)
                {
                    continue;
                }
                results.AddIfNotExists(childAssemblies);
            }

            return results;
        }

        /// <summary>
        /// 向列表中添加不存在的Assembly
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="assembly"></param>
        private static void AddIfNotExists(this List<Assembly> assemblies, Assembly assembly)
        {
            if (assemblies == null)
            {
                assemblies = new List<Assembly>();
            }

            if (assembly == null)
            {
                return;
            }
            else if (assemblies.Any(x => x.FullName == assembly.FullName))
            {
                return;
            }

            assemblies.Add(assembly);
        }

        /// <summary>
        /// 向列表中添加不存在的Assembly
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="assembly"></param>
        private static void AddIfNotExists(this List<Assembly> assemblies, List<Assembly> addAssemblies)
        {
            if (addAssemblies?.FirstOrDefault() != null)
            {
                foreach (var item in addAssemblies)
                {
                    AddIfNotExists(assemblies, item);
                }
            }

        }
    }
}
