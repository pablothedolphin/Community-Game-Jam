using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatementGenerator : MonoBehaviour
{
	public Transform optionsRoot;
	public Transform usedOptionsRoot;
	public StatementUI statementUIPrefab;

	public Image timerImage;
	[Range (3, 10)] public float timerLength;

    List<Statement> allStatements;
    List<Statement> usedStatements;

    Statement[] statementOptions;

    // Start is called before the first frame update
    void Start()
    {
        usedStatements = new List<Statement> ();
        allStatements = new List<Statement> (Resources.LoadAll<Statement> ("/"));

		ResetOptions ();
	}

	private IEnumerator CountDownTime ()
	{
		float timeElapsed = 0;

		while (timeElapsed < timerLength)
		{
			timeElapsed += Time.deltaTime;
			timerImage.fillAmount = 1 - (timeElapsed / timerLength);

			yield return null;
		}

		// reduce score

		ResetOptions ();
	}

	public void ResetOptions ()
	{
		StartCoroutine (CountDownTime ());
		optionsRoot.DestroyChildren ();
		GenerateSetOfFour ();
	}

	public void GenerateSetOfFour ()
    {
		if (allStatements.Count < 4) return;

        List<int> randomIndecies = new List<int> ();

        while (randomIndecies.Count < 4)
        {
            int index = Random.Range (0, allStatements.Count);

            if (!randomIndecies.Contains (index))
                randomIndecies.Add (index);
        }

        statementOptions = new Statement[4];

        statementOptions[0] = allStatements[randomIndecies[0]];
        statementOptions[1] = allStatements[randomIndecies[1]];
        statementOptions[2] = allStatements[randomIndecies[2]];
        statementOptions[3] = allStatements[randomIndecies[3]];

		PresentOptions ();
    }

	private void PresentOptions ()
	{
		for (int i = 0; i < statementOptions.Length; i++)
		{
			Instantiate<StatementUI> (statementUIPrefab, optionsRoot).Initialise (statementOptions[i]);
		}
	}

	public void SelectStatement (int index)
    {
        allStatements.Remove (statementOptions[index]);

        if (DoesStatementContradict (statementOptions[index])) Debug.Log ("oops");
        else Debug.Log ("phew!");

		Transform selectedOption = optionsRoot.GetChild (index);

		selectedOption.SetParent (usedOptionsRoot);
		selectedOption.SetSiblingIndex (0);

        usedStatements.Add (statementOptions[index]);

		StopAllCoroutines ();
		ResetOptions ();
    }

    private bool DoesStatementContradict (Statement statement)
    {
        for (int i = 0; i < usedStatements.Count; i++)
        {
            for (int j = 0; j < usedStatements[i].contradictiveStatements.Count; j++)
            {
                if (usedStatements[i].contradictiveStatements.Contains (statement)) return true;
            }
        }

        return false;
    }
}

public static class Extensions
{
	public static void DestroyChildren (this Transform transform)
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy (transform.GetChild (i).gameObject);
		}
	}

	public static void DestroyChildren (this GameObject gameObject)
	{
		for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy (gameObject.transform.GetChild (i).gameObject);
		}
	}
}
