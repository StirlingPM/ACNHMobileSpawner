using NHSE.Core;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class UI_ACItem : MonoBehaviour, IPointerClickHandler
{
	public RawImage ImageComponent, TopRightImage;
	public Button ButtonComponent;
    public MultiImage TreeMultiImage;
	public Text[] FiveInts;
	public bool Dummy;
    public Texture blank;

	[HideInInspector]
	public Item ItemAssigned;

	private void Start()
	{
    }

	private void Update()
	{
	}

	public void Assign(Item item)
	{
        //IL_0026: Unknown result type (might be due to invalid IL or missing references)
        //IL_006f: Unknown result type (might be due to invalid IL or missing references)

        ItemAssigned = item;

        if (item.IsMoneyTree() && SpriteBehaviour.SpritesExist())
        {
            TreeMultiImage.gameObject.SetActive(true);
            ImageComponent.enabled = false;
            //var firstInstance = IconSpriteHelper.CurrentInstance.GetCurrentParser().SpritePointerTable.FirstOrDefault(x => x.Key == item.Count.ToString("X"));
            Texture2D t2d = IconSpriteHelper.CurrentInstance.GetIconTexture(item.Count.ToString("X"));
            TreeMultiImage.SetRawImageAll(t2d);
        }
        else
        {
            TreeMultiImage.gameObject.SetActive(false);
            ImageComponent.enabled = true;
            Texture2D imageToAssign = SpriteBehaviour.ItemToTexture2D(ItemAssigned, out var c);
            if (imageToAssign)
            {
                ImageComponent.texture = imageToAssign;
            }
            else
            {
                ImageComponent.texture = blank;
            }
            ImageComponent.color = c;
        }

        if (item.IsInternalItem())
        {
            TopRightImage.texture = ResourceLoader.GetExclaimImage();
            TopRightImage.gameObject.SetActive(true);
        }
        else
            TopRightImage.gameObject.SetActive(false);
        
        FiveInts[0].text = item.Count.ToString();
        FiveInts[1].text = item.SystemParam.ToString();
        FiveInts[2].text = item.AdditionalParam.ToString();
        FiveInts[3].text = 0.ToString();
        FiveInts[4].text = item.UseCount.ToString();
        Text[] fiveInts = FiveInts;
        foreach (Text val in fiveInts)
            if (val.text == 0.ToString())
                val.text = "";
    }

    // for downloading acnh images (old/deprecated)
    public void DownloadImages()
    {
        System.IO.Compression.ZipFile.ExtractToDirectory("", "");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ItemAssigned.Delete();
            Assign(ItemAssigned);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            UI_ACItemGrid.LastInstanceOfItemGrid.DeleteRow(ItemAssigned);
    }
}
