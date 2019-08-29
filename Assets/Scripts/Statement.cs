using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Game/Statement")]
public class Statement : ScriptableObject
{
    public int impressiveness;

    public List<Tag> likedBy;
    public List<Tag> dislikedBy;

	public List<Statement> contradictiveStatements;
}