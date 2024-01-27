using System;
using UnityEngine;
[Serializable]
public class Dialogue
{
    [Serializable]
    public struct Sentence
    {
        [TextArea(5, 15)]
        public string sentence;
    }
    public Sentence[] sentences;
}
