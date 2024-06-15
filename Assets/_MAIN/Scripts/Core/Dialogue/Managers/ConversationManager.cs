using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;

        private Coroutine process = null;
        public bool isRunning => process != null;

        private TextArchitect architect = null;
        private bool userPromt = false;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPromt_Next += OnUserPromt_Next;
        }

        private void OnUserPromt_Next()
        {
            userPromt = true;
        }

        public void StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));
        }
        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for(int i = 0; i < conversation.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                Dialogue_Line line = DialogueParser.Parse(conversation[i]);

                //Show dialogue.
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                //Run any commands
                if (line.hasCommands)
                    yield return Line_RunCommands(line);
            }
        }

        IEnumerator Line_RunDialogue(Dialogue_Line line)
        {
            //Hide or show the speaker name if there is one present.
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerName(line.Speaker.displayName);
            else
                dialogueSystem.HideSpeakerName();

            //Build dialogue.
            yield return BuildDialogue(line.Dialogue);

            //Wait for user input.
            yield return WaitForUserInput();
                
        }

        IEnumerator Line_RunCommands(Dialogue_Line line)
        {
            Debug.Log(line.Commands);
            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            architect.Build(dialogue);

            while (architect.isBuilding)
            {
                if (userPromt)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();

                    userPromt = false;
                }

                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while(!userPromt)
                yield return null;

            userPromt = false;
        }
    }
}
