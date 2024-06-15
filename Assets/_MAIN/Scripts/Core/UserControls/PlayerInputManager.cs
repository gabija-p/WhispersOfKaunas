using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class PlayerInputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    PromptAdvance();
                }
            }
        }

        public void PromptAdvance()
        {
            DialogueSystem.instance.OnUserPromt_Next();
        }
    }
}
