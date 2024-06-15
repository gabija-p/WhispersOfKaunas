using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        // Start is called before the first frame update
        void Start()
        {
            SendFileToParse();
        }

        void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset("testFile", false);

            foreach (var line in lines)
            {
                Dialogue_Line dl = DialogueParser.Parse(line);
            }
        }
    }
}
