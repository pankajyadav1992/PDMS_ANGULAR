import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MonthMaster, YearMaster, ddlList, OperatingStatus, OperationalArea, WellCatgory, BlockType, RigWisePerformance } from '../../DrillingTable/drillingclass';
import { DrillingService } from '../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'App-Drilling-RigWise',
  templateUrl: './rigwiseperformance.component.html',
  styleUrls: ['./rigwiseperformance.component.css'],
})
export class RigWiseComponent {
  btnSubmitvisibility: boolean = true;
  btnUpdatevisibility: boolean = false;
  Months: MonthMaster[];
  Years: YearMaster[];
  WellCategory: WellCatgory[];
  BlockTypes: BlockType[];
  OperatingStatus: OperatingStatus[];
  OperationalArea: OperationalArea[];
  RigWisePerformance: RigWisePerformance[];
  ddlMaster: ddlList[];
  Status: string;
  RigWisePerformanceForm: FormGroup;
  RigWisePerformanceSearch: FormGroup;
  submitted = false;
  submittedsearch = false;

  dataSource;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  displayedColumns: string[] = ['Block', 'Months', 'Years', 'OperationalArea', 'OperatingStatus', 'RigName', 'RigType',  'Status', 'Edit/View'];

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {

  }

  ngOnInit() {
    this.GetDdl();
    this.RigWisePerformanceForm = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      MONTH: ['', Validators.required],
      RIG_OPERATING_STATUS: ['', Validators.required],
      OPERATION_AREA_ID: ['', Validators.required],
      RIG_NAME: ['', Validators.required],
      RIG_TYPE: ['', Validators.required],
      //EXP_WELLS: ["0"], EXP_MET: ["0"], DEV_WELLS: ["0"], DEV_MET: ["0"], RIG_MODE_TIME_BREAKUP_RB: ["0"], RIG_MODE_TIME_BREAKUP_DR: ["0"], RIG_MODE_TIME_BREAKUP_PT: ["0"],
      //OUTCYCLE_CAPREP: ["0"], OUTCYCLE_OTH: ["0"], NONPRD_WELL_COMPLICATION_DAY: ["0"], NONPRD_WELL_COMPLICATION_PER: ["0"], NONPRD_WELL_REPAIR_DAY: ["0"], NONPRD_WELL_REPAIR_PER: ["0"], CYCLE_DAYS: ["0"], COMMERCIAL_DAYS: ["0"],
      //CYCLE_EXP: ["0"], CYCLE_DEV: ["0"], CYCLE_SPEED: ["0"], COMMERCIAL_EXP: ["0"], COMMERCIAL_DEV: ["0"], COMMERCIAL_SPEED: ["0"],
      EXP_WELLS: [''], EXP_MET: [''], DEV_WELLS: [''], DEV_MET: [''], RIG_MODE_TIME_BREAKUP_RB: [''], RIG_MODE_TIME_BREAKUP_DR: [''], RIG_MODE_TIME_BREAKUP_PT: [''],
      OUTCYCLE_CAPREP: [''], OUTCYCLE_OTH: [''], NONPRD_WELL_COMPLICATION_DAY: [''], NONPRD_WELL_COMPLICATION_PER: [''], NONPRD_WELL_REPAIR_DAY: [''], NONPRD_WELL_REPAIR_PER: [''], CYCLE_DAYS: [''], COMMERCIAL_DAYS: [''],
      CYCLE_EXP: [''], CYCLE_DEV: [''], CYCLE_SPEED: [''], COMMERCIAL_EXP: [''], COMMERCIAL_DEV: [''], COMMERCIAL_SPEED: [''],

      Remarks: [],

    })

    this.RigWisePerformanceSearch = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      YEAR: ['', Validators.required],
      MONTH: ['', Validators.required],   
    })
  }
  get GetRequired() { return this.RigWisePerformanceForm.controls; }
  get GetRequiredSearch() { return this.RigWisePerformanceSearch.controls; }

  GetDdl() {
    this.drillingservice.GetRigCountAllList().subscribe(data => {
      this.ddlMaster = data;
      this.Months = this.ddlMaster[0].Months;
      this.Years = this.ddlMaster[0].Years;
      //this.WellCategory = this.ddlMaster[0].WellCatgory;
      this.OperationalArea = this.ddlMaster[0].OperationalArea;
      this.OperatingStatus = this.ddlMaster[0].OperatingStatus;
      this.BlockTypes = this.ddlMaster[0].BlockType;
      // console.log(this.Months);
    })

  }


  onSubmit() {
    this.submitted = true;
    if (this.RigWisePerformanceForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.SubmitRigWisePerformanceDetails(this.RigWisePerformanceForm.value).subscribe(
          data => { this.Status, alert("Success"); this.RigWisePerformanceForm.reset(); this.router.navigate(['/drilling/RigWise']) })
      }
    }
  }


  onDraft() {
    this.submitted = true;
    if (this.RigWisePerformanceForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftRigWisePerformanceDetails(this.RigWisePerformanceForm.value).subscribe(
          data => { this.Status, alert("Success"); this.RigWisePerformanceForm.reset(); this.router.navigate(['/drilling/RigWise']) })
      }
    }
  }

  onSearch() {
    this.submittedsearch = true;
    if (this.RigWisePerformanceSearch.invalid) {
      return;
    }
    else {
      this.drillingservice.SearchRigWisePerformance(this.RigWisePerformanceSearch.value).subscribe(
            data => {
            this.RigWisePerformance = data, console.log(this.RigWisePerformance),
              this.dataSource = new MatTableDataSource<RigWisePerformance>(this.RigWisePerformance),
              this.dataSource.paginator = this.paginator
          }
      )

    }
  }


  EditRigWiseDetails(RigWiseDetails: RigWisePerformance): void {
    let Status = RigWiseDetails.STATUS;
    if (Status == 'Submit') {
      localStorage.removeItem('EditRigWiseId');
      alert("Data Submited Not Editable");
    }
    else {
      this.btnUpdatevisibility = true;
      localStorage.removeItem('EditRigWiseId');
      localStorage.setItem('EditRigWiseId', RigWiseDetails.SL_NO.toString());
      let empid = localStorage.getItem('EditRigWiseId');
      if (+empid > 0) {
        this.btnSubmitvisibility = false;
        this.drillingservice.GetRigWiseDetailsId(+empid).subscribe(data => {
          this.RigWisePerformance = data,
          console.log(this.RigWisePerformance),
          this.RigWisePerformanceForm.controls['BLOCK_CATEGORY'].setValue(this.RigWisePerformance[0].BLOCK_CATEGORY);
          this.RigWisePerformanceForm.controls['Remarks'].setValue(this.RigWisePerformance[0].REMARKS);
          this.RigWisePerformanceForm.controls['YEAR'].setValue(this.RigWisePerformance[0].YEAR);
          this.RigWisePerformanceForm.controls['RIG_NAME'].setValue(this.RigWisePerformance[0].RIG_NAME);
          this.RigWisePerformanceForm.controls['RIG_TYPE'].setValue(this.RigWisePerformance[0].RIG_TYPE);
          this.RigWisePerformanceForm.controls['COMMERCIAL_DAYS'].setValue(this.RigWisePerformance[0].COMMERCIAL_DAYS);
          this.RigWisePerformanceForm.controls['COMMERCIAL_DEV'].setValue(this.RigWisePerformance[0].COMMERCIAL_DEV);
          this.RigWisePerformanceForm.controls['COMMERCIAL_EXP'].setValue(this.RigWisePerformance[0].COMMERCIAL_EXP);
          this.RigWisePerformanceForm.controls['COMMERCIAL_SPEED'].setValue(this.RigWisePerformance[0].COMMERCIAL_SPEED);
          this.RigWisePerformanceForm.controls['CYCLE_DAYS'].setValue(this.RigWisePerformance[0].CYCLE_DAYS);
          this.RigWisePerformanceForm.controls['CYCLE_DEV'].setValue(this.RigWisePerformance[0].CYCLE_DEV);
         // alert(this.RigWisePerformance[0].CYCLE_DEV);
          this.RigWisePerformanceForm.controls['CYCLE_EXP'].setValue(this.RigWisePerformance[0].CYCLE_EXP);
          this.RigWisePerformanceForm.controls['CYCLE_SPEED'].setValue(this.RigWisePerformance[0].CYCLE_SPEED);
          this.RigWisePerformanceForm.controls['DEV_MET'].setValue(this.RigWisePerformance[0].DEV_MET);
          this.RigWisePerformanceForm.controls['DEV_WELLS'].setValue(this.RigWisePerformance[0].DEV_WELLS);
          this.RigWisePerformanceForm.controls['EXP_MET'].setValue(this.RigWisePerformance[0].EXP_MET);
          this.RigWisePerformanceForm.controls['EXP_WELLS'].setValue(this.RigWisePerformance[0].EXP_WELLS);
          this.RigWisePerformanceForm.controls['MONTH'].setValue(this.RigWisePerformance[0].MONTH);
          this.RigWisePerformanceForm.controls['NONPRD_WELL_COMPLICATION_DAY'].setValue(this.RigWisePerformance[0].NONPRD_WELL_COMPLICATION_DAY);
          this.RigWisePerformanceForm.controls['NONPRD_WELL_COMPLICATION_PER'].setValue(this.RigWisePerformance[0].NONPRD_WELL_COMPLICATION_PER);
          this.RigWisePerformanceForm.controls['NONPRD_WELL_REPAIR_DAY'].setValue(this.RigWisePerformance[0].NONPRD_WELL_REPAIR_DAY);
          this.RigWisePerformanceForm.controls['NONPRD_WELL_REPAIR_PER'].setValue(this.RigWisePerformance[0].NONPRD_WELL_REPAIR_PER);
          this.RigWisePerformanceForm.controls['OPERATION_AREA_ID'].setValue(this.RigWisePerformance[0].OPERATION_AREA_ID);
          this.RigWisePerformanceForm.controls['OUTCYCLE_CAPREP'].setValue(this.RigWisePerformance[0].OUTCYCLE_CAPREP);
          this.RigWisePerformanceForm.controls['OUTCYCLE_OTH'].setValue(this.RigWisePerformance[0].OUTCYCLE_OTH);
          this.RigWisePerformanceForm.controls['RIG_MODE_TIME_BREAKUP_DR'].setValue(this.RigWisePerformance[0].RIG_MODE_TIME_BREAKUP_DR);
          this.RigWisePerformanceForm.controls['RIG_MODE_TIME_BREAKUP_PT'].setValue(this.RigWisePerformance[0].RIG_MODE_TIME_BREAKUP_PT);
          this.RigWisePerformanceForm.controls['RIG_MODE_TIME_BREAKUP_RB'].setValue(this.RigWisePerformance[0].RIG_MODE_TIME_BREAKUP_RB);
          this.RigWisePerformanceForm.controls['RIG_OPERATING_STATUS'].setValue(this.RigWisePerformance[0].RIG_OPERATING_STATUS);
         })
      }

    }
  }

  onDraftUpdate() {
    this.submitted = true;

    if (this.RigWisePerformanceForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftUpdateRigWiseDetails(this.RigWisePerformanceForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.RigWisePerformanceForm); this.RigWisePerformanceForm.reset();
            this.router.navigate(['/drilling/RigWise']);
          },
            error => () => {

            },
            () => console.log(this.RigWisePerformance)
          );
      }
    }
  }


  onDraftSubmit() {
    this.submitted = true;

    if (this.RigWisePerformanceForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.DraftSubmitRigWiseDetails(this.RigWisePerformanceForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.RigWisePerformance); this.RigWisePerformanceForm.reset();
            this.router.navigate(['/drilling/RigWise']);
          },
            error => () => {

            },
            () => console.log(this.RigWisePerformance)
          );
      }
    }
  }

}
