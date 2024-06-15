using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueFiles : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    // Start is called before the first frame update
    void Start()
    {
        StartConversation();
    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset("testFile", false);

        DialogueSystem.instance.Say(lines);
    }
}
