using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatementUI : MonoBehaviour
{
    Animator anim;
    private TextMeshProUGUI statementText;

    // Start is called before the first frame update
    public void Initialise (Statement statement)
    {
        statementText.text = statement.name;
    }

    public void Select ()
    {
        // float around
        // change appearance
    }

    public void Contradicted_Used ()
    {
        // stop floating around
        Contradicted ();
        // float up to a given point
    }

    public void Contradicted_Selected ()
    {
        Contradicted ();
        // float up to a given point
    }

    private void Contradicted ()
    {
        // change appearance
    }
}
