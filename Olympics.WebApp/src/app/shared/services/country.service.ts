import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { catchError } from "rxjs/operators";
import { throwError } from "rxjs";

import { environment } from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  baseUrl: string = environment.olympicsApiUrl + '/api/Country/';
  constructor(private httpClient: HttpClient) {

  }

  public GetCountryResults(querryString: string, onSuccess: Function, onFailure: Function) {
    this.httpClient.get<any>(this.baseUrl + querryString)
      .pipe(catchError(error => {
        onFailure(error);
        return throwError(error);
      }))
      .subscribe(data => onSuccess(data));
  }
}
