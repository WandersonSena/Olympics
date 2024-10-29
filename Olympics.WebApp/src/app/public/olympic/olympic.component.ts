import { Component } from '@angular/core';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

import { OlympicsService } from '../../shared/services/olympics.service';

@Component({
  selector: 'app-olympic',
  standalone: true,
  imports: [NgbCollapseModule, FormsModule],
  templateUrl: './olympic.component.html',
  styleUrl: './olympic.component.css'
})
export class OlympicComponent {
  isCollapsed = false;
  queryString = "1972";
  searchResult = "";

  constructor(private olympicsService: OlympicsService) {

  }

  GetOlympicYearResults() {
    this.olympicsService.GetOlympicYearResults(this.queryString, (data:any) => {
      this.searchResult = JSON.stringify(data, undefined, 4);
      console.log(data)
    }, () => {});
  }
}
