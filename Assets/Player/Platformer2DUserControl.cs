using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                if (this.transform.GetComponent<Player>().playerNum == 1)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump1");
                }
                else if (this.transform.GetComponent<Player>().playerNum == 2)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump2");
                }
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = 0;
            if (this.transform.GetComponent<Player>().playerNum == 1)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal1");
            }
            else if(this.transform.GetComponent<Player>().playerNum == 2)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal2");
            }
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}