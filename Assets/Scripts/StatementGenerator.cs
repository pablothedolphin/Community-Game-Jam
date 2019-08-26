using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatementGenerator : MonoBehaviour
{
    public StatementUI statementUI;

    List<Statement> allStatements;
    List<Statement> usedStatements;

    Statement[] shownStatements;

    // Start is called before the first frame update
    void Start()
    {
        usedStatements = new List<Statement> ();
        allStatements = new List<Statement> (Resources.LoadAll<Statement> ("/Statements/"));
    }

    public void GenerateSetOfFour ()
    {
        List<int> randomIndecies = new List<int> ();

        while (randomIndecies.Count < 4)
        {
            int index = Random.Range (0, allStatements.Count);

            if (!randomIndecies.Contains (index))
                randomIndecies.Add (index);
        }

        shownStatements = new Statement[4];

        shownStatements[0] = allStatements[randomIndecies[0]];
        shownStatements[1] = allStatements[randomIndecies[1]];
        shownStatements[2] = allStatements[randomIndecies[2]];
        shownStatements[3] = allStatements[randomIndecies[3]];
    }

    public void SelectStatement (int index)
    {
        allStatements.Remove (shownStatements[index]);

        if (DoesStatementContradict (shownStatements[index])) Debug.Log ("oops");
        else Debug.Log ("phew!");

        usedStatements.Add (shownStatements[index]);
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
