import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MonthMaster, WellField, YearMaster, State, WellBlock, WellType, ddlList, OperatingStatus, OperationalArea, WellCatgory, BlockType, WellWisePerformance } from '../../DrillingTable/drillingclass';
import { DrillingService } from '../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';
@Component({
  selector: 'App-Drilling-WellWise',
  styleUrls: ['./wellwise.component.css'],
  templateUrl: './wellwise.component.html',
})
export class WellWiseComponent {
  btnSubmitvisibility: boolean = true;
  btnUpdatevisibility: boolean = false;
  //Block_Field_Id: boolean = false;
  Months: MonthMaster[];
  Years: YearMaster[];
  WellCategory: WellCatgory[];
  BlockTypes: BlockType[];
  OperatingStatus: OperatingStatus[];
  OperationalArea: OperationalArea[];
  StateList: State[];
  WellBlocks: WellBlock[];
  WellTypes: WellType[];
  WellField: WellField[];
  WellWiseDetails: WellWisePerformance[];
  ddlMaster: ddlList[];
  Status: string;
  WellWiseForm: FormGroup;
  WellWiseSearch: FormGroup;
  submitted = false;
  submittedsearch = false;

  dataSource;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  displayedColumns: string[] = ['BLOCK CATEGORY', 'MONTH', 'YEAR', 'Blocktype', 'BlockField', 'WELL NAME', 'OPERATIONAL AREA', 'WELL CATEGORY', 'WELL TYPE', 'Fields','State', 'Status',  'Edit/View'];

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {

  }

  ngOnInit() {
    this.GetDdl();
    this.WellWiseForm = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],// All And Nomination Block
      BLOCK_TYPE: ['', Validators.required], //Well Wise Block
      WELL_YEAR: ['', Validators.required],
      WELL_MONTH: ['', Validators.required],
      WELL_CATEGORY_ID: ['', Validators.required],
      BLOCK_ID: ['', Validators.required],//Block/Field Id
      OPERATIONAL_AREA: ['', Validators.required],
      WELL_TYPE_ID: ['', Validators.required],
      STATE: ['', Validators.required],
      WELL_NAME: ['', Validators.required],
      //S_LONGITUDE: ['0'], S_LATITUDE: ['0'], SS_LATITUDE: ['0'], SS_LONGITUDE: ['0'], RIG_NAME: [''], WATER_DEPTH: ['0'], TARGET_DEPTH_MD: ['0'],
      //TARGET_DEPTH_TVD: ['0'], DRILLED_DEPTH_MD: ['0'], DRILLED_DEPTH_TVD: ['0'],      SPUD_DATE_TIME: [],      HER_DATE_TIME: [],
      //RR_DATE_TIME: [], CP_PLANNED: ['0'], CP_ACTUAL: ['0'], RB_PLANNED: ['0'], RB_ACTUAL: ['0'], DR_PLANNED: ['0'],
      //DR_ACTUAL: ['0'], PT_PLANED: ['0'], PT_ACTUAL: ['0'], CYCLE_SPEED: ['0'], COMMERCIAL_SPPED: ['0'],
      S_LONGITUDE: [''], S_LATITUDE: [''], SS_LATITUDE: [''], SS_LONGITUDE: [''], RIG_NAME: ['', Validators.required], WATER_DEPTH: [''], TARGET_DEPTH_MD: [''],
      TARGET_DEPTH_TVD: [''], DRILLED_DEPTH_MD: [''], DRILLED_DEPTH_TVD: [''], SPUD_DATE_TIME: [], HER_DATE_TIME: [],
      RR_DATE_TIME: [], CP_PLANNED: [''], CP_ACTUAL: [''], RB_PLANNED: [''], RB_ACTUAL: [''], DR_PLANNED: [''],
      DR_ACTUAL: [''], PT_PLANED: [''], PT_ACTUAL: [''], CYCLE_SPEED: [''], COMMERCIAL_SPPED: [''],
      REMARKS: ['']
    })
    //localStorage.removeItem('EditWellWiseId');
    //let aa = localStorage.getItem('EditWellWiseId');
    //if (+aa > 0) {
    //  this.btnSubmitvisibility = false;
    //  this.btnUpdatevisibility = true;
    //}

    this.WellWiseSearch = this.formbuilder.group({
      BLOCK_CATEGORY: ['', Validators.required],
      WELL_MONTH: ['', Validators.required], 
      WELL_YEAR: ['', Validators.required],
    })
  }
  get GetRequired() { return this.WellWiseForm.controls; }
  get GetRequiredSearch() { return this.WellWiseSearch.controls; }



  GetDdl() {
    this.drillingservice.GetRigCountAllList().subscribe(data => {
      this.ddlMaster = data;
      this.Months = this.ddlMaster[0].Months;
      this.Years = this.ddlMaster[0].Years;
      this.WellCategory = this.ddlMaster[0].WellCatgory;
      this.OperationalArea = this.ddlMaster[0].OperationalArea;
      this.OperatingStatus = this.ddlMaster[0].OperatingStatus;
      this.BlockTypes = this.ddlMaster[0].BlockType;
      this.StateList = this.ddlMaster[0].States;
      this.WellTypes = this.ddlMaster[0].WellTypes;
      this.WellBlocks = this.ddlMaster[0].WellBlocks;
      //console.log(this.WellTypes);
      //console.log(this.WellBlocks);
    })

  }

  wellblockchange(value: number) {
   // alert(value);   
    this.drillingservice.GetBlockField(value).subscribe(data => {
      this.WellField = data; console.log(this.WellField);
      if (this.WellField.length > 0) {
        //this.Block_Field_Id = true;
      }
      else {
        //this.Block_Field_Id = false;
        this.WellWiseForm.controls['BLOCK_ID'].setValue(null);
      
      }
    })

  }

  onSubmit() {
    this.submitted = true;
    if (this.WellWiseForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.SubmitWellWiseDetails(this.WellWiseForm.value).subscribe(
          data => { this.Status, alert("Success"); this.WellWiseForm.reset(); this.router.navigate(['/drilling/WellWise']) })
      }
    }
  }


  onDraft() {
    this.submitted = true;
    if (this.WellWiseForm.invalid) {
      console.log(this.WellWiseForm.value)
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftWellWiseDetails(this.WellWiseForm.value).subscribe(
          data => { this.Status, alert("Success"); this.WellWiseForm.reset(); this.router.navigate(['/drilling/WellWise']) })
      }
    }
  }


 


  EditWellWiseDetails(WellWiseDetails: WellWisePerformance): void {
    let Status = WellWiseDetails.STATUS;
    if (Status == 'Submit') {
      localStorage.removeItem('EditWellWiseId');
      alert("Data Submited Not Editable");
    }
    else {
      this.btnUpdatevisibility = true;
      localStorage.removeItem('EditWellWiseId');
      localStorage.setItem('EditWellWiseId', WellWiseDetails.SL_NO.toString());

      let empid = localStorage.getItem('EditWellWiseId');
      if (+empid > 0) {
        this.btnSubmitvisibility = false;
        this.drillingservice.GetWellWiseDetailsId(+empid).subscribe(data => {
          this.WellWiseDetails = data,
            console.log(this.WellWiseDetails),
          this.WellWiseForm.controls['BLOCK_CATEGORY'].setValue(this.WellWiseDetails[0].BLOCK_CATEGORY);
          this.WellWiseForm.controls['WELL_CATEGORY_ID'].setValue(this.WellWiseDetails[0].WELL_CATEGORY_ID);
          this.WellWiseForm.controls['WELL_MONTH'].setValue(this.WellWiseDetails[0].WELL_MONTH);
          this.WellWiseForm.controls['OPERATIONAL_AREA'].setValue(this.WellWiseDetails[0].OPERATIONAL_AREA);
          this.WellWiseForm.controls['WELL_TYPE_ID'].setValue(this.WellWiseDetails[0].WELL_TYPE_ID);
          this.WellWiseForm.controls['STATE'].setValue(this.WellWiseDetails[0].STATE);
          this.WellWiseForm.controls['WELL_YEAR'].setValue(this.WellWiseDetails[0].WELL_YEAR);          
          this.WellWiseForm.controls['COMMERCIAL_SPPED'].setValue(this.WellWiseDetails[0].COMMERCIAL_SPPED);
          this.WellWiseForm.controls['CP_ACTUAL'].setValue(this.WellWiseDetails[0].CP_ACTUAL);
          this.WellWiseForm.controls['CP_PLANNED'].setValue(this.WellWiseDetails[0].CP_PLANNED);
          this.WellWiseForm.controls['CYCLE_SPEED'].setValue(this.WellWiseDetails[0].CYCLE_SPEED);
          this.WellWiseForm.controls['WELL_NAME'].setValue(this.WellWiseDetails[0].WELL_NAME);
          this.WellWiseForm.controls['DRILLED_DEPTH_MD'].setValue(this.WellWiseDetails[0].DRILLED_DEPTH_MD);
          this.WellWiseForm.controls['DRILLED_DEPTH_TVD'].setValue(this.WellWiseDetails[0].DRILLED_DEPTH_TVD);
          this.WellWiseForm.controls['DR_ACTUAL'].setValue(this.WellWiseDetails[0].DR_ACTUAL);
          this.WellWiseForm.controls['DR_PLANNED'].setValue(this.WellWiseDetails[0].DR_PLANNED);  
          this.WellWiseForm.controls['PT_ACTUAL'].setValue(this.WellWiseDetails[0].PT_ACTUAL);
          this.WellWiseForm.controls['PT_PLANED'].setValue(this.WellWiseDetails[0].PT_PLANED);
          this.WellWiseForm.controls['RB_ACTUAL'].setValue(this.WellWiseDetails[0].RB_ACTUAL);
          this.WellWiseForm.controls['RB_PLANNED'].setValue(this.WellWiseDetails[0].RB_PLANNED);
          this.WellWiseForm.controls['REMARKS'].setValue(this.WellWiseDetails[0].REMARKS);
          this.WellWiseForm.controls['RIG_NAME'].setValue(this.WellWiseDetails[0].RIG_NAME);
          this.WellWiseForm.controls['HER_DATE_TIME'].setValue(this.WellWiseDetails[0].HER_DATE_TIME);
          this.WellWiseForm.controls['RR_DATE_TIME'].setValue(this.WellWiseDetails[0].RR_DATE_TIME);
          this.WellWiseForm.controls['SPUD_DATE_TIME'].setValue(this.WellWiseDetails[0].SPUD_DATE_TIME);
          this.WellWiseForm.controls['SS_LATITUDE'].setValue(this.WellWiseDetails[0].SS_LATITUDE);
          this.WellWiseForm.controls['SS_LONGITUDE'].setValue(this.WellWiseDetails[0].SS_LONGITUDE);
          this.WellWiseForm.controls['S_LATITUDE'].setValue(this.WellWiseDetails[0].S_LATITUDE);
          this.WellWiseForm.controls['S_LONGITUDE'].setValue(this.WellWiseDetails[0].S_LONGITUDE);
          this.WellWiseForm.controls['BLOCK_ID'].setValue(this.WellWiseDetails[0].BLOCK_ID);
          //alert(this.WellWiseDetails[0].BLOCK_ID);
          this.WellWiseForm.controls['BLOCK_TYPE'].setValue(this.WellWiseDetails[0].BLOCK_TYPE);
          this.WellWiseForm.controls['TARGET_DEPTH_MD'].setValue(this.WellWiseDetails[0].TARGET_DEPTH_MD);
          this.WellWiseForm.controls['TARGET_DEPTH_TVD'].setValue(this.WellWiseDetails[0].TARGET_DEPTH_TVD);
          this.WellWiseForm.controls['WATER_DEPTH'].setValue(this.WellWiseDetails[0].WATER_DEPTH);
             
         
        })
      }

    }
  }




  onSearch() {
    this.submittedsearch = true;
    if (this.WellWiseSearch.invalid) {
      return;
    }
    else {
      this.drillingservice.SearchWellWise(this.WellWiseSearch.value).subscribe(
        data => {
          this.WellWiseDetails = data, console.log(this.WellWiseDetails),
            this.dataSource = new MatTableDataSource<WellWisePerformance>(this.WellWiseDetails),
            this.dataSource.paginator = this.paginator
        }
      )

    }
  }

  onDraftUpdate() {
    this.submitted = true;

    if (this.WellWiseForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftUpdateWellWiseDetails(this.WellWiseForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.WellWiseForm); this.WellWiseForm.reset();
            this.router.navigate(['/drilling/WellWise']);
          },
            error => () => {

            },
            () => console.log(this.WellWiseDetails)
          );
      }
    }
  }


  onDraftSubmit() {
    this.submitted = true;

    if (this.WellWiseForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.DraftSubmitWellWiseDetails(this.WellWiseForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.WellWiseForm); this.WellWiseForm.reset();
            this.router.navigate(['/drilling/WellWise']);
          },
            error => () => {

            },
            () => console.log(this.WellWiseDetails)
          );
      }
    }
  }
}

