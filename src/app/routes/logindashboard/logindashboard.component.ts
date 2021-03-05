
import {  Component,  OnInit,  AfterViewInit,  OnDestroy,  ChangeDetectionStrategy,  ChangeDetectorRef,  NgZone,} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../dashboard/dashboard.srevice';
@Component({
  selector: 'app-logindashboard',
  templateUrl: './logindashboard.component.html',
  providers: [DashboardService],
})
export class LoginDashboardComponent implements OnInit {

  constructor(private router: Router
  ) { }

  ngOnInit()
  {
   // alert();
    this.router.navigate(['/auth/login']);
  }
}
