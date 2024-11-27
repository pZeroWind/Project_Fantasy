/*
 * 文件名：SqliteManager.cs
 * 作者：ZeroWind
 * 创建时间：2024/11/27
 * 
 * 文件描述：
 * 拥有管理Sqlite数据库
 */

using SQLite;
using System;
using System.IO;
using UnityEngine;

namespace Framework.Runtime
{
    public class SqliteManager : Singleton<SqliteManager>
    {
        private SQLiteConnection _connection;

        public override void OnInit()
        {
            var databasePath = Path.Combine(Application.streamingAssetsPath , "project_fantasy.db");
            _connection = new SQLiteConnection(databasePath);
            CreateTables();
        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        private void CreateTables(params Type[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                _connection.CreateTable(type);
            }
        }
    }
}

