using System.IO;
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

public partial class LbgInfoFDialog : Window
{
    public List<LbgFInfo> ShotsAvailable {get;set;} = new()
    {
        new(0,"0x00","[x]","x","x","x","","","x","x","","","","","x","","","x","","","","x","","","","","","x","","","","","x","x","","","","","","Nrm1 (5,Low)","","",""),
        new(1,"0x01","x","x","","x","[x]","","x","","","x","","","","","","x","","x","x","","","x","x","","","","","","","","x","x","","","","","","Prc2 (3,Avg)","","",""),
        new(2,"0x02","x","x","","","","","[x]","x","","x","","","","","","x","x","[x]","x","x","","x","","x","","","x","","","","x","x","x","x","","","","Plt1 (3,Avg)","Psn1 (3,Hi)","",""),
        new(3,"0x03","x","[x]","x","x","x","","x","","","x","x","","x","x","","","","x","x","","","","","","","","","","[x]","","x","x","","","","","","Frz (3,Low)","Nrm2 (3,Low)","",""),
        new(4,"0x04","x","[x]","","x","[x]","","x","x","","x","","","","","","x","x","","","","","x","","","","","x","","","","x","x","x","x","","","","Nrm2 (4,Low)","Prc2 (3, Avg)","",""),
        new(5,"0x05","x","x","","x","","","[x]","x","","x","","","x","","","x","x","[x]","x","","","x","x","","","x","","","","","x","x","x","x","","","","Plt1 (3,Avg)","Psn1 (3,Hi)","",""),
        new(6,"0x06","x","x","","x","","","x","[x]","","x","","","x","","","x","x","","","[x]","x","x","x","","","","","x","","","x","x","x","x","","","","Plt1 (3,Avg)","Par1 (2,Hi)","",""),
        new(7,"0x07","x","x","x","","[x]","","x","x","","","","","x","","","x","","","","x","","","","[x]","","x","","","","","x","x","","","","","","Prc2 (3,Avg)","Exh1 (3, Avg)","",""),
        new(8,"0x08","x","[x]","x","x","","x","x","","x","x","x","","x","x","","","","x","x","","","","","","","[x]","","","","","x","x","","","","","","Flm (3,Low)","Nrm2 (3,Low)","",""),
        new(9,"0x09","x","x","x","","x","","","x","","[x]","","","","","","x","","","","x","","","","x","","","","[x]","","","x","x","x","x","x","","","Thn (3,Low)","Crg1 (3,Avg)","",""),
        new(10,"0x0A","x","x","x","","","","x","x","","","","","x","","","x","","","","x","","","","","","x","[x]","","","","x","x","","","[x]","","","Wtr (3,Low)","Slc (2,Avg)","",""),
        new(11,"0x0B","x","x","","x","","","x","x","","x","","","x","","","x","x","x","","[x]","","[x]","","x","","","","x","","","x","x","x","x","","","","Slp1 (2,Hi)","Par1 (2,Hi)","",""),
        new(12,"0x0C","x","x","x","","x","","[x]","","x","","x","","x","x","","","","","","x","","","x","","","","","","[x]","","x","x","","x","","","","Frz (3,Low)","Plt1 (3,Avg)","",""),
        new(13,"0x0D","x","[x]","x","","[x]","x","","x","","x","","","x","","","x","x","x","[x]","","","x","","x","","","","","","","x","x","x","x","x","","","Prc2 (3,Avg)","Nrm2 (3,Low)","Psn2 (2,Hi)",""),
        new(14,"0x0E","x","x","x","x","x","x","x","x","x","x","x","","[x]","x","x","x","x","x","[x]","x","x","","","","","[x]","","","","","x","x","x","","","","","Flm (4,Avg)","Psn2 (2,Hi)","Clst1 (2,V.Hi)",""),
        new(15,"0x0F","x","[x]","","x","x","","x","","","","","","","","","x","","x","","","","x","","","","[x]","x","[x]","x","x","x","x","","","","","","Nrm2 (3,Low)","Thn (3,Low)","Flm (4,Avg)",""),
        new(16,"0x10","x","x","","[x]","x","x","","x","","x","","","x","","","x","x","x","","x","","","","x","","x","x","x","","[x]","x","x","","","x","[x]","","Prc1 (3,Avg)","Drg (2,V.Hi)","Blst (2,Hi)",""),
        new(17,"0x11","x","x","x","x","","","x","[x]","x","x","","","[x]","x","","x","x","x","x","x","","","","[x]","x","x","","x","x","","x","x","","x","","","","Plt2 (2, Avg)","Cls1 (2,V.Hi)","Exh1 (3, Avg)","Nrm2 (3,Low)"),
        new(18,"0x12","x","x","x","","x","","","x","","x","x","","x","","","x","","[x]","x","x","x","x","","x","","","[x]","","x","x","x","x","x","x","[x]","","","Wtr (3,Low)","Slc (2,Avg)","Psn1 (3,Hi)","Psn2 (2,Hi)"),
        new(19,"0x13","x","x","x","x","[x]","x","x","","","x","x","","x","","","x","x","","","","","x","x","","","[x]","","[x]","x","","x","x","x","","x","","","Prc2 (3,Avg)","Flm (4,Avg)","Thn (3,Low)","Slp1 (2,Hi)"),
        new(20,"0x14","x","[x]","x","x","x","","x","x","","[x]","x","x","x","x","","x","","x","","[x]","x","","","x","","","x","","[x]","x","x","x","x","x","","","","Nrm2 (3,Low)","Crg1 (3,Avg)","Frz (3,Low)","Par1 (2,Hi)"),
        new(189,"0xBD","","[x]","x","x","x","x","[x]","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","","x","x","x","","","Nrm1 (5,Low)","Nrm2 (3,Low)","Plt1 (3,Low)",""),
        new(192,"0xC0","","x","x","[x]","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","x","[x]","x","","x","x","x","","","Prc1 (3,Avg)","Drg (2,V.Hi)","Nrm1 (5,Low)","Nrm1 (3,Low)"),
    };


    public LbgInfoFDialog()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void Button_ApplyClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void Button_CancelClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
