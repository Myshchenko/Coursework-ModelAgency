import { Component, Input } from '@angular/core';
import { SortedReportData } from 'src/app/models/SortedReportData';

@Component({
  selector: 'app-report-card',
  templateUrl: './report-card.component.html',
  styleUrls: ['./report-card.component.css']
})
export class ReportCardComponent {

  @Input() report: SortedReportData | undefined;
}
