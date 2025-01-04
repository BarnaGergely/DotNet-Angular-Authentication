import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { PaymentDetail } from './payment-detail.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {
  apiUrl = environment.apiUrl + '/payments';
  list: PaymentDetail[] = [];
  formData: PaymentDetail = new PaymentDetail();

  constructor(private http: HttpClient) {

  }

  refreshList() {
    this.http.get(this.apiUrl).subscribe({
      next: (response) => {
        this.list = response as PaymentDetail[];
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  postPaymentDetail() {
    console.log(this.formData);
    return this.http.post(this.apiUrl, this.formData);
  }

  putPaymentDetail() {
    console.log(this.formData);
    return this.http.put(`${this.apiUrl}/${this.formData.paymentDetailId}`, this.formData);
  }

  deletePaymentDetail(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.formData = new PaymentDetail();
  }
}
