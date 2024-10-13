/*
 * 文件名：XMLHelper.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/14
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/14
 * 
 * 文件描述：
 * 静态XML工具类
 */

using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Framework.Units
{
    public static class XMLHelper
    {
        public static IEnumerable<XElement> XmlLoad(string path)
        {
            XDocument xDoc = XDocument.Load($"{Application.dataPath}/{path}");
            return xDoc.Elements();
        }

        public static void XmlSave(IEnumerable<XElement> xElements, string path)
        {
            XDocument xmlDoc = new XDocument(xElements);
            xmlDoc.Save($"{Application.dataPath}/{path}");
        }
    }
}

