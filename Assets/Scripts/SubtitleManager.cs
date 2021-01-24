using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    string[] myStrings = { "Esther dear, come help me with breakfast. Did you wash your hands?", "Sure, Mom!", "Great job, honey! Can you please bring me the gefilte fish recipe from the basement?", "Sure, Mom!", "Want to come help us, Mira?", "No, I don’t want your gross gefilte", "Thank you honey. And now we can put the onions to the frying pan and stir until it’s getting golden" };


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
        //GUI.Button(new Rect(10, 10, 100, 25), "Pause");
    }
}
