using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Extensions
{
	[MenuItem ("Game/Balance Contradictions")]
	public static void BalanceContradictions ()
	{
		int additions = 0;
		Statement[] allStatements = Resources.LoadAll<Statement> ("/");

		for (int i = 0; i < allStatements.Length; i++)
		{
			for (int j = 0; j < allStatements.Length; j++)
			{
				if (i == j) continue;

				if (allStatements[i].contradictiveStatements.Contains (allStatements[j]))
				{
					if (!allStatements[j].contradictiveStatements.Contains (allStatements[i]))
					{
						EditorUtility.SetDirty (allStatements[j]);
						allStatements[j].contradictiveStatements.Add (allStatements[i]);
						additions++;
					}
				}
			}
		}

		Debug.Log ("additions made: " + additions.ToString ());
	}

	[MenuItem ("Game/Get Tag Stats")]
	public static void GetTagStats ()
	{
		Tag[] allTags = Resources.LoadAll<Tag> ("/");
		int[] likeOccurances = new int[allTags.Length];
		int[] dislikeOccurances = new int[allTags.Length];
		Statement[] allStatements = Resources.LoadAll<Statement> ("/");

		for (int i = 0; i < allStatements.Length; i++)
		{
			for (int j = 0; j < allTags.Length; j++)
			{
				if (allStatements[i].likedBy.Contains (allTags[j])) likeOccurances[j]++;
				if (allStatements[i].dislikedBy.Contains (allTags[j])) dislikeOccurances[j]++;
			}
		}

		for (int i = 0; i < allTags.Length; i++)
		{
			Debug.Log (string.Format ("{0} likes: {1} and dislikes {2}", allTags[i].name, likeOccurances[i], dislikeOccurances[i]));
		}
	}
}
