import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { catchError } from "rxjs/operators";
import { throwError } from "rxjs";

import { environment } from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class OlympicsService {

  baseUrl: string = environment.olympicsApiUrl + '/api/Olympics/';
  constructor(private httpClient: HttpClient) {

  }

  public GetOlympicYearResults(year: string, onSuccess: Function, onFailure: Function) {
    this.httpClient.get<any>(this.baseUrl + year)
      .pipe(catchError(error => {
        onFailure(error);
        return throwError(error);
      }))
      .subscribe(data => onSuccess(data));
  }
}
