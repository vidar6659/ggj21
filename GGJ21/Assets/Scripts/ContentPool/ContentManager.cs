using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public Profile[] profiles;

    private List<LostObject> poolObjects;
    public List<Profile> sessionProfiles;

    private int numberOfProfiles = 4;
    private List<int> profilesIndex = new List<int>{ 0, 1, 2, 3, 4, 5 };

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GenerateProfiles()
    {
        int index = 0;
        LostObject lObj;
        while (index < numberOfProfiles)
        {
            int i = Random.Range(0, profilesIndex.Count);
            Profile p = profiles[profilesIndex[i]];
            profilesIndex.RemoveAt(i);
            p.lostObjs.RemoveAt(Random.Range(0, p.lostObjs.Count));
            p.lostObjs.RemoveAt(Random.Range(0, p.lostObjs.Count));
            GenerateObjectPool(p.id);
            for(int j = 0; j < 7; j++)
            {
                int k = Random.Range(0, poolObjects.Count);
                lObj = poolObjects[k];
                lObj.isOwned = false;
                p.lostObjs.Add(lObj);
                poolObjects.RemoveAt(k);
            }
            ShuffleLostObjects(p.lostObjs);
            sessionProfiles.Add(p);
        }
    }

    private void GenerateObjectPool(int _id)
    {
        foreach(Profile p in profiles)
        {
            if(p.id != _id)
            {
                poolObjects.AddRange(p.lostObjs);
            }
        }
    }

    private void ShuffleLostObjects(List<LostObject> _objList)
    {
        for (int i = 0; i < _objList.Count; i++)
        {
            LostObject temp = _objList[i];
            int randomIndex = Random.Range(i, _objList.Count);
            _objList[i] = _objList[randomIndex];
            _objList[randomIndex] = temp;
        }
    }
}

[System.Serializable]
public struct Profile
{
    public Sprite image;
    public string desc;
    public List<LostObject> lostObjs;
    public int id;
}

[System.Serializable]
public struct LostObject
{
    public Sprite image;
    public string desc;
    public bool isOwned;
}