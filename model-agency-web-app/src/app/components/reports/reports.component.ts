import { Component } from '@angular/core';
import { SortedReportData } from 'src/app/models/SortedReportData';
import { ReportService } from 'src/app/services/ReportService';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
//import * as RobotoRegular from 'src/assets/fonts/Roboto-Regular.ttf';
//import RobotoRegular from '../../assets/fonts/Roboto-Regular.ttf';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent {
  title:string;
  myHero:string;
  heroes: any[];

  sortedReportData: SortedReportData[] | undefined;

  constructor(private reportService: ReportService) {
     this.title = 'Tour of Heros';
     this.heroes=['Windstorm','Bombasto','Magneta', 'Windstorm','Bombasto','Magneta']
     this.myHero = this.heroes[0];
  }

  ngOnInit(): void {
    this.loadReportData();
  }

  
  loadReportData(){
    
    this.reportService.uploadReportData().subscribe({
     next: response => {
       this.sortedReportData = response;
       console.log(this.sortedReportData)
     }
   })    
 }

 generatePDF() {
  (pdfMake as any).vfs = pdfFonts.pdfMake.vfs;

  const contentArray = this.sortedReportData!.flatMap(item => [
    { text: item.details, fontSize: 14 },
    { text: item.address, fontSize: 14 },
    { text: item.targetDate.toString(), fontSize: 14 },
    { text: `Прийняли участь: ${item.acceptedUsers.join(', ')}`, fontSize: 14 },
  ]);
  
  const documentDefinition = {
    content: [
      { text: 'Список підтверджених моделей-учасниць.', fontSize: 16 },
      ...contentArray,
    ],
  };

  const pdf = pdfMake.createPdf(documentDefinition);

  pdf.download('AcceptedList.pdf');
}
}
