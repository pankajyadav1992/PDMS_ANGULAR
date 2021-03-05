import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators} from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MonthMaster, YearMaster, ddlList, OperatingStatus, OperationalArea, WellCatgory, BlockType, RigCountDetails } from '../../DrillingTable/drillingclass';
import { DrillingService } from '../../DrillingTable/DrillingServices';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'app-drilling-rigcount',
  templateUrl: './rigcount.component.html',
  styleUrls: ['./rigcount.component.css'],
})

export class RigCountComponent {
  btnSubmitvisibility: boolean = true;
  btnUpdatevisibility: boolean = false;
  Months: MonthMaster[];  
  Years: YearMaster[];
  WellCategory: WellCatgory[];
  BlockTypes: BlockType[];
  OperatingStatus: OperatingStatus[];
  OperationalArea: OperationalArea[];
  RigCountDetails: RigCountDetails[];
  ddlMaster: ddlList[];
  Status: string;
  RigCountForm: FormGroup;
  RigCountSearch: FormGroup;
  submitted = false;
  submittedsearch = false;

  dataSource;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
 
  displayedColumns: string[] = ['BlockTypes', 'Months', 'Years', 'OperatingStatus', 'WellCategory', 'OperationalArea', 'TotalOnshoreOffShore','Remarks','Status','Edit/View'];

  constructor(private formbuilder: FormBuilder, private drillingservice: DrillingService, private router: Router) {

  }
  
  ngOnInit() {
    this.GetDdl();
    this.RigCountForm = this.formbuilder.group({   
      BlockTypes: ['', Validators.required],
      Years: ['', Validators.required],
      Months: ['', Validators.required],
      WellCategory: ['', Validators.required],
      OperatingStatus: ['', Validators.required],
      OperationalArea: ['', Validators.required],
      TotalOnshoreOffShore: ['', Validators.required],
     Remarks: [],

    })
    //localStorage.removeItem('EditRigCountId');
    //let aa = localStorage.getItem('EditRigCountId');
    //if (+aa > 0) {
    //  this.btnSubmitvisibility = false;
    //  this.btnUpdatevisibility = true;
    //}

    this.RigCountSearch = this.formbuilder.group({
      BlockTypes: ['', Validators.required],
      Years: ['', Validators.required],
      Months: ['', Validators.required],
    })
  }
  get GetRequired() { return this.RigCountForm.controls; }
  get GetRequiredSearch() { return this.RigCountSearch.controls; }

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

  onSubmit()
  {
    this.submitted = true;
    if (this.RigCountForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.SubmitRigCountDetails(this.RigCountForm.value).subscribe(
          data => { this.Status, alert("Success"); this.RigCountForm.reset(); this.router.navigate(['/drilling/RigCount']) })
      }
    }
  }


  onDraft() {
    this.submitted = true;
    if (this.RigCountForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftRigCountDetails(this.RigCountForm.value).subscribe(
          data => { this.Status, alert(data); this.RigCountForm.reset(); this.router.navigate(['/drilling/RigCount']) })
      }
    }
  }


  onDraftUpdate() {
    this.submitted = true;

    if (this.RigCountForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Save As Draft this ?")) {
        this.drillingservice.DraftUpdateRigCountDetails(this.RigCountForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.RigCountDetails); this.RigCountForm.reset();
            this.router.navigate(['/drilling/RigCount']);
          },
            error => () => {

            },
            () => console.log(this.RigCountDetails)
          );
      }
    }
  }


   onDraftSubmit() {
    this.submitted = true;

    if (this.RigCountForm.invalid) {
      return;
    }
    else {
      if (confirm("Are you sure you want to Submit this Form ?")) {
        this.drillingservice.DraftSubmitRigCountDetails(this.RigCountForm.value)
          .subscribe(data => {
            this.Status, alert("Success");
            this.btnUpdatevisibility = false;
            this.btnSubmitvisibility = true;
            console.log(this.RigCountDetails); this.RigCountForm.reset();
            this.router.navigate(['/drilling/RigCount']);
          },
            error => () => {

            },
            () => console.log(this.RigCountDetails)
          );
      }
    }
  }


  EditRigCountDetails(riggetdetails: RigCountDetails): void {

    let Status = riggetdetails.Status;
    if (Status == 'Submit')
    {
      localStorage.removeItem('EditRigCountId');
      alert("Data Submited Not Editable");
    }
    else
    {
      this.btnUpdatevisibility = true;
      localStorage.removeItem('EditRigCountId');
      localStorage.setItem('EditRigCountId', riggetdetails.Id.toString());

      let empid = localStorage.getItem('EditRigCountId');
      if (+empid > 0) {
        this.btnSubmitvisibility = false;
        this.drillingservice.GetRigCountDetailsId(+empid).subscribe(data => {
          this.RigCountDetails = data,
            console.log(this.RigCountDetails),
            this.RigCountForm.controls['BlockTypes'].setValue(this.RigCountDetails[0].BlockTypes);
          this.RigCountForm.controls['Years'].setValue(this.RigCountDetails[0].Years);
          this.RigCountForm.controls['Months'].setValue(this.RigCountDetails[0].Months);
          this.RigCountForm.controls['OperatingStatus'].setValue(this.RigCountDetails[0].OperatingStatus);
          this.RigCountForm.controls['WellCategory'].setValue(this.RigCountDetails[0].WellCategory);
          this.RigCountForm.controls['OperationalArea'].setValue(this.RigCountDetails[0].OperationalArea);
          this.RigCountForm.controls['TotalOnshoreOffShore'].setValue(this.RigCountDetails[0].TotalOnshoreOffShore);
          this.RigCountForm.controls['Remarks'].setValue(this.RigCountDetails[0].Remarks);
          //this.MaterialForm.controls['ID'].setValue(this.materials[0].ID);
        })
      }

    }
  }
   



  onSearch() {
    this.submittedsearch = true;
    if (this.RigCountSearch.invalid) {
      return;
    }
    else {
      this.drillingservice.SearchRigCount(this.RigCountSearch.value).subscribe(
        data => {
          this.RigCountDetails = data, console.log(this.RigCountDetails),
            this.dataSource = new MatTableDataSource<RigCountDetails>(this.RigCountDetails),
            this.dataSource.paginator = this.paginator
        }
      )

    }
  }
}
