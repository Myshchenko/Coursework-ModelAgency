import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environment';
import { Model } from '../models/Model';
import { SortedReportData } from '../models/SortedReportData';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  uploadReportData(){
    return this.http.get<SortedReportData[]>(this.baseUrl + 'report');
  }
}
