using UnityEngine;

public class BerryControl : MonoBehaviour
{
    public enum FruitType
    {
        Blueberry, Peach, Straweberry
    }

    [SerializeField]
    private FruitType fruitType;
    private float startPositionX;
    private float startPositionY;
    private bool onHold = false;
    private Vector3 initialPosition;
    private Collider2D basketCollider;

    private const int BERRY_Z_POSITION  = 1;
    private const string BASKET_LAYER   = "Basket";
    private const string BASKET_NAME    = "Peach";


    private void Start()
    {
        basketCollider = GameObject.FindGameObjectWithTag(BASKET_NAME).GetComponent<Collider2D>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (onHold)
        {
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            gameObject.transform.localPosition = new Vector3(mousePosition.x - startPositionX, mousePosition.y - startPositionY, BERRY_Z_POSITION);
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {        
            Vector3 _mousePosition;
            _mousePosition = Input.mousePosition;
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

            startPositionX = _mousePosition.x - this.transform.localPosition.x;
            startPositionY = _mousePosition.y - this.transform.localPosition.y;

            onHold = true;
        }        
    }

    private void OnMouseUp()
    {
        onHold = false;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, basketCollider.bounds.extents.x, LayerMask.GetMask(BASKET_LAYER));

        if(collider != null)
        {
            Basket _foundBasket = collider.GetComponent<Basket>();

            if (_foundBasket != null)
            {
                if (fruitType == _foundBasket.basketFruitType)
                {
                    _foundBasket.AddBerry();
                    GameManager.Instance.CheckIfWon();
                    Destroy(this.gameObject);
                }
            }
        }
        ReturnBerry();
    }

    private void ReturnBerry()
    {
        transform.position = new Vector3(initialPosition.x, initialPosition.y, BERRY_Z_POSITION);
    }
}
