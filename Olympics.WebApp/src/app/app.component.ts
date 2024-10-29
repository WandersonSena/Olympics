import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgbNavModule  } from '@ng-bootstrap/ng-bootstrap';
import { CountryComponent } from '../app/public/country/country.component';
import { OlympicComponent } from '../app/public/olympic/olympic.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgbNavModule, CountryComponent, OlympicComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Olympics.WebApp';
  active = 1;
}
