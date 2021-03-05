using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Syncfusion.XlsIO;
using System.IO;
using System.Web;
using System.Configuration;
using OfficeOpenXml;
using System.Data.OleDb;

using System.Drawing;

using pdms123.Models;

using Oracle.ManagedDataAccess.Client;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace pdms123.Controllers
{


  [Route("api/[controller]")]
  [ApiController]

  public class DrillingController : ControllerBase
  {
    public static string blk_cat;
    public static string mt;
    public static string qt;
    public static int yr;
    public static string yer;
    public static string dpr_dt;
    public static int OPERATORID;
    public int OPERATOR_ID = 44;
    public string USERID = "123764";
    public string OPERATOR_NAMEexcel = "Oil & Natural Gas Corp. Ltd.";

    public readonly IHostingEnvironment _hostingEnvironment;
  //  private readonly IActionContextAccessor _accessor;
    public DrillingController(IHostingEnvironment hostingEnvironment)
    {
      _hostingEnvironment = hostingEnvironment;
     // _accessor = accessor;
    }


    [HttpPost("[action]")]
    public IActionResult GetAllddlList()
    {
      return Ok(new Datamodel().GetRigCountAllList());
    }

    [HttpPost("[action]")]
    public string SubmitRigCountDetails(RigCountDetails rig)
    {
      return new Datamodel().SubmitRigCountDetails(rig);
    }

    [HttpPost("[action]")]
    public IActionResult SearchRigCount(RigCountDetails rig)
    {
      return Ok(new Datamodel().SearchRigCount(rig));
    }

    [HttpGet("GetRigCountDetailsId/{Id}")]
    public IActionResult GetRigCountDetailsId(int ID)
    {
      return Ok(new Datamodel().GetRigCountDetailsId(ID));
    }

    [HttpPost("[action]")]
    public string DraftSubmitRigCountDetails(RigCountDetails rig)
    {
      return new Datamodel().DraftSubmitRigCountDetails(rig);
    }

    [HttpPost("[action]")]
    public string DraftUpdateRigCountDetails(RigCountDetails rig)
    {
      return new Datamodel().DraftUpdateRigCountDetails(rig);
    }

    [HttpPost("[action]")]
    public string DraftRigCountDetails(RigCountDetails rig)
    {
      return new Datamodel().DraftRigCountDetails(rig);
    }


    [HttpPost("[action]")]
    public string SubmitRigWisePerformanceDetails(RigWisePerformance rigper)
    {
      return new Datamodel().SubmitRigWisePerformanceDetails(rigper);
    }


    [HttpPost("[action]")]
    public string DraftRigWisePerformanceDetails(RigWisePerformance rigper)
    {
      return new Datamodel().DraftRigWisePerformanceDetails(rigper);
    }
    [HttpPost("[action]")]
    public IActionResult SearchRigWise(RigWisePerformance rwp)
    {
      return Ok(new Datamodel().SearchRigWise(rwp));
    }


    [HttpGet("GetRigWiseDetailsId/{Id}")]
    public IActionResult GetRigWiseDetailsId(int ID)
    {
      return Ok(new Datamodel().GetRigWiseDetailsId(ID));
    }

    [HttpPost("[action]")]
    public string DraftUpdateRigWiseDetails(RigWisePerformance rwp)
    {
      return new Datamodel().DraftUpdateRigWiseDetails(rwp);
    }

    [HttpPost("[action]")]
    public string DraftSubmitRigWiseDetails(RigWisePerformance rwp)
    {
      return new Datamodel().DraftSubmitRigWiseDetails(rwp);
    }

    [HttpGet("GetFieldName/{str}")]
    public IActionResult GetFieldName(int str)
    {
      return Ok(new Datamodel().GetFieldName(str));
    }

    [HttpPost("[action]")]
    public string SubmitWellWiseDetails(WellWisePerformance wwp)
    {
      return new Datamodel().SubmitWellWiseDetails(wwp);
    }
    [HttpPost("[action]")]
    public string DraftWellWiseDetails(WellWisePerformance wwp)
    {
      return new Datamodel().DraftWellWiseDetails(wwp);
    }

    [HttpPost("[action]")]
    public IActionResult SearchWellWise(WellWisePerformance wp)
    {
      return Ok(new Datamodel().SearchWellWise(wp));
    }

    [HttpGet("GetWellWiseDetailsId/{Id}")]
    public IActionResult GetWellWiseDetailsId(int ID)
    {
      return Ok(new Datamodel().GetWellWiseDetailsId(ID));
    }

    [HttpPost("[action]")]
    public string DraftUpdateWellWiseDetails(WellWisePerformance wwp)
    {
      return new Datamodel().DraftUpdateWellWiseDetails(wwp);
    }

    [HttpPost("[action]")]
    public string DraftSubmitWellWiseDetails(WellWisePerformance wwp)
    {
      return new Datamodel().DraftSubmitWellWiseDetails(wwp);
    }


    [HttpPost("[action]")]
    public IActionResult GetDPRList()
    {
      return Ok(new Datamodel().GetDPRList());
    }

    [HttpPost("[action]")]
    public string SubmitDPRDetails(DPR dpr)
    {
      return new Datamodel().SubmitDPRDetails(dpr);
    }

    [HttpPost("[action]")]
    public IActionResult SearchDPR(DPR dpr)
    {
      return Ok(new Datamodel().SearchDPR(dpr));
    }

    [HttpGet("GetDPRDetailsId/{Id}")]
    public IActionResult GetDPRDetailsId(int ID)
    {
      return Ok(new Datamodel().GetDPRDetailsId(ID));
    }

    [HttpPost("[action]")]
    public string DraftSubmitDPRDetails(DPR dpr)
    {
      return new Datamodel().DraftSubmitDPRDetails(dpr);
    }

    [HttpPost("[action]")]
    public string DraftUpdateDPRDetails(DPR dpr)
    {
      return new Datamodel().DraftUpdateDPRDetails(dpr);
    }

    [HttpPost("[action]")]
    public string DraftDPRDetails(DPR dpr)
    {
      return new Datamodel().DraftDPRDetails(dpr);
    }


    [HttpPost("[action]")]
    public string SubmitCDPDetails(CumulativeDrillingPerformance cdp)
    {
      return new Datamodel().SubmitCDPDetails(cdp);
    }

    [HttpPost("[action]")]
    public string DraftCDPDetails(CumulativeDrillingPerformance cdp)
    {
      return new Datamodel().DraftCDPDetails(cdp);
    }

    [HttpPost("[action]")]
    public string DraftSubmitCDPDetails(CumulativeDrillingPerformance cdp)
    {
      return new Datamodel().DraftSubmitCDPDetails(cdp);
    }

    [HttpPost("[action]")]
    public string DraftUpdateCDPDetails(CumulativeDrillingPerformance cdp)
    {
      return new Datamodel().DraftUpdateCDPDetails(cdp);
    }
    [HttpPost("[action]")]
    public IActionResult SearchCDP(CumulativeDrillingPerformance cdp)
    {
      return Ok(new Datamodel().SearchCDP(cdp));
    }

    [HttpGet("GetCDPDetailsId/{Id}")]
    public IActionResult GetCDPDetailsId(int ID)
    {
      return Ok(new Datamodel().GetCDPDetailsId(ID));
    }


    [HttpPost("[action]")]
    public string SubmitADPDetails(AnnualDrillingPlan adp)
    {
      return new Datamodel().SubmitADPDetails(adp);
    }



    [HttpPost("[action]")]
    public string DraftADPDetails(AnnualDrillingPlan adp)
    {
      return new Datamodel().DraftADPDetails(adp);
    }


    [HttpPost("[action]")]
    public string DraftSubmitADPDetails(AnnualDrillingPlan adp)
    {
      return new Datamodel().DraftSubmitADPDetails(adp);
    }

    [HttpPost("[action]")]
    public string DraftUpdateADPDetails(AnnualDrillingPlan adp)
    {
      return new Datamodel().DraftUpdateADPDetails(adp);
    }
    [HttpPost("[action]")]
    public IActionResult SearchADP(AnnualDrillingPlan adp)
    {
      return Ok(new Datamodel().SearchADP(adp));
    }

    [HttpGet("GetADPDetailsId/{Id}")]
    public IActionResult GetADPDetailsId(int ID)
    {
      return Ok(new Datamodel().GetADPDetailsId(ID));
    }


    [HttpPost("[action]")]
    public string DownloadTempleteRigCountExcel(RigCountDetails rig)
    {
      mt = rig.Months.ToString();
      yr = rig.Years;
      OPERATORID = OPERATOR_ID;
      blk_cat = rig.BlockTypes.ToString();
      string _filePath = _hostingEnvironment.WebRootPath;
      return new Datamodel().DownloadTempleteRigCountExcel(rig, _filePath);
    }

    [HttpPost("[action]")]
    public IActionResult DownloadExcel_NoOfRigsUP()
    {
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcel_Template\\";
      string block = "";
      if (blk_cat == "1")
        block = "NOM";
      if (blk_cat == "2")
        block = "ALL";
      string date = "01/" + mt + "/" + yr;
      try
      {
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "RIG_COUNT" + "_" + block + ".xlsx";
        string fpath = _filePath + filenm;
        byte[] fileByteArray = System.IO.File.ReadAllBytes(_filePath + filenm);
        // System.IO.File.Delete(fpath);
        return File(fileByteArray, "application/vnd.ms-excel", filenm);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> Download()
    {
      // string fname = "RIG_COUNT_NOM.xlsx";
      // var uploads = _hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcel_Template\\";
      //// var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
      // var filePath = Path.Combine(uploads, fname);
      // if (!System.IO.File.Exists(filePath))
      //   return NotFound();

      // var memory = new MemoryStream();
      // using (var stream = new FileStream(filePath, FileMode.Open))
      // {
      //   await stream.CopyToAsync(memory);
      // }
      // memory.Position = 0;

      // return File(memory, GetContentType(filePath), fname);
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcel_Template\\";
      string block = "";
      if (blk_cat == "1")
        block = "NOM";
      if (blk_cat == "2")
        block = "ALL";
      string date = "01/" + mt + "/" + yr;
      try
      {
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "RIG_COUNT" + "_" + block + ".xlsx";
        string fpath = _filePath + filenm;
        if (!System.IO.File.Exists(fpath))
          return NotFound();

        var memory = new MemoryStream();
        using (var stream = new FileStream(fpath, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, GetContentType(fpath), filenm);

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private string GetContentType(string path)
    {
      var provider = new FileExtensionContentTypeProvider();
      string contentType;
      if (!provider.TryGetContentType(path, out contentType))
      {
        contentType = "application/octet-stream";
      }
      return contentType;
    }
    public List<string> getRigNoFileExistUserOp(string monthyear, string blkcat)
    {

      List<string> filenames = new List<string>();

      string AppFilePath = "";

      AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" + USERID);
      if (!exists)
        System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1)
        System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)        System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsx").Select(path => Path.GetFileName(path)) .ToList<string>();

      return filenames;
    }
    public int getRigNoFileExistAdmin(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      int total = 0;
      string adminapprootpath = _hostingEnvironment.WebRootPath;
      string AppFilePath = "";

      AppFilePath = adminapprootpath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\NoRigExcelUpload\\" + USERID);
      if (!exists)        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\NoRigExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1)        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)        System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsx").Select(path => Path.GetFileName(path)).ToList<string>();
      total = filenames.Count;

      return total;
    }

    [HttpPost("[action]")]
    public string UploadExcelRig(IFormFile IFormFile)
    {
      var file = Request.Form.Files[0];
      string mthidden = HttpContext.Request.Form["Months"];
      string yrhidden = HttpContext.Request.Form["Years"];
      string blkcathidden = HttpContext.Request.Form["BlockTypes"];
      string Status = HttpContext.Request.Form["Status"];
      mt = "";
      yr = 0;
      blk_cat = "";
      int slno = 0;
      int block_category = 0;
      if (blk_cat == "0" || blk_cat == null || blk_cat == "")
      {
        block_category = Convert.ToInt32(blkcathidden);
        blk_cat = blkcathidden;
      }
      else
      {
        block_category = Convert.ToInt32(blk_cat);
      }
      string operator_id = "";
      int month = 0;
      int year = 0;
      int rig_op_status_id = 0;
      int well_category_id = 0;
      int operation_area_id = 0;
      int no_rig = 0;
      string file_name = "";
      string Message = "";

      string rigdataexist = "";
      string monthyearfolder = "";
      string monthstring = "";
      string yearstring = "";
      string path = "";

      if (file != null && file.Length > 0)
      {
        try
        {
          if (mt == "" || mt == null)
          {
            month = Convert.ToInt16(mthidden);
          }
          else
          {
            if (mt == "January")              month = 1;
            if (mt == "February")              month = 2;
            if (mt == "March")              month = 3;
            if (mt == "April")              month = 4;
            if (mt == "May")              month = 5;
            if (mt == "June")              month = 6;
            if (mt == "July")              month = 7;
            if (mt == "August")              month = 8;
            if (mt == "September")              month = 9;
            if (mt == "October")              month = 10;
            if (mt == "November")              month = 11;
            if (mt == "December")              month = 12;
          }

          if (yr == 0 || yr == null)
          {
            year = Convert.ToInt16(yrhidden);
          }
          else
          {
            year = yr;
          }

          monthstring = Convert.ToString(month);
          yearstring = Convert.ToString(year);

          monthyearfolder = "1" + "-" + month + "-" + year;
          rigdataexist = new Datamodel().get_RIGNODataExist1(monthstring, yearstring, blk_cat);
          List<string> filenames = new List<string>();
          int exist_file = 0;

          if (rigdataexist == "")
          {
         
            string file_name_ext = Path.GetFileName(file.FileName);
            string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);

            filenames = getRigNoFileExistUserOp(monthyearfolder, blk_cat);
            exist_file = filenames.Count;
            int exist_file_admin = getRigNoFileExistAdmin(monthyearfolder, blk_cat);

            string file_name_new = file_name_get.Replace(" ", "");
            file_name = file_name_new.Replace("\'", "");

            string final_file = file_name + ".xlsx";
            string date = "01/" + month + "/" + year;

            string AppFilePath = "";
            string adminrigpath = "";
            string delfilepath = "";
            string filenameadmin = "";
            string adminapprootpath = _hostingEnvironment.WebRootPath;

            AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" +USERID + "\\" + monthyearfolder + "\\" + blk_cat;
            path = Path.Combine(AppFilePath, final_file);

            if (exist_file == 0)
            {
            }
            else
            {
              
              for (int i = 0; i < exist_file; i++)
              {
                
                delfilepath = Path.Combine(AppFilePath, filenames[i]);
                if (System.IO.File.Exists(delfilepath))
                {
                  string rigdatadELETE = new Datamodel().get_RIGNODataDELETE(monthstring, yearstring, blk_cat);
                  try
                  {                    
                    System.IO.File.Delete(delfilepath);                   
                  }
                  catch (Exception ex)
                  {
                    Message = "File is used by another Process.Please try again";
                    goto label;
                  }
                }
              }
            }
            string subpath = Path.Combine("\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat, final_file);
            string monthyearfolderdata = "1" + "-" + mthidden + "-" + yrhidden;
            string folderName = "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyearfolderdata + "\\" + blkcathidden;
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(folderName))
            {
              Directory.CreateDirectory(folderName);
            }
          
            if (file.Length > 0)
            {
              final_file = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
              string fullPath = Path.Combine(AppFilePath, final_file);
              try
              {
                // System.IO.File.Delete(fullPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                  file.CopyTo(stream);
                }
              }
              catch (Exception e)
              {
                Message = "File is used by another Process.Please try again";
                goto label;
              }
            }
            OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"");
            //OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Persist Security Info=False");
            my_con.Open();
            try
            {
              OleDbCommand o_cmd = new OleDbCommand("select * from [Sheet1$]", my_con);
              OleDbDataReader o_dr = o_cmd.ExecuteReader();
              DataTable dt = new DataTable();
              dt.Load(o_dr);

              //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insert is successfull')", true);
              //PageUtility.MessageBox(this, "Success !");
              //Response.BodyWriter("<script>alert('Inserted successfully!')</script>");
             // MessageBox.Show("Data inserted successfully");
              int count = dt.Rows.Count;
              if (count < 2)
                Message = "Please enter no of rigs";
              int result1 = 0;
              int result2 = 0;

              //string opnameexcel = dt.Columns[2].ColumnName;
              string opnameexcel = dt.Rows[1][2].ToString();
              opnameexcel = opnameexcel.Replace("#", ".");
              string excelfiledate = dt.Rows[4][1].ToString();
              string excelcode = dt.Rows[2][0].ToString();

              string blkdesc = "";
              string blknom = "";
              string mtdesc = "";

              if (month == 1)                mtdesc = "January";
              if (month == 2)                mtdesc = "February";
              if (month == 3)                mtdesc = "March";
              if (month == 4)                mtdesc = "April";
              if (month == 5)                mtdesc = "May";
              if (month == 6)                mtdesc = "June";
              if (month == 7)                mtdesc = "July";
              if (month == 8)                mtdesc = "August";
              if (month == 9)                mtdesc = "September";
              if (month == 10)                mtdesc = "October";
              if (month == 11)                mtdesc = "November";
              if (month == 12)                mtdesc = "December";

              string uploadfiledate = mtdesc + "_" + year;

              if (block_category == 1)
              {
                blkdesc = "NOMINATION";
                blknom = "NOM";
              }
              else if (block_category == 2)
              {
                blkdesc = "ALL";
                blknom = "ALL";
              }
              //check validation
              if (excelcode == "NORIGCODE308AM1")
              {
              }
              else
              {
                Message = "This is not a uploaded template.Please download correct template";
                goto label;
              }
              if (opnameexcel == OPERATOR_NAMEexcel)
              {
              }
              else
              {
                Message = "Please specify correct Operator Name";
                goto label;
              }

              if (excelfiledate == uploadfiledate)
              {
              }
              else
              {
                Message = "Month and year in Excel File should be same as Selected Month and year";
                goto label;
              }

              if (dt.Rows[1][6].ToString() == blkdesc || dt.Rows[1][6].ToString() == blknom)
              {
              }
              else
              {
                Message = "Please specify correct Block Type";
                goto label;
              }
              Datamodel rgm = new Datamodel();
              // slno = 0;

              for (int i = 8; i <= count - 5; i++)
              {
                for (int j = 0; j <= 3; j++)
                {
                  if (j == 0)
                  {
                    well_category_id = 1;
                    rig_op_status_id = 1;
                  }
                  if (j == 1)
                  {
                    well_category_id = 2;
                    rig_op_status_id = 1;
                  }

                  if (j == 2)
                  {
                    well_category_id = 1;
                    rig_op_status_id = 2;
                  }

                  if (j == 3)
                  {
                    well_category_id = 2;
                    rig_op_status_id = 2;
                  }

                  slno++;
                  operator_id = OPERATOR_ID.ToString();
                  string remarks = dt.Rows[14][1].ToString(); ;
                  operation_area_id = i - 7;

                  no_rig = Convert.ToInt32(((dt.Rows[i][j + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][j + 1]).ToString());
                //  var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
                  result1 = rgm.UploadNoRigExcel(slno, operator_id, month, year, rig_op_status_id, well_category_id, operation_area_id, no_rig, block_category, remarks,Status);
                }
              }
              rigdataexist = new Datamodel().get_RIGNODataExist2(monthstring, yearstring, blk_cat);
              if (rigdataexist == "")
              {
                Message = "There is problem in File uploading!!";
                goto label;
              }
              else
              {
                result2 = rgm.UploadFileData(final_file, date, 8, 3, subpath, block_category, null, USERID);

                //adminrigpath = _hostingEnvironment.WebRootPath + "\\Upload\\NoRigExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
                //if (exist_file_admin == 0)
                //{
                //  filenameadmin = final_file;
                //}
                //else
                //{
                //  filenameadmin = file_name + "_Updated" + exist_file_admin + ".xlsx";
                //}
                //string pathadmin = Path.Combine(adminrigpath, filenameadmin);
                //file.SaveAs(pathadmin);
                //file.InputStream.Dispose();
                //GC.Collect();

               // string subpathadmin = Path.Combine("NoRigExcelUpload\\" + WebSession.UserId + "\\" + monthyearfolder + "\\" + blk_cat, filenameadmin);
                //result2 = rgm.UploadFileData(filenameadmin, date, 8, 3, subpathadmin, block_category, null, "Admin");
                Message = "File uploaded successfully";
                my_con.Close();
                goto label;
              }
            }
            catch (Exception ex2)
            {
              Message = "ERROR:" + ex2.Message.ToString();
            }

            
          }
          else
          {
            Message = "You have already uploaded a file.";
          }
        }
        catch (Exception ex1)
        {
          Message = "ERROR:" + ex1.Message.ToString();
        }
       }
      else
      {
        Message = "You have not specified a file.";
      }
    label:
    return Message;
    }

    [HttpPost("[action]")]
    public string CreateDPRExcel(DPR dpr)
    {
      string op_name = OPERATOR_NAMEexcel;
      DateTime dprdate1= dpr.DPR_DATE.AddDays(1);
      DateTime dprdate = dprdate1;
      int op_id = OPERATOR_ID;
      string _filePath = _hostingEnvironment.WebRootPath;
      return new Datamodel().CreateDPRExcel(op_name, dprdate, op_id, _filePath);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadDPRFile()
    {
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\DPRExcel_Template\\";
      try
      {
        //string d;
        //d = dpr_dt == null ? null : Convert.ToDateTime(dpr_dt, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        //string adate = d;
        //string r = adate.Replace("/", "-");
        string filenm = "DPR" + ".xlsm";
        string fpath = _filePath + filenm;
        if (!System.IO.File.Exists(fpath))
          return NotFound();
        var memory = new MemoryStream();
        using (var stream = new FileStream(fpath, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, GetContentType(fpath), filenm);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }



    [HttpPost("[action]")]
    public string UploadExceldprExcel(IFormFile IFormFile)
    {
      var file = Request.Form.Files[0];
      string dthidden = HttpContext.Request.Form["DPR_DATE"];
      string Status = HttpContext.Request.Form["Status"];
      dpr_dt = "";
      string dpr_date = dpr_dt;
      if (dpr_dt == "" || dpr_dt == null)
      {
        dpr_date = dthidden;
        dpr_dt = dthidden;
      }
      else
      {
        dpr_date = dpr_dt;
      }
      string date2 = DateTime.Now.ToString("dd/MM/yyyy");
      string startdate = "01/04/2018";
      string file_name = string.Empty;
      int operator_id = OPERATOR_ID;
      string block_name = "";
      int[] block_id = { 0, 0 };
      string area = "";
      string op_area = "";
      int op_area_id = 0;
      string rig_name = "";
      string rig_status = "";
      int rig_status_id = 0;
      string well_name = "";
      string well_category = "";
      int well_category_id = 0;
      string well_type = "";
      int well_type_id = 0;
      string spud_date = "";
      string phase = "";
      int phase_id = 0;
      double? target_depth = null;
      double? present_depth = null;
      double? metarge = null;
      string dpr_remarks = "";
      string query = "";
      string Message = "";
      string block_Message = "";

      string state = "";
      string lat = "";
      string lon = "";
      string file_final_name = "";
      string file_final_name_user = "";
      // String myDate = 'Tue Nov 18 00:00:00 GMT 2014';
      string DateData = dpr_date;

      string strMnth = DateData.Substring(4, 3);
      string strday = DateData.Substring(8, 2);
      string stryear = DateData.Substring(11, 4);
      string strMonth = "";
      if (strMnth == "Jan") strMonth = "01";
      else if (strMnth == "Feb") strMonth = "02";
      else if (strMnth == "Mar") strMonth = "03";
      else if (strMnth == "Apr") strMonth = "04";
      else if (strMnth == "May") strMonth = "05";
      else if (strMnth == "Jun") strMonth = "06";
      else if (strMnth == "Jul") strMonth = "07";
      else if (strMnth == "Aug") strMonth = "08";
      else if (strMnth == "Sep") strMonth = "09";
      else if (strMnth == "Oct") strMonth = "10";
      else if (strMnth == "Nov") strMonth = "11";
      else if (strMnth == "Dec") strMonth = "12";

      string strDate = strday + '/' + strMonth + '/' + stryear;

      DateTime dt1 = DateTime.ParseExact(strDate, "dd/MM/yyyy", null);
      // DateTime dt1 = Convert.ToDateTime(strDate);
      // string dt1 = strDate;
      DateTime dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
      DateTime dtstartdate = DateTime.ParseExact(startdate, "dd/MM/yyyy", null);

      string dprdataexists = "";
      string datefolder = "";
      datefolder = strDate.Replace("/", "-");
      string daily_date = strday + "-" + strMnth + "-" + stryear;
      string path = "";

      Datamodel dm = new Datamodel();

      if (dt1 > dt2 || dt1 < dtstartdate)
      {
        Message = "Please upload data after March 2018 and do not upload for future date!!";
      }
      else
      {
        if (file != null && file.Length > 0)
        {
          try
          {
            dprdataexists = new Datamodel().get_DPRDataExist1(dt1);
            List<string> filenames = new List<string>();
            int exist_file_user = 0;
            if (dprdataexists == "")
            {
              string file_name_ext = Path.GetFileName(file.FileName);
              string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);
              filenames = GetFileExistUser(datefolder);
              exist_file_user = filenames.Count;
               int exist_file_admin = GetFileExist(datefolder);

              string file_name_new = file_name_get.Replace(" ", "");
              file_name = file_name_new.Replace("\'", "");

              string final_file = file_name + ".xlsm";

              string AppFilePath = "";
              string adminrigpath = "";
              string filenameadmin = "";
              string delfilepath = "";
              string adminapprootpath = _hostingEnvironment.WebRootPath;

              AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + datefolder;
              path = Path.Combine(AppFilePath, final_file);
              

              if (exist_file_user == 0)
              {
              }
              else
              {
                for (int i = 0; i < exist_file_user; i++)
                {
                  delfilepath = Path.Combine(AppFilePath, filenames[i]);
                  if (System.IO.File.Exists(delfilepath))
                  {
                    string dprdelete = new Datamodel().delete_DPRData(dt1);
                    try
                    {
                      System.IO.File.Delete(delfilepath);
                    }
                    catch (Exception ex)
                    {
                      Message = "File is used by another Process.Please try again";
                    }
                  }
                }
              }

              if (!Directory.Exists(path))
              {
                Directory.CreateDirectory(AppFilePath);
              }
              string UploadfileName = "";
              if (file.Length > 0)
              {
                UploadfileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(AppFilePath, UploadfileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                  file.CopyTo(stream);
                }
              }
              string subpath = Path.Combine("DPRExcelUpload\\" + USERID + "\\" + datefolder, final_file);
             
              Datamodel dprdm = new Datamodel();
              OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"");
              my_con.Open();
              try
              {
                query = "select*from [Sheet1$]";
                OleDbCommand o_cmd = new OleDbCommand(query, my_con);
                OleDbDataReader o_dr = o_cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(o_dr);
                int count = dt.Rows.Count;
                int result = 0;

                //for fetching sheet2 data
                string query1 = "select*from [Sheet2$]";
                OleDbCommand o_cmd1 = new OleDbCommand(query1, my_con);
                OleDbDataReader o_dr1 = o_cmd1.ExecuteReader();
                DataTable dttable = new DataTable();
                dttable.Load(o_dr1);

                string opnameexcel = dt.Rows[0][1].ToString();
                opnameexcel = opnameexcel.Replace("#", ".");
                string excelfiledate = dt.Rows[1][1].ToString();

                // string uploadfiledate = dpr_date;
                string uploadfiledate = strDate;
                string excelcode = dttable.Columns[0].ColumnName.ToString();
                excelfiledate = excelfiledate.Replace("-", "/");
                //check validation
                if (excelcode == "DRLILLINGDPRCODE313AM1")
                {
                }
                else
                {
                  Message = "This is not a uploaded template.Please download correct template";
                  my_con.Close();
                  goto label;
                }
                if (opnameexcel == OPERATOR_NAMEexcel)
                {
                }
                else
                {
                  Message = "Please specify correct Operator Name";
                  my_con.Close();
                  goto label;
                }

                if (excelfiledate == uploadfiledate)
                {
                }
                else
                {
                  Message = "DPR date in Excel File should be same as Selected date";
                  my_con.Close();
                  goto label;
                }


                for (int i = 5; i < count; i++)
                {
                  var sno = dt.Rows[i][0].ToString();
                  block_name = dt.Rows[i][1].ToString().Trim();
                  area = dt.Rows[i][2].ToString();
                  state = dt.Rows[i][3].ToString();
                  rig_name = dt.Rows[i][4].ToString();
                  rig_status = dt.Rows[i][5].ToString();
                  well_name = dt.Rows[i][6].ToString();
                  lat = dt.Rows[i][7].ToString();
                  lon = dt.Rows[i][8].ToString();
                  well_category = dt.Rows[i][9].ToString();
                  well_type = dt.Rows[i][10].ToString();
                  spud_date = dt.Rows[i][11].ToString();
                  phase = dt.Rows[i][12].ToString();

                  //chk spud date format
                  int len = spud_date.Length;
                  if (len > 11)
                    spud_date = Convert.ToDateTime(spud_date).ToString("dd-MMM-yyyy");

                  string[] strArr = null;
                  char[] splitchar = { '-' };
                  strArr = spud_date.Split(splitchar);
                  if (strArr.Length == 0 && spud_date != "")
                  {
                    Message = "Please enter Spud Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                    int delete_datad = 0;
                    // delete_datad = dpr_data.delete_DPRData(daily_date);
                    my_con.Close();
                    goto label;
                  }
                  else if (strArr.Length <= 1 && spud_date != "")
                  {
                    Message = "Please enter Spud Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                    int delete_datad = 0;
                    my_con.Close();
                    // delete_datad = dpr_data.delete_DPRData(daily_date);
                    goto label;
                  }
                  else if (strArr.Length > 1 && spud_date != "")
                  {
                    if (strArr[1] == "Jan" || strArr[1] == "Feb" || strArr[1] == "Mar" || strArr[1] == "Apr" || strArr[1] == "May" || strArr[1] == "Jun" || strArr[1] == "Jul" || strArr[1] == "Aug" || strArr[1] == "Sep" || strArr[1] == "Oct" || strArr[1] == "Nov" || strArr[1] == "Dec")
                    {

                    }
                    else
                    {
                      Message = "Please enter Spud Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datadd = 0;
                      my_con.Close();
                      //  delete_datadd = dpr_data.delete_DPRData(daily_date);
                      goto label;
                    }
                  }
                  if (dt.Rows[i][13].ToString().Length == 0)
                    target_depth = (double?)null;
                  else
                    target_depth = Convert.ToDouble(dt.Rows[i][13].ToString());
                  if (dt.Rows[i][14].ToString().Length == 0)
                    present_depth = (double?)null;
                  else
                    present_depth = Convert.ToDouble(dt.Rows[i][14].ToString());
                  if (dt.Rows[i][15].ToString().Length == 0)
                    metarge = (double?)null;
                  else
                    metarge = Convert.ToDouble(dt.Rows[i][15].ToString());

                  dpr_remarks = dt.Rows[i][16].ToString();

                  if (block_name == "")
                  {
                    //no insertion
                  }
                  else
                  {
                    // DPRMaster dpr_data = new DPRMaster();
                    block_id = dm.GetBlockID(block_name);
                    if (block_id[0] == 0)
                    {
                      Message = "Please specify correct Block Name";
                      my_con.Close();
                      int delete_data = 0;
                      // delete_data = dpr_data.delete_DPRData(daily_date);
                      goto label;
                    }
                    else
                    {

                      string dpr_data_exist = string.Empty;
                      // dpr_data_exist = dpr_data.existData_DPRBlockwise(block_id[0], block_id[1], dpr_date, well_name);

                      if (dpr_data_exist == "")
                      {

                        switch (rig_status)
                        {
                          case "Owned":
                            rig_status_id = 1;
                            break;
                          case "Ch-Hired":
                            rig_status_id = 2;
                            break;
                          case "MMC":
                            rig_status_id = 3;
                            break;
                        }

                        switch (well_category)
                        {
                          case "Exploratory":
                            well_category_id = 1;
                            break;
                          case "Development":
                            well_category_id = 2;
                            break;
                          case "Appraisal":
                            well_category_id = 3;
                            break;
                        }

                        switch (well_type)
                        {
                          case "Vertical":
                            well_type_id = 1;
                            break;
                          case "Directional":
                            well_type_id = 2;
                            break;
                          case "Horizontal":
                            well_type_id = 3;
                            break;
                          case "Multilateral":
                            well_type_id = 4;
                            break;
                          case "Sidetrack":
                            well_type_id = 5;
                            break;
                        }

                        switch (phase)
                        {
                          case "RB (Rig Building)":
                            phase_id = 1;
                            break;
                          case "DR (Drilling)":
                            phase_id = 2;
                            break;
                          case "PT (Production Testing)":
                            phase_id = 3;
                            break;
                          case "OC (Out Cycle)":
                            phase_id = 4;
                            break;
                        }

                        switch (area)
                        {
                          case "Onshore":
                            op_area_id = 1;
                            break;
                          case "Offshore(SW)":
                            op_area_id = 2;
                            break;
                          case "Offshore(DW)":
                            op_area_id = 3;
                            break;
                        }

                        DPR dpardata = new DPR();
                        dpardata.DPR_DATE = dt1; dpardata.BLOCK_ID = block_id[0]; dpardata.OPERATION_AREA_ID = op_area_id; dpardata.STATE = 1;
                        dpardata.RIG_NAME = rig_name; dpardata.RIG_OPERATOR_STATUS_ID = rig_status_id; dpardata.WELL_NAME = well_name;
                        dpardata.LATITUDE = lat; dpardata.LONGITUDE = lon; dpardata.WELL_CATEGORY_ID = well_category_id; dpardata.WELL_TYPE_ID = well_type_id;
                        dpardata.SPUD_DATE = Convert.ToDateTime(spud_date); dpardata.PHASE_ID = phase_id; dpardata.TARGET_DEPTH = target_depth.ToString();
                        dpardata.PRESENT_DEPTH = present_depth.ToString(); dpardata.METARGE = metarge.ToString(); dpardata.DPR_BRIEF = dpr_remarks;

                        result = dm.InsertDrillingDPR(dpardata ,Status);



                        //result = dm.InsertDrillingDPR(daily_date, operator_id, block_id[0], block_id[1], op_area_id, state, rig_name, rig_status_id, well_name, lat, lon, well_category_id, well_type_id, spud_date, phase_id, target_depth, present_depth, metarge, dpr_remarks);
                      }

                      else
                      {
                        Message = "Well name:- " + well_name + " for " + block_name + "  block already exists!!";
                        
                        int delete_data = 0;
                        // delete_data = dpr_data.delete_DPRData(daily_date);
                        goto label;
                      }                      

                    }
                  }
                }
                my_con.Close();
                dprdataexists = new Datamodel().get_DPRDataExist2(dt1);
                if (dprdataexists == "")
                {
                  Message = "There is problem in File uploading!!";
                  goto label;
                }
                else
                {
                  int result1 = new Datamodel().UploadFileData(final_file, daily_date, 8, 3, subpath, 0, null, USERID);
                  Message = "File uploaded successfully";
                  goto label;
                }               
              }
              catch (Exception e)
              {
                Message = "ERROR:" + e.Message.ToString();
              }
            }
            else
            {
              Message = "You have already uploaded a file.";
            }
          }

          catch (Exception ex1)
          {

            Message = "ERROR:" + ex1.Message.ToString();
          }

        }
        else
        {
          Message = "You have not specified a file.";
        }
      }
      
    label:
      return Message;
    }


    public List<string> GetFileExistUser(string dpr_dt)
    {

      List<string> filenames = new List<string>();

      string AppFilePath = "";

      AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + dpr_dt;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\DPRExcelUpload\\" + USERID);
      if (!exists)
        System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\DPRExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + dpr_dt);
      if (!exists1)
        System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + dpr_dt);


      filenames = Directory.GetFiles(AppFilePath, "*.xlsm")
                              .Select(path => Path.GetFileName(path))
                              .ToList<string>();
      return filenames;
    }

    public int GetFileExist(string file_date)
    {
      List<string> filenames = new List<string>();
      int total = 0;
      string adminapprootpath = _hostingEnvironment.WebRootPath;
      string AppFilePath = "";

      AppFilePath = adminapprootpath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + file_date;

      bool exists = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\DPRExcelUpload\\" + USERID);
      if (!exists)
        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\DPRExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + file_date);
      if (!exists1)
        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\DPRExcelUpload\\" + USERID + "\\" + file_date);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsm")
                              .Select(path => Path.GetFileName(path))
                              .ToList<string>();
      total = filenames.Count;

      return total;
    }




    [HttpPost("[action]")]
    public string CreateTempleteRigWiseExcel(RigWisePerformance rwp)
    {
      mt = rwp.MONTH.ToString();
      string monthname = "";
      if (mt == "1") { monthname = "January"; }
      if (mt == "2") { monthname = "February"; }
      if (mt == "3") { monthname = "March"; }
      if (mt == "4") { monthname = "April"; }
      if (mt == "5") { monthname = "May"; }
      if (mt == "6") { monthname = "June"; }
      if (mt == "7") { monthname = "July"; }
      if (mt == "8") { monthname = "August"; }
      if (mt == "9") { monthname = "September"; }
      if (mt == "10") { monthname = "October"; }
      if (mt == "11") { monthname = "November"; }
      if (mt == "12") { monthname = "December"; }

      yr = rwp.YEAR;
      OPERATORID = OPERATOR_ID;
      blk_cat = rwp.BLOCK_CATEGORY.ToString();
      string _filePath = _hostingEnvironment.WebRootPath;
      return new Datamodel().CreateTempleteRigWiseExcel(monthname, yr, OPERATORID, blk_cat, _filePath);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadFileRigWise()
    {
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\RIGExcel_Template\\";
      string block = "";
      if (blk_cat == "1")
        block = "NOM";
      if (blk_cat == "2")
        block = "ALL";
      string date = "01/" + mt + "/" + yr;
      try
      {
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "RIG_WISE_PERFORMANCE" + "_" + block + ".xlsm";
        string fpath = _filePath + filenm;
        if (!System.IO.File.Exists(fpath))
          return NotFound();
        var memory = new MemoryStream();
        using (var stream = new FileStream(fpath, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, GetContentType(fpath), filenm);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public int getRigFileExist(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      int total = 0;
      string adminapprootpath = _hostingEnvironment.WebRootPath; 
      string AppFilePath = "";
      AppFilePath = adminapprootpath + "\\Upload\\RigExcelUpload\\" +USERID+ "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\RigExcelUpload\\" + USERID);
      if (!exists) System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\RigExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1) System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2) System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsm").Select(path => Path.GetFileName(path)).ToList<string>();
      total = filenames.Count;


      return total;
    }

    public List<string> GetRIgFileExixtUser(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      string AppFilePath = "";
      AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" +USERID + "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" + USERID);
      if (!exists)  System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1)  System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)  System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsm").Select(path => Path.GetFileName(path)).ToList<string>();

      return filenames;
    }

    [HttpPost("[action]")]
    public string UploadExcelRigWiseExcel(IFormFile IFormFile)
    {
      var file = Request.Form.Files[0];
      string mthidden = HttpContext.Request.Form["Months"];
      string yrhidden = HttpContext.Request.Form["Years"];
      string blkcathidden = HttpContext.Request.Form["BlockTypes"];
      string Status = HttpContext.Request.Form["Status"];
      string Message = "";
      
      mt = "";
      yr = 0;
      blk_cat = "";
      Datamodel rgm = new Datamodel();
      int slno = 0;
      string start = "";
      int operatiorid = OPERATOR_ID;

      string opearation_area = "";
      int operation_area_id = 0;
      int[] blockid = { 0, 0 };
      string block_name = "";
      string rigname = "";
      string operating_status = "";
      int operating_status_id = 0;
      string rig_type = "";
      int rig_type_id = 0;
      int? exp_well = 0;
      double? exp_met = 0;
      int? dev_well = 0;
      double? dev_met = 0;
      double? rig_mode_time_rb = 0;
      double? rig_mode_time_dr = 0;
      double? rig_mode_time_pt = 0;
      double? outcycle_caprep = 0;
      double? outcycle_oth = 0;
      double? npd_complication_day = 0;
      double? npd_complication_per = 0;
      double? npd_repair_day = 0;
      double? npd_repair_per = 0;
      double? cycle_day = 0;
      double? comm_day = 0;
      double? cycle_exp = null;
      double? tempInt = null;
      double? cycle_dev = null;
      double? cyclespeed = 0;
      double? comm_exp = null;
      double? comm_dev = null;
      double? commspeed = 0;
      string remarks = "";
      int month = 0;
      int year = 0;
      string file_name = "";
    
      string file_final_name = "";
      string file_final_name_user = "";
      string submissiondate = "";
      int block_category = 0;

      if (blk_cat == "0" || blk_cat == null || blk_cat == "")
      {
        block_category = Convert.ToInt32(blkcathidden);
        blk_cat = blkcathidden;
      }
      else
      {
        block_category = Convert.ToInt32(blk_cat);
      }
      string rigexists = "";
      string monthyearfolder = "";
      string monthstring = "";
      string yearstring = "";
      if (file != null && file.Length > 0)
      {
        if (mt == "" || mt == null)
        {
          month = Convert.ToInt16(mthidden);
        }
        else
        {
          if (mt == "January")month = 1;
          if (mt == "February")month = 2;
          if (mt == "March") month = 3;
          if (mt == "April") month = 4;
          if (mt == "May")month = 5;
          if (mt == "June")  month = 6;
          if (mt == "July")month = 7;
          if (mt == "August")month = 8;
          if (mt == "September") month = 9;
          if (mt == "October") month = 10;
          if (mt == "November") month = 11;
          if (mt == "December") month = 12;
        }

        if (yr == 0 || yr == null)
        {
          year = Convert.ToInt16(yrhidden);
        }
        else
        {
          year = yr;
        }

        monthstring = Convert.ToString(month);
        yearstring = Convert.ToString(year);
        string path = "";
        monthyearfolder = "1" + "-" + month + "-" + year;
        try
        {
          rigexists = new Datamodel().get_RIGDataExist1(monthstring, yearstring, blk_cat);
          List<string> filenames = new List<string>();
          int exist_file_user = 0;
          if (rigexists == "")
          {
            string file_name_ext = Path.GetFileName(file.FileName);
            string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);
            filenames = GetRIgFileExixtUser(monthyearfolder, blk_cat);
            exist_file_user = filenames.Count;
            int exist_file_admin = getRigFileExist(monthyearfolder, blk_cat);

            string file_name_new = file_name_get.Replace(" ", "");
            file_name = file_name_new.Replace("\'", "");

            string final_file = file_name + ".xlsm";
            string date = "01/" + month + "/" + year;

            string AppFilePath = "";
            string adminrigpath = "";
            string filenameadmin = "";
            string delfilepath = "";
            string adminapprootpath = _hostingEnvironment.WebRootPath;

            AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" +USERID + "\\" + monthyearfolder + "\\" + blk_cat;
            path = Path.Combine(AppFilePath, final_file);
            if (exist_file_user == 0)
            {}
            else
            {
              for (int i = 0; i < exist_file_user; i++)
              {
                delfilepath = Path.Combine(AppFilePath, filenames[i]);
                if (System.IO.File.Exists(delfilepath))
                {
                  string rigwisedelete = new Datamodel().get_RIG_Wise_delete(monthstring, yearstring, blk_cat);
                  try
                  {
                    System.IO.File.Delete(delfilepath);
                  }
                  catch (Exception ex)
                  {
                    Message = "File is used by another Process.Please try again";
                    goto label;
                  }
                }
              }
            }

            string UploadfileName = "";
            if (file.Length > 0)
            {
              UploadfileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
              string fullPath = Path.Combine(AppFilePath, UploadfileName);
              try
              {
                // System.IO.File.Delete(fullPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                  file.CopyTo(stream);
                }
              }
              catch (Exception e)
              {
                Message = "File is used by another Process.Please try again";
                goto label;
              }
            }
            string subpath = Path.Combine("\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat, final_file);
            
            OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Persist Security Info=False");
            my_con.Open();
            try
            {
              OleDbCommand o_cmd = new OleDbCommand("select * from [Sheet1$]", my_con);
              OleDbDataReader o_dr = o_cmd.ExecuteReader();
              DataTable dt = new DataTable();
              dt.Load(o_dr);
              int count = dt.Rows.Count;
              int result1 = 0;
              int result2 = 0;

              //for fetching sheet2 data
              string query1 = "select*from [Sheet2$]";
              OleDbCommand o_cmd1 = new OleDbCommand(query1, my_con);
              OleDbDataReader o_dr1 = o_cmd1.ExecuteReader();
              DataTable dttable = new DataTable();
              dttable.Load(o_dr1);

              string opnameexcel = dt.Rows[1][2].ToString();
              opnameexcel = opnameexcel.Replace("#", ".");
              string excelfiledate = dt.Rows[1][5].ToString();
              string excelcode = dttable.Columns[0].ColumnName.ToString();

              string blkdesc = "";
              string blknom = "";
              string mtdesc = "";

              if (month == 1) mtdesc = "January";
              if (month == 2) mtdesc = "February";
              if (month == 3) mtdesc = "March";
              if (month == 4) mtdesc = "April";
              if (month == 5)mtdesc = "May";
              if (month == 6)mtdesc = "June";
              if (month == 7)mtdesc = "July";
              if (month == 8)  mtdesc = "August";
              if (month == 9)  mtdesc = "September";
              if (month == 10) mtdesc = "October";
              if (month == 11)  mtdesc = "November";
              if (month == 12) mtdesc = "December";

              string uploadfiledate = mtdesc + "_" + year;
              if (block_category == 1)
              {
                blkdesc = "NOMINATION";
                blknom = "NOM";
              }
              else if (block_category == 2)
              {
                blkdesc = "ALL";
                blknom = "ALL";
              }
              //check validation
              if (excelcode == "RIGCODE309AM1")
              {
              }
              else
              {
                Message = "This is not a uploaded template.Please download correct template";
                goto label;
              }
              if (opnameexcel == OPERATOR_NAMEexcel)
              {
              }
              else
              {
                Message = "Please specify correct Operator Name";
                goto label;
              }

              if (excelfiledate == uploadfiledate)
              {
              }
              else
              {
                Message = "Month and year in Excel File should be same as Selected Month and year";
                goto label;
              }

              if (dt.Rows[1][8].ToString() == blkdesc || dt.Rows[1][8].ToString() == blknom)
              {
              }
              else
              {
                Message = "Please specify correct Block Type";
                goto label;
              }

              for (int i = 7; i < count; i++)
              {
                start = dt.Rows[i][0].ToString();
                if (start != "")
                {
                  slno = Convert.ToInt32(dt.Rows[i][0].ToString());
                  opearation_area = dt.Rows[i][1].ToString();
                  rigname = dt.Rows[i][2].ToString();
                  operating_status = dt.Rows[i][3].ToString();
                  rig_type = dt.Rows[i][4].ToString();
                  if (dt.Rows[i][5].ToString().Length == 0)
                    exp_well = (int?)null;
                  else
                    exp_well = Convert.ToInt32(dt.Rows[i][5].ToString());
                  if (dt.Rows[i][6].ToString().Length == 0)
                    exp_met = (double?)null;
                  else
                    exp_met = Convert.ToDouble(dt.Rows[i][6].ToString());
                  if (dt.Rows[i][7].ToString().Length == 0)
                    dev_well = (int?)null;
                  else
                    dev_well = Convert.ToInt32(dt.Rows[i][7].ToString());
                  if (dt.Rows[i][8].ToString().Length == 0)
                    dev_met = (double?)null;
                  else
                    dev_met = Convert.ToDouble(dt.Rows[i][8].ToString());
                  if (dt.Rows[i][9].ToString().Length == 0)
                    rig_mode_time_rb = (double?)null;
                  else
                    rig_mode_time_rb = Convert.ToDouble(dt.Rows[i][9].ToString());
                  if (dt.Rows[i][10].ToString().Length == 0)
                    rig_mode_time_dr = (double?)null;
                  else
                    rig_mode_time_dr = Convert.ToDouble(dt.Rows[i][10].ToString());
                  if (dt.Rows[i][11].ToString().Length == 0)
                    rig_mode_time_pt = (double?)null;
                  else
                    rig_mode_time_pt = Convert.ToDouble(dt.Rows[i][11].ToString());
                  if (dt.Rows[i][12].ToString().Length == 0)
                    outcycle_caprep = (double?)null;
                  else
                    outcycle_caprep = Convert.ToDouble(dt.Rows[i][12].ToString());
                  if (dt.Rows[i][13].ToString().Length == 0)
                    outcycle_oth = (double?)null;
                  else
                    outcycle_oth = Convert.ToDouble(dt.Rows[i][13].ToString());
                  if (dt.Rows[i][14].ToString().Length == 0)
                    npd_complication_day = (double?)null;
                  else
                    npd_complication_day = Convert.ToDouble(dt.Rows[i][14].ToString());
                  if (dt.Rows[i][15].ToString().Length == 0)
                    npd_complication_per = (double?)null;
                  else
                    npd_complication_per = Convert.ToDouble(dt.Rows[i][15].ToString());
                  if (dt.Rows[i][16].ToString().Length == 0)
                    npd_repair_day = (double?)null;
                  else
                    npd_repair_day = Convert.ToDouble(dt.Rows[i][16].ToString());
                  if (dt.Rows[i][17].ToString().Length == 0)
                    npd_repair_per = (double?)null;
                  else
                    npd_repair_per = Convert.ToDouble(dt.Rows[i][17].ToString());
                  if (dt.Rows[i][18].ToString().Length == 0)
                    cycle_day = (double?)null;
                  else
                    cycle_day = Convert.ToDouble(dt.Rows[i][18].ToString());
                  if (dt.Rows[i][19].ToString().Length == 0)
                    comm_day = (double?)null;
                  else
                    comm_day = Convert.ToDouble(dt.Rows[i][19].ToString());
                  if (dt.Rows[i][20].ToString().Length == 0)
                  {
                    cycle_exp = (double?)null;
                  }
                  else
                    cycle_exp = Convert.ToDouble(dt.Rows[i][20].ToString());
                  if (dt.Rows[i][21].ToString().Length == 0)
                    cycle_dev = (double?)null;
                  else
                    cycle_dev = Convert.ToDouble(dt.Rows[i][21].ToString());
                  if (dt.Rows[i][22].ToString().Length == 0)
                    cyclespeed = (double?)null;
                  else
                    cyclespeed = Convert.ToDouble(dt.Rows[i][22].ToString());
                  if (dt.Rows[i][23].ToString().Length == 0)
                    comm_exp = (double?)null;
                  else
                    comm_exp = Convert.ToDouble(dt.Rows[i][23].ToString());
                  if (dt.Rows[i][24].ToString().Length == 0)
                    comm_dev = (double?)null;
                  else
                    comm_dev = Convert.ToDouble(dt.Rows[i][24].ToString());
                  if (dt.Rows[i][25].ToString().Length == 0)
                    commspeed = (double?)null;
                  else
                    commspeed = Convert.ToDouble(dt.Rows[i][25].ToString());
                  if (dt.Rows[i][26].ToString().Length == 0)
                    remarks = "";
                  else
                    remarks = dt.Rows[i][26].ToString();
                  submissiondate = DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (start == "")
                {
                  //no Insertion
                }
                else
                {
                  switch (opearation_area)
                  {
                    case "Onshore":
                      operation_area_id = 1;
                      break;
                    case "Offshore(SW)":
                      operation_area_id = 2;
                      break;
                    case "Offshore(DW)":
                      operation_area_id = 3;
                      break;
                  }

                  switch (operating_status)
                  {
                    case "Owned":
                      operating_status_id = 1;
                      break;
                    case "Ch-Hired":
                      operating_status_id = 2;
                      break;
                    case "MMC":
                      operating_status_id = 3;
                      break;
                  }                 
                  result1 = rgm.UploadExcelRigWiseExcel(slno, operatiorid, operation_area_id, blockid[0], blockid[1], rigname, operating_status_id, rig_type, exp_well, exp_met, dev_well, dev_met, rig_mode_time_rb,
                    rig_mode_time_dr, rig_mode_time_pt, outcycle_caprep, outcycle_oth, npd_complication_day, npd_complication_per,
                    npd_repair_day, npd_repair_per, cycle_day, comm_day, cycle_exp, cycle_dev, cyclespeed, comm_exp, comm_dev, commspeed,
                    remarks, month, year, submissiondate, block_category,Status);
                }
              }

              rigexists = new Datamodel().get_RIGDataExist2(monthstring, yearstring, blk_cat);
              if (rigexists == "")
              {
                Message = "There is problem in File uploading!!";
              }
              else
              {
                result2 = rgm.UploadFileData(final_file, date, 9, 3, subpath, block_category, null, USERID);

                adminrigpath = _hostingEnvironment.WebRootPath + "\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
                if (exist_file_admin == 0)
                {
                  filenameadmin = final_file;
                }
                else
                {
                  filenameadmin = file_name + "_Updated" + exist_file_admin + ".xlsm";
                }
                string pathadmin = Path.Combine(adminrigpath, filenameadmin);
                //file.SaveAs(pathadmin);
                //file.InputStream.Dispose();
                //GC.Collect();

                //string subpathadmin = Path.Combine("\\Upload\\RigExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat, filenameadmin);
                //result2 = rgm.UploadFileData(filenameadmin, date, 9, 3, subpathadmin, block_category, null, "Admin");
                Message = "File uploaded successfully";
              }

            }
            catch (Exception ex2)
            {
              Message = "ERROR:" + ex2.Message.ToString();
            }
            my_con.Close();
          }
          else
          {
            Message = "You have already uploaded a file.";
          }
        }
        catch (Exception ex1)
        {
          Message = "ERROR:" + ex1.Message.ToString();
        }
        //finally
        //{
        //  file.InputStream.Dispose();
        //}
      }
      else
      {
        Message = "You have not specified a file.";
      }
       label:
      return Message;
    }

    [HttpPost("[action]")]
    public string CreateTempleteWellWiseExcel(WellWisePerformance wwp)
    {
      mt = wwp.WELL_MONTH.ToString();
      string monthname = "";
      if (mt == "1") { monthname = "January"; }
      if (mt == "2") { monthname = "February"; }
      if (mt == "3") { monthname = "March"; }
      if (mt == "4") { monthname = "April"; }
      if (mt == "5") { monthname = "May"; }
      if (mt == "6") { monthname = "June"; }
      if (mt == "7") { monthname = "July"; }
      if (mt == "8") { monthname = "August"; }
      if (mt == "9") { monthname = "September"; }
      if (mt == "10") { monthname = "October"; }
      if (mt == "11") { monthname = "November"; }
      if (mt == "12") { monthname = "December"; }

      yr = wwp.WELL_YEAR;
      OPERATORID = OPERATOR_ID;
      blk_cat = wwp.BLOCK_CATEGORY.ToString();
      string _filePath = _hostingEnvironment.WebRootPath;
      return new Datamodel().CreateTempleteWellWiseExcel(monthname, yr, OPERATORID, blk_cat, _filePath);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadFileWellWise()
    {
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\WellExcel_Template\\";
      string block = "";
      if (blk_cat == "1")
        block = "NOM";
      if (blk_cat == "2")
        block = "ALL";
      string date = "01/" + mt + "/" + yr;
      try
      {
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "WELL_WISE_PERFORMANCE" + "_" + block + ".xlsm";
        string fpath = _filePath + filenm;
        if (!System.IO.File.Exists(fpath))
          return NotFound();
        var memory = new MemoryStream();
        using (var stream = new FileStream(fpath, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, GetContentType(fpath), filenm);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }


    public int getWellFileExist(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      int total = 0;
      string adminapprootpath = _hostingEnvironment.WebRootPath;
      string AppFilePath = "";
      AppFilePath = adminapprootpath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyear + "\\" + blkcat;
      bool exists = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\WellExcelUpload\\" + USERID);
      if (!exists)       System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\WellExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyear);
       if (!exists1)        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)       System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsm").Select(path => Path.GetFileName(path)) .ToList<string>();
      total = filenames.Count;
      return total;
    }


    public List<string> GetWellFileExistUser(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      string AppFilePath = "";
      AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID+ "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID);
      if (!exists)   System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1)  System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)        System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsm")  .Select(path => Path.GetFileName(path)).ToList<string>();
      return filenames;
    }

    [HttpPost("[action]")]
    public string UploadExcelWellWiseExcel(IFormFile IFormFile)
    {
      var file = Request.Form.Files[0];
      string mthidden = HttpContext.Request.Form["Months"];
      string yrhidden = HttpContext.Request.Form["Years"];
      string blkcathidden = HttpContext.Request.Form["BlockTypes"];
      string Status = HttpContext.Request.Form["Status"];
      mt = "";
      yr = 0;
      blk_cat = "";
      int block_category = 0;
      if (blk_cat == "0" || blk_cat == null || blk_cat == "")
      {
        block_category = Convert.ToInt32(blkcathidden);
        blk_cat = blkcathidden;
      }
      else
      {
        block_category = Convert.ToInt32(blk_cat);
      }
      int slno = 0;
      string start = "";
      string wellname = "";
      string blocktype = "";
      int[] blockid = { 0, 0 };
      string block_name = "";
      string op_area = "";
      int op_area_id = 0;
      string rigname = "";
      double? td_md = null;
      double? td_tvd = null;
      double? dd_md = null;
      double? dd_tvd = null;
      double? water_depth = null;
      string spudate = "";
      string herdate = "";
      string rrdate = "";
      double? cpplanned = null;
      double? cpactual = null;
      double? rbplanned = null;
      double? rbactual = null;
      double? drplanned = null;
      double? dractual = null;
      double? ptplanned = null;
      double? ptactual = null;
      double? cyclespeed = null;
      double? commspeed = null;
      string remarks = "";
      string s_lat = "";
      string s_lon = "";
      string ss_lat = "";
      string ss_lon = "";
      int operationid = OPERATOR_ID;
      string submissiondate = "";
      int month = 0;
      int year = 0;
      string wellcategory = "";
      int wellcategoryid = 0;
      string welltype = "";
      int welltypeid = 0;
      string file_name = "";
      int valid_block = 0;
      string Message = "";
      string file_final_name = "";
      string file_final_name_user = "";
      string state = "";
      string welldataexist = "";
      string monthyearfolder = "";
      string monthstring = "";
      string yearstring = "";
      string path = "";
      if (file != null && file.Length > 0)
      {
        try
        {
          if (mt == "" || mt == null)
          {
            month = Convert.ToInt16(mthidden);
          }
          else
          {
            if (mt == "January")month = 1;
            if (mt == "February")month = 2;
            if (mt == "March") month = 3;
            if (mt == "April")month = 4;
            if (mt == "May") month = 5;
            if (mt == "June")month = 6;
            if (mt == "July")month = 7;
            if (mt == "August")month = 8;
            if (mt == "September")month = 9;
            if (mt == "October")month = 10;
            if (mt == "November")month = 11;
            if (mt == "December")month = 12;
          }

          if (yr == 0 || yr == null)  { year = Convert.ToInt16(yrhidden);}
          else {year = yr;}

          monthstring = Convert.ToString(month);
          yearstring = Convert.ToString(year);

          monthyearfolder = "1" + "-" + month + "-" + year;

          welldataexist = new Datamodel().get_WELLDataExist1(monthstring, yearstring, blk_cat);
          List<string> filenames = new List<string>();
          int exist_file_user = 0;
          if (welldataexist == "")
          {
            string file_name_ext = Path.GetFileName(file.FileName);
            string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);
            int exist_file_admin = getWellFileExist(monthyearfolder, blk_cat);
            filenames = GetWellFileExistUser(monthyearfolder, blk_cat);
            exist_file_user = filenames.Count;
            string file_name_new = file_name_get.Replace(" ", "");
            file_name = file_name_new.Replace("\'", "");

            string final_file = file_name + ".xlsm";
            string date = "01/" + month + "/" + year;
            string AppFilePath = "";
            string adminrigpath = "";
            string filenameadmin = "";
            string delfilepath = "";
            string adminapprootpath = _hostingEnvironment.WebRootPath;

            AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
            path = Path.Combine(AppFilePath, final_file);

            if (exist_file_user == 0)
            {              
            }
            else
            {
              for (int i = 0; i < exist_file_user; i++)
              {
                delfilepath = Path.Combine(AppFilePath, filenames[i]);
                if (System.IO.File.Exists(delfilepath))
                {
                  string deletewelldata = new Datamodel().Delete_WELLDataExist(monthstring, yearstring, blk_cat);
                  try
                  {
                    System.IO.File.Delete(delfilepath);
                  }
                  catch (Exception ex)
                  {
                    Message = "File is used by another Process.Please try again";
                    goto label;
                  }
                }
              }
            }

            string monthyearfolderdata = "1" + "-" + mthidden + "-" + yrhidden;
            string folderName = "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
              Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {              
                final_file = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(AppFilePath, final_file);
                try
                {
                  // System.IO.File.Delete(fullPath);
                  using (var stream = new FileStream(fullPath, FileMode.Create))
                  {
                    file.CopyTo(stream);
                  }
                }
                catch (Exception e)
                {
                  Message = "File is used by another Process.Please try again";

                }
            }

            string subpath = Path.Combine("\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat, final_file);
            OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Persist Security Info=False");
            my_con.Open();
            Datamodel rgm = new Datamodel();
            try
            {
              OleDbCommand o_cmd = new OleDbCommand("select * from [Sheet1$]", my_con);
              OleDbDataReader o_dr = o_cmd.ExecuteReader();
              DataTable dt = new DataTable();
              dt.Load(o_dr);
              int count = dt.Rows.Count;
              int result1 = 0;

              //for fetching sheet2 data
              string query1 = "select*from [Sheet2$]";
              OleDbCommand o_cmd1 = new OleDbCommand(query1, my_con);
              OleDbDataReader o_dr1 = o_cmd1.ExecuteReader();
              DataTable dttable = new DataTable();
              dttable.Load(o_dr1);
              string excelcode = dttable.Columns[0].ColumnName.ToString();

              string opnameexcel = dt.Rows[1][2].ToString();
              opnameexcel = opnameexcel.Replace("#", ".");
              string excelfiledate = dt.Rows[1][5].ToString();

              string blkdesc = "";
              string blknom = "";
              string mtdesc = "";

              if (month == 1) mtdesc = "January";
              if (month == 2) mtdesc = "February";
              if (month == 3) mtdesc = "March";
              if (month == 4) mtdesc = "April";
              if (month == 5) mtdesc = "May";
              if (month == 6) mtdesc = "June";
              if (month == 7) mtdesc = "July";
              if (month == 8) mtdesc = "August";
              if (month == 9) mtdesc = "September";
              if (month == 10) mtdesc = "October";
              if (month == 11) mtdesc = "November";
              if (month == 12) mtdesc = "December";

              string uploadfiledate = mtdesc + "_" + year;

              if (block_category == 1)
              {
                blkdesc = "NOMINATION";
                blknom = "NOM";
              }
              else if (block_category == 2)
              {
                blkdesc = "ALL";
                blknom = "ALL";
              }
              //check validation
              if (excelcode == "WELLCODE310AM1")
              {
              }
              else
              {
                Message = "This is not a uploaded template.Please download correct template";
                goto label;
              }
              if (opnameexcel == OPERATOR_NAMEexcel)
              {
              }
              else
              {
                Message = "Please specify correct Operator Name";
                goto label;
              }

              if (excelfiledate == uploadfiledate)
              {
              }
              else
              {
                Message = "Month and year in Excel File should be same as Selected Month and year";
                goto label;
              }

              if (dt.Rows[1][7].ToString() == blkdesc || dt.Rows[1][7].ToString() == blknom)
              {
              }
              else
              {
                Message = "Please specify correct Block Type";
                goto label;
              }

              for (int i = 6; i < count; i++)
              {
                start = dt.Rows[i][0].ToString();
                if (start != "")
                {
                  slno = Convert.ToInt32(dt.Rows[i][0].ToString());
                  wellname = dt.Rows[i][1].ToString();
                  blocktype = dt.Rows[i][2].ToString();

                  if (blocktype == "PRE_NELP")
                    blocktype = "PRE-NELP";
                  if (blocktype == "SHALE_GAS")
                    blocktype = "SHALE GAS";

                  block_name = dt.Rows[i][3].ToString();
                  if (dt.Rows[i][4].ToString().Length == 0)
                    op_area = "";
                  else
                    op_area = dt.Rows[i][4].ToString();
                  if (dt.Rows[i][5].ToString().Length == 0)
                    state = "";
                  else
                    state = dt.Rows[i][5].ToString();
                  if (dt.Rows[i][6].ToString().Length == 0)
                    s_lat = "";
                  else
                    s_lat = dt.Rows[i][6].ToString();
                  if (dt.Rows[i][7].ToString().Length == 0)
                    s_lon = "";
                  else
                    s_lon = dt.Rows[i][7].ToString();
                  if (dt.Rows[i][8].ToString().Length == 0)
                    ss_lat = "";
                  else
                    ss_lat = dt.Rows[i][8].ToString();
                  if (dt.Rows[i][9].ToString().Length == 0)
                    ss_lon = "";
                  else
                    ss_lon = dt.Rows[i][9].ToString();
                  rigname = dt.Rows[i][10].ToString();
                  wellcategory = dt.Rows[i][11].ToString();
                  if (dt.Rows[i][12].ToString().Length == 0)
                    welltype = "";
                  else
                    welltype = dt.Rows[i][12].ToString();
                  water_depth = Convert.ToDouble((dt.Rows[i][13].ToString().Length == 0 ? 0 : dt.Rows[i][13]).ToString());
                  if (dt.Rows[i][14].ToString().Length == 0)
                    td_md = (double?)null;
                  else
                    td_md = Convert.ToDouble(dt.Rows[i][14].ToString());
                  if (dt.Rows[i][15].ToString().Length == 0)
                    td_tvd = (double?)null;
                  else
                    td_tvd = Convert.ToDouble(dt.Rows[i][15].ToString());
                  if (dt.Rows[i][16].ToString().Length == 0)
                    dd_md = (double?)null;
                  else
                    dd_md = Convert.ToDouble(dt.Rows[i][16].ToString());
                  if (dt.Rows[i][17].ToString().Length == 0)
                    dd_tvd = (double?)null;
                  else
                    dd_tvd = Convert.ToDouble(dt.Rows[i][17].ToString());
                  if (dt.Rows[i][18].ToString().Length == 0)
                    spudate = "";
                  else
                  {
                    spudate = dt.Rows[i][18].ToString();
                    //chk spud date format
                    int len = spudate.Length;
                    if (len > 11)
                      spudate = Convert.ToDateTime(spudate).ToString("dd-MMM-yyyy");
                    string[] strArr = null;
                    char[] splitchar = { '-' };
                    strArr = spudate.Split(splitchar);
                    if (strArr.Length == 0 && spudate != "")
                    {
                      Message = "Please enter Spud Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datad = 0;
                      delete_datad = rgm.delete_WellData(month, year);
                      goto label;
                    }
                    else if (strArr.Length <= 1 && spudate != "")
                    {
                      Message = "Please enter Spud Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datad = 0;
                      delete_datad = rgm.delete_WellData(month, year);
                      goto label;
                    }
                    else if (strArr.Length > 1 && spudate != "")
                    {
                      if (strArr[1] == "Jan" || strArr[1] == "Feb" || strArr[1] == "Mar" || strArr[1] == "Apr" || strArr[1] == "May" || strArr[1] == "Jun" || strArr[1] == "Jul" || strArr[1] == "Aug" || strArr[1] == "Sep" || strArr[1] == "Oct" || strArr[1] == "Nov" || strArr[1] == "Dec")
                      {

                      }
                      else
                      {
                        Message = "Please enter Spud Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                        int delete_datadd = 0;
                        delete_datadd = rgm.delete_WellData(month, year);
                        goto label;
                      }
                    }

                  }
                  if (dt.Rows[i][19].ToString().Length == 0)
                    herdate = "";
                  else
                  {
                    herdate = dt.Rows[i][19].ToString();
                    //chk hermatical date format
                    int len = herdate.Length;
                    if (len > 11)
                      herdate = Convert.ToDateTime(herdate).ToString("dd-MMM-yyyy");
                    string[] strArr1 = null;
                    char[] splitchar1 = { '-' };
                    strArr1 = herdate.Split(splitchar1);
                    if (strArr1.Length == 0 && herdate != "")
                    {
                      Message = "Please enter Hermetical Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datad1 = 0;
                      delete_datad1 = rgm.delete_WellData(month, year);
                      goto label;
                    }
                    else if (strArr1.Length <= 1 && herdate != "")
                    {
                      Message = "Please enter Hermetical Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datadd = 0;
                      delete_datadd = rgm.delete_WellData(month, year);
                      goto label;
                    }
                    else if (strArr1.Length > 1 && herdate != "")
                    {
                      if (strArr1[1] == "Jan" || strArr1[1] == "Feb" || strArr1[1] == "Mar" || strArr1[1] == "Apr" || strArr1[1] == "May" || strArr1[1] == "Jun" || strArr1[1] == "Jul" || strArr1[1] == "Aug" || strArr1[1] == "Sep" || strArr1[1] == "Oct" || strArr1[1] == "Nov" || strArr1[1] == "Dec")
                      {

                      }
                      else
                      {
                        Message = "Please enter Hermetical Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                        int delete_datadd = 0;
                        delete_datadd = rgm.delete_WellData(month, year);
                        goto label;
                      }
                    }


                  }
                  if (dt.Rows[i][20].ToString().Length == 0)
                    rrdate = "";
                  else
                  {
                    rrdate = dt.Rows[i][20].ToString();
                    //chk rig release date format
                    int len = rrdate.Length;
                    if (len > 11)
                      rrdate = Convert.ToDateTime(rrdate).ToString("dd-MMM-yyyy");
                    string[] strArr2 = null;
                    char[] splitchar2 = { '-' };
                    strArr2 = rrdate.Split(splitchar2);
                    if (strArr2.Length == 0 && rrdate != "")
                    {
                      Message = "Please enter Rig Release Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datad2 = 0;
                      delete_datad2 = rgm.delete_WellData(month, year);
                      goto label;
                    }
                    else if (strArr2.Length <= 1 && rrdate != "")
                    {
                      Message = "Please enter Rig Release Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                      int delete_datad2 = 0;
                      delete_datad2 = rgm.delete_WellData(month, year);
                      goto label;
                    }
                    else if (strArr2.Length > 1 && rrdate != "")
                    {
                      if (strArr2[1] == "Jan" || strArr2[1] == "Feb" || strArr2[1] == "Mar" || strArr2[1] == "Apr" || strArr2[1] == "May" || strArr2[1] == "Jun" || strArr2[1] == "Jul" || strArr2[1] == "Aug" || strArr2[1] == "Sep" || strArr2[1] == "Oct" || strArr2[1] == "Nov" || strArr2[1] == "Dec")
                      {

                      }
                      else
                      {
                        Message = "Please enter Rig Release Date in correct format (dd-mmm-yyyy i.e 01-Mar-2012)";
                        int delete_datadd2 = 0;
                        delete_datadd2 = rgm.delete_WellData(month, year);
                        goto label;
                      }
                    }

                  }
                  if (dt.Rows[i][21].ToString().Length == 0)
                    cpplanned = (double?)null;
                  else
                    cpplanned = Convert.ToDouble(dt.Rows[i][21].ToString());
                  if (dt.Rows[i][22].ToString().Length == 0)
                    cpactual = (double?)null;
                  else
                    cpactual = Convert.ToDouble(dt.Rows[i][22].ToString());
                  if (dt.Rows[i][23].ToString().Length == 0)
                    rbplanned = (double?)null;
                  else
                    rbplanned = Convert.ToDouble(dt.Rows[i][23].ToString());
                  if (dt.Rows[i][24].ToString().Length == 0)
                    rbactual = (double?)null;
                  else
                    rbactual = Convert.ToDouble(dt.Rows[i][24].ToString());
                  if (dt.Rows[i][25].ToString().Length == 0)
                    drplanned = (double?)null;
                  else
                    drplanned = Convert.ToDouble(dt.Rows[i][25].ToString());
                  if (dt.Rows[i][26].ToString().Length == 0)
                    dractual = (double?)null;
                  else
                    dractual = Convert.ToDouble(dt.Rows[i][26].ToString());
                  if (dt.Rows[i][27].ToString().Length == 0)
                    ptplanned = (double?)null;
                  else
                    ptplanned = Convert.ToDouble(dt.Rows[i][27].ToString());
                  if (dt.Rows[i][28].ToString().Length == 0)
                    ptactual = (double?)null;
                  else
                    ptactual = Convert.ToDouble(dt.Rows[i][28].ToString());
                  if (dt.Rows[i][29].ToString().Length == 0)
                    cyclespeed = (double?)null;
                  else
                    cyclespeed = Convert.ToDouble(dt.Rows[i][29].ToString());
                  if (dt.Rows[i][30].ToString().Length == 0)
                    commspeed = (double?)null;
                  else
                    commspeed = Convert.ToDouble(dt.Rows[i][30].ToString());
                  if (dt.Rows[i][31].ToString().Length == 0)
                    remarks = "";
                  else
                    remarks = dt.Rows[i][31].ToString();
                  submissiondate = DateTime.Now.ToString("dd/MM/yyyy");
                  operationid = OPERATOR_ID;
                }
                if (start == "")
                {
                  //no insertion
                }
                else
                {
                  Datamodel dpr_data = new Datamodel();
                  blockid = dpr_data.GetBlockID(block_name);
                  if (blockid[0] == 0)
                  {
                    Message = "Please specify correct Block Name";
                    int delete_datadd2 = 0;
                    delete_datadd2 = rgm.delete_WellData(month, year);
                    goto label;

                  }
                  else
                  {
                    string blktypename = "";

                    if (blockid[1] == 1)
                      blktypename = "NELP";
                    if (blockid[1] == 2)
                      blktypename = "PRE-NELP";
                    if (blockid[1] == 3)
                      blktypename = "CBM";
                    if (blockid[1] == 4)
                      blktypename = "NOMINATION";
                    if (blockid[1] == 5)
                      blktypename = "SHALE GAS";

                    if (blktypename == blocktype)
                    {

                    }
                    else
                    {
                      Message = block_name + " block is not a " + blocktype + " block but it is a " + blktypename + " block. Please specify correct block type for block name.";
                      int delete_datadd2 = 0;
                      delete_datadd2 = rgm.delete_WellData(month, year);
                      goto label;
                    }

                    string Well_data_exist = string.Empty;
                    Well_data_exist = rgm.existData_WELL(blockid[0], blockid[1], block_category, month, year, wellname);

                    if (Well_data_exist == "")
                    {
                      switch (wellcategory)
                      {
                        case "Exploratory":
                          wellcategoryid = 1;
                          break;
                        case "Development":
                          wellcategoryid = 2;
                          break;
                        case "Appraisal":
                          wellcategoryid = 3;
                          break;
                      }

                      switch (welltype)
                      {
                        case "Vertical":
                          welltypeid = 1;
                          break;
                        case "Directional":
                          welltypeid = 2;
                          break;
                        case "Horizontal":
                          welltypeid = 3;
                          break;
                        case "Multilateral":
                          welltypeid = 4;
                          break;
                        case "Sidetrack":
                          welltypeid = 5;
                          break;
                      }

                      switch (op_area)
                      {
                        case "Onshore":
                          op_area_id = 1;
                          break;
                        case "Offshore(SW)":
                          op_area_id = 2;
                          break;
                        case "Offshore(DW)":
                          op_area_id = 3;
                          break;
                      }
                      if (spudate == "")
                      {
                        Message = "Spud Date can not be null!";


                        int delete_data = 0;
                        delete_data = rgm.delete_WellData(month, year);
                        goto label;
                      }

                      if (op_area == "ONSHORE")
                      {
                        if (water_depth == 0.0 || water_depth == null)
                        {
                        }
                        else
                        {
                          Message = "In Onshore area do not enter water depth !!";

                          int delete_datawd = 0;
                          delete_datawd = rgm.delete_WellData(month, year);
                          goto label;
                        }
                      }

                      if (welltype == "Vertical")
                      {
                        if (dd_md == dd_tvd || td_md == td_tvd)
                        {
                        }
                        else
                        {
                          Message = "In Vertical well Target and Drill depth DD and TVD must be same !!";

                          int delete_datawt = 0;
                          delete_datawt = rgm.delete_WellData(month, year);
                          goto label;
                        }
                      }
                      result1 = rgm.UploadExcelWellWiseExcel(slno, wellname, blocktype, blockid[0], blockid[1], state, s_lat, s_lon, ss_lat, ss_lon, rigname, wellcategoryid, td_md, td_tvd, dd_md, dd_tvd, op_area_id, water_depth, welltypeid, spudate, herdate, rrdate, cpplanned, cpactual, rbplanned, rbactual, drplanned, dractual, ptplanned, ptactual, cyclespeed, commspeed, remarks, operationid, submissiondate, month, year, block_category,Status);
                    }
                    else
                    {
                      Message = "User can not upload WELL data of that block which is not listed in list of Block Name.";
                      int delete_data = 0;
                      delete_data = rgm.delete_WellData(month, year);
                      goto label;
                    }
                  }
                }
              }
              welldataexist = new Datamodel().get_WELLDataExist2(monthstring, yearstring, blk_cat);
              if (welldataexist == "")
              {

                Message = "There is problem in File uploading!!";
              }
              else
              {
                int result2 = 0;
                result2 = rgm.UploadFileData(final_file, date, 10, 3, subpath, block_category, null, USERID);

                adminrigpath = _hostingEnvironment.WebRootPath + "\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
                if (exist_file_admin == 0)
                {
                  filenameadmin = final_file;
                }
                else
                {
                  filenameadmin = file_name + "_Updated" + exist_file_admin + ".xlsm";
                }
                string pathadmin = Path.Combine(adminrigpath, filenameadmin);
                //file.SaveAs(pathadmin);
                //file.InputStream.Dispose();
                //GC.Collect();
                
                string subpathadmin = Path.Combine("\\Upload\\WellExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat, filenameadmin);
               // result2 = rgm.UploadFileData(filenameadmin, date, 10, 3, subpathadmin, block_category, null, "Admin");
                Message = "File uploaded successfully";
              }

            }
            catch (Exception ex2)
            {
              Message = "ERROR:" + ex2.Message.ToString();

              int delete_data = 0;
              delete_data = rgm.delete_WellData(month, year);
              goto label;
            }
            my_con.Close();
          }
          else
          {
            Message = "You have already uploaded a file.";
          }


        }
        catch (Exception ex1)
        {

          Message = "ERROR:" + ex1.Message.ToString();
        }
        //finally
        //{
        //  file.InputStream.Dispose();
        //}
      }
      else
      {
        Message = "You have not specified a file.";
      }
    label:
      return Message;
    }

    [HttpPost("[action]")]
    public string CreateTempleteCumulativeDrillingExcel(CumulativeDrillingPerformance cdp)
    {
      mt = cdp.MONTH.ToString();
      string monthname = "";
      if (mt == "1") { monthname = "January"; }
      if (mt == "2") { monthname = "February"; }
      if (mt == "3") { monthname = "March"; }
      if (mt == "4") { monthname = "April"; }
      if (mt == "5") { monthname = "May"; }
      if (mt == "6") { monthname = "June"; }
      if (mt == "7") { monthname = "July"; }
      if (mt == "8") { monthname = "August"; }
      if (mt == "9") { monthname = "September"; }
      if (mt == "10") { monthname = "October"; }
      if (mt == "11") { monthname = "November"; }
      if (mt == "12") { monthname = "December"; }

      yr = cdp.YEAR;
      OPERATORID = OPERATOR_ID;
      blk_cat = cdp.BLOCK_CATEGORY.ToString();
      string _filePath = _hostingEnvironment.WebRootPath;
      return new Datamodel().CreateTempleteCumulativeDrillingExcel(monthname, yr, OPERATORID, blk_cat, _filePath);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadFileCumulativeDrilling()
    {
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcel_Template\\";
      string block = "";
      if (blk_cat == "1")
        block = "NOM";
      if (blk_cat == "2")
        block = "ALL";
      string date = "01/" + mt + "/" + yr;
      try
      {
        string d;
        d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        string adate = d;
        string r = adate.Replace("/", "-");
        string filenm = "CUMULATIVE_DRILLING_PERFORMANCE" + "_" + block + ".xlsx";
        string fpath = _filePath + filenm;
        if (!System.IO.File.Exists(fpath))
          return NotFound();

        var memory = new MemoryStream();
        using (var stream = new FileStream(fpath, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, GetContentType(fpath), filenm);

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int getPerformanceFileExist(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      int total = 0;
      string adminapprootpath = _hostingEnvironment.WebRootPath;
      string AppFilePath = "";

      AppFilePath = adminapprootpath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\PerformanceExcelUpload\\" + USERID);
      if (!exists)
        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\PerformanceExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(adminapprootpath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1)
        System.IO.Directory.CreateDirectory(adminapprootpath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)
        System.IO.Directory.CreateDirectory(AppFilePath);


      filenames = Directory.GetFiles(AppFilePath, "*.xlsx")
                              .Select(path => Path.GetFileName(path))
                              .ToList<string>();
      total = filenames.Count;


      return total;
    }
    public List<string> GetPerformanceFileExistUser(string monthyear, string blkcat)
    {
      List<string> filenames = new List<string>();
      string AppFilePath = "";

      AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyear + "\\" + blkcat;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID);
      if (!exists) System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyear);
      if (!exists1) System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyear);

      bool exists2 = System.IO.Directory.Exists(AppFilePath);
      if (!exists2)System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsx").Select(path => Path.GetFileName(path)).ToList<string>();
      return filenames;
    }
    

    [HttpPost("[action]")]
    public string UploadExcelCumulativeDrillingExcel(IFormFile IFormFile)
    {
      var file = Request.Form.Files[0];
      string mthidden = HttpContext.Request.Form["Months"];
      string yrhidden = HttpContext.Request.Form["Years"];
      string blkcathidden = HttpContext.Request.Form["BlockTypes"];
      string Status = HttpContext.Request.Form["Status"];
      string Message = "";
      mt = "";
      yr = 0;
      blk_cat = ""; 
      int slno = 0;
      string operatorid = "";
      int month = 0;
      int year = 0;

      int rigoperatorstatus = 0;
      int wellcategoryid = 0;
      int operationareaid = 0;
      int nowells = 0;
      double meterage = 0;
      string cyclespeed = "";
      string commspeed = "";
      string file_name = "";
      string rigmonth = "";
      string file_final_name = "";
      string file_final_name_user = "";

      int block_category = 0;

      if (blk_cat == "0" || blk_cat == null || blk_cat == "")
      {
        block_category = Convert.ToInt32(blkcathidden);
        blk_cat = blkcathidden;
      }
      else
      {
        block_category = Convert.ToInt32(blk_cat);
      }

      string MPRexists = "";
      string monthyearfolder = "";
      string monthstring = "";
      string yearstring = "";
      //month =Convert.ToInt32( file.FileName.Substring(25, 2));
      //year = Convert.ToInt32(file.FileName.Substring(20, 4));
      if (file != null && file.Length > 0)
      {
        if (mt == "" || mt == null)
        {
          month = Convert.ToInt16(mthidden);
        }
        else
        {
          if (mt == "January")            month = 1;
          if (mt == "February")            month = 2;
          if (mt == "March")            month = 3;
          if (mt == "April")            month = 4;
          if (mt == "May")            month = 5;
          if (mt == "June")            month = 6;
          if (mt == "July")            month = 7;
          if (mt == "August")            month = 8;
          if (mt == "September")            month = 9;
          if (mt == "October")            month = 10;
          if (mt == "November")            month = 11;
          if (mt == "December")            month = 12;
        }

        if (yr == 0 || yr == null)
        {
          year = Convert.ToInt16(yrhidden);
        }
        else
        {
          year = yr;
        }

        monthstring = Convert.ToString(month);
        yearstring = Convert.ToString(year);
        string path = "";
        monthyearfolder = "1" + "-" + month + "-" + year;
        try
        {
          MPRexists = new Datamodel().get_PerformanceDataExist1(monthstring, yearstring, blk_cat);
          List<string> filenames = new List<string>();
          int exist_file_user = 0;
          if (MPRexists == "")
          {
            string file_name_ext = Path.GetFileName(file.FileName);
            string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);
            int exist_file_admin = getPerformanceFileExist(monthyearfolder, blk_cat);
            filenames = GetPerformanceFileExistUser(monthyearfolder, blk_cat);
            exist_file_user = filenames.Count;
            string file_name_new = file_name_get.Replace(" ", "");
            file_name = file_name_new.Replace("\'", "");

            string final_file = file_name + ".xlsx";
            string date = "01/" + month + "/" + year;
            string AppFilePath = "";
            string adminrigpath = "";
            string filenameadmin = "";
            string delfilepath = "";
            string adminapprootpath = _hostingEnvironment.WebRootPath;
            AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
            path = Path.Combine(AppFilePath, final_file);
            string subpath = Path.Combine(AppFilePath, final_file);
            if (exist_file_user == 0)
            {
            }
            else
            {
              for (int i = 0; i < exist_file_user; i++)
              {
                delfilepath = Path.Combine(AppFilePath, filenames[i]);
                if (System.IO.File.Exists(delfilepath))
                {
                 string Delete_Performance = new Datamodel().Delete_PerformanceDataExist(monthstring, yearstring, blk_cat);
                  try
                  {
                    System.IO.File.Delete(delfilepath);
                  }
                  catch (Exception ex)
                  {
                    Message = "File is used by another Process.Please try again";
                    goto label;
                  }
                }
              }
            }
        
            if (file.Length > 0)
            {
              final_file = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
              string fullPath = Path.Combine(AppFilePath, final_file);
              try
              {
                // System.IO.File.Delete(fullPath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                  file.CopyTo(stream);
                }
              }
              catch (Exception e)
              {
                Message = "File is used by another Process.Please try again";
                goto label;
              }
            }

            OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Persist Security Info=False");
            my_con.Open();
            try
            {
              OleDbCommand o_cmd = new OleDbCommand("select*from [Sheet1$]", my_con);
              OleDbDataReader o_dr = o_cmd.ExecuteReader();
              DataTable dt = new DataTable();
              dt.Load(o_dr);
              int count = dt.Rows.Count;
              if (count < 2)
                Message = "Please specify correct Block Name";
              int result1 = 0;

              string opnameexcel = dt.Rows[1][3].ToString();
              opnameexcel = opnameexcel.Replace("#", ".");
              string excelfiledate = dt.Rows[1][8].ToString();
              string excelcode = dt.Rows[2][0].ToString();

              string blkdesc = "";
              string blknom = "";
              string mtdesc = "";

              if (month == 1) mtdesc = "January";              if (month == 2) mtdesc = "February";              if (month == 3) mtdesc = "March";
              if (month == 4) mtdesc = "April";              if (month == 5) mtdesc = "May";              if (month == 6) mtdesc = "June";
              if (month == 7) mtdesc = "July";              if (month == 8) mtdesc = "August";              if (month == 9) mtdesc = "September";
              if (month == 10) mtdesc = "October";              if (month == 11) mtdesc = "November";              if (month == 12) mtdesc = "December";

              string uploadfiledate = mtdesc + "_" + year;
              if (block_category == 1)
              {
                blkdesc = "NOMINATION";
                blknom = "NOM";
              }
              else if (block_category == 2)
              {
                blkdesc = "ALL";
                blknom = "ALL";
              }
              //check validation
              if (excelcode == "MPRCODE312AM1")
              {
              }
              else
              {
                Message = "This is not a uploaded template.Please download correct template";
                goto label;
              }

              if (opnameexcel == OPERATOR_NAMEexcel)
              {
              }
              else
              {
                Message = "Please specify correct Operator Name";
                goto label;
              }

              if (excelfiledate == uploadfiledate)
              {
              }
              else
              {
                Message = "Month and year in Excel File should be same as Selected Month and year";
                goto label;
              }

              if (dt.Rows[1][11].ToString() == blkdesc || dt.Rows[1][11].ToString() == blknom)
              {
              }
              else
              {
                Message = "Please specify correct Block Type";
                goto label;
              }
              Datamodel rgm = new Datamodel();
              // slno = 0;
              for (int i = 7; i <= 11; i++)
              {
                for (int j = 0; j <= 6; j++)
                {
                  if (j == 0)
                    wellcategoryid = 1;
                  if (j == 1)
                    wellcategoryid = 2;
                  if (j == 2)
                    wellcategoryid = 1;
                  if (j == 3)
                    wellcategoryid = 2;
                  if (j == 4)
                    wellcategoryid = 1;
                  if (j == 5)
                    wellcategoryid = 2;
                  if (j == 6)
                    wellcategoryid = 0;
                  if (j <= 1)
                    rigoperatorstatus = 1;
                  if (j > 1 && j <= 3)
                    rigoperatorstatus = 2;
                  if (j > 3)
                    rigoperatorstatus = 0;

                  slno++;
                  operatorid = OPERATOR_ID.ToString();

                  operationareaid = i - 6;

                  nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());
                  meterage = Convert.ToDouble(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                  rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                  cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                  commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();
                  result1 = rgm.UploadPerformanceExcel(slno, operatorid, month, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                 }
              }
              MPRexists = new Datamodel().get_PerformanceDataExist2(monthstring, yearstring, blk_cat);
              if (MPRexists == "")
              {
                Message = "There is problem in File uploading!!";
              }
              else
              {
                int result2 = 0;
                result2 = rgm.UploadFileData(final_file, date, 12, 3, subpath, block_category, null, USERID);
                adminrigpath = _hostingEnvironment.WebRootPath + "\\Upload\\PerformanceExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat;
                if (exist_file_admin == 0)
                {
                  filenameadmin = final_file;
                }
                else
                {
                  filenameadmin = file_name + "_Updated" + exist_file_admin + ".xlsx";
                }
                string pathadmin = Path.Combine(adminrigpath, filenameadmin);
                
                string subpathadmin = Path.Combine("PerformanceExcelUpload\\" + USERID + "\\" + monthyearfolder + "\\" + blk_cat, filenameadmin);
               // result2 = rgm.UploadFileData(filenameadmin, date, 12, 3, subpathadmin, block_category, null, "Admin");
                Message = "File uploaded successfully";
              }
            }
            catch (Exception ex2)
            {
              Message = "ERROR:" + ex2.Message.ToString();
            }
            my_con.Close();
          }
          else
          {
            Message = "You have already uploaded a file.";
          }
        }
        catch (Exception ex1)
        {
          Message = "ERROR:" + ex1.Message.ToString();
        }
      }
      else
      {
        Message = "You have not specified a file.";
      }
    label:
      return Message;      
    }
       
    [HttpPost("[action]")]
    public string CreateTempleteAnnualDrillingExcel(AnnualDrillingPlan adp)
    {
      string BE_RE = adp.BE_RE;
      string BE_RE_YR = adp.YEAR;
      OPERATORID = OPERATOR_ID;
      string qt1 = "";
      if (qt == "0" || qt == null || qt == "")
      {
        if (BE_RE == "5")
        {
          qt1 = "BE";
        }
        if (BE_RE == "6")
        {
          qt1 = "RE";
        }
      }
      blk_cat = adp.BLOCK_CATEGORY.ToString();
      string _filePath = _hostingEnvironment.WebRootPath;
      return new Datamodel().CreateTempleteAnnualDrillingExcel(qt1, BE_RE_YR, OPERATORID, blk_cat, _filePath);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> DownloadFileAnnualDrilling()
    {
      string _filePath = _hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcel_Template\\";
      string block = "";
      if (blk_cat == "1")
        block = "NOM";
      if (blk_cat == "2")
        block = "ALL";
      string date = qt + "/" + yer;
      // string date = "01/" + mt + "/" + yr;
      try
      {
        //string d;
        //d = date == null ? null : Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat).ToString("dd/MM/yyyy");
        //string adate = d;
        string r = date.Replace("/", "-");
        string filenm = "ANNUAL_DRILLING_PLAN" + "_" + block + ".xlsx";
        string fpath = _filePath + filenm;
        if (!System.IO.File.Exists(fpath))
          return NotFound();
        var memory = new MemoryStream();
        using (var stream = new FileStream(fpath, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, GetContentType(fpath), filenm);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpPost("[action]")]
    public string UploadExcelAnnualDrillingExcel(IFormFile IFormFile)
    {
      var file = Request.Form.Files[0];
      string bere = HttpContext.Request.Form["BE_RE"];
      string yrhidden = HttpContext.Request.Form["Years"];
      string blkcathidden = HttpContext.Request.Form["BlockTypes"];
      string Status = HttpContext.Request.Form["Status"];
      blk_cat = "";
      yer = "";
      qt = "";
      int block_category = 0;
      if (blk_cat == "0" || blk_cat == null || blk_cat == "")
      {
        block_category = Convert.ToInt32(blkcathidden);
        blk_cat = blkcathidden;
      }
      else
      {
        block_category = Convert.ToInt32(blk_cat);
      }
      // qt = "";
      int slno = 0;
      string operatorid = "";
      int quarter = 0;
      string year = "";

      int rigoperatorstatus = 0;
      int wellcategoryid = 0;
      int operationareaid = 0;
      int nowells = 0;
      int meterage = 0;

      string cyclespeed = "";
      string commspeed = "";
      string file_name = "";
      string rigmonth = "";
      List<int> allwells = new List<int>();
      List<int> allmeterage = new List<int>();
      List<string> allcyspeed = new List<string>();
      List<string> allcommspeed = new List<string>();
      List<string> allmonth = new List<string>();
      string Message = "";
      string file_final_name = "";
      string file_final_name_user = "";
      int curmonth = Convert.ToInt32(DateTime.Now.Month.ToString("00"));
      int FQ = 0;
      int SQ = 0;
      int TQ = 0;
      int LQ = 0;

      if (yer == "0" || yer == null || yer == "")
      {
        year = yrhidden;
        yer = yrhidden;
      }
      else
      {
        year = yer;
      }

      if (qt == "0" || qt == null || qt == "")
      {
        if (bere == "5")
        {
          qt = "BE";
        }
        else
        {
          qt = "RE";
        }
      }
      else
      {
      }
      string planperformancedataexist = "";
      string filedate = "";
      filedate = "01/04/" + year.Substring(0, 4);

      if (qt == "BE")
      {
        if (curmonth >= 1 && curmonth <= 12)
        {
          if (file != null && file.Length > 0)
          {
            string path = "";
            planperformancedataexist = new Datamodel().get_QuaterlyPerformanceDataExist1(bere, year, blk_cat);
            List<string> filenames = new List<string>();
            int exist_file = 0;
            try
            {
              if (planperformancedataexist == "")
              {
                string file_name_ext = Path.GetFileName(file.FileName);
                string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);
                filenames = getQPerformanceFileExistUser(qt, year, blk_cat);
                exist_file = filenames.Count;
                int exist_file_admin = getQPerformanceFileExistAdmin(qt, year, blk_cat);

                string file_name_new = file_name_get.Replace(" ", "");
                file_name = file_name_new.Replace("\'", "");

                string final_file = file_name + ".xlsx";
                string AppFilePath = "";
                string adminrigpath = "";
                string filenameadmin = "";
                string delfilepath = "";
                string adminapprootpath = _hostingEnvironment.WebRootPath;

                AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt;
                path = Path.Combine(AppFilePath, final_file);
                if (exist_file == 0)
                {
                }
                else
                {
                  for (int i = 0; i < exist_file; i++)
                  {
                    delfilepath = Path.Combine(AppFilePath, filenames[i]);
                    if (System.IO.File.Exists(delfilepath))
                    {
                      int DeleteData1 = new Datamodel().delete_QUARTERLYPERFORMANCEData("1", year, blk_cat);
                      int DeleteData2 = new Datamodel().delete_QUARTERLYPERFORMANCEData("2", year, blk_cat);
                      int DeleteData3 = new Datamodel().delete_QUARTERLYPERFORMANCEData("3", year, blk_cat);
                      int DeleteData4 = new Datamodel().delete_QUARTERLYPERFORMANCEData("4", year, blk_cat);
                      int DeleteData5 = new Datamodel().delete_QUARTERLYPERFORMANCEData("5", year, blk_cat);
                      try
                      {
                        System.IO.File.Delete(delfilepath);
                      }
                      catch (Exception ex)
                      {
                        Message = "File is used by another Process.Please try again";
                        goto label;
                      }
                    }
                  }
                }
                string subpath = Path.Combine("\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt, final_file);
                if (file.Length > 0)
                {
                  final_file = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                  string fullPath = Path.Combine(AppFilePath, final_file);
                  try
                  {
                    // System.IO.File.Delete(fullPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                      file.CopyTo(stream);
                    }                   
                  }
                  catch (Exception e)
                  {
                    Message = "File is used by another Process.Please try again";
                    goto label;
                  }
                }

                OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Persist Security Info=False");
                my_con.Open();
                try
                {
                  OleDbCommand o_cmd = new OleDbCommand("select*from [Sheet1$]", my_con);
                  OleDbDataReader o_dr = o_cmd.ExecuteReader();
                  DataTable dt = new DataTable();
                  dt.Load(o_dr);
                  int count = dt.Rows.Count;
                  if (count < 2)
                    Message = "Please specify correct Block Name";
                  int result1 = 0;

                  string opnameexcel = dt.Rows[1][3].ToString();
                  opnameexcel = opnameexcel.Replace("#", ".");
                  string excelfiledate = dt.Rows[1][8].ToString();
                  string excelcode = dt.Rows[2][0].ToString();

                  string blkdesc = "";
                  string blknom = "";

                  string uploadfiledate = qt + "_(" + year + ")";

                  if (block_category == 1)
                  {
                    blkdesc = "NOMINATION";
                    blknom = "NOM";
                  }
                  else if (block_category == 2)
                  {
                    blkdesc = "ALL";
                    blknom = "ALL";
                  }
                  //check validation
                  if (excelcode == "PLANBECODE311AM1")
                  {
                  }
                  else
                  {
                    Message = "This is not a uploaded template.Please download correct template";
                    goto label;
                  }
                  if (opnameexcel == OPERATOR_NAMEexcel)
                  {
                  }
                  else
                  {
                    Message = "Please specify correct Operator Name";
                    goto label;
                  }

                  if (excelfiledate == uploadfiledate)
                  {
                  }
                  else
                  {
                    Message = "Quarter and year in Excel File should be same as Selected Quarter and year";
                    goto label;
                  }

                  if (dt.Rows[1][11].ToString() == blkdesc || dt.Rows[1][11].ToString() == blknom)
                  {
                  }
                  else
                  {
                    Message = "Please specify correct Block Type";
                    goto label;
                  }
                  Datamodel rgm = new Datamodel();
                  // slno = 0;
                  for (int i = 7; i <= 11; i++)
                  {
                    for (int j = 0; j <= 6; j++)
                    {
                      if (j == 0)
                        wellcategoryid = 1;
                      if (j == 1)
                        wellcategoryid = 2;
                      if (j == 2)
                        wellcategoryid = 1;
                      if (j == 3)
                        wellcategoryid = 2;
                      if (j == 4)
                        wellcategoryid = 1;
                      if (j == 5)
                        wellcategoryid = 2;
                      if (j == 6)
                        wellcategoryid = 0;


                      if (j <= 1)
                        rigoperatorstatus = 1;
                      if (j > 1 && j <= 3)
                        rigoperatorstatus = 2;
                      if (j > 3)
                        rigoperatorstatus = 0;

                      slno++;
                      operatorid = OPERATOR_ID.ToString();
                      year = yer;
                      quarter = 1;
                      operationareaid = i - 6;
                      // string test=((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString();
                      nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());

                      meterage = Convert.ToInt32(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                      rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                      cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                      commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();
                      allwells.Add(nowells);
                      int no_well = allwells.All(c => c == 0) ? 0 : 1;
                      allmeterage.Add(meterage);
                      int all_met = allmeterage.All(c => c == 0) ? 0 : 1;
                      allcyspeed.Add(cyclespeed);
                      string cy_speed = allcyspeed.All(c => c == "0") ? "0" : "1";
                      allcommspeed.Add(commspeed);
                      string comm_speed = allcommspeed.All(c => c == "0") ? "0" : "1";
                      allmonth.Add(rigmonth);
                      string rig_month = allmonth.All(c => c == "0") ? "0" : "1";
                      result1 = rgm.UploadExcelAnnualDrilling(slno, operatorid, quarter, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                      if (no_well == 0 && all_met == 0 && rig_month == "0" && cy_speed == "0" && comm_speed == "0")
                      {
                        FQ = 0;
                      }
                      else
                      {
                        FQ = 1;
                      }
                    }
                  }
                  for (int i = 18; i <= 22; i++)
                  {
                    for (int j = 0; j <= 6; j++)
                    {
                      if (j == 0)
                        wellcategoryid = 1;
                      if (j == 1)
                        wellcategoryid = 2;
                      if (j == 2)
                        wellcategoryid = 1;
                      if (j == 3)
                        wellcategoryid = 2;
                      if (j == 4)
                        wellcategoryid = 1;
                      if (j == 5)
                        wellcategoryid = 2;
                      if (j == 6)
                        wellcategoryid = 0;

                      if (j <= 1)
                        rigoperatorstatus = 1;
                      if (j > 1 && j <= 3)
                        rigoperatorstatus = 2;
                      if (j > 3)
                        rigoperatorstatus = 0;

                      slno++;
                      operatorid = OPERATOR_ID.ToString();
                      year = yer;
                      quarter = 2;
                      operationareaid = i - 17;

                      nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());
                      meterage = Convert.ToInt32(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                      rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                      cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                      commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();
                      allwells.Add(nowells);
                      int no_well = allwells.All(c => c == 0) ? 0 : 1;
                      allmeterage.Add(meterage);
                      int all_met = allmeterage.All(c => c == 0) ? 0 : 1;
                      allcyspeed.Add(cyclespeed);
                      string cy_speed = allcyspeed.All(c => c == "0") ? "0" : "1";
                      allcommspeed.Add(commspeed);
                      string comm_speed = allcommspeed.All(c => c == "0") ? "0" : "1";
                      allmonth.Add(rigmonth);
                      string rig_month = allmonth.All(c => c == "0") ? "0" : "1";
                       result1 = rgm.UploadExcelAnnualDrilling(slno, operatorid, quarter, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                      if (no_well == 0 && all_met == 0 && rig_month == "0" && cy_speed == "0" && comm_speed == "0")
                      { SQ = 0; }
                      else { SQ = 1; }
                    }
                  }
                  for (int i = 29; i <= 33; i++)
                  {
                    for (int j = 0; j <= 6; j++)
                    {
                      if (j == 0)
                        wellcategoryid = 1;
                      if (j == 1)
                        wellcategoryid = 2;
                      if (j == 2)
                        wellcategoryid = 1;
                      if (j == 3)
                        wellcategoryid = 2;
                      if (j == 4)
                        wellcategoryid = 1;
                      if (j == 5)
                        wellcategoryid = 2;
                      if (j == 6)
                        wellcategoryid = 0;

                      if (j <= 1)
                        rigoperatorstatus = 1;
                      if (j > 1 && j <= 3)
                        rigoperatorstatus = 2;
                      if (j > 3)
                        rigoperatorstatus = 0;

                      slno++;
                      operatorid = OPERATOR_ID.ToString();
                      year = yer;
                      quarter = 3;
                      operationareaid = i - 28;

                      nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());
                      meterage = Convert.ToInt32(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                      rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                      cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                      commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();
                      allwells.Add(nowells);
                      int no_well = allwells.All(c => c == 0) ? 0 : 1;
                      allmeterage.Add(meterage);
                      int all_met = allmeterage.All(c => c == 0) ? 0 : 1;
                      allcyspeed.Add(cyclespeed);
                      string cy_speed = allcyspeed.All(c => c == "0") ? "0" : "1";
                      allcommspeed.Add(commspeed);
                      string comm_speed = allcommspeed.All(c => c == "0") ? "0" : "1";
                      allmonth.Add(rigmonth);
                      string rig_month = allmonth.All(c => c == "0") ? "0" : "1";
                       result1 = rgm.UploadExcelAnnualDrilling(slno, operatorid, quarter, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                      if (no_well == 0 && all_met == 0 && rig_month == "0" && cy_speed == "0" && comm_speed == "0")
                      { TQ = 0; }
                      else { TQ = 1; }
                    }
                  }
                  for (int i = 40; i <= 44; i++)
                  {
                    for (int j = 0; j <= 6; j++)
                    {
                      if (j == 0)
                        wellcategoryid = 1;
                      if (j == 1)
                        wellcategoryid = 2;
                      if (j == 2)
                        wellcategoryid = 1;
                      if (j == 3)
                        wellcategoryid = 2;
                      if (j == 4)
                        wellcategoryid = 1;
                      if (j == 5)
                        wellcategoryid = 2;
                      if (j == 6)
                        wellcategoryid = 0;

                      if (j <= 1)
                        rigoperatorstatus = 1;
                      if (j > 1 && j <= 3)
                        rigoperatorstatus = 2;
                      if (j > 3)
                        rigoperatorstatus = 0;

                      slno++;
                      operatorid = OPERATOR_ID.ToString();
                      year = yer;
                      quarter = 4;
                      operationareaid = i - 39;

                      nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());
                      meterage = Convert.ToInt32(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                      rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                      cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                      commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();
                      allwells.Add(nowells);
                      int no_well = allwells.All(c => c == 0) ? 0 : 1;
                      allmeterage.Add(meterage);
                      int all_met = allmeterage.All(c => c == 0) ? 0 : 1;
                      allcyspeed.Add(cyclespeed);
                      string cy_speed = allcyspeed.All(c => c == "0") ? "0" : "1";
                      allcommspeed.Add(commspeed);
                      string comm_speed = allcommspeed.All(c => c == "0") ? "0" : "1";
                      allmonth.Add(rigmonth);
                      string rig_month = allmonth.All(c => c == "0") ? "0" : "1";
                      result1 = rgm.UploadExcelAnnualDrilling(slno, operatorid, quarter, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                      if (no_well == 0 && all_met == 0 && rig_month == "0" && cy_speed == "0" && comm_speed == "0")
                      { LQ = 0; }
                      else { LQ = 1; }
                    }
                  }
                  if (FQ == 0 && SQ == 0 && TQ == 0 && LQ == 0)
                  {
                    Message = "Can't Upload Enter Atleast One Quarter Details";

                  }
                  else
                  {
                    for (int i = 51; i <= 55; i++)
                    {
                      for (int j = 0; j <= 6; j++)
                      {
                        if (j == 0)
                          wellcategoryid = 1;
                        if (j == 1)
                          wellcategoryid = 2;
                        if (j == 2)
                          wellcategoryid = 1;
                        if (j == 3)
                          wellcategoryid = 2;
                        if (j == 4)
                          wellcategoryid = 1;
                        if (j == 5)
                          wellcategoryid = 2;
                        if (j == 6)
                          wellcategoryid = 0;

                        if (j <= 1)
                          rigoperatorstatus = 1;
                        if (j > 1 && j <= 3)
                          rigoperatorstatus = 2;
                        if (j > 3)
                          rigoperatorstatus = 0;

                        slno++;
                        operatorid = OPERATOR_ID.ToString();
                        year = yer;
                        quarter = 5;
                        operationareaid = i - 50;

                        nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());
                        meterage = Convert.ToInt32(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                        rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                        cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                        commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();
                        result1 = rgm.UploadExcelAnnualDrilling(slno, operatorid, quarter, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                      }
                    }
                  }
                  planperformancedataexist = new Datamodel().get_QuaterlyPerformanceDataExist2(bere, year, blk_cat);
                  if (planperformancedataexist == "")
                  {
                    string qtrdata1 = "";
                    string qtrdata2 = "";
                    string qtrdata3 = "";
                    string qtrdata4 = "";

                    //Quater1
                    qtrdata1 = new Datamodel().get_QuaterlyPerformanceDataExist1("1", year, blk_cat);
                    if (planperformancedataexist == "")
                    {
                    }
                    else
                    {
                      int delqtr1 = new Datamodel().delete_QUARTERLYPERFORMANCEData("1", year, blk_cat);
                    }
                    //Quater2
                    qtrdata2 = new Datamodel().get_QuaterlyPerformanceDataExist1("2", year, blk_cat);
                    if (planperformancedataexist == "")
                    {
                    }
                    else
                    {
                      int delqtr2 = new Datamodel().delete_QUARTERLYPERFORMANCEData("2", year, blk_cat);
                    }
                    //Quater3
                    qtrdata3 = new Datamodel().get_QuaterlyPerformanceDataExist1("3", year, blk_cat);
                    if (planperformancedataexist == "")
                    {
                    }
                    else
                    {
                      int delqtr3 = new Datamodel().delete_QUARTERLYPERFORMANCEData("3", year, blk_cat);
                    }
                    //Quater4
                    qtrdata4 = new Datamodel().get_QuaterlyPerformanceDataExist1("4", year, blk_cat);
                    if (planperformancedataexist == "")
                    {
                    }
                    else
                    {
                      int delqtr4 = new Datamodel().delete_QUARTERLYPERFORMANCEData("4", year, blk_cat);
                    }
                    Message = "There is problem in File uploading!! Please Enter atleast one quater details!!";
                  }
                  else
                  {
                    int result2 = 0;
                    result2 = rgm.UploadFileData(final_file, filedate, 11, 3, subpath, block_category, qt, USERID);

                    adminrigpath = _hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt;
                    if (exist_file_admin == 0)
                    {
                      filenameadmin = final_file;
                    }
                    else
                    {
                      filenameadmin = file_name + "_Updated" + exist_file_admin + ".xlsx";
                    }
                    string pathadmin = Path.Combine(adminrigpath, filenameadmin);
                    //file.SaveAs(pathadmin);
                    //file.InputStream.Dispose();
                    //GC.Collect();
                    string fullPath = Path.Combine(adminrigpath, final_file);
                    //try
                    //{
                    //  System.IO.File.Delete(fullPath);
                    //  using (var stream = new FileStream(pathadmin, FileMode.Create))
                    //  {
                    //    file.CopyTo(stream);
                    //  }
                    //}
                    //catch (Exception e)
                    //{
                    //  Message = "File is used by another Process.Please try again";
                    //}
                    //string subpathadmin = Path.Combine("\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt, filenameadmin);
                    //result2 = rgm.UploadFileData(filenameadmin, filedate, 11, 3, subpathadmin, block_category, qt, "Admin");
                    Message = "File uploaded successfully";
                  }
                }
                catch (Exception ex2)
                {
                  Message = "ERROR:" + ex2.Message.ToString();
                }
                my_con.Close();
              }
              else
              {
                Message = "You have already uploaded a file.";
              }
            }
            catch (Exception ex1)
            {

              Message = "ERROR:" + ex1.Message.ToString();
            }
            //finally
            //{
            //  file.InputStream.Dispose();
            //}
          }
          else
          {
            Message = "You have not specified a file.";
          }
          //Message = "Successfully uploaded.";
        }
        else
        {
          Message = "BE is uploaded only in April";
        }
      }
      if (qt == "RE")
      {
        if (curmonth >= 1)
        {
          if (file != null && file.Length > 0)
          {
            string path = "";
            planperformancedataexist = new Datamodel().get_QuaterlyPerformanceDataExist1(bere, year, blk_cat);
            List<string> filenames = new List<string>();
            int exist_file = 0;
            try
            {
              if (planperformancedataexist == "")
              {
                string file_name_ext = Path.GetFileName(file.FileName);
                string file_name_get = Path.GetFileNameWithoutExtension(file.FileName);
                filenames = getQPerformanceFileExistUser(qt, year, blk_cat);
                exist_file = filenames.Count;
                int exist_file_admin = getQPerformanceFileExistAdmin(qt, year, blk_cat);

                string file_name_new = file_name_get.Replace(" ", "");
                file_name = file_name_new.Replace("\'", "");

                string final_file = file_name + ".xlsx";

                string AppFilePath = "";
                string adminrigpath = "";
                string filenameadmin = "";
                string delfilepath = "";
                string adminapprootpath = _hostingEnvironment.WebRootPath;

                AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt;
                path = Path.Combine(AppFilePath, final_file);
                if (exist_file == 0)
                {
                }
                else
                {
                  for (int i = 0; i < exist_file; i++)
                  {
                    delfilepath = Path.Combine(AppFilePath, filenames[i]);
                    if (System.IO.File.Exists(delfilepath))
                    {
                      int DeleteData= new Datamodel().delete_QUARTERLYPERFORMANCEData(bere, year, blk_cat);
                      try
                      {
                        System.IO.File.Delete(delfilepath);
                      }
                      catch (Exception ex)
                      {
                        Message = "File is used by another Process.Please try again";
                        goto label;
                      }
                    }
                  }
                }
                string subpath = Path.Combine("PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt, final_file);
                if (file.Length > 0)
                {
                  final_file = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                  string fullPath = Path.Combine(AppFilePath, final_file);
                  try
                  {
                   // System.IO.File.Delete(fullPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                      file.CopyTo(stream);
                    }
                  }
                  catch (Exception e)
                  {
                    Message = "File is used by another Process.Please try again";
                    goto label;
                  }
                }
                OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Persist Security Info=False");
                my_con.Open();
                try
                {
                  OleDbCommand o_cmd = new OleDbCommand("select*from [Sheet1$]", my_con);
                  OleDbDataReader o_dr = o_cmd.ExecuteReader();
                  DataTable dt = new DataTable();
                  dt.Load(o_dr);
                  int count = dt.Rows.Count;
                  if (count < 2)
                    Message = "Please specify correct Block Name";
                  int result1 = 0;
                  string opnameexcel = dt.Rows[1][3].ToString();
                  opnameexcel = opnameexcel.Replace("#", ".");
                  string excelfiledate = dt.Rows[1][8].ToString();
                  string excelcode = dt.Rows[2][0].ToString();

                  string blkdesc = "";
                  string blknom = "";

                  string uploadfiledate = qt + "_(" + year + ")";

                  if (block_category == 1)
                  {
                    blkdesc = "NOMINATION";
                    blknom = "NOM";
                  }
                  else if (block_category == 2)
                  {
                    blkdesc = "ALL";
                    blknom = "ALL";
                  }
                  //check validation
                  if (excelcode == "PLANRECODE311AM1")
                  {
                  }
                  else
                  {
                    Message = "This is not a uploaded template.Please download correct template";
                    goto label;
                  }
                  if (opnameexcel == OPERATOR_NAMEexcel)
                  {
                  }
                  else
                  {
                    Message = "Please specify correct Operator Name";
                    goto label;
                  }

                  if (excelfiledate == uploadfiledate)
                  {
                  }
                  else
                  {
                    Message = "Quarter and year in Excel File should be same as Selected Quarter and year";
                    goto label;
                  }

                  if (dt.Rows[1][11].ToString() == blkdesc || dt.Rows[1][11].ToString() == blknom)
                  {
                  }
                  else
                  {
                    Message = "Please specify correct Block Type";
                    goto label;
                  }
                  Datamodel rgm = new Datamodel();
                  // slno = 0;
                  for (int i = 7; i <= 11; i++)
                  {
                    for (int j = 0; j <= 6; j++)
                    {
                      if (j == 0)
                        wellcategoryid = 1;
                      if (j == 1)
                        wellcategoryid = 2;
                      if (j == 2)
                        wellcategoryid = 1;
                      if (j == 3)
                        wellcategoryid = 2;
                      if (j == 4)
                        wellcategoryid = 1;
                      if (j == 5)
                        wellcategoryid = 2;
                      if (j == 6)
                        wellcategoryid = 0;

                      if (j <= 1)
                        rigoperatorstatus = 1;
                      if (j > 1 && j <= 3)
                        rigoperatorstatus = 2;
                      if (j > 3)
                        rigoperatorstatus = 0;

                      slno++;
                      operatorid = OPERATOR_ID.ToString();
                      quarter = 6;
                      year = yer;
                      operationareaid = i - 6;

                      nowells = Convert.ToInt32(((dt.Rows[i][(j * 5) + 1]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 1]).ToString());
                      meterage = Convert.ToInt32(((dt.Rows[i][(j * 5) + 2]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 2]).ToString());
                      rigmonth = ((dt.Rows[i][(j * 5) + 3]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 3]).ToString();
                      cyclespeed = ((dt.Rows[i][(j * 5) + 4]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 4]).ToString();
                      commspeed = ((dt.Rows[i][(j * 5) + 5]).ToString().Length == 0 ? 0 : dt.Rows[i][(j * 5) + 5]).ToString();

                      result1 = rgm.UploadExcelAnnualDrilling(slno, operatorid, quarter, year, rigoperatorstatus, wellcategoryid, operationareaid, nowells, meterage, cyclespeed, commspeed, rigmonth, block_category,Status);
                    }
                  }
                  planperformancedataexist = new Datamodel().get_QuaterlyPerformanceDataExist2(bere, year, blk_cat);
                  if (planperformancedataexist == "")
                  {
                    Message = "There is problem in File uploading!!";
                  }
                  else
                  {
                    int result2 = 0;
                    result2 = rgm.UploadFileData(final_file, filedate, 11, 3, subpath, block_category, qt, USERID);
                    adminrigpath = _hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt;
                    if (exist_file_admin == 0)
                    {
                      filenameadmin = final_file;
                    }
                    else
                    {
                      filenameadmin = file_name + "_Updated" + exist_file_admin + ".xlsx";
                    }
                    
                    string subpathadmin = Path.Combine("PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blk_cat + "\\" + qt, filenameadmin);
                  //  result2 = rgm.UploadFileData(filenameadmin, filedate, 11, 3, subpathadmin, block_category, qt, "Admin");
                    Message = "File uploaded successfully";
                  }
                  my_con.Close();
                }
                catch (Exception ex2)
                {
                  Message = "ERROR:" + ex2.Message.ToString();
                }
              }
              else
              {
                Message = "You have already uploaded a file.";
              }
            }
            catch (Exception ex1)
            {
                Message = "ERROR:" + ex1.Message.ToString();
            }
            //finally
            //{
            //  file.InputStream.Dispose();
            //}
          }
          else
          {
            Message = "You have not specified a file.";
          }
          //Message = "Successfully uploaded.";
        }
        else
        {
          Message = "Can not upload in this month";
        }
      }
    label:
   return   Message;


    }
    public int getQPerformanceFileExistAdmin(string quarter, string year, string blktype)
    {
      List<string> filenames = new List<string>();
      int total = 0;
      string adminapprootpath = _hostingEnvironment.WebRootPath;
      string AppFilePath = "";
      AppFilePath = adminapprootpath + "PlanPerformanceExcelUpload\\" +USERID + "\\" + year + "\\" + blktype + "\\" + quarter;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID);
      if (!exists)System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year);
      if (!exists1) System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year);

      bool exists2 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blktype);
      if (!exists2)System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blktype);

      bool exists3 = System.IO.Directory.Exists(AppFilePath);
      if (!exists3)System.IO.Directory.CreateDirectory(AppFilePath);
      filenames = Directory.GetFiles(AppFilePath, "*.xlsx").Select(path => Path.GetFileName(path)).ToList<string>();
      total = filenames.Count;


      return total;
    }

    public List<string> getQPerformanceFileExistUser(string quarter, string year, string blktype)
    {
      List<string> filenames = new List<string>();
      string AppFilePath = "";

      AppFilePath = _hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blktype + "\\" + quarter;

      bool exists = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID);
      if (!exists) System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID);

      bool exists1 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year);
      if (!exists1) System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year);

      bool exists2 = System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "\\UploadUpload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blktype);
      if (!exists2) System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Upload\\PlanPerformanceExcelUpload\\" + USERID + "\\" + year + "\\" + blktype);

      bool exists3 = System.IO.Directory.Exists(AppFilePath);
      if (!exists3) System.IO.Directory.CreateDirectory(AppFilePath);

      filenames = Directory.GetFiles(AppFilePath, "*.xlsx").Select(path => Path.GetFileName(path)).ToList<string>();
      return filenames;
    }
  }
}


