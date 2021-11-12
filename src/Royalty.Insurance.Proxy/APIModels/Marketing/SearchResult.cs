using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.APIModels.Marketing
{

    public class DetailedSearch
    {
        public int dot { get; set; }
        public List<L_I> L_I { get; set; }
        public Events events { get; set; }
        public Contact contact { get; set; }
        public Units units { get; set; }
        public Drivers drivers { get; set; }
        public Mileage mileage { get; set; }
        public MCS150 MCS150 { get; set; }
        public List<string> operations { get; set; }
        public List<Score> scores { get; set; }

    }

    public class Events
    {
        public int inspection_Radius { get; set; }
    }

    public class Contact
    {
        public Name name { get; set; }
        public Phone phone { get; set; }
        public Address address { get; set; }
        public string email { get; set; }
        public Reps reps { get; set; }
    }

    public class Name
    {
        public string leg { get; set; }
        public string dba { get; set; }
    }

    public class Phone
    {
        public string phone { get; set; }
        public string cell { get; set; }
        public string fax { get; set; }
    }

    public class Address
    {
        public Business business { get; set; }
        public Mailing mailing { get; set; }
    }

    public class Business
    {
        public string street { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string ZIP { get; set; }
    }

    public class Mailing
    {
        public string street { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string ZIP { get; set; }
    }

    public class Reps
    {
        public string name1 { get; set; }
        public string name2 { get; set; }
    }

    public class Units
    {
        public int pu { get; set; }
        public Trucks trucks { get; set; }
        public Trailers trailers { get; set; }
        public Bus bus { get; set; }
    }

    public class Trucks
    {
        public int all { get; set; }
        public int own { get; set; }
        public int lease { get; set; }
    }

    public class Trailers
    {
        public int own { get; set; }
        public int lease { get; set; }
    }

    public class Bus
    {
        public int all { get; set; }
        public int own { get; set; }
        public int lease { get; set; }
    }

    public class Drivers
    {
        public float total { get; set; }
        public int CDL { get; set; }
    }

    public class Mileage
    {
        public int Mlg150 { get; set; }
        public int MCS150MileageYear { get; set; }
    }

    public class MCS150
    {
        public int yib { get; set; }
        public string DOTAddDate { get; set; }
        public string date { get; set; }
        public bool hazmat { get; set; }
    }

    public class L_I
    {
        public string pre { get; set; }
        public int doc { get; set; }
        public string common { get; set; }
        public string contract { get; set; }
        public string broker { get; set; }
        public bool pass { get; set; }
        public bool hHold { get; set; }
        public int bipdReq { get; set; }
        public List<Insurance> insurance { get; set; }

    }

    public class Insurance
    {
        public string polNum { get; set; }
        public string effDt { get; set; }
        public string insurer { get; set; }
        public string locale { get; set; }
        public float typeCd { get; set; }
        public string insType { get; set; }
        public bool boc3 { get; set; }
        public int polExpMonth { get; set; }
        public int polExpDay { get; set; }
        public string polExpDate { get; set; }
    }

    public class Score
    {
        public Dotrating DOTRating { get; set; }
        public ISS ISS { get; set; }
        public BASICS BASICS { get; set; }
    }

    public class Dotrating
    {
        public string rating { get; set; }
        public string date { get; set; }
    }

    public class ISS
    {
        public int score { get; set; }
        public string src { get; set; }
    }

    public class BASICS
    {
        public Unsafe _unsafe { get; set; }
        public HOS HOS { get; set; }
        public Drfit drFit { get; set; }
        public Contrsubst contrSubst { get; set; }
        public Vm vm { get; set; }
        public Hazmat hazmat { get; set; }
        public Crash crash { get; set; }
    }

    public class Unsafe
    {
        public bool alert { get; set; }
        public float score { get; set; }
    }

    public class HOS
    {
        public float score { get; set; }
        public bool alert { get; set; }
    }

    public class Drfit
    {
        public float score { get; set; }
        public bool alert { get; set; }
    }

    public class Contrsubst
    {
        public float score { get; set; }
        public bool alert { get; set; }
    }

    public class Vm
    {
        public float score { get; set; }
        public bool alert { get; set; }
    }

    public class Hazmat
    {
        public bool alert { get; set; }
    }

    public class Crash
    {
        public float score { get; set; }
        public bool alert { get; set; }
    }
}

