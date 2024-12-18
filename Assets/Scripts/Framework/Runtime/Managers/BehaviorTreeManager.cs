﻿/*
 * 文件名：BehaviorTreeManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 文件描述：
 * 行为树管理器类
 */

using Framework.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Framework.Runtime.Behavior
{
    public class BehaviorTreeManager : Singleton<BehaviorTreeManager>
    {
        private const string ResRoot = "BehaviorTrees";

        private readonly Dictionary<string, BehaviorTree> _loadedTrees;

        private readonly List<Type> _types;

        public BehaviorTreeManager() 
        {
            _loadedTrees = new Dictionary<string, BehaviorTree>();
            _types = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                _types.AddRange(assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BehaviorNode))));
            }
        }

        /// <summary>
        /// 载入行为树
        /// </summary>
        public BehaviorTree LoadTree(string treeName, XElement xml)
        {
            BehaviorTree tree = new BehaviorTree();
            var queue = new Queue<(XElement Element, BehaviorNode ParentNode)>();
            queue.Enqueue((xml, null));
            while (queue.Count > 0)
            {
                (XElement Element, BehaviorNode ParentNode) = queue.Dequeue();
                var type = _types.FirstOrDefault(t => t.Name == Element.Name.LocalName);
                if (type != null)
                {
                    var obj = Activator.CreateInstance(type);
                    if (obj is BehaviorNode node)
                    {
                        if (ParentNode == null)
                        {
                            tree.SetRoot(node);
                        }
                        else if (ParentNode is CompositeNode composite)
                        {
                            composite.AddChildren(node);
                        }
                        using (var childrenElements = Element.Elements().GetEnumerator())
                        {
                            while (childrenElements.MoveNext())
                            {
                                queue.Enqueue((childrenElements.Current, node));
                            }
                        }
                    }
                }
            }
            _loadedTrees.Add(treeName, tree);
            return tree;
        }

        /// <summary>
        /// 获取行为树
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BehaviorTree GetTree(string name)
        {
            if (_loadedTrees.TryGetValue(name, out var tree))
            {
                return tree;
            }
            // 若缓存中不存在树，尝试载入文件
            return LoadTree(name, XMLHelper.XmlLoad($"{ResRoot}/{name}"));
        }
    }
}

