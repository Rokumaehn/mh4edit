
namespace mh4edit;

public class BowCInfo
{
    public int Val { get; set; }
    public string Value { get; set; }
    public string Shot { get; set; }
    public string L1 { get; set; }
    public string L2 { get; set; }
    public string L3 { get; set; }
    public string L4 { get; set; }
    public string Pwr { get; set; }
    public string Psn { get; set; }
    public string Par { get; set; }
    public string Slp { get; set; }
    public string CR { get; set; }
    public string Pnt { get; set; }
    public string Exh { get; set; }
    public string Bls { get; set; }
    public string Boost { get; set; }

    public BowCInfo(int val, string value, string shot, string l1, string l2, string l3, string l4, string pwr, string psn, string par, string slp, string cR, string pnt, string exh, string bls, string boost)
    {
        Val = val;
        Value = value;
        Shot = shot;
        L1 = l1;
        L2 = l2;
        L3 = l3;
        L4 = l4;
        Pwr = pwr;
        Psn = psn;
        Par = par;
        Slp = slp;
        CR = cR;
        Pnt = pnt;
        Exh = exh;
        Bls = bls;
        Boost = boost;
    }
    
    
}
