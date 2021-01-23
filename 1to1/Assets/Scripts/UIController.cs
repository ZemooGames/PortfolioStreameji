using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Xml;

public class UIController : MonoBehaviour
{

    public GameObject itemUIPrefab;
    public GameObject inventoryScreenGO;
    public GameObject buttonsContainerGO;
    public Transform inventoryContainer;
    string path;
    string list;
    public RawImage Image;


    XmlDocument itemDataXml;

    private void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("XML/InventoryItemData");
        itemDataXml = new XmlDocument();
        itemDataXml.LoadXml(xmlTextAsset.text);
        inventoryScreenGO.SetActive(false);
    }

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Overwrite with xml", "", "xml");
        if (path == null)
        {
            Debug.Log("no path");
        }
        else
        {
            Debug.Log("PAth found " + path);
            LoadByXML(path);
            SaveByXML();
        }


        //path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        //GetImage();
    }

    private void SaveByXML()
    {
        //        Save save = createSaveGameObject();
        XmlDocument xmlDocument = new XmlDocument();//MARKER using System.Xml; NAMESPACE

        #region CreateXML elements

        XmlElement root = xmlDocument.CreateElement("movepattern");//MARKER <movepattern>...elements...</movepattern>
        root.SetAttribute("version", "5");//OPTIONAL

        XmlElement moveIdle = xmlDocument.CreateElement("move");
        moveIdle.SetAttribute("id", "0");
        moveIdle.SetAttribute("index", "0");
        moveIdle.SetAttribute("loop", "1");
        root.AppendChild(moveIdle);

        XmlElement stand000 = xmlDocument.CreateElement("frame");//MARKER <frame> Frame Data </frame> under root
        stand000.SetAttribute("image", "stand000.bmp");
        stand000.SetAttribute("texwidth", "118");
        stand000.SetAttribute("texheight", "88");
        stand000.SetAttribute("xoffset", "138");
        stand000.SetAttribute("yoffset", "171");
        stand000.SetAttribute("duration", "6");
        stand000.InnerText = "IN TEXT";
        moveIdle.AppendChild(stand000);

        XmlElement stand001 = xmlDocument.CreateElement("frame");//MARKER <frame> Frame Data </frame> under root
        stand001.SetAttribute("image", "stand001.bmp");
        stand001.SetAttribute("texwidth", "118");
        stand001.SetAttribute("texheight", "88");
        stand001.SetAttribute("xoffset", "138");
        stand001.SetAttribute("yoffset", "171");
        stand001.SetAttribute("duration", "6");
        stand001.InnerText = "IN TEXT";
        moveIdle.AppendChild(stand001);
        #endregion 

        xmlDocument.AppendChild(root);//Add the child and its children elements to the XML Document
        xmlDocument.Save(Application.dataPath + "/DataXML.text");
        //if(File.Exists(Application.dataPath + "/DataXML.text"))
        {
            Debug.Log("Application data path: " + Application.dataPath);
        }
    }

    private void LoadByXML(string pathTofile)
    {

        if (pathTofile != null)
        {
            ArrayList moves = new ArrayList();
            ArrayList frames = new ArrayList();
            XmlTextReader reader = new XmlTextReader(pathTofile);
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode node = xmlDocument.ReadNode(reader);
            xmlDocument.Load(pathTofile);
            /*
            XmlNodeList move = xmlDocument.GetElementsByTagName("move");

            for(int i=0; i < 20; i++)
            {
                //string idNum = move[i].Name;
                string idNum = move[i].Attributes["id"].Value;
                list += idNum;
                list += " ";
            }
            //Debug.Log(list);*/
            foreach (XmlNode movechild in node.ChildNodes)
            {
                if (movechild.Name == "move")
                {
                    if (movechild.HasChildNodes)
                    {
                        moves.Add(movechild);
                        frames = null;
                        //foreach (Xmlnode frame in node.ChildNodes)
                        {
                       //     frames.Add(frame);


                        }
                        moves.Add(frames);
                        //Debug.Log(moves.ToString());
                    }
                }
            }


        }
        else
        {
            Debug.Log("No path providede");
        }

    }
    /*
    void GetImage()
    {
        if(path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        WWW www = new WWW("file:///" + path);
        Image.texture = www.texture;
    }
    */
    public void FindAllItems()
    {
        Debug.Log("Finding all items");

        XmlNodeList items = itemDataXml.SelectNodes("/InventoryItems/InventoryItem");

        foreach (XmlNode item in items)
        {
            SpawnInventoryItem(item);
        }

        ShowInventoryItems();
    }

    public void FindItemsOfType(string itemType)
    {
        Debug.Log("Finding all items of type: " + itemType);

        XmlNodeList items = itemDataXml.SelectNodes("/InventoryItems/InventoryItem[@Type='" + itemType + "']");

        foreach (XmlNode item in items)
        {
            SpawnInventoryItem(item);
        }

        ShowInventoryItems();
    }

    public void FindItemsWithID(string itemID)
    {
        XmlNode curNode = itemDataXml.SelectSingleNode("/InventoryItems/InventoryItem[@ID='" + itemID + "']");
        if (curNode == null)
        {
            Debug.LogError("Error could not find Inventory Item with ID: " + itemID + " in IeventoryItemData.xml");
            return;
        }

        SpawnInventoryItem(curNode);
        ShowInventoryItems();
    }

    void ShowInventoryItems()
    {
        buttonsContainerGO.SetActive(false);
        inventoryScreenGO.SetActive(true);
    }

    void SpawnInventoryItem(XmlNode item)
    {
        Debug.Log("Spawning Inventory Item");

        GameObject newItemUI = GameObject.Instantiate(itemUIPrefab, inventoryContainer);

        InventoryItem newInventoryItem = new InventoryItem(item);
        newInventoryItem.UpdateInventoryUI(newItemUI);
    }

    public void OnButtonBack()
    {
        foreach (Transform t in inventoryContainer)
        {
            Destroy(t.gameObject);
        }

        inventoryScreenGO.SetActive(false);
        buttonsContainerGO.SetActive(true);
    }

    class InventoryItem
    {
        public string itemID { get; private set; }
        public string itemType { get; private set; }
        public string itemTitle { get; private set; }
        public string itemDescription { get; private set; }
        public Color bgColor { get; private set; }
        public Texture itemImage { get; private set; }

        public InventoryItem(XmlNode curItemNode)
        {
            itemID = curItemNode.Attributes["ID"].Value;
            itemType = curItemNode.Attributes["Type"].Value;
            itemTitle = curItemNode["ItemTitle"].InnerText;
            itemDescription = curItemNode["ItemDesc"].InnerText;

            XmlNode colorNode = curItemNode.SelectSingleNode("Color");

            float bgR = float.Parse(colorNode["r"].InnerText);
            float bgG = float.Parse(colorNode["g"].InnerText);
            float bgB = float.Parse(colorNode["b"].InnerText);
            float bgA = float.Parse(colorNode["a"].InnerText);

            bgR = NormalizeColorValue(bgR);
            bgG = NormalizeColorValue(bgG);
            bgB = NormalizeColorValue(bgB);
            bgA = NormalizeColorValue(bgA);

            Debug.Log("Color of " + itemTitle + " is " + bgR + ", " + bgG + ", " + bgB + ", " + bgA);

            bgColor = new Color(bgR, bgG, bgB, bgA);


            string pathToImage = "InventoryIcons/" + curItemNode["Image"].InnerText;

            itemImage = Resources.Load<Texture2D>(pathToImage);
        }

        float NormalizeColorValue(float value)
        {
            value = value / 255f;
            return value;
        }

        public void UpdateInventoryUI(GameObject inventoryUI)
        {
            Transform inventoryUITransform = inventoryUI.transform;

            Image itemBGPanel;
            RawImage itemRawImage;
            Text itemTitleText;
            Text itemDescriptionText;

            itemBGPanel = inventoryUITransform.GetComponent<Image>();
            Transform itemBGPanelTransform = itemBGPanel.GetComponent<Transform>();
            itemRawImage = itemBGPanelTransform.Find("ItemRawImage").GetComponent<RawImage>();
            itemTitleText = itemBGPanelTransform.Find("ItemTitleText").GetComponent<Text>();
            itemDescriptionText = itemBGPanelTransform.Find("ItemDescriptionText").GetComponent<Text>();

            if (itemBGPanel == null)
            {
                Debug.LogError("Error could not find itemBGPanel on inventoryUITransform: " + inventoryUITransform.gameObject.name);
            }
            if (itemRawImage == null)
            {
                Debug.LogError("Error could not find itemRawImage on inventoryUITransform: " + inventoryUITransform.gameObject.name);
            }
            if (itemTitleText == null)
            {
                Debug.LogError("Error could not find itemTitleText on inventoryUITransform: " + inventoryUITransform.gameObject.name);
            }
            if (itemDescriptionText == null)
            {
                Debug.LogError("Error could not find itemDescriptionText on inventoryUITransform: " + inventoryUITransform.gameObject.name);
            }

            itemBGPanel.color = bgColor;
            itemRawImage.texture = itemImage;
            itemTitleText.text = itemTitle;
            itemDescriptionText.text = itemDescription;
        }
    }
}
