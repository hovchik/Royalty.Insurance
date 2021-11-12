using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.APIModels.Core
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Census
    {
        public string act { get; set; }
        public string entTyp { get; set; }
        public int dot { get; set; }
        public string legNm { get; set; }
        public string dbaNm { get; set; }
        public string dbNo { get; set; }
        public string phNtn { get; set; }
        public int reg { get; set; }
        public string phStr { get; set; }
        public string phCty { get; set; }
        public int phCnty { get; set; }
        public string phSt { get; set; }
        public string phZip { get; set; }
        public string phUndel { get; set; }
        public string telNo { get; set; }
        public string cellNo { get; set; }
        public string faxNo { get; set; }
        public string mNtn { get; set; }
        public string mStr { get; set; }
        public string mCty { get; set; }
        public int mCnty { get; set; }
        public string mSt { get; set; }
        public string mZip { get; set; }
        public string mUndel { get; set; }
        public int oic { get; set; }
        public string terr { get; set; }
        public string docPre1 { get; set; }
        public int doc1 { get; set; }
        public string docPre2 { get; set; }
        public int doc2 { get; set; }
        public string docPre3 { get; set; }
        public int doc3 { get; set; }
        public string @class { get; set; }
        public string class2 { get; set; }
        public string class3 { get; set; }
        public string class4 { get; set; }
        public string class5 { get; set; }
        public string class6 { get; set; }
        public string class7 { get; set; }
        public string class8 { get; set; }
        public string class9 { get; set; }
        public string class10 { get; set; }
        public string class11 { get; set; }
        public string class12 { get; set; }
        public string classDef { get; set; }
        public string crInter { get; set; }
        public string crHMIntra { get; set; }
        public string crIntra { get; set; }
        public string shInter { get; set; }
        public string shIntra { get; set; }
        public string vehReg { get; set; }
        public string org { get; set; }
        public string genFrght { get; set; }
        public string hh { get; set; }
        public string metalSheet { get; set; }
        public string motorVeh { get; set; }
        public string drvTow { get; set; }
        public string log { get; set; }
        public string bldgMat { get; set; }
        public string mblHm { get; set; }
        public string machLg { get; set; }
        public string prdc { get; set; }
        public string liqGas { get; set; }
        public string interMod { get; set; }
        public string psg { get; set; }
        public string oilFld { get; set; }
        public string livestock { get; set; }
        public string grainfeed { get; set; }
        public string coalCoke { get; set; }
        public string meat { get; set; }
        public string grbg { get; set; }
        public string usmail { get; set; }
        public string chem { get; set; }
        public string dryBulk { get; set; }
        public string coldFood { get; set; }
        public string bvgs { get; set; }
        public string paperProd { get; set; }
        public string utility { get; set; }
        public string farmSupp { get; set; }
        public string construct { get; set; }
        public string waterwell { get; set; }
        public string cargoOthr { get; set; }
        public string otherCargo { get; set; }
        public string hmInd { get; set; }
        public int ownTruck { get; set; }
        public int ownTract { get; set; }
        public int ownTrail { get; set; }
        public int ownHMTrail { get; set; }
        public int ownHMTruck { get; set; }
        public int ownCoach { get; set; }
        public int ownSchool1_8 { get; set; }
        public int ownSchool9_15 { get; set; }
        public int ownSchool16 { get; set; }
        public int ownBus16 { get; set; }
        public int ownVan1_8 { get; set; }
        public int ownVan9_15 { get; set; }
        public int ownLimo1_8 { get; set; }
        public int ownLimo9_15 { get; set; }
        public int ownLimo16 { get; set; }
        public int trmTruck { get; set; }
        public int trmTract { get; set; }
        public int trmTrail { get; set; }
        public int trmHMTrail { get; set; }
        public int trmHMTruck { get; set; }
        public int trmCoach { get; set; }
        public int trmSchool1_8 { get; set; }
        public int trmSchool9_15 { get; set; }
        public int trmSchool16 { get; set; }
        public int trmBus16 { get; set; }
        public int trmVan1_8 { get; set; }
        public int trmVan9_15 { get; set; }
        public int trmLimo1_8 { get; set; }
        public int trmLimo9_15 { get; set; }
        public int trmLimo16 { get; set; }
        public int trpTruck { get; set; }
        public int trpTract { get; set; }
        public int trpTrail { get; set; }
        public int trpHMTrail { get; set; }
        public int trpHMTruck { get; set; }
        public int trpCoach { get; set; }
        public int trpSchool1_8 { get; set; }
        public int trpSchool9_15 { get; set; }
        public int trpSchool16 { get; set; }
        public int trpBus16 { get; set; }
        public int trpVan1_8 { get; set; }
        public int trpVan9_15 { get; set; }
        public int trpLimo1_8 { get; set; }
        public int trpLimo9_15 { get; set; }
        public int trpLimo16 { get; set; }
        public int totTrucks { get; set; }
        public int totBuses { get; set; }
        public int totPwr { get; set; }
        public string fleetSize { get; set; }
        public int drvInterLt100 { get; set; }
        public int drvInterGT100 { get; set; }
        public int drvInterTot { get; set; }
        public int drvIntraLT100 { get; set; }
        public int drvIntraGT100 { get; set; }
        public int drvIntraTot { get; set; }
        public int avgTld { get; set; }
        public int totDrvs { get; set; }
        public int cdlDrvs { get; set; }
        public string revTyp { get; set; }
        public int revDocNo { get; set; }
        public string revDt { get; set; }
        public float accRate { get; set; }
        public float repPrevRat { get; set; }
        public float mlg150 { get; set; }
        public float mlg151 { get; set; }
        public string rating { get; set; }
        public string ratedDt { get; set; }
        public string phBarrio { get; set; }
        public string mBarrio { get; set; }
        public int mcsipStep { get; set; }
        public string mcsipDt { get; set; }
        public string userID { get; set; }
        public string addCd { get; set; }
        public string updReas { get; set; }
        public string delCd { get; set; }
        public int mcs150MlgYr { get; set; }
        public string addDt { get; set; }
        public string chngDt { get; set; }
        public string delDt { get; set; }
        public int totCars { get; set; }
        public string ver { get; set; }
        public string createDt { get; set; }
        public string addUsrID { get; set; }
        public string delUsrID { get; set; }
        public string mcs150Dt { get; set; }
        public string recUpdFl { get; set; }
        public string emailAddr { get; set; }
        public string dotRevFl { get; set; }
        public int dotRevNo { get; set; }
        public string rep1 { get; set; }
        public string rep2 { get; set; }
    }

    public class ActPendIn
    {
        public string frmCd { get; set; }
        public string insType { get; set; }
        public int insComp { get; set; }
        public string insCar { get; set; }
        public string pol { get; set; }
        public string pstDt { get; set; }
        public string effDt { get; set; }
        public string bipdUnd { get; set; }
        public int bipdMax { get; set; }
        public string cnclDt { get; set; }
    }

    public class AuthHist
    {
        public string authType { get; set; }
        public string authAct { get; set; }
        public string actDt { get; set; }
        public string dis { get; set; }
        public string srvDt { get; set; }
        public string disDt { get; set; }
    }

    public class InsHist
    {
        public string frmCd { get; set; }
        public string clsAct { get; set; }
        public string clsFrm { get; set; }
        public string insType { get; set; }
        public string pol { get; set; }
        public int bipdReq { get; set; }
        public string bipdCl { get; set; }
        public int bipdUnd { get; set; }
        public int bipdMax { get; set; }
        public string clsCd { get; set; }
        public int insComp { get; set; }
        public string insBr { get; set; }
        public string insCompNm { get; set; }
        public string effDt { get; set; }
        public string effDtTo { get; set; }
    }

    public class LicensingAndInsurance
    {
        public int dot { get; set; }
        public string docPre { get; set; }
        public int doc { get; set; }
        public string mxOp { get; set; }
        public string rfc { get; set; }
        public string comAuth { get; set; }
        public string conAuth { get; set; }
        public string broAuth { get; set; }
        public string penComAuth { get; set; }
        public string penConAuth { get; set; }
        public string penBroAuth { get; set; }
        public string comAuthRev { get; set; }
        public string conAuthRev { get; set; }
        public string broAuthRev { get; set; }
        public string frt { get; set; }
        public string pas { get; set; }
        public string hhold { get; set; }
        public string prvt { get; set; }
        public string entr { get; set; }
        public string bipdReq { get; set; }
        public string carReq { get; set; }
        public string bonSurReq { get; set; }
        public string bipdFile { get; set; }
        public string carFile { get; set; }
        public string bonSurFile { get; set; }
        public string adrStat { get; set; }
        public string dbaNm { get; set; }
        public string legNm { get; set; }
        public string baStr { get; set; }
        public string baCol { get; set; }
        public string baCty { get; set; }
        public string baCo { get; set; }
        public string baSt { get; set; }
        public string baZip { get; set; }
        public string baTel { get; set; }
        public string baFax { get; set; }
        public string maStr { get; set; }
        public string maCol { get; set; }
        public string maCty { get; set; }
        public string maCo { get; set; }
        public string maSt { get; set; }
        public string maZip { get; set; }
        public string maTel { get; set; }
        public string maFax { get; set; }
        public List<ActPendIn> actPendIns { get; set; }
        public List<AuthHist> authHist { get; set; }
        public List<InsHist> insHist { get; set; }
    }

    public class CarrierDetail
    {
        public int carrierID { get; set; }
        public string nm { get; set; }
        public string str { get; set; }
        public string cty { get; set; }
        public string st { get; set; }
        public string z { get; set; }
        public string col { get; set; }
        public string pre { get; set; }
        public int docket { get; set; }
        public string inter { get; set; }
        public string stID { get; set; }
        public int srcCd { get; set; }
        public string ctyCd { get; set; }
        public string prefix { get; set; }
        public string interst { get; set; }
        public string noId { get; set; }
        public string stNo { get; set; }
        public string stIssNo { get; set; }
    }

    public class Unit
    {
        public int utID { get; set; }
        public int typ { get; set; }
        public int utNo { get; set; }
        public string mk { get; set; }
        public string co { get; set; }
        public string lic { get; set; }
        public string licSt { get; set; }
        public string vin { get; set; }
        public string dcl { get; set; }
        public string dclNo { get; set; }
        public int utYear { get; set; }
        public int utGVWR { get; set; }
        public string oosNo { get; set; }
        public string decStat { get; set; }
        public string decExst { get; set; }
        public string rmCrgSealID { get; set; }
        public string rplCrgSealID { get; set; }
    }

    public class VioCodeDetails
    {
        public string display { get; set; }
        public string desc { get; set; }
        public string cat { get; set; }
        public string catType { get; set; }
        public int basWt { get; set; }
        public int basGrpID { get; set; }
        public string basGrp { get; set; }
        public int basCatID { get; set; }
        public string basCat { get; set; }
        public bool OOSWtChngFl { get; set; }
        public string inspTyp { get; set; }
        public string svVio { get; set; }
    }

    public class Violation
    {
        public int vioID { get; set; }
        public int seq { get; set; }
        public string part { get; set; }
        public string partSec { get; set; }
        public string vUt { get; set; }
        public int utID { get; set; }
        public int vCatID { get; set; }
        public string OOSInd { get; set; }
        public int defVrfID { get; set; }
        public string citNo { get; set; }
        public string vioCd { get; set; }
        public string pstCrshFl { get; set; }
        public string vioDet { get; set; }
        public VioCodeDetails vioCodeDetails { get; set; }
    }

    public class Driver
    {
        public int drvID { get; set; }
        public string typ { get; set; }
        public string fNm { get; set; }
        public string lNm { get; set; }
        public string dob { get; set; }
        public int age { get; set; }
        public string licSt { get; set; }
        public string validLic { get; set; }
        public string licClassID { get; set; }
        public string citIss { get; set; }
    }

    public class HmCarried
    {
        public int hmID { get; set; }
        public string hmMatID { get; set; }
        public string repQty { get; set; }
        public string waste { get; set; }
        public string code { get; set; }
        public string codeDesc { get; set; }
    }

    public class Inspection
    {
        public int id { get; set; }
        public int dot { get; set; }
        public string rptSt { get; set; }
        public string rptNo { get; set; }
        public string dt { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string regisDt { get; set; }
        public string reg { get; set; }
        public string statCd { get; set; }
        public string sftyInspKey { get; set; }
        public string loc { get; set; }
        public string locDesc { get; set; }
        public string cntyCdSt { get; set; }
        public string cntyCd { get; set; }
        public int inspLv { get; set; }
        public string srvCntr { get; set; }
        public int cenSrc { get; set; }
        public string inspFac { get; set; }
        public string shpNm { get; set; }
        public string shpPprNo { get; set; }
        public string crgTnk { get; set; }
        public string hmInspTyp { get; set; }
        public string hmPReq { get; set; }
        public string aspVerNo { get; set; }
        public string snetVerNo { get; set; }
        public string snetSrchDt { get; set; }
        public string alcSub { get; set; }
        public string drgSrch { get; set; }
        public string drgArr { get; set; }
        public string szWtEnf { get; set; }
        public string trfcEnf { get; set; }
        public string lclEnfJur { get; set; }
        public string conLvl { get; set; }
        public string penCenM { get; set; }
        public string pen2 { get; set; }
        public string pen3 { get; set; }
        public string fnlStatDt { get; set; }
        public string postAcc { get; set; }
        public int gcvw { get; set; }
        public string defVer { get; set; }
        public string oosDefVer { get; set; }
        public int hmSent { get; set; }
        public int vioNSent { get; set; }
        public int oosNSent { get; set; }
        public int vioTtl { get; set; }
        public int oosTtl { get; set; }
        public int drVioTtl { get; set; }
        public int drvOOSTtl { get; set; }
        public int vehVioTtl { get; set; }
        public int vehOOSTtl { get; set; }
        public int hmVioTtl { get; set; }
        public int hmOOSTtl { get; set; }
        public int snetSeq { get; set; }
        public string oRptSt { get; set; }
        public string oRptNo { get; set; }
        public string oRptDt { get; set; }
        public string oRptTm { get; set; }
        public string tranCd { get; set; }
        public string tranDt { get; set; }
        public string uplDt { get; set; }
        public string uplFrstBt { get; set; }
        public string uplDOT { get; set; }
        public string uplSrchInd { get; set; }
        public string cenSrchDt { get; set; }
        public string mcmisDt { get; set; }
        public string chngUsr { get; set; }
        public string chngDt { get; set; }
        public string chngApp { get; set; }
        public string snetInpDt { get; set; }
        public string srcOff { get; set; }
        public int dist { get; set; }
        public List<CarrierDetail> carrierDetails { get; set; }
        public List<Unit> units { get; set; }
        public List<Violation> violations { get; set; }
        public List<Driver> drivers { get; set; }
        public List<HmCarried> hmCarried { get; set; }
    }

    public class Event
    {
        public int crEvID { get; set; }
        public int seq { get; set; }
        public string evID { get; set; }
        public string evDesc { get; set; }
    }

    public class Crash
    {
        public int id { get; set; }
        public string rptSt { get; set; }
        public string rptNo { get; set; }
        public string rptDt { get; set; }
        public int rptTm { get; set; }
        public int seq { get; set; }
        public int dot { get; set; }
        public string statCd { get; set; }
        public string statDt { get; set; }
        public string loc { get; set; }
        public string cty { get; set; }
        public string ctyCd { get; set; }
        public string st { get; set; }
        public string cntyCd { get; set; }
        public string trckBusInd { get; set; }
        public string trfcID { get; set; }
        public string rdAccCntrl { get; set; }
        public string rdSurfCon { get; set; }
        public int ax { get; set; }
        public string cdgBdy { get; set; }
        public string gvwr { get; set; }
        public string gvwrID { get; set; }
        public string cenSrc { get; set; }
        public string wxCnd { get; set; }
        public string vin { get; set; }
        public string vehLicNo { get; set; }
        public string vehLicSt { get; set; }
        public string hmPlac { get; set; }
        public string vehConfig { get; set; }
        public string lightCond { get; set; }
        public string hmRel { get; set; }
        public string agency { get; set; }
        public string offBdg { get; set; }
        public int amtVeh { get; set; }
        public int fat { get; set; }
        public int inj { get; set; }
        public string tow { get; set; }
        public string fedRec { get; set; }
        public string stRec { get; set; }
        public string snetVerNo { get; set; }
        public string snetSrchDt { get; set; }
        public string snetSeq { get; set; }
        public string snetInDt { get; set; }
        public string oRptSt { get; set; }
        public string oRptNo { get; set; }
        public string oRptDt { get; set; }
        public string oRptTm { get; set; }
        public string tranCd { get; set; }
        public string tranDt { get; set; }
        public string uplDt { get; set; }
        public string addDt { get; set; }
        public string chngDt { get; set; }
        public List<CarrierDetail> carrierDetails { get; set; }
        public List<Event> events { get; set; }
        public List<Driver> drivers { get; set; }
    }

    public class Iss
    {
        public int dot { get; set; }
        public int iss { get; set; }
        public string issSrc { get; set; }
        public string issDt { get; set; }
    }

    public class BasicsHistory
    {
        public string snapDt { get; set; }
        public int grpID { get; set; }
        public int dot { get; set; }
        public double score { get; set; }
        public string alert { get; set; }
        public string thresh { get; set; }
        public string srVio { get; set; }
        public string disp { get; set; }
        public int inspWvio { get; set; }
        public double measure { get; set; }
        public int peergroup { get; set; }
    }

    public class Oo
    {
        public int dot { get; set; }
        public string oosCat { get; set; }
        public string oosDt { get; set; }
        public string rescDt { get; set; }
    }

    public class DotCoreResponse
    {
        public Census census { get; set; }
        public List<LicensingAndInsurance> licensingAndInsurance { get; set; }
        public List<Inspection> inspections { get; set; }
        //public List<Crash> crashes { get; set; }
        //public List<Iss> iss { get; set; }
        //public List<BasicsHistory> basicsHistory { get; set; }
        //public List<Oo> oos { get; set; }
    }


}