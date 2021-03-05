using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IO;
using System.Reflection;
using OfficeOpenXml;
//using System.Web.Hosting;
using System.Net;
using System.Net.Http;

//using ClosedXML.Excel;

using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;

namespace pdms123.Models
{
  public class Datamodel
  {
   
   
    public int  OPERATOR_ID=44;
    public string USERID = "123764";
    public string OPERATOR_NAME = "Oil & Natural Gas Corp. Ltd.";

    string connection = "User ID=xuser;Connection Timeout=600;Password=xuser;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST= 192.168.0.111)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME= dgh)));";
    public List<ddlMaster> GetRigCountAllList()
    {
      List<ddlMaster> ret = new List<ddlMaster>();

      try
      {
        var dd = "";
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_GET_ALL_MASTER_DETAILS", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          cmd.Parameters.Add("DATA_CURSOR1", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("DATA_CURSOR2", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor3", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor4", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor5", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor6", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor7", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor8", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor9", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor10", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          // DataTable ds = new DataTable();
          DataSet ds = new DataSet();
          da.Fill(ds);
          ret = ds.Tables[2].ToList<ddlMaster>();
          ret[0].Months = ds.Tables[0].ToList<MonthMaster>();
          ret[0].Years = ds.Tables[1].ToList<YearMaster>();
          ret[0].OperationalArea = ds.Tables[3].ToList<OperationalArea>();
          ret[0].WellCatgory = ds.Tables[4].ToList<WellCatgory>();
          ret[0].OperatingStatus = ds.Tables[5].ToList<OperatingStatus>();
          ret[0].BlockType = ds.Tables[6].ToList<BlockType>();
          ret[0].WellTypes = ds.Tables[7].ToList<WellType>();
          ret[0].States = ds.Tables[8].ToList<State>();
          ret[0].WellBlocks = ds.Tables[9].ToList<WellBlock>();

          con.Close();
        }
      }
      catch (Exception e)
      {
        // ErrorHandlingLogSave(e.Message, "StateData");
      }
      return ret;
    }


    public int CheckDataExists(int Years,int Months,int Block)
    {
      int ret = 0;
      using (OracleConnection con=new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_DATA_EXIST_TABLE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OMONTH_ID", Months);
        cmd.Parameters.Add("OYEAR_ID", Years);     
        cmd.Parameters.Add("OBLOCK_ID", Block);       
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
       // cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "1");
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          ret = 0;
        }
        else
        {
          ret =Convert.ToInt32( ds.Tables[0].Rows[0][0]);
        }
      }
      return ret;    
    }


    #region "Rig Count Details Code Start"

    public string DraftRigCountDetails(RigCountDetails rig)
    {
      string ret = "";
      //int Exist = CheckDataExists(rig.Years,rig.Months,rig.BlockTypes);
      //if (Exist == 0)
      //{
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_RIGCOUNT_FORM_SUBMISSION", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          // OracleParameter op = new OracleParameter("data_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
          cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("OID", rig.Id);
          cmd.Parameters.Add("OMONTH_ID", rig.Months);
          cmd.Parameters.Add("OYEAR_ID", rig.Years);
          cmd.Parameters.Add("OSTATUS_ID", rig.OperatingStatus);
          cmd.Parameters.Add("OWELL_ID", rig.WellCategory);
          cmd.Parameters.Add("OAREA_ID", rig.OperationalArea);
          cmd.Parameters.Add("OTOTAL_RIG", rig.TotalOnshoreOffShore);
          cmd.Parameters.Add("OBLOCK_ID", rig.BlockTypes);
          cmd.Parameters.Add("OREMARKS", rig.Remarks);
          cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
          cmd.Parameters.Add("OUSERID", USERID);
          //   cmd.Parameters.Add("P_SUBCATEGORY", PrintRepository.SUBCATEGORY == null ? DBNull.Value : (object)PrintRepository.SUBCATEGORY);
          cmd.Parameters.Add("CALLVAL", "1");
          DataTable ds = new DataTable();
          da.Fill(ds);
          ret = ds.Rows[0][0].ToString();        
        }
      //}
      //else
      //{
      //  ret = "Data already Exists Please Check Your Draft Using Search.";
      //}
        return ret;
    }

    public string SubmitRigCountDetails(RigCountDetails rig)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIGCOUNT_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OID", rig.Id);
        cmd.Parameters.Add("OMONTH_ID", rig.Months);
        cmd.Parameters.Add("OYEAR_ID", rig.Years);
        cmd.Parameters.Add("OSTATUS_ID", rig.OperatingStatus);
        cmd.Parameters.Add("OWELL_ID", rig.WellCategory);
        cmd.Parameters.Add("OAREA_ID", rig.OperationalArea);
        cmd.Parameters.Add("OTOTAL_RIG", rig.TotalOnshoreOffShore);
        cmd.Parameters.Add("OBLOCK_ID", rig.BlockTypes);
        cmd.Parameters.Add("OREMARKS", rig.Remarks);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
       }
      return ret;
    }


    public string DraftUpdateRigCountDetails(RigCountDetails rig)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIGCOUNT_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OID", rig.Id);
        cmd.Parameters.Add("OMONTH_ID", rig.Months);
        cmd.Parameters.Add("OYEAR_ID", rig.Years);
        cmd.Parameters.Add("OSTATUS_ID", rig.OperatingStatus);
        cmd.Parameters.Add("OWELL_ID", rig.WellCategory);
        cmd.Parameters.Add("OAREA_ID", rig.OperationalArea);
        cmd.Parameters.Add("OTOTAL_RIG", rig.TotalOnshoreOffShore);
        cmd.Parameters.Add("OBLOCK_ID", rig.BlockTypes);
        cmd.Parameters.Add("OREMARKS", rig.Remarks);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string DraftSubmitRigCountDetails(RigCountDetails rig)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIGCOUNT_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OID", rig.Id);
        cmd.Parameters.Add("OMONTH_ID", rig.Months);
        cmd.Parameters.Add("OYEAR_ID", rig.Years);
        cmd.Parameters.Add("OSTATUS_ID", rig.OperatingStatus);
        cmd.Parameters.Add("OWELL_ID", rig.WellCategory);
        cmd.Parameters.Add("OAREA_ID", rig.OperationalArea);
        cmd.Parameters.Add("OTOTAL_RIG", rig.TotalOnshoreOffShore);
        cmd.Parameters.Add("OBLOCK_ID", rig.BlockTypes);
        cmd.Parameters.Add("OREMARKS", rig.Remarks);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }





    public List<RigCountDetails> SearchRigCount(RigCountDetails rig)
    {
      List<RigCountDetails> ret = new List<RigCountDetails>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_SEARCH_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        // OracleParameter op = new OracleParameter("data_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OMONTH_ID", rig.Months);
        cmd.Parameters.Add("OYEAR_ID", rig.Years);
        cmd.Parameters.Add("OBLOCK_ID", rig.BlockTypes);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<RigCountDetails>();

      }
      return ret;
    }


    public List<RigCountDetails> GetRigCountDetailsId(int Id)
    {
      List<RigCountDetails> ret = new List<RigCountDetails>();
      using (OracleConnection con = new OracleConnection(connection))
      {

        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DETAILS_BY_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_ID", Id);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<RigCountDetails>();
      }
      return ret;

    }
#endregion

    #region "Rig Wise Performance Code start"

    public string DraftRigWisePerformanceDetails(RigWisePerformance rigwise)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIG_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", rigwise.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", rigwise.BLOCK_CATEGORY);
        cmd.Parameters.Add("OMONTH", rigwise.MONTH);
        cmd.Parameters.Add("OYEAR", rigwise.YEAR);
        cmd.Parameters.Add("OOPERATION_AREA_ID", rigwise.OPERATION_AREA_ID);
        cmd.Parameters.Add("ORIG_NAME", rigwise.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATING_STATUS", rigwise.RIG_OPERATING_STATUS);
        cmd.Parameters.Add("ORIG_TYPE", rigwise.RIG_TYPE);
        cmd.Parameters.Add("OEXP_WELLS", rigwise.EXP_WELLS);
        cmd.Parameters.Add("OEXP_MET", rigwise.EXP_MET);
        cmd.Parameters.Add("ODEV_WELLS", rigwise.DEV_WELLS);
        cmd.Parameters.Add("ODEV_MET", rigwise.DEV_MET);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_RB", rigwise.RIG_MODE_TIME_BREAKUP_RB);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_DR", rigwise.RIG_MODE_TIME_BREAKUP_DR);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_PT", rigwise.RIG_MODE_TIME_BREAKUP_PT);
        cmd.Parameters.Add("OOUTCYCLE_CAPREP", rigwise.OUTCYCLE_CAPREP);
        cmd.Parameters.Add("OOUTCYCLE_OTH", rigwise.OUTCYCLE_OTH);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_DAY", rigwise.NONPRD_WELL_COMPLICATION_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_PER", rigwise.NONPRD_WELL_COMPLICATION_PER);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_DAY", rigwise.NONPRD_WELL_REPAIR_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_PER", rigwise.NONPRD_WELL_REPAIR_PER);
        cmd.Parameters.Add("OCYCLE_DAYS", rigwise.CYCLE_DAYS);
        cmd.Parameters.Add("OCOMMERCIAL_DAYS", rigwise.COMMERCIAL_DAYS);
        cmd.Parameters.Add("OCYCLE_EXP", rigwise.CYCLE_EXP);
        cmd.Parameters.Add("OCYCLE_DEV", rigwise.CYCLE_DEV);
        cmd.Parameters.Add("OCYCLE_SPEED", rigwise.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_EXP", rigwise.COMMERCIAL_EXP);
        cmd.Parameters.Add("OCOMMERCIAL_DEV", rigwise.COMMERCIAL_DEV);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", rigwise.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OREMARKS", rigwise.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }

    public string SubmitRigWisePerformanceDetails(RigWisePerformance rigwise)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIG_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", rigwise.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", rigwise.BLOCK_CATEGORY);
        cmd.Parameters.Add("OMONTH", rigwise.MONTH);
        cmd.Parameters.Add("OYEAR", rigwise.YEAR);
        cmd.Parameters.Add("OOPERATION_AREA_ID", rigwise.OPERATION_AREA_ID);
        cmd.Parameters.Add("ORIG_NAME", rigwise.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATING_STATUS", rigwise.RIG_OPERATING_STATUS);
        cmd.Parameters.Add("ORIG_TYPE", rigwise.RIG_TYPE);
        cmd.Parameters.Add("OEXP_WELLS", rigwise.EXP_WELLS);
        cmd.Parameters.Add("OEXP_MET", rigwise.EXP_MET);
        cmd.Parameters.Add("ODEV_WELLS", rigwise.DEV_WELLS);
        cmd.Parameters.Add("ODEV_MET", rigwise.DEV_MET);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_RB", rigwise.RIG_MODE_TIME_BREAKUP_RB);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_DR", rigwise.RIG_MODE_TIME_BREAKUP_DR);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_PT", rigwise.RIG_MODE_TIME_BREAKUP_PT);
        cmd.Parameters.Add("OOUTCYCLE_CAPREP", rigwise.OUTCYCLE_CAPREP);
        cmd.Parameters.Add("OOUTCYCLE_OTH", rigwise.OUTCYCLE_OTH);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_DAY", rigwise.NONPRD_WELL_COMPLICATION_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_PER", rigwise.NONPRD_WELL_COMPLICATION_PER);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_DAY", rigwise.NONPRD_WELL_REPAIR_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_PER", rigwise.NONPRD_WELL_REPAIR_PER);
        cmd.Parameters.Add("OCYCLE_DAYS", rigwise.CYCLE_DAYS);
        cmd.Parameters.Add("OCOMMERCIAL_DAYS", rigwise.COMMERCIAL_DAYS);
        cmd.Parameters.Add("OCYCLE_EXP", rigwise.CYCLE_EXP);
        cmd.Parameters.Add("OCYCLE_DEV", rigwise.CYCLE_DEV);
        cmd.Parameters.Add("OCYCLE_SPEED", rigwise.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_EXP", rigwise.COMMERCIAL_EXP);
        cmd.Parameters.Add("OCOMMERCIAL_DEV", rigwise.COMMERCIAL_DEV);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", rigwise.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OREMARKS", rigwise.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }

    public List<RigWisePerformance> SearchRigWise(RigWisePerformance rwp)
    {
      List<RigWisePerformance> ret = new List<RigWisePerformance>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_SEARCH_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        // OracleParameter op = new OracleParameter("data_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OMONTH_ID", rwp.MONTH);
        cmd.Parameters.Add("OYEAR_ID", rwp.YEAR);
        cmd.Parameters.Add("OBLOCK_ID", rwp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<RigWisePerformance>();

      }
      return ret;
    }


    public List<RigWisePerformance> GetRigWiseDetailsId(int Id)
    {
      List<RigWisePerformance> ret = new List<RigWisePerformance>();
      using (OracleConnection con = new OracleConnection(connection))
      {

        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DETAILS_BY_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_ID", Id);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<RigWisePerformance>();
      }
      return ret;
    }

    public string DraftUpdateRigWiseDetails(RigWisePerformance rigwise)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIG_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", rigwise.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", rigwise.BLOCK_CATEGORY);
        cmd.Parameters.Add("OMONTH", rigwise.MONTH);
        cmd.Parameters.Add("OYEAR", rigwise.YEAR);
        cmd.Parameters.Add("OOPERATION_AREA_ID", rigwise.OPERATION_AREA_ID);
        cmd.Parameters.Add("ORIG_NAME", rigwise.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATING_STATUS", rigwise.RIG_OPERATING_STATUS);
        cmd.Parameters.Add("ORIG_TYPE", rigwise.RIG_TYPE);
        cmd.Parameters.Add("OEXP_WELLS", rigwise.EXP_WELLS);
        cmd.Parameters.Add("OEXP_MET", rigwise.EXP_MET);
        cmd.Parameters.Add("ODEV_WELLS", rigwise.DEV_WELLS);
        cmd.Parameters.Add("ODEV_MET", rigwise.DEV_MET);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_RB", rigwise.RIG_MODE_TIME_BREAKUP_RB);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_DR", rigwise.RIG_MODE_TIME_BREAKUP_DR);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_PT", rigwise.RIG_MODE_TIME_BREAKUP_PT);
        cmd.Parameters.Add("OOUTCYCLE_CAPREP", rigwise.OUTCYCLE_CAPREP);
        cmd.Parameters.Add("OOUTCYCLE_OTH", rigwise.OUTCYCLE_OTH);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_DAY", rigwise.NONPRD_WELL_COMPLICATION_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_PER", rigwise.NONPRD_WELL_COMPLICATION_PER);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_DAY", rigwise.NONPRD_WELL_REPAIR_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_PER", rigwise.NONPRD_WELL_REPAIR_PER);
        cmd.Parameters.Add("OCYCLE_DAYS", rigwise.CYCLE_DAYS);
        cmd.Parameters.Add("OCOMMERCIAL_DAYS", rigwise.COMMERCIAL_DAYS);
        cmd.Parameters.Add("OCYCLE_EXP", rigwise.CYCLE_EXP);
        cmd.Parameters.Add("OCYCLE_DEV", rigwise.CYCLE_DEV);
        cmd.Parameters.Add("OCYCLE_SPEED", rigwise.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_EXP", rigwise.COMMERCIAL_EXP);
        cmd.Parameters.Add("OCOMMERCIAL_DEV", rigwise.COMMERCIAL_DEV);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", rigwise.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OREMARKS", rigwise.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string DraftSubmitRigWiseDetails(RigWisePerformance rigwise)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_RIG_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", rigwise.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", rigwise.BLOCK_CATEGORY);
        cmd.Parameters.Add("OMONTH", rigwise.MONTH);
        cmd.Parameters.Add("OYEAR", rigwise.YEAR);
        cmd.Parameters.Add("OOPERATION_AREA_ID", rigwise.OPERATION_AREA_ID);
        cmd.Parameters.Add("ORIG_NAME", rigwise.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATING_STATUS", rigwise.RIG_OPERATING_STATUS);
        cmd.Parameters.Add("ORIG_TYPE", rigwise.RIG_TYPE);
        cmd.Parameters.Add("OEXP_WELLS", rigwise.EXP_WELLS);
        cmd.Parameters.Add("OEXP_MET", rigwise.EXP_MET);
        cmd.Parameters.Add("ODEV_WELLS", rigwise.DEV_WELLS);
        cmd.Parameters.Add("ODEV_MET", rigwise.DEV_MET);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_RB", rigwise.RIG_MODE_TIME_BREAKUP_RB);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_DR", rigwise.RIG_MODE_TIME_BREAKUP_DR);
        cmd.Parameters.Add("ORIG_MODE_TIME_BREAKUP_PT", rigwise.RIG_MODE_TIME_BREAKUP_PT);
        cmd.Parameters.Add("OOUTCYCLE_CAPREP", rigwise.OUTCYCLE_CAPREP);
        cmd.Parameters.Add("OOUTCYCLE_OTH", rigwise.OUTCYCLE_OTH);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_DAY", rigwise.NONPRD_WELL_COMPLICATION_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_COMPLICATION_PER", rigwise.NONPRD_WELL_COMPLICATION_PER);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_DAY", rigwise.NONPRD_WELL_REPAIR_DAY);
        cmd.Parameters.Add("ONONPRD_WELL_REPAIR_PER", rigwise.NONPRD_WELL_REPAIR_PER);
        cmd.Parameters.Add("OCYCLE_DAYS", rigwise.CYCLE_DAYS);
        cmd.Parameters.Add("OCOMMERCIAL_DAYS", rigwise.COMMERCIAL_DAYS);
        cmd.Parameters.Add("OCYCLE_EXP", rigwise.CYCLE_EXP);
        cmd.Parameters.Add("OCYCLE_DEV", rigwise.CYCLE_DEV);
        cmd.Parameters.Add("OCYCLE_SPEED", rigwise.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_EXP", rigwise.COMMERCIAL_EXP);
        cmd.Parameters.Add("OCOMMERCIAL_DEV", rigwise.COMMERCIAL_DEV);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", rigwise.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OREMARKS", rigwise.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    #endregion

    #region "Well Wise Performance Code start"
    public List<WellField> GetFieldName(int Id)
    {
      List<WellField> ret = new List<WellField>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_WELL_SUB_DDL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OBLOCK_TYPE", Id);
        cmd.Parameters.Add("OOperator_Id", OPERATOR_ID);
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<WellField>();
      }
      return ret;
    }
    public string DraftWellWiseDetails(WellWisePerformance wwp)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_WELL_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", wwp.SL_NO);
        cmd.Parameters.Add("OWELL_MONTH ", wwp.WELL_MONTH);
        cmd.Parameters.Add("OWELL_YEAR", wwp.WELL_YEAR);
        cmd.Parameters.Add("OBLOCK_TYPE ", wwp.BLOCK_TYPE);
        cmd.Parameters.Add("OBLOCK_CATEGORY ", wwp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OBLOCK_ID ", wwp.BLOCK_ID);//Field Id
        cmd.Parameters.Add("OWELL_NAME", wwp.WELL_NAME);
        cmd.Parameters.Add("OOPERATIONAL_AREA ", wwp.OPERATIONAL_AREA);
        cmd.Parameters.Add("OSTATE ", wwp.STATE);
        cmd.Parameters.Add("OS_LATITUDE ", wwp.S_LATITUDE);
        cmd.Parameters.Add("OS_LONGITUDE ", wwp.S_LONGITUDE);
        cmd.Parameters.Add("OSS_LATITUDE ", wwp.SS_LATITUDE);
        cmd.Parameters.Add("OSS_LONGITUDE ", wwp.SS_LONGITUDE);
        cmd.Parameters.Add("ORIG_NAME ", wwp.RIG_NAME);
        cmd.Parameters.Add("OWELL_CATEGORY_ID ", wwp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID ", wwp.WELL_TYPE_ID);
        cmd.Parameters.Add("OWATER_DEPTH ", wwp.WATER_DEPTH);
        cmd.Parameters.Add("OTARGET_DEPTH_MD ", wwp.TARGET_DEPTH_MD);
        cmd.Parameters.Add("OTARGET_DEPTH_TVD ", wwp.TARGET_DEPTH_TVD);
        cmd.Parameters.Add("ODRILLED_DEPTH_MD ", wwp.DRILLED_DEPTH_MD);
        cmd.Parameters.Add("ODRILLED_DEPTH_TVD ", wwp.DRILLED_DEPTH_TVD);
        cmd.Parameters.Add("OSPUD_DATE_TIME ", wwp.SPUD_DATE_TIME);
        cmd.Parameters.Add("OHER_DATE_TIME ", wwp.HER_DATE_TIME);
        cmd.Parameters.Add("ORR_DATE_TIME ", wwp.RR_DATE_TIME);
        cmd.Parameters.Add("OCP_PLANNED ", wwp.CP_PLANNED);
        cmd.Parameters.Add("OCP_ACTUAL ", wwp.CP_ACTUAL);
        cmd.Parameters.Add("ORB_PLANNED ", wwp.RB_PLANNED);
        cmd.Parameters.Add("ORB_ACTUAL ", wwp.RB_ACTUAL);
        cmd.Parameters.Add("ODR_PLANNED ", wwp.DR_PLANNED);
        cmd.Parameters.Add("ODR_ACTUAL ", wwp.DR_ACTUAL);
        cmd.Parameters.Add("OPT_PLANED ", wwp.PT_PLANED);
        cmd.Parameters.Add("OPT_ACTUAL ", wwp.PT_ACTUAL);
        cmd.Parameters.Add("OCYCLE_SPEED ", wwp.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_SPPED ", wwp.COMMERCIAL_SPPED);
        cmd.Parameters.Add("OREMARKS", wwp.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }

    public string SubmitWellWiseDetails(WellWisePerformance wwp)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_WELL_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", wwp.SL_NO);
        cmd.Parameters.Add("OWELL_MONTH ", wwp.WELL_MONTH);
        cmd.Parameters.Add("OWELL_YEAR", wwp.WELL_YEAR);
        cmd.Parameters.Add("OBLOCK_TYPE ", wwp.BLOCK_TYPE);
        cmd.Parameters.Add("OBLOCK_CATEGORY ", wwp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OBLOCK_ID ", wwp.BLOCK_ID);//Field Id
        cmd.Parameters.Add("OWELL_NAME", wwp.WELL_NAME);
        cmd.Parameters.Add("OOPERATIONAL_AREA ", wwp.OPERATIONAL_AREA);
        cmd.Parameters.Add("OSTATE ", wwp.STATE);
        cmd.Parameters.Add("OS_LATITUDE ", wwp.S_LATITUDE);
        cmd.Parameters.Add("OS_LONGITUDE ", wwp.S_LONGITUDE);
        cmd.Parameters.Add("OSS_LATITUDE ", wwp.SS_LATITUDE);
        cmd.Parameters.Add("OSS_LONGITUDE ", wwp.SS_LONGITUDE);
        cmd.Parameters.Add("ORIG_NAME ", wwp.RIG_NAME);
        cmd.Parameters.Add("OWELL_CATEGORY_ID ", wwp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID ", wwp.WELL_TYPE_ID);
        cmd.Parameters.Add("OWATER_DEPTH ", wwp.WATER_DEPTH);
        cmd.Parameters.Add("OTARGET_DEPTH_MD ", wwp.TARGET_DEPTH_MD);
        cmd.Parameters.Add("OTARGET_DEPTH_TVD ", wwp.TARGET_DEPTH_TVD);
        cmd.Parameters.Add("ODRILLED_DEPTH_MD ", wwp.DRILLED_DEPTH_MD);
        cmd.Parameters.Add("ODRILLED_DEPTH_TVD ", wwp.DRILLED_DEPTH_TVD);
        cmd.Parameters.Add("OSPUD_DATE_TIME ", wwp.SPUD_DATE_TIME);
        cmd.Parameters.Add("OHER_DATE_TIME ", wwp.HER_DATE_TIME);
        cmd.Parameters.Add("ORR_DATE_TIME ", wwp.RR_DATE_TIME);
        cmd.Parameters.Add("OCP_PLANNED ", wwp.CP_PLANNED);
        cmd.Parameters.Add("OCP_ACTUAL ", wwp.CP_ACTUAL);
        cmd.Parameters.Add("ORB_PLANNED ", wwp.RB_PLANNED);
        cmd.Parameters.Add("ORB_ACTUAL ", wwp.RB_ACTUAL);
        cmd.Parameters.Add("ODR_PLANNED ", wwp.DR_PLANNED);
        cmd.Parameters.Add("ODR_ACTUAL ", wwp.DR_ACTUAL);
        cmd.Parameters.Add("OPT_PLANED ", wwp.PT_PLANED);
        cmd.Parameters.Add("OPT_ACTUAL ", wwp.PT_ACTUAL);
        cmd.Parameters.Add("OCYCLE_SPEED ", wwp.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_SPPED ", wwp.COMMERCIAL_SPPED);
        cmd.Parameters.Add("OREMARKS", wwp.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }

    public List<WellWisePerformance> SearchWellWise(WellWisePerformance wwp)
    {
      List<WellWisePerformance> ret = new List<WellWisePerformance>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_SEARCH_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        // OracleParameter op = new OracleParameter("data_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OMONTH_ID", wwp.WELL_MONTH);
        cmd.Parameters.Add("OYEAR_ID", wwp.WELL_YEAR);
        cmd.Parameters.Add("OBLOCK_ID", wwp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<WellWisePerformance>();

      }
      return ret;
    }

    public List<WellWisePerformance> GetWellWiseDetailsId(int Id)
    {
      List<WellWisePerformance> ret = new List<WellWisePerformance>();
      using (OracleConnection con = new OracleConnection(connection))
      {

        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DETAILS_BY_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_ID", Id);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<WellWisePerformance>();
      }
      return ret;

    }



    public string DraftUpdateWellWiseDetails(WellWisePerformance wwp)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_WELL_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", wwp.SL_NO);
        cmd.Parameters.Add("OWELL_MONTH ", wwp.WELL_MONTH);
        cmd.Parameters.Add("OWELL_YEAR", wwp.WELL_YEAR);
        cmd.Parameters.Add("OBLOCK_TYPE ", wwp.BLOCK_TYPE);
        cmd.Parameters.Add("OBLOCK_CATEGORY ", wwp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OBLOCK_ID ", wwp.BLOCK_ID);//Field Id
        cmd.Parameters.Add("OWELL_NAME", wwp.WELL_NAME);
        cmd.Parameters.Add("OOPERATIONAL_AREA ", wwp.OPERATIONAL_AREA);
        cmd.Parameters.Add("OSTATE ", wwp.STATE);
        cmd.Parameters.Add("OS_LATITUDE ", wwp.S_LATITUDE);
        cmd.Parameters.Add("OS_LONGITUDE ", wwp.S_LONGITUDE);
        cmd.Parameters.Add("OSS_LATITUDE ", wwp.SS_LATITUDE);
        cmd.Parameters.Add("OSS_LONGITUDE ", wwp.SS_LONGITUDE);
        cmd.Parameters.Add("ORIG_NAME ", wwp.RIG_NAME);
        cmd.Parameters.Add("OWELL_CATEGORY_ID ", wwp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID ", wwp.WELL_TYPE_ID);
        cmd.Parameters.Add("OWATER_DEPTH ", wwp.WATER_DEPTH);
        cmd.Parameters.Add("OTARGET_DEPTH_MD ", wwp.TARGET_DEPTH_MD);
        cmd.Parameters.Add("OTARGET_DEPTH_TVD ", wwp.TARGET_DEPTH_TVD);
        cmd.Parameters.Add("ODRILLED_DEPTH_MD ", wwp.DRILLED_DEPTH_MD);
        cmd.Parameters.Add("ODRILLED_DEPTH_TVD ", wwp.DRILLED_DEPTH_TVD);
        cmd.Parameters.Add("OSPUD_DATE_TIME ", wwp.SPUD_DATE_TIME);
        cmd.Parameters.Add("OHER_DATE_TIME ", wwp.HER_DATE_TIME);
        cmd.Parameters.Add("ORR_DATE_TIME ", wwp.RR_DATE_TIME);
        cmd.Parameters.Add("OCP_PLANNED ", wwp.CP_PLANNED);
        cmd.Parameters.Add("OCP_ACTUAL ", wwp.CP_ACTUAL);
        cmd.Parameters.Add("ORB_PLANNED ", wwp.RB_PLANNED);
        cmd.Parameters.Add("ORB_ACTUAL ", wwp.RB_ACTUAL);
        cmd.Parameters.Add("ODR_PLANNED ", wwp.DR_PLANNED);
        cmd.Parameters.Add("ODR_ACTUAL ", wwp.DR_ACTUAL);
        cmd.Parameters.Add("OPT_PLANED ", wwp.PT_PLANED);
        cmd.Parameters.Add("OPT_ACTUAL ", wwp.PT_ACTUAL);
        cmd.Parameters.Add("OCYCLE_SPEED ", wwp.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_SPPED ", wwp.COMMERCIAL_SPPED);
        cmd.Parameters.Add("OREMARKS", wwp.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string DraftSubmitWellWiseDetails(WellWisePerformance wwp)
    {
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_WELL_WISE_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", wwp.SL_NO);
        cmd.Parameters.Add("OWELL_MONTH ", wwp.WELL_MONTH);
        cmd.Parameters.Add("OWELL_YEAR", wwp.WELL_YEAR);
        cmd.Parameters.Add("OBLOCK_TYPE ", wwp.BLOCK_TYPE);
        cmd.Parameters.Add("OBLOCK_CATEGORY ", wwp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OBLOCK_ID ", wwp.BLOCK_ID);//Field Id
        cmd.Parameters.Add("OWELL_NAME", wwp.WELL_NAME);
        cmd.Parameters.Add("OOPERATIONAL_AREA ", wwp.OPERATIONAL_AREA);
        cmd.Parameters.Add("OSTATE ", wwp.STATE);
        cmd.Parameters.Add("OS_LATITUDE ", wwp.S_LATITUDE);
        cmd.Parameters.Add("OS_LONGITUDE ", wwp.S_LONGITUDE);
        cmd.Parameters.Add("OSS_LATITUDE ", wwp.SS_LATITUDE);
        cmd.Parameters.Add("OSS_LONGITUDE ", wwp.SS_LONGITUDE);
        cmd.Parameters.Add("ORIG_NAME ", wwp.RIG_NAME);
        cmd.Parameters.Add("OWELL_CATEGORY_ID ", wwp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID ", wwp.WELL_TYPE_ID);
        cmd.Parameters.Add("OWATER_DEPTH ", wwp.WATER_DEPTH);
        cmd.Parameters.Add("OTARGET_DEPTH_MD ", wwp.TARGET_DEPTH_MD);
        cmd.Parameters.Add("OTARGET_DEPTH_TVD ", wwp.TARGET_DEPTH_TVD);
        cmd.Parameters.Add("ODRILLED_DEPTH_MD ", wwp.DRILLED_DEPTH_MD);
        cmd.Parameters.Add("ODRILLED_DEPTH_TVD ", wwp.DRILLED_DEPTH_TVD);
        cmd.Parameters.Add("OSPUD_DATE_TIME ", wwp.SPUD_DATE_TIME);
        cmd.Parameters.Add("OHER_DATE_TIME ", wwp.HER_DATE_TIME);
        cmd.Parameters.Add("ORR_DATE_TIME ", wwp.RR_DATE_TIME);
        cmd.Parameters.Add("OCP_PLANNED ", wwp.CP_PLANNED);
        cmd.Parameters.Add("OCP_ACTUAL ", wwp.CP_ACTUAL);
        cmd.Parameters.Add("ORB_PLANNED ", wwp.RB_PLANNED);
        cmd.Parameters.Add("ORB_ACTUAL ", wwp.RB_ACTUAL);
        cmd.Parameters.Add("ODR_PLANNED ", wwp.DR_PLANNED);
        cmd.Parameters.Add("ODR_ACTUAL ", wwp.DR_ACTUAL);
        cmd.Parameters.Add("OPT_PLANED ", wwp.PT_PLANED);
        cmd.Parameters.Add("OPT_ACTUAL ", wwp.PT_ACTUAL);
        cmd.Parameters.Add("OCYCLE_SPEED ", wwp.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_SPPED ", wwp.COMMERCIAL_SPPED);
        cmd.Parameters.Add("OREMARKS", wwp.REMARKS);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }

    #endregion

    #region "DPR Code Start"
    public List<ddlListDPR> GetDPRList()
    {
      List<ddlListDPR> ret = new List<ddlListDPR>();
      try
      {
        var dd = "";
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_GET_ALL_DPR_DDL", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          cmd.Parameters.Add("DATA_CURSOR1", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("DATA_CURSOR2", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor3", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor4", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor5", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor6", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor7", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("data_cursor8", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
          // DataTable ds = new DataTable();
          DataSet ds = new DataSet();
          da.Fill(ds);
          ret = ds.Tables[0].ToList<ddlListDPR>();
          ret[0].OperationalArea = ds.Tables[1].ToList<OperationalArea>();
          ret[0].WellCatgory = ds.Tables[2].ToList<WellCatgory>();
          ret[0].OperatingStatus = ds.Tables[3].ToList<OperatingStatus>();
          ret[0].WellTypes = ds.Tables[4].ToList<WellType>();
          ret[0].States = ds.Tables[5].ToList<State>();
          ret[0].PhaseMaster = ds.Tables[6].ToList<PhaseMaster>();
          ret[0].DPRFields = ds.Tables[7].ToList<DPRField>();

          con.Close();
        }
      }
      catch (Exception e)
      {
        // ErrorHandlingLogSave(e.Message, "StateData");
      }
      return ret;
    }
    public string DraftDPRDetails(DPR dpr)
    {
      string ret = "";
      DateTime Dprdate = dpr.DPR_DATE.AddDays(1);

      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_DPR_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);       
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("ODPR_ID", dpr.DPR_ID);
        cmd.Parameters.Add("ODPR_DATE", Dprdate);
        cmd.Parameters.Add("OBLOCK_ID", dpr.BLOCK_ID);
        cmd.Parameters.Add("OOPERATION_AREA_ID", dpr.OPERATION_AREA_ID);
        cmd.Parameters.Add("OSTATE", dpr.STATE);
        cmd.Parameters.Add("ORIG_NAME", dpr.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATOR_STATUS_ID", dpr.RIG_OPERATOR_STATUS_ID);
        cmd.Parameters.Add("OWELL_NAME", dpr.WELL_NAME);
        cmd.Parameters.Add("OLATITUDE", dpr.LATITUDE);
        cmd.Parameters.Add("OLONGITUDE", dpr.LONGITUDE);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", dpr.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID", dpr.WELL_TYPE_ID);
        cmd.Parameters.Add("OSPUD_DATE", dpr.SPUD_DATE);
        cmd.Parameters.Add("OPHASE_ID", dpr.PHASE_ID);
        cmd.Parameters.Add("OTARGET_DEPTH", dpr.TARGET_DEPTH);
        cmd.Parameters.Add("OPRESENT_DEPTH", dpr.PRESENT_DEPTH);
        cmd.Parameters.Add("OMETARGE", dpr.METARGE);
        cmd.Parameters.Add("ODPR_BRIEF", dpr.DPR_BRIEF);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }

    public string SubmitDPRDetails(DPR dpr)
    {
      string ret = "";
      DateTime Dprdate = dpr.DPR_DATE.AddDays(1);
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_DPR_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("ODPR_ID", dpr.DPR_ID);
        cmd.Parameters.Add("ODPR_DATE", Dprdate);
        cmd.Parameters.Add("OBLOCK_ID", dpr.BLOCK_ID);
        cmd.Parameters.Add("OOPERATION_AREA_ID", dpr.OPERATION_AREA_ID);
        cmd.Parameters.Add("OSTATE", dpr.STATE);
        cmd.Parameters.Add("ORIG_NAME", dpr.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATOR_STATUS_ID", dpr.RIG_OPERATOR_STATUS_ID);
        cmd.Parameters.Add("OWELL_NAME", dpr.WELL_NAME);
        cmd.Parameters.Add("OLATITUDE", dpr.LATITUDE);
        cmd.Parameters.Add("OLONGITUDE", dpr.LONGITUDE);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", dpr.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID", dpr.WELL_TYPE_ID);
        cmd.Parameters.Add("OSPUD_DATE", dpr.SPUD_DATE);
        cmd.Parameters.Add("OPHASE_ID", dpr.PHASE_ID);
        cmd.Parameters.Add("OTARGET_DEPTH", dpr.TARGET_DEPTH);
        cmd.Parameters.Add("OPRESENT_DEPTH", dpr.PRESENT_DEPTH);
        cmd.Parameters.Add("OMETARGE", dpr.METARGE);
        cmd.Parameters.Add("ODPR_BRIEF", dpr.DPR_BRIEF);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
       
      }
      return ret;
    }

    public string DraftUpdateDPRDetails(DPR dpr)
    {
      //DateTime Dprdate = dpr.DPR_DATE.AddDays(1);
      string ret = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_DPR_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("ODPR_ID", dpr.DPR_ID);
        cmd.Parameters.Add("ODPR_DATE", dpr.DPR_DATE);
        cmd.Parameters.Add("OBLOCK_ID", dpr.BLOCK_ID);
        cmd.Parameters.Add("OOPERATION_AREA_ID", dpr.OPERATION_AREA_ID);
        cmd.Parameters.Add("OSTATE", dpr.STATE);
        cmd.Parameters.Add("ORIG_NAME", dpr.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATOR_STATUS_ID", dpr.RIG_OPERATOR_STATUS_ID);
        cmd.Parameters.Add("OWELL_NAME", dpr.WELL_NAME);
        cmd.Parameters.Add("OLATITUDE", dpr.LATITUDE);
        cmd.Parameters.Add("OLONGITUDE", dpr.LONGITUDE);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", dpr.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID", dpr.WELL_TYPE_ID);
        cmd.Parameters.Add("OSPUD_DATE", dpr.SPUD_DATE);
        cmd.Parameters.Add("OPHASE_ID", dpr.PHASE_ID);
        cmd.Parameters.Add("OTARGET_DEPTH", dpr.TARGET_DEPTH);
        cmd.Parameters.Add("OPRESENT_DEPTH", dpr.PRESENT_DEPTH);
        cmd.Parameters.Add("OMETARGE", dpr.METARGE);
        cmd.Parameters.Add("ODPR_BRIEF", dpr.DPR_BRIEF);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("CALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string DraftSubmitDPRDetails(DPR dpr)
    {
      string ret = "";
     // DateTime Dprdate = dpr.DPR_DATE.AddDays(1);
    
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_DPR_FORM_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("ODPR_ID", dpr.DPR_ID);
        cmd.Parameters.Add("ODPR_DATE", dpr.DPR_DATE);
        cmd.Parameters.Add("OBLOCK_ID", dpr.BLOCK_ID);
        cmd.Parameters.Add("OOPERATION_AREA_ID", dpr.OPERATION_AREA_ID);
        cmd.Parameters.Add("OSTATE", dpr.STATE);
        cmd.Parameters.Add("ORIG_NAME", dpr.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATOR_STATUS_ID", dpr.RIG_OPERATOR_STATUS_ID);
        cmd.Parameters.Add("OWELL_NAME", dpr.WELL_NAME);
        cmd.Parameters.Add("OLATITUDE", dpr.LATITUDE);
        cmd.Parameters.Add("OLONGITUDE", dpr.LONGITUDE);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", dpr.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID", dpr.WELL_TYPE_ID);
        cmd.Parameters.Add("OSPUD_DATE", dpr.SPUD_DATE);
        cmd.Parameters.Add("OPHASE_ID", dpr.PHASE_ID);
        cmd.Parameters.Add("OTARGET_DEPTH", dpr.TARGET_DEPTH);
        cmd.Parameters.Add("OPRESENT_DEPTH", dpr.PRESENT_DEPTH);
        cmd.Parameters.Add("OMETARGE", dpr.METARGE);
        cmd.Parameters.Add("ODPR_BRIEF", dpr.DPR_BRIEF);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
   public List<DPR> SearchDPR(DPR dpr)
    {
      List<DPR> ret = new List<DPR>();
      DateTime Dprdate = dpr.DPR_DATE.AddDays(1);
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DPR_SEARCH", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("ODPR_DATE", Dprdate);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<DPR>();

      }
      return ret;
    }
   public List<DPR> GetDPRDetailsId(int Id)
    {
      List<DPR> ret = new List<DPR>();
      using (OracleConnection con = new OracleConnection(connection))
      {

        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DETAILS_BY_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_ID", Id);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "6");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<DPR>();
      }
      return ret;

    }

    #endregion
    
    #region "Commulative Drilling Paln Code Start"

    public string DraftCDPDetails(CumulativeDrillingPerformance cdp)
    {
      string ret = "";
      double a = Convert.ToDouble(cdp.METARAGE);
      double b = Convert.ToDouble(cdp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_CUMULATIVE_DRILLING_FORM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", cdp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", cdp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", cdp.YEAR);
        cmd.Parameters.Add("OMONTH", cdp.MONTH);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", cdp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", cdp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", cdp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", cdp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", cdp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", cdp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", cdp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string SubmitCDPDetails(CumulativeDrillingPerformance cdp)
    {
      string ret = "";
      double a = Convert.ToDouble(cdp.METARAGE);
      double b = Convert.ToDouble(cdp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_CUMULATIVE_DRILLING_FORM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", cdp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", cdp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", cdp.YEAR);
        cmd.Parameters.Add("OMONTH", cdp.MONTH);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", cdp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", cdp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", cdp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", cdp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", cdp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", cdp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", cdp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();

      }
      return ret;
    }
    public string DraftUpdateCDPDetails(CumulativeDrillingPerformance cdp)
    {
      string ret = "";
      double a = Convert.ToDouble(cdp.METARAGE);
      double b = Convert.ToDouble(cdp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_CUMULATIVE_DRILLING_FORM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", cdp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", cdp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", cdp.YEAR);
        cmd.Parameters.Add("OMONTH", cdp.MONTH);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", cdp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", cdp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", cdp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", cdp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", cdp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", cdp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", cdp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("CALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string DraftSubmitCDPDetails(CumulativeDrillingPerformance cdp)
    {
      string ret = "";
      double a = Convert.ToDouble(cdp.METARAGE);
      double b = Convert.ToDouble(cdp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();

      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_CUMULATIVE_DRILLING_FORM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", cdp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", cdp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", cdp.YEAR);
        cmd.Parameters.Add("OMONTH", cdp.MONTH);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", cdp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", cdp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", cdp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", cdp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", cdp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", cdp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", cdp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public List<CumulativeDrillingPerformance> SearchCDP(CumulativeDrillingPerformance cdp)
    {
      List<CumulativeDrillingPerformance> ret = new List<CumulativeDrillingPerformance>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_SEARCH_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OMONTH_ID", cdp.MONTH);
        cmd.Parameters.Add("OYEAR_ID", cdp.YEAR);
        cmd.Parameters.Add("OBLOCK_ID", cdp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<CumulativeDrillingPerformance>();
      }
      return ret;
    }
    public List<CumulativeDrillingPerformance> GetCDPDetailsId(int Id)
    {
      List<CumulativeDrillingPerformance> ret = new List<CumulativeDrillingPerformance>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DETAILS_BY_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_ID", Id);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<CumulativeDrillingPerformance>();
      }
      return ret;

    }
    #endregion

    #region Annul Drilling Plan Code Start Here"

    public string DraftADPDetails(AnnualDrillingPlan adp)
    {
      string ret = "";
      double a = Convert.ToDouble(adp.METARAGE);
      double b = Convert.ToDouble(adp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_ANNUAL_DRILLING_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;   
        cmd.Parameters.Add("OSL_NO", adp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", adp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", adp.YEAR);
        cmd.Parameters.Add("OQUARTER_NO", adp.QUARTER_NO);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", adp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", adp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", adp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED",adp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", adp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", adp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", adp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string SubmitADPDetails(AnnualDrillingPlan adp)
    {
      string ret = "";
      double a = Convert.ToDouble(adp.METARAGE);
      double b = Convert.ToDouble(adp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_ANNUAL_DRILLING_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", adp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", adp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", adp.YEAR);
        cmd.Parameters.Add("OQUARTER_NO", adp.QUARTER_NO);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", adp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", adp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", adp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", adp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", adp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", adp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", adp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "2");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();

      }
      return ret;
    }
    public string DraftUpdateADPDetails(AnnualDrillingPlan adp)
    {
      string ret = "";
      double a = Convert.ToDouble(adp.METARAGE);
      double b = Convert.ToDouble(adp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();

      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_ANNUAL_DRILLING_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", adp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", adp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", adp.YEAR);
        cmd.Parameters.Add("OQUARTER_NO", adp.QUARTER_NO);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", adp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", adp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", adp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", cs);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", adp.COMMERCIAL_SPEED);
        cmd.Parameters.Add("OMETARAGE", adp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", adp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", adp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("CALLVAL", "4");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public string DraftSubmitADPDetails(AnnualDrillingPlan adp)
    {
      string ret = "";
      double a = Convert.ToDouble(adp.METARAGE);
      double b = Convert.ToDouble(adp.RIG_MONTHS);
      double c = a / b;
      string cs = c.ToString();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_ANNUAL_DRILLING_SUBMISSION", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OSL_NO", adp.SL_NO);
        cmd.Parameters.Add("OBLOCK_CATEGORY", adp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OYEAR", adp.YEAR);
        cmd.Parameters.Add("OQUARTER_NO", adp.QUARTER_NO);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", adp.WELL_CATEGORY_ID);
        cmd.Parameters.Add("ORIG_OPERATE_STATUS", adp.RIG_OPERATE_STATUS);
        cmd.Parameters.Add("OOPERATION_AREA_ID", adp.OPERATION_AREA_ID);
        cmd.Parameters.Add("OCYCLE_SPEED", adp.CYCLE_SPEED);
        cmd.Parameters.Add("OCOMMERCIAL_SPEED", cs);
        cmd.Parameters.Add("OMETARAGE", adp.METARAGE);
        cmd.Parameters.Add("ONO_WELLS", adp.NO_WELLS);
        cmd.Parameters.Add("ORIG_MONTHS", adp.RIG_MONTHS);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OCALLVAL", "3");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.Rows[0][0].ToString();
      }
      return ret;
    }
    public List<AnnualDrillingPlan> SearchADP(AnnualDrillingPlan adp)
    {
      List<AnnualDrillingPlan> ret = new List<AnnualDrillingPlan>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_SEARCH_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OMONTH_ID", adp.QUARTER_NO);
        cmd.Parameters.Add("OYEAR_ID", adp.YEAR);
        cmd.Parameters.Add("OBLOCK_ID", adp.BLOCK_CATEGORY);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OCALLVAL", "5");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<AnnualDrillingPlan>();
      }
      return ret;
    }
    public List<AnnualDrillingPlan> GetADPDetailsId(int Id)
    {
      List<AnnualDrillingPlan> ret = new List<AnnualDrillingPlan>();
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_FORM_DETAILS_BY_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("data_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_ID", Id);
        cmd.Parameters.Add("OOPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("CALLVAL", "5");
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = ds.ToList<AnnualDrillingPlan>();
      }
      return ret;

    }



    #endregion


    public string get_RIGNODataExist1(string month, string year, string block_category)
    {
      string result = string.Empty;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_NORIGDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_NORIGDATAEXIST", results))

        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        //cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
        cmd.Parameters.Add("P_CallVall",1);
        DataSet ds = new DataSet();
        da.Fill(ds);
          if (ds.Tables[0].Rows.Count == 0)
          {
            result = "";
          }
          else
          {
            result = ds.Tables[0].Rows[0]["USERID"].ToString();
          }
        }
      return result;
    }

    public string get_RIGNODataExist2(string month, string year, string block_category)
    {
      string result = string.Empty;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_NORIGDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_NORIGDATAEXIST", results))

        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        //cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
        cmd.Parameters.Add("P_CallVall", 2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string get_RIGNODataDELETE(string month, string year, string block_category)
    {
      string result = string.Empty;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_NORIGDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_NORIGDATAEXIST", results))

        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        //cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
        cmd.Parameters.Add("P_CallVall", 3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0][0].ToString();
        }
      }
      return result;
    }

    public string DownloadTempleteRigCountExcel(RigCountDetails rig ,string _filePath)
    {
      string ExcelMessage = "";
      string filenm="";
      string block = "";
      string blknom = "";
      string monthname = "";
     string month = rig.Months.ToString();
      string year = rig.Years.ToString();
      string block_category =rig.BlockTypes.ToString();
      string op_name = OPERATOR_NAME; 
      if (block_category == "1")
      {
        block = "NOMINATION";
        blknom = "NOM";
      }
      if (block_category == "2")
      {
        block = "ALL";
        blknom = "ALL";
      }
      if (month == "1") { monthname = "January"; }
      if (month == "2") { monthname = "February"; }
      if (month == "3") { monthname = "March"; }
      if (month == "4") { monthname = "April"; }
      if (month == "5") { monthname = "May"; }
      if (month == "6") { monthname = "June"; }
      if (month == "7") { monthname = "July"; }
      if (month == "8") { monthname = "August"; }
      if (month == "9") { monthname = "September"; }
      if (month == "10") { monthname = "October"; }
      if (month == "11") { monthname = "November"; }
      if (month == "12") { monthname = "December"; }

      try
      {
        string opr_name = op_name.Replace("&amp;", "&");
        // string uid = WebSession.UserId.ToString();
        string uid = USERID;
        //List<ListBlock> retList = null;
        string date = "01/" + month + "/" + year;
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        filenm = "RIG_COUNT" + "_" + blknom + ".xlsx";
      
        string filenmt = "Excel_NO_RIG.xlsx";
       
        //var fpath = "C:/Users/Administrator/Desktop/PDMS/src/app/routes/drilling/Upload/NoRigExcel_Template/";
        var fpath = _filePath+"/Upload/NoRigExcel_Template/";
        FileInfo template = new FileInfo(fpath + filenmt);
        FileInfo newFile = new FileInfo(fpath + filenm);
        System.IO.File.Delete(fpath+ filenm);
        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
          foreach (ExcelWorksheet aworksheet in excelPackage.Workbook.Worksheets)
          {
            aworksheet.Cells[1, 1].Value = aworksheet.Cells[1, 1].Value;
          }
          // Getting the complete workbook...
          ExcelWorkbook myWorkbook = excelPackage.Workbook;
          // Getting the worksheet by its name... 
          ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];
          //ExcelWorksheet myWorksheet1 = myWorkbook.Worksheets["Block List"];
          int startrow = 3;
          //int block_row = 4;
          //retList = GetOperatorList(op_id);
          myWorksheet.Cells[startrow, 3].Value = opr_name.ToString();
          myWorksheet.Cells[startrow + 3, 2].Value = monthname + "_" + year;
          myWorksheet.Cells[startrow, 7].Value = block;
          // Saving the change... 
          excelPackage.Save();
          // System.Diagnostics.Process.Start("C:/Users/Administrator/Desktop/PDMS/src/app/routes/drilling/Upload/NoRigExcel_Template/" + filenm +" ");
         
        }
        ExcelMessage = "Excel report created successfully!";
      }

      catch (Exception ex)
      {
       // ExcelMessage = "Oops! Something went wrong";
        throw ex;
      }
      return ExcelMessage;     
      
    }

   public int UploadNoRigExcel(int slno, string operator_id, int month, int year, int rig_op_status_id, int well_category_id, int operation_area_id, int no_rig, int block_cateogy, string remarks,string Status)
    {
      int ret = '0';     
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_INSERT_DRL_RIG_EXCEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_OPERATOR_ID", operator_id);
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_WELL_CATEGORY", well_category_id);
        cmd.Parameters.Add("P_OPERATION_AREA", operation_area_id);
        cmd.Parameters.Add("P_OPERATOR_STATUS", rig_op_status_id);
        cmd.Parameters.Add("P_UpdatedBy", USERID);
        cmd.Parameters.Add("P_TOTAL_RIG", no_rig);
        cmd.Parameters.Add("P_IP", "");
        cmd.Parameters.Add("P_SNO", slno);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_cateogy);
        cmd.Parameters.Add("P_STATUS", Status);
        cmd.Parameters.Add("P_REMARKS", remarks);
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret =Convert.ToInt32(ds.Rows[0][0]);
      }
      return ret;    


     
    }

    public List<DPRField> GetOperatorALLBlockList(int op_id, int opr_id)
    {
      List<DPRField> ret = new List<DPRField>();
      try
      {
        var dd = "";
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_GET_DPR_FIELD_EXCEL", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          cmd.Parameters.Add("DATA_CURSOR1", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("P_OPERATOR_ID", op_id);
          cmd.Parameters.Add("P_OPR_ID", opr_id);
          // DataTable ds = new DataTable();
          DataSet ds = new DataSet();
          da.Fill(ds);
          ret = ds.Tables[0].ToList<DPRField>();
          con.Close();
        }
      }
      catch (Exception e)
      {
        // ErrorHandlingLogSave(e.Message, "StateData");
      }
      return ret;
    }

    public int[] GetBlockID(string block_name)
    {
      int[] block_exist = { 0, 0 };
      int[] result = { 0, 0 };

      using (OracleConnection con = new OracleConnection(connection))        
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_BLOCK_EXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
     //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_NAME", block_name);

        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          block_exist[0] = 0;
          block_exist[1] = 0;

        }
        else
        {
          block_exist[0] = Convert.ToInt32(ds.Tables[0].Rows[0]["BLOCK_ID"]);
          block_exist[1] = Convert.ToInt32(ds.Tables[0].Rows[0]["BLOCK_TYPE"]);
        }
      }

      if (block_exist[0] == 0)
      {
        result[0] = 0;
        result[1] = 0;
      }
      else
      {
        result = block_exist;
      }

      return result;
    }


    public string get_DPRDataExist1(DateTime dpr_date)
    {
      string result = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_DPRDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_DPR_DATE", dpr_date);
       // cmd.Parameters.Add("P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", "1");
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string get_DPRDataExist2(DateTime dpr_date)
    {
      string result = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_DPRDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_DPR_DATE", dpr_date);
        // cmd.Parameters.Add("P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }


    public string delete_DPRData(DateTime dpr_date)
    {
      string result = "";
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_DPRDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_DPR_DATE", dpr_date);
        // cmd.Parameters.Add("P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0][0].ToString();
        }
      }
      return result;
    }

    public int InsertDrillingDPR(DPR dpr,string STATUS)
    {
      int ret = '0';
      DateTime dprdt = dpr.DPR_DATE;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_INSERT_DPR_EXCEL_DATA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("ODPR_ID", dpr.DPR_ID);
        cmd.Parameters.Add("ODPR_DATE", dprdt);
        cmd.Parameters.Add("OBLOCK_ID", dpr.BLOCK_ID);
        cmd.Parameters.Add("OOPERATION_AREA_ID", dpr.OPERATION_AREA_ID);
        cmd.Parameters.Add("OSTATE", dpr.STATE);
        cmd.Parameters.Add("ORIG_NAME", dpr.RIG_NAME);
        cmd.Parameters.Add("ORIG_OPERATOR_STATUS_ID", dpr.RIG_OPERATOR_STATUS_ID);
        cmd.Parameters.Add("OWELL_NAME", dpr.WELL_NAME);
        cmd.Parameters.Add("OLATITUDE", dpr.LATITUDE);
        cmd.Parameters.Add("OLONGITUDE", dpr.LONGITUDE);
        cmd.Parameters.Add("OWELL_CATEGORY_ID", dpr.WELL_CATEGORY_ID);
        cmd.Parameters.Add("OWELL_TYPE_ID", dpr.WELL_TYPE_ID);
        cmd.Parameters.Add("OSPUD_DATE", dpr.SPUD_DATE);
        cmd.Parameters.Add("OPHASE_ID", dpr.PHASE_ID);
        cmd.Parameters.Add("OTARGET_DEPTH", dpr.TARGET_DEPTH);
        cmd.Parameters.Add("OPRESENT_DEPTH", dpr.PRESENT_DEPTH);
        cmd.Parameters.Add("OMETARGE", dpr.METARGE);
        cmd.Parameters.Add("ODPR_BRIEF", dpr.DPR_BRIEF);
        cmd.Parameters.Add("OUSERID", USERID);
        cmd.Parameters.Add("OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OSTATUS", STATUS);
        cmd.Parameters.Add("OCALLVAL", "1");
        DataTable ds = new DataTable();
        da.Fill(ds);      
        ret = Convert.ToInt32(ds.Rows[0][0]);
      }
      return ret;
    }

    public string CreateDPRExcel(string op_name, DateTime dpr_date, int op_id, string _filePath)
    {
      string ExcelMessage = "";
      List<DPRField> retList = null;
      int opr_id = 0;
      if (op_id == 44)
      {
        opr_id = 2;
      }
      else if (op_id == 45)
      {
        opr_id = 3;
      }
      else
      {
        opr_id = op_id;
      }
      retList = GetOperatorALLBlockList(op_id, opr_id);
      try
      {
        string opr_name = op_name.Replace("&amp;", "&");
        string uid =USERID;
        //List<ListBlock> retList = null;
        string d;
        d = dpr_date == null ? null : Convert.ToDateTime(dpr_date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "DPR" + ".xlsm";
        string filenmt = "DRILLING_DPR.xlsm";
        // var fpath = System.Web.Hosting.HostingEnvironment.MapPath("~/DPRExcel_Template/");
        var fpath = _filePath + "/Upload/DPRExcel_Template/";
        FileInfo template = new FileInfo(fpath + filenmt);
        FileInfo newFile = new FileInfo(fpath + filenm);
        // Using the template to create the newFile...
        System.IO.File.Delete(fpath + filenm);
        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
          foreach (ExcelWorksheet aworksheet in excelPackage.Workbook.Worksheets)
          {
            aworksheet.Cells[1, 1].Value = aworksheet.Cells[1, 1].Value;
          }
          // Getting the complete workbook...
          ExcelWorkbook myWorkbook = excelPackage.Workbook;
          // Getting the worksheet by its name... 
          ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];
          //ExcelWorksheet myWorksheet1 = myWorkbook.Worksheets["Block List"];

          int startcol = 2;
          int block_row = 8;
          //retList = GetOperatorList(op_id);
          myWorksheet.Cells[3, startcol].Value = opr_name.ToString();
          myWorksheet.Cells[4, startcol].Value = r;
          int len = retList.Count;
          for (int i = 0; i < len; i++)
          {
            myWorksheet.Cells[block_row + i, 20].Value = retList[i].FIELD_NAME;
            //string blk_name = retList[0].BLOCK_NAME.ToString();
          }

          excelPackage.Save();
        }
        ExcelMessage = "Excel report created successfully!";

      }
      catch (Exception ex)
      {
        ExcelMessage = "Oops! Something went wrong.";

      }
      return ExcelMessage;    

    }

   
    public string CreateTempleteRigWiseExcel(string month, int year, int op_id, string block_category, string _filePath)
    {   
      string ExcelMessage = "";    
      string block = "";
      string blknom = "";
      if (block_category == "1")
      {

        block = "NOMINATION";
        blknom = "NOM";
      }
      if (block_category == "2")
      {
        block = "ALL";
        blknom = "ALL";
      }  
      try
      {
        string opr_name =OPERATOR_NAME.Replace("&amp;", "&");
        string uid =USERID;
        string date = "01/" + month + "/" + year;
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "RIG_WISE_PERFORMANCE" + "_" + blknom + ".xlsm";
        string filenmt = "Excel_RigEntry_Template.xlsm";
        var fpath = _filePath + "/Upload/RigExcel_Template/";
        FileInfo template = new FileInfo(fpath + filenmt);
        FileInfo newFile = new FileInfo(fpath + filenm);
        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
          foreach (ExcelWorksheet aworksheet in excelPackage.Workbook.Worksheets)
          {
            aworksheet.Cells[1, 1].Value = aworksheet.Cells[1, 1].Value;
          }
          // Getting the complete workbook...
          ExcelWorkbook myWorkbook = excelPackage.Workbook;
          // Getting the worksheet by its name... 
          ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];
          // ExcelWorksheet myWorksheet1 = myWorkbook.Worksheets["Block List"];
          int startrow = 3;
          int block_row = 9;
          //   retList = GetOperatorList(op_id);
          myWorksheet.Cells[startrow, 3].Value = opr_name.ToString();
          myWorksheet.Cells[startrow, 6].Value = month + "_" + year;
          myWorksheet.Cells[startrow, 9].Value = block;
          // Saving the change... 
          excelPackage.Save();
        }
        ExcelMessage = "Excel report created successfully!";
      }
      catch (Exception ex)
      {
        ExcelMessage = "Oops! Something went wrong.";

      }
      return ExcelMessage;
    }


   public int UploadExcelRigWiseExcel(int slno, int operatiorid, int operation_area_id, int blockid, int block_type, string rigname, int operating_status_id, string rig_type, int? exp_well, double? exp_met, int? dev_well, double? dev_met, double? rig_mode_time_rb, double? rig_mode_time_dr, double? rig_mode_time_pt, double? outcycle_caprep, double? outcycle_oth, double? npd_complication_day, double? npd_complication_per, double? npd_repair_day, double? npd_repair_per, double? cycle_day, double? comm_day, double? cycle_exp, double? cycle_dev, double? cyclespeed, double? comm_exp, double? comm_dev, double? commspeed, string remarks, int month, int year, string submissiondate, int block_category,string Status)
    {
      int result = 0;
      //DPRMaster DP = new DPRMaster();
      //string ip = DP.GetUser_IP();

      //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INSERT_DRL_RIGDATA", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_INSERT_RIG_WISE_EXCEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_SNO", slno);
        cmd.Parameters.Add("P_OPERATOR_ID", operatiorid);
        cmd.Parameters.Add("P_OPERATION_AREA_ID", operation_area_id);
        cmd.Parameters.Add("P_BLOCK_ID", blockid);
        cmd.Parameters.Add("P_RIG_NAME", rigname);
        cmd.Parameters.Add("P_RIG_OPERATING_STATUS", operating_status_id);
        cmd.Parameters.Add("P_RIG_TPYE", rig_type);
        cmd.Parameters.Add("P_EXP_WELLS", exp_well);
        cmd.Parameters.Add("P_EXP_MET", exp_met);
        cmd.Parameters.Add("P_DEV_WELLS", dev_well);
        cmd.Parameters.Add("P_DEV_MET", dev_met);
        cmd.Parameters.Add("P_RIG_MODE_TIME_BREAKEUP_RB", rig_mode_time_rb);
        cmd.Parameters.Add("P_RIG_MODE_TIME_BREAKEUP_DR", rig_mode_time_dr);
        cmd.Parameters.Add("P_RIG_MODE_TIME_BREAKEUP_PT", rig_mode_time_pt);
        cmd.Parameters.Add("P_OUTCYCLE_CAPREP", outcycle_caprep);
        cmd.Parameters.Add("P_OUTCYCLE_OTH", outcycle_oth);
        cmd.Parameters.Add("P_NONPRD_WELL_COMPLICATION_DAY", npd_complication_day);
        cmd.Parameters.Add("P_NONPRD_WELL_COMPLICATION_PER", npd_complication_per);
        cmd.Parameters.Add("P_NONPRD_WELL_REPAIR_DAY", npd_repair_day);
        cmd.Parameters.Add("P_NONPRD_WELL_REPAIR_PER", npd_repair_per);
        cmd.Parameters.Add("P_CYCLE_DAYS", cycle_day);
        cmd.Parameters.Add("P_COMM_DAYS", comm_day);
        cmd.Parameters.Add("P_CYCLE_EXP", cycle_exp);
        cmd.Parameters.Add("P_CYCLE_DEV", cycle_dev);
        cmd.Parameters.Add("P_CYCLE_SPEED", cyclespeed);
        cmd.Parameters.Add("P_COMM_EXP", comm_exp);
        cmd.Parameters.Add("P_COMM_DEV", comm_dev);
        cmd.Parameters.Add("P_COMM_SPEED", commspeed);
        cmd.Parameters.Add("P_REMARKS", remarks);
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_UpdatedBy", USERID);
        cmd.Parameters.Add("P_IP", "");
        cmd.Parameters.Add("P_BLOCK_TYPE", block_type);
        cmd.Parameters.Add("P_SUBMISSION_DATE", submissiondate);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
        cmd.Parameters.Add("P_STATUS", Status);
        DataSet ds = new DataSet();
        da.Fill(ds);
        result =Convert.ToInt32( ds.Tables[0].Rows[0][0]);
        }
        return result;
      }

   public string get_RIGDataExist1(string month, string year, string block)
   {
      string result = string.Empty;
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_RIGDATAEXIST", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_RIGDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add ("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
       // cmd.Parameters.Add( "P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add ("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_CallVall", 1);
        DataSet ds = new DataSet();
        da.Fill(ds);
         
          if (ds.Tables[0].Rows.Count == 0)
          {
            result = "";
          }
          else
          {
            result = ds.Tables[0].Rows[0]["USERID"].ToString();
          }
        }   
      return result;
    }

    public string get_RIGDataExist2(string month, string year, string block)
    {
      string result = string.Empty;
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_RIGDATAEXIST", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_RIGDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        // cmd.Parameters.Add( "P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_CallVall", 2);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string get_RIG_Wise_delete(string month, string year, string block)
    {
      string result = string.Empty;
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_RIGDATAEXIST", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_RIGDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        // cmd.Parameters.Add( "P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_CallVall", 3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0][0].ToString();
        }
      }
      return result;
    }
    public List<DPRField> GetOperatorList(int OP_ID)
    {
      List<DPRField> retList = null;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
      OracleCommand cmd = new OracleCommand("DRL_GET_BLOCKLIST_DRILLING", con);
      cmd.CommandType = CommandType.StoredProcedure;
      OracleDataAdapter da = new OracleDataAdapter(cmd);
      cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
      da.Fill(ds);
     // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_BLOCKLIST_DRILLING", results))    
        retList = ds.Tables[0].ToList<DPRField>();
      }
      return retList;
    }

    public List<DPRField> GetOperatorNOMBlockList(int OP_ID)
    {
      //  using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_NOM_BLOCKLIST_DRL", results))
      List<DPRField> retList = null;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_NOM_BLOCKLIST_DRL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
        da.Fill(ds);       
        retList = ds.Tables[0].ToList<DPRField>();
      }
      return retList;
    }

    public List<State> GetStatae()
    {
      List<State> retList = null;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_STATE_LIST_DRL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        //cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
        da.Fill(ds);
        retList = ds.Tables[0].ToList<State>();
      }
      return retList;

    }

    public List<DPRField> GetOperatorCBMBlockList(int OP_ID)
    {
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_CBM_BLOCKLIST_DRL", results))
      List<DPRField> retList = null;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_CBM_BLOCKLIST_DRL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
        da.Fill(ds);
        retList = ds.Tables[0].ToList<DPRField>();
      }
      return retList;
    }
    public List<DPRField> GetOperatorNELPBlockList(int OP_ID)
    {
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_NELP_BLOCKLIST_DRL", results))
      List<DPRField> retList = null;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_NELP_BLOCKLIST_DRL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
        da.Fill(ds);
        retList = ds.Tables[0].ToList<DPRField>();
      }
      return retList;
    }
    public List<DPRField> GetOperatorPRENELPBlockList(int OP_ID)
    {
      List<DPRField> retList = null;
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_PRENELP_BLOCKLIST_DRL", results))     
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_PRENELP_BLOCKLIST_DRL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
        da.Fill(ds);
         retList = ds.Tables[0].ToList<DPRField>();
      }
      return retList;
    }
    public List<DPRField> GetOperatorSHELLGASBlockList(int OP_ID)
    {
      // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_SHELLGAS_BLOCKLIST_DRL", results))
      List<DPRField> retList = null;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_SHELLGAS_BLOCKLIST_DRL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //   cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("OPERATORID", OP_ID);
        DataSet ds = new DataSet();
        da.Fill(ds);
        retList = ds.Tables[0].ToList<DPRField>();
      }
      return retList;
    }

    public string CreateTempleteWellWiseExcel(string month, int year, int op_id, string block_category, string _filePath)
    {
      int opr_id = 0;
      if (op_id == 44)
      {
        opr_id = 2;
      }
      else if (op_id == 45)
      {
        opr_id = 3;
      }
      else
      {
        opr_id = op_id;
      }

      string ExcelMessage = "";
      List<DPRField> retListnom = null;
      List<DPRField> retListnelp = null;
      List<DPRField> retListprenelp = null;
      List<DPRField> retListcbm = null;
      List<DPRField> retListshellgas = null;


      List<State> retState = null;
      retState = GetStatae();

     // List<State> retState = GetStatae();

      List<string> retblkcat = new List<string>();
      string block = "";
      string blknom = "";
      if (block_category == "1")
      {
        block = "NOMINATION";
        blknom = "NOM";
        retblkcat.Add("NOMINATION");
      }
      if (block_category == "2")
      {
        block = "ALL";
        blknom = "ALL";
        retblkcat.Add("NELP");
        retblkcat.Add("PRE_NELP");
        retblkcat.Add("NOMINATION");
        retblkcat.Add("CBM");
        retblkcat.Add("SHALE_GAS");
      }

      if (block_category == "1")
        retListnom = GetOperatorNOMBlockList(opr_id);
      if (block_category == "2")
      {
        retListnom = GetOperatorNOMBlockList(opr_id);
        retListnelp = GetOperatorNELPBlockList(op_id);
        retListprenelp = GetOperatorPRENELPBlockList(op_id);
        retListcbm = GetOperatorCBMBlockList(op_id);
        retListshellgas = GetOperatorSHELLGASBlockList(op_id);
      }

      try
      {
        string opr_name =OPERATOR_NAME.Replace("&amp;", "&");
        string uid = USERID.ToString();
        //List<ListBlock> retList = null;

        string date = "01/" + month + "/" + year;
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "WELL_WISE_PERFORMANCE" + "_" + blknom + ".xlsm";
        string filenmt = "Excel_WellEntry_Template.xlsm";
        var fpath = _filePath + "/Upload/WellExcel_Template/";
      

        FileInfo template = new FileInfo(fpath + filenmt);
        FileInfo newFile = new FileInfo(fpath + filenm);

        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
          foreach (ExcelWorksheet aworksheet in excelPackage.Workbook.Worksheets)
          {
            aworksheet.Cells[1, 1].Value = aworksheet.Cells[1, 1].Value;
          }
          // Getting the complete workbook...
          ExcelWorkbook myWorkbook = excelPackage.Workbook;
          // Getting the worksheet by its name... 
          ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];
          ExcelWorksheet myWorksheet1 = myWorkbook.Worksheets["Sheet3"];
          // ExcelWorksheet myWorksheet1 = myWorkbook.Worksheets["Block List"];
          int startrow = 3;

          // retList = GetOperatorList(op_id);
          myWorksheet.Cells[startrow, 3].Value = opr_name.ToString();
          myWorksheet.Cells[startrow, 6].Value = month + "_" + year;
          myWorksheet.Cells[startrow, 8].Value = block;

          int lenblkcat = retblkcat.Count;
          for (int i = 0; i < lenblkcat; i++)
          {
            myWorksheet1.Cells[3 + i, 2].Value = retblkcat[i].ToString();

          }

          if (lenblkcat == 1)
          {

            myWorkbook.Names.Add("BLOCK_TYPE", myWorksheet1.Cells["B3"]);
          }
          else if (lenblkcat > 1)
          {
            myWorkbook.Names.Add("BLOCK_TYPE", myWorksheet1.Cells["B3:B7"]);
          }
                 
                  


          //nomination
          List<DPRField> dislistnom = retListnom.GroupBy(x => x.FIELD_NAME).Select(y => y.FirstOrDefault()).ToList();
          int lennom = dislistnom.Count;

          for (int i = 0; i < lennom; i++)
          {
            myWorksheet1.Cells[3 + i, 10].Value = dislistnom[i].FIELD_NAME;

          }
          int nomendrange = lennom + 2;
          if (lennom == 0 || lennom == 1)
          {
            myWorkbook.Names.Add("NOMINATION", myWorksheet1.Cells["J3"]);
          }
          else
          {
            myWorkbook.Names.Add("NOMINATION", myWorksheet1.Cells["J3:J" + nomendrange]);
          }


          if (block_category == "2")
          {
            //nelp
            List<DPRField> dislistnelp = retListnelp.GroupBy(x => x.FIELD_NAME).Select(y => y.FirstOrDefault()).ToList();
            int lennelp = dislistnelp.Count;
            for (int i = 0; i < lennelp; i++)
            {
              myWorksheet1.Cells[3 + i, 11].Value = dislistnelp[i].FIELD_NAME;

            }
            int nelpendrange = lennelp + 2;
            if (lennelp == 0 || lennelp == 1)
            {
              myWorkbook.Names.Add("NELP", myWorksheet1.Cells["K3"]);
            }
            else
            {
              myWorkbook.Names.Add("NELP", myWorksheet1.Cells["K3:K" + nelpendrange]);
            }


            //pre nelp
            List<DPRField> dislistprenelp = retListprenelp.GroupBy(x => x.FIELD_NAME).Select(y => y.FirstOrDefault()).ToList();
            int lenprenelp = dislistprenelp.Count;
            for (int i = 0; i < lenprenelp; i++)
            {
              myWorksheet1.Cells[3 + i, 12].Value = dislistprenelp[i].FIELD_NAME;

            }
            int prenelpendrange = lenprenelp + 2;
            if (lenprenelp == 0 || lenprenelp == 1)
            {
              myWorkbook.Names.Add("PRE_NELP", myWorksheet1.Cells["L3"]);
            }
            else
            {
              myWorkbook.Names.Add("PRE_NELP", myWorksheet1.Cells["L3:L" + prenelpendrange]);
            }


            //cbm
            List<DPRField> dislistcbm = retListcbm.GroupBy(x => x.FIELD_NAME).Select(y => y.FirstOrDefault()).ToList();
            int lencbm = dislistcbm.Count;
            for (int i = 0; i < lencbm; i++)
            {
              myWorksheet1.Cells[3 + i, 13].Value = dislistcbm[i].FIELD_NAME;

            }
            int cbmendrange = lencbm + 2;
            if (lencbm == 0 || lencbm == 1)
            {
              myWorkbook.Names.Add("CBM", myWorksheet1.Cells["M3"]);
            }
            else
            {
              myWorkbook.Names.Add("CBM", myWorksheet1.Cells["M3:M" + cbmendrange]);
            }


            //shell gas
            List<DPRField> dislistshellgas = retListshellgas.GroupBy(x => x.FIELD_NAME).Select(y => y.FirstOrDefault()).ToList();
            int lenshellgas = dislistshellgas.Count;
            for (int i = 0; i < lenshellgas; i++)
            {
              myWorksheet1.Cells[3 + i, 14].Value = dislistshellgas[i].FIELD_NAME;

            }
            int shellgasendrange = lenshellgas + 2;
            if (lenshellgas == 0 || lenshellgas == 1)
            {
              myWorkbook.Names.Add("SHALE_GAS", myWorksheet1.Cells["N3"]);
            }
            else
            {
              myWorkbook.Names.Add("SHALE_GAS", myWorksheet1.Cells["N3:N" + shellgasendrange]);
            }
            //state list
            List<State> statelist = retState.GroupBy(x => x.STATE).Select(y => y.FirstOrDefault()).ToList();
            int statel = statelist.Count;

            for (int i = 0; i < statel; i++)
            {
              myWorksheet1.Cells[6 + i, 15].Value = statelist[i].STATE;
            }

          }
          // Saving the change... 
          excelPackage.Save();
        }
        ExcelMessage = "Excel report created successfully!";
      }
      catch (Exception ex)
      {
        ExcelMessage = "Oops! Something went wrong.";

      }
      return ExcelMessage;
    }
    
    public string existData_WELL(int block_id, int block_type, int block_category, int month, int year, string wellname)
    {
      string result = string.Empty;

      //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_WELDATAEXIST_BLOCK", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_WELDATAEXIST_BLOCK", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_BLOCK_ID", block_id);
        cmd.Parameters.Add("P_BLOCK_TYPE", block_type);
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
        cmd.Parameters.Add("P_WELL_NAME", wellname);
        DataSet ds = new DataSet();
        da.Fill(ds);       
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
           return result;
    }

    public string get_WELLDataExist1(string month, string year, string block)
    {
      string result = string.Empty;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_WELLDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_WELLDATAEXIST", results))
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
      //  cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 1);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string get_WELLDataExist2(string month, string year, string block)
    {
      string result = string.Empty;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_WELLDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_WELLDATAEXIST", results))
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        //cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string Delete_WELLDataExist(string month, string year, string block)
    {
      string result = string.Empty;
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_WELLDATAEXIST", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_WELLDATAEXIST", results))
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        //cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0][0].ToString();
        }
      }
      return result;
    }

    public int delete_WellData(int month, int year)
      {
        //DPRMaster dpr = new DPRMaster();
       int result = 0;
        //string ip = string.Empty;
        //ip = dpr.GetUser_IP();
        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_DELETE_WELLDATA_BLOCK", results))
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_DELETE_WELLDATA_BLOCK", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("P_SUBMISSION_DATE", DateTime.Now.ToString("dd/MM/yyyy"));
          cmd.Parameters.Add("P_UPDATED_BY", USERID);
          cmd.Parameters.Add("P_MONTH", month);
          cmd.Parameters.Add("P_YEAR", year);
          DataSet ds = new DataSet();
          da.Fill(ds);
          result =Convert.ToInt32( ds.Tables[0].Rows[0][0]);          
        }
        return result;
      }

    public int UploadExcelWellWiseExcel(int slno, string wellname, string blocktype, int blockid, int block_type, string state, string s_lat, string s_lon, string ss_lat, string ss_lon, string rigname, int wellcategoryid, double? td_md, double? td_tvd, double? dd_md, double? dd_tvd, int op_area, double? water_depth, int welltypeid, string spudate, string herdate, string rrdate, double? cpplanned, double? cpactual, double? rbplanned, double? rbactual, double? drplanned, double? dractual, double? ptplanned, double? ptactual, double? cyclespeed, double? commspeed, string remarks, int operationid, string submissiondate, int month, int year, int block_category,string Status)
    {
  int result = 0;
  //DPRMaster DP = new DPRMaster();
  //string ip = DP.GetUser_IP();
  // using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INSERT_DRL_WELLDETAILS", results))
  using (OracleConnection con = new OracleConnection(connection))
  {
    con.Open();
    OracleCommand cmd = new OracleCommand("DRL_INSERT_WELL_WISE_EXCEL", con);
    cmd.CommandType = CommandType.StoredProcedure;
    OracleDataAdapter da = new OracleDataAdapter(cmd);
    cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add( "P_SNO", slno);
          cmd.Parameters.Add( "P_WELL_NAME", wellname);
          cmd.Parameters.Add("P_BLOCK_TYPE_NAME", blocktype);
          cmd.Parameters.Add("P_BLOCK_ID", blockid);
          cmd.Parameters.Add("P_STATE", state);
          cmd.Parameters.Add("P_S_LATITUDE", s_lat);
          cmd.Parameters.Add("P_S_LONGITUDE", s_lon);
          cmd.Parameters.Add("P_SS_LATITUDE", ss_lat);
          cmd.Parameters.Add("P_SS_LONGITUDE", ss_lon);
          cmd.Parameters.Add("P_RIG_NAME", rigname);
          cmd.Parameters.Add("P_OPERATIONAL_AREA", op_area);
          cmd.Parameters.Add("P_TARGET_DEPTH_MD", td_md);
          cmd.Parameters.Add("P_TARGET_DEPTH_TVD", td_tvd);
          cmd.Parameters.Add("P_DRILLED_DEPTH_MD", dd_md);
          cmd.Parameters.Add("P_DRILLED_DEPTH_TVD", dd_tvd);
          cmd.Parameters.Add("P_WATER_DEPTH", water_depth);
          cmd.Parameters.Add("P_SPUD_DATE_TIME", spudate);
          cmd.Parameters.Add("P_HER_DATE_TIME", herdate);
          cmd.Parameters.Add("P_RR_DATE_TIME", rrdate);
          cmd.Parameters.Add("P_CP_PLANNED", cpplanned);
          cmd.Parameters.Add("P_CP_ACTUAL", cpactual);
          cmd.Parameters.Add("P_RB_PLANNED", rbplanned);
          cmd.Parameters.Add("P_RB_ACTUAL", rbactual);
          cmd.Parameters.Add("P_DR_PLANNED", drplanned);
          cmd.Parameters.Add("P_DR_ACTUAL", dractual);
          cmd.Parameters.Add("P_PT_PLANED", ptplanned);
          cmd.Parameters.Add("P_PT_ACTUAL", ptactual);
          cmd.Parameters.Add("P_CYCLE_SPEED", cyclespeed);
          cmd.Parameters.Add("P_COMM_SPEED", commspeed);
          cmd.Parameters.Add("P_REMARKS", remarks);
          cmd.Parameters.Add("P_OPERATOR_ID", operationid);
          cmd.Parameters.Add("P_SUBMISSION_DATE", submissiondate);
          cmd.Parameters.Add("P_MONTH", (month));
          cmd.Parameters.Add("P_YEAR", year);
          cmd.Parameters.Add("P_WELL_CATEGORY_ID", wellcategoryid);
          cmd.Parameters.Add("P_WELL_TYPE_ID", welltypeid);
          cmd.Parameters.Add("P_UpdatedBy", USERID);
          cmd.Parameters.Add("P_IP", "");
          cmd.Parameters.Add("P_BLOCK_TYPE", block_type);
          cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
          cmd.Parameters.Add("P_STATUS", Status);
          DataSet ds = new DataSet();
          da.Fill(ds);

          result =Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        return result;
      }

   
    public string get_PerformanceDataExist1(string month, string year, string block)
    {
        string result = string.Empty;
        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_PERFORMANCEDATA", results))
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_GET_CUMULATIVE_DATA", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("P_MONTH", month);
          cmd.Parameters.Add("P_YEAR", year);
        // cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_CallVall", 1);
        DataSet ds = new DataSet();
          da.Fill(ds);
          if (ds.Tables[0].Rows.Count == 0)
          {
            result = "";
          }
          else
          {
            result = ds.Tables[0].Rows[0]["USERID"].ToString();
          }
        }
        return result;
      }

    public string get_PerformanceDataExist2(string month, string year, string block)
    {
      string result = string.Empty;
      //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_PERFORMANCEDATA", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_CUMULATIVE_DATA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        // cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_CallVall", 2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string Delete_PerformanceDataExist(string month, string year, string block)
    {
      string result = string.Empty;
      //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_PERFORMANCEDATA", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_CUMULATIVE_DATA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_MONTH", month);
        cmd.Parameters.Add("P_YEAR", year);
        // cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        cmd.Parameters.Add("P_CallVall", 3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0][0].ToString();
        }
      }
      return result;
    }

    public string CreateTempleteCumulativeDrillingExcel(string month, int year, int op_id, string block_category, string _filePath)
    {
      string ExcelMessage = "";
      string block = "";

      string blknom = "";
      if (block_category == "1")
      {

        block = "NOMINATION";
        blknom = "NOM";
      }
      if (block_category == "2")
      {
        block = "ALL";
        blknom = "ALL";
      }
      try
      {
        string opr_name = OPERATOR_NAME.Replace("&amp;", "&");
        string uid = USERID;
        string date = "01/" + month + "/" + year;
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "CUMULATIVE_DRILLING_PERFORMANCE" + "_" + blknom + ".xlsx";
        string filenmt = "Excel_Performance_Template.xlsx";
        var fpath = _filePath + "/Upload/PerformanceExcel_Template/";
        FileInfo template = new FileInfo(fpath + filenmt);
        FileInfo newFile = new FileInfo(fpath + filenm);

        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
          foreach (ExcelWorksheet aworksheet in excelPackage.Workbook.Worksheets)
          {
            aworksheet.Cells[1, 1].Value = aworksheet.Cells[1, 1].Value;
          }
          // Getting the complete workbook...
          ExcelWorkbook myWorkbook = excelPackage.Workbook;
          // Getting the worksheet by its name... 
          ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];
          int startrow = 3;
          myWorksheet.Cells[startrow, 4].Value = opr_name.ToString();
          myWorksheet.Cells[startrow, 9].Value = month + "_" + year;
          myWorksheet.Cells[startrow, 12].Value = block;
          // Saving the change... 
          excelPackage.Save();
        }
        ExcelMessage = "Excel report created successfully!";
      }
      catch (Exception ex)
      {
        ExcelMessage = "Oops! Something went wrong.";

      }
      return ExcelMessage;
    }

    public int UploadPerformanceExcel(int slno, string operatorid, int month, int year, int rigoperatorstatus, int wellcategoryid, int operationareaid, int nowells, double meterage, string cyclespeed, string commspeed, string rigmonth, int block_category,string Status)
    {
      int result = 0;
      //DPRMaster DP = new DPRMaster();
      //string ip = DP.GetUser_IP();
      //  using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INSERT_DRL_PERFORMANCE", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_INSERT_CUMULATIVE_EXCEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("P_SNO", slno);
            cmd.Parameters.Add("P_OPERATOR_ID", operatorid);
            cmd.Parameters.Add("P_MONTH", month);
            cmd.Parameters.Add("P_YEAR", year);
            cmd.Parameters.Add("P_RIG_OPERATOR_STATUS", rigoperatorstatus);
            cmd.Parameters.Add("P_WELL_CATEGORY_ID", wellcategoryid);
            cmd.Parameters.Add("P_OPERATIONAL_AREA_ID", operationareaid);
            cmd.Parameters.Add("P_NO_WELL", nowells);
            cmd.Parameters.Add("P_METERAGE", meterage);
            cmd.Parameters.Add("P_Cycle_Speed", cyclespeed);
            cmd.Parameters.Add("P_COMM_SPEED", commspeed);
            cmd.Parameters.Add("P_RIG_MONTH", rigmonth);
            cmd.Parameters.Add("P_UpdatedBy",USERID);
            cmd.Parameters.Add("P_IP", "");
            cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
            cmd.Parameters.Add("P_STATUS", Status);
            DataSet ds = new DataSet();
            da.Fill(ds);       
           result =Convert.ToInt32( ds.Tables[0].Rows[0][0]);
          }     
      return result;
    }


    //public string GetUser_IP()
    //{
    //  string VisitorsIPAddr = string.Empty;
      
    //  if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)

        
    //  {
    //    VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
    //  }
    //  else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
    //  {
    //    VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
    //  }
    //  return VisitorsIPAddr;
    //}

    public string CreateTempleteAnnualDrillingExcel(string quarter, string year, int op_id, string block_category, string _filePath)
    {  
      string ExcelMessage = "";    
      string filenmt = "";
      string block = "";
      string blknom = "";
      if (block_category == "1")      {        block = "NOMINATION";        blknom = "NOM";      }
      if (block_category == "2")      {        block = "ALL";        blknom = "ALL";      }
      try
      {
        string opr_name = OPERATOR_NAME.Replace("&amp;", "&");
        string filenm = "ANNUAL_DRILLING_PLAN" + "_" + blknom + ".xlsx";
        if (quarter == "BE")          filenmt = "Excel_Performance_BE_Template.xlsx";
        if (quarter == "RE")          filenmt = "Excel_Performance_Template.xlsx";
        var fpath = _filePath + "/Upload/PlanPerformanceExcel_Template/";
        FileInfo template = new FileInfo(fpath + filenmt);
        FileInfo newFile = new FileInfo(fpath + filenm);
        using (ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
          foreach (ExcelWorksheet aworksheet in excelPackage.Workbook.Worksheets)
          {
            aworksheet.Cells[1, 1].Value = aworksheet.Cells[1, 1].Value;
          }
          // Getting the complete workbook...
          ExcelWorkbook myWorkbook = excelPackage.Workbook;
          // Getting the worksheet by its name... 
          ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];
          //ExcelWorksheet myWorksheet1 = myWorkbook.Worksheets["Block List"];
          int startrow = 3;
          //int block_row = 4;
          myWorksheet.Cells[startrow, 4].Value = opr_name.ToString();
          myWorksheet.Cells[startrow, 9].Value = quarter + "_(" + year + ")".ToString();
          myWorksheet.Cells[startrow, 12].Value = block;
          // Saving the change... 
          excelPackage.Save();
        }
        ExcelMessage = "Excel report created successfully!";
      }
      catch (Exception ex)
      {
        ExcelMessage = "Oops! Something went wrong";
      }
      return ExcelMessage;
    }


    public int UploadExcelAnnualDrilling(int slno, string operatorid, int quarter, string year, int rigoperatorstatus, int wellcategoryid, int operationareaid, int? nowells, int? meterage, string cyclespeed, string commspeed, string rigmonth, int block_category,string Status)
    {
      //DPRMaster dp = new DPRMaster();
      //string ip = dp.GetUser_IP();
      int ret = '0';
      using (OracleConnection con = new OracleConnection(connection))
      {
        //procedure USP_INSERT_DRL_PLANPERFORMANCE
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_INSERT_ANNUAL_PLAN_EXCEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_SNO", slno);
        cmd.Parameters.Add("P_OPERATOR_ID", operatorid);
        cmd.Parameters.Add("P_QUARTER", quarter);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_RIG_OPERATOR_STATUS", rigoperatorstatus);
        cmd.Parameters.Add("P_WELL_CATEGORY_ID", wellcategoryid);
        cmd.Parameters.Add("P_OPERATIONAL_AREA_ID", operationareaid);
        cmd.Parameters.Add("P_NO_WELL", nowells);
        cmd.Parameters.Add("P_METERAGE", meterage);
        cmd.Parameters.Add("P_Cycle_Speed", cyclespeed);
        cmd.Parameters.Add("P_COMM_SPEED", commspeed);
        cmd.Parameters.Add("P_RIG_MONTH", rigmonth);
        cmd.Parameters.Add("P_UpdatedBy", USERID);
        cmd.Parameters.Add("P_IP", "");
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block_category);
        cmd.Parameters.Add("P_STATUS", Status);
        DataTable ds = new DataTable();
        da.Fill(ds);
        ret = Convert.ToInt32(ds.Rows[0][0]);
      }
      return ret;
    }

    public string get_QuaterlyPerformanceDataExist1(string quarter, string year, string block)
    {
      string result = string.Empty;
      //  using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_PLAN_PERFORMANCE", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_PLAN_PERFORMANCE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_QUARTER", quarter);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
       // cmd.Parameters.Add("P_UPDATED_BY",USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 1);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public string get_QuaterlyPerformanceDataExist2(string quarter, string year, string block)
    {
      string result = string.Empty;
      //  using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_GET_PLAN_PERFORMANCE", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_GET_PLAN_PERFORMANCE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_QUARTER", quarter);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_BLOCK_CATEGORY", block);
        //cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        cmd.Parameters.Add("P_CallVall", 2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
          result = "";
        }
        else
        {
          result = ds.Tables[0].Rows[0]["USERID"].ToString();
        }
      }
      return result;
    }

    public int delete_QUARTERLYPERFORMANCEData(string quarter, string year, string blk)
    {
      int result = 0;
      string ip = string.Empty;
      //DPRMaster dp = new DPRMaster();
      //ip = dp.GetUser_IP();
      //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DRL_DELETE_PLANPERFORMDATA", results))
      using (OracleConnection con = new OracleConnection(connection))
      {
        con.Open();
        OracleCommand cmd = new OracleCommand("DRL_DELETE_PLANPERFORMDATA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_QUARTER", quarter);
        cmd.Parameters.Add("P_YEAR", year);
        cmd.Parameters.Add("P_IP", ip);
        cmd.Parameters.Add("P_BLK_TYPE", blk);
        cmd.Parameters.Add("P_UPDATED_BY", USERID);
        cmd.Parameters.Add("P_OPERATOR_ID", OPERATOR_ID);
        DataTable ds = new DataTable();
        da.Fill(ds);
        result = result = Convert.ToInt32(ds.Rows[0][0]);
      }

      return result;
    }

    public int UploadFileData(string file_name, string filedate, int filetype, int application_id, string filepath, int block_cateogy, string bere, string filesubmitby)
    {
      int result = 0;
      string blocktypeiddesc = "";
      if (block_cateogy == 1)
      {
        blocktypeiddesc = "N";
      }
      else if (block_cateogy == 2)
      {
        blocktypeiddesc = "A";
      }
      else
      {
        blocktypeiddesc = "0";
      }
      try
      {
        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INSERT_FILE_DETAILS", results))
        using (OracleConnection con = new OracleConnection(connection))
        {
          con.Open();
          OracleCommand cmd = new OracleCommand("DRL_INSERT_FILE_DETAILS", con);
          cmd.CommandType = CommandType.StoredProcedure;
          OracleDataAdapter da = new OracleDataAdapter(cmd);
          cmd.Parameters.Add("DATA_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Parameters.Add("P_USERID", USERID);
          cmd.Parameters.Add("P_FILE_NAME", file_name);
          cmd.Parameters.Add("P_FILE_DATE", filedate);
          cmd.Parameters.Add("P_FILE_TYPE", filetype);
          cmd.Parameters.Add("P_APPLICATION_ID", application_id);
          cmd.Parameters.Add("P_FILE_PATH", filepath);
          cmd.Parameters.Add("P_BLOCK_TYPE", blocktypeiddesc);
          cmd.Parameters.Add("P_BE_RE", bere);
          cmd.Parameters.Add("P_FILE_SUBMIT_BY", filesubmitby);
          DataTable ds = new DataTable();
          da.Fill(ds);
          result =Convert.ToInt32(ds.Rows[0][0]);
        }
      }
      catch (Exception ex)
      {

      }
      return result;
    }



  }
  public static class Extensions
  {
    public static List<T> ToList<T>(this DataTable table) where T : new()
    {
      IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
      List<T> result = new List<T>();

      foreach (var row in table.Rows)
      {
        var item = CreateItemFromRow<T>((DataRow)row, properties);
        result.Add(item);
      }

      return result;
    }

    private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
    {
      T item = new T();
      foreach (var property in properties)
      {
        if (row.Table.Columns.Contains(property.Name))
        {
          if (row[property.Name] != System.DBNull.Value)
          {
            if (property.PropertyType == typeof(System.String))
              property.SetValue(item, Convert.ToString(row[property.Name]), null);
            else if (property.PropertyType == typeof(System.Int32))
              property.SetValue(item, Convert.ToInt32(row[property.Name]), null);
            else if (property.PropertyType == typeof(System.DayOfWeek))
            {
              DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
              property.SetValue(item, day, null);
            }
            else
              property.SetValue(item, row[property.Name], null);
          }
        }
      }
      return item;
    }





  }
}
