using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// ���x���ƌo���l���f�[�^�t�@�C������ǂݍ���ŊǗ�����N���X
/// </summary>
public class LevelTable
{
    static Dictionary<int,int> level_table = new Dictionary<int,int>();

    /// <summary>
    /// ���݂̃��x�����擾���Ď��̃��x���A�b�v�ɕK�v�Ȍo���l��Ԃ�
    /// </summary>
    /// <param name="_iskey">���݂̃��x��</param>
    /// <returns>���̃��x���A�b�v�ɕK�v�Ȍo���l�iMaxValue�j</returns>
    public static int NextLevelEXP(int _iskey)
    {
        int next_exp;
        try
        {
            next_exp = level_table[_iskey];
        }
        catch(KeyNotFoundException e)
        {
            next_exp = level_table[level_table.Count];
        }
        return next_exp;
    }

    /// <summary>
    /// Resources���̃t�@�C����ǂݍ���Value��Key����int�^�̃f�B�N�V���i���Ɋi�[
    /// </summary>
    /// <param name="_filepath">�f�[�^���i�[���ꂽ�t�@�C����</param>
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
            Debug.LogError($"�t�@�C����ǂݍ��߂܂���:{e.Message}");
        }
        catch(NullReferenceException e)
        {
            Debug.LogError($"�t�@�C����������܂���:{e.Message}");
        }
    }

}
