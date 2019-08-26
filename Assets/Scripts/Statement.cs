using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Game/Statement")]
public class Statement : ScriptableObject
{
    public int impressiveness;

    public Tag[] tags;

    public Statement[] contradictiveStatements;
    public Statement[] supportingStatements;


}