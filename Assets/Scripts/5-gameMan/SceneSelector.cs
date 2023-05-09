using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public InputField inRandomFillPercent;
    public InputField inNumOfTilesToSee;
    public InputField inNumOfTries;


    public void ScreenSelector(int page)
    {
        SceneManager.LoadScene(page);
    }

    public void SetParam()
    {
        // Get the input from the user.
        float randomFillPercent = float.TryParse(inRandomFillPercent.text, out float valueA) ? valueA : 0.432f;
        int numOfTilesToSee = int.TryParse(inNumOfTilesToSee.text, out int valueB) ? valueB : 100;
        int numOfTries = int.TryParse(inNumOfTries.text, out int valueC) ? valueC : 100;

        // Check the input to make sure that it is valid.
        if (randomFillPercent < 0 || randomFillPercent > 1)
            Debug.LogError("The inRandomFillPercent value must be between 0 and 1.");

        if (numOfTilesToSee < 0 || numOfTilesToSee > 1000)
            Debug.LogError("The inNumOfTilesToSee value must be between 0 and 1000.");

        if (numOfTries < 0 || numOfTries > 1000)
            Debug.LogError("The inNumOfTries value must be between 0 and 1000.");

        // Assign the value of the input to the corresponding field.
        inRandomFillPercent.text = randomFillPercent.ToString();
        inNumOfTilesToSee.text = numOfTilesToSee.ToString();
        inNumOfTries.text = numOfTries.ToString();
    }
}
