using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;
using System.Data;

/*string型別轉換
Boolean.Parse(), Byte.Parse(), Char.Parse(), DateTime.Parse(),
Decimal.Parse(), Double.Parse(), Int32.Parse(), SByte.Parse(),
Single.Parse(), TimeSpan.Parse()*/

public class Test : MonoBehaviour
{
    public Bag playerBag;
    string a;
    int b;
    public GameObject objPlayer;
    

    // Use this for initialization
    void Start()
    {
        
        // 資料庫檔案的具體路徑，有的是.sqlite/.db
        string path = "data source =" + Application.streamingAssetsPath + "/" + "testDB.db";

        SQLite sql = new SQLite(path);


        //sql.CreateTable("BBB", new string[] { "ItemName", "ItemHeld" }, new string[] { "STRING", "DOUBLE"});       //創建資料表
        
        //SqliteDataReader reader1 = sql.InsertData("FirstTable", new string[] { "name", "score" }, new object[] { "'Aa'", 99 }); //資料庫的字串資料必須使用單引號框起來
        // 讀取到的資訊使用之後需要關閉
        //SqliteDataReader reader1 = sql.SelectData("FirstTable", new string[] { "score" }, new string[] { "name = 'Cc'"});

        //SqliteDataReader reader1 = sql.SelectData("FirstTable", new string[] { "score" }, new string[] { "name = 'Aa' " });

        //a = reader1[0].ToString();
        //b = Int32.Parse(a);
        //Debug.Log(b);
        // Debug.Log(reader1);

        //reader1.Close();

        //SqliteDataReader reader2 = sql.DeleteData("FirstTable", new string[] { "name = 'Bb'" });
        //reader2.Close();

        //SqliteDataReader reader3 = sql.UpdateData("FirstTable", new string[] { "name = 'Cc'"}, new string[] { "name = 'Aa' " }); 
        //reader3.Close();



        sql.CloseDataBase();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void OnGUI()
     {
         string path = "data source =" + Application.streamingAssetsPath + "/" + "testDB.db";

         SQLite sql = new SQLite(path);

         if (GUILayout.Button("create table"))
         {


             try
             {
                 SqliteDataReader reader = sql.CreateTable("bag", new string[] { "name ", "number" }, new string[] { "string PRIMARY KEY", "int" });  //創建背包table
                 Debug.Log("create table ok");
                 reader.Close();

                 for (int i = 0; i < playerBag.itemList.Count; i++) //插入背包欄位
                 {  
                     if (playerBag.itemList[i] != null) 
                     {
                         try
                         {
                             reader = sql.InsertData("bag", new string[] { "name", "number" }, new object[] { "'" + playerBag.itemList[i].itemName + "'", playerBag.itemList[i].itemHeld });
                             reader.Close();

                         }
                         catch (SqliteException e)
                         {
                             Debug.Log(e.ToString());
                         }
                     }
                 }
             }
             catch(SqliteException e)
             {
                 Debug.Log(e.ToString());
             }

             //玩家位置
            try
            {
                SqliteDataReader reader = sql.CreateTable("transform", new string[] { "x","y","z" }, new string[] { "string", "string", "string" });
                Debug.Log("create table 'player transform'");
                reader.Close();


                Vector3 playerPos = objPlayer.transform.position;
                reader = sql.InsertData("transform", new string[] { "x", "y", "z" }, new object[] { "'"+ playerPos.x + "'", "'"+ playerPos.y + "'", "'" + playerPos.z + "'" });
                reader.Close();

            }
            catch (SqliteException e)
            {
                Debug.Log(e.ToString());
            }

         sql.CloseDataBase();
         }


         if (GUILayout.Button("SAVE"))
         {
             //SqliteDataReader reader = sql.UpdateData("bag", new string[] { "name = 'Cc'"}, new string[] { "name = 'Aa' " }); 
             //reader.Close();
             for (int i = 0; i < playerBag.itemList.Count; i++)
             {
                 if (playerBag.itemList[i] != null)
                 {
                     try
                     {
                         SqliteDataReader reader = sql.UpdateData("bag", new string[] { "number = " + playerBag.itemList[i].itemHeld }, new string[] { "name = '" + playerBag.itemList[i].itemName + "'" });
                         reader.Close();
                         Debug.Log("SAVE OK");
                         BagManager.RefreshItem();
                     }
                     catch (SqliteException e)
                     {
                         Debug.Log(e.ToString());
                     }
                 }
             }

         }

         if (GUILayout.Button("LOARD"))
         {           
             for (int i = 0; i < playerBag.itemList.Count; i++)
             {
                 if (playerBag.itemList[i] != null)
                 {
                     try
                     {
                         SqliteDataReader reader = sql.SelectData("bag", new string[] { "number" }, new string[] { "name = '" + playerBag.itemList[i].itemName + "'" });
                         a = reader[0].ToString();
                         b = Int32.Parse(a);
                         playerBag.itemList[i].itemHeld = b;
                         BagManager.RefreshItem();
                         reader.Close();
                         Debug.Log("LOARD OK");
                     }
                     catch (SqliteException e)
                     {
                         Debug.Log(e.ToString());
                     }
                 }
             }
         }
     }*/

    public void CreateTable()
    {
        string path = "data source =" + Application.streamingAssetsPath + "/" + "testDB.db";

        SQLite sql = new SQLite(path);
        try
        {
            SqliteDataReader reader = sql.CreateTable("bag", new string[] { "name ", "number" }, new string[] { "string PRIMARY KEY", "int" });  //創建背包table
            Debug.Log("create table 'bag'");
            reader.Close();

            for (int i = 0; i < playerBag.itemList.Count; i++) //插入背包欄位
            {
                if (playerBag.itemList[i] != null)
                {
                    try
                    {
                        reader = sql.InsertData("bag", new string[] { "name", "number" }, new object[] { "'" + playerBag.itemList[i].itemName + "'", playerBag.itemList[i].itemHeld });
                        reader.Close();

                    }
                    catch (SqliteException e)
                    {
                        Debug.Log(e.ToString());
                    }
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }


        //玩家狀態資料庫
        try
        {
            SqliteDataReader reader = sql.CreateTable("player status", new string[] { "MaxHP ", "HP", "MaxSP", "SP", "MaxDF", "DF", "melee1Cost", "melee2Cost", "dashCost", "regSpeed" }, 
                                                                        new string[] { "float", "float", "float", "float", "float", "float", "float", "float", "float", "float" });  
            Debug.Log("create table 'player status'");
            reader.Close();

            reader = sql.InsertData("player status", new string[] { "MaxHP ", "HP", "MaxSP", "SP", "MaxDF", "DF", "melee1Cost", "melee2Cost", "dashCost", "regSpeed" }, 
                                                    new object[] { Game.sav.maxHp, Game.sav.hp, Game.sav.maxSp, Game.sav.sp, Game.sav.maxDf, Game.sav.df, Game.sav.melee1Cost, Game.sav.melee2Cost, Game.sav.dashCost, Game.sav.regSpeed, });
            reader.Close();

        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }


        //玩家位置
        try
        {
            SqliteDataReader reader = sql.CreateTable("position", new string[] {"name", "x","y","z" }, new string[] { "string", "float", "float", "float" });
            Debug.Log("create table 'position'");
            reader.Close();


            Vector3 playerPos = objPlayer.transform.position;
            reader = sql.InsertData("position", new string[] { "name", "x", "y", "z" }, new object[] {"'Player'" ,playerPos.x , playerPos.y ,playerPos.z  });
            reader.Close();

        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }

        sql.CloseDataBase();

        //Vector3 playerPos = objPlayer.transform.position;
        //Debug.Log(playerPos);
    }

    public void Save() //背包存檔
    {
        string path = "data source =" + Application.streamingAssetsPath + "/" + "testDB.db";

        SQLite sql = new SQLite(path);
        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            if (playerBag.itemList[i] != null)
            {
                try
                {
                    SqliteDataReader reader = sql.UpdateData("bag", new string[] { "number = " + playerBag.itemList[i].itemHeld }, new string[] { "name = '" + playerBag.itemList[i].itemName + "'" });
                    reader.Close();
                    Debug.Log("SAVE bag OK");
                    BagManager.RefreshItem();
                }
                catch (SqliteException e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
        //玩家位置存檔
        try
        {
            Vector3 playerPos = objPlayer.transform.position;
            SqliteDataReader reader = sql.UpdateData("position", new string[] { "x = " + playerPos.x }, new string[] { "name = 'Player'" });
            reader.Close();

            reader = sql.UpdateData("position", new string[] { "y = " + playerPos.y }, new string[] { "name = 'Player'" });
            reader.Close();

            reader = sql.UpdateData("position", new string[] { "z = " + playerPos.z }, new string[] { "name = 'Player'" });
            reader.Close();
            Debug.Log("SAVE position OK");
        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }

        sql.CloseDataBase();
    }

    public void Load() //背包讀檔
    {
        string path = "data source =" + Application.streamingAssetsPath + "/" + "testDB.db";

        SQLite sql = new SQLite(path);
        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            if (playerBag.itemList[i] != null)
            {
                try
                {
                    SqliteDataReader reader = sql.SelectData("bag", new string[] { "number" }, new string[] { "name = '" + playerBag.itemList[i].itemName + "'" });
                    a = reader[0].ToString();
                    b = Int32.Parse(a);
                    playerBag.itemList[i].itemHeld = b;
                    BagManager.RefreshItem();
                    reader.Close();
                    Debug.Log("LOARD OK");
                }
                catch (SqliteException e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
        //玩家位置讀檔
        try
        {
            SqliteDataReader reader = sql.SelectData("position", new string[] { "x" }, new string[] { "name = 'Player'" });
            float temp1, temp2, temp3;
            a = reader[0].ToString();
            temp1 = float.Parse(a);
            reader.Close();

            reader = sql.SelectData("position", new string[] { "y" }, new string[] { "name = 'Player'" });
            a = reader[0].ToString();
            temp2 = float.Parse(a);
            reader.Close();

            reader = sql.SelectData("position", new string[] { "z" }, new string[] { "name = 'Player'" });
            a = reader[0].ToString();
            temp3 = float.Parse(a);
            reader.Close();

            objPlayer.transform.position = new Vector3(temp1, temp2, temp3);

            Debug.Log("LOARD position OK");
        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }

        sql.CloseDataBase();
    }

    public void SavePlayerStatus()
    {
        string path = "data source =" + Application.streamingAssetsPath + "/" + "testDB.db";

        SQLite sql = new SQLite(path);

        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            if (playerBag.itemList[i] != null)
            {
                try
                {
                    SqliteDataReader reader = sql.UpdateData("player", new string[] { "number = " + playerBag.itemList[i].itemHeld }, new string[] { "name = '" + playerBag.itemList[i].itemName + "'" });
                    reader.Close();
                    Debug.Log("SAVE OK");
                    BagManager.RefreshItem();
                }
                catch (SqliteException e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }

        sql.CloseDataBase();
    }

}
