using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    string[] mamaStrings = { "Esther dear, come help me with breakfast. Did you wash your hands?",  "Great job, honey! Can you please bring me the gefilte fish recipe from the basement?",  "Want to come help us, Mira?" };
    string[] estherStrings = { "Sure, Mom!", "Sure, Mom!"};
    string[] miraStrings = { "No, I don’t want your gross gefilte"};
    string[] myStrings;

    int curStringIdx = 0;

    bool displaying = false;

    public GUIStyle mamaStyle;

    public IEnumerator ShowMe(int stringIdx, string character)
    {
        getCharacterArrayt(character);
        
        curStringIdx = stringIdx;

        displaying = true;
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds((myStrings[curStringIdx].Length / 20) + 5);

        displaying = false;
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void getCharacterArrayt(string character)
    {
        if (character == "mama")
        {
            myStrings = mamaStrings;
        }
        else if (character == "esther")
        {
            myStrings = estherStrings;
        }
        else if (character == "mira")
        {
            myStrings = miraStrings;
        }
    }

    void OnGUI()
    {
        if (displaying)
        {
            if (curStringIdx % 2 == 0)
            {
                GUI.Label(new Rect(20, Screen.height - 80, Screen.width - 40, 60), myStrings[curStringIdx], mamaStyle);
            }
        }
    }
}
