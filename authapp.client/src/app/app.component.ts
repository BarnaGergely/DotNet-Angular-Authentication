import { Component } from '@angular/core';
import { WeatherForecastComponent } from "./weather-forecast/weather-forecast.component";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [WeatherForecastComponent]
})
export class AppComponent {
 
}
