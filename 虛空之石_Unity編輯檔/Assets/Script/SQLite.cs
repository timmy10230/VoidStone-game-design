using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class SQLite
{
	public SqliteConnection connection;
	private SqliteCommand command;


	public SQLite(string path)
	{
		connection = new SqliteConnection(path);    // 建立SQLite物件的同時，建立SqliteConnection物件
		connection.Open();                          // 開啟資料庫連結
		Command();
        
	}


	public SqliteCommand Command()
	{
		command = connection.CreateCommand();
		return command;
	}


	//建立資料庫程式

	/*public void CreateSQLiteDatabase(string Database)
	{
		string cnstr = string.Format("Data Source=" + Database + ";Version=3;New=True;Compress=True;");
		SQLiteConnection icn = new SQLiteConnection();
		icn.ConnectionString = cnstr;
		icn.Open();
		icn.Close();
	}

	//建立資料表程式

	public void CreateSQLiteTable(string Database, string CreateTableString)
	{
		SQLiteConnection icn = OpenConn(Database);
		SQLiteCommand cmd = new SQLiteCommand(CreateTableString, icn);
		SQLiteTransaction mySqlTransaction = icn.BeginTransaction();
		try
		{
			cmd.Transaction = mySqlTransaction;
			cmd.ExecuteNonQuery();
			mySqlTransaction.Commit();
		}
		catch (Exception ex)
		{
			mySqlTransaction.Rollback();
			throw (ex);
		}
		if (icn.State == ConnectionState.Open) icn.Close();
	}*/

	
	// 建立資料表
	// <returns>The table.</returns>
	// <param name="tableName">資料表名</param>
	// <param name="colNames">欄位名</param>
	// <param name="colTypes">欄位名型別</param>

	public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
	{
		
		command.CommandText = "CREATE TABLE  '"  + tableName + "'( " + colNames[0] + " " + colTypes[0];
		for (int i = 1; i < colNames.Length; i++)
		{
			command.CommandText += ", " + colNames[i] + " " + colTypes[i];
		}
		command.CommandText += "  ) ";
		return command.ExecuteReader();

	}


	// 【增加資料】
	public SqliteDataReader InsertData(string table_name, string[] fieldNames, object[] values)
	{
		// 如果欄位的個數，和資料的個數不相等，無法執行插入的語句，所以返回一個null
		if (fieldNames.Length != values.Length)
		{
			return null;
		}

		command.CommandText = "insert into '" + table_name + "'(";

		for (int i = 0; i < fieldNames.Length; i++)
		{
			command.CommandText += fieldNames[i];
			if (i < fieldNames.Length - 1)
			{
				command.CommandText += ",";
			}
		}

		command.CommandText += ")" + "values (";

		for (int i = 0; i < values.Length; i++)
		{
			command.CommandText += values[i];

			if (i < values.Length - 1)
			{
				command.CommandText += ",";
			}
		}

		command.CommandText += ")";

		Debug.Log(command.CommandText);

		return command.ExecuteReader();

	}


	// 【刪除資料】
	public SqliteDataReader DeleteData(string table_name, string[] conditions)
	{
		command.CommandText = "delete from '" + table_name + "' where " + conditions[0];

		for (int i = 1; i < conditions.Length; i++)
		{
			// or：表示或者
			// and：表示並且
			command.CommandText += " or " + conditions[i];
		}

		return command.ExecuteReader();
	}

	// 【修改資料】

	public SqliteDataReader UpdateData(string table_name, string[] values, string[] conditions)
	{
		command.CommandText = "update '" + table_name + "' set " + values[0];

		for (int i = 1; i < values.Length; i++)
		{
			command.CommandText += "," + values[i];
		}

		command.CommandText += " where " + conditions[0];
		for (int i = 1; i < conditions.Length; i++)
		{
			command.CommandText += " or " + conditions[i];
		}

		return command.ExecuteReader();
	}

	// 【查詢資料】
	public SqliteDataReader SelectData(string table_name, string[] fields)
	{
		command.CommandText = "select " + fields[0];
		for (int i = 1; i < fields.Length; i++)
		{
			command.CommandText += "," + fields[i];
		}
		command.CommandText += " from '" + table_name + "'";

		return command.ExecuteReader();
	}

	public SqliteDataReader SelectData(string table_name, string[] fields, string[] conditions)
	{
		command.CommandText = "select " + fields[0];
		for (int i = 1; i < fields.Length; i++)
		{
			command.CommandText += "," + fields[i];
		}
		command.CommandText += " from '" + table_name + "'";

		command.CommandText += " where " + conditions[0];
		for (int i = 1; i < conditions.Length; i++)
		{
			command.CommandText += " or " + conditions[i];
		}

		return command.ExecuteReader();
	}


	// 【查詢整張表的資料】
	public SqliteDataReader SelectFullTableData(string table_name)
	{
		command.CommandText = "select * from '" + table_name + "'";
		return command.ExecuteReader();
	}


	// 【關閉資料庫】
	public void CloseDataBase()
	{
		connection.Close();
		command.Cancel();
	}

}
