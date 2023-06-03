import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environment';
import { Model } from '../models/Model';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getModels(){
    return this.http.get<Model[]>(this.baseUrl + 'models');
  }
}
