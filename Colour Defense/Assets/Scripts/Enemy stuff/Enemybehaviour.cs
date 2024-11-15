using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    SpriteRenderer m_SpriteRenderer;
    public float red = 0;
    public float green = 0;
    public float blue = 0;
    public int gold;
    public int playerdamage;
    public float damageScale;
    public GoldAndHealthManager goldAndHealthManager;

    // ------------------for following the path------------------
    public List<Transform> sortedPointsTransform;
    public float movespeed;
    private int i = 0;
    // ---------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 2;
        FindMapWaypoints();
        transform.position = sortedPointsTransform[i].transform.position;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        red = m_SpriteRenderer.color.r;
        green = m_SpriteRenderer.color.g;
        blue = m_SpriteRenderer.color.b;
        if (goldAndHealthManager == null)
        {
            goldAndHealthManager = (GoldAndHealthManager)FindAnyObjectByType(typeof(GoldAndHealthManager));
            if (goldAndHealthManager == null)
            {
                Debug.Log("Error: no GoldAndHealthManager found");
            }
        }
    }
  

    public void TakeDamage(Vector3 color)
    {
        
        red += color.x * damageScale;
        green += color.y * damageScale;
        blue += color.z * damageScale;
        m_SpriteRenderer.color = new Color(red, green, blue);
        
        if (red <= 0 && green <= 0 && blue <= 0)
        {
            //Debug.Log("enemy shoudl die");
            goldAndHealthManager.GainGold(gold);
            Destroy(gameObject);
        }
    }


    // ---------------------------for following the path---------------------------
    private void FindMapWaypoints()
    {
        GameObject[] WayPointsObjects = GameObject.FindGameObjectsWithTag("Points");
        foreach (GameObject child in WayPointsObjects)
        {
            sortedPointsTransform.Add(child.transform);
        }
        sortedPointsTransform = sortedPointsTransform.OrderBy(go => go.name).ToList();

    }
    // ---------------------------------------------------------------------------------

    void Update()
    {
        // --------------------------- for following the path ---------------------------
        transform.position = Vector2.MoveTowards(transform.position, sortedPointsTransform[i].transform.position, movespeed * Time.deltaTime);

        if (transform.position == sortedPointsTransform[i].position)
        {
            i++;
        }

        if (i > sortedPointsTransform.Count - 1)
        {

            goldAndHealthManager.ReduceHealth(playerdamage);
            Destroy(gameObject);
        }
        // ---------------------------------------------------------------------------------
    }
}
