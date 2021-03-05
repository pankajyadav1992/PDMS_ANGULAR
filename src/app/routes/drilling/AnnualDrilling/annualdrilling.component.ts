import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {  YearMaster, ddlList, OperatingStatus, OperationalArea, WellCatgory, BlockType, AnnualDrillingPlan } from '../../DrillingTable/drillingclass';
import { DrillingService } from '../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'App-Drilling-AnnualDrilling',
  templateUrl: './annualdrilling.component.html',
  styleUrls: ['./annualdrilling.component.css'],
})
export class AnnualDrillingComponent {
  btnSubmitvisibility: boolean = true;
  btnUpdatevisibility: boolean = false;
//  Months: MonthMaster[];
  Years: YearMaster[];
  WellCategory: WellCatgory[];
  BlockTypes: BlockType[];
  OperatingStatus: OperatingStatus[];
  OperationalArea: OperationalArea[];
  ADP: AnnualDrillingPlan[];
 // Quarter: Quartertype[];
  ddlMaster: ddlList[];
  Status: string;
  ADPForm: FormGroup;
  ADPSearch: FormGroup;
  submitted = false;
  submittedsearch = false;
  dataSource;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  displayedColumns: string[] = ['BlockTypes', 'Years', 'QuarterType', 'OperatingStatus', 'WellCategory', 'OperationalArea', 'Status', 'Edit/View'];

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {

  }



  ngOnInit() {
    this.GetDdl();
    this.ADPForm = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      QUARTER_NO: ['', Validators.required],
      RIG_OPERATE_STATUS: ['', Validators.required],
      WELL_CATEGORY_ID: ['', Validators.required],
      OPERATION_AREA_ID: ['', Validators.required],
      NO_WELLS: [''], METARAGE: [''], CYCLE_SPEED: [''],
      COMMERCIAL_SPEED: [''], RIG_MONTHS: ['']
    })

    this.ADPSearch = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      QUARTER_NO: ['', Validators.required],
    })
    this.ADPForm.controls["CYCLE_SPEED"].disable();
  }

  get GetRequired() { return this.ADPForm.controls; }
  get GetRequiredSearch() { return this.ADPSearch.controls; }

  //GetMonthChange(value: string) {
  //  console.log(value);
  //}

  GetDdl() {
    this.drillingservice.GetRigCountAllList().subscribe(data => {
      this.ddlMaster = data;
     // this.Months = this.ddlMaster[0].Months;
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
    var mv = this.ADPForm.get("METARAGE").value;
    var rmv = this.ADPForm.get("RIG_MONTHS").value;
    var cs = mv / rmv;
    this.ADPForm.controls["CYCLE_SPEED"].setValue(cs);
  }

  onChangeRIG_MONTHS(event: any) {
  //  alert(event.target.value) 
    this.SumMETARAGE()
  };

  onSubmit() {
    this.submitted = true;
    if (this.ADPForm.invalid) {
      return;
    }
    else {
      var hh1 = this.ADPForm.get("CYCLE_SPEED").value;


      if (confirm("Are you sure you want to Submit this Form ?")) {
       
      
        this.drillingservice.SubmitADPDetails(this.ADPForm.value).subscribe(
          data => { this.Status, alert("Success"); this.ADPForm.reset(); this.router.navigate(['/drilling/AnnualDrilling']) })
      }
    }
  }


  onDraft() {
    this.submitted = true;
    if (this.ADPForm.invalid) {
      return;
    }
    else {
      var hh2 = this.ADPForm.get("CYCLE_SPEED").value;
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftADPDetails(this.ADPForm.value).subscribe(
          data => { this.Status, alert("Success"); this.ADPForm.reset(); this.router.navigate(['/drilling/AnnualDrilling']) })
      }
    }
  }


  onDraftUpdate() {
    this.submitted = true;

    if (this.ADPForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftUpdateADPDetails(this.ADPForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.ADP); this.ADPForm.reset();
            this.router.navigate(['/drilling/AnnualDrilling']);
          },
            error => () => {

            },
            () => console.log(this.ADP)
          );
      }
    }
  }


  onDraftSubmit() {
    this.submitted = true;

    if (this.ADPForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.DraftSubmitADPDetails(this.ADPForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.ADP); this.ADPForm.reset();
            this.router.navigate(['/drilling/AnnualDrilling']);
          },
            error => () => {

            },
            () => console.log(this.ADP)
          );
      }
    }
  }


  EditAnnualDetails(cdp: AnnualDrillingPlan): void {

    let Status = cdp.STATUS;
    if (Status == 'Submit') {
      localStorage.removeItem('ADPId');
      alert("Data Submited Not Editable");
    }
    else {
      this.btnUpdatevisibility = true;
      localStorage.removeItem('ADPId');
      localStorage.setItem('ADPId', cdp.SL_NO.toString());

      let empid = localStorage.getItem('ADPId');
      if (+empid > 0) {
        this.btnSubmitvisibility = false;
        this.drillingservice.GetADPDetailsId(+empid).subscribe(data => {
          this.ADP = data,
            console.log(this.ADP),
            this.ADPForm.controls['BLOCK_CATEGORY'].setValue(this.ADP[0].BLOCK_CATEGORY);
          this.ADPForm.controls['YEAR'].setValue(this.ADP[0].YEAR);
          this.ADPForm.controls['QUARTER_NO'].setValue(this.ADP[0].QUARTER_NO);
          this.ADPForm.controls['RIG_OPERATE_STATUS'].setValue(this.ADP[0].RIG_OPERATE_STATUS);
          this.ADPForm.controls['WELL_CATEGORY_ID'].setValue(this.ADP[0].WELL_CATEGORY_ID);
          this.ADPForm.controls['OPERATION_AREA_ID'].setValue(this.ADP[0].OPERATION_AREA_ID);
          this.ADPForm.controls['METARAGE'].setValue(this.ADP[0].METARAGE);
          this.ADPForm.controls['NO_WELLS'].setValue(this.ADP[0].NO_WELLS);
          this.ADPForm.controls['CYCLE_SPEED'].setValue(this.ADP[0].CYCLE_SPEED);
          this.ADPForm.controls['COMMERCIAL_SPEED'].setValue(this.ADP[0].COMMERCIAL_SPEED);
          this.ADPForm.controls['RIG_MONTHS'].setValue(this.ADP[0].RIG_MONTHS);
        })
      }

    }
  }




  onSearch() {
    this.submittedsearch = true;
    if (this.ADPSearch.invalid) {
      return;
    }
    else {
      this.drillingservice.SearchADP(this.ADPSearch.value).subscribe(
        data => {
          this.ADP = data, console.log(this.ADP),
            this.dataSource = new MatTableDataSource<AnnualDrillingPlan>(this.ADP),
            this.dataSource.paginator = this.paginator
        }
      )

    }
  }
 
}
