using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

namespace TESTING
{
    public class Testing_TextArchitect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        string[] lines = new string[5]
        {
            "This is a random line of dialogue.",
            "I want to say something, come over here.",
            "The world is a crazy place sometimes.",
            "Don't lose hope, things will get better!",
            "Can you guess what stood here a long time ago?"
        };

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
        }

        // Update is called once per frame
        void Update()
        {
            string longLine = "This is a very long line that makes no sense but I am just populating it with stuff because I need some long text.";
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (architect.isBuilding)
                    {
                        if (!architect.hurryUp)
                            architect.hurryUp = true;
                        else
                            architect.ForceComplete();
                    }
                    else
                        architect.Build(longLine);
                }
                //else
                    //architect.Build(lines[Random.Range(0, lines.Length)]);

            }
        }
    }
}
