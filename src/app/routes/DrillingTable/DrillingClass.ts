export interface MonthMaster
{
    Month_Id: number;
  Month_Name: string;  
}

export interface YearMaster {
  Id: number;
  Year: string;
}

export interface OperationalArea {
  Area_Id: number;
  Area_Name: string;
}

export interface WellCatgory {
  Well_Category_Id: number;
  Well_Category_Name: string;
}

export interface BlockType {
  BLOCK_ID: number;
  BLOCK_NAME: string;
}



export interface OperatingStatus {
  Status_Id: number;
  Status_Name: string;
}



export interface WellType {
  WellType_Id: number;
  WellType_Name: string;
  }
export interface WellBlock {
 Wblock_Id: number;
  //Wblock_Id: string;
  Wblock_Name: string;
  }
export interface State {
    STATE_ID: number;
    STATE: string;
}

export interface WellField {
  Field_Id: number;
  PHASE_NAME: string;
}

export interface PhaseMaster {
  PHASE_ID: number;
  PHASE_NAME: string;
}

export interface DPRField {
  FIELD_ID: number;
  FIELD_NAME: string;
  OPERATOR_ID: number;
  }

export interface ddlList {
  Months: MonthMaster[];
  Years: YearMaster[];
  WellCatgory: WellCatgory[];
  OperationalArea: OperationalArea[];
  OperatingStatus: OperatingStatus[];
  BlockType: BlockType[];
  States: State[];
  WellTypes: WellType[];
  WellBlocks: WellBlock[];
 
}


export interface ddlListDPR { 
  OperationalArea: OperationalArea[];
  OperatingStatus: OperatingStatus[];
  BlockType: BlockType[];
  States: State[];
  WellTypes: WellType[];
  WellCatgory: WellCatgory[];
  PhaseMaster: PhaseMaster[];
  DPRFields: DPRField[];
}

export interface RigCountDetails {
  Id: number;
  BlockTypes: number;
  Months: number;
  Years: number;
  OperatingStatus: number;
  WellCategory: number;
  OperationalArea: number;
  TotalOnshoreOffShore: string;
  Remarks: string;
  BlockTypesName: string;
  MonthName: string;
  OperationalAreaName: string;
  OperatingStatusName: string;
  WellCategoryName: string;
  Status: string;
  
}

export interface RigWisePerformance {
      SL_NO :number
  OPERATOR_ID: number;
  OPERATION_AREA_ID: number; 
      RIG_NAME: string;
  RIG_OPERATING_STATUS: number;
  RIG_TYPE: string;
  EXP_WELLS: string ;
  EXP_MET: string;
  DEV_WELLS: string;
  DEV_MET: string;
  RIG_MODE_TIME_BREAKUP_RB: string;
  RIG_MODE_TIME_BREAKUP_DR: string;
  RIG_MODE_TIME_BREAKUP_PT: string;
  OUTCYCLE_CAPREP: string;
  OUTCYCLE_OTH: string;
  NONPRD_WELL_COMPLICATION_DAY: string;
  NONPRD_WELL_COMPLICATION_PER: string;
  NONPRD_WELL_REPAIR_DAY: string;
  NONPRD_WELL_REPAIR_PER: string;
  CYCLE_DAYS: string;
  COMMERCIAL_DAYS: string;
  CYCLE_EXP: string;
  CYCLE_DEV: string;
  CYCLE_SPEED: string;
  COMMERCIAL_EXP: string;
  COMMERCIAL_DEV: string;
  COMMERCIAL_SPEED: string;
  REMARKS: string;
  MONTH: number;
  YEAR: number;
  USERID: string;
  BLOCK_CATEGORY: number;
  STATUS: string;
  OperatingStatusName: string;
  BlockCategoryName: string;
  MonthName: string; 
  OperationalAreaName: string;
  
 
}

export interface WellWisePerformance {
  SL_NO: number;
  USERID: string;
  OPERATOR_ID: number;
  WELL_MONTH: number;
  WELL_YEAR: number;
  BLOCK_CATEGORY: number;
  WELL_NAME: string;
  //BLOCK_TYPE_NAME: number;
  BLOCK_ID: number;
  OPERATIONAL_AREA: number; 
  STATE: number;
  S_LATITUDE: string;
  S_LONGITUDE: string;
  SS_LATITUDE: string;
  SS_LONGITUDE: string;
  RIG_NAME: string;
  WELL_CATEGORY_ID: number;
  WELL_TYPE_ID: number;
  WATER_DEPTH: string;
  TARGET_DEPTH_MD: string;
  TARGET_DEPTH_TVD: string;
  DRILLED_DEPTH_MD: string;
  DRILLED_DEPTH_TVD: string;
  SPUD_DATE_TIME?: Date;
  HER_DATE_TIME?: Date;
  RR_DATE_TIME?: Date;
  CP_PLANNED: string;
  CP_ACTUAL: string;
  RB_PLANNED: string;
  RB_ACTUAL: string;
  DR_PLANNED: string;
  DR_ACTUAL: string;
  PT_PLANED: string;
  PT_ACTUAL: string;
  CYCLE_SPEED: string;
  COMMERCIAL_SPPED: string;
  REMARKS: string;
  //USERID  
  BLOCK_TYPE: number;
  BlockCategoryName: string;
  MonthName: string;
  OperatingStatusName: string;
  OperationalAreaName: string;
  WellCategoryName: string;
  WellTypeName: string;
  BlockTypeName: string;
  BlockFieldsName: string;
  STATUS: string;
    StateName: string;
  //IS_INSERT_NULL_DATA
}

export interface DPR {
  DPR_ID: number;
  DPR_DATE?: Date;
  BLOCK_ID: number;
  AREA_LOCATION: string;
  OPERATION_AREA_ID: number;
  RIG_NAME: string;
  RIG_OPERATOR_STATUS_ID: number;
  WELL_NAME: string;
  WELL_CATEGORY_ID: number;
  WELL_TYPE_ID: number;
  SPUD_DATE?: Date;
  PHASE_ID: number;
  TARGET_DEPTH: string;
  PRESENT_DEPTH: string;
  METARGE: string;
  DPR_BRIEF: string;
  BLOCK_TYPE: number;
  STATE: number;
  LATITUDE: string;
  LONGITUDE: string;
  STATUS: string;
  OperatingStatusName: string;
  OperationalAreaName: string;
  WellCategoryName: string;
  StateName: string;
  PhaseName: string;
  FieldName: string;
  DPRDATE :string;
  }


export interface CumulativeDrillingPerformance {
  SL_NO: number;
  OPERATOR_ID: number;
  MONTH: number;
  YEAR: number;
  BLOCK_CATEGORY: number;
  RIG_OPERATE_STATUS: number;
  WELL_CATEGORY_ID: number;
  OPERATION_AREA_ID : number;
  NO_WELLS: string;
  METARAGE: string;
  CYCLE_SPEED: string;
  COMMERCIAL_SPEED: string;
  RIG_MONTHS: string;
  STATUS: string;
  OperatingStatusName: string;
  OperationalAreaName: string;
  WellCategoryName: string;
  MonthName: string;
  BlockName: string; 
  }



export interface AnnualDrillingPlan {

  SL_NO: number;
  OPERATOR_ID: number;
  QUARTER_NO: string;
  YEAR: string;
  BLOCK_CATEGORY: number;
  RIG_OPERATE_STATUS: number;
  WELL_CATEGORY_ID: number;
  OPERATION_AREA_ID: number;
  NO_WELLS: string;
  METARAGE: string;
  CYCLE_SPEED: string;
  COMMERCIAL_SPEED: string;
  RIG_MONTHS: string;
  STATUS: string;
  OperatingStatusName: string;
  OperationalAreaName: string;
  WellCategoryName: string;
  QuarterType: string;
  BlockName: string;
  BE_RE: string;
}
