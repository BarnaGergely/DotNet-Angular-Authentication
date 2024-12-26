import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {
  apiUrl = environment.apiUrl + '/payments';

  constructor(private http: HttpClient) {

  }

  refreshList() {
    this.http.get(this.apiUrl).subscribe({
      next: (result) => {
        console.log(result);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
