import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { DrilingRoutingModule } from './drilling-routing.module';

import { RigCountComponent } from './rigcount/rigcount.component';
import { AnnualDrillingComponent } from './annualdrilling/annualdrilling.component';
import { DPRComponent } from './dpr/dpr.component';
import { WellWiseComponent } from './wellwise/wellwise.component';
import { CumulativeDrillingComponent } from './cumulativedrilling/cumulativedrilling.component';
import { RigWiseComponent } from './rigwiseperformance/rigwiseperformance.component'

import { RigCountExcelComponent } from './RigCount/RigCountExcel/rigcountexcel.component';
import { AnnualDrillingExcelComponent } from './annualdrilling/AnnualDrillingExcel/annualdrillingwxcel.component';
import { DPRExcelComponent } from './dpr/DPRExcel/dprexcel.component';
import { WellWiseExcelComponent } from './wellwise/WellWiseExcel/wellwiseexcel.component';
import { CumulativeDrillingExcelComponent } from './cumulativedrilling/CumulativeDrillingExcel/cumulativedrillingexcel.component';
import { RigWiseExcelComponent } from './rigwiseperformance/RigWisePerformanceExcel/rigwiseperformanceexcel.component'

const COMPONENTS = [RigCountComponent, AnnualDrillingComponent, DPRComponent, WellWiseComponent, CumulativeDrillingComponent, RigWiseComponent,
  RigCountExcelComponent,  AnnualDrillingExcelComponent,  DPRExcelComponent,  WellWiseExcelComponent, CumulativeDrillingExcelComponent, RigWiseExcelComponent
];
const COMPONENTS_DYNAMIC = [];

@NgModule({
  imports: [SharedModule, DrilingRoutingModule],
  declarations: [...COMPONENTS, ...COMPONENTS_DYNAMIC],
  entryComponents: COMPONENTS_DYNAMIC,
})
export class DrillingModule {
}
