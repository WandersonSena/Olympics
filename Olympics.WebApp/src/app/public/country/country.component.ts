import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

import { CountryService } from '../../shared/services/country.service';

@Component({
  selector: 'app-country',
  standalone: true,
  imports: [NgbCollapseModule, FormsModule],
  templateUrl: './country.component.html',
  styleUrl: './country.component.css'
})

export class CountryComponent {
  isCollapsed = false;
  queryString = "POR";
  searchResult = "";

  constructor(private countryService: CountryService) {

  }

  searchCountryResults() {
    this.countryService.GetCountryResults(this.queryString, (data:any) => {
      this.searchResult = JSON.stringify(data, undefined, 4);
      console.log(data)
    }, () => {});
  }
}
