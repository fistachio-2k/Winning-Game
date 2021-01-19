using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    string[] myStrings = { "I think the horseradish is in the fridge!", "Taking that yummy horseradish" , "Closing the fridge, fun fun fun!"};
 
    int curStringIdx = 0;

    bool displaying = false;

    GUIStyle myStyle;

    private void Start()
    {
        //StartCoroutine(ShowMe());
    }

    public IEnumerator ShowMe(int stringIdx)
    {
        curStringIdx = stringIdx;

        displaying = true;
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds((myStrings[curStringIdx].Length / 20) + 5);

        displaying = false;
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    void OnGUI()
    {
        if (displaying)
        {
            GUI.Label(new Rect(20, Screen.height - 80, Screen.width - 40, 60), myStrings[curStringIdx]);
        }
        GUI.Button(new Rect(10, 10, 100, 25), "Pause");
    }
}
