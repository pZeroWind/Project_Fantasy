/*
 * 文件名：BehaviorTree.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/13
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 行为树类
 */

using System.Collections;
using System.Collections.Generic;

namespace Framework.Runtime.Behavior
{
    public class BehaviorTree
    {
        private BehaviorNode _root;

        public void SetRoot(BehaviorNode root) => _root = root;

        public void OnUpdate(Blackborad blackborad, float fTick)
        {
            if (_root != null)
                _root.OnExecute(blackborad, fTick);
        }
    }
}

