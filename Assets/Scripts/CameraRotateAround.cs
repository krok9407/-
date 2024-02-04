using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraRotateAround : MonoBehaviour
{
    public Camera MyCamera;
    public float ZoomSmoothSpeed = 4f;  // скорость плавности зума
    public float ZoomScale = 1f;        // множитель для изменения зума
 
    private Vector3 _targetZoomPosition;
    private bool _isZoomed;

	public float cam_sens_rotate; // скорость поворота
	public float cam_sens_move; // скорость движения
    private float camX;
    private float camY;
    private float camZ;
    private Transform camTransform;

	void Start()
	{
		camX = MyCamera.transform.eulerAngles.x;
        camY = MyCamera.transform.eulerAngles.y;
        camTransform = MyCamera.transform;
        camZ = Vector3.Distance(Vector3.zero, camTransform.position);
	}
 
    void Update()
    {
        float zoom = Input.mouseScrollDelta.y;
        if (zoom != 0)
        {
            _isZoomed = true;
            _targetZoomPosition = MyCamera.ScreenPointToRay(Input.mousePosition).GetPoint(zoom * ZoomScale);
        }
 
        if (_isZoomed)
        {
            MyCamera.transform.position = Vector3.Lerp(MyCamera.transform.position, _targetZoomPosition, ZoomSmoothSpeed * Time.deltaTime);
            if ((MyCamera.transform.position - _targetZoomPosition).sqrMagnitude < 0.001f)
            {
                MyCamera.transform.position = _targetZoomPosition;
                _isZoomed = false;
            }
        }

		float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");
		// если нажата правая мышка
		if (Input.GetAxis("Fire2") > 0)
            {
                if (mX != 0)
                {
                    camX += mX * cam_sens_rotate;
                    camTransform.transform.Rotate(Vector3.up, camX); // крутим оси "вверх"
                }
                if (mY != 0)
                {
                    camY -= mY * cam_sens_rotate;
                    camTransform.transform.Rotate(Vector3.right, camY); // крутим по оси "вправо"
                }
            }

		if (Input.GetAxis("Fire3")>0)
        {
            mX = -mX * cam_sens_move;
            mY = -mY * cam_sens_move;
            camTransform.position += (Vector3)(camTransform.rotation * (                      // кватерион камеры (угол поворота камеры) умножаем на
                (mX > 0 ? Vector3.right * mX : Vector3.left * (-mX)) +          // абсолютный вектор "влево"/"вправо" [с множителем от соответствующего шевеления мышкой] плюс
                (mY > 0 ? Vector3.up * mY : Vector3.down * (-mY))               // абсолютный вектор  "вверх"/"вниз" [с множителем от соответствующего шевеления мышкой]
                )   );                                                          // всё в одну строку сделал, чтобы position & rotation вызывался только один раз
        }
    }
}


// using UnityEngine;
// using System.Collections;

// public class CameraRotateAround : MonoBehaviour {

//     public Camera cam; // камера
//     public float cam_sens_rotate; // скорость поворота
//     public float cam_sens_move; // скорость движения
//     public float cam_wheel;

//     private float camX;
//     private float camY;
//     private float camZ;
//     private Transform camTr;
//     private bool Fire1Press;


//         // Use this for initialization
//         void Start () {
//         cam.transform.LookAt( Vector3.zero );
//         camX = cam.transform.eulerAngles.x;
//         camY = cam.transform.eulerAngles.y;
//         camTr = cam.transform;
//         camZ = Vector3.Distance(Vector3.zero, camTr.position);
//         Fire1Press = false;
//         }
       
//         // Update is called once per frame
//     void Update()
//     {
//         camX = 0f;
//         camY = 0f;
//         camZ = 0f;
//         // берем состояние мыши
//         // а конкретно - именованных осей
//         float mX = Input.GetAxis("Mouse X");
//         float mY = Input.GetAxis("Mouse Y");
//         // и колесо мышки
//         float mW = Input.GetAxis("MouseWheel");

//          // если нажата правая мышка
//             if (Input.GetAxis("Fire2") > 0)
//             {
//                 if (mX != 0)
//                 {
//                     camX += mX * cam_sens_rotate;
//                     camTr.transform.Rotate(Vector3.up, camX); // крутим оси "вверх"
//                 }
//                 if (mY != 0)
//                 {
//                     camY -= mY * cam_sens_rotate;
//                     camTr.transform.Rotate(Vector3.right, camY); // крутим по оси "вправо"
//                 }
//             }
//             // если крутили крутили колесо мыши
//             if (mW != 0)
//             {
//                 camZ = mW * cam_wheel;
//                 // здесь интересно:
//                 // умножаем кватерион поворота камеры (угол поворота камеры)
//                 // на абсолютный вектор "вперед" или "назад"[с множителем от соответствующего шевеления колесом мышки]
//                 // и прибавляем результат к положению камеры
//                 camTr.transform.position +=  (Vector3) (camTr.transform.rotation * (camZ>0 ? Vector3.forward * camZ : Vector3.back * (-camZ)));
//             }

//         // если нажата средняя мышка
//         if (Input.GetAxis("Fire3")>0)
//         {
//             mX = -mX * cam_sens_move;
//             mY = -mY * cam_sens_move;
//             camTr.position += (Vector3)(camTr.rotation * (                      // кватерион камеры (угол поворота камеры) умножаем на
//                 (mX > 0 ? Vector3.right * mX : Vector3.left * (-mX)) +          // абсолютный вектор "влево"/"вправо" [с множителем от соответствующего шевеления мышкой] плюс
//                 (mY > 0 ? Vector3.up * mY : Vector3.down * (-mY))               // абсолютный вектор  "вверх"/"вниз" [с множителем от соответствующего шевеления мышкой]
//                 )   );                                                          // всё в одну строку сделал, чтобы position & rotation вызывался только один раз
//         }

//          // если нажата левая мышка
//             if (Input.GetAxis("Fire1") > 0f && !Fire1Press) {
//                 Fire1Press = true;
//             }
//             if (Input.GetAxis("Fire1") == 0 && Fire1Press) Fire1Press = false;

//     }
// }
 
