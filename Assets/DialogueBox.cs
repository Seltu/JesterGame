using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sentenceText;
    [SerializeField] private Dialogue dialogue;
    private Queue<Dialogue.Sentence> _sentences = new Queue<Dialogue.Sentence>();

    private bool _typing;
    private bool _hasStarted;

    public void DoDialogue()
    {
        if (!_hasStarted)
            StartDialogue(dialogue);
        else
            StartCoroutine(DisplayNextSentence());
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();
        foreach (var sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
        StartCoroutine(DisplayNextSentence());
        _hasStarted = true;
    }

    public IEnumerator DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            yield break;
        }
        sentenceText.text = "";
        if (_hasStarted)
            yield return new WaitForSecondsRealtime(0.2f);
        if (_sentences.Count != 0)
        {
            var sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence.sentence));
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        string hold = "";
        bool holding = false;
        foreach (var letter in sentence.ToCharArray())
        {
            if (letter == '<')
                holding = true;
            else if (letter == '>')
            {
                sentenceText.text += hold;
                holding = false;
                hold = "";
            }
            if (holding)
                hold += letter;
            else
            {
                sentenceText.text += letter;
                yield return null;
            }
        }
    }

    private void EndDialogue()
    {
        _sentences.Clear();
        _hasStarted = false;
    }
}
