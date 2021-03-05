import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DPR } from '../../../DrillingTable/drillingclass';
import { DrillingService } from '../../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import * as XLSX from 'xlsx';
type AOA = any[][];
@Component({
  selector: 'App-Drilling-DPRExcel',
  templateUrl: './dprexcel.component.html',
  styleUrls: ['./dprexcel.component.css'],
})
export class DPRExcelComponent {
  data: AOA = [];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'SheetJS.xlsx';

    divForm: boolean = true;
  divExcel: boolean = false;
  Status: string;
  DPRTemplateExcel: FormGroup;
  submitted = false;
  public progress: number;
  @ViewChild('fileInput', { static: false }) fileInput;

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {
  }
    ngOnInit() {
 this.DPRTemplateExcel = this.formbuilder.group({
   DPR_DATE: ['', Validators.required],
    })
    }

  get GetRequired()
  { return this.DPRTemplateExcel.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.DPRTemplateExcel.invalid) {
      this.divForm = true;
      this.divExcel = false;
      return;
    }
    else {
      this.divForm = true;
      this.divExcel = true;
    }
  }


      CreateDPRExcel() {
    this.submitted = true;
    if (this.DPRTemplateExcel.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Upload Excel File ?")) {
        this.drillingservice.CreateDPRExcel(this.DPRTemplateExcel.value).subscribe(
          data => {
            //this.Status, alert("Success");            
            // this.drillingservice.downloadFile().subscribe(data => { alert('Success') });
            this.download(); alert(data)
          })
      }
    }
  }


  download()
  {
    this.drillingservice.DownloadDPRFile().subscribe(
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
            // a.download = this.fileName;
            a.download = "Daily-DPR";
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


  UploadExceldprDraft() {
    let formData = new FormData();
    const file = this.fileInput.nativeElement.files[0];
  const data = this.DPRTemplateExcel.value;
    console.log(file);
  console.log(data);
    formData.append('upload', file, file.name)
    formData.append('DPR_DATE', data.DPR_DATE)
    formData.append('Status', "1")
    this.drillingservice.UploadExceldprExcel(formData).subscribe(
      data => {
        //if (result.type === HttpEventType.UploadProgress)
        //  this.progress = Math.round(100 * result.loaded / result.total),
        //    this.Status = result.toString();
        alert(data); this.divExcel = false; this.DPRTemplateExcel.reset(); this.data = [];
      });
  }

  UploadExceldprSubmit() {
    let formData = new FormData();
    const file = this.fileInput.nativeElement.files[0];
    const data = this.DPRTemplateExcel.value;
    console.log(file);
    console.log(data);
    formData.append('upload', file, file.name)
    formData.append('DPR_DATE', data.DPR_DATE)
    formData.append('Status', "2")
    this.drillingservice.UploadExceldprExcel(formData).subscribe(
      data => {
        //if (result.type === HttpEventType.UploadProgress)
        //  this.progress = Math.round(100 * result.loaded / result.total),
        //    this.Status = result.toString();
        alert(data); this.divExcel = false; this.DPRTemplateExcel.reset(); this.data = [];
      });
  }


}


