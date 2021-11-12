using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.APIModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Score
    {
        public double avg { get; set; }
        public string score { get; set; }
        public string category { get; set; }
        public string source { get; set; }
        public string status { get; set; }
    }

    public class OO
    {
        public string date { get; set; }
        public List<Score> scores { get; set; }
    }

    public class Inspection
    {
        public int oos { get; set; }
        public int ttl { get; set; }
        public string type { get; set; }
    }

    public class InspSummary
    {
        public int months { get; set; }
        public List<Inspection> inspection { get; set; }
    }

    public class ISSScore
    {
        public string date { get; set; }
        public List<Score> scores { get; set; }
    }

    public class Src
    {
        public string typ { get; set; }
        public string tbl { get; set; }
    }

    public class Name
    {
        public List<Src> srcs { get; set; }
        public string name { get; set; }
    }

    public class Email
    {
        public List<Src> srcs { get; set; }
        public string address { get; set; }
        public int matches { get; set; }
    }

    public class Phone
    {
        public List<Src> srcs { get; set; }
        public string num { get; set; }
        public int matches { get; set; }
    }

    public class Address
    {
        public List<Src> srcs { get; set; }
        public int zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public int matches { get; set; }
    }

    public class Rep
    {
        public List<Src> srcs { get; set; }
        public string name { get; set; }
    }

    public class Mlg150
    {
        public float mlg150 { get; set; }
        public int year { get; set; }
    }

    public class Oos
    {
        public string issued { get; set; }
        public string status { get; set; }
        public string category { get; set; }
    }

    public class Status
    {
        public string active { get; set; }
        public string dot_active { get; set; }
        public string dot_inactive { get; set; }
    }

    public class Review
    {
        public string date { get; set; }
        public string type { get; set; }
    }

    public class CompanyInfo
    {
        public List<Name> names { get; set; }
        public List<Email> emails { get; set; }
        public List<Phone> phones { get; set; }
        public List<Address> addresses { get; set; }
        public List<Rep> reps { get; set; }
        public string asOf { get; set; }
        public string dotFirstAssigned { get; set; }
        public string operations { get; set; }
        public Mlg150 mlg150 { get; set; }
        public string mlg151 { get; set; }
        public string hhg { get; set; }
        public string passenger { get; set; }
        public string hazmat { get; set; }
        public string classification { get; set; }
        public int tot_power { get; set; }
        public int tot_dr { get; set; }
        public int tot_drivers { get; set; }
        public Oos oos { get; set; }
        public string cargo { get; set; }
        public Status status { get; set; }
        public Review review { get; set; }
    }

    public class Coverage
    {
        public string insurer { get; set; }
        public string policy { get; set; }
        public string canceled { get; set; }
        public string effective { get; set; }
        public string type { get; set; }
    }

    public class Insurance
    {
        public string CarFile { get; set; }
        public float BIPD_File { get; set; }
        public string pas { get; set; }
        public string BonSurFile { get; set; }
        public string BonSure_Req { get; set; }
        public float BIPD_Req { get; set; }
        public string hhold { get; set; }
        public string frt { get; set; }
        public string Car_Req { get; set; }
        public List<Coverage> coverages { get; set; }
    }

    public class Authority
    {
        public string status { get; set; }
        public string type { get; set; }
    }

    public class Docket
    {
        public int docket { get; set; }
        public string prefix { get; set; }
        public Insurance insurance { get; set; }
        public List<Authority> authorities { get; set; }
    }

    public class Policy
    {
        public string insurer { get; set; }
        public string form { get; set; }
        public int max { get; set; }
        public string fed_min { get; set; }
        public int ins_id { get; set; }
        public string effective { get; set; }
        public int under { get; set; }
        public string ins_type { get; set; }
        public string ins_branch { get; set; }
        public string close_form { get; set; }
        public string close_code { get; set; }
        public string policy { get; set; }
        public string canceled { get; set; }
        public float bipd_req { get; set; }
        public string close_action { get; set; }
    }

    public class InsHist
    {
        public int dot { get; set; }
        public string docpre { get; set; }
        public int docnum { get; set; }
        public List<Policy> policies { get; set; }
    }

    public class BucketsByYear
    {
        public int year { get; set; }
        public string inspections { get; set; }
        public string percent { get; set; }
        public string bucket { get; set; }
    }

    public class Total
    {
        public string inspections { get; set; }
        public string percent { get; set; }
        public string bucket { get; set; }
    }

    public class Radiuses
    {
        public List<BucketsByYear> bucketsByYear { get; set; }
        public List<Total> totals { get; set; }
    }

    public class Crashes
    {
        public int months { get; set; }
        public int total { get; set; }
        public int crashesWithFatalities { get; set; }
        public int crashesWithInjuries { get; set; }
        public int crashesWithTowaway { get; set; }
        public int total_fat { get; set; }
        public int total_inj { get; set; }
    }

    public class Alert
    {
        public string category { get; set; }
        public string text { get; set; }
        public string name { get; set; }
    }

    public class DOTScore
    {
        public string date { get; set; }
        public List<Score> scores { get; set; }
    }

    public class Category
    {
        public string scoreAlert { get; set; }
        public string score { get; set; }
        public string seriousVio { get; set; }
        public string basicAlert { get; set; }
        public string type { get; set; }
        public string measure { get; set; }
        public int peerGroup { get; set; }
    }

    public class BASIC
    {
        public string date { get; set; }
        public List<Category> category { get; set; }
    }

    public class DOTResponse
    {
        //public List<OO> OOS { get; set; }
        //public InspSummary inspSummary { get; set; }
        //public List<ISSScore> ISS_Score { get; set; }
        public CompanyInfo companyInfo { get; set; }
        //public List<Docket> dockets { get; set; }
        //public List<InsHist> insHist { get; set; }
        //public string link { get; set; }
        //public Radiuses radiuses { get; set; }
        //public Crashes crashes { get; set; }
        //public List<Alert> alerts { get; set; }
        // public List<DOTScore> DOT_Score { get; set; }
        // public List<BASIC> BASICs { get; set; }
    }




}

