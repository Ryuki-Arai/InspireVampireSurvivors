using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LevelTable : MonoBehaviour
{
    Dictionary<int,int> level_table = new Dictionary<int,int>();
    int _key;

    TextAsset _csv;

    /// <summary>
    /// レベルを取得して次のレベルアップに必要な経験値を返す
    /// </summary>
    public int Level
    {
        get => level_table[_key];
        set { _key = value; }
    }

    private void Start()
    {
        try
        {
            _csv = Resources.Load("LevelUpTable") as TextAsset;
            StreamReader _sr = new StreamReader($"Assets\\Resources\\{_csv.name}.csv", Encoding.GetEncoding("UTF-8"));
            string line;
            while ((line = _sr.ReadLine()) != null)
            {
                var data = line.Split(',');
                level_table.Add(int.Parse(data[0]), int.Parse(data[1]));
            }
            _sr.Close();
            foreach (var level in level_table)
            {
                Debug.Log(level.Key + ":" + level.Value);
            }
        }
        catch(IOException e)
        {
            Debug.LogError(e.Message);
        }
    }

}
