using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
	private RectTransform rect;

	[SerializeField]
	private RectTransform itemPrototype;

	[SerializeField]
	public ScrollRect scrollRect = null;
	[SerializeField]
	private MonoBehaviour controller = null;

	[SerializeField, Range(0, 30)]
	int instantateItemCount = 9;

	[SerializeField]
	private Direction direction;

	public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

	[System.NonSerialized]
	public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

	[SerializeField]
	private RectTransform m_BackGround = null;

	protected float diffPreFramePosition = 0;

	protected int currentItemNo = 0;

	public enum Direction
	{
		Vertical,
		Horizontal,
	}

	// cache component

	private RectTransform _rectTransform;
	protected RectTransform rectTransform
	{
		get
		{
			if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
	}

	private float anchoredPosition
	{
		get
		{
			return direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
		}
	}

	private float _itemScale = -1;
	public float itemScale
	{
		get
		{
			if (itemPrototype != null && _itemScale == -1)
			{
				_itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y : itemPrototype.sizeDelta.x;
			}
			return _itemScale;
		}
	}

	protected override void Start()
	{
		Init();
	}

	public void Init()
    {
		if (itemList.Count == 0)
		{
			scrollRect.horizontal = direction == Direction.Horizontal;
			scrollRect.vertical = direction == Direction.Vertical;
			scrollRect.content = rectTransform;

			itemPrototype.gameObject.SetActive(false);

			for (int i = 0; i < instantateItemCount; i++)
			{
				var item = GameObject.Instantiate(itemPrototype) as RectTransform;
				item.SetParent(transform, false);
				item.name = i.ToString();
				item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemScale * i) : new Vector2(itemScale * i, 0);
				itemList.AddLast(item);

				item.gameObject.SetActive(true);

				(controller as IInfiniteScrollSetup).OnUpdateItem(i, item.gameObject);
			}

		(controller as IInfiniteScrollSetup).OnPostSetupItems();
		}
	}

	void FixedUpdate()
	{
		if (itemList.First == null)
		{
			return;
		}


		scrollRect.movementType = ScrollRect.MovementType.Clamped;
		while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
		{
			diffPreFramePosition -= itemScale;

			var item = itemList.First.Value;
			itemList.RemoveFirst();
			itemList.AddLast(item);

			var pos = itemScale * instantateItemCount + itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

			onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

			currentItemNo++;
		}

		while (anchoredPosition - diffPreFramePosition > 0)
		{
			diffPreFramePosition += itemScale;

			var item = itemList.Last.Value;
			itemList.RemoveLast();
			itemList.AddFirst(item);

			currentItemNo--;

			var pos = itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

			onUpdateItem.Invoke(currentItemNo, item.gameObject);
		}



	}


	private ScrollRect.MovementType Position()
	{

		var PosY = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
		var MaxPosY = gameObject.GetComponent<RectTransform>().sizeDelta.y;


		ScrollRect.MovementType scroll = ScrollRect.MovementType.Unrestricted;
		float changeY = 130.0f;
		switch (scroll)
		{
			case ScrollRect.MovementType.Unrestricted:
				if (PosY < 0 || PosY >= MaxPosY - m_BackGround.sizeDelta.y)
				{
					scroll = ScrollRect.MovementType.Clamped;
					scrollRect.inertia = false;
				}
				else
					scrollRect.inertia = true;
				break;
			case ScrollRect.MovementType.Elastic:
				if (PosY >= 0 || PosY < MaxPosY - m_BackGround.sizeDelta.y)
					scroll = ScrollRect.MovementType.Unrestricted;
				break;
			case ScrollRect.MovementType.Clamped:
				if (PosY >= 0 || PosY < MaxPosY - m_BackGround.sizeDelta.y)
					scroll = ScrollRect.MovementType.Unrestricted;
				break;
			default:
				break;
		}

		return scroll;
	}

	[System.Serializable]
	public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}
