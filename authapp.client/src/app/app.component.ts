import { Component } from '@angular/core';
import { WeatherForecastComponent } from "./weather-forecast/weather-forecast.component";
import { PaymentDetailsComponent } from "./payment-details/payment-details.component";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [WeatherForecastComponent, PaymentDetailsComponent]
})
export class AppComponent {
 
}
