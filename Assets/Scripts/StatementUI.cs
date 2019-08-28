using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableFramework;

public class StatementUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statementText;
    [SerializeField] private IntEvent onOptionSelected;
	Animator anim;
	Image background;
	Button button;

	// Start is called before the first frame update
	public void Initialise (Statement statement)
    {
        statementText.text = statement.name;
		background = GetComponent<Image> ();
    }

    public void Select ()
    {
		onOptionSelected.RaiseEvent (transform.GetSiblingIndex ());
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
		background.color = Color.red;
    }
}
