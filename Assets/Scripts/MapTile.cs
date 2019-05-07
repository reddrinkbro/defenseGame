using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

    [SerializeField] private bool isSetPlayer;

    private GameObject player = null;
    private SpriteRenderer render;
    private Color objectColor;
    private Sprite selectPlayer;
    Build build;
    UIManager ui;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        objectColor = render.color;
        build = Build.instance;
        ui = UIManager.instance;
    }
    void OnMouseOver()
    {
        if (isSetPlayer || player)
        {
            if (build.GetPlayerToBuild() == null) return;
            render.color = new Color(1, 0, 0, 1);
        }
        else
        {
            render.color = new Color(1, 1, 1, 0.5f);
        }
    }

    void OnMouseExit()
    {
        render.color = objectColor;
    }

    void OnMouseDown()
    {
        if (build.GetPlayerToBuild() == null) return;
        if (player != null) return;
        if (isSetPlayer) return;
        if (ui.buyUnit(ui.costMoney))
        {
            GameObject selectPlayer = Build.instance.GetPlayerToBuild();
            player = Instantiate(selectPlayer, transform.position, transform.rotation);
        }
    }
    private void OnMouseUp()
    {
        build.SetPlayerToBuild(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            isSetPlayer = true;
            Debug.Log("true");
        }*/
    }
}
