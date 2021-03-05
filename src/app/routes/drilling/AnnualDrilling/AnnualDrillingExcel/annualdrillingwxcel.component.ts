import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MonthMaster, YearMaster, ddlList, OperatingStatus, OperationalArea, WellCatgory, BlockType, AnnualDrillingPlan } from '../../../DrillingTable/drillingclass';
import { DrillingService } from '../../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import * as XLSX from 'xlsx';
type AOA = any[][];
@Component({
  selector: 'App-Drilling-AnnualDrillingExcel',
  templateUrl: './annualdrillingwxcel.component.html',
  styleUrls: ['./annualdrillingwxcel.component.css'],
})
export class AnnualDrillingExcelComponent {
  data: AOA = [];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'SheetJS.xlsx';

  divForm: boolean = true;
  divExcel: boolean = false;
  Months: MonthMaster[];
  Years: YearMaster[];
  BlockTypes: BlockType[];
  ddlMaster: ddlList[];
  Status: string;
  AnnualDrillingTemplateExcel: FormGroup;
  submitted = false;
  public progress: number;
  @ViewChild('fileInput', { static: false }) fileInput;

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {
  }

  ngOnInit() {
    this.GetDdl();
    this.AnnualDrillingTemplateExcel = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      BE_RE: ['', Validators.required],
    })

  }
  get GetRequired() { return this.AnnualDrillingTemplateExcel.controls; }



  GetDdl() {
    this.drillingservice.GetRigCountAllList().subscribe(data => {
      this.ddlMaster = data;
    //  this.Months = this.ddlMaster[0].Months;
    //  this.Years = this.ddlMaster[0].Years;
      this.BlockTypes = this.ddlMaster[0].BlockType;
      // console.log(this.BlockTypes);
    })

  }

  onSubmit() {
    this.submitted = true;
    if (this.AnnualDrillingTemplateExcel.invalid) {
      this.divForm = true;
      this.divExcel = false;
      return;
    }
    else {
      this.divForm = true;
      this.divExcel = true;
    }
  }



  CreateTempleteCumulativeDrillingExcel() {
    this.submitted = true;
    if (this.AnnualDrillingTemplateExcel.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Download Excel Templete File ?")) {
        console.log(this.AnnualDrillingTemplateExcel.value);
        this.drillingservice.CreateTempleteAnnualDrillingExcel(this.AnnualDrillingTemplateExcel.value).subscribe(
          data => {
            //this.Status, alert("Success");            
            // this.drillingservice.downloadFile().subscribe(data => { alert('Success') });
            this.download();
          })
      }
    }
  }


  onFileChange(evt: any) {
    /* wire up file reader */
    const target: DataTransfer = <DataTransfer>(evt.target);
    if (target.files.length !== 1) throw new Error('Cannot use multiple files');
    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      /* read workbook */
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

      /* grab first sheet */
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];

      /* save data */
      this.data = <AOA>(XLSX.utils.sheet_to_json(ws, { header: 1 }));
      // this.data.splice(3, 0);
      console.log(this.data);
    };
    reader.readAsBinaryString(target.files[0]);
  }

  download() {
    this.drillingservice.DownloadFileAnnualDrilling().subscribe(
      data => {
        switch (data.type) {
          case HttpEventType.DownloadProgress:
            //this.downloadStatus.emit({ status: ProgressStatusEnum.IN_PROGRESS, percentage: Math.round((data.loaded / data.total) * 100) });
            break;
          case HttpEventType.Response:
            // this.downloadStatus.emit({ status: ProgressStatusEnum.COMPLETE });
            const downloadedFile = new Blob([data.body], { type: data.body.type });
            const a = document.createElement('a');
            a.setAttribute('style', 'display:none;');
            document.body.appendChild(a);
            //  a.download = this.fileName;
            a.download = "Annual-Drilling-Plan";
            a.href = URL.createObjectURL(downloadedFile);
            a.target = '_blank';
            a.click();
            document.body.removeChild(a);
            break;
        }
      },
      error => {
        //  this.downloadStatus.emit({ status: ProgressStatusEnum.ERROR });
      }
    );
  }



  UploadExcelRigSaveDraft() {
    let formData = new FormData();
    const file = this.fileInput.nativeElement.files[0];
    const data = this.AnnualDrillingTemplateExcel.value;
    console.log(file);
    console.log(data);
    formData.append('upload', file, file.name)
    formData.append('BlockTypes', data.BLOCK_CATEGORY)
    formData.append('BE_RE', data.BE_RE)
    formData.append('Years', data.YEAR)
    formData.append('Status', "1")
    this.drillingservice.UploadExcelAnnualDrillingExcel(formData).subscribe(
      data => {
        //if (result.type === HttpEventType.UploadProgress)
        //  this.progress = Math.round(100 * result.loaded / result.total),
        //    this.Status = result.toString();
        alert(data); this.divExcel = false; this.AnnualDrillingTemplateExcel.reset(); this.data = [];
      });
  }


  UploadExcelRigFinalSubmit() {
    let formData = new FormData();
    const file = this.fileInput.nativeElement.files[0];
    const data = this.AnnualDrillingTemplateExcel.value;
    console.log(file);
    console.log(data);
    formData.append('upload', file, file.name)
    formData.append('BlockTypes', data.BLOCK_CATEGORY)
    formData.append('BE_RE', data.BE_RE)
    formData.append('Years', data.YEAR)
    formData.append('Status', "2")
    this.drillingservice.UploadExcelAnnualDrillingExcel(formData).subscribe(
      data => {
        //if (result.type === HttpEventType.UploadProgress)
        //  this.progress = Math.round(100 * result.loaded / result.total),
        //    this.Status = result.toString();
        alert(data); this.divExcel = false; this.AnnualDrillingTemplateExcel.reset(); this.data = [];
      });
  }

}




