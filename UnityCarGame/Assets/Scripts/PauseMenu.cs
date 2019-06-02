using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    public GameObject Car1;
    public GameObject Car2;

    public void Resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

   public void SaveGame()
    {
        // Create database
        string connection = "URI=file:" + "./My_Database";

        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Create table
        /*IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS race_data (id, INTEGER PRIMARY KEY, xp1 FLOAT, yp1 FLOAT, zp1 FLOAT, xp2 FLOAT, yp2 FLOAT, zp2 FLOAT, xr1 FLOAT, yr1 FLOAT, zr1 FLOAT, xr2 FLOAT, yr2 FLOAT, zr2 FLOAT)";

        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();*/

        // Insert values in table
        IDbCommand cmnd = dbcon.CreateCommand();
        //cmnd.CommandText = "INSERT INTO race_data (xp1, yp1, zp1, xp2, yp2, zp2, xr1, yr1, zr1, xr2, yr2, zr2) VALUES (" +  + ", " +  + ", " + Car1.transform.position.z + ", " + Car2.transform.position.x + ", " + Car2.transform.position.y + ", " + Car2.transform.position.z + ", " + Car1.transform.rotation.x + ", " + Car1.transform.rotation.y + ", " + Car1.transform.rotation.z + ", " + Car2.transform.rotation.x + ", " + Car2.transform.rotation.y + ", " + Car2.transform.rotation.z + ")";
        int b = SceneManager.GetActiveScene().buildIndex;
        if(b == 2) cmnd.CommandText = string.Format ("UPDATE race_data SET xp1 = @0, yp1 = @1, zp1 = @2, xp2 = @3, yp2 = @4, zp2 = @5, xr1 = @6, yr1 = @7, zr1 = @8, xr2 = @9, yr2 = @10, zr2 = @11, mn = @12, sc = @13, ml = @14, rt = @15, lap = @16 WHERE id=1");
        else cmnd.CommandText = string.Format("UPDATE race_data SET xp1 = @0, yp1 = @1, zp1 = @2, xp2 = @3, yp2 = @4, zp2 = @5, xr1 = @6, yr1 = @7, zr1 = @8, xr2 = @9, yr2 = @10, zr2 = @11, mn = @12, sc = @13, ml = @14, rt = @15, lap = @16 WHERE id=2");

        cmnd.Parameters.Add(new SqliteParameter("@0", Car1.transform.position.x));
        cmnd.Parameters.Add(new SqliteParameter("@1", Car1.transform.position.y));
        cmnd.Parameters.Add(new SqliteParameter("@2", Car1.transform.position.z));
        cmnd.Parameters.Add(new SqliteParameter("@3", Car2.transform.position.x));
        cmnd.Parameters.Add(new SqliteParameter("@4", Car2.transform.position.y));
        cmnd.Parameters.Add(new SqliteParameter("@5", Car1.transform.position.z));
        cmnd.Parameters.Add(new SqliteParameter("@6", Car1.transform.rotation.x));
        cmnd.Parameters.Add(new SqliteParameter("@7", Car1.transform.rotation.y));
        cmnd.Parameters.Add(new SqliteParameter("@8", Car1.transform.rotation.z));
        cmnd.Parameters.Add(new SqliteParameter("@9", Car2.transform.rotation.x));
        cmnd.Parameters.Add(new SqliteParameter("@10", Car2.transform.rotation.y));
        cmnd.Parameters.Add(new SqliteParameter("@11", Car2.transform.rotation.z));
        cmnd.Parameters.Add(new SqliteParameter("@12", LapTimeManager.MinuteCount));
        cmnd.Parameters.Add(new SqliteParameter("@13", LapTimeManager.SecondCount));
        cmnd.Parameters.Add(new SqliteParameter("@14", LapTimeManager.MilliCount));
        cmnd.Parameters.Add(new SqliteParameter("@15", LapTimeManager.RawTime));
        cmnd.Parameters.Add(new SqliteParameter("@16", LapComplete.LapsDone));
        cmnd.ExecuteNonQuery();

        // Read and print all values in table
       /* IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
        }*/

        // Close connection
        dbcon.Close();
    }

    public void LoadGame()
    {
        // Create database
        string connection = "URI=file:" + "./My_Database";

        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        int b = SceneManager.GetActiveScene().buildIndex;
        string query;
        if (b == 2) query = "SELECT * FROM race_data WHERE id=1";
        else query = "SELECT * FROM race_data WHERE id=2";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        if (reader.Read())
        {
            Car1.transform.position = new Vector3(float.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()), float.Parse(reader[2].ToString()));
            Car2.transform.position = new Vector3(float.Parse(reader[3].ToString()), float.Parse(reader[4].ToString()), float.Parse(reader[5].ToString()));
            Car1.transform.eulerAngles = new Vector3(float.Parse(reader[6].ToString()), float.Parse(reader[7].ToString()) + 180, float.Parse(reader[8].ToString()));
            Car2.transform.eulerAngles = new Vector3(float.Parse(reader[9].ToString()), float.Parse(reader[10].ToString()) + 180, float.Parse(reader[11].ToString()));
            LapTimeManager.MinuteCount = int.Parse(reader[12].ToString());
            LapTimeManager.SecondCount = int.Parse(reader[13].ToString());
            LapTimeManager.MilliCount = float.Parse(reader[14].ToString());
            LapTimeManager.RawTime = int.Parse(reader[15].ToString());
            LapComplete.LapsDone = int.Parse(reader[16].ToString());
        }

        // Close connection
        dbcon.Close();
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MilliCount = 0;
        LapTimeManager.RawTime = 0;
    }
}
