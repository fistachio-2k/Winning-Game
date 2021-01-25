using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    string[] mamaEstherDialogStrings = { "Esther dear, come help me with breakfast. Did you wash your hands?\nSure, Mama!\nGreat job, honey! Can you please bring me the gefilte fish recipe from the basement?\nSure, Mama!"};
    string[] mamaMiraDialogStrings = { "Want to come help us, Mira?\nNo, I don’t want your gross gefilte"};
    string[] AfterRecipyDialog = { "Thank you honey. And now we can put the onions to the frying pan and stir until it’s getting golden" };
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
            myStrings = mamaEstherDialogStrings;
        }
        else if (character == "mira")
        {
            myStrings = mamaMiraDialogStrings;
        }
        else if(character == "after")
        {
            myStrings = AfterRecipyDialog;
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

    void OnGUI()
    {
        if (displaying)
        {
            GUI.Label(new Rect(20, Screen.height - 80, Screen.width - 40, 60), myStrings[curStringIdx], mamaStyle);
        }
    }
}
