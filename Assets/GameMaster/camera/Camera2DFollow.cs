using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target1 = null;
        public Transform target2 = null;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
		public float yRestriction = -16;
        public float minY = 10;
        public float heightCeling = 30.0f;
        public float widthCeiling = 30.0f;
        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 midpoint;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

		private float nextTimeToSearchForPlayer = 0;

        // Use this for initialization
        private void Start()
        {

            midpoint = findMidpoint();
            m_LastTargetPosition = midpoint;
            m_OffsetZ = (transform.position - midpoint).z + 3;
            transform.parent = null;

        }

        // Update is called once per frame
        public void Update()
        {

            // only update lookahead pos if accelerating or changed direction

                float xMoveDelta = (findMidpoint() - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

                Vector3 aheadTargetPos = findMidpoint() + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yRestriction, Mathf.Infinity), newPos.z);

                transform.position = newPos;

                SetCameraZoom();

                m_LastTargetPosition = findMidpoint();
            
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

		

        Vector3 findMidpoint()
        {
            Player[] players = FindObjectsOfType<Player>();
            ControlledMovement[] rockets = FindObjectsOfType<ControlledMovement>();
            Vector3 sum;
            sum.x = 0;
            sum.y = 0;
            sum.z = 0;
            float count = 0f;
            foreach(Player p in players)
            {
                if (!p.IsDead) {
                    sum += p.transform.position;
                    count++;
                }
                
            }
            foreach (ControlledMovement r in rockets)
            {
                sum += r.transform.position;
                count++;
            }
            return (sum) / count;
        }

        public void SetCameraZoom()
        {
            //float minX = minY * Screen.width / Screen.height;


            float width = Mathf.Abs(iminX() - maxX());
            float height = Mathf.Abs(iminY() - maxY());
            float minX = minY * Screen.width / Screen.height;
            float camX = Mathf.Max(width, minX);

            //Debug.Log("sreen width= " + width);

            if (height > heightCeling)
            {
                height = heightCeling;
            }
            if (camX > widthCeiling)
            {
                camX = widthCeiling;
            }
            Debug.Log("screen height= " + height + " screen widtth = " + (camX * Screen.height / Screen.width));
            transform.GetComponent<Camera>().orthographicSize = Mathf.Max(height, camX * Screen.height / Screen.width, minY);
        }

        public float iminX()
        {
            bool initted = false;
            float tminX = 1;
            Player[] players = FindObjectsOfType<Player>();
            ControlledMovement[] rockets = FindObjectsOfType<ControlledMovement>();
            foreach (Player p in players)
            {
                if (!initted) {
                    initted = true;
                    tminX = p.transform.position.x;
                } else
                {
                    if (tminX > p.transform.position.x)
                    {
                        tminX = p.transform.position.x;
                    }
                }
            }
            foreach (ControlledMovement r in rockets)
            {
                if (!initted)
                {
                    initted = true;
                    tminX = r.transform.position.x;
                }
                else
                {
                    if (tminX > r.transform.position.x)
                    {
                        tminX = r.transform.position.x;
                    }
                }
            }
            return tminX;
        }

        public float maxX()
        {
            bool initted = false;
            float tminX = 1;
            Player[] players = FindObjectsOfType<Player>();
            ControlledMovement[] rockets = FindObjectsOfType<ControlledMovement>();
            foreach (Player p in players)
            {
                if (!initted)
                {
                    initted = true;
                    tminX = p.transform.position.x;
                }
                else
                {
                    if (tminX < p.transform.position.x)
                    {
                        tminX = p.transform.position.x;
                    }
                }
            }
            foreach (ControlledMovement r in rockets)
            {
                if (!initted)
                {
                    initted = true;
                    tminX = r.transform.position.x;
                }
                else
                {
                    if (tminX < r.transform.position.x)
                    {
                        tminX = r.transform.position.x;
                    }
                }
            }
            return tminX;
        }

        public float iminY()
        {
            bool initted = false;
            float tminX = -10;
            Player[] players = FindObjectsOfType<Player>();
            ControlledMovement[] rockets = FindObjectsOfType<ControlledMovement>();
            foreach (Player p in players)
            {
                if (p.IsDead) { continue; }

                if (!initted)
                {
                    initted = true;
                    tminX = p.transform.position.x;
                }
                else
                {
                    if (tminX > p.transform.position.x)
                    {
                        tminX = p.transform.position.x;
                    }
                }
            }
            foreach (ControlledMovement r in rockets)
            {
                if (!initted)
                {
                    initted = true;
                    tminX = r.transform.position.x;
                }
                else
                {
                    if (tminX > r.transform.position.x)
                    {
                        tminX = r.transform.position.x;
                    }
                }
            }
            return tminX;
        }

        public float maxY()
        {
            bool initted = false;
            float tminX = 10;
            Player[] players = FindObjectsOfType<Player>();
            ControlledMovement[] rockets = FindObjectsOfType<ControlledMovement>();
            foreach (Player p in players)
            {
                if (p.IsDead)
                {
                    continue;
                }
                if (!initted)
                {
                    initted = true;
                    tminX = p.transform.position.x;
                }
                else
                {
                    if (tminX < p.transform.position.x)
                    {
                        tminX = p.transform.position.x;
                    }
                }
            }
            foreach (ControlledMovement r in rockets)
            {
                if (!initted)
                {
                    initted = true;
                    tminX = r.transform.position.x;
                }
                else
                {
                    if (tminX < r.transform.position.x)
                    {
                        tminX = r.transform.position.x;
                    }
                }
            }
            return tminX;
        }
    }
}
