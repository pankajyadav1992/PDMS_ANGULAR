import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { ResponseContentType } from '@angular/http';
import { map } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MonthMaster, ddlList, RigCountDetails, RigWisePerformance, WellBlock, WellField, WellWisePerformance, ddlListDPR, DPR, CumulativeDrillingPerformance, AnnualDrillingPlan } from '../DrillingTable/DrillingClass'
import { ActivatedRoute } from '@angular/router';
import { RigWiseComponent } from '../drilling/rigwiseperformance/rigwiseperformance.component';
//import * as  saveAs from 'file-saver';

//import * as fileSaver from 'file-saver';
@Injectable()
export class DrillingService {
  private actionUrl: string = "https://localhost:44329/";
  constructor(private httpclient: HttpClient, private route: ActivatedRoute) { }

  public GetRigCountAllList(): Observable<ddlList[]> {

    return this.httpclient.post<ddlList[]>(this.actionUrl + "api/Drilling/GetAllddlList", null)

  }


  public GetBlockField(str: number): Observable<WellField[]> {

    return this.httpclient.get<WellField[]>(this.actionUrl + "api/Drilling/GetFieldName/" + str)

  }

  public SubmitRigCountDetails(rigdetails: RigCountDetails): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/SubmitRigCountDetails", rigdetails)
  }

  public DraftRigCountDetails(rigdetails: RigCountDetails): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftRigCountDetails", rigdetails)
  }


  public DraftSubmitRigCountDetails(rigdetails: RigCountDetails): Observable<string> {

    let Id = localStorage.getItem('EditRigCountId');
    rigdetails.Id = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftSubmitRigCountDetails", rigdetails)
  }

  public DraftUpdateRigCountDetails(rigdetails: RigCountDetails): Observable<string> {
    let Id = localStorage.getItem('EditRigCountId');
    rigdetails.Id = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftUpdateRigCountDetails", rigdetails)
  }

  public SearchRigCount(rigdetails: RigCountDetails): Observable<RigCountDetails[]> {
    return this.httpclient.post<RigCountDetails[]>(this.actionUrl + "api/Drilling/SearchRigCount", rigdetails)
  }

  GetRigCountDetailsId(Id: number): Observable<RigCountDetails[]> {
    let body = {
      'ID': Id
    }
    return this.httpclient.get<RigCountDetails[]>(this.actionUrl + "api/Drilling/GetRigCountDetailsId/" + Id);
  }


  public SubmitRigWisePerformanceDetails(rigwise: RigWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/SubmitRigWisePerformanceDetails", rigwise)
  }

  public DraftRigWisePerformanceDetails(rigwise: RigWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftRigWisePerformanceDetails", rigwise)
  }

  public SearchRigWisePerformance(rigper: RigWisePerformance): Observable<RigWisePerformance[]> {
    return this.httpclient.post<RigWisePerformance[]>(this.actionUrl + "api/Drilling/SearchRigWise", rigper)
  }

  GetRigWiseDetailsId(Id: number): Observable<RigWisePerformance[]> {
    let body = {
      'ID': Id
    }
    return this.httpclient.get<RigWisePerformance[]>(this.actionUrl + "api/Drilling/GetRigWiseDetailsId/" + Id);
  }

  public DraftUpdateRigWiseDetails(rwp: RigWisePerformance): Observable<string> {
    let Id = localStorage.getItem('EditRigWiseId');
    rwp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftUpdateRigWiseDetails", rwp)
  }

  public DraftSubmitRigWiseDetails(rwp: RigWisePerformance): Observable<string> {
    let Id = localStorage.getItem('EditRigWiseId');
    rwp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftSubmitRigWiseDetails", rwp)
  }

  public SubmitWellWiseDetails(wellwise: WellWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/SubmitWellWiseDetails", wellwise)
  }

  public DraftWellWiseDetails(wellwise: WellWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftWellWiseDetails", wellwise)
  }

  public SearchWellWise(wellwise: WellWisePerformance): Observable<WellWisePerformance[]> {
    return this.httpclient.post<WellWisePerformance[]>(this.actionUrl + "api/Drilling/SearchWellWise", wellwise)
  }

  GetWellWiseDetailsId(Id: number): Observable<WellWisePerformance[]> {
    let body = {
      'ID': Id
    }
    return this.httpclient.get<WellWisePerformance[]>(this.actionUrl + "api/Drilling/GetWellWiseDetailsId/" + Id);
  }

  public DraftUpdateWellWiseDetails(wwp: WellWisePerformance): Observable<string> {
    let Id = localStorage.getItem('EditWellWiseId');
    wwp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftUpdateWellWiseDetails", wwp)
  }

  public DraftSubmitWellWiseDetails(wwp: WellWisePerformance): Observable<string> {
    let Id = localStorage.getItem('EditWellWiseId');
    wwp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftSubmitWellWiseDetails", wwp)
  }


  public GetDPRList(): Observable<ddlListDPR[]> {
    return this.httpclient.post<ddlListDPR[]>(this.actionUrl + "api/Drilling/GetDPRList", null)
  }

  public SubmitDPRDetails(dpr: DPR): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/SubmitDPRDetails", dpr)
  }

  public DraftDPRDetails(dpr: DPR): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftDPRDetails", dpr)
  }

  public DraftSubmitDPRDetails(dpr: DPR): Observable<string> {

    let Id = localStorage.getItem('EditDPRId');
    dpr.DPR_ID = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftSubmitDPRDetails", dpr)
  }

  public DraftUpdateDPRDetails(dpr: DPR): Observable<string> {
    let Id = localStorage.getItem('EditDPRId');
    dpr.DPR_ID = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftUpdateDPRDetails", dpr)
  }

  public SearchDPR(dpr: DPR): Observable<DPR[]> {
    return this.httpclient.post<DPR[]>(this.actionUrl + "api/Drilling/SearchDPR", dpr)
  }

  GetDPRDetailsId(Id: number): Observable<DPR[]> {
    let body = {
      'ID': Id
    }
    return this.httpclient.get<DPR[]>(this.actionUrl + "api/Drilling/GetDPRDetailsId/" + Id);
  }


  public SubmitCDPDetails(cdp: CumulativeDrillingPerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/SubmitCDPDetails", cdp)
  }

  public DraftCDPDetails(cdp: CumulativeDrillingPerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftCDPDetails", cdp)
  }

  public DraftSubmitCDPDetails(cdp: CumulativeDrillingPerformance): Observable<string> {

    let Id = localStorage.getItem('EditCDPId');
    cdp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftSubmitCDPDetails", cdp)
  }

  public DraftUpdateCDPDetails(cdp: CumulativeDrillingPerformance): Observable<string> {
    let Id = localStorage.getItem('EditCDPId');
    cdp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftUpdateCDPDetails", cdp)
  }

  public SearchCDP(cdp: CumulativeDrillingPerformance): Observable<CumulativeDrillingPerformance[]> {
    return this.httpclient.post<CumulativeDrillingPerformance[]>(this.actionUrl + "api/Drilling/SearchCDP", cdp)
  }

  GetCDPDetailsId(Id: number): Observable<CumulativeDrillingPerformance[]> {
    let body = {
      'ID': Id
    }
    return this.httpclient.get<CumulativeDrillingPerformance[]>(this.actionUrl + "api/Drilling/GetCDPDetailsId/" + Id);
  }
  
  public SubmitADPDetails(adp: AnnualDrillingPlan): Observable<string> {
  
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/SubmitADPDetails", adp)
  }

  public DraftADPDetails(adp: AnnualDrillingPlan): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftADPDetails", adp)
  }

  public DraftSubmitADPDetails(adp: AnnualDrillingPlan): Observable<string> {

    let Id = localStorage.getItem('ADPId');
    adp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftSubmitADPDetails", adp)
  }

  public DraftUpdateADPDetails(adp: AnnualDrillingPlan): Observable<string> {
    let Id = localStorage.getItem('ADPId');
    adp.SL_NO = Number(Id);
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DraftUpdateADPDetails", adp)
  }

  public SearchADP(adp: AnnualDrillingPlan): Observable<AnnualDrillingPlan[]> {
    return this.httpclient.post<AnnualDrillingPlan[]>(this.actionUrl + "api/Drilling/SearchADP", adp)
  }

  GetADPDetailsId(Id: number): Observable<AnnualDrillingPlan[]> {
    let body = {
      'ID': Id
    }
    return this.httpclient.get<AnnualDrillingPlan[]>(this.actionUrl + "api/Drilling/GetADPDetailsId/" + Id);
  }


  public DownloadTempleteRigCountExcel(rigdetails: RigCountDetails): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/DownloadTempleteRigCountExcel", rigdetails)
  }

    //Rig Count Excel Code//

  public DownloadExcel_NoOfRigsUP(): Observable<any>
  {
    return this.httpclient.post<any>(this.actionUrl + "api/Drilling/DownloadExcel_NoOfRigsUP", null);
  }
  public downloadFile(): Observable<HttpEvent<Blob>> {   return this.httpclient.request(new HttpRequest(      'GET',
      `${this.actionUrl +"api/Drilling/download"}`,
      null,      {        reportProgress: true,
        responseType: 'blob'
      }));
  }
  public UploadExcelRigExcel(formData: FormData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers };
    return this.httpclient.post<string>(this.actionUrl + 'api/Drilling/UploadExcelRig', formData, httpOptions)
  }

 
  //DPR Excel code//
  public CreateDPRExcel(dpr: DPR): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/CreateDPRExcel", dpr)
  }
  public DownloadDPRFile(): Observable<HttpEvent<Blob>> {
    return this.httpclient.request(new HttpRequest('GET', `${this.actionUrl + "api/Drilling/DownloadDPRFile"}`, null, {
      reportProgress: true,
      responseType: 'blob'
    }));
  }
  public UploadExceldprExcel(formData: FormData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers };
    return this.httpclient.post<string>(this.actionUrl + 'api/Drilling/UploadExceldprExcel', formData, httpOptions)
  }





  //Rig Wise Excel//

  public CreateTempleteRigWiseExcel(rwp: RigWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/CreateTempleteRigWiseExcel", rwp)
  }
  public DownloadFileRigWise(): Observable<HttpEvent<Blob>> {
    return this.httpclient.request(new HttpRequest('GET',
      `${this.actionUrl + "api/Drilling/DownloadFileRigWise"}`,
      null, {
        reportProgress: true,
      responseType: 'blob'
    }));
  }
  public UploadExcelRigWiseExcel(formData: FormData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers };
    return this.httpclient.post<string>(this.actionUrl + 'api/Drilling/UploadExcelRigWiseExcel', formData, httpOptions)
  }
 
  

  //Well Wise Excel//

  public CreateTempleteWellWiseExcel(rwp: RigWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/CreateTempleteWellWiseExcel", rwp)
  }
  public DownloadFileWellWise(): Observable<HttpEvent<Blob>> {
    return this.httpclient.request(new HttpRequest('GET',
      `${this.actionUrl + "api/Drilling/DownloadFileWellWise"}`,
      null, {
      reportProgress: true,
      responseType: 'blob'
    }));
  }
  public UploadExcelWellWiseExcel(formData: FormData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers };
    return this.httpclient.post<string>(this.actionUrl + 'api/Drilling/UploadExcelWellWiseExcel', formData, httpOptions)
  }



  //Cumulative Drilling Excel code//

  public CreateTempleteCumulativeDrillingExcel(rwp: RigWisePerformance): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/CreateTempleteCumulativeDrillingExcel", rwp)
  }
  public DownloadFileCumulativeDrilling(): Observable<HttpEvent<Blob>> {
    return this.httpclient.request(new HttpRequest('GET',
      `${this.actionUrl + "api/Drilling/DownloadFileCumulativeDrilling"}`,
      null, {
      reportProgress: true,
      responseType: 'blob'
    }));
  }
  public UploadExcelCumulativeDrillingExcel(formData: FormData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers };
    return this.httpclient.post<string>(this.actionUrl + 'api/Drilling/UploadExcelCumulativeDrillingExcel', formData, httpOptions)
  }




  //Annual Drilling Excel code//
  public CreateTempleteAnnualDrillingExcel(adp: AnnualDrillingPlan): Observable<string> {
    return this.httpclient.post<string>(this.actionUrl + "api/Drilling/CreateTempleteAnnualDrillingExcel", adp)
  }
  public DownloadFileAnnualDrilling(): Observable<HttpEvent<Blob>> {
    return this.httpclient.request(new HttpRequest('GET',
      `${this.actionUrl + "api/Drilling/DownloadFileAnnualDrilling"}`,
      null, {
      reportProgress: true,
      responseType: 'blob'
    }));
  }
  public UploadExcelAnnualDrillingExcel(formData: FormData): Observable<any> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');
    const httpOptions = { headers: headers };
    return this.httpclient.post<string>(this.actionUrl + 'api/Drilling/UploadExcelAnnualDrillingExcel', formData, httpOptions)
  }
}

