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

public partial class BowInfoCDialog : Window
{
    public List<BowCInfo> Coatings {get;set;} = new()
    {
        new(0,"0x00","Wide","R1","R2","R3","[S3]","x","x","x","x","","x","","","none"),
        new(1,"0x01","Focus","R1","R2","S3","[P3]","","x","x","x","x","x","x","","none"),
        new(2,"0x02","Wide","R1","P1","P3","[S3]","x","","","x","x","x","","","none"),
        new(3,"0x03","Focus","P2","P3","S3","[R3]","","x","x","","x","x","","","none"),
        new(4,"0x04","Power","S2","P2","S3","[R3]","x","x","x","","","x","","","none"),
        new(5,"0x05","Focus","P1","S2","R3","[S3]","x","","","","x","x","x","","none"),
        new(6,"0x06","Focus","S1","R2","S2","[R4]","x","","x","x","x","x","","","none"),
        new(7,"0x07","Wide","R1","R2","P3","[R4]","x","x","x","","x","x","x","","none"),
        new(8,"0x08","Power","P2","R2","R3","[S4]","x","x","","","x","x","","","none"),
        new(9,"0x09","Blast","P2","P3","R3","[S4]","","","","x","x","x","","","none"),
        new(10,"0x0A","Wide","P2","R3","P3","[S3]","","x","","x","","x","","[x]","Blast+"),
        new(11,"0x0B","Blast","S1","R3","S3","[P3]","x","x","x","x","x","x","x","","none"),
        new(12,"0x0C","Power","R2","S2","S3","[R4]","x","","x","x","x","x","","","none"),
        new(13,"0x0D","Blast","S2","S2","P4","R5","","x","[x]","x","x","x","x","","Paralysis+"),
        new(14,"0x0E","Focus","R2","R3","S4","S4","x","","","","","x","[x]","x","Exhaust+"),
        new(15,"0x0F","Power","R3","P2","R4","P4","x","[x]","","","x","x","","","Poison+"),
        new(16,"0x10","Wide","S3","R4","P4","[S5]","x","","x","","x","x","","[x]","Blast+"),
        new(17,"0x11","Blast","R3","S3","R4","R5","x","x","","","x","x","x","","none"),
        new(18,"0x12","Power","P2","P3","P4","[P5]","","x","x","x","x","x","","x","none"),
        new(19,"0x13","Focus","R3","R4","S5","[S5]","","x","[x]","x","","x","","","Paralysis+"),
        new(20,"0x14","Power","P3","S4","R5","[R5]","x","","","[x]","x","x","","","Sleep+"),
        new(21,"0x15","Power","R3","S3","[S4]","","x","","","","","x","[x]","x","Exhaust+"),
    };


    public BowInfoCDialog(bool isHbg)
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
