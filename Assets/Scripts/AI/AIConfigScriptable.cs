using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

[CreateAssetMenu(fileName = "AIConfig" , menuName = "Scriptable/AIConfig")]
public class AIConfigScriptable : ScriptableObject
{
    [SerializeField] List<AIData> allTypes=new List<AIData>();
    public Dictionary<BotType,AIData> dict=new Dictionary<BotType, AIData>();

    //Make sure the name of the model matches the id
    [SerializeField] List<AIModel> ai_models;
    public Dictionary<int,NNModel> modelDict = new Dictionary<int, NNModel>();


    //Refresh the dictionaries everytime a value in any list is updated in edit mode
    #if UNITY_EDITOR
    void OnValidate()
    {
        SetDictionary();
    }
    #endif

    void Awake()
    {
        SetDictionary();
    }

    void SetDictionary()
    {
        dict = new Dictionary<BotType, AIData>();
        foreach(AIData data in allTypes)
        {
            dict.Add(data.type ,data);
        }
        foreach(AIModel data in ai_models)
        {
            modelDict.Add(data.boardSize ,data.nNModel);
        }
    }
}
[System.Serializable]
public class AIData
{ 
    public BotType type;
    public float delay;
}
[System.Serializable]
public class AIModel
{
    public int boardSize;
    public NNModel nNModel;
}