using UnityEngine;
using UnityEngine.EventSystems;

public class DragSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabToSpawn;   // le prefab correspondant à ce bouton
    private GameObject ghostObject;    // l'objet qui suit la souris pendant le drag

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Crée un "fantôme" qui suit la souris
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        ghostObject = Instantiate(prefabToSpawn, worldPos, Quaternion.identity);

        // Désactive le collider pendant le drag pour pas gêner le joueur
        ghostObject.GetComponent<Collider2D>().enabled = false;

        // Légèrement transparent pour indiquer que c'est en cours de placement
        ghostObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ghostObject == null) return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        ghostObject.transform.position = worldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ghostObject == null) return;

        // Vérifie qu'on a pas relâché sur le panel UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Confirme le placement — réactive le collider
            ghostObject.GetComponent<Collider2D>().enabled = true;
            ghostObject.GetComponent<SpriteRenderer>().color = Color.white;
            ghostObject = null;
        }
        else
        {
            // Relâché sur le UI — annule
            Destroy(ghostObject);
        }
    }
}
