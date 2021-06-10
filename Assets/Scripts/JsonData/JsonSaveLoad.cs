﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class JsonSaveLoad : MonoBehaviour
{
    public InputField NickNameInputField;

    public void SaveToJson()
    {
        LoginData data = new LoginData();
        data.NickName = NickNameInputField.text;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/LoginData.Json", json);
    }


}
