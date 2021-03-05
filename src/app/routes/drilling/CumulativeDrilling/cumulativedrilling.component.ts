import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MonthMaster, YearMaster, ddlList, OperatingStatus, OperationalArea, WellCatgory, BlockType, CumulativeDrillingPerformance} from '../../DrillingTable/drillingclass';
import { DrillingService } from '../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'app-drilling-CumulativeDrilling',
  templateUrl: './cumulativedrilling.component.html',
  styleUrls: ['./cumulativedrilling.component.css'],
})
export class CumulativeDrillingComponent {
  btnSubmitvisibility: boolean = true;
  btnUpdatevisibility: boolean = false;
  Months: MonthMaster[];
  Years: YearMaster[];
  WellCategory: WellCatgory[];
  BlockTypes: BlockType[];
  OperatingStatus: OperatingStatus[];
  OperationalArea: OperationalArea[];
  CDP: CumulativeDrillingPerformance[];
  ddlMaster: ddlList[];
  Status: string;
  CDPForm: FormGroup;
  CDPSearch: FormGroup;
  submitted = false;
  submittedsearch = false;

  dataSource;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  displayedColumns: string[] = [ 'BlockTypes', 'Years', 'Months', 'OperatingStatus', 'WellCategory', 'OperationalArea',  'Status', 'Edit/View'];

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {

  }

  ngOnInit() {
    this.GetDdl();
    this.CDPForm = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      MONTH: ['', Validators.required],
      RIG_OPERATE_STATUS: ['', Validators.required],
      WELL_CATEGORY_ID: ['', Validators.required],
      OPERATION_AREA_ID: ['', Validators.required],
      NO_WELLS: [''], METARAGE: [''], CYCLE_SPEED: [''],
      COMMERCIAL_SPEED: [''], RIG_MONTHS: ['']
    })

    this.CDPForm.controls["CYCLE_SPEED"].disable();

    this.CDPSearch = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      MONTH: ['', Validators.required],
    })
  }
  get GetRequired() { return this.CDPForm.controls; }
  get GetRequiredSearch() { return this.CDPSearch.controls; }

  //GetMonthChange(value: string) {
  //  console.log(value);
  //}

  GetDdl() {
    this.drillingservice.GetRigCountAllList().subscribe(data => {
      this.ddlMaster = data;
      this.Months = this.ddlMaster[0].Months;
      this.Years = this.ddlMaster[0].Years;
      this.WellCategory = this.ddlMaster[0].WellCatgory;
      this.OperationalArea = this.ddlMaster[0].OperationalArea;
      this.OperatingStatus = this.ddlMaster[0].OperatingStatus;
      this.BlockTypes = this.ddlMaster[0].BlockType;
      // console.log(this.Months);
    })
   
  }


  onChangeMETARAGE(event: any) {
    //  alert(event.target.value) 
    this.SumMETARAGE()
  };

  SumMETARAGE() {
    var mv = this.CDPForm.get("METARAGE").value;
    var rmv = this.CDPForm.get("RIG_MONTHS").value;
    var cs = mv / rmv;
    this.CDPForm.controls["CYCLE_SPEED"].setValue(cs);
  }

  onChangeRIG_MONTHS(event: any) {
    //  alert(event.target.value) 
    this.SumMETARAGE()
  };

  onSubmit() {
    this.submitted = true;
    if (this.CDPForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.SubmitCDPDetails(this.CDPForm.value).subscribe(
          data => { this.Status, alert("Success"); this.CDPForm.reset(); this.router.navigate(['/drilling/CumulativeDrilling']) })
      }
    }
  }


  onDraft() {
    this.submitted = true;
    if (this.CDPForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftCDPDetails(this.CDPForm.value).subscribe(
          data => { this.Status, alert("Success"); this.CDPForm.reset(); this.router.navigate(['/drilling/CumulativeDrilling']) })
      }
    }
  }


  onDraftUpdate() {
    this.submitted = true;

    if (this.CDPForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftUpdateCDPDetails(this.CDPForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.CDP); this.CDPForm.reset();
            this.router.navigate(['/drilling/CumulativeDrilling']);
          },
            error => () => {

            },
            () => console.log(this.CDP)
          );
      }
    }
  }


  onDraftSubmit() {
    this.submitted = true;

    if (this.CDPForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.DraftSubmitCDPDetails(this.CDPForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.CDP); this.CDPForm.reset();
            this.router.navigate(['/drilling/CumulativeDrilling']);
          },
            error => () => {

            },
            () => console.log(this.CDP)
          );
      }
    }
  }


  EditRigCountDetails(cdp: CumulativeDrillingPerformance ): void {

    let Status = cdp.STATUS;
    if (Status == 'Submit') {
      localStorage.removeItem('EditCDPId');
      alert("Data Submited Not Editable");
    }
    else {
      this.btnUpdatevisibility = true;
      localStorage.removeItem('EditCDPId');
      localStorage.setItem('EditCDPId', cdp.SL_NO.toString());

      let empid = localStorage.getItem('EditCDPId');
      if (+empid > 0) {
        this.btnSubmitvisibility = false;
        this.drillingservice.GetCDPDetailsId(+empid).subscribe(data => {
          this.CDP = data,
            console.log(this.CDP),
            this.CDPForm.controls['BLOCK_CATEGORY'].setValue(this.CDP[0].BLOCK_CATEGORY);
          this.CDPForm.controls['YEAR'].setValue(this.CDP[0].YEAR);
          this.CDPForm.controls['MONTH'].setValue(this.CDP[0].MONTH);
          this.CDPForm.controls['RIG_OPERATE_STATUS'].setValue(this.CDP[0].RIG_OPERATE_STATUS);
          this.CDPForm.controls['WELL_CATEGORY_ID'].setValue(this.CDP[0].WELL_CATEGORY_ID);
          this.CDPForm.controls['OPERATION_AREA_ID'].setValue(this.CDP[0].OPERATION_AREA_ID);
          this.CDPForm.controls['METARAGE'].setValue(this.CDP[0].METARAGE);
          this.CDPForm.controls['NO_WELLS'].setValue(this.CDP[0].NO_WELLS);
          this.CDPForm.controls['CYCLE_SPEED'].setValue(this.CDP[0].CYCLE_SPEED);
          this.CDPForm.controls['COMMERCIAL_SPEED'].setValue(this.CDP[0].COMMERCIAL_SPEED);
          this.CDPForm.controls['RIG_MONTHS'].setValue(this.CDP[0].RIG_MONTHS);
        })
      }

    }
  }




  onSearch() {
    this.submittedsearch = true;
    if (this.CDPSearch.invalid) {
      return;
    }
    else {
      this.drillingservice.SearchCDP(this.CDPSearch.value).subscribe(
        data => {
          this.CDP = data, console.log(this.CDP),
            this.dataSource = new MatTableDataSource<CumulativeDrillingPerformance>(this.CDP),
            this.dataSource.paginator = this.paginator
        }
      )

    }
  }
}
