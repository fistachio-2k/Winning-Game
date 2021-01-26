using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    string[] mamaEstherDialogStrings = { "Esther dear, come help me with breakfast. Did you wash your hands?\nSure, Mama!\nGreat job, honey! Can you please bring me the gefilte fish recipe from the basement?\nSure, Mama!"};
    string[] mamaMiraDialogStrings = { "Want to come help us, Mira?\nNo, I don’t want your gross gefilte"};
    string[] afterRecipyDialog = { "Thank you honey. And now we can put the onions on the frying pan and stir until are they become golden" };
    string[] answeringMachine = { "Don't call me until you agree to give me the recipe. This has been going for far too long" };
    string[] myStrings;

    int curStringIdx = 0;

    bool displaying = false;

    public GUIStyle mamaStyle;

    public IEnumerator ShowMe(int stringIdx, string arrayName)
    {
        getCharacterArrayt(arrayName);
        
        curStringIdx = stringIdx;

        displaying = true;
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds((myStrings[curStringIdx].Length / 20) + 5);

        displaying = false;
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void getCharacterArrayt(string arrayName)
    {
        if (arrayName == "mama")
        {
            myStrings = mamaEstherDialogStrings;
        }
        else if (arrayName == "mira")
        {
            myStrings = mamaMiraDialogStrings;
        }
        else if(arrayName == "after")
        {
            myStrings = afterRecipyDialog;
        }
        else if(arrayName == "answeringMachine")
        {
            myStrings = answeringMachine;
        }
    }

    public void startMamaEstherDialog()
    {
        StartCoroutine(ShowMe(0, "mama"));
    }

    public void startMamaMiraDialog()
    {
        StartCoroutine(ShowMe(0, "mira"));
    }

    public void startAfterRecipyDialog()
    {
        StartCoroutine(ShowMe(0, "after"));
    }

    public void startAnsweringMachine()
    {
        StartCoroutine(ShowMe(0, "answeringMachine"));
    }

    void OnGUI()
    {
        if (displaying)
        {
            GUI.Label(new Rect(20, Screen.height - 80, Screen.width - 40, 60), myStrings[curStringIdx], mamaStyle);
        }
    }
}
