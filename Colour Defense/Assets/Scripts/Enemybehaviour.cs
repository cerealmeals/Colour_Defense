using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    SpriteRenderer m_SpriteRenderer;
    public float red = 0;
    public float green = 0;
    public float blue = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        red = m_SpriteRenderer.color.r;
        green = m_SpriteRenderer.color.g;
        blue = m_SpriteRenderer.color.b;
    }
  

    public void TakeDamage(Vector3 color)
    {
        
        red += color.x;
        green += color.y;
        blue += color.z;
        m_SpriteRenderer.color = new Color(red, green, blue);
        
        if (red <= 0 && green <= 0 && blue <= 0)
        {
            Destroy(gameObject);
        }
    }
}
