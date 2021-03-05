import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RigCountComponent } from './rigcount/rigcount.component';
import { AnnualDrillingComponent } from './annualdrilling/annualdrilling.component';
import { DPRComponent } from './dpr/dpr.component';
import { WellWiseComponent } from './wellwise/wellwise.component';
import { CumulativeDrillingComponent } from './cumulativedrilling/cumulativedrilling.component';
import { RigWiseComponent } from './rigwiseperformance/rigwiseperformance.component';

import { DrillingService } from '../DrillingTable/DrillingServices';

import { RigCountExcelComponent } from './RigCount/RigCountExcel/rigcountexcel.component';
import { AnnualDrillingExcelComponent } from './annualdrilling/AnnualDrillingExcel/annualdrillingwxcel.component';
import { DPRExcelComponent } from './dpr/DPRExcel/dprexcel.component';
import { WellWiseExcelComponent } from './wellwise/WellWiseExcel/wellwiseexcel.component';
import { CumulativeDrillingExcelComponent } from './cumulativedrilling/CumulativeDrillingExcel/cumulativedrillingexcel.component';
import { RigWiseExcelComponent } from './rigwiseperformance/RigWisePerformanceExcel/rigwiseperformanceexcel.component'

const routes: Routes = [
  { path: 'RigCount', component: RigCountComponent, data: { title: 'RigCount Form' } },
  { path: 'AnnualDrilling', component: AnnualDrillingComponent, data: { title: 'Annual Drilling Form' } },
  { path: 'DPR', component: DPRComponent, data: { title: 'DPR Form' } },
  { path: 'WellWise', component: WellWiseComponent, data: { title: 'Well Wise Form' } },
  { path: 'CumulativeDrilling', component: CumulativeDrillingComponent, data: { title: 'Cumulative Drilling Form' } },
  { path: 'RigWise', component: RigWiseComponent, data: { title: 'RigWise Form ' } },

  { path: 'RigCountExcel', component: RigCountExcelComponent, data: { title: 'RigCount Excel' } },
  { path: 'AnnualDrillingExcel', component: AnnualDrillingExcelComponent, data: { title: 'Annual Drilling Excel' } },
  { path: 'DPRExcel', component: DPRExcelComponent, data: { title: 'DPR Excel' } },
  { path: 'WellWiseExcel', component: WellWiseExcelComponent, data: { title: 'Well Wise Excel' } },
  { path: 'CumulativeDrillingExcel', component: CumulativeDrillingExcelComponent, data: { title: 'Cumulative Drilling Excel' } },
  { path: 'RigWiseExcel', component: RigWiseExcelComponent, data: { title: 'RigWise Excel ' } },
 
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [DrillingService]
})
export class DrilingRoutingModule { }
