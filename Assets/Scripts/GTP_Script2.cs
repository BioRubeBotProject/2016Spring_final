using UnityEngine;
using System.Collections;

public class GTPScript2 : MonoBehaviour
{
    GameObject GTP;
    GameObject GDP;
    public GameObject Gprotein;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GTP.transform.position = GDP.transform.position;
    }
}

public class GTP_Roam : MonoBehaviour
{
    public static float _max = 150f;
    public static float _min = -150f;
    public static float _speed = 5.0f;
    public static float heading;
    public static float headMin = -180;
    public static float headMax = 180;
    public static float maxDirectionChange = 0;
    public static float speed = 4;
    public static float randomizer = 0;
    public Transform origin;                    // origin location/rotation is the physical GTP
    public static Quaternion _lookRotation;
    public static Vector3 _direction;

    public static IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    public static void OnGUI() //creates the GUI with the scoreboard on it
    {
        GUI.Box(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Collision\n");
    }

    public static void gtpRoaming(GameObject Obj) //NEW GTP roaming function, similar to ATP
    {
        heading = Random.Range(0, 360);
        if (Time.timeScale > 0) // if simulation is running (not paused)
        {
            RaycastHit2D collision = Physics2D.Raycast(Obj.transform.position, Obj.transform.up); // get location and direction
            randomizer = Random.Range(1, 100);
            maxDirectionChange = Random.Range(-15, 15); //Max heading that the GTP can make
            if (randomizer <= 20)
            {   // avoid collision with membrane and nucleus
                if (collision.collider != null && collision.collider.name == "Cell Membrane" && collision.distance < 2 ||
                    collision.collider != null && collision.collider.name == "Inner Cell Wall" && collision.distance < 2)
                {
                    if (heading <= 180)
                    {
                        for (int i = 0; i < 180; i++)
                        {
                            randomizer = Random.Range(1, 100); //forces the GTP to actually turn instead of snap to new direction
                            if (randomizer <= 20)
                            {
                                Obj.transform.eulerAngles = new Vector3(0, 0, Obj.transform.eulerAngles.z + 1);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 180; i > 0; i--)
                        {
                            randomizer = Random.Range(1, 100); //forces the GTP to turn
                            if (randomizer <= 20)
                            {
                                Obj.transform.eulerAngles = new Vector3(0, 0, Obj.transform.eulerAngles.z - 1);
                            }
                        }
                    }
                }
                else
                {
                    Obj.transform.eulerAngles = new Vector3(0, 0, Obj.transform.eulerAngles.z - maxDirectionChange); //direction change
                }
            }
            Obj.transform.position += Obj.transform.up * Time.deltaTime * speed; //move forward
        }
    }

    public static void RoamingTandem(GameObject Obj1, GameObject Obj2, Vector3 Offset)
    {
        if (Time.timeScale > 0)// if simulation is running
        {
            float randomX, randomY;     //random number between minX/maxX and minY/maxY
            randomX = Random.Range(_min, _max); //get random x vector coordinate
            randomY = Random.Range(_min, _max); //get random y vector coordinate
                                                //apply a force to the object in direction (x,y):
            Obj1.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY), ForceMode2D.Force);
            Obj2.transform.position = Obj1.transform.position + Offset;
        }
    }

    public static Vector3 CalcMidPoint(GameObject obj_1, GameObject obj_2)
    {
        float[] temp = new float[2];
        temp[0] = (obj_1.transform.position.x + obj_2.transform.position.x) / 2.0f;
        temp[1] = (obj_1.transform.position.y + obj_2.transform.position.y) / 2.0f;
        Vector3 meetingPoint = new Vector3(temp[0], temp[1], obj_1.transform.position.z);

        return meetingPoint;
    }

    public interface CollectObject
    {
        void GetObject(GameObject obj, string newTag);
    }

    public static void FindAndWait<T>(T obj, GameObject self, ref Transform myTarget, ref float delay, string changeTag) where T : MonoBehaviour, CollectObject
    {
        if (obj != null && myTarget == null)
        {
            delay = 0;
            obj.GetObject(self, changeTag);
            myTarget = obj.transform;
        }
        if (myTarget != null && (delay += Time.deltaTime) >= 5)
        {

        }
        else
        {
            Roam.Roaming(self.gameObject);
        }
    }

    public static GameObject FindClosest(Transform my, string objTag)
    {
        float distance = Mathf.Infinity; //initialize distance to 'infinity'

        GameObject[] gos; //array of gameObjects to evaluate
        GameObject closestObject = null;
        //populate the array with the objects you are looking for
        gos = GameObject.FindGameObjectsWithTag(objTag);

        //find the nearest object ('objectTag') to me:
        foreach (GameObject go in gos)
        {
            //calculate square magnitude between objects
            float curDistance = Vector3.Distance(my.position, go.transform.position);
            if (curDistance < distance)
            {
                closestObject = go; //update closest object
                distance = curDistance;//update closest distance
            }
        }
        return closestObject;
    }/* end FindClosest */

    public static bool ApproachMidpoint(GameObject obj1, GameObject obj2, bool[] midpointAchieved, Vector3 midpoint, Vector3 Offset, float Restraint)
    {
        if (!midpointAchieved[0])
        {
            midpointAchieved[0] = Roam.ApproachVector(obj1, midpoint, Offset, Restraint);
        }

        if (!midpointAchieved[1])
        {
            midpointAchieved[1] = Roam.ApproachVector(obj2, midpoint, -1 * Offset, Restraint);
        }
        return (midpointAchieved[0] && midpointAchieved[1]);
    }

    public static bool ApproachVector(GameObject obj, Vector3 destination, Vector3 offset, float restraint)
    {
        if (Vector3.Distance(obj.transform.position, destination) > restraint)
        {
            gtpRoaming(obj);
        }
        return ProceedToVector(obj, destination + offset);
    }

    public static bool ProceedToVector(GameObject obj, Vector3 approachVector)
    {
        float angle1 = Vector3.Angle(obj.transform.up, obj.transform.up); //GTP faces straight up
        Vector3 lookPos = approachVector - obj.transform.position;
        Quaternion q1 = Quaternion.AngleAxis(angle1, Vector3.forward);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, q1, Time.deltaTime * speed + 5);
        //StartCoroutine("Wait");
        ///float angle2 = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg; // this doesn't seem to work, intention was to have the GTP face the approachVector
        //_direction = (approachVector).normalized;
        //_lookRotation = Quaternion.LookRotation(_direction);
        float step = (_speed + 2) * Time.deltaTime;
        //obj.transform.eulerAngles = new Vector3(0, 0, approachVector);
        //obj.transform.LookAt(approachVector);
        //obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, _lookRotation, Time.deltaTime * 2);
        ///Quaternion q = Quaternion.AngleAxis(angle2, Vector3.forward);
        ///obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, q, Time.deltaTime * speed+5);
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, approachVector, step);  //moves toward g protein once it is available
        return (approachVector == obj.transform.position);
    }

    public static void setAlpha(GameObject obj, float alpha)
    {
        if (obj.GetComponent<Renderer>() != null)
        {
            Color a = obj.gameObject.GetComponent<Renderer>().material.color;
            a.a = alpha;

            obj.gameObject.GetComponent<Renderer>().material.color = a;
        }
        else if (obj.GetComponentInChildren<Renderer>() != null)
        {
            Color a = obj.gameObject.GetComponentInChildren<Renderer>().material.color;
            a.a = alpha;

            obj.gameObject.GetComponentInChildren<Renderer>().material.color = a;
            for (int i = 0; i < obj.gameObject.transform.childCount; i++)
            {
                setAlpha(obj.gameObject.transform.GetChild(i).gameObject, alpha);
            }
        }
    }
}