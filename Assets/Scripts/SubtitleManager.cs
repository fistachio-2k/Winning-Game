using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    string[] mamaEstherDialogStrings = { "MAMA: Esther dear, come help me with breakfast. Did you wash your hands?\nESTHER: Sure, Mama!", "MAMA: Great job, my little bunny! Would you go and get me the gefilte fish recipe from the basement?\nESTHER: OK!", "MAMA: Be careful when you go down!" };
    string[] mamaMiraDialogStrings = { "MAMA: Want to come help us, Mira?\nMIRA: No, I don’t want your gross gefilte" };
    string[] afterRecipyDialog = { "MAMA: Thank you honey. And now we can put the onions on the frying pan and stir until are they become golden" };
    string[] answeringMachine = { "MIRA: Don't call me until you agree to give me the recipe. This has been going for far too long" };
    string[] myStrings;

    int curStringIdx = 0;

    bool displaying = false;

    public GUIStyle mamaStyle;

    public IEnumerator ShowMe(int stringIdx, string arrayName)
    {
        getCharacterArray(arrayName);
        
        curStringIdx = stringIdx;

        displaying = true;
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds((myStrings[curStringIdx].Length / 20) + 5);

        displaying = false;
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void getCharacterArray(string arrayName)
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

    public IEnumerator startMamaEstherDialog()
    {
        //StartCoroutine(ShowMe(0, "mama"));
        myStrings = mamaEstherDialogStrings;
        displaying = true;
        curStringIdx = 0;
        yield return new WaitForSeconds((mamaEstherDialogStrings[curStringIdx].Length / 20) + 2);

        displaying = false;
        displaying = true;
        curStringIdx = 1;
        yield return new WaitForSeconds((myStrings[curStringIdx].Length / 20) + 2);

        displaying = false;
        displaying = true;
        curStringIdx = 2;
        yield return new WaitForSeconds((myStrings[curStringIdx].Length / 20) + 2);

        displaying = false;
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
