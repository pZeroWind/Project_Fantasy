/*
 * 文件名：XMLHelper.cs
 * 作者：ZeroWind
 * 创建时间：2024/10/14
 * 
 * 最后编辑者：ZeroWind
 * 最后编辑时间：2024/10/17
 * 
 * 文件描述：
 * 静态XML工具类
 */

using Framework.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Framework.Units
{
    public static class XMLHelper
    {
        public static XElement XmlLoad(string path)
        {
            var xmlAsset = GameResourceManager.Instance.Load<TextAsset>(path);
            if (xmlAsset == null) return default;
            XDocument xDoc = XDocument.Parse(xmlAsset.text);
            return xDoc.Elements().FirstOrDefault();
        }

        public static XElement Parse(TextAsset xmlAsset)
        {
            XDocument xDoc = XDocument.Parse(xmlAsset.text);
            return xDoc.Elements().FirstOrDefault();
        }

        public static void XmlSave(IEnumerable<XElement> xElements, string path)
        {
            path = $"{Application.dataPath}/{path}.xml";
            XDocument xmlDoc = new XDocument(xElements);
            if (!File.Exists(path))
            {
                using var fs = File.Create(path);
                xmlDoc.Save(fs);
            }
            else
            {
                xmlDoc.Save(path);
            }
        }
    }
}

