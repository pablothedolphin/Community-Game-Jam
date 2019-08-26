using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Game/Question")]
public class Question : ScriptableObject
{
    public Tag tag;

    public Statement[] responses;
}
