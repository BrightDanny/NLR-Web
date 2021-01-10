using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectJSON : MonoBehaviour
{
    public JsonLoader jsonList = new JsonLoader();
    public void LoadData(string a, string b)
    {
        //set values from button
        string model = a;
        string tail = b;
        TextAsset jsonText = Resources.Load<TextAsset>("Database/" + model + "/" + tail + "/JSON/" + tail + "_Data");
        jsonList = JsonUtility.FromJson<JsonLoader>(jsonText.ToString());
    }
}

[System.Serializable]
public class JsonLoader
{
    public List<double> caution_limit;
    public List<string> ci_identifier;
    public List<string> ci_name;
    public List<double> color;
    public List<double> exceed_limit;
    public List<double> goal_limit;
    public List<double> latest_value;
}
[System.Serializable]
public class JsonData
{
    public List<JsonLoader> jsonPush = new List<JsonLoader>();
}
