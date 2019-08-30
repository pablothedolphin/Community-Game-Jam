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
    [SerializeField] private Sprite heart;
    [SerializeField] private Sprite questionMark;
    [SerializeField] private Sprite anger;
	[SerializeField] private Image responseIcon;
	Animator anim;
	Image background;
	Button button;

	// Start is called before the first frame update
	public void Initialise (Statement statement)
    {
        statementText.text = statement.name;
		background = GetComponent<Image> ();
		button = GetComponent<Button> ();
	}

    public void Select ()
    {
		button.interactable = false;
		onOptionSelected.RaiseEvent (transform.GetSiblingIndex ());
        // float around
        // change appearance
    }

    public void Contradicted ()
    {
		// change appearance
		background.color = Color.red;

		responseIcon.sprite = questionMark;
	}

	public void Disliked ()
	{
		// change appearance
		background.color = Color.red;

		responseIcon.sprite = anger;

	}

	public void Liked ()
	{
		// change appearance
		background.color = Color.green;
		responseIcon.sprite = heart;
	}
}
