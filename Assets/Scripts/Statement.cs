using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Game/Statement")]
public class Statement : ScriptableObject
{
    public int impressiveness;

    public List<Tag> tags;

    public List<Statement> contradictiveStatements;
    public List<Statement> supportingStatements;


}