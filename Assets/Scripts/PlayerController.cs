using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static Player player;
    CharacterController contrl;
    public Camera cam;
    Vector3 camPos;
    //вторая камера чисто для видосов или типо того, потом надо вырезать
    public Camera cam2;
    public float speed = 1;

    //public Player player;

    //взаимодействие с предметами мышкой
    public bool choseUsable;
    public Usable currentUsable;



      void Start () {
        player = GetComponent<Player>();
        contrl = GetComponent<CharacterController>();
        camPos = new Vector3(0, 15, -13);
	}

    void FixedUpdate()
    {

        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        Vector3 movHorizontal = Vector3.right * xMov;
        Vector3 movVertical = Vector3.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        if (!player.seating)
        {
            transform.LookAt(transform.position + velocity);
            contrl.Move(velocity * Time.fixedDeltaTime);
        }

        cam.transform.position = gameObject.transform.position + camPos;

        //НАДО ОТДЕЛЬНЫЙ КОНТРОЛЛЕР ДЛЯ АНИМАЦИИ
        if (velocity != new Vector3(0, 0, 0))
            GetComponent<Animator>().SetBool("move", true);
        else
            GetComponent<Animator>().SetBool("move", false);

    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {


            //over object text
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10);

            if (!choseUsable)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.GetComponent<Usable>() != null)
                    {
                        currentUsable = hit.transform.GetComponent<Usable>();

                        Transform objectHit = hit.transform;
                        //Debug.Log("Ray Hit! " + objectHit.name);
                        UIManager.uiManager.ShowOverText(hit.transform.gameObject);




                        //КЛИК!
                        if (Input.GetMouseButtonDown(0))
                        {
                            player.Triggered(hit.collider);
                            player.avUses.CollectUses();
                            UIManager.uiManager.ContextDraw(hit.transform.gameObject.GetComponent<Usable>(), player);
                            choseUsable = true;
                        }
                    }
                    else
                    {
                        player.avUses.ClearUses();
                        UIManager.uiManager.ContextClear();
                        player.UnTriggered(hit.collider);
                        currentUsable = null;
                        UIManager.uiManager.HideOverText();

                    }
                }

            }
            else
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.GetComponent<Usable>() != null)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            choseUsable = false;

                            player.avUses.ClearUses();
                            UIManager.uiManager.ContextClear();
                            player.UnTriggered(hit.collider);
                            currentUsable = null;
                            UIManager.uiManager.HideOverText();
                        }
                    }
                }
            }
        }
        //-------------



        if (player.useBool)
        {
            if (Input.GetKeyDown(KeyCode.E))
                player.Use();

        }

        //управление временем (для удобства, потом надо вырезать)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Time.timeScale = 0.5f;

        //смена камеры, для видосов
        if (Input.GetKeyDown(KeyCode.C))
        {
            cam.enabled = !cam.enabled;
            cam2.enabled = !cam2.enabled;
        }

    }


}
