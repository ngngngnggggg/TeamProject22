using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door door;
    Animator animator;
    [SerializeField] GameObject[] environments;
    [SerializeField] private HW_Player player;

    public int count = 1;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && player.Getislaying)
        {
            count++;
            StartCoroutine(Open());
            foreach(GameObject environment in environments)
            {
                environment.SetActive(!environment.activeSelf);
            }
        }
    }
    public IEnumerator Open()
    {
        if (count == 1)
        {
            if (player.Getislaying)
            {
                yield return new WaitForSeconds(2f);
            }
            animator.SetBool("IsOpen", true);
        }
        yield return null;
    }
    public void Close()
    {
        //if (count == 1)
        //{
            animator.SetBool("IsOpen", false);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);

        }
    }

}
