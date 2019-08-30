using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScriptableFramework;
using TMPro;

public class StatementGenerator : MonoBehaviour
{
	public Transform optionsRoot;
	public Transform usedOptionsRoot;
	public StatementUI statementUIPrefab;

	public AppEvent onContradiction;
	public AppEvent onDislike;
	public AppEvent onLike;
	public AppEvent onTimeTooLong;

	public Tag[] allTypeAs;
	public Tag[] allTypeBs;

	public string[] names;
	public TextMeshProUGUI nameLabel;
	public TextMeshProUGUI typeLabel;

	private Tag typeA;
	private Tag typeB;

	public Image timerImage;
	[Range (3, 10)] public float timerLength;

	public Image loveBar;
	public Animator loveHeart;
	public Transform timeLossPrefab;

	List<Statement> allStatements;
    List<Statement> usedStatements;

    Statement[] statementOptions;
	StatementUI[] statementButtons;

    // Start is called before the first frame update
    void Start()
    {
		typeA = allTypeAs[Random.Range (0, allTypeAs.Length)];
		typeB = allTypeBs[Random.Range (0, allTypeBs.Length)];

		usedStatements = new List<Statement> ();
        allStatements = new List<Statement> (Resources.LoadAll<Statement> ("/"));

		nameLabel.text = names[Random.Range (0, names.Length)];
		typeLabel.text = typeA.name + " " + typeB.name;
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

		UpdateScore (-1);

		Transform timeLossInstance = Instantiate (timeLossPrefab, usedOptionsRoot);
		timeLossInstance.SetSiblingIndex (0);

		onTimeTooLong.RaiseEvent ();

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
		if (allStatements.Count < 4)
		{
			StopAllCoroutines ();

			// lose

			return;
		}

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
		statementButtons = new StatementUI[4];

		for (int i = 0; i < statementOptions.Length; i++)
		{ 
			statementButtons[i] = Instantiate<StatementUI> (statementUIPrefab, optionsRoot);
			statementButtons[i].Initialise (statementOptions[i]);
		}
	}

	public void SelectStatement (int index)
    {
        allStatements.Remove (statementOptions[index]);

		AssessResponse (statementOptions[index], index);

		Transform selectedOption = optionsRoot.GetChild (index);

		selectedOption.SetParent (usedOptionsRoot);
		selectedOption.SetSiblingIndex (0);

        usedStatements.Add (statementOptions[index]);

		StopAllCoroutines ();
		ResetOptions ();
    }

	private void AssessResponse (Statement statement, int index)
	{
		if (DoesStatementContradict (statement))
		{
			UpdateScore (-1);

			statementButtons[index].Contradicted ();

			onContradiction.RaiseEvent ();
			return;
		}

		if (IsStatementDisliked (statement))
		{
			UpdateScore (-1);
			statementButtons[index].Disliked ();
			onDislike.RaiseEvent ();

			return;
		}

		if (IsStatementLiked (statement))
		{
			UpdateScore (1);
			statementButtons[index].Liked ();
			onLike.RaiseEvent ();

			return;
		}
	}

    private bool DoesStatementContradict (Statement statement)
    {
        for (int i = 0; i < usedStatements.Count; i++)
        {
            if (usedStatements[i].contradictiveStatements.Contains (statement)) return true;
        }

        return false;
    }

	private bool IsStatementDisliked (Statement statement)
	{
		if (statement.dislikedBy.Contains (typeA)) return true;
		if (statement.dislikedBy.Contains (typeB)) return true;

		return false;
	}

	private bool IsStatementLiked (Statement statement)
	{
		if (statement.likedBy.Contains (typeA)) return true;
		if (statement.likedBy.Contains (typeB)) return true;

		return false;
	}

	private void UpdateScore (int direction)
	{
		loveBar.fillAmount += 0.1f * direction;
		loveHeart.SetFloat ("Speed", loveHeart.GetFloat ("Speed") + (0.1f * direction));

		CheckWinLoseState ();
	}

	private void CheckWinLoseState ()
	{

		if (loveBar.fillAmount >= 1)
		{
			StopAllCoroutines ();
			// win
		}
		else if (loveBar.fillAmount <= 0)
		{
			StopAllCoroutines ();
			// lose
		}
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
