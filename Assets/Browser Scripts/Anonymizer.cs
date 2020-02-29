using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Anonymizer
{
    private static Dictionary<string, string> users = new Dictionary<string, string>();
    private static Dictionary<int, bool> unique = new Dictionary<int, bool>();

    private static int idLength = 7; //Keep small....
    private static int lowerBound = (int) Mathf.Pow(10, 7);
    private static int upperBound = (int) Mathf.Pow(10, 8) - 1;



    private static int GenerateUnique()
    {
        int newID;
        do
        {
            newID = Random.Range(lowerBound, upperBound);
        } while (unique.ContainsKey(newID));
        unique.Add(newID, true);
        return newID;
    }

    public static string FetchID(string name)
    {
        if (!users.ContainsKey(name))
        {
            int newID = GenerateUnique();
            users.Add(name, newID.ToString());
        }

        return users[name];
    }
}
