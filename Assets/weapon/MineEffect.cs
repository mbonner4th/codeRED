using UnityEngine;
using System.Collections;

public class MineEffect : MonoBehaviour {

    private Transform[] m_GroundCheck = new Transform[3];
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_Grounded = false;
    const float k_GroundedRadius = .2f;
    public Transform explosionPrefab;
    private Vector3 inPlace;
    private float damage;

    void Awake () {
        m_GroundCheck[0] = transform.Find("GroundCheck");
        m_GroundCheck[1] = transform.Find("GroundCheck1");
        m_GroundCheck[2] = transform.Find("GroundCheck2");
    }
	// Update is called once per frame
	void Update () {
        if(!m_Grounded)
        {
            fixedUpdate();
        }
        if (m_Grounded) {
            stayInPlace();
            Vector3 velocity;
            velocity.x = 0;
            velocity.y = 0;
            velocity.z = 0;
            transform.GetComponent<Rigidbody2D>().velocity = velocity;
        }
	}

    private void stayInPlace() {
        transform.position = inPlace;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (m_Grounded) {
            if (other.gameObject.tag == "Player") {
                Effect();
                Destroy(this.gameObject);
            }
        }
    }

    private void fixedUpdate(){
        if (m_Grounded) { return; }
        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        for (int j = 0; j < m_GroundCheck.Length; j++) {
            if (m_Grounded) { break; }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck[j].position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject) {
                    m_Grounded = true;
                    inPlace.x = transform.position.x;
                    inPlace.y = transform.position.y;
                    inPlace.z = transform.position.z;
                    break;
                }
            }
        }
    }

    private void Effect() {
        ((Transform)Instantiate(explosionPrefab, transform.position, transform.rotation)).GetComponent<Damager>().setDamage(damage);
    }

    public void setDamage(float o_damage) {
        damage = o_damage;
    }
}
