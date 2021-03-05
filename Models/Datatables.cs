using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace pdms123.Models
{
  public class Datatables
  {
   
  }
  public class MonthMaster
  {
    public int Month_Id { get; set; }
    public string Month_Name { get; set; }


  }
  public class YearMaster
  {
    public int Id { get; set; }
    public string Year { get; set; }
   
  }

  public class OperationalArea
  {
   public int Area_Id { get; set; }
    public string Area_Name { get; set; }
  }

  public class WellCatgory
  {
    public int Well_Category_Id { get; set; }
    public string Well_Category_Name { get; set; }
  }

  public class BlockType
  {
    public int BLOCK_ID { get; set; }
    public string BLOCK_NAME { get; set; }
  }

  public class OperatingStatus
  {
    public int Status_Id { get; set; }
    public string Status_Name { get; set; }
  }

  public class WellType
  {

     public int WellType_Id { get; set; }
    public string WellType_Name { get; set; }
  }
  public class WellBlock
  {
    public int Wblock_Id { get; set; }
   // public string Wblock_Id { get; set; }
    public string Wblock_Name { get; set; }
  }

  public class WellField
  {
   public int Field_Id { get; set; }
   public string  Field_Name { get; set; }
  }


  public class State
  {
    public int STATE_ID { get; set; }
    public string STATE { get; set; }
  }

  public class PhaseMaster
  {
    public int  PHASE_ID { get; set; }
    public string PHASE_NAME { get; set; }
}

  public class DPRField
  {
    public int FIELD_ID { get; set; }
    public string FIELD_NAME { get; set; }

    public int OPERATOR_ID { get; set; }
  }
  
  public class ddlMaster
  {
    public List<MonthMaster> Months { get; set; }
    public List<YearMaster> Years { get; set; }
    public List<OperatingStatus> OperatingStatus { get; set; }
    public List<OperationalArea> OperationalArea { get; set; }
    public List<WellCatgory> WellCatgory { get; set; }
    public  List<BlockType> BlockType { get; set; }
    public List<State> States { get; set; }  
    public List<WellType> WellTypes { get; set; }
    public List<WellBlock> WellBlocks { get; set; }
   

  }
  public class ddlListDPR
  {
    
    public List<OperatingStatus> OperatingStatus { get; set; }
    public List<OperationalArea> OperationalArea { get; set; }
    public List<WellCatgory> WellCatgory { get; set; }
    public List<WellType> WellTypes { get; set; }
    public List<State> States { get; set; }
    public List<PhaseMaster> PhaseMaster { get; set; }
    public List<DPRField>  DPRFields { get; set; }


  }


  public class RigCountDetails
  {
    public int Id { get; set; }
    public int BlockTypes { get; set; }
    public int Months { get; set; }
    public int Years { get; set; }
    public int OperatingStatus { get; set; }
    public int WellCategory { get; set; }
    public int OperationalArea { get; set; }
    public string TotalOnshoreOffShore { get; set; }
    public string Remarks { get; set; }
    public string BlockTypesName { get; set; }
    public string MonthName { get; set; } 
    public string OperatingStatusName { get; set; }
    public string OperationalAreaName { get; set; }
    public string WellCategoryName { get; set; }
    public string Status { get; set; }  

  }

  public class RigWisePerformance
  {
    public int  SL_NO { get; set; }
    public int OPERATOR_ID { get; set; }
    public int OPERATION_AREA_ID { get; set; }
    public int BLOCK_ID { get; set; }
    public string RIG_NAME { get; set; }
    public int RIG_OPERATING_STATUS { get; set; }
    public string RIG_TYPE  { get; set; }
    public string EXP_WELLS  { get; set; }
    public string EXP_MET  { get; set; }
    public string DEV_WELLS  { get; set; }
    public string DEV_MET  { get; set; }
    public string RIG_MODE_TIME_BREAKUP_RB  { get; set; }
    public string RIG_MODE_TIME_BREAKUP_DR  { get; set; }
    public string RIG_MODE_TIME_BREAKUP_PT  { get; set; }
    public string OUTCYCLE_CAPREP  { get; set; }
    public string OUTCYCLE_OTH  { get; set; }
    public string NONPRD_WELL_COMPLICATION_DAY  { get; set; }
    public string NONPRD_WELL_COMPLICATION_PER  { get; set; }
    public string NONPRD_WELL_REPAIR_DAY  { get; set; }
    public string NONPRD_WELL_REPAIR_PER  { get; set; }
    public string CYCLE_DAYS  { get; set; }
    public string COMMERCIAL_DAYS  { get; set; }
    public string CYCLE_EXP { get; set; }
    public string CYCLE_DEV  { get; set; }
    public string CYCLE_SPEED  { get; set; }
    public string COMMERCIAL_EXP { get; set; }
    public string COMMERCIAL_DEV { get; set; }
    public string COMMERCIAL_SPEED { get; set; }
    public string REMARKS { get; set; }
    public int MONTH { get; set; }
    public int YEAR { get; set; }
    public string  USERID { get; set; }    
    public int BLOCK_CATEGORY { get; set; }
    public string  STATUS { get; set; }

    public string OperatingStatusName { get; set; }
    public string BlockCategoryName { get; set; }
    public string MonthName { get; set; }    
    public string OperationalAreaName { get; set; }
      

  }

  public class WellWisePerformance
  {
    public int SL_NO { get; set; }
    public string USERID { get; set; }
    public int OPERATOR_ID { get; set; }
    public int WELL_MONTH { get; set; }
    public int WELL_YEAR{ get; set; }
    public int BLOCK_CATEGORY { get; set; }
    public string WELL_NAME{ get; set; }
    //BLOCK_TYPE_NAME{ get; set; }
    public int BLOCK_ID { get; set; }
    public int OPERATIONAL_AREA { get; set; }
    public int STATE { get; set; }
    public string S_LATITUDE { get; set; }
    public string S_LONGITUDE { get; set; }
    public string SS_LATITUDE { get; set; }
    public string SS_LONGITUDE { get; set; }
    public string RIG_NAME { get; set; }
    public int WELL_CATEGORY_ID { get; set; }
    public int WELL_TYPE_ID { get; set; }
   public string WATER_DEPTH { get; set; }
   public string TARGET_DEPTH_MD { get; set; }
   public string TARGET_DEPTH_TVD { get; set; }
   public string DRILLED_DEPTH_MD { get; set; }
   public string DRILLED_DEPTH_TVD { get; set; }
   public DateTime? SPUD_DATE_TIME { get; set; }
    public DateTime? HER_DATE_TIME { get; set; }
    public DateTime? RR_DATE_TIME { get; set; }
    public string CP_PLANNED { get; set; }
    public string CP_ACTUAL { get; set; }
    public string RB_PLANNED { get; set; }
    public string RB_ACTUAL { get; set; }
    public string DR_PLANNED { get; set; }
    public string DR_ACTUAL { get; set; }
    public string PT_PLANED { get; set; }
    public string PT_ACTUAL { get; set; }
    public string CYCLE_SPEED { get; set; }
    public string COMMERCIAL_SPPED { get; set; }
    public string REMARKS{ get; set; }
      //USERID  
    public int BLOCK_TYPE { get; set; }   
    public string BlockCategoryName { get; set; }
    public string MonthName { get; set; }
    public string OperatingStatusName { get; set; }
    public string OperationalAreaName { get; set; }
    public string WellCategoryName { get; set; }
    public string BlockTypeName { get; set; }
    public string BlockFieldsName { get; set; }
    public string WellTypeName { get; set; }  
    public string STATUS { get; set; }
    public string StateName { get; set; }
  }

  public class DPR
  {
    public int DPR_ID { get; set; }
    public DateTime DPR_DATE { get; set; }
    public int BLOCK_ID { get; set; }
    public string AREA_LOCATION { get; set; }
    public int OPERATION_AREA_ID { get; set; }
    public string RIG_NAME { get; set; }
    public int RIG_OPERATOR_STATUS_ID { get; set; }
    public string WELL_NAME { get; set; }
    public int WELL_CATEGORY_ID { get; set; }
    public int WELL_TYPE_ID { get; set; }
    public DateTime? SPUD_DATE { get; set; }
    public int PHASE_ID { get; set; }
    public string TARGET_DEPTH { get; set; }
    public string PRESENT_DEPTH { get; set; }
    public string METARGE { get; set; }
    public string DPR_BRIEF { get; set; }
    public int BLOCK_TYPE { get; set; }
    public int STATE { get; set; }
    public string LATITUDE { get; set; }
    public string LONGITUDE { get; set; }
    public string STATUS { get; set; }
    public string OperatingStatusName { get; set; }
    public string OperationalAreaName { get; set; }
    public string WellCategoryName { get; set; }
    public string StateName { get; set; }
    public string PhaseName { get; set; }
    public string FieldName { get; set; }
    public string DPRDATE { get; set; }


  }

  public class CumulativeDrillingPerformance
  {
    
    public int SL_NO { get; set; }
    public int  OPERATOR_ID { get; set; }
    public int MONTH { get; set; }
    public int YEAR { get; set; }
    public int BLOCK_CATEGORY { get; set; }
    public int RIG_OPERATE_STATUS { get; set; }
    public int WELL_CATEGORY_ID { get; set; }
    public int OPERATION_AREA_ID { get; set; }
    public string NO_WELLS { get; set; }
    public string METARAGE { get; set; }
    public string CYCLE_SPEED { get; set; }
    public string COMMERCIAL_SPEED { get; set; }
    public string RIG_MONTHS { get; set; }
    public string STATUS { get; set; }
    public string OperatingStatusName { get; set; }
    public string OperationalAreaName { get; set; }
    public string WellCategoryName { get; set; }
    public string MonthName { get; set; }
    public string BlockName { get; set; }  
  }

  public class AnnualDrillingPlan
  {

    public int SL_NO { get; set; }
    public int OPERATOR_ID { get; set; }
    public string QUARTER_NO { get; set; }
    public string YEAR { get; set; }
    public int BLOCK_CATEGORY { get; set; }
    public int RIG_OPERATE_STATUS { get; set; }
    public int WELL_CATEGORY_ID { get; set; }
    public int OPERATION_AREA_ID { get; set; }
    public string NO_WELLS { get; set; }
    public string METARAGE { get; set; }
    public string CYCLE_SPEED { get; set; }
    public string COMMERCIAL_SPEED { get; set; }
    public string RIG_MONTHS { get; set; }
    public string STATUS { get; set; }
    public string OperatingStatusName { get; set; }
    public string OperationalAreaName { get; set; }
    public string WellCategoryName { get; set; }
    public string QuarterType { get; set; }
    public string BlockName { get; set; }
    public string BE_RE { get; set; }
  }


}
