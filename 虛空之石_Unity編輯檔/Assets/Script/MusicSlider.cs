using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class MusicSlider : MonoBehaviour
{
    public Slider backgroundMusic;
    public Slider soundEffect;

    public AudioMixer audioMixer;
    public AudioMixer audioBackgound;
    void Awake()
    {
        //string path = "data source =" + Application.streamingAssetsPath + "/" + "MusicSliderValueDB.db";

        //SQLite sql = new SQLite(path);
        CreateTable();
        
        //sql.CloseDataBase();
        //Load();
    }

    void Start()
    {
        //string path = "data source =" + Application.streamingAssetsPath + "/" + "MusicSliderValueDB.db";

        //SQLite sql = new SQLite(path);
        
        Load();
        //sql.CloseDataBase();
        //Load();
    }

    public void CreateTable()
    {

        string path = "data source =" + Application.streamingAssetsPath + "/" + "MusicSliderValueDB.db";

        SQLite sql = new SQLite(path);
        //音樂設定
        try
        {
            SqliteDataReader reader = sql.CreateTable("Music Setting", new string[] { "Name ", "Value" }, new string[] { "string", "float" });
            Debug.Log("create table 'Music Setting'");
            reader.Close();

            reader = sql.InsertData("Music Setting", new string[] { "Name ", "Value" }, new object[] {"'BackgroundMusic'" , backgroundMusic.value });
            reader.Close();

            reader = sql.InsertData("Music Setting", new string[] { "Name ", "Value" }, new object[] { "'SoundEffect'", soundEffect.value });
            reader.Close();

        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }
        sql.CloseDataBase();
    }

    public void Save() //背包存檔
    {
        string path = "data source =" + Application.streamingAssetsPath + "/" + "MusicSliderValueDB.db";

        SQLite sql = new SQLite(path);        
        //玩家位置存檔
        try
        {
            
            SqliteDataReader reader = sql.UpdateData("Music Setting", new string[] { "Value = " + backgroundMusic.value }, new string[] { "Name = 'BackgroundMusic'" });
            reader.Close();

            reader = sql.UpdateData("Music Setting", new string[] { "Value = " + soundEffect.value }, new string[] { "Name = 'SoundEffect'" });
            reader.Close();
            //Debug.Log("SAVE OK");
        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }

        sql.CloseDataBase();
    }

    [RuntimeInitializeOnLoadMethodAttribute]
    public void Load() //背包讀檔
    {
        string path = "data source =" + Application.streamingAssetsPath + "/" + "MusicSliderValueDB.db";

        SQLite sql = new SQLite(path);
        
        
        try
        {
            SqliteDataReader reader = sql.SelectData("Music Setting", new string[] { "Value" }, new string[] { "name = 'BackgroundMusic'" });
            backgroundMusic.value = float.Parse(reader[0].ToString());
            SetBackgoundVolume(backgroundMusic.value);
            reader.Close();
            /*float a, temp1, temp2;
            a = reader[0].ToString();
            temp = float.Parse(a);
            reader.Close();*/
            

            reader = sql.SelectData("Music Setting", new string[] { "Value" }, new string[] { "name = 'SoundEffect'" });
            soundEffect.value = float.Parse(reader[0].ToString());
            SetMasterVolume(soundEffect.value);
            reader.Close();
            /*a = reader[0].ToString();
            temp2 = float.Parse(a);
            reader.Close();*/

            
            
       

            //Debug.Log("LOARD OK");
        }
        catch (SqliteException e)
        {
            Debug.Log(e.ToString());
        }

        sql.CloseDataBase();
    }

    public void SetMasterVolume(float volume)  //控制音量參數
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetBackgoundVolume(float volume)  //控制音量參數
    {
        audioBackgound.SetFloat("BackgroundVolume", volume);
    }

    void OnApplicationQuit()
    {
        Save();
        //return;
    }
}
