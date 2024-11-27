/*
 * �ļ�����SqliteManager.cs
 * ���ߣ�ZeroWind
 * ����ʱ�䣺2024/11/27
 * 
 * �ļ�������
 * ӵ�й���Sqlite���ݿ�
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
        /// �������ݱ�
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

