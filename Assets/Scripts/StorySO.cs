using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Story", fileName = "New Level Story")]
public class StorySO : ScriptableObject
{
    [TextArea(4, 6)]
    [SerializeField] string story = "Enter new story text here";

    public string GetStory()
    { return story; }
}
