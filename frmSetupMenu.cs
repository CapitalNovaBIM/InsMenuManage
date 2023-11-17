using Autodesk.Revit.UI;
using Autodesk.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramManageMenu
{
    public partial class frmSetupMenu : Form
    {
        private static Dictionary<string, bool> TreeInfo = null;

        private static bool TreeChecked = true;

        private const string FileMenu = "InsMenuAddin.mnu";

        private const string PREFIX_MENU = "ВКЛАДКА: ";

        private const string PREFIX_PANEL = "ПАНЕЛЬ: ";

        private const string SEP_TYP_NAM = "@";

        private const string PREF_ID = "CustomCtrl_";

        private const string SEPAR_ID = "%";

        private const string C_REP = " ";

        private const char C_SEP = '|';

        private const string T_BUT = "BUTTON";

        private const string T_CBO = "COMBO_BEG";

        private const string T_CBE = "COMBO_END";

        private const string T_GUI = "GUID";

        private const string T_ITM = "ITEM";

        private const string T_MNU = "MENU";

        private const string T_PAN = "PANEL";

        private const string T_PSB = "SPLITBUT_BEG";

        private const string T_PSE = "SPLITBUT_END";

        private const string T_STB = "STACKED_BEG";

        private const string T_STE = "STACKED_END";

        private const string T_TLB = "TOGGLE_BEG";

        private const string T_TLE = "TOGGLE_END";

        private const string T_SEP = "SEPARATOR";

        private const string T_SLD = "SLIDEOUT";

        private const string T_PDB = "PULLDOWN_BEG";

        private const string T_PDE = "PULLDOWN_END";

        private const string T_PTH = "PTH";

        private const string T_REM = "REM";

        private const string T_TXT = "TEXTBOX";

        private const string P_CHM = "CHM";

        private const string P_CMD = "CMD";

        private const string P_DLL = "DLL";

        private const string P_ENA = "ENA";

        private const string P_GRP = "GRP";

        private const string P_HLP = "HLP";

        private const string P_ICO = "ICO";

        private const string P_IMG = "IMG";

        private const string P_NAM = "NAM";

        private const string P_TIP = "TIP";

        private const string P_TXT = "TXT";

        private const string P_TYP = "TYP";

        private const string P_VIS = "VIS";

        private const string P_URL = "URL";

        private const string RegisterPath = "SOFTWARE\\INSYS-AddIns\\Saved Menu Settings\\";

        private ExternalCommandData ExtCmd;

        private static Dictionary<string, bool> Checkeds;

        private bool ModeChild = false;

        private bool Restore = true;

        private TreeView treView;

        private Button butCancel;

        private Button butSave;

        private CheckBox chkChild;

        private Button butShow;

        private ToolTip toolTip1;

        public frmSetupMenu()
        {
            InitializeComponent();
        }
        public frmSetupMenu(ExternalCommandData commandData)
        {
            //IL_004f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0054: Unknown result type (might be due to invalid IL or missing references)
            InitializeComponent();
            ExtCmd = commandData;
            int num = 0;
            string text = "";
            string text2 = "@";
            string text3 = "";
            TreeNode treeNode = null;
            TreeNode treeNode2 = null;
            TreeNode treeNode3 = null;
            TreeNode treeNode4 = null;
            string currentUserAddinsLocation = commandData.Application.Application.CurrentUserAddinsLocation;
            string[] files = Directory.GetFiles(currentUserAddinsLocation, "InsMenuAddin.mnu", SearchOption.AllDirectories);
            string[] array = files;
            foreach (string path in array)
            {
                string directoryName = Path.GetDirectoryName(path);
                if (directoryName != currentUserAddinsLocation)
                {
                    directoryName = Path.GetDirectoryName(path);
                }
                List<string> dat = ReadMenu(Path.Combine(directoryName, "InsMenuAddin.mnu"));
                List<Dictionary<string, object>> list = Parsing(dat);
                for (int j = 0; j < list.Count; j++)
                {
                    Dictionary<string, object> dictionary = list.ElementAt(j);
                    string text4 = dictionary["TYP"].ToString();
                    if (text4 == "GUID")
                    {
                        string tag = dictionary["NAM"].ToString();
                        treView.Tag = tag;
                    }
                    else if (text4 == "MENU")
                    {
                        text3 = ItemText(dictionary["NAM"].ToString(), " ");
                        treeNode3 = treView.Nodes.Add("ВКЛАДКА: " + text3);
                        treeNode3.Tag = text4 + text2 + text3 + text2 + text3;
                        if (text3.Length == 0)
                        {
                            break;
                        }
                        num = 0;
                    }
                    else
                    {
                        text = dictionary["NAM"].ToString();
                        string text5 = "ПАНЕЛЬ: " + ItemText(dictionary["TXT"].ToString(), " ");
                        treeNode4 = treeNode3.Nodes.Add(text5);
                        treeNode4.Tag = text4 + text2 + text + text2 + "CustomCtrl_%" + text3 + "%" + text;
                        Dictionary<string, object>.Enumerator enumerator = dictionary.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            KeyValuePair<string, object> current = enumerator.Current;
                            if (current.Value.GetType().Equals(dictionary.GetType()))
                            {
                                current = enumerator.Current;
                                Dictionary<string, object> dictionary2 = current.Value as Dictionary<string, object>;
                                if (dictionary2.ContainsKey("TYP"))
                                {
                                    string text6 = dictionary2["NAM"].ToString();
                                    text4 = dictionary2["TYP"].ToString();
                                    Dictionary<string, object>.Enumerator enumerator2;
                                    switch (text4)
                                    {
                                        case "BUTTON":
                                            treeNode = treeNode4.Nodes.Add("КНОПКА: " + ItemText(dictionary2["TXT"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            break;
                                        case "PULLDOWN_BEG":
                                            treeNode = treeNode4.Nodes.Add("ВЫПАДАЮЩИЕ КНОПКИ: " + ItemText(dictionary2["TXT"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            break;
                                        case "SPLITBUT_BEG":
                                            treeNode = treeNode4.Nodes.Add("СПИСОК КНОПОК: " + ItemText(dictionary2["TXT"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            break;
                                        case "COMBO_BEG":
                                            treeNode = treeNode4.Nodes.Add("ВЫПАДАЮЩИЙ СПИСОК: " + ItemText(dictionary2["TXT"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            break;
                                        case "STACKED_BEG":
                                            enumerator2 = dictionary2.GetEnumerator();
                                            while (enumerator2.MoveNext())
                                            {
                                                current = enumerator2.Current;
                                                if (current.Value.GetType().Equals(dictionary2.GetType()))
                                                {
                                                    current = enumerator2.Current;
                                                    Dictionary<string, object> dictionary3 = current.Value as Dictionary<string, object>;
                                                    if (dictionary3.ContainsKey("TYP"))
                                                    {
                                                        text5 = dictionary3["NAM"].ToString();
                                                        text4 = dictionary3["TYP"].ToString();
                                                        switch (text4)
                                                        {
                                                            case "BUTTON":
                                                                treeNode = treeNode4.Nodes.Add("КНОПКА: " + ItemText(dictionary3["TXT"].ToString(), " "));
                                                                treeNode.Tag = text4 + text2 + text5 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text5;
                                                                break;
                                                            case "COMBO_BEG":
                                                                treeNode = treeNode4.Nodes.Add("ВЫПАДАЮЩИЙ СПИСОК: " + ItemText(dictionary3["TXT"].ToString(), " "));
                                                                treeNode.Tag = text4 + text2 + text5 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text5;
                                                                break;
                                                            case "TEXTBOX":
                                                                treeNode = treeNode4.Nodes.Add("ТЕКСТОВОЕ ПОЛЕ: " + ItemText(dictionary3["TXT"].ToString(), " "));
                                                                treeNode.Tag = text4 + text2 + text5 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text5;
                                                                break;
                                                            case "PULLDOWN_BEG":
                                                                treeNode = treeNode4.Nodes.Add("ВЫПАДАЮЩИЕ КНОПКИ: " + ItemText(dictionary3["TXT"].ToString(), " "));
                                                                treeNode.Tag = text4 + text2 + text5 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text5;
                                                                break;
                                                            case "SPLITBUT_BEG":
                                                                treeNode = treeNode4.Nodes.Add("СПИСОК КНОПОК: " + ItemText(dictionary3["TXT"].ToString(), " "));
                                                                treeNode.Tag = text4 + text2 + text5 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text5;
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case "TOGGLE_BEG":
                                            treeNode = treeNode4.Nodes.Add("ГРУППА ВЫБОРА: " + ItemText(dictionary2["TXT"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            enumerator2 = dictionary2.GetEnumerator();
                                            while (enumerator2.MoveNext())
                                            {
                                                current = enumerator2.Current;
                                                if (current.Value.GetType().Equals(dictionary2.GetType()))
                                                {
                                                    current = enumerator2.Current;
                                                    Dictionary<string, object> dictionary3 = current.Value as Dictionary<string, object>;
                                                    if (dictionary3.ContainsKey("TYP"))
                                                    {
                                                        text5 = dictionary3["NAM"].ToString();
                                                        text4 = dictionary3["TYP"].ToString();
                                                        if (text4 == "BUTTON")
                                                        {
                                                            treeNode2 = treeNode.Nodes.Add("КНОПКА: " + ItemText(dictionary3["TXT"].ToString(), " "));
                                                            treeNode2.Tag = text4 + text2 + text5 + text2 + "CustomCtrl_%CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6 + "%" + text5;
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case "TEXTBOX":
                                            treeNode = treeNode4.Nodes.Add(ItemText("ТЕКСТОВОЕ ПОЛЕ: " + dictionary2["TXT"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            break;
                                        case "SLIDEOUT":
                                            treeNode = treeNode4.Nodes.Add("ДОПОЛ. ПАНЕЛЬ: " + ItemText(dictionary2["NAM"].ToString(), " "));
                                            treeNode.Tag = text4 + text2 + text6 + text2 + "CustomCtrl_%CustomCtrl_%" + text3 + "%" + text + "%" + text6;
                                            num++;
                                            break;
                                    }
                                }
                            }
                        }
                        if (--num < 0)
                        {
                            num = 0;
                        }
                    }
                }
                list = null;
                dat = null;
            }
            ModeChild = true;
            treView.CheckBoxes = true;
            chkChild.Checked = true;
            if (TreeInfo == null)
            {
                SetVisible(treView);
            }
            else
            {
                ReadChecked(treView, TreeInfo);
            }
            Checkeds = SaveChecked(treView);
            chkChild.Checked = TreeChecked;
        }

        private void ReadChecked(TreeView tre, Dictionary<string, bool> chk)
        {
            if (chk != null)
            {
                foreach (TreeNode node in tre.Nodes)
                {
                    SetChecked(node, chk);
                }
            }
        }

        private void SetChecked(TreeNode nod, Dictionary<string, bool> dic)
        {
            string fullPath = nod.FullPath;
            if (dic.Keys.Contains(fullPath))
            {
                nod.Checked = dic[fullPath];
                foreach (TreeNode node in nod.Nodes)
                {
                    SetChecked(node, dic);
                }
            }
        }

        private Dictionary<string, bool> SaveChecked(TreeView tre)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            foreach (TreeNode node in tre.Nodes)
            {
                ListChecked(node, ref dic);
            }
            return dic;
        }

        private void ListChecked(TreeNode nod, ref Dictionary<string, bool> dic)
        {
            if (!dic.ContainsKey(nod.FullPath))
            { dic.Add(nod.FullPath, nod.Checked); }
            else
            { dic[nod.FullPath] = nod.Checked; }
            foreach (TreeNode node in nod.Nodes)
            {
                ListChecked(node, ref dic);
            }
        }

        private void SetVisible(TreeView tre)
        {
            foreach (TreeNode node in tre.Nodes)
            {
                node.Checked = true;
            }
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            using (RegistryKey registryKey2 = registryKey.CreateSubKey("SOFTWARE\\INSYS-AddIns\\Saved Menu Settings\\"))
            {
                string[] array = registryKey2.GetValue("InsMenuAddin.mnu_Revit_" + tre.Tag, "Invisible Items").ToString().Split('|');
                for (int i = 1; i < array.Length; i++)
                {
                    array[i] = array[i].Trim();
                }
                TreeNodeCollection nodes = tre.Nodes;
                foreach (TreeNode item in nodes)
                {
                    NodeVisible(item, array);
                }
            }
            registryKey.Close();
        }

        private void NodeVisible(TreeNode nod, string[] vis)
        {
            string strA = nod.Tag.ToString();
            for (int i = 1; i < vis.Length; i++)
            {
                if (string.Compare(strA, vis[i], true) == 0)
                {
                    nod.Checked = false;
                }
            }
            foreach (TreeNode node in nod.Nodes)
            {
                NodeVisible(node, vis);
            }
        }

        private string ItemText(string txt, string chr)
        {
            return txt.Replace("\r", chr);
        }

        private void treView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SetCheckedNodes(e.Node);
        }

        private void treView_DoubleClick(object sender, EventArgs e)
        {
        }

        private void SetCheckedNodes(TreeNode nod)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000d: Expected O, but got Unknown
            //IL_0090: Unknown result type (might be due to invalid IL or missing references)
            //IL_0097: Expected O, but got Unknown
            //IL_00b2: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b9: Expected O, but got Unknown
            //IL_00c7: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ce: Expected O, but got Unknown
            //IL_00e7: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ec: Expected O, but got Unknown
            //IL_0116: Unknown result type (might be due to invalid IL or missing references)
            //IL_011b: Expected O, but got Unknown
            //IL_013c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0141: Expected O, but got Unknown
            //IL_01fc: Unknown result type (might be due to invalid IL or missing references)
            //IL_0201: Expected O, but got Unknown
            bool @checked = nod.Checked;
            RibbonControl val = ComponentManager.Ribbon;
            nod.Expand();
            if (ModeChild && val.IsMainRibbon)
            {
                string[] array = nod.Tag.ToString().Split("@"[0]);
                if (array.GetUpperBound(0) > 1)
                {
                    if (array[0] == "MENU")
                    {
                        for (int i = 0; i < ((Collection<RibbonTab>)val.Tabs).Count; i++)
                        {
                            if (((Collection<RibbonTab>)val.Tabs)[i].Id == array[2])
                            {
                                ((Collection<RibbonTab>)val.Tabs)[i].IsVisible = @checked;
                                ((Collection<RibbonTab>)val.Tabs)[i].IsActive = @checked;
                                break;
                            }
                        }
                    }
                    else if (array[0] == "PANEL")
                    {
                        Autodesk.Windows.RibbonPanel val2 = val.FindPanel(array[2], false);                        
                        if (val2 != null)
                        {
                            val2.IsVisible = @checked;
                        }
                    }
                    else
                    {
                        Autodesk.Windows.RibbonItem val3 = val.FindItem(array[2], false, true);
                        if (val3 != null)
                        {
                            val3.IsVisible = @checked;
                        }
                    }
                }
                if (chkChild.Checked)
                {
                    foreach (TreeNode node in nod.Nodes)
                    {
                        node.Checked = nod.Checked;
                        TreeNode treeNode2 = RootNode(node);
                        RibbonTab ribbonTab = GetRibbonTab(treeNode2.Text);
                        if (ribbonTab != null)
                        {
                            array = node.Tag.ToString().Split("@"[0]);
                            if (array.GetUpperBound(0) > 1)
                            {
                                Autodesk.Windows.RibbonItem val3 = val.FindItem(array[2], false);
                                if (val3 != null)
                                {
                                    val3.IsVisible = @checked;
                                }
                            }
                        }
                        if (node.Nodes.Count > 0)
                        {
                            SetCheckedNodes(node);
                        }
                    }
                }
            }
        }

        private void treView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private List<string> ReadMenu(string nam)
        {
            int length = "REM".Length;
            string text = "";
            List<string> list = new List<string>();
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(nam, Encoding.Default);
                while (true)
                {
                    text = streamReader.ReadLine();
                    if (text == null)
                    {
                        break;
                    }
                    text = text.Trim().Replace('\t', ' ');
                    if (text.Length > 0)
                    {
                        if (text.Substring(0, length).ToUpper() != "REM")
                        {
                            list.Add(text);
                        }
                        else if (text.Length > length && text[length] != ' ')
                        {
                            list.Add(text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message("Ошибка при чтении файла описания меню!", ex.Message);
                list = null;
            }
            streamReader?.Close();
            return list;
        }

        private List<Dictionary<string, object>> Parsing(List<string> dat)
        {
            int num = 0;
            int num2 = 0;
            Dictionary<string, object> dictionary = null;
            Dictionary<string, object> dictionary2 = null;
            Dictionary<string, object> dictionary3 = null;
            Dictionary<string, object> dictionary4 = null;
            Dictionary<string, object> dictionary5 = null;
            Dictionary<string, object> dictionary6 = null;
            Dictionary<string, object> dictionary7 = null;
            Dictionary<string, object> dictionary8 = null;
            bool flag = false;
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            foreach (string item in dat)
            {
                string[] array = item.Split('|');
                int upperBound = array.GetUpperBound(0);
                for (int i = 0; i <= upperBound; i++)
                {
                    array[i] = array[i].Trim();
                }
                string text = array[0].ToUpper();
                if (text == "SEPARATOR")
                {
                    int num3;
                    if (dictionary != null)
                    {
                        string str = text;
                        num3 = num + 1;
                        num = num3;
                        string text2 = str + num3.ToString();
                        Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                        dictionary9.Add("TYP", text);
                        dictionary9.Add("NAM", text2);
                        dictionary.Add(text2, dictionary9);
                        dictionary9 = null;
                    }
                    else if (dictionary6 != null)
                    {
                        string str2 = text;
                        num3 = num + 1;
                        num = num3;
                        string text2 = str2 + num3.ToString();
                        Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                        dictionary9.Add("TYP", text);
                        dictionary9.Add("NAM", text2);
                        dictionary6.Add(text2, dictionary9);
                        dictionary9 = null;
                    }
                    else if (dictionary7 != null)
                    {
                        string str3 = text;
                        num3 = num + 1;
                        num = num3;
                        string text2 = str3 + num3.ToString();
                        Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                        dictionary9.Add("TYP", text);
                        dictionary9.Add("NAM", text2);
                        dictionary7.Add(text2, dictionary9);
                        dictionary9 = null;
                    }
                    else
                    {
                        if (dictionary8 != null)
                        {
                            Message("Ошибка " + text, "В описании контейнера элементов Stacked недопустимо использование разделителя!");
                            break;
                        }
                        if (dictionary3 != null)
                        {
                            Message("Ошибка " + text, "Разделитель не может быть использован в группе RadioButton!");
                            break;
                        }
                        if (dictionary2 == null)
                        {
                            Message("Ошибка", "[" + text + "] Разделитель не может стоять до определения группы элементов с помощью тега PANEL !");
                            break;
                        }
                        string str4 = text;
                        num3 = num + 1;
                        num = num3;
                        string text2 = str4 + num3.ToString();
                        Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                        dictionary9.Add("TYP", text);
                        dictionary9.Add("NAM", text2);
                        dictionary2.Add(text2, dictionary9);
                        dictionary9 = null;
                    }
                }
                else if (text == "GUID")
                {
                    if (flag)
                    {
                        Message("Ошибка " + text, "Дублирование GUID в описании файла меню!");
                        break;
                    }
                    if (upperBound == 0)
                    {
                        Message("Ошибка " + text, "В параметре GUID описания файла меню не задано его значение!");
                        break;
                    }
                    if (upperBound > 1)
                    {
                        Message("Ошибка " + text, "Лишние параметры в описателе GUID файла меню InsMenuAddin.mnu!");
                        break;
                    }
                    string text2 = array[1];
                    if (list2.Count > 0)
                    {
                        Message("Ошибка " + text, "Описатель GUID должен быть первым параметром в файле меню!");
                        break;
                    }
                    Guid result;
                    if (!Guid.TryParseExact(text2, "N", out result))
                    {
                        Message("Ошибка " + text, "Недопустимый формат параметра GUID в описании файла меню:\n" + text2);
                        break;
                    }
                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                    dictionary9.Add("TYP", text);
                    dictionary9.Add("NAM", result.ToString());
                    list2.Add(dictionary9);
                    dictionary9 = null;
                    flag = true;
                }
                else
                {
                    if (text != "STACKED_BEG" && text != "STACKED_END" && text != "PULLDOWN_END" && text != "SPLITBUT_END" && text != "COMBO_END" && upperBound < 2)
                    {
                        if (!(text == "TOGGLE_BEG") && !(text == "SLIDEOUT"))
                        {
                            if (!(text == "TOGGLE_END"))
                            {
                                Message("Описатель [" + text + "]", "В строке описания меню должно быть не менее 3-х параметров:\n" + item);
                                break;
                            }
                            dictionary3 = null;
                            continue;
                        }
                        if (upperBound < 1)
                        {
                            Message("Описатель [" + text + "]", "В строке описания RadioButtons должно быть не менее 2-х параметров:\n" + item);
                            break;
                        }
                    }
                    string text2 = (upperBound > 0) ? array[1] : "";
                    string text3 = (upperBound > 1) ? array[2] : "";
                    text3 = text3.Replace("\\r", "\r");
                    string value = (upperBound > 2) ? array[3] : "true";
                    string value2 = (upperBound > 3) ? array[4] : "true";
                    string value3 = (upperBound > 4) ? array[5] : "";
                    string value4 = (upperBound > 5) ? array[6] : "";
                    string value5 = (upperBound > 6) ? array[7] : "";
                    string value6 = (upperBound > 7) ? array[8] : "";
                    string text4 = (upperBound > 8) ? array[9] : "";
                    text4 = text4.Replace("\\r", "\r");
                    string value7 = (upperBound > 9) ? array[10] : "";
                    string text5;
                    if (upperBound > 10)
                    {
                        text5 = array[11];
                        if (Path.GetExtension(text5).Length == 0)
                        {
                            text5 = Path.ChangeExtension(text5, ".chm");
                        }
                    }
                    else
                    {
                        text5 = "";
                    }
                    string value8 = (upperBound > 11) ? array[12] : "";
                    if (text2 == "" && text != "PULLDOWN_END" && text != "SPLITBUT_END" && text != "STACKED_END" && text != "COMBO_END")
                    {
                        Message("Описатель [" + text + "]", "Не указано имя элемента в строке:\n" + item);
                    }
                    else if (text2.Length > 0)
                    {
                        if (list.Contains(text2))
                        {
                            Message("Описатель [" + text + "]", "Дублирование имени элемента меню:\n" + text2 + "\nв строке:\n" + item);
                        }
                        else
                        {
                            list.Add(text2);
                        }
                    }
                    if (text == "MENU")
                    {
                        dictionary = null;
                        dictionary3 = null;
                        dictionary6 = null;
                        dictionary7 = null;
                        dictionary5 = null;
                        Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                        dictionary9.Add("TYP", text);
                        dictionary9.Add("NAM", text3);
                        dictionary9.Add("TXT", text2);
                        dictionary9.Add("VIS", value);
                        dictionary9.Add("ENA", value2);
                        if (upperBound > 0)
                        {
                            list2.Add(dictionary9);
                        }
                        dictionary9 = null;
                    }
                    else if (list2.Count == 0)
                    {
                        Message("Ошибка", "[" + text + "] Не определено название меню или оно стоит после определения панели и ее параметров!");
                    }
                    else
                    {
                        switch (text)
                        {
                            case "PANEL":
                                {
                                    dictionary = null;
                                    dictionary3 = null;
                                    dictionary6 = null;
                                    dictionary7 = null;
                                    dictionary5 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    if (upperBound > 0)
                                    {
                                        list2.Add(dictionary9);
                                        dictionary2 = list2.Last();
                                    }
                                    else
                                    {
                                        dictionary2 = null;
                                    }
                                    dictionary9 = null;
                                    break;
                                }
                            case "SLIDEOUT":
                                dictionary = null;
                                dictionary3 = null;
                                dictionary6 = null;
                                dictionary7 = null;
                                dictionary8 = null;
                                if (dictionary2 == null)
                                {
                                    Message("Описатель " + text, "Не определена группа элементов с помощью тега PANEL, к которой надо добавить выпадающую панель элементов SLIDEOUT с именем: " + text2);
                                }
                                else if (dictionary5 == null)
                                {
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary2.Add(text2, dictionary9);
                                    dictionary5 = (dictionary2[text2] as Dictionary<string, object>);
                                    dictionary9 = null;
                                }
                                else
                                {
                                    Message("Описатель " + text, "Повторное использование тега SLIDEOUT для панели меню: " + dictionary2["NAM"]);
                                }
                                break;
                            case "SPLITBUT_BEG":
                                if (dictionary7 != null)
                                {
                                    Message("Описатель " + text, "Ранее не завершено описание для аналогичного тега SPLITBUT_BEG");
                                }
                                else
                                {
                                    dictionary = null;
                                    dictionary3 = null;
                                    dictionary6 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("TIP", value6);
                                    dictionary9.Add("HLP", text4);
                                    dictionary9.Add("CHM", text5);
                                    dictionary9.Add("URL", value8);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    if (dictionary8 != null)
                                    {
                                        if (dictionary8.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary8.Add(text2, dictionary9);
                                            num2++;
                                            dictionary7 = (dictionary8[text2] as Dictionary<string, object>);
                                        }
                                    }
                                    else
                                    {
                                        dictionary2.Add(text2, dictionary9);
                                        dictionary7 = (dictionary2[text2] as Dictionary<string, object>);
                                    }
                                    dictionary9 = null;
                                }
                                break;
                            case "SPLITBUT_END":
                                if (dictionary7 == null)
                                {
                                    Message("Описатель " + text, "Нет тега SPLITBUT_BEG, определяющего начало набора кнопок SplitButtons!");
                                }
                                else
                                {
                                    dictionary7 = null;
                                }
                                break;
                            case "BUTTON":
                                {
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("DLL", value3);
                                    dictionary9.Add("CMD", value4);
                                    dictionary9.Add("ICO", value5);
                                    dictionary9.Add("TIP", value6);
                                    dictionary9.Add("IMG", value7);
                                    dictionary9.Add("HLP", text4);
                                    dictionary9.Add("CHM", text5);
                                    dictionary9.Add("URL", value8);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    if (dictionary6 != null)
                                    {
                                        if (dictionary6.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary6.Add(text2, dictionary9);
                                        }
                                    }
                                    else if (dictionary7 != null)
                                    {
                                        if (dictionary7.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary7.Add(text2, dictionary9);
                                        }
                                    }
                                    else if (dictionary8 != null)
                                    {
                                        if (dictionary8.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary8.Add(text2, dictionary9);
                                            num2++;
                                        }
                                    }
                                    else if (dictionary3 != null)
                                    {
                                        if (dictionary3.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary3.Add(text2, dictionary9);
                                        }
                                    }
                                    else if (dictionary2 != null)
                                    {
                                        if (dictionary2.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary2.Add(text2, dictionary9);
                                        }
                                    }
                                    else
                                    {
                                        Message("Описатель " + text, "Не определена группа PANEL, к которой добавить описатель!");
                                    }
                                    dictionary9 = null;
                                    break;
                                }
                            case "STACKED_BEG":
                                if (dictionary8 != null)
                                {
                                    Message("Описатель " + text, "Ранее не завершено описание для аналогичного тега STACKED_BEG");
                                }
                                else
                                {
                                    dictionary = null;
                                    dictionary3 = null;
                                    dictionary6 = null;
                                    dictionary7 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    dictionary2.Add(text2, dictionary9);
                                    dictionary8 = (dictionary2[text2] as Dictionary<string, object>);
                                    dictionary9 = null;
                                }
                                break;
                            case "STACKED_END":
                                if (dictionary8 == null)
                                {
                                    Message("Описатель " + text, "Нет тега STACKED_BEG, определяющего начало описания контейнера Stacked!");
                                }
                                else if (num2 < 2)
                                {
                                    Message("Описатель " + text, "Контейнер Stacked содержит менее 2-х элементов!");
                                }
                                else
                                {
                                    num2 = 0;
                                    dictionary = null;
                                    dictionary8 = null;
                                }
                                break;
                            case "COMBO_BEG":
                                if (dictionary != null)
                                {
                                    Message("Описатель " + text, "Ранее не завершено описание для аналогичного тега COMBO_BEG");
                                }
                                else
                                {
                                    dictionary3 = null;
                                    dictionary6 = null;
                                    dictionary7 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("CHM", text5);
                                    dictionary9.Add("URL", value8);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    if (dictionary8 != null)
                                    {
                                        if (dictionary8.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary8.Add(text2, dictionary9);
                                            num2++;
                                            dictionary = (dictionary8[text2] as Dictionary<string, object>);
                                        }
                                    }
                                    else
                                    {
                                        dictionary2.Add(text2, dictionary9);
                                        dictionary = (dictionary2[text2] as Dictionary<string, object>);
                                    }
                                    dictionary9 = null;
                                }
                                break;
                            case "COMBO_END":
                                if (dictionary == null)
                                {
                                    Message("Описатель " + text, "Нет тега COMBO_BEG, определяющего начало описания элементов ComboBox!");
                                }
                                else
                                {
                                    dictionary = null;
                                }
                                break;
                            case "ITEM":
                                if (dictionary == null)
                                {
                                    Message("Описатель " + text, "Нет тега COMBO_BEG, определяющего начало списка ComboBox!");
                                }
                                else
                                {
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("GRP", value3);
                                    dictionary9.Add("CMD", value4);
                                    dictionary9.Add("ICO", value5);
                                    dictionary9.Add("TIP", value6);
                                    dictionary9.Add("HLP", text4);
                                    dictionary9.Add("CHM", text5);
                                    dictionary9.Add("URL", value8);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    dictionary.Add(text2, dictionary9);
                                    dictionary4 = (dictionary[text2] as Dictionary<string, object>);
                                    dictionary9 = null;
                                }
                                break;
                            case "TOGGLE_BEG":
                                if (dictionary3 != null)
                                {
                                    Message("Описатель " + text, "Ранее не завершено описание для аналогичного тега TOGGLE_BEG");
                                }
                                else
                                {
                                    dictionary = null;
                                    dictionary6 = null;
                                    dictionary7 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", (text3.Length == 0) ? text2 : text3);
                                    dictionary9.Add("ICO", value5);
                                    dictionary9.Add("TIP", value6);
                                    dictionary9.Add("HLP", text4);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    dictionary2.Add(text2, dictionary9);
                                    dictionary3 = (dictionary2[text2] as Dictionary<string, object>);
                                    dictionary9 = null;
                                }
                                break;
                            case "TOGGLE_END":
                                if (dictionary3 == null)
                                {
                                    Message("Описатель " + text, "Нет тега TOGGLE_BEG, определяющего начало описания опциональных кнопок ToggleButtons!");
                                }
                                else
                                {
                                    dictionary3 = null;
                                }
                                break;
                            case "PULLDOWN_BEG":
                                if (dictionary6 != null)
                                {
                                    Message("Описатель " + text, "Ранее не завершено описание для аналогичного тега PULLDOWN_BEG");
                                }
                                else
                                {
                                    dictionary = null;
                                    dictionary3 = null;
                                    dictionary7 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("ICO", value5);
                                    dictionary9.Add("TIP", value6);
                                    dictionary9.Add("HLP", text4);
                                    dictionary9.Add("CHM", text5);
                                    dictionary9.Add("URL", value8);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    if (dictionary8 != null)
                                    {
                                        if (dictionary8.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary8.Add(text2, dictionary9);
                                            num2++;
                                            dictionary6 = (dictionary8[text2] as Dictionary<string, object>);
                                        }
                                    }
                                    else
                                    {
                                        dictionary2.Add(text2, dictionary9);
                                        dictionary6 = (dictionary2[text2] as Dictionary<string, object>);
                                    }
                                    dictionary9 = null;
                                }
                                break;
                            case "PULLDOWN_END":
                                if (dictionary6 == null)
                                {
                                    Message("Описатель " + text, "Нет тега PULLDOWN_BEG, определяющего начало списка кнопок PullDownButtons!");
                                }
                                else
                                {
                                    dictionary6 = null;
                                }
                                break;
                            case "TEXTBOX":
                                {
                                    dictionary = null;
                                    dictionary3 = null;
                                    dictionary6 = null;
                                    dictionary7 = null;
                                    Dictionary<string, object> dictionary9 = new Dictionary<string, object>();
                                    dictionary9.Add("TYP", text);
                                    dictionary9.Add("NAM", text2);
                                    dictionary9.Add("TXT", text3);
                                    dictionary9.Add("DLL", value3);
                                    dictionary9.Add("CMD", value4);
                                    dictionary9.Add("ICO", value5);
                                    dictionary9.Add("TIP", value6);
                                    dictionary9.Add("HLP", text4);
                                    dictionary9.Add("CHM", text5);
                                    dictionary9.Add("URL", value8);
                                    dictionary9.Add("VIS", value);
                                    dictionary9.Add("ENA", value2);
                                    if (dictionary8 != null)
                                    {
                                        if (dictionary8.ContainsKey(text2))
                                        {
                                            Message("Описатель " + text, "Ключ " + text2 + " уже имеется!");
                                        }
                                        else if (upperBound > 0)
                                        {
                                            dictionary8.Add(text2, dictionary9);
                                            num2++;
                                        }
                                    }
                                    else
                                    {
                                        dictionary2.Add(text2, dictionary9);
                                        dictionary4 = (dictionary2[text2] as Dictionary<string, object>);
                                    }
                                    dictionary9 = null;
                                    break;
                                }
                            default:
                                Message("Ошибка", "Неизвестная сигнатура <" + text + "> в строке:\r" + item);
                                break;
                        }
                        if (num2 > 3)
                        {
                            Message("Ошибка", "В описании контейнера Stacked не может быть более 3-х элементов!");
                            break;
                        }
                    }
                }
            }
            return list2;
        }

        private static void Message(string cap, string txt)
        {
            MessageBox.Show(txt, cap);
        }

        private bool SetupToRegistr(TreeView tre)
        {
            bool result = false;
            List<string> list = new List<string>();
            RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\INSYS-AddIns\\Saved Menu Settings\\", true);
            if (registryKey2 == null)
            {
                registryKey2 = registryKey.CreateSubKey("SOFTWARE\\INSYS-AddIns\\Saved Menu Settings\\");
            }
            if (registryKey2 != null)
            {
                list.Add("Invisible Items");
                foreach (TreeNode node in tre.Nodes)
                {
                    list.AddRange(ListInvisible(node, false));
                }
                string name = "InsMenuAddin.mnu_Revit_" + tre.Tag;
                registryKey2.SetValue(name, string.Join(" | ", list));
                registryKey2.Close();
            }
            registryKey.Close();
            return result;
        }

        private List<string> ListInvisible(TreeNode nod, bool vis)
        {
            List<string> list = new List<string>();
            if (nod.Checked == vis)
            {
                list.Add(nod.Tag.ToString());
            }
            foreach (TreeNode node in nod.Nodes)
            {
                list.AddRange(ListInvisible(node, vis));
            }
            return list;
        }

        private TreeNode RootNode(TreeNode cld)
        {
            TreeNode treeNode = cld;
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        private RibbonTab GetRibbonTab(string nam)
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_000c: Expected O, but got Unknown
            foreach (RibbonTab item in (Collection<RibbonTab>)ComponentManager.Ribbon.Tabs)
            {
                if (item.AutomationName == nam)
                {
                    return item;
                }
            }
            return null;
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            TreeInfo = SaveChecked(treView);
            if (SetupToRegistr(treView))
            {
                MessageBox.Show("Ошибка записи параметров в реестр Windows!", "Сохранение параметров видимости элементов меню Revit");
            }
            else
            {
                MessageBox.Show("Сохранение выполнено!", "Сохранение параметров видимости элементов меню Revit");
            }
            Restore = false;
            Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = RootNode((sender as TreeView).SelectedNode);
            RibbonTab ribbonTab = GetRibbonTab(treeNode.Text.Substring("ВКЛАДКА: ".Length));
            if (ribbonTab != null)
            {
                ribbonTab.IsActive = true;
            }
        }

        private void butShow_Click(object sender, EventArgs e)
        {
            TreeInfo = SaveChecked(treView);
            Restore = false;
            Close();
        }

        private void frmSetupMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Unknown result type (might be due to invalid IL or missing references)
            //IL_0026: Expected O, but got Unknown
            //IL_0043: Unknown result type (might be due to invalid IL or missing references)
            TreeChecked = chkChild.Checked;
            if (Restore)
            {
                RibbonTab val = ComponentManager.Ribbon.ActiveTab;
                ReadChecked(treView, Checkeds);
                if (val.IsVisible)
                {
                    ComponentManager.Ribbon.ActiveTab = val;
                }
            }
        }
    }
}
