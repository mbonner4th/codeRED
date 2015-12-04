using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target1;
        public Transform target2;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
		public float yRestriction = -16;
        public float minY = 10;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 midpoint;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

		private float nextTimeToSearchForPlayer = 0;

        // Use this for initialization
        private void Start()
        {
            midpoint = findMidpoint(target1.position,target2.position);
            m_LastTargetPosition = midpoint;
            m_OffsetZ = (transform.position - midpoint).z+3;
            transform.parent = null;
        }


        // Update is called once per frame
        public void Update()
        {
            if (target1 == null || target2 == null) {
				findPlayer();
				return;
			}
            // only update lookahead pos if accelerating or changed direction
            if (target1 && target2)
            {
                float xMoveDelta = (findMidpoint(target1.position, target2.position) - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

                Vector3 aheadTargetPos = findMidpoint(target1.position, target2.position) + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yRestriction, Mathf.Infinity), newPos.z);

                transform.position = newPos;

                SetCameraZoom();

                m_LastTargetPosition = findMidpoint(target1.position,target2.position);
            }
        }

        void findPlayer()
        {
            if (nextTimeToSearchForPlayer <= Time.time)
            {

                GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
                if (searchResult != null)
                {
                    if (target1 == null)
                    {
                        target1 = searchResult.transform;
                    }
                    else if (target2 == null)
                    {
                        target2 = searchResult.transform;
                    }
                }
                nextTimeToSearchForPlayer = Time.time + 0.5f;
            }
        }

		

        Vector3 findMidpoint(Vector3 target1, Vector3 target2)
        {
            return (target1 + target2) * 0.5f;
        }

        public void SetCameraZoom()
        {
            float minX = minY * Screen.width / Screen.height;

            float width = Mathf.Abs(target1.position.x - target2.position.x);
            float height = Mathf.Abs(target1.position.y - target2.position.y);
            float camX = Mathf.Max(width, minX);

            transform.GetComponent<Camera>().orthographicSize = Mathf.Max(height, camX * Screen.height / Screen.width, minY);
        }

    }
}
