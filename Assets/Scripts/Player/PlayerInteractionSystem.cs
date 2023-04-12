using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float interactionDistance = 3f;
    [SerializeField]
    private GameObject interactibleIndication;

    private Camera cam;
    private bool interactibleFound = false;

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, interactionDistance, mask))
        {
            
            if (hitInfo.collider.GetComponent<Interactible>() != null)
            {
                Interactible interactible = hitInfo.collider.GetComponent<Interactible>();
                interactibleIndication.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"{interactible.promptMessage}: F";
                interactibleIndication.SetActive(true);
                interactibleFound = true;
            }
            else
            {
                interactibleFound = false;
                interactibleIndication.SetActive(false);
            }
        }
    }

    public void Interact()
    {
        if (!interactibleFound) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, interactionDistance, mask))
        {
            hitInfo.collider.gameObject.GetComponent<Interactible>().BaseInteract();
        }
    }
}
