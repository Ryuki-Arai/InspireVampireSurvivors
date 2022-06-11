using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

/// <summary>
/// レベルアップテーブルをCSVから読み込んで管理するクラス
/// </summary>
public class LevelTable
{
    static Dictionary<int,int> level_table = new Dictionary<int,int>();

    /// <summary>
    /// レベルを取得して次のレベルアップに必要な経験値を返す
    /// </summary>
    /// <param name="_iskey">現在のレベル</param>
    /// <returns></returns>
    public static int NextLevelEXP(int _iskey)
    {
        return level_table[_iskey];
    }

    /// <summary>
    /// Resources下のCSVファイルを読み込みValueとKey共にint型のディクショナリに格納
    /// </summary>
    /// <param name="_filepath">データが格納されたファイル名</param>
    public static void LoadFile(string _filepath)
    {
        try
        {
            var _file = Resources.Load<TextAsset>(_filepath);
            StringReader _sr = new StringReader(_file.text);
            string line;
            while ((line = _sr.ReadLine()) != null)
            {
                var data = line.Split(',');
                level_table.Add(int.Parse(data[0]), int.Parse(data[1]));
            }
            _sr.Close();
        }
        catch(IOException e)
        {
            Debug.LogError($"ファイルを読み込めません:{e.Message}");
        }
        catch(NullReferenceException e)
        {
            Debug.LogError($"ファイルが見つかりません:{e.Message}");
        }
    }

}
