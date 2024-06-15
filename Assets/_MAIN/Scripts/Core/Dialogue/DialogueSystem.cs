using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueSystem instance;

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPromt_Next;

        public bool isRunningConversation => conversationManager.isRunning;

        //make sure that there's only one instance of Dialogue System in the scene.
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);
        }

        bool _initialized = false;
        private void Initialize()
        {
            if(_initialized) 
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        public void OnUserPromt_Next()
        {
            onUserPromt_Next?.Invoke();
        }

        public void ShowSpeakerName(string speakerName = "")
        {
            if (speakerName.ToLower() != "narrator")
                dialogueContainer.nameContainer.Show(speakerName);
            else
                HideSpeakerName();
        }
        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}