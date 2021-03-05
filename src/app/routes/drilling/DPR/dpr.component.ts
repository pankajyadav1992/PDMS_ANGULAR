import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ddlListDPR, OperatingStatus, OperationalArea, WellCatgory, DPR, State, WellType, PhaseMaster, DPRField } from '../../DrillingTable/drillingclass';
import { DrillingService } from '../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'App-Drilling-DPR',
  templateUrl: './dpr.component.html',
  styleUrls: ['./dpr.component.css'],
})
export class DPRComponent {
  btnSubmitvisibility: boolean = true;
  btnUpdatevisibility: boolean = false;
  OperationalArea: OperationalArea[];
  StateList: State[];
  OperatingStatus: OperatingStatus[];
  WellCategory: WellCatgory[];
  WellType: WellType[];
  PhaseType: PhaseMaster[];
  DPR: DPR[];  
  ddlListDPR: ddlListDPR[];
  DPRField: DPRField[];
  Status: string;
  dprForm: FormGroup;
  dprSearch: FormGroup;
  submitted = false;
  submittedsearch = false;

  dataSource;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  displayedColumns: string[] = ['dprdate', 'Field','OperatingStatus', 'state', 'RigName', 'OperatingStatus', 'WellName', 'Status', 'Edit/View'];

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {
  }

  ngOnInit() {
    this.GetDdl();
    this.dprForm = this.formbuilder.group({     
      WELL_CATEGORY_ID: ['', Validators.required],
      WELL_TYPE_ID: ['', Validators.required],
      OPERATION_AREA_ID: ['', Validators.required],
      STATE: ['', Validators.required],
      BLOCK_ID: ['', Validators.required],
      RIG_OPERATOR_STATUS_ID: ['', Validators.required],
      DPR_DATE: ['', Validators.required],
      PHASE_ID: ['', Validators.required],
      WELL_NAME: ['', Validators.required],
      RIG_NAME: ['', Validators.required],
      //LONGITUDE: ['0'],  LATITUDE: ['0'], TARGET_DEPTH: ['0'], PRESENT_DEPTH: ['0'], METARGE: ['0'],
      LONGITUDE: [''], LATITUDE: [''], TARGET_DEPTH: [''], PRESENT_DEPTH: [''], METARGE: [''],
      DPR_BRIEF: [''], SPUD_DATE:[]

    })
   

    this.dprSearch = this.formbuilder.group({
      DPR_DATE: ['', Validators.required]
    
    })
  }
  get GetRequired() { return this.dprForm.controls; }
  get GetRequiredSearch() { return this.dprSearch.controls; }

  

  GetDdl() {
    this.drillingservice.GetDPRList().subscribe(data => {
      this.ddlListDPR = data;
      this.WellType = this.ddlListDPR[0].WellTypes;
      this.WellCategory = this.ddlListDPR[0].WellCatgory;
      this.PhaseType = this.ddlListDPR[0].PhaseMaster;
      this.StateList = this.ddlListDPR[0].States;
      this.OperatingStatus = this.ddlListDPR[0].OperatingStatus;
      this.OperationalArea = this.ddlListDPR[0].OperationalArea;
      this.DPRField = this.ddlListDPR[0].DPRFields;
      console.log(this.DPRField);
    })

  }

  onSubmit() {
    this.submitted = true;
    if (this.dprForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.SubmitDPRDetails(this.dprForm.value).subscribe(
          data => { this.Status, alert("Success"); this.dprForm.reset(); this.router.navigate(['/drilling/DPR']) })
      }
    }
  }


  onDraft() {
    this.submitted = true;
    if (this.dprForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftDPRDetails(this.dprForm.value).subscribe(
          data => { this.Status, alert("Success"); this.dprForm.reset(); this.router.navigate(['/drilling/DPR']) })
      }
    }
  }


  onDraftUpdate() {
    this.submitted = true;

    if (this.dprForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftUpdateDPRDetails(this.dprForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.DPR); this.dprForm.reset(); this.router.navigate(['/drilling/DPR']);
          },
            error => () => {

            },
            () => console.log(this.DPR)
          );
      }
    }
  }


  onDraftSubmit() {
    this.submitted = true;
    if (this.dprForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.DraftSubmitDPRDetails(this.dprForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.DPR); this.dprForm.reset(); this.router.navigate(['/drilling/DPR']);
          },
            error => () => {
            },
            () => console.log(this.DPR)
          );
      }
    }
  }


  EditRigCountDetails(dpr: DPR): void {

    let Status = dpr.STATUS;
    if (Status == 'Submit') {
      localStorage.removeItem('EditDPRId');
      alert("Data Submited Not Editable");
    }
    else {
      this.btnUpdatevisibility = true;
      localStorage.removeItem('EditDPRId');
      localStorage.setItem('EditDPRId', dpr.DPR_ID.toString());

      let empid = localStorage.getItem('EditDPRId');
     // alert(empid);
      if (+empid > 0) {
        this.btnSubmitvisibility = false;
        this.drillingservice.GetDPRDetailsId(+empid).subscribe(data => {
          this.DPR = data,
            console.log(this.DPR),
            this.dprForm.controls['DPR_DATE'].setValue(this.DPR[0].DPR_DATE);
          this.dprForm.controls['BLOCK_ID'].setValue(this.DPR[0].BLOCK_ID);
          this.dprForm.controls['OPERATION_AREA_ID'].setValue(this.DPR[0].OPERATION_AREA_ID);
          this.dprForm.controls['STATE'].setValue(this.DPR[0].STATE);
          this.dprForm.controls['RIG_NAME'].setValue(this.DPR[0].RIG_NAME);
          this.dprForm.controls['WELL_NAME'].setValue(this.DPR[0].WELL_NAME);
          this.dprForm.controls['RIG_OPERATOR_STATUS_ID'].setValue(this.DPR[0].RIG_OPERATOR_STATUS_ID);
          this.dprForm.controls['WELL_NAME'].setValue(this.DPR[0].WELL_NAME);
          this.dprForm.controls['LATITUDE'].setValue(this.DPR[0].LATITUDE);
          this.dprForm.controls['LONGITUDE'].setValue(this.DPR[0].LONGITUDE);
          this.dprForm.controls['WELL_CATEGORY_ID'].setValue(this.DPR[0].WELL_CATEGORY_ID);
          this.dprForm.controls['WELL_TYPE_ID'].setValue(this.DPR[0].WELL_TYPE_ID);
          this.dprForm.controls['SPUD_DATE'].setValue(this.DPR[0].SPUD_DATE);
          this.dprForm.controls['PHASE_ID'].setValue(this.DPR[0].PHASE_ID);
          this.dprForm.controls['TARGET_DEPTH'].setValue(this.DPR[0].TARGET_DEPTH);
          this.dprForm.controls['PRESENT_DEPTH'].setValue(this.DPR[0].PRESENT_DEPTH);
          this.dprForm.controls['METARGE'].setValue(this.DPR[0].METARGE);
          this.dprForm.controls['DPR_BRIEF'].setValue(this.DPR[0].DPR_BRIEF);
          
        })
      }

    }
  }

  onSearch() {
    this.submittedsearch = true;
    if (this.dprSearch.invalid) {
      return;
    }
    else {
      this.drillingservice.SearchDPR(this.dprSearch.value).subscribe(
        data => {
          this.DPR = data, console.log(this.DPR),
            this.dataSource = new MatTableDataSource<DPR>(this.DPR),
            this.dataSource.paginator = this.paginator
        }
      )

    }
  }
}
