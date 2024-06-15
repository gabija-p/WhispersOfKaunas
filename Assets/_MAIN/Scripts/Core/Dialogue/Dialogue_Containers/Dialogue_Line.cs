using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Dialogue_Line
    {
        public DL_SPEAKER_DATA Speaker;
        public string Dialogue;
        public string Commands;

        public bool hasSpeaker => Speaker != null;
        public bool hasDialogue => Dialogue != string.Empty;
        public bool hasCommands => Commands != string.Empty;
        public Dialogue_Line(string speaker, string dialogue, string commands) 
        {
            Speaker = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_SPEAKER_DATA(speaker));
            Dialogue = dialogue;
            Commands = commands;
        }
    }
}
