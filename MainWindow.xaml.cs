using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace mh4edit;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public string WindowTitle => "MH4UEdit " + Assembly.GetExecutingAssembly().GetName().Version;
    
    public MonHunSave save;
    public MonHunCharacter character;
    public MainWindow()
    {
        InitializeComponent();

        ShowNoneUI();
    }

    private void Button_OpenFileClick(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.ShowHiddenItems = true;
        openFileDialog.Filter = "MH4U Save Files (user*)|user*";
        if(openFileDialog.ShowDialog() == true)
        {
            save = new MonHunSave(openFileDialog.FileName);
            character = save.slot;
            DataContext = character;

            ReloadEquipBoxPage();

            ComboGqQuest.ItemsSource = character.GuildQuests;
            ComboPalPalico.ItemsSource = character.Palicos;
        }
    }

    private void Button_SaveFileClick(object sender, RoutedEventArgs e)
    {
        if(save!=null)
        {
            save.Save();
            MessageBox.Show("Saved to file:\n" + save.FileName, "Save Complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void Button_LbgCInfoClick(object sender, RoutedEventArgs e)
    {
        var dialog = new LbgInfoCDialog(false);
        if (dialog.ShowDialog() == true) {
            var info = dialog.DataMagSel.SelectedItem as LbgCInfo;
            if(info!=null)
            {
                var eqp = character.EquipBox[idxSelectedEquip];
                if(eqp is MonHunWeapon weap)
                    weap.Sharpness = (byte)info.Val;
            }
        }
    }

    private void Button_HbgCInfoClick(object sender, RoutedEventArgs e)
    {
        var dialog = new LbgInfoCDialog(true);
        if (dialog.ShowDialog() == true) {
            var info = dialog.DataMagSel.SelectedItem as LbgCInfo;
            if(info!=null)
            {
                var eqp = character.EquipBox[idxSelectedEquip];
                if(eqp is MonHunWeapon weap)
                    weap.Sharpness = (byte)info.Val;
            }
        }
    }

    private void Button_LbgFInfoClick(object sender, RoutedEventArgs e)
    {
        var dialog = new LbgInfoFDialog();
        if (dialog.ShowDialog() == true) {
            var info = dialog.DataShotSel.SelectedItem as LbgFInfo;
            if(info!=null)
            {
                var eqp = character.EquipBox[idxSelectedEquip];
                if(eqp is MonHunWeapon weap)
                    weap.Special = (byte)info.Val;
            }
        }
    }

    private void Button_HbgFInfoClick(object sender, RoutedEventArgs e)
    {
        var dialog = new HbgInfoFDialog();
        if (dialog.ShowDialog() == true) {
            var info = dialog.DataShotSel.SelectedItem as HbgFInfo;
            if(info!=null)
            {
                var eqp = character.EquipBox[idxSelectedEquip];
                if(eqp is MonHunWeapon weap)
                    weap.Special = (byte)info.Val;
            }
        }
    }

    private void Button_BowCInfoClick(object sender, RoutedEventArgs e)
    {
        var dialog = new BowInfoCDialog(true);
        if (dialog.ShowDialog() == true) {
            var info = dialog.DataCoatingsSel.SelectedItem as BowCInfo;
            if(info!=null)
            {
                var eqp = character.EquipBox[idxSelectedEquip];
                if(eqp is MonHunWeapon weap)
                    weap.Sharpness = (byte)info.Val;
            }
        }
    }

    private void ComboGqQuest_SelectionChanged(object sender, RoutedEventArgs e)
    {
        var combo = sender as ComboBox;
        var gq = combo.SelectedItem as GuildQuest;
        if(gq != null)
        {
            Binding myBinding = new Binding("Owner");
            myBinding.Source = gq;
            TextboxGqOwner.SetBinding(TextBox.TextProperty, myBinding);

            myBinding = new Binding("Id");
            myBinding.Source = gq;
            TextboxGqId.SetBinding(TextBox.TextProperty, myBinding);

            myBinding = new Binding("InitialLevel");
            myBinding.Source = gq;
            TextboxGqInitialLevel.SetBinding(TextBox.TextProperty, myBinding);

            myBinding = new Binding("CurrentLevel");
            myBinding.Source = gq;
            TextboxGqCurrentLevel.SetBinding(TextBox.TextProperty, myBinding);

            ComboGqBiasWeapon.ItemsSource = GuildQuestStatic.WeaponBiasValues;
            myBinding = new Binding("WeaponBias");
            myBinding.Source = gq;
            ComboGqBiasWeapon.SetBinding(ComboBox.SelectedValueProperty, myBinding);

            ComboGqBiasArmorSeries.ItemsSource = GuildQuestStatic.ArmorSeriesBiasValues;
            myBinding = new Binding("ArmorSeriesBias");
            myBinding.Source = gq;
            ComboGqBiasArmorSeries.SetBinding(ComboBox.SelectedValueProperty, myBinding);

            ComboGqBiasArmorType.ItemsSource = GuildQuestStatic.ArmorTypeBiasValues;
            myBinding = new Binding("ArmorTypeBias");
            myBinding.Source = gq;
            ComboGqBiasArmorType.SetBinding(ComboBox.SelectedValueProperty, myBinding);

            ComboGqMonsterAId.ItemsSource = GuildQuestStatic.MonsterAIdValues;
            myBinding = new Binding("MonsterAId");
            myBinding.Source = gq;
            ComboGqMonsterAId.SetBinding(ComboBox.SelectedValueProperty, myBinding);

            ComboGqMonsterBId.ItemsSource = GuildQuestStatic.MonsterBIdValues;
            myBinding = new Binding("MonsterBId");
            myBinding.Source = gq;
            ComboGqMonsterBId.SetBinding(ComboBox.SelectedValueProperty, myBinding);
        }
    }

    private void ComboPalPalico_SelectionChanged(object sender, RoutedEventArgs e)
    {
        var combo = sender as ComboBox;
        var pal = combo.SelectedItem as Palico;
        if(pal != null)
        {
            Binding myBinding = new Binding("Name");
            myBinding.Source = pal;
            TextboxPalName.SetBinding(TextBox.TextProperty, myBinding);

            myBinding = new Binding("Exp");
            myBinding.Source = pal;
            TextboxPalExp.SetBinding(TextBox.TextProperty, myBinding);
        }
    }


    private void Button_SetItemBox99(object sender, RoutedEventArgs e)
    {
        if(save!=null)
        {
            foreach(var item in character.ItemBox)
            {
                if(item.ID == 0)
                    continue;
                item.Count = 99;
            }
        }
    }

    private void Button_RemoveItemBoxDupes(object sender, RoutedEventArgs e)
    {
        if(save!=null)
        {
            List<MonHunItem> items = new List<MonHunItem>();
            for(int i=0; i < character.itemBox.Length; i++)
            {
                var item = character.ItemBox[i];
                if(item.ID == 0 || items.Any(x => x.ID == item.ID))
                    continue;
                
                items.Add(item);
            }

            int idx = 0;
            foreach(var item in items)
            {
                character.ItemBox[idx] = item;
                idx++;
            }

            for(; idx < character.itemBox.Length; idx++)
            {
                character.ItemBox[idx].ID = 0;
                character.ItemBox[idx].Count = 0;
            }
        }
    }

    bool suppressEqpTypeSelChange = false;
    private void ComboEquipType_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if(suppressEqpTypeSelChange)
            return;

        var eqp = character.EquipBox[idxSelectedEquip];

        if(idxSelectedEquip != -1)
        {
            eqp = character.EquipBox[idxSelectedEquip];
            eqp.PropertyChanged -= Eqp_PropertyChanged;
        }

        if(eqp.Kind!=eqp.KindLast)
        {
            var bt = eqp.SerializeOnlyTypeAndId();
            var alter = MonHunEquip.Create(bt);
            character.EquipBox[idxSelectedEquip] = alter;
        }

        var buttons = ButtonGrid.Children.OfType<Button>();
        Button button = buttons.ElementAt(idxSelectedEquip % 100);
        var img = button.Content as Image;
        if(img != null)
        {
            img.Source = eqp.GetImage();
        }

        eqp = character.EquipBox[idxSelectedEquip];
        eqp.PropertyChanged += Eqp_PropertyChanged;

        RefreshEquipmentBindings();
    }

    int idxSelectedEquip = -1;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MonHunEquip eqp = null;
        if(idxSelectedEquip != -1)
        {
            eqp = character.EquipBox[idxSelectedEquip];
            eqp.PropertyChanged -= Eqp_PropertyChanged;
        }

        Button button = sender as Button;
        if (button != null)
        {
            string tag = (string)button.Tag;
            int idx = int.Parse(tag) + idxEquipBoxPage * 100;
            idxSelectedEquip = idx;
        }

        eqp = character.EquipBox[idxSelectedEquip];
        eqp.PropertyChanged += Eqp_PropertyChanged;

        RefreshEquipmentBindings();
    }

    private void Eqp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == "Type")
        {
            var buttons = ButtonGrid.Children.OfType<Button>();
            foreach(var button in buttons)
            {
                string tag = (string)button.Tag;
                int idx = int.Parse(tag) + idxEquipBoxPage * 100;
                if(idx!=idxSelectedEquip)
                    continue;
                
                var img = button.Content as Image;
                if(img != null)
                {
                    img.Source = character.EquipBox[idx].GetImage();
                }
                break;
            }
        }

        var eqp = sender as MonHunEquip;
        if(eqp != null)
        {
            switch(eqp)
            {
                case MonHunWeapon weap:
                    {
                        if(e.PropertyName == "ElementType")
                        {
                            ComboEquipElementValue.ItemsSource = weap.ElementValueList;
                        }
                        if(e.PropertyName == nameof(MonHunEquip.ID))
                        {
                            RefreshEquipmentBindings();
                        }
                    }
                    break;
                case MonHunArmor armor:
                    {
                        if(e.PropertyName == nameof(MonHunEquip.ID))
                        {
                            RefreshEquipmentBindings();
                        }
                    }
                    break;
                case MonHunTalisman tal:
                    {
                        if(e.PropertyName == nameof(MonHunEquip.ID))
                        {
                            RefreshEquipmentBindings();
                        }
                    }
                    break;
            }
        }
    }


    public void RefreshEquipmentBindings()
    {
        var eqp = character.EquipBox[idxSelectedEquip];

        suppressEqpTypeSelChange = true;

        Binding myBinding = new Binding("Type");
        myBinding.Source = character.EquipBox[idxSelectedEquip];
        ComboEquipType.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipId.ItemsSource = eqp.Type switch
        {
            1 => MonHunEquip.equipChest,
            2 => MonHunEquip.equipArms,
            3 => MonHunEquip.equipWaist,
            4 => MonHunEquip.equipLegs,
            5 => MonHunEquip.equipHead,
            6 => MonHunEquip.equipTalisman,
            7 => MonHunEquip.equipGreatsword,
            8 => MonHunEquip.equipSns,
            9 => MonHunEquip.equipHammer,
            10 => MonHunEquip.equipLance,
            11 => MonHunEquip.equipLbg,
            12 => MonHunEquip.equipHbg,
            13 => MonHunEquip.equipLongsword,
            14 => MonHunEquip.equipSwitchaxe,
            15 => MonHunEquip.equipGunlance,
            16 => MonHunEquip.equipBow,
            17 => MonHunEquip.equipDualblade,
            18 => MonHunEquip.equipHuntinghorn,
            19 => MonHunEquip.equipInsectglaive,
            20 => MonHunEquip.equipChargeblade,
            _ => null
        };
        myBinding = new Binding("ID");
        myBinding.Source = character.EquipBox[idxSelectedEquip];
        ComboEquipId.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        switch(eqp.Kind)
        {
            case MonHunEquip.Kinds.None:
                ShowNoneUI();
                break;
            case MonHunEquip.Kinds.Weapon:
                {
                    var weap = eqp as MonHunWeapon;

                    if(weap.IsRelic)
                        ShowRelicWeaponUI(eqp, weap);
                    else
                        ShowNormalWeaponUI(eqp, weap);
                }
                break;
            case MonHunEquip.Kinds.Armor:
                {
                    var armor = eqp as MonHunArmor;
                    ShowRelicArmorUI(eqp, armor);
                }
                break;
            case MonHunEquip.Kinds.Talisman:
                {
                    var tal = eqp as MonHunTalisman;
                    ShowTalismanUI(eqp, tal);
                }
                break;
        }

        suppressEqpTypeSelChange = false;
    }

    public void ShowNoneUI()
    {
        HideTalismansUI();
        HideWeaponsUI();
        HideArmorsUI();
    }

    public void HideWeaponsUI()
    {
        LabelEqpElementType.Visibility = Visibility.Collapsed;
        ComboEquipElementType.Visibility = Visibility.Collapsed;
        LabelEqpElementValue.Visibility = Visibility.Collapsed;
        ComboEquipElementValue.Visibility = Visibility.Collapsed;
        LabelEqpSharpness.Visibility = Visibility.Collapsed;
        ComboEquipSharpness.Visibility = Visibility.Collapsed;
        LabelEqpModifier.Visibility = Visibility.Collapsed;
        ComboEquipModifier.Visibility = Visibility.Collapsed;
        LabelEqpUpgrade.Visibility = Visibility.Collapsed;
        ComboEquipUpgrade.Visibility = Visibility.Collapsed;
        LabelEqpSpecial.Visibility = Visibility.Collapsed;
        ComboEquipSpecial.Visibility = Visibility.Collapsed;
        LabelEqpNumSlots.Visibility = Visibility.Collapsed;
        ComboEquipNumSlots.Visibility = Visibility.Collapsed;
        LabelEqpPolished.Visibility = Visibility.Collapsed;
        CheckEquipPolished.Visibility = Visibility.Collapsed;
        LabelEqpGlow.Visibility = Visibility.Collapsed;
        CheckEquipGlow.Visibility = Visibility.Collapsed;
        LabelEqpKinsectLevel.Visibility = Visibility.Collapsed;
        ComboEquipKinsectLevel.Visibility = Visibility.Collapsed;
        LabelEqpHoning.Visibility = Visibility.Collapsed;
        ComboEquipHoning.Visibility = Visibility.Collapsed;
        LabelEqpRarity.Visibility = Visibility.Collapsed;
        ComboEquipRarity.Visibility = Visibility.Collapsed;
        LabelEqpPolishReq.Visibility = Visibility.Collapsed;
        ComboEquipPolishReq.Visibility = Visibility.Collapsed;

        ButtonLbgCInfo.Visibility = Visibility.Collapsed;
        ButtonHbgCInfo.Visibility = Visibility.Collapsed;
        ButtonBowCInfo.Visibility = Visibility.Collapsed;

        ButtonLbgFInfo.Visibility = Visibility.Collapsed;
        ButtonHbgFInfo.Visibility = Visibility.Collapsed;
    }

    public void HideArmorsUI()
    {
        LabelArmorUpgrade.Visibility = Visibility.Collapsed;
        ComboArmorUpgrade.Visibility = Visibility.Collapsed;
        LabelArmorResist.Visibility = Visibility.Collapsed;
        ComboArmorResist.Visibility = Visibility.Collapsed;
        LabelArmorDefense.Visibility = Visibility.Collapsed;
        ComboArmorDefense.Visibility = Visibility.Collapsed;

        LabelEqpNumSlots.Visibility = Visibility.Collapsed;
        ComboEquipNumSlots.Visibility = Visibility.Collapsed;
        LabelEqpPolished.Visibility = Visibility.Collapsed;
        CheckEquipPolished.Visibility = Visibility.Collapsed;
        LabelEqpGlow.Visibility = Visibility.Collapsed;
        CheckEquipGlow.Visibility = Visibility.Collapsed;
        LabelEqpRarity.Visibility = Visibility.Collapsed;
        ComboEquipRarity.Visibility = Visibility.Collapsed;
        LabelEqpPolishReq.Visibility = Visibility.Collapsed;
        ComboEquipPolishReq.Visibility = Visibility.Collapsed;
    }

    public void HideTalismansUI()
    {
        LabelTalSkill1Id.Visibility = Visibility.Collapsed;
        ComboTalSkill1Id.Visibility = Visibility.Collapsed;
        LabelTalSkill1Amount.Visibility = Visibility.Collapsed;
        ComboTalSkill1Amount.Visibility = Visibility.Collapsed;
        LabelTalSkill2Id.Visibility = Visibility.Collapsed;
        ComboTalSkill2Id.Visibility = Visibility.Collapsed;
        LabelTalSkill2Amount.Visibility = Visibility.Collapsed;
        ComboTalSkill2Amount.Visibility = Visibility.Collapsed;

        LabelEqpNumSlots.Visibility = Visibility.Collapsed;
        ComboEquipNumSlots.Visibility = Visibility.Collapsed;
    }

    public void ShowTalismanUI(MonHunEquip eqp, MonHunTalisman tal)
    {
        HideWeaponsUI();
        HideArmorsUI();

        LabelTalSkill1Id.Visibility = Visibility.Visible;
        ComboTalSkill1Id.Visibility = Visibility.Visible;
        LabelTalSkill1Amount.Visibility = Visibility.Visible;
        ComboTalSkill1Amount.Visibility = Visibility.Visible;
        LabelTalSkill2Id.Visibility = Visibility.Visible;
        ComboTalSkill2Id.Visibility = Visibility.Visible;
        LabelTalSkill2Amount.Visibility = Visibility.Visible;
        ComboTalSkill2Amount.Visibility = Visibility.Visible;

        LabelEqpNumSlots.Visibility = Visibility.Visible;
        ComboEquipNumSlots.Visibility = Visibility.Visible;

        Binding myBinding;

        ComboTalSkill1Id.ItemsSource = MonHunEquipStatic.Skill1IdValues;
        myBinding = new Binding("Skill1ID");
        myBinding.Source = eqp;
        ComboTalSkill1Id.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboTalSkill1Amount.ItemsSource = MonHunEquipStatic.Skill1AmountValues;
        myBinding = new Binding("Skill1Amount");
        myBinding.Source = eqp;
        ComboTalSkill1Amount.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboTalSkill2Id.ItemsSource = MonHunEquipStatic.Skill2IdValues;
        myBinding = new Binding("Skill2ID");
        myBinding.Source = eqp;
        ComboTalSkill2Id.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboTalSkill2Amount.ItemsSource = MonHunEquipStatic.Skill2AmountValues;
        myBinding = new Binding("Skill2Amount");
        myBinding.Source = eqp;
        ComboTalSkill2Amount.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipNumSlots.ItemsSource = MonHunEquipStatic.NumSlotsValues;
        myBinding = new Binding("NumSlots");
        myBinding.Source = eqp;
        ComboEquipNumSlots.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot1.ItemsSource = MonHunEquipStatic.Slot1Values;
        myBinding = new Binding("Slot1");
        myBinding.Source = eqp;
        ComboSlot1.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot2.ItemsSource = MonHunEquipStatic.Slot2Values;
        myBinding = new Binding("Slot2");
        myBinding.Source = eqp;
        ComboSlot2.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot3.ItemsSource = MonHunEquipStatic.Slot3Values;
        myBinding = new Binding("Slot3");
        myBinding.Source = eqp;
        ComboSlot3.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        myBinding = new Binding("Slot1Fixed");
        myBinding.Source = eqp;
        CheckSlot1Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot2Fixed");
        myBinding.Source = eqp;
        CheckSlot2Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot3Fixed");
        myBinding.Source = eqp;
        CheckSlot3Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);
    }


    public void ShowRelicArmorUI(MonHunEquip eqp, MonHunArmor armor)
    {
        HideWeaponsUI();
        HideTalismansUI();

        LabelArmorUpgrade.Visibility = Visibility.Visible;
        ComboArmorUpgrade.Visibility = Visibility.Visible;
        LabelArmorResist.Visibility = Visibility.Visible;
        ComboArmorResist.Visibility = Visibility.Visible;
        LabelArmorDefense.Visibility = Visibility.Visible;
        ComboArmorDefense.Visibility = Visibility.Visible;

        LabelEqpNumSlots.Visibility = Visibility.Visible;
        ComboEquipNumSlots.Visibility = Visibility.Visible;
        LabelEqpPolished.Visibility = Visibility.Visible;
        CheckEquipPolished.Visibility = Visibility.Visible;
        LabelEqpGlow.Visibility = Visibility.Visible;
        CheckEquipGlow.Visibility = Visibility.Visible;
        LabelEqpRarity.Visibility = Visibility.Visible;
        ComboEquipRarity.Visibility = Visibility.Visible;
        LabelEqpPolishReq.Visibility = Visibility.Visible;
        ComboEquipPolishReq.Visibility = Visibility.Visible;

        Binding myBinding;

        ComboArmorDefense.ItemsSource = MonHunEquipStatic.DefenseValues;
        myBinding = new Binding("Defense");
        myBinding.Source = eqp;
        ComboArmorDefense.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboArmorResist.ItemsSource = MonHunEquipStatic.ResistancesValues;
        myBinding = new Binding("Resistances");
        myBinding.Source = eqp;
        ComboArmorResist.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboArmorUpgrade.ItemsSource = MonHunEquipStatic.UpgradeArmorValues;
        myBinding = new Binding("Upgrade");
        myBinding.Source = eqp;
        ComboArmorUpgrade.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipNumSlots.ItemsSource = MonHunEquipStatic.NumSlotsValues;
        myBinding = new Binding("NumSlots");
        myBinding.Source = eqp;
        ComboEquipNumSlots.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        myBinding = new Binding("Polished");
        myBinding.Source = eqp;
        CheckEquipPolished.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Glow");
        myBinding.Source = eqp;
        CheckEquipGlow.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        ComboEquipRarity.ItemsSource = MonHunEquipStatic.RarityValues;
        myBinding = new Binding("Rarity");
        myBinding.Source = eqp;
        ComboEquipRarity.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipPolishReq.ItemsSource = MonHunEquipStatic.PolishReqValues;
        myBinding = new Binding("PolishReq");
        myBinding.Source = eqp;
        ComboEquipPolishReq.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot1.ItemsSource = MonHunEquipStatic.Slot1Values;
        myBinding = new Binding("Slot1");
        myBinding.Source = eqp;
        ComboSlot1.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot2.ItemsSource = MonHunEquipStatic.Slot2Values;
        myBinding = new Binding("Slot2");
        myBinding.Source = eqp;
        ComboSlot2.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot3.ItemsSource = MonHunEquipStatic.Slot3Values;
        myBinding = new Binding("Slot3");
        myBinding.Source = eqp;
        ComboSlot3.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        myBinding = new Binding("Slot1Fixed");
        myBinding.Source = eqp;
        CheckSlot1Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot2Fixed");
        myBinding.Source = eqp;
        CheckSlot2Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot3Fixed");
        myBinding.Source = eqp;
        CheckSlot3Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);
    }

    public void ShowNormalWeaponUI(MonHunEquip eqp, MonHunWeapon weap)
    {
        HideTalismansUI();
        HideArmorsUI();
        HideWeaponsUI();

        LabelEqpHoning.Visibility = Visibility.Visible;
        ComboEquipHoning.Visibility = Visibility.Visible;

        Binding myBinding;

        ComboEquipHoning.ItemsSource = MonHunEquipStatic.HoningValues;
        myBinding = new Binding("Honing");
        myBinding.Source = eqp;
        ComboEquipHoning.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        if(weap.IsGlaive)
        {
            LabelEqpKinsectLevel.Visibility = Visibility.Visible;
            ComboEquipKinsectLevel.Visibility = Visibility.Visible;

            myBinding = new Binding("KinsectLevel");
            myBinding.Source = eqp;
            ComboEquipKinsectLevel.SetBinding(ComboBox.SelectedValueProperty, myBinding);
        }

        ComboSlot1.ItemsSource = MonHunEquipStatic.Slot1Values;
        myBinding = new Binding("Slot1");
        myBinding.Source = eqp;
        ComboSlot1.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot2.ItemsSource = MonHunEquipStatic.Slot2Values;
        myBinding = new Binding("Slot2");
        myBinding.Source = eqp;
        ComboSlot2.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot3.ItemsSource = MonHunEquipStatic.Slot3Values;
        myBinding = new Binding("Slot3");
        myBinding.Source = eqp;
        ComboSlot3.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        myBinding = new Binding("Slot1Fixed");
        myBinding.Source = eqp;
        CheckSlot1Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot2Fixed");
        myBinding.Source = eqp;
        CheckSlot2Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot3Fixed");
        myBinding.Source = eqp;
        CheckSlot3Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);
    }

    public void ShowRelicWeaponUI(MonHunEquip eqp, MonHunWeapon weap)
    {
        HideTalismansUI();
        HideArmorsUI();

        LabelEqpElementType.Visibility = Visibility.Visible;
        ComboEquipElementType.Visibility = Visibility.Visible;
        LabelEqpElementValue.Visibility = Visibility.Visible;
        ComboEquipElementValue.Visibility = Visibility.Visible;
        LabelEqpSharpness.Visibility = Visibility.Visible;
        ComboEquipSharpness.Visibility = Visibility.Visible;
        LabelEqpModifier.Visibility = Visibility.Visible;
        ComboEquipModifier.Visibility = Visibility.Visible;
        LabelEqpUpgrade.Visibility = Visibility.Visible;
        ComboEquipUpgrade.Visibility = Visibility.Visible;
        LabelEqpSpecial.Visibility = Visibility.Visible;
        ComboEquipSpecial.Visibility = Visibility.Visible;
        LabelEqpNumSlots.Visibility = Visibility.Visible;
        ComboEquipNumSlots.Visibility = Visibility.Visible;
        LabelEqpPolished.Visibility = Visibility.Visible;
        CheckEquipPolished.Visibility = Visibility.Visible;
        LabelEqpGlow.Visibility = Visibility.Visible;
        CheckEquipGlow.Visibility = Visibility.Visible;
        LabelEqpHoning.Visibility = Visibility.Visible;
        ComboEquipHoning.Visibility = Visibility.Visible;
        LabelEqpRarity.Visibility = Visibility.Visible;
        ComboEquipRarity.Visibility = Visibility.Visible;
        LabelEqpPolishReq.Visibility = Visibility.Visible;
        ComboEquipPolishReq.Visibility = Visibility.Visible;

        LabelEqpKinsectLevel.Visibility = Visibility.Collapsed;
        ComboEquipKinsectLevel.Visibility = Visibility.Collapsed;

        ButtonLbgCInfo.Visibility = weap.IsLbg ? Visibility.Visible : Visibility.Collapsed;
        ButtonHbgCInfo.Visibility = weap.IsHbg ? Visibility.Visible : Visibility.Collapsed;
        ButtonBowCInfo.Visibility = weap.IsBow ? Visibility.Visible : Visibility.Collapsed;

        ButtonLbgFInfo.Visibility = weap.IsLbg ? Visibility.Visible : Visibility.Collapsed;
        ButtonHbgFInfo.Visibility = weap.IsHbg ? Visibility.Visible : Visibility.Collapsed;

        Binding myBinding;

        if(weap.IsGlaive)
        {
            LabelEqpKinsectLevel.Visibility = Visibility.Visible;
            ComboEquipKinsectLevel.Visibility = Visibility.Visible;

            myBinding = new Binding("KinsectLevel");
            myBinding.Source = eqp;
            ComboEquipKinsectLevel.SetBinding(ComboBox.SelectedValueProperty, myBinding);
        }

        if(weap.IsBowgun)
        {
            LabelEqpElementType.Visibility = Visibility.Collapsed;
            ComboEquipElementType.Visibility = Visibility.Collapsed;
            LabelEqpElementValue.Visibility = Visibility.Collapsed;
            ComboEquipElementValue.Visibility = Visibility.Collapsed;
        }
        else
        {
            LabelEqpElementType.Visibility = Visibility.Visible;
            ComboEquipElementType.Visibility = Visibility.Visible;
            LabelEqpElementValue.Visibility = Visibility.Visible;
            ComboEquipElementValue.Visibility = Visibility.Visible;

            myBinding = new Binding("ElementType");
            myBinding.Source = eqp;
            ComboEquipElementType.SetBinding(ComboBox.SelectedValueProperty, myBinding);

            ComboEquipElementValue.ItemsSource = weap.ElementValueList;
            myBinding = new Binding("ElementValue");
            myBinding.Source = eqp;
            ComboEquipElementValue.SetBinding(ComboBox.SelectedValueProperty, myBinding);
            myBinding = new Binding("HasElement");
            myBinding.Source = eqp;
            ComboEquipElementValue.SetBinding(ComboBox.IsEnabledProperty, myBinding);
        }

        if(weap.IsRanged)
        {
            if(weap.IsBowgun)
            {
                LabelEqpSharpness.Content = "Magazine Sizes";
            }
            else
            {
                LabelEqpSharpness.Content = "Coatings";
            }
        }
        else
        {
            LabelEqpSharpness.Content = "Sharpness";
        }

        ComboEquipSharpness.ItemsSource = weap.SharpnessValueList;
        myBinding = new Binding("Sharpness");
        myBinding.Source = eqp;
        ComboEquipSharpness.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipModifier.ItemsSource = weap.ModifierValueList;
        myBinding = new Binding("Modifier");
        myBinding.Source = eqp;
        ComboEquipModifier.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipUpgrade.ItemsSource = weap.UpgradeValueList;
        myBinding = new Binding("Upgrade");
        myBinding.Source = eqp;
        ComboEquipUpgrade.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        LabelEqpSpecial.Content = weap.SpecialValueName;
        ComboEquipSpecial.ItemsSource = weap.SpecialValueList;
        myBinding = new Binding("Special");
        myBinding.Source = eqp;
        ComboEquipSpecial.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipNumSlots.ItemsSource = MonHunEquipStatic.NumSlotsValues;
        myBinding = new Binding("NumSlots");
        myBinding.Source = eqp;
        ComboEquipNumSlots.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        myBinding = new Binding("Polished");
        myBinding.Source = eqp;
        CheckEquipPolished.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Glow");
        myBinding.Source = eqp;
        CheckEquipGlow.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        ComboEquipHoning.ItemsSource = MonHunEquipStatic.HoningValues;
        myBinding = new Binding("Honing");
        myBinding.Source = eqp;
        ComboEquipHoning.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipRarity.ItemsSource = MonHunEquipStatic.RarityValues;
        myBinding = new Binding("Rarity");
        myBinding.Source = eqp;
        ComboEquipRarity.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboEquipPolishReq.ItemsSource = MonHunEquipStatic.PolishReqValues;
        myBinding = new Binding("PolishReq");
        myBinding.Source = eqp;
        ComboEquipPolishReq.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot1.ItemsSource = MonHunEquipStatic.Slot1Values;
        myBinding = new Binding("Slot1");
        myBinding.Source = eqp;
        ComboSlot1.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot2.ItemsSource = MonHunEquipStatic.Slot2Values;
        myBinding = new Binding("Slot2");
        myBinding.Source = eqp;
        ComboSlot2.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        ComboSlot3.ItemsSource = MonHunEquipStatic.Slot3Values;
        myBinding = new Binding("Slot3");
        myBinding.Source = eqp;
        ComboSlot3.SetBinding(ComboBox.SelectedValueProperty, myBinding);

        myBinding = new Binding("Slot1Fixed");
        myBinding.Source = eqp;
        CheckSlot1Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot2Fixed");
        myBinding.Source = eqp;
        CheckSlot2Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);

        myBinding = new Binding("Slot3Fixed");
        myBinding.Source = eqp;
        CheckSlot3Fixed.SetBinding(CheckBox.IsCheckedProperty, myBinding);
    }


    int idxEquipBoxPage = 0;
    private void Button_EquipBoxLeftClick(object sender, RoutedEventArgs e)
    {
        if(idxEquipBoxPage == 0)
            return;
        
        idxEquipBoxPage--;

        LabelEquipBoxPage.Content = $"{idxEquipBoxPage + 1} / {14}";

        ReloadEquipBoxPage();
    }

    private void Button_EquipBoxRightClick(object sender, RoutedEventArgs e)
    {
        if(idxEquipBoxPage == 13)
            return;
        
        idxEquipBoxPage++;

        LabelEquipBoxPage.Content = $"{idxEquipBoxPage + 1} / {14}";

        ReloadEquipBoxPage();
    }

    protected void ReloadEquipBoxPage()
    {
        var buttons = ButtonGrid.Children.OfType<Button>();
        foreach(var button in buttons)
        {
            string tag = (string)button.Tag;
            int idx = int.Parse(tag) + idxEquipBoxPage * 100;
            var img = button.Content as Image;
            if(img != null)
            {
                img.Source = character.EquipBox[idx].GetImage();
            }
        }
    }











    
}